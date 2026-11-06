param(
    [Parameter(Mandatory = $true)]
    [string]$EncryptedBackupPath,
    [string]$ServerInstance = ".\SQLEXPRESS",
    [string]$TargetDatabase = "DBSistema_Agricola_RestoreTest",
    [string]$KeyPath = "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos\keys\backup.key",
    [string]$WorkDir = "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos\restore",
    [string]$SqlCmdPath = "sqlcmd",
    [switch]$ReplaceExisting,
    [switch]$RestoreProduction
)

$ErrorActionPreference = "Stop"

function Invoke-Sql {
    param([string]$Query)
    & $SqlCmdPath -S $ServerInstance -d master -E -b -Q $Query
    if ($LASTEXITCODE -ne 0) {
        throw "sqlcmd fallo con codigo $LASTEXITCODE."
    }
}

function Invoke-SqlScalar {
    param([string]$Query)
    $output = & $SqlCmdPath -S $ServerInstance -d master -E -b -h -1 -W -Q $Query
    if ($LASTEXITCODE -ne 0) {
        throw "sqlcmd fallo con codigo $LASTEXITCODE."
    }

    $lines = @($output | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    if ($lines.Count -eq 0) {
        throw "La consulta SQL no devolvio ningun valor."
    }
    return $lines[0].ToString().Trim()
}

function Invoke-SqlRows {
    param([string]$Query)
    $output = & $SqlCmdPath -S $ServerInstance -d master -E -b -h -1 -W -s "|" -Q $Query
    if ($LASTEXITCODE -ne 0) {
        throw "sqlcmd fallo con codigo $LASTEXITCODE."
    }

    return $output | Where-Object {
        -not [string]::IsNullOrWhiteSpace($_) -and
        $_ -notmatch "^\(\d+ rows? affected\)$"
    }
}

function Escape-SqlLiteral {
    param([string]$Value)
    return $Value.Replace("'", "''")
}

function Test-BytesEqual {
    param([byte[]]$A, [byte[]]$B)
    if ($A.Length -ne $B.Length) {
        return $false
    }

    $diff = 0
    for ($i = 0; $i -lt $A.Length; $i++) {
        $diff = $diff -bor ($A[$i] -bxor $B[$i])
    }

    return $diff -eq 0
}

function Unprotect-FileAes {
    param(
        [string]$InputPath,
        [string]$OutputPath,
        [byte[]]$Key
    )

    $data = [System.IO.File]::ReadAllBytes($InputPath)
    $header = [System.Text.Encoding]::ASCII.GetBytes("SAGBAK1")
    if ($data.Length -le ($header.Length + 16 + 32)) {
        throw "El respaldo cifrado no tiene un formato valido."
    }

    for ($i = 0; $i -lt $header.Length; $i++) {
        if ($data[$i] -ne $header[$i]) {
            throw "El respaldo cifrado no tiene la firma esperada."
        }
    }

    $payloadLength = $data.Length - 32
    $payload = New-Object byte[] $payloadLength
    $tag = New-Object byte[] 32
    [Array]::Copy($data, 0, $payload, 0, $payloadLength)
    [Array]::Copy($data, $payloadLength, $tag, 0, 32)

    $hmac = [System.Security.Cryptography.HMACSHA256]::new($Key)
    $expected = $hmac.ComputeHash($payload)
    $hmac.Dispose()

    if (-not (Test-BytesEqual -A $tag -B $expected)) {
        throw "La verificacion HMAC fallo. El respaldo o la clave no son validos."
    }

    $iv = New-Object byte[] 16
    [Array]::Copy($payload, $header.Length, $iv, 0, 16)
    $cipherLength = $payload.Length - $header.Length - 16
    $cipher = New-Object byte[] $cipherLength
    [Array]::Copy($payload, $header.Length + 16, $cipher, 0, $cipherLength)

    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.KeySize = 256
    $aes.Mode = [System.Security.Cryptography.CipherMode]::CBC
    $aes.Padding = [System.Security.Cryptography.PaddingMode]::PKCS7
    $aes.Key = $Key
    $aes.IV = $iv
    $decryptor = $aes.CreateDecryptor()
    $plain = $decryptor.TransformFinalBlock($cipher, 0, $cipher.Length)
    $decryptor.Dispose()
    $aes.Dispose()
    [System.IO.File]::WriteAllBytes($OutputPath, $plain)
}

if (-not (Test-Path -LiteralPath $EncryptedBackupPath)) {
    throw "No existe el respaldo cifrado: $EncryptedBackupPath"
}

if (-not (Test-Path -LiteralPath $KeyPath)) {
    throw "No existe la clave de cifrado: $KeyPath"
}

if ($TargetDatabase -eq "DBSistema_Agricola" -and -not $RestoreProduction) {
    throw "Para restaurar sobre produccion use -RestoreProduction -ReplaceExisting conscientemente."
}

New-Item -ItemType Directory -Force -Path $WorkDir | Out-Null
$key = [System.IO.File]::ReadAllBytes($KeyPath)
if ($key.Length -ne 32) {
    throw "La clave debe tener 32 bytes."
}

$plainBackup = Join-Path $WorkDir ("restore_" + (Get-Date -Format "yyyyMMdd_HHmmss") + ".bak")

try {
    Write-Host "Descifrando respaldo..."
    Unprotect-FileAes -InputPath $EncryptedBackupPath -OutputPath $plainBackup -Key $key

    $escapedBackup = Escape-SqlLiteral $plainBackup
    Write-Host "Verificando integridad SQL..."
    Invoke-Sql "RESTORE VERIFYONLY FROM DISK = N'$escapedBackup' WITH CHECKSUM;"

    $targetEscaped = Escape-SqlLiteral $TargetDatabase
    $DbExists = Invoke-SqlScalar "SELECT CASE WHEN DB_ID(N'$targetEscaped') IS NULL THEN 0 ELSE 1 END;"

    if ($DbExists -eq "1" -and -not $ReplaceExisting) {
        throw "La base $TargetDatabase ya existe. Use -ReplaceExisting si desea reemplazarla."
    }

    $dataPath = Invoke-SqlScalar "SELECT CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR(4000));"
    $logPath = Invoke-SqlScalar "SELECT CAST(SERVERPROPERTY('InstanceDefaultLogPath') AS NVARCHAR(4000));"
    if ([string]::IsNullOrWhiteSpace($dataPath)) {
        $dataPath = $WorkDir
    }
    if ([string]::IsNullOrWhiteSpace($logPath)) {
        $logPath = $dataPath
    }

    $fileListQuery = @"
DECLARE @FileList TABLE
(
    LogicalName NVARCHAR(128),
    PhysicalName NVARCHAR(260),
    [Type] CHAR(1),
    FileGroupName NVARCHAR(128) NULL,
    Size NUMERIC(20,0),
    MaxSize NUMERIC(20,0),
    FileId BIGINT,
    CreateLSN NUMERIC(25,0) NULL,
    DropLSN NUMERIC(25,0) NULL,
    UniqueId UNIQUEIDENTIFIER,
    ReadOnlyLSN NUMERIC(25,0) NULL,
    ReadWriteLSN NUMERIC(25,0) NULL,
    BackupSizeInBytes BIGINT,
    SourceBlockSize INT,
    FileGroupId INT,
    LogGroupGUID UNIQUEIDENTIFIER NULL,
    DifferentialBaseLSN NUMERIC(25,0) NULL,
    DifferentialBaseGUID UNIQUEIDENTIFIER NULL,
    IsReadOnly BIT,
    IsPresent BIT,
    TDEThumbprint VARBINARY(32) NULL,
    SnapshotUrl NVARCHAR(360) NULL
);
INSERT INTO @FileList
EXEC('RESTORE FILELISTONLY FROM DISK = N''$escapedBackup''');
SELECT LogicalName + '|' + [Type]
FROM @FileList;
"@

    $moveParts = @()
    $dataIndex = 0
    $logIndex = 0
    foreach ($row in Invoke-SqlRows $fileListQuery) {
        $parts = $row.Split("|")
        if ($parts.Length -lt 2) {
            continue
        }

        $logical = $parts[0].Trim()
        $type = $parts[1].Trim()
        $logicalEscaped = Escape-SqlLiteral $logical

        if ($type -eq "L") {
            $logIndex++
            $physical = Join-Path $logPath "$TargetDatabase`_log$logIndex.ldf"
        }
        else {
            $dataIndex++
            $suffix = if ($dataIndex -eq 1) { "" } else { "_$dataIndex" }
            $physical = Join-Path $dataPath "$TargetDatabase$suffix.mdf"
        }

        $moveParts += "MOVE N'$logicalEscaped' TO N'$(Escape-SqlLiteral $physical)'"
    }

    if ($moveParts.Count -eq 0) {
        throw "No se pudo obtener la lista de archivos logicos del respaldo."
    }

    $moveClause = ", " + ($moveParts -join ", ")

    if ($ReplaceExisting -and $DbExists -eq "1") {
        Invoke-Sql "ALTER DATABASE [$TargetDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
    }

    Write-Host "Restaurando en $TargetDatabase..."
    Invoke-Sql "RESTORE DATABASE [$TargetDatabase] FROM DISK = N'$escapedBackup' WITH REPLACE, RECOVERY, CHECKSUM$moveClause;"

    if ($ReplaceExisting) {
        Invoke-Sql "ALTER DATABASE [$TargetDatabase] SET MULTI_USER;"
    }

    Write-Host "Restauracion completada correctamente en $TargetDatabase."
}
finally {
    if (Test-Path -LiteralPath $plainBackup) {
        Remove-Item -LiteralPath $plainBackup -Force
    }
}

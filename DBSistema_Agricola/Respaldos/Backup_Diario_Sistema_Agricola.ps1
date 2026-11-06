param(
    [string]$ServerInstance = ".\SQLEXPRESS",
    [string]$Database = "DBSistema_Agricola",
    [string]$BackupRoot = "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos",
    [string]$OffsitePath = "",
    [int]$RetentionDays = 14,
    [string]$KeyPath = "",
    [string]$SqlCmdPath = "sqlcmd"
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($ServerInstance) -or
    [string]::IsNullOrWhiteSpace($Database) -or
    [string]::IsNullOrWhiteSpace($BackupRoot)) {
    throw "Servidor, base de datos y carpeta de respaldo son obligatorios."
}

if ($RetentionDays -lt 1 -or $RetentionDays -gt 3650) {
    throw "RetentionDays debe estar entre 1 y 3650 dias."
}

function Write-Log {
    param([string]$Message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $line = "[$timestamp] $Message"
    Write-Host $line
    Add-Content -LiteralPath $script:LogPath -Value $line
}

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

    $lines = @($output | Where-Object {
        -not [string]::IsNullOrWhiteSpace($_)
    })
    if ($lines.Count -eq 0) {
        throw "La consulta SQL no devolvio ningun valor."
    }
    return $lines[0].ToString().Trim()
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

function Get-BackupKey {
    param([string]$Path)

    $folder = Split-Path -Parent $Path
    if (-not (Test-Path -LiteralPath $folder)) {
        New-Item -ItemType Directory -Force -Path $folder | Out-Null
    }

    if (-not (Test-Path -LiteralPath $Path)) {
        $key = New-Object byte[] 32
        $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
        $rng.GetBytes($key)
        $rng.Dispose()
        [System.IO.File]::WriteAllBytes($Path, $key)
        Write-Log "Clave de cifrado creada en $Path. Guarde una copia segura fuera de este equipo."
        return $key
    }

    $existing = [System.IO.File]::ReadAllBytes($Path)
    if ($existing.Length -ne 32) {
        throw "La clave de cifrado debe tener 32 bytes: $Path"
    }

    return $existing
}

function Save-BackupStatus {
    param(
        [bool]$Success,
        [string]$Message,
        [string]$BackupPath = ""
    )

    $status = [ordered]@{
        exitoso = $Success
        fecha = (Get-Date).ToString("o")
        servidor = $ServerInstance
        baseDatos = $Database
        respaldo = $BackupPath
        copiaExterna = $OffsitePath
        retencionDias = $RetentionDays
        mensaje = $Message
    }
    $status | ConvertTo-Json | Set-Content -LiteralPath $script:StatusPath -Encoding UTF8
}

function Protect-FileAes {
    param(
        [string]$InputPath,
        [string]$OutputPath,
        [byte[]]$Key
    )

    $iv = New-Object byte[] 16
    $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    $rng.GetBytes($iv)
    $rng.Dispose()

    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.KeySize = 256
    $aes.Mode = [System.Security.Cryptography.CipherMode]::CBC
    $aes.Padding = [System.Security.Cryptography.PaddingMode]::PKCS7
    $aes.Key = $Key
    $aes.IV = $iv

    $plain = [System.IO.File]::ReadAllBytes($InputPath)
    $encryptor = $aes.CreateEncryptor()
    $cipher = $encryptor.TransformFinalBlock($plain, 0, $plain.Length)
    $encryptor.Dispose()
    $aes.Dispose()

    $hmac = [System.Security.Cryptography.HMACSHA256]::new($Key)
    $header = [System.Text.Encoding]::ASCII.GetBytes("SAGBAK1")
    $payload = New-Object byte[] ($header.Length + $iv.Length + $cipher.Length)
    [Array]::Copy($header, 0, $payload, 0, $header.Length)
    [Array]::Copy($iv, 0, $payload, $header.Length, $iv.Length)
    [Array]::Copy($cipher, 0, $payload, $header.Length + $iv.Length, $cipher.Length)
    $tag = $hmac.ComputeHash($payload)
    $hmac.Dispose()

    $final = New-Object byte[] ($payload.Length + $tag.Length)
    [Array]::Copy($payload, 0, $final, 0, $payload.Length)
    [Array]::Copy($tag, 0, $final, $payload.Length, $tag.Length)
    [System.IO.File]::WriteAllBytes($OutputPath, $final)
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

$today = Get-Date -Format "yyyy-MM-dd"
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$backupDir = Join-Path $BackupRoot "backups"
$logDir = Join-Path $BackupRoot "logs"
$verifyDir = Join-Path $BackupRoot "verify"
if ([string]::IsNullOrWhiteSpace($KeyPath)) {
    $KeyPath = Join-Path $BackupRoot "keys\backup.key"
}

New-Item -ItemType Directory -Force -Path $backupDir, $logDir, $verifyDir | Out-Null
$script:LogPath = Join-Path $logDir "backup_$today.log"
$script:StatusPath = Join-Path $BackupRoot "estado_ultimo_respaldo.json"

$bakPath = Join-Path $backupDir "$Database`_$timestamp.bak"
$encryptedPath = "$bakPath.aes"
$verifyPlainPath = Join-Path $verifyDir "$Database`_$timestamp.verify.bak"

try {
    Write-Log "Iniciando respaldo de $Database en $ServerInstance."
    $key = Get-BackupKey -Path $KeyPath

    $escapedBak = Escape-SqlLiteral $bakPath
    $edition = Invoke-SqlScalar "SET NOCOUNT ON; SELECT CAST(SERVERPROPERTY('Edition') AS NVARCHAR(128));"
    $backupOptions = if ($edition -match "Express") {
        Write-Log "SQL Server Express detectado; el respaldo SQL se creara sin COMPRESSION."
        "INIT, CHECKSUM, STATS = 10"
    }
    else {
        "INIT, COMPRESSION, CHECKSUM, STATS = 10"
    }
    Invoke-Sql "BACKUP DATABASE [$Database] TO DISK = N'$escapedBak' WITH $backupOptions;"
    Write-Log "Backup SQL creado: $bakPath"

    Invoke-Sql "RESTORE VERIFYONLY FROM DISK = N'$escapedBak' WITH CHECKSUM;"
    Write-Log "Verificacion SQL del .bak completada."

    Protect-FileAes -InputPath $bakPath -OutputPath $encryptedPath -Key $key
    Remove-Item -LiteralPath $bakPath -Force
    Write-Log "Backup cifrado creado: $encryptedPath"

    $hashPath = "$encryptedPath.sha256"
    $hash = (Get-FileHash -LiteralPath $encryptedPath -Algorithm SHA256).Hash
    Set-Content -LiteralPath $hashPath -Value "$hash  $([IO.Path]::GetFileName($encryptedPath))" -Encoding ASCII
    Write-Log "Huella SHA-256 creada: $hash"

    Unprotect-FileAes -InputPath $encryptedPath -OutputPath $verifyPlainPath -Key $key
    $escapedVerify = Escape-SqlLiteral $verifyPlainPath
    Invoke-Sql "RESTORE VERIFYONLY FROM DISK = N'$escapedVerify' WITH CHECKSUM;"
    Remove-Item -LiteralPath $verifyPlainPath -Force
    Write-Log "Verificacion de descifrado y restaurabilidad completada."

    if (-not [string]::IsNullOrWhiteSpace($OffsitePath)) {
        New-Item -ItemType Directory -Force -Path $OffsitePath | Out-Null
        $offsiteBackup = Join-Path $OffsitePath ([IO.Path]::GetFileName($encryptedPath))
        $offsiteHash = "$offsiteBackup.sha256"
        Copy-Item -LiteralPath $encryptedPath -Destination $offsiteBackup -Force
        Copy-Item -LiteralPath $hashPath -Destination $offsiteHash -Force

        $copiedHash = (Get-FileHash -LiteralPath $offsiteBackup -Algorithm SHA256).Hash
        if ($copiedHash -ne $hash) {
            throw "La copia externa no supero la verificacion SHA-256."
        }
        Write-Log "Copia fuera del equipo/directorio principal enviada a: $OffsitePath"
    }

    $limit = (Get-Date).AddDays(-$RetentionDays)
    Get-ChildItem -LiteralPath $backupDir -Filter "*.bak.aes" -File |
        Where-Object { $_.LastWriteTime -lt $limit } |
        ForEach-Object {
            Write-Log "Eliminando respaldo local vencido: $($_.FullName)"
            $expiredHash = "$($_.FullName).sha256"
            Remove-Item -LiteralPath $_.FullName -Force
            if (Test-Path -LiteralPath $expiredHash) {
                Remove-Item -LiteralPath $expiredHash -Force
            }
        }

    if (-not [string]::IsNullOrWhiteSpace($OffsitePath) -and (Test-Path -LiteralPath $OffsitePath)) {
        Get-ChildItem -LiteralPath $OffsitePath -Filter "*.bak.aes" -File |
            Where-Object { $_.LastWriteTime -lt $limit } |
            ForEach-Object {
                Write-Log "Eliminando respaldo externo vencido: $($_.FullName)"
                $expiredHash = "$($_.FullName).sha256"
                Remove-Item -LiteralPath $_.FullName -Force
                if (Test-Path -LiteralPath $expiredHash) {
                    Remove-Item -LiteralPath $expiredHash -Force
                }
            }
    }

    Save-BackupStatus -Success $true -Message "Respaldo cifrado y verificado correctamente." -BackupPath $encryptedPath
    Write-Log "Respaldo finalizado correctamente."
    exit 0
}
catch {
    Write-Log "ERROR: $($_.Exception.Message)"
    Save-BackupStatus -Success $false -Message $_.Exception.Message
    if (Test-Path -LiteralPath $bakPath) {
        Remove-Item -LiteralPath $bakPath -Force
    }
    if (Test-Path -LiteralPath $verifyPlainPath) {
        Remove-Item -LiteralPath $verifyPlainPath -Force
    }
    exit 1
}

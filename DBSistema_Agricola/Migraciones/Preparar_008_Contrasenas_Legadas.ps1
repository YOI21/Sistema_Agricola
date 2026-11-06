param(
    [string]$Servidor = ".\SQLEXPRESS",
    [string]$BaseDatos = "DBSistema_Agricola",
    [string]$CadenaConexion = ""
)

$ErrorActionPreference = "Stop"
$iteraciones = 150000

if ([string]::IsNullOrWhiteSpace($CadenaConexion)) {
    $CadenaConexion = "Server=$Servidor;Database=$BaseDatos;Integrated Security=SSPI;TrustServerCertificate=True"
}

$conexion = [System.Data.SqlClient.SqlConnection]::new($CadenaConexion)
$conexion.Open()
$transaccion = $conexion.BeginTransaction()

try {
    $cmdValidar = $conexion.CreateCommand()
    $cmdValidar.Transaction = $transaccion
    $cmdValidar.CommandText = @"
SELECT
    CASE WHEN COL_LENGTH(N'dbo.Usuario', N'Contraseña') IS NULL THEN 0 ELSE 1 END AS TieneLegada,
    ISNULL((SELECT MAX(Version) FROM dbo.Esquema_Version), -1) AS VersionActual;
"@
    $lectorValidar = $cmdValidar.ExecuteReader()
    $lectorValidar.Read() | Out-Null
    $tieneLegada = $lectorValidar.GetInt32(0) -eq 1
    $versionActual = $lectorValidar.GetInt32(1)
    $lectorValidar.Close()

    if (-not $tieneLegada) {
        Write-Output "La columna heredada ya no existe. No hay contraseñas por convertir."
        $transaccion.Rollback()
        exit 0
    }

    if ($versionActual -ne 7) {
        throw "La preparación 008 requiere la versión 7. Versión encontrada: $versionActual."
    }

    $cmdLeer = $conexion.CreateCommand()
    $cmdLeer.Transaction = $transaccion
    $cmdLeer.CommandText = @"
SELECT ID_Usuario, [Contraseña]
FROM dbo.Usuario
WHERE Contrasena_Hash IS NULL
   OR Contrasena_Salt IS NULL
   OR Contrasena_Iteraciones IS NULL
ORDER BY ID_Usuario;
"@

    $cuentas = [System.Collections.Generic.List[object]]::new()
    $lector = $cmdLeer.ExecuteReader()
    while ($lector.Read()) {
        $cuentas.Add([pscustomobject]@{
            ID = $lector.GetInt32(0)
            Contrasena = if ($lector.IsDBNull(1)) { "" } else { $lector.GetString(1) }
        })
    }
    $lector.Close()

    $sinContrasena = @($cuentas | Where-Object { [string]::IsNullOrEmpty($_.Contrasena) })
    if ($sinContrasena.Count -gt 0) {
        $ids = ($sinContrasena | ForEach-Object { $_.ID }) -join ", "
        throw "Las cuentas ID $ids no tienen hash ni contraseña heredada. Restablezca esas cuentas antes de continuar."
    }

    foreach ($cuenta in $cuentas) {
        $saltBytes = [System.Security.Cryptography.RandomNumberGenerator]::GetBytes(16)
        $hash = [System.Security.Cryptography.Rfc2898DeriveBytes]::Pbkdf2(
            $cuenta.Contrasena,
            $saltBytes,
            $iteraciones,
            [System.Security.Cryptography.HashAlgorithmName]::SHA256,
            32)

        $cmdActualizar = $conexion.CreateCommand()
        $cmdActualizar.Transaction = $transaccion
        $cmdActualizar.CommandText = @"
UPDATE dbo.Usuario
SET Contrasena_Hash = @Hash,
    Contrasena_Salt = @Salt,
    Contrasena_Iteraciones = @Iteraciones
WHERE ID_Usuario = @ID_Usuario;
"@
        $cmdActualizar.Parameters.Add("@Hash", [System.Data.SqlDbType]::VarBinary, 64).Value = $hash
        $cmdActualizar.Parameters.Add("@Salt", [System.Data.SqlDbType]::VarChar, 64).Value =
            [Convert]::ToBase64String($saltBytes)
        $cmdActualizar.Parameters.Add("@Iteraciones", [System.Data.SqlDbType]::Int).Value = $iteraciones
        $cmdActualizar.Parameters.Add("@ID_Usuario", [System.Data.SqlDbType]::Int).Value = $cuenta.ID
        $cmdActualizar.ExecuteNonQuery() | Out-Null
        [Array]::Clear($saltBytes, 0, $saltBytes.Length)
        [Array]::Clear($hash, 0, $hash.Length)
    }

    $cmdComprobar = $conexion.CreateCommand()
    $cmdComprobar.Transaction = $transaccion
    $cmdComprobar.CommandText = @"
SELECT COUNT(*)
FROM dbo.Usuario
WHERE Contrasena_Hash IS NULL
   OR Contrasena_Salt IS NULL
   OR Contrasena_Iteraciones IS NULL;
"@
    if ([int]$cmdComprobar.ExecuteScalar() -ne 0) {
        throw "La verificación final encontró cuentas sin hash."
    }

    $transaccion.Commit()
    Write-Output "Conversión PBKDF2 completada. Cuentas convertidas: $($cuentas.Count)."
    Write-Output "Las contraseñas no fueron impresas ni copiadas fuera de SQL Server."
}
catch {
    if ($transaccion.Connection) {
        $transaccion.Rollback()
    }
    throw
}
finally {
    $transaccion.Dispose()
    $conexion.Dispose()
}

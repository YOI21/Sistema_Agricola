param(
    [string]$Instancia = ".\SQLEXPRESS",
    [string]$RutaConexion = ""
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($RutaConexion)) {
    $carpeta = Join-Path -Path (
        [Environment]::GetFolderPath([Environment+SpecialFolder]::ApplicationData)
    ) -ChildPath "Sistema_Agricola"
    $RutaConexion = Join-Path $carpeta "conexion_bd.dat"
}

if (-not (Test-Path -LiteralPath $RutaConexion)) {
    throw "No existe la conexion restringida: $RutaConexion. Ejecute Configurar_Conexion_Restringida.ps1."
}

if (-not ("System.Security.Cryptography.ProtectedData" -as [type])) {
    Add-Type -AssemblyName System.Security
}

$cifrado = [Convert]::FromBase64String(
    [System.IO.File]::ReadAllText($RutaConexion).Trim())
$entropia = [System.Text.Encoding]::UTF8.GetBytes(
    "Sistema_Agricola.ConexionBaseDatos.v1")
$datos = [System.Security.Cryptography.ProtectedData]::Unprotect(
    $cifrado,
    $entropia,
    [System.Security.Cryptography.DataProtectionScope]::CurrentUser)
$cadenaConexion = [System.Text.Encoding]::UTF8.GetString($datos)
[Array]::Clear($datos, 0, $datos.Length)
[Array]::Clear($cifrado, 0, $cifrado.Length)
$tipoConexion = "Aplicacion cifrada restringida"

$constructorAplicacion = [System.Data.SqlClient.SqlConnectionStringBuilder]::new(
    $cadenaConexion)
if ($constructorAplicacion.IntegratedSecurity -or
    [string]::IsNullOrWhiteSpace($constructorAplicacion.UserID)) {
    throw "La conexion configurada no utiliza el usuario SQL restringido."
}
$servidorReal = $constructorAplicacion.DataSource
$baseReal = $constructorAplicacion.InitialCatalog

$conexion = [System.Data.SqlClient.SqlConnection]::new($cadenaConexion)
try {
    $conexion.Open()
    $cmd = $conexion.CreateCommand()
    $cmd.CommandText = @"
SELECT
    DB_NAME(),
    CONVERT(VARCHAR(128), SERVERPROPERTY('ServerName')),
    ISNULL((SELECT MAX(Version) FROM dbo.Esquema_Version), -1),
    CASE WHEN OBJECT_ID(N'dbo.Registrar_Auditoria_Seguridad', N'P') IS NULL THEN 0 ELSE 1 END,
    ISNULL(IS_SRVROLEMEMBER(N'sysadmin'), 0),
    ISNULL(IS_MEMBER(N'db_owner'), 0),
    ISNULL(IS_MEMBER(N'db_ddladmin'), 0),
    ISNULL(IS_MEMBER(N'db_datawriter'), 0),
    ISNULL(IS_ROLEMEMBER(N'db_sistema_agricola_app'), 0);
"@
    $lector = $cmd.ExecuteReader()
    $lector.Read() | Out-Null
    $resultado = [pscustomobject]@{
        Conexion = $tipoConexion
        Servidor = $lector.GetString(1)
        BaseDatos = $lector.GetString(0)
        Version = $lector.GetInt32(2)
        AuditoriaSegura = $lector.GetInt32(3) -eq 1
        EsSysadmin = $lector.GetInt32(4) -eq 1
        EsDbOwner = $lector.GetInt32(5) -eq 1
        EsDdlAdmin = $lector.GetInt32(6) -eq 1
        EsDataWriter = $lector.GetInt32(7) -eq 1
        RolAplicacion = $lector.GetInt32(8) -eq 1
    }
    $lector.Close()
}
finally {
    $conexion.Dispose()
}

$cadenaAdmin =
    "Server=$servidorReal;Database=$baseReal;Integrated Security=SSPI;TrustServerCertificate=True"
$conexionAdmin = [System.Data.SqlClient.SqlConnection]::new($cadenaAdmin)
try {
    $conexionAdmin.Open()
    $cmdAdmin = $conexionAdmin.CreateCommand()
    $cmdAdmin.CommandText = @"
SELECT
    (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0),
    (SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped = 0);
"@
    $lectorAdmin = $cmdAdmin.ExecuteReader()
    $lectorAdmin.Read() | Out-Null
    $tablas = $lectorAdmin.GetInt32(0)
    $procedimientos = $lectorAdmin.GetInt32(1)
    $lectorAdmin.Close()
}
finally {
    $conexionAdmin.Dispose()
}

$resultadoFinal = [pscustomobject]@{
    Conexion = $resultado.Conexion
    Servidor = $resultado.Servidor
    BaseDatos = $resultado.BaseDatos
    Version = $resultado.Version
    Tablas = $tablas
    Procedimientos = $procedimientos
    AuditoriaSegura = $resultado.AuditoriaSegura
    MinimoPrivilegio = -not (
        $resultado.EsSysadmin -or
        $resultado.EsDbOwner -or
        $resultado.EsDdlAdmin -or
        $resultado.EsDataWriter) -and $resultado.RolAplicacion
}
$resultadoFinal | Format-List

if ($resultadoFinal.BaseDatos -ne "DBSistema_Agricola" -or
    $resultadoFinal.Version -ne 12 -or
    $resultadoFinal.Tablas -ne 32 -or
    $resultadoFinal.Procedimientos -ne 142 -or
    -not $resultadoFinal.AuditoriaSegura -or
    -not $resultadoFinal.MinimoPrivilegio) {
    throw "La instalacion no coincide con el esquema requerido por la aplicacion."
}

Write-Host "VERIFICACION CORRECTA: la base y la conexion estan listas." -ForegroundColor Green

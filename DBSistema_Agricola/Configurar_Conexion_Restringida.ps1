param(
    [string]$Servidor = ".\SQLEXPRESS",
    [string]$BaseDatos = "DBSistema_Agricola",
    [string]$UsuarioAplicacion = "sistema_agricola_runtime",
    [string]$RutaConexion = ""
)

$ErrorActionPreference = "Stop"

if ($BaseDatos -notmatch '^[A-Za-z0-9_]+$') {
    throw "El nombre de la base de datos contiene caracteres no permitidos."
}

if ($UsuarioAplicacion -notmatch '^[A-Za-z0-9_]+$') {
    throw "El nombre del usuario de aplicacion contiene caracteres no permitidos."
}

$claveBytes = New-Object byte[] 32
$generador = [System.Security.Cryptography.RandomNumberGenerator]::Create()
try {
    $generador.GetBytes($claveBytes)
}
finally {
    $generador.Dispose()
}

$clave = [Convert]::ToBase64String($claveBytes) + "!aA9"
[Array]::Clear($claveBytes, 0, $claveBytes.Length)

$baseDelimitada = "[" + $BaseDatos.Replace("]", "]]" ) + "]"
$usuarioDelimitado = "[" + $UsuarioAplicacion.Replace("]", "]]" ) + "]"
$usuarioLiteral = $UsuarioAplicacion.Replace("'", "''")
$claveLiteral = $clave.Replace("'", "''")

$cadenaAdmin =
    "Server=$Servidor;Database=master;Integrated Security=SSPI;TrustServerCertificate=True"
$conexionAdmin = [System.Data.SqlClient.SqlConnection]::new($cadenaAdmin)
$requiereReinicio = $false

try {
    $conexionAdmin.Open()
    $cmdModo = $conexionAdmin.CreateCommand()
    $cmdModo.CommandText =
        "SELECT CONVERT(INT, SERVERPROPERTY('IsIntegratedSecurityOnly'));"
    $requiereReinicio = [int]$cmdModo.ExecuteScalar() -eq 1

    if ($requiereReinicio) {
        $cmdHabilitarModoMixto = $conexionAdmin.CreateCommand()
        $cmdHabilitarModoMixto.CommandText = @"
EXEC master.dbo.xp_instance_regwrite
    N'HKEY_LOCAL_MACHINE',
    N'Software\Microsoft\MSSQLServer\MSSQLServer',
    N'LoginMode',
    REG_DWORD,
    2;
"@
        $cmdHabilitarModoMixto.ExecuteNonQuery() | Out-Null
    }

    $cmdServidor = $conexionAdmin.CreateCommand()
    $cmdServidor.CommandText = @"
EXEC sys.sp_configure N'contained database authentication', 1;
RECONFIGURE;
ALTER DATABASE $baseDelimitada SET CONTAINMENT = PARTIAL;
"@
    $cmdServidor.ExecuteNonQuery() | Out-Null
}
finally {
    $conexionAdmin.Dispose()
}

if ($requiereReinicio) {
    $nombreServicio = if ($Servidor -match '\\([^\\]+)$') {
        'MSSQL$' + $Matches[1]
    }
    else {
        'MSSQLSERVER'
    }

    try {
        Restart-Service -Name $nombreServicio -Force
        (Get-Service -Name $nombreServicio).WaitForStatus(
            [System.ServiceProcess.ServiceControllerStatus]::Running,
            [TimeSpan]::FromSeconds(45))
        Start-Sleep -Seconds 2
    }
    catch {
        throw "SQL Server necesita reiniciarse para habilitar la conexion segura. Ejecute PowerShell como administrador y vuelva a intentar. Detalle: $($_.Exception.Message)"
    }
}

$cadenaBaseAdmin =
    "Server=$Servidor;Database=$BaseDatos;Integrated Security=SSPI;TrustServerCertificate=True"
$conexionBase = [System.Data.SqlClient.SqlConnection]::new($cadenaBaseAdmin)

try {
    $conexionBase.Open()
    $cmdUsuario = $conexionBase.CreateCommand()
    $cmdUsuario.CommandText = @"
IF DATABASE_PRINCIPAL_ID(N'$usuarioLiteral') IS NULL
    EXEC(N'CREATE USER $usuarioDelimitado WITH PASSWORD = ''$claveLiteral'';');
ELSE
    EXEC(N'ALTER USER $usuarioDelimitado WITH PASSWORD = ''$claveLiteral'';');

IF IS_ROLEMEMBER(N'db_sistema_agricola_app', N'$usuarioLiteral') <> 1
    ALTER ROLE db_sistema_agricola_app ADD MEMBER $usuarioDelimitado;
"@
    $cmdUsuario.ExecuteNonQuery() | Out-Null
}
finally {
    $conexionBase.Dispose()
}

$constructor = [System.Data.SqlClient.SqlConnectionStringBuilder]::new()
$constructor["Data Source"] = $Servidor
$constructor["Initial Catalog"] = $BaseDatos
$constructor["Integrated Security"] = $false
$constructor["User ID"] = $UsuarioAplicacion
$constructor["Password"] = $clave
$constructor["TrustServerCertificate"] = $true
$constructor["Persist Security Info"] = $false
$constructor["Application Name"] = "Sistema Agricola"
$cadenaAplicacion = $constructor.ConnectionString

$conexionPrueba = [System.Data.SqlClient.SqlConnection]::new($cadenaAplicacion)
try {
    $conexionPrueba.Open()
    $cmdPrueba = $conexionPrueba.CreateCommand()
    $cmdPrueba.CommandText = @"
SELECT
    HAS_PERMS_BY_NAME(N'dbo.Usuario', N'OBJECT', N'SELECT'),
    HAS_PERMS_BY_NAME(N'dbo.Siembra', N'OBJECT', N'UPDATE'),
    HAS_PERMS_BY_NAME(N'dbo.Obtener_Cuenta_Autenticacion', N'OBJECT', N'EXECUTE'),
    ISNULL((SELECT MAX(Version) FROM dbo.Esquema_Version), -1);
"@
    $lector = $cmdPrueba.ExecuteReader()
    $lector.Read() | Out-Null
    $puedeLeerUsuarios = $lector.GetInt32(0)
    $puedeActualizarTabla = $lector.GetInt32(1)
    $puedeAutenticar = $lector.GetInt32(2)
    $versionEsquema = $lector.GetInt32(3)
    $lector.Close()

    if ($puedeLeerUsuarios -ne 0 -or
        $puedeActualizarTabla -ne 0 -or
        $puedeAutenticar -ne 1) {
        throw "La comprobacion de minimo privilegio no produjo los permisos esperados."
    }

    if ($versionEsquema -ne 12) {
        throw "La conexion apunta a una base en version $versionEsquema; se requiere la version 12."
    }
}
finally {
    $conexionPrueba.Dispose()
}

if (-not ("System.Security.Cryptography.ProtectedData" -as [type])) {
    Add-Type -AssemblyName System.Security
}

$entropia =
    [System.Text.Encoding]::UTF8.GetBytes("Sistema_Agricola.ConexionBaseDatos.v1")
$datos = [System.Text.Encoding]::UTF8.GetBytes($cadenaAplicacion)
$cifrado = [System.Security.Cryptography.ProtectedData]::Protect(
    $datos,
    $entropia,
    [System.Security.Cryptography.DataProtectionScope]::CurrentUser)

if ([string]::IsNullOrWhiteSpace($RutaConexion)) {
    $carpeta = Join-Path -Path (
        [Environment]::GetFolderPath([Environment+SpecialFolder]::ApplicationData)
    ) -ChildPath "Sistema_Agricola"
    $RutaConexion = Join-Path $carpeta "conexion_bd.dat"
}
else {
    $carpeta = Split-Path -Parent $RutaConexion
}

if (-not [string]::IsNullOrWhiteSpace($carpeta)) {
    [System.IO.Directory]::CreateDirectory($carpeta) | Out-Null
}

$rutaTemporal = $RutaConexion + ".tmp"
[System.IO.File]::WriteAllText($rutaTemporal, [Convert]::ToBase64String($cifrado))
Move-Item -LiteralPath $rutaTemporal -Destination $RutaConexion -Force

# La conexion cifrada debe ser la unica fuente para evitar que una variable
# antigua dirija la aplicacion hacia otra instancia o una base desactualizada.
[Environment]::SetEnvironmentVariable(
    "SISTEMA_AGRICOLA_CONNECTION_STRING",
    $null,
    [EnvironmentVariableTarget]::User)

try {
    [Environment]::SetEnvironmentVariable(
        "SISTEMA_AGRICOLA_CONNECTION_STRING",
        $null,
        [EnvironmentVariableTarget]::Machine)
}
catch [System.Security.SecurityException] {
    Write-Warning "No se pudo limpiar la variable a nivel de maquina sin permisos de administrador. La aplicacion ignorara ese valor y usara el archivo DPAPI."
}

[Environment]::SetEnvironmentVariable(
    "SISTEMA_AGRICOLA_CONNECTION_STRING",
    $null,
    [EnvironmentVariableTarget]::Process)

[Array]::Clear($datos, 0, $datos.Length)
[Array]::Clear($cifrado, 0, $cifrado.Length)
$clave = $null
$cadenaAplicacion = $null

Write-Output "Conexion restringida configurada correctamente."
Write-Output "Servidor: $Servidor"
Write-Output "Base de datos: $BaseDatos"
Write-Output "Version del esquema: $versionEsquema"
Write-Output "Usuario contenido: $UsuarioAplicacion"
Write-Output "Archivo DPAPI: $RutaConexion"
Write-Output "La clave no fue mostrada ni guardada en texto plano."
Write-Output "Se eliminaron conexiones antiguas de SISTEMA_AGRICOLA_CONNECTION_STRING."

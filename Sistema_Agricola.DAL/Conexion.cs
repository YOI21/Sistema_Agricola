using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Sistema_Agricola.DAL
{
    public static class Conexion
    {
        private const string VariableCadena = "SISTEMA_AGRICOLA_CONNECTION_STRING";
        private const string NombreBaseDatos = "DBSistema_Agricola";
        private const string RolAplicacion = "db_sistema_agricola_app";

        private static readonly byte[] EntropiaConexion =
            Encoding.UTF8.GetBytes("Sistema_Agricola.ConexionBaseDatos.v1");
        private static readonly object SincronizacionValidacion = new object();

        private static string? huellaCadenaValidada;

        public static SqlConnection conectardb()
        {
            string cadenaConexion = ObtenerCadenaConexion();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                ValidarMinimoPrivilegio(conexion, cadenaConexion);
                return conexion;
            }
            catch
            {
                conexion.Dispose();
                throw;
            }
        }

        private static string ObtenerCadenaConexion()
        {
            string? cadenaConexion = LeerConexionCifrada();
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                cadenaConexion = Environment.GetEnvironmentVariable(VariableCadena);
            }

            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new InvalidOperationException(
                    "No existe una conexion restringida para el Sistema Agricola. " +
                    "Ejecute Configurar_Conexion_Restringida.ps1 antes de iniciar la aplicacion.");
            }

            ValidarCadenaConexion(cadenaConexion);
            return cadenaConexion;
        }

        private static string? LeerConexionCifrada()
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException(
                    "La conexion cifrada DPAPI solo puede utilizarse en Windows.");
            }

            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sistema_Agricola",
                "conexion_bd.dat");

            if (!File.Exists(ruta))
            {
                return null;
            }

            try
            {
                byte[] cifrado = Convert.FromBase64String(File.ReadAllText(ruta).Trim());
                byte[] datos = ProtectedData.Unprotect(
                    cifrado,
                    EntropiaConexion,
                    DataProtectionScope.CurrentUser);

                try
                {
                    return Encoding.UTF8.GetString(datos);
                }
                finally
                {
                    Array.Clear(datos, 0, datos.Length);
                    Array.Clear(cifrado, 0, cifrado.Length);
                }
            }
            catch (Exception ex) when (ex is CryptographicException or FormatException or IOException)
            {
                throw new InvalidOperationException(
                    "La conexion cifrada de la base de datos no es valida para este usuario de Windows. " +
                    "Ejecute nuevamente Configurar_Conexion_Restringida.ps1.",
                    ex);
            }
        }

        private static void ValidarCadenaConexion(string cadenaConexion)
        {
            SqlConnectionStringBuilder constructor;
            try
            {
                constructor = new SqlConnectionStringBuilder(cadenaConexion);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(
                    "La configuracion de conexion de la base de datos no es valida.",
                    ex);
            }

            if (constructor.IntegratedSecurity)
            {
                throw new InvalidOperationException(
                    "La aplicacion no permite autenticacion integrada de Windows. " +
                    "Use el usuario restringido creado por Configurar_Conexion_Restringida.ps1.");
            }

            if (string.IsNullOrWhiteSpace(constructor.UserID) ||
                string.IsNullOrWhiteSpace(constructor.Password))
            {
                throw new InvalidOperationException(
                    "La conexion debe utilizar un usuario SQL restringido con credenciales cifradas.");
            }

            if (!string.Equals(
                    constructor.InitialCatalog,
                    NombreBaseDatos,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"La conexion debe apuntar exclusivamente a {NombreBaseDatos}.");
            }
        }

        private static void ValidarMinimoPrivilegio(
            SqlConnection conexion,
            string cadenaConexion)
        {
            string huella = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(cadenaConexion)));

            lock (SincronizacionValidacion)
            {
                if (string.Equals(huellaCadenaValidada, huella, StringComparison.Ordinal))
                {
                    return;
                }

                const string consulta = @"
SELECT
    ISNULL(IS_SRVROLEMEMBER(N'sysadmin'), 0),
    ISNULL(IS_MEMBER(N'db_owner'), 0),
    ISNULL(IS_MEMBER(N'db_ddladmin'), 0),
    ISNULL(IS_MEMBER(N'db_datawriter'), 0),
    ISNULL(IS_ROLEMEMBER(N'db_sistema_agricola_app'), 0);";

                using SqlCommand comando = new SqlCommand(consulta, conexion)
                {
                    CommandType = CommandType.Text
                };
                using SqlDataReader lector = comando.ExecuteReader();

                if (!lector.Read())
                {
                    throw new InvalidOperationException(
                        "No fue posible comprobar los permisos de la conexion SQL.");
                }

                bool tienePrivilegiosAdministrativos =
                    lector.GetInt32(0) == 1 ||
                    lector.GetInt32(1) == 1 ||
                    lector.GetInt32(2) == 1 ||
                    lector.GetInt32(3) == 1;
                bool perteneceAlRolAplicacion = lector.GetInt32(4) == 1;

                if (tienePrivilegiosAdministrativos || !perteneceAlRolAplicacion)
                {
                    throw new UnauthorizedAccessException(
                        $"La conexion SQL no cumple el minimo privilegio requerido. " +
                        $"Debe pertenecer solamente al rol {RolAplicacion} y no a roles administrativos.");
                }

                huellaCadenaValidada = huella;
            }
        }
    }
}

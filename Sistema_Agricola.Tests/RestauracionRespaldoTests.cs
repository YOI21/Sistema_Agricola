using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class RestauracionRespaldoTests
{
    [RestauracionFact]
    [Trait("Categoria", "Restauracion")]
    public void Respaldo_SeRestauraYSuperaVerificacionDeIntegridad()
    {
        string cadena = Environment.GetEnvironmentVariable(
            "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING")!;
        string rutaRespaldo = Environment.GetEnvironmentVariable(
            "SISTEMA_AGRICOLA_BACKUP_PATH")!;
        string basePrueba = "DBSistema_Agricola_RestoreTest_" +
            Guid.NewGuid().ToString("N")[..10];

        SqlConnectionStringBuilder constructor = new(cadena)
        {
            InitialCatalog = "master",
            TrustServerCertificate = true
        };

        using SqlConnection conexion = new(constructor.ConnectionString);
        conexion.Open();

        try
        {
            List<ArchivoRespaldo> archivos = LeerArchivosRespaldo(
                conexion,
                rutaRespaldo);
            Assert.NotEmpty(archivos);

            (string rutaDatos, string rutaLogs) = ObtenerRutasServidor(conexion);
            string clausulasMove = string.Join(
                "," + Environment.NewLine,
                archivos.Select((archivo, indice) =>
                {
                    string extension = archivo.Tipo == "L"
                        ? ".ldf"
                        : indice == 0 ? ".mdf" : $"_{indice}.ndf";
                    string carpeta = archivo.Tipo == "L" ? rutaLogs : rutaDatos;
                    string destino = Path.Combine(carpeta, basePrueba + extension);
                    return $"MOVE N'{EscaparLiteral(archivo.NombreLogico)}' " +
                           $"TO N'{EscaparLiteral(destino)}'";
                }));

            string restaurar = $@"
RESTORE DATABASE [{basePrueba}]
FROM DISK = N'{EscaparLiteral(rutaRespaldo)}'
WITH {clausulasMove}, REPLACE, RECOVERY, STATS = 10;";
            using (SqlCommand comando = new(restaurar, conexion))
            {
                comando.CommandTimeout = 600;
                comando.ExecuteNonQuery();
            }

            VerificarBaseRestaurada(constructor, basePrueba);
        }
        finally
        {
            EliminarBaseTemporal(conexion, basePrueba);
        }
    }

    private static List<ArchivoRespaldo> LeerArchivosRespaldo(
        SqlConnection conexion,
        string rutaRespaldo)
    {
        string consulta =
            $"RESTORE FILELISTONLY FROM DISK = N'{EscaparLiteral(rutaRespaldo)}';";
        using SqlCommand comando = new(consulta, conexion)
        {
            CommandTimeout = 120
        };
        using SqlDataReader lector = comando.ExecuteReader();
        List<ArchivoRespaldo> archivos = new();

        int columnaNombre = lector.GetOrdinal("LogicalName");
        int columnaTipo = lector.GetOrdinal("Type");
        while (lector.Read())
        {
            archivos.Add(new ArchivoRespaldo(
                lector.GetString(columnaNombre),
                lector.GetString(columnaTipo)));
        }

        return archivos;
    }

    private static (string Datos, string Logs) ObtenerRutasServidor(
        SqlConnection conexion)
    {
        using SqlCommand comando = new SqlCommand(@"
SELECT
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath')),
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultLogPath'));", conexion);
        using SqlDataReader lector = comando.ExecuteReader();
        Assert.True(lector.Read());

        string datos = lector.IsDBNull(0) ? string.Empty : lector.GetString(0);
        string logs = lector.IsDBNull(1) ? string.Empty : lector.GetString(1);
        lector.Close();

        if (string.IsNullOrWhiteSpace(datos) || string.IsNullOrWhiteSpace(logs))
        {
            using SqlCommand respaldo = new SqlCommand(@"
SELECT
    MAX(CASE WHEN type = 0 THEN LEFT(physical_name,
        LEN(physical_name) - CHARINDEX('\', REVERSE(physical_name)) + 1) END),
    MAX(CASE WHEN type = 1 THEN LEFT(physical_name,
        LEN(physical_name) - CHARINDEX('\', REVERSE(physical_name)) + 1) END)
FROM sys.master_files
WHERE database_id = DB_ID(N'master');", conexion);
            using SqlDataReader rutas = respaldo.ExecuteReader();
            Assert.True(rutas.Read());
            datos = string.IsNullOrWhiteSpace(datos) && !rutas.IsDBNull(0)
                ? rutas.GetString(0)
                : datos;
            logs = string.IsNullOrWhiteSpace(logs) && !rutas.IsDBNull(1)
                ? rutas.GetString(1)
                : logs;
        }

        Assert.False(string.IsNullOrWhiteSpace(datos));
        Assert.False(string.IsNullOrWhiteSpace(logs));
        return (datos, logs);
    }

    private static void VerificarBaseRestaurada(
        SqlConnectionStringBuilder constructor,
        string basePrueba)
    {
        constructor.InitialCatalog = basePrueba;
        using SqlConnection restaurada = new(constructor.ConnectionString);
        restaurada.Open();

        using (SqlCommand integridad = new SqlCommand(
                   "DBCC CHECKDB WITH NO_INFOMSGS;",
                   restaurada))
        {
            integridad.CommandTimeout = 600;
            integridad.ExecuteNonQuery();
        }

        using SqlCommand inventario = new SqlCommand(@"
SELECT
    (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0),
    (SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped = 0),
    (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version);", restaurada);
        using SqlDataReader lector = inventario.ExecuteReader();
        Assert.True(lector.Read());
        Assert.True(lector.GetInt32(0) >= 31, "El respaldo tiene menos de 31 tablas.");
        Assert.True(lector.GetInt32(1) >= 106, "El respaldo tiene menos de 106 procedimientos.");
        Assert.Equal(BLL_Esquema.VersionRequerida, lector.GetInt32(2));
    }

    private static void EliminarBaseTemporal(
        SqlConnection conexion,
        string basePrueba)
    {
        if (conexion.State != ConnectionState.Open)
        {
            conexion.Open();
        }

        string eliminar = $@"
IF DB_ID(N'{EscaparLiteral(basePrueba)}') IS NOT NULL
BEGIN
    ALTER DATABASE [{basePrueba}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [{basePrueba}];
END;";
        using SqlCommand comando = new(eliminar, conexion)
        {
            CommandTimeout = 120
        };
        comando.ExecuteNonQuery();
    }

    private static string EscaparLiteral(string valor) => valor.Replace("'", "''");

    private sealed record ArchivoRespaldo(string NombreLogico, string Tipo);
}

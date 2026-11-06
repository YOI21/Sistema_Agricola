using Microsoft.Data.SqlClient;

namespace Sistema_Agricola.Tests;

public sealed class CatalogoAdministrativoTests
{
    private static readonly string[] IndicesUnicosEsperados =
    {
        "UX_Usuario_Usuario",
        "UX_Agricultor_Cedula",
        "UX_Administrador_Email",
        "UX_Trabajador_Email",
        "UX_Insumos_Codigo",
        "UX_RMaquinaria_Codigo",
        "UX_Cosecha_Cultivo",
        "UX_Agricultor_Siembra",
        "UX_MovimientoFinanciero_Origen"
    };

    [AdministracionBaseDatosFact]
    [Trait("Categoria", "AdministracionBD")]
    public void BaseInstalada_TieneIndicesUnicosYRestriccionesConfiables()
    {
        string cadena = Environment.GetEnvironmentVariable(
            "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING")!;
        SqlConnectionStringBuilder constructor = new(cadena)
        {
            InitialCatalog = "DBSistema_Agricola",
            TrustServerCertificate = true
        };

        using SqlConnection conexion = new(constructor.ConnectionString);
        conexion.Open();

        using (SqlCommand comando = new SqlCommand(@"
SELECT i.name
FROM sys.indexes AS i
WHERE i.is_unique = 1
  AND i.name IS NOT NULL;", conexion))
        using (SqlDataReader lector = comando.ExecuteReader())
        {
            HashSet<string> instalados = new(StringComparer.OrdinalIgnoreCase);
            while (lector.Read())
            {
                instalados.Add(lector.GetString(0));
            }

            foreach (string indice in IndicesUnicosEsperados)
            {
                Assert.Contains(indice, instalados);
            }
        }

        using SqlCommand restricciones = new SqlCommand(@"
SELECT
    (SELECT COUNT(*) FROM sys.check_constraints
     WHERE is_disabled = 1 OR is_not_trusted = 1) +
    (SELECT COUNT(*) FROM sys.foreign_keys
     WHERE is_disabled = 1 OR is_not_trusted = 1);", conexion);

        Assert.Equal(0, Convert.ToInt32(restricciones.ExecuteScalar()));
    }
}

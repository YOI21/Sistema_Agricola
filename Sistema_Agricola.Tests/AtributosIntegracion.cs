namespace Sistema_Agricola.Tests;

public sealed class BaseDatosFactAttribute : FactAttribute
{
    public BaseDatosFactAttribute()
    {
        if (!string.Equals(
                Environment.GetEnvironmentVariable("SISTEMA_AGRICOLA_RUN_DB_TESTS"),
                "1",
                StringComparison.Ordinal))
        {
            Skip =
                "Prueba SQL no ejecutada. Defina SISTEMA_AGRICOLA_RUN_DB_TESTS=1 " +
                "y configure la conexión restringida.";
        }
    }
}

public sealed class RestauracionFactAttribute : FactAttribute
{
    public RestauracionFactAttribute()
    {
        string? cadena = Environment.GetEnvironmentVariable(
            "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING");
        string? respaldo = Environment.GetEnvironmentVariable(
            "SISTEMA_AGRICOLA_BACKUP_PATH");

        if (string.IsNullOrWhiteSpace(cadena) || string.IsNullOrWhiteSpace(respaldo))
        {
            Skip =
                "Restauración no ejecutada. Defina " +
                "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING y SISTEMA_AGRICOLA_BACKUP_PATH.";
        }
    }
}

public sealed class AdministracionBaseDatosFactAttribute : FactAttribute
{
    public AdministracionBaseDatosFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(
                Environment.GetEnvironmentVariable(
                    "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING")))
        {
            Skip =
                "Catálogo administrativo no verificado. Defina " +
                "SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING.";
        }
    }
}

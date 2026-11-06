using Sistema_Agricola.UI;

namespace Sistema_Agricola.UI.Tests;

public sealed class DependenciasUiTests
{
    [Fact]
    public void Ui_NoReferenciaReglasAgricolasNiSqlClient()
    {
        string?[] referencias = typeof(FrmSiembra).Assembly
            .GetReferencedAssemblies()
            .Select(referencia => referencia.Name)
            .ToArray();

        Assert.DoesNotContain("Sistema_Agricola.Pr", referencias);
        Assert.DoesNotContain("System.Data.SqlClient", referencias);
        Assert.DoesNotContain("Microsoft.Data.SqlClient", referencias);
    }

    [Fact]
    public void Ui_NoContieneFormularioDeMaquinariaObsoleto()
    {
        Assert.Null(typeof(FrmSiembra).Assembly.GetType(
            "RegistroMaquinaria.FormRegistroMaquinaria",
            throwOnError: false,
            ignoreCase: false));
    }
}

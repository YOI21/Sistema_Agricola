using System.Data;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class ProduccionTests
{
    [Fact]
    public void Resumen_AcumulaCantidadesCostosYBeneficio()
    {
        DataTable datos = new DataTable();
        datos.Columns.Add("Cantidad bruta", typeof(decimal));
        datos.Columns.Add("Merma", typeof(decimal));
        datos.Columns.Add("Cantidad neta", typeof(decimal));
        datos.Columns.Add("Valor produccion", typeof(decimal));
        datos.Columns.Add("Costo total", typeof(decimal));
        datos.Columns.Add("Beneficio neto", typeof(decimal));
        datos.Rows.Add(100m, 5m, 95m, 950m, 400m, 550m);
        datos.Rows.Add(50m, 2m, 48m, 480m, 180m, 300m);

        var resumen = BLL_Produccion.CalcularResumen(datos);

        Assert.Equal(2, resumen.Lotes);
        Assert.Equal(150m, resumen.CantidadBruta);
        Assert.Equal(7m, resumen.Merma);
        Assert.Equal(143m, resumen.CantidadNeta);
        Assert.Equal(1430m, resumen.ValorProduccion);
        Assert.Equal(580m, resumen.Costos);
        Assert.Equal(850m, resumen.Beneficio);
    }

    [Fact]
    public void Resumen_NuloDevuelveCeros()
    {
        var resumen = BLL_Produccion.CalcularResumen(null);

        Assert.Equal(0, resumen.Lotes);
        Assert.Equal(0m, resumen.Beneficio);
    }
}

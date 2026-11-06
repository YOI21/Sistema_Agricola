using System.Data;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class AlertasTests
{
    [Fact]
    public void ObtenerAlertasProximas_DevuelveElResultadoDelOrigen()
    {
        DataTable esperado = new();
        esperado.Columns.Add("ID_Alerta", typeof(int));
        esperado.Rows.Add(5);

        DataTable resultado = BLL_Alertas.ObtenerAlertasProximas(() => esperado);

        Assert.Same(esperado, resultado);
    }

    [Fact]
    public void ObtenerAlertasProximas_NoConvierteUnFalloEnTablaVacia()
    {
        InvalidOperationException error = Assert.Throws<InvalidOperationException>(() =>
            BLL_Alertas.ObtenerAlertasProximas(
                () => throw new InvalidOperationException("Base no disponible")));

        Assert.Equal("Base no disponible", error.Message);
    }
}

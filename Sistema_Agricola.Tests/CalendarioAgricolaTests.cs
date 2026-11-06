using Sistema_Agricola.Pr;

namespace Sistema_Agricola.Tests;

public sealed class CalendarioAgricolaTests
{
    [Theory]
    [InlineData(1, "norte humeda", "Período lluvioso")]
    [InlineData(7, "norte humeda", "Período seco")]
    [InlineData(5, "subhumeda cibao este", "Período lluvioso")]
    [InlineData(2, "subhumeda cibao este", "Período seco")]
    public void PeriodoClimatico_RespetaZonaYMes(
        int mes,
        string zona,
        string esperado)
    {
        string periodo = CalendarioAgricola.ObtenerPeriodoClimatico(
            new DateTime(2026, mes, 1),
            zona);

        Assert.Equal(esperado, periodo);
    }

    [Fact]
    public void CicloArroz_IncluyeDuracionAgricola()
    {
        var ciclo = CalendarioAgricola.ObtenerCicloParaCultivo("Arroz");

        Assert.Equal(4, ciclo.DuracionAproxMeses);
        Assert.Contains("Marzo", ciclo.MejorMesSiembra);
    }
}

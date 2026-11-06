using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class PoliticaContrasenaTests
{
    [Fact]
    public void Temporal_CumpleLongitudYComplejidad()
    {
        string temporal = BLL_PoliticaContrasena.GenerarTemporal();

        Assert.Equal(BLL_PoliticaContrasena.LongitudTemporal, temporal.Length);
        BLL_PoliticaContrasena.Validar(temporal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("corta1!A")]
    [InlineData("TODAMAYUSCULA1!")]
    [InlineData("todaminuscula1!")]
    [InlineData("SinNumeroAlguno!")]
    [InlineData("SinSimbolo123A")]
    public void Invalida_RechazaContrasenasDebiles(string contrasena)
    {
        Assert.Throws<ArgumentException>(() => BLL_PoliticaContrasena.Validar(contrasena));
    }

    [Fact]
    public void Confirmacion_DebeCoincidirExactamente()
    {
        Assert.Throws<ArgumentException>(() =>
            BLL_PoliticaContrasena.ValidarConfirmacion(
                "ClaveSegura123!",
                "ClaveSegura124!"));
    }
}

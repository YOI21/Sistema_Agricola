using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class EsquemaBaseDatosTests
{
    [BaseDatosFact]
    [Trait("Categoria", "Integracion")]
    public void BaseConfigurada_EsCompatibleConLaAplicacion()
    {
        ResultadoVerificacionEsquema resultado = BLL_Esquema.VerificarCompatibilidad();

        Assert.True(resultado.EsCompatible, resultado.Mensaje);
        Assert.Equal(BLL_Esquema.VersionRequerida, resultado.VersionEncontrada);
    }
}

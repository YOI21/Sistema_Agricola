using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class CorreoTests
{
    [Fact]
    public void ConexionCerrada_IndicaLosDosPuertosYLaRed()
    {
        Exception error = new IOException(
            "No se puede escribir datos en la conexion de transporte: net_io_connectionclosed.");

        string mensaje = ServicioCorreoRecuperacion.ObtenerMensajeError(error);

        Assert.Contains("587 y 465", mensaje);
        Assert.Contains("antivirus", mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CredencialesRechazadas_IndicaCrearClaveDeAplicacion()
    {
        Exception error = new InvalidOperationException(
            "535-5.7.8 Username and Password not accepted");

        string mensaje = ServicioCorreoRecuperacion.ObtenerMensajeError(error);

        Assert.Contains("clave de aplicacion", mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("mismo Gmail", mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ErrorGenericoAnterior_ExigePublicacionActualizada()
    {
        Exception error = new InvalidOperationException("Failure sending mail");

        string mensaje = ServicioCorreoRecuperacion.ObtenerMensajeError(error);

        Assert.Contains("MailKit", mensaje);
        Assert.DoesNotContain("Failure sending mail", mensaje, StringComparison.OrdinalIgnoreCase);
    }
}

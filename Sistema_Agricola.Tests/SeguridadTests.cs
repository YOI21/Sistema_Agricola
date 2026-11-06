using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class SeguridadTests : IDisposable
{
    public SeguridadTests()
    {
        BLL_Seguridad.CerrarSesion();
    }

    public void Dispose()
    {
        BLL_Seguridad.CerrarSesion();
    }

    [Fact]
    public void SinSesion_NoConcedePermisos()
    {
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.SiembrasCrear));
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.FinanzasCrear));
    }

    [Fact]
    public void Administrador_PuedeGestionarModulosSensiblesYCrearAgricultores()
    {
        BLL_Seguridad.IniciarSesion(1, "admin", "Administrador", "Administrador");

        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.FinanzasEliminar));
        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.TrabajadoresCrear));
        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.AgricultoresCrear));
        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.ProduccionFinalizar));
    }

    [Fact]
    public void Agricultor_OperaCultivosPeroNoAdministraRegistrosSensibles()
    {
        BLL_Seguridad.IniciarSesion(2, "agricultor", "Agricultor", "Agricultor");

        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.SiembrasCrear));
        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.CosechasFinalizar));
        Assert.True(BLL_Seguridad.TienePermiso(PermisosSistema.ProduccionActualizar));
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.FinanzasCrear));
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.TrabajadoresCrear));
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.AgricultoresCrear));
        Assert.False(BLL_Seguridad.TienePermiso(PermisosSistema.ProduccionEliminar));
    }
}

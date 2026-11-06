using System.Reflection;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class AutorizacionOperacionTests : IDisposable
{
    private static readonly HashSet<string> PermisosAgricultor = new(
        StringComparer.OrdinalIgnoreCase)
    {
        PermisosSistema.SiembrasCrear,
        PermisosSistema.SiembrasActualizar,
        PermisosSistema.SiembrasFinalizar,
        PermisosSistema.SiembrasAnular,
        PermisosSistema.CosechasCrear,
        PermisosSistema.CosechasActualizar,
        PermisosSistema.CosechasFinalizar,
        PermisosSistema.CosechasAnular,
        PermisosSistema.AlertasCrear,
        PermisosSistema.AlertasActualizar,
        PermisosSistema.AlertasCompletar,
        PermisosSistema.AlertasAnular,
        PermisosSistema.AlertasNotificar,
        PermisosSistema.ProduccionCrear,
        PermisosSistema.ProduccionActualizar,
        PermisosSistema.ProduccionFinalizar,
        PermisosSistema.ProduccionAnular,
        PermisosSistema.SeguimientoCrear,
        PermisosSistema.SeguimientoActualizar
    };

    private readonly List<EventoAuditoriaSeguridad> eventos = new();

    public AutorizacionOperacionTests()
    {
        BLL_Seguridad.CerrarSesion();
        BLL_Seguridad.ConfigurarAuditoriaParaPruebas(eventos.Add);
    }

    public static IEnumerable<object[]> TodosLosPermisos => ObtenerPermisos()
        .Select(permiso => new object[] { permiso });

    public static IEnumerable<object[]> MatrizAgricultor => ObtenerPermisos()
        .Select(permiso => new object[]
        {
            permiso,
            PermisosAgricultor.Contains(permiso)
        });

    [Theory]
    [MemberData(nameof(TodosLosPermisos))]
    public void Administrador_TieneCadaPermisoDeclarado(string permiso)
    {
        BLL_Seguridad.IniciarSesion(1, "admin", "Administrador", "Administrador");

        Assert.True(BLL_Seguridad.TienePermiso(permiso));
    }

    [Theory]
    [MemberData(nameof(MatrizAgricultor))]
    public void Agricultor_RespetaMatrizDeMinimoPrivilegio(
        string permiso,
        bool esperado)
    {
        BLL_Seguridad.IniciarSesion(2, "agricultor", "Agricultor", "Agricultor");

        Assert.Equal(esperado, BLL_Seguridad.TienePermiso(permiso));
    }

    [Theory]
    [MemberData(nameof(TodosLosPermisos))]
    public void SinSesion_DeniegaCadaOperacion(string permiso)
    {
        Assert.False(BLL_Seguridad.TienePermiso(permiso));
    }

    [Fact]
    public void PermisosDeclarados_NoTienenValoresDuplicados()
    {
        string[] permisos = ObtenerPermisos();

        Assert.Equal(
            permisos.Length,
            permisos.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Fact]
    public void EjecutarAutorizado_ExitoEjecutaYAudita()
    {
        BLL_Seguridad.IniciarSesion(1, "admin", "Administrador", "Administrador");

        int resultado = BLL_Seguridad.EjecutarAutorizado(
            PermisosSistema.FinanzasCrear,
            "MovimientoFinanciero",
            "Crear",
            "Prueba=1",
            () => 27,
            valor => valor > 0);

        Assert.Equal(27, resultado);
        EventoAuditoriaSeguridad evento = Assert.Single(eventos);
        Assert.Equal(ResultadosAuditoria.Exito, evento.Resultado);
        Assert.Equal("admin", evento.Usuario);
        Assert.Contains("Resultado=27", evento.Detalle);
    }

    [Fact]
    public void EjecutarAutorizado_ResultadoFallidoQuedaAuditado()
    {
        BLL_Seguridad.IniciarSesion(1, "admin", "Administrador", "Administrador");

        string resultado = BLL_Seguridad.EjecutarAutorizado(
            PermisosSistema.InventarioCrear,
            "Inventario",
            "Crear",
            "Codigo=TEST",
            () => "Ya existe un producto con ese codigo",
            BLL_Seguridad.EsResultadoTextoExitoso);

        Assert.Contains("Ya existe", resultado);
        Assert.Equal(ResultadosAuditoria.Fallido, Assert.Single(eventos).Resultado);
    }

    [Fact]
    public void EjecutarAutorizado_ExcepcionQuedaAuditadaYSePropaga()
    {
        BLL_Seguridad.IniciarSesion(1, "admin", "Administrador", "Administrador");

        InvalidOperationException error = Assert.Throws<InvalidOperationException>(() =>
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaActualizar,
                "Maquinaria",
                "Actualizar",
                "ID=9",
                () => throw new InvalidOperationException("Fallo controlado")));

        Assert.Equal("Fallo controlado", error.Message);
        EventoAuditoriaSeguridad evento = Assert.Single(eventos);
        Assert.Equal(ResultadosAuditoria.Error, evento.Resultado);
        Assert.Contains("Fallo controlado", evento.Detalle);
    }

    [Fact]
    public void EjecutarAutorizado_DenegadoNoEjecutaLaOperacion()
    {
        bool ejecutada = false;
        BLL_Seguridad.IniciarSesion(2, "agricultor", "Agricultor", "Agricultor");

        Assert.Throws<UnauthorizedAccessException>(() =>
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.FinanzasEliminar,
                "MovimientoFinanciero",
                "Eliminar",
                "ID=12",
                () => ejecutada = true));

        Assert.False(ejecutada);
        EventoAuditoriaSeguridad evento = Assert.Single(eventos);
        Assert.Equal(ResultadosAuditoria.Denegado, evento.Resultado);
    }

    [Theory]
    [InlineData("Guardado correctamente", true)]
    [InlineData("Actualizacion completada", true)]
    [InlineData("Ya existe un registro", false)]
    [InlineData("No se pudo guardar", false)]
    [InlineData("Error de validacion", false)]
    [InlineData("", false)]
    public void ResultadoTexto_ClasificaExitoYFallo(string resultado, bool esperado)
    {
        Assert.Equal(esperado, BLL_Seguridad.EsResultadoTextoExitoso(resultado));
    }

    public void Dispose()
    {
        BLL_Seguridad.ConfigurarAuditoriaParaPruebas(null);
        BLL_Seguridad.CerrarSesion();
    }

    private static string[] ObtenerPermisos()
    {
        return typeof(PermisosSistema)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(campo => campo.IsLiteral && !campo.IsInitOnly)
            .Select(campo => campo.GetRawConstantValue() as string)
            .Where(valor => !string.IsNullOrWhiteSpace(valor))
            .Cast<string>()
            .OrderBy(valor => valor, StringComparer.Ordinal)
            .ToArray();
    }
}

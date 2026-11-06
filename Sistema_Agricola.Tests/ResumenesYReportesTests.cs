using System.Data;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class ResumenesYReportesTests
{
    [Fact]
    public void Finanzas_SeparaPagadosPendientesYCalculaBalance()
    {
        DataTable datos = CrearTablaFinanzas();
        datos.Rows.Add("Ingreso", "Pagado", 1250m);
        datos.Rows.Add("Gasto", "Pagado", 430m);
        datos.Rows.Add("Ingreso", "Pendiente", 275m);
        datos.Rows.Add("Gasto", "Pendiente", 90m);

        var resumen = BLL_Finanzas.CalcularResumen(datos);

        Assert.Equal(1250m, resumen.Ingresos);
        Assert.Equal(430m, resumen.Gastos);
        Assert.Equal(820m, resumen.Balance);
        Assert.Equal(275m, resumen.PendienteCobro);
        Assert.Equal(90m, resumen.PendientePago);
    }

    [Fact]
    public void Finanzas_IgnoraTiposDesconocidosYValoresNulos()
    {
        DataTable datos = CrearTablaFinanzas();
        datos.Rows.Add("Otro", "Pagado", 500m);
        datos.Rows.Add("Ingreso", "Pagado", DBNull.Value);

        var resumen = BLL_Finanzas.CalcularResumen(datos);

        Assert.Equal(0m, resumen.Ingresos);
        Assert.Equal(0m, resumen.Gastos);
        Assert.Equal(0m, resumen.Balance);
    }

    [Fact]
    public void Inventario_CuentaStockBajoVencimientoYValorSoloDeActivos()
    {
        DataTable datos = new();
        datos.Columns.Add("Estado", typeof(string));
        datos.Columns.Add("Cantidad", typeof(decimal));
        datos.Columns.Add("Stock mínimo", typeof(decimal));
        datos.Columns.Add("Costo unitario", typeof(decimal));
        datos.Columns.Add("Fecha vencimiento", typeof(DateTime));
        datos.Rows.Add("Activo", 5m, 5m, 10m, DateTime.Today.AddDays(20));
        datos.Rows.Add("Activo", 12m, 4m, 3m, DateTime.Today.AddDays(90));
        datos.Rows.Add("Inactivo", 1m, 5m, 999m, DateTime.Today.AddDays(2));

        var resumen = BLL_Inventario.CalcularResumen(datos);

        Assert.Equal(2, resumen.ProductosActivos);
        Assert.Equal(1, resumen.BajoStock);
        Assert.Equal(1, resumen.PorVencer);
        Assert.Equal(86m, resumen.ValorInventario);
    }

    [Fact]
    public void Maquinaria_ResumeEstadosValorYMantenimientosProximos()
    {
        DataTable datos = new();
        datos.Columns.Add("Estado", typeof(string));
        datos.Columns.Add("Costo", typeof(decimal));
        datos.Columns.Add("Proximo mantenimiento", typeof(DateTime));
        datos.Rows.Add("Disponible", 1000m, DateTime.Today.AddDays(15));
        datos.Rows.Add("Asignada", 2500m, DateTime.Today.AddDays(60));
        datos.Rows.Add("Inactiva", 9999m, DateTime.Today.AddDays(1));

        var resumen = BLL_GestionMaquinaria.CalcularResumen(datos);

        Assert.Equal(2, resumen.TotalActivos);
        Assert.Equal(1, resumen.Disponibles);
        Assert.Equal(1, resumen.Asignadas);
        Assert.Equal(1, resumen.MantenimientoProximo);
        Assert.Equal(3500m, resumen.ValorActivos);
    }

    [Fact]
    public void Equipos_MultiplicaCantidadPorCostoYExcluyeInactivos()
    {
        DataTable datos = new();
        datos.Columns.Add("Estado", typeof(string));
        datos.Columns.Add("Cantidad", typeof(int));
        datos.Columns.Add("Costo", typeof(decimal));
        datos.Columns.Add("Proximo mantenimiento", typeof(DateTime));
        datos.Rows.Add("Activo", 4, 25m, DateTime.Today.AddDays(10));
        datos.Rows.Add("En mantenimiento", 2, 50m, DateTime.Today.AddDays(40));
        datos.Rows.Add("Inactivo", 20, 100m, DateTime.Today.AddDays(1));

        var resumen = BLL_MantenimientoEquipos.CalcularResumen(datos);

        Assert.Equal(2, resumen.TotalActivos);
        Assert.Equal(6, resumen.Unidades);
        Assert.Equal(1, resumen.MantenimientoProximo);
        Assert.Equal(200m, resumen.ValorTotal);
    }

    [Theory]
    [InlineData("finanzas")]
    [InlineData("inventario")]
    [InlineData("maquinaria")]
    [InlineData("equipos")]
    [InlineData("produccion")]
    public void Reportes_RechazanRangosDeFechaInvertidos(string modulo)
    {
        DateTime desde = new DateTime(2026, 8, 2);
        DateTime hasta = new DateTime(2026, 8, 1);

        Assert.Throws<ArgumentException>(() => modulo switch
        {
            "finanzas" => BLL_Finanzas.ListarMovimientos(desde, hasta, string.Empty),
            "inventario" => BLL_Inventario.ListarMovimientos(null, desde, hasta),
            "maquinaria" => BLL_GestionMaquinaria.ListarMantenimientos(null, desde, hasta),
            "equipos" => BLL_MantenimientoEquipos.ListarMantenimientos(null, desde, hasta),
            "produccion" => BLL_Produccion.Listar(desde, hasta),
            _ => throw new InvalidOperationException()
        });
    }

    private static DataTable CrearTablaFinanzas()
    {
        DataTable datos = new();
        datos.Columns.Add("Tipo", typeof(string));
        datos.Columns.Add("Estado", typeof(string));
        datos.Columns.Add("Monto", typeof(decimal));
        return datos;
    }
}

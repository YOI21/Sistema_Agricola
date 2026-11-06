using System.Data;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.Tests;

public sealed class DashboardSeguimientoTests
{
    [Fact]
    public void UltimosSeguimientos_SinCultivoConsultaTodosYOrdenaGlobalmente()
    {
        DataTable cultivos = new();
        cultivos.Columns.Add("ID_Cultivo", typeof(int));
        cultivos.Rows.Add(1);
        cultivos.Rows.Add(2);
        cultivos.Rows.Add(2);

        List<int> consultados = new();
        DataTable resultado = BLL_Dashboard.ObtenerUltimosSeguimientos(
            -1,
            2,
            id =>
            {
                consultados.Add(id);
                DataTable datos = CrearTablaSeguimientos();
                if (id == 1)
                {
                    datos.Rows.Add(11, 1, new DateTime(2026, 10, 1));
                    datos.Rows.Add(12, 1, new DateTime(2026, 10, 5));
                }
                else
                {
                    datos.Rows.Add(21, 2, new DateTime(2026, 10, 8));
                }

                return datos;
            },
            () => cultivos);

        Assert.Equal(new[] { 1, 2 }, consultados);
        Assert.Equal(2, resultado.Rows.Count);
        Assert.Equal(21, resultado.Rows[0].Field<int>("ID_Seguimiento"));
        Assert.Equal(12, resultado.Rows[1].Field<int>("ID_Seguimiento"));
    }

    [Fact]
    public void UltimosSeguimientos_ConCultivoSoloConsultaElSeleccionado()
    {
        List<int> consultados = new();

        DataTable resultado = BLL_Dashboard.ObtenerUltimosSeguimientos(
            7,
            10,
            id =>
            {
                consultados.Add(id);
                DataTable datos = CrearTablaSeguimientos();
                datos.Rows.Add(71, id, new DateTime(2026, 11, 1));
                return datos;
            },
            () => throw new InvalidOperationException(
                "No debe consultar el catálogo completo."));

        Assert.Equal(new[] { 7 }, consultados);
        Assert.Single(resultado.Rows.Cast<DataRow>());
        Assert.Equal(71, resultado.Rows[0].Field<int>("ID_Seguimiento"));
    }

    [Fact]
    public void UltimosSeguimientos_RechazaLimiteNoPositivo()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            BLL_Dashboard.ObtenerUltimosSeguimientos(
                -1,
                0,
                _ => CrearTablaSeguimientos(),
                () => new DataTable()));
    }

    private static DataTable CrearTablaSeguimientos()
    {
        DataTable datos = new();
        datos.Columns.Add("ID_Seguimiento", typeof(int));
        datos.Columns.Add("ID_Cultivo", typeof(int));
        datos.Columns.Add("Fecha_Registro", typeof(DateTime));
        return datos;
    }
}

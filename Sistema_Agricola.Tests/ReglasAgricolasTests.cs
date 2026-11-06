using Sistema_Agricola.Pr;

namespace Sistema_Agricola.Tests;

public sealed class ReglasAgricolasTests
{
    [Fact]
    public void TodosLosCultivosSoportados_DeclaranFuentesInstitucionales()
    {
        CondicionesAgroclimaticas condiciones = new()
        {
            FechaSiembra = new DateTime(2026, 5, 1),
            Provincia = "Duarte",
            TipoSuelo = "Franco",
            PH = 6,
            Humedad = 65,
            Drenaje = "Bueno",
            Pendiente = 5,
            PrecipitacionAnual = 1500,
            DisponibilidadRiego = true,
            Variedad = "Variedad local por validar"
        };

        foreach (string cultivo in RecomendadorAgricola.CultivosSoportados)
        {
            EvaluacionAgricola evaluacion = RecomendadorAgricola.Evaluar(
                cultivo,
                condiciones);

            Assert.NotEmpty(evaluacion.Fuentes);
            Assert.All(evaluacion.Fuentes, fuente =>
            {
                Assert.False(string.IsNullOrWhiteSpace(fuente.Id));
                Assert.False(string.IsNullOrWhiteSpace(fuente.Institucion));
                Assert.StartsWith("https://", fuente.Url);
            });
            Assert.Contains("validación agronómica", evaluacion.EstadoValidacion);
        }
    }

    [Theory]
    [InlineData("Arroz", 11, true)]
    [InlineData("Arroz", 5, false)]
    [InlineData("Maíz", 6, true)]
    [InlineData("Tabaco", 9, true)]
    [InlineData("Café", 2, true)]
    [InlineData("Cacao", 8, true)]
    [InlineData("Coco", 12, true)]
    public void VentanasOficiales_SeReflejanEnElCriterioDeSiembra(
        string cultivo,
        int mes,
        bool esperadoDentroDeVentana)
    {
        EvaluacionAgricola evaluacion = RecomendadorAgricola.Evaluar(
            cultivo,
            new CondicionesAgroclimaticas
            {
                FechaSiembra = new DateTime(2026, mes, 1),
                Provincia = "Duarte",
                TipoSuelo = "Franco",
                PH = 6,
                Humedad = 65,
                Drenaje = "Bueno",
                Pendiente = 5,
                PrecipitacionAnual = 1500,
                DisponibilidadRiego = false,
                Variedad = "Referencia"
            });

        CriterioEvaluadoAgricola periodo = Assert.Single(
            evaluacion.CriteriosEvaluados,
            criterio => criterio.Nombre == "Período de siembra");

        Assert.Equal(
            esperadoDentroDeVentana,
            periodo.Estado == EstadoCriterioAgricola.Favorable);
    }
}

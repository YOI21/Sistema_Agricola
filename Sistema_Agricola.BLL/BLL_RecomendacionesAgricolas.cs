using PrCalendario = Sistema_Agricola.Pr.CalendarioAgricola;
using PrCondiciones = Sistema_Agricola.Pr.CondicionesAgroclimaticas;
using PrEvaluacion = Sistema_Agricola.Pr.EvaluacionAgricola;
using PrNivel = Sistema_Agricola.Pr.NivelRecomendacionAgricola;
using PrRecomendador = Sistema_Agricola.Pr.RecomendadorAgricola;
using PrZonificacion = Sistema_Agricola.Pr.ZonificacionAgroclimaticaDominicana;

namespace Sistema_Agricola.BLL;

public enum NivelRecomendacionAgricola
{
    SinInformacion = 0,
    NoRecomendado = 1,
    Condicionado = 2,
    Recomendado = 3
}

public sealed class CondicionesAgroclimaticas
{
    public DateTime FechaSiembra { get; init; } = DateTime.Today;
    public string Provincia { get; init; } = string.Empty;
    public string ZonaAgroclimatica { get; init; } = string.Empty;
    public string TipoSuelo { get; init; } = string.Empty;
    public decimal? PH { get; init; }
    public decimal? Humedad { get; init; }
    public string Drenaje { get; init; } = string.Empty;
    public decimal? Pendiente { get; init; }
    public decimal? PrecipitacionAnual { get; init; }
    public bool DisponibilidadRiego { get; init; }
    public string Variedad { get; init; } = string.Empty;
}

public sealed record FuenteAgricola(
    string Id,
    string Institucion,
    string Titulo,
    string Url,
    string Alcance);

public sealed class EvaluacionAgricola
{
    public NivelRecomendacionAgricola Nivel { get; init; }
    public bool TemporadaCompatible { get; init; }
    public bool SueloCompatible { get; init; }
    public bool ExisteReglaCombinada { get; init; }
    public int Puntuacion { get; init; }
    public string ZonaAgroclimatica { get; init; } = string.Empty;
    public string PeriodoClimatico { get; init; } = string.Empty;
    public string Resumen { get; init; } = string.Empty;
    public IReadOnlyList<string> Criterios { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Advertencias { get; init; } = Array.Empty<string>();
    public IReadOnlyList<FuenteAgricola> Fuentes { get; init; } = Array.Empty<FuenteAgricola>();
    public string EstadoValidacion { get; init; } = string.Empty;
    public int Prioridad => (int)Nivel;
    public bool RequiereConfirmacion =>
        Nivel is NivelRecomendacionAgricola.NoRecomendado or
            NivelRecomendacionAgricola.SinInformacion;
}

public static class BLL_RecomendacionesAgricolas
{
    public static IReadOnlyList<string> CultivosSoportados =>
        PrRecomendador.CultivosSoportados;

    public static IReadOnlyList<string> Provincias => PrZonificacion.Provincias;

    public static IReadOnlyList<string> ZonasAgroclimaticas => PrZonificacion.Zonas;

    public static string InferirZona(string provincia) =>
        PrZonificacion.InferirZona(provincia);

    public static EvaluacionAgricola Evaluar(
        string nombreCultivo,
        CondicionesAgroclimaticas condiciones)
    {
        ArgumentNullException.ThrowIfNull(condiciones);
        return Convertir(PrRecomendador.Evaluar(
            nombreCultivo,
            new PrCondiciones
            {
                FechaSiembra = condiciones.FechaSiembra,
                Provincia = condiciones.Provincia,
                ZonaAgroclimatica = condiciones.ZonaAgroclimatica,
                TipoSuelo = condiciones.TipoSuelo,
                PH = condiciones.PH,
                Humedad = condiciones.Humedad,
                Drenaje = condiciones.Drenaje,
                Pendiente = condiciones.Pendiente,
                PrecipitacionAnual = condiciones.PrecipitacionAnual,
                DisponibilidadRiego = condiciones.DisponibilidadRiego,
                Variedad = condiciones.Variedad
            }));
    }

    public static EvaluacionAgricola Evaluar(
        string nombreCultivo,
        string periodo,
        string tipoSuelo) =>
        Convertir(PrRecomendador.Evaluar(nombreCultivo, periodo, tipoSuelo));

    public static string ObtenerTemporadaActual() =>
        PrCalendario.ObtenerTemporadaActual();

    public static string ObtenerPeriodoClimatico(
        DateTime fecha,
        string zonaAgroclimatica) =>
        PrCalendario.ObtenerPeriodoClimatico(fecha, zonaAgroclimatica);

    public static List<string> RecomendacionesPorMes(int mes) =>
        PrCalendario.RecomendacionesPorMes(mes);

    public static (
        string MejorMesSiembra,
        string MejorMesCosecha,
        int DuracionAproxMeses) ObtenerCicloParaCultivo(string nombreCultivo) =>
        PrCalendario.ObtenerCicloParaCultivo(nombreCultivo);

    public static bool EsBuenaTemporadaSiembra(
        string nombreCultivo,
        DateTime fechaSiembra) =>
        PrCalendario.EsBuenaTemporadaSiembra(nombreCultivo, fechaSiembra);

    private static EvaluacionAgricola Convertir(PrEvaluacion evaluacion) => new()
    {
        Nivel = evaluacion.Nivel switch
        {
            PrNivel.Recomendado => NivelRecomendacionAgricola.Recomendado,
            PrNivel.Condicionado => NivelRecomendacionAgricola.Condicionado,
            PrNivel.NoRecomendado => NivelRecomendacionAgricola.NoRecomendado,
            _ => NivelRecomendacionAgricola.SinInformacion
        },
        TemporadaCompatible = evaluacion.TemporadaCompatible,
        SueloCompatible = evaluacion.SueloCompatible,
        ExisteReglaCombinada = evaluacion.ExisteReglaCombinada,
        Puntuacion = evaluacion.Puntuacion,
        ZonaAgroclimatica = evaluacion.ZonaAgroclimatica,
        PeriodoClimatico = evaluacion.PeriodoClimatico,
        Resumen = evaluacion.Resumen,
        Criterios = evaluacion.Criterios.ToArray(),
        Advertencias = evaluacion.Advertencias.ToArray(),
        Fuentes = evaluacion.Fuentes
            .Select(fuente => new FuenteAgricola(
                fuente.Id,
                fuente.Institucion,
                fuente.Titulo,
                fuente.Url,
                fuente.Alcance))
            .ToArray(),
        EstadoValidacion = evaluacion.EstadoValidacion
    };
}

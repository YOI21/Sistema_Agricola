using System.Globalization;
using System.Text;

namespace Sistema_Agricola.Pr
{
    public enum NivelRecomendacionAgricola
    {
        SinInformacion = 0,
        NoRecomendado = 1,
        Condicionado = 2,
        Recomendado = 3
    }

    public enum EstadoCriterioAgricola
    {
        SinDatos = 0,
        NoFavorable = 1,
        Condicionado = 2,
        Favorable = 3
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

    public sealed class CriterioEvaluadoAgricola
    {
        public string Nombre { get; init; } = string.Empty;
        public EstadoCriterioAgricola Estado { get; init; }
        public string Detalle { get; init; } = string.Empty;
        public bool Critico { get; init; }

        public override string ToString()
        {
            string estado = Estado switch
            {
                EstadoCriterioAgricola.Favorable => "Favorable",
                EstadoCriterioAgricola.Condicionado => "Condicionado",
                EstadoCriterioAgricola.NoFavorable => "No favorable",
                _ => "Sin datos"
            };

            return $"[{estado}] {Nombre}: {Detalle}";
        }
    }

    public sealed record FuenteAgricola(
        string Id,
        string Institucion,
        string Titulo,
        string Url,
        string Alcance);

    public static class BibliografiaAgricola
    {
        public const string FaoEcocrop = "FAO-ECOCROP";
        public const string MinisterioCalendario = "MARD-CALENDARIO";
        public const string IndometPrecipitacion = "INDOMET-PRECIPITACION";
        public const string CedafGuias = "CEDAF-GUIAS";
        public const string DigeraGuias = "DIGERA-GUIAS";

        private static readonly IReadOnlyDictionary<string, FuenteAgricola> Catalogo =
            new Dictionary<string, FuenteAgricola>(StringComparer.OrdinalIgnoreCase)
            {
                [FaoEcocrop] = new(
                    FaoEcocrop,
                    "Organización de las Naciones Unidas para la Alimentación y la Agricultura (FAO)",
                    "ECOCROP: base de requisitos ecológicos de cultivos",
                    "https://www.fao.org/geospatial/data-and-tools/data-portals/ecocrop/",
                    "Rangos de precipitación, pH, textura y drenaje por especie."),
                [MinisterioCalendario] = new(
                    MinisterioCalendario,
                    "Ministerio de Agricultura de la República Dominicana",
                    "Calendario de siembra, cosecha y ciclo vegetativo de los principales cultivos",
                    "https://agricultura.gob.do/wp-content/uploads/2022/09/Indice-Estacional-de-Precios-2019.pdf",
                    "Ventanas nacionales de siembra y cosecha."),
                [IndometPrecipitacion] = new(
                    IndometPrecipitacion,
                    "Instituto Dominicano de Meteorología (INDOMET)",
                    "Acumulados de precipitación mensual 2018-2026",
                    "https://indomet.gob.do/transparencia/datos-abiertos/acumulados-de-precipitacion/",
                    "Contraste climático y pluviométrico por estación y período."),
                [CedafGuias] = new(
                    CedafGuias,
                    "Centro para el Desarrollo Agropecuario y Forestal (CEDAF)",
                    "Guías técnicas de cultivos para República Dominicana",
                    "https://www.cedaf.org.do/guias-tecnicas/",
                    "Manejo local de suelo, material de siembra, riego y cultivo."),
                [DigeraGuias] = new(
                    DigeraGuias,
                    "Dirección General de Riesgos Agropecuarios (DIGERA)",
                    "Guías de cultivos",
                    "https://digera.gob.do/seguro-agropecuario/guia-de-cultivos/",
                    "Riesgos y condiciones de referencia para cultivos dominicanos.")
            };

        public static IReadOnlyList<FuenteAgricola> Obtener(IEnumerable<string> ids) => ids
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Where(Catalogo.ContainsKey)
            .Select(id => Catalogo[id])
            .ToArray();
    }

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
        public IReadOnlyList<CriterioEvaluadoAgricola> CriteriosEvaluados { get; init; } =
            Array.Empty<CriterioEvaluadoAgricola>();
        public IReadOnlyList<string> Criterios => CriteriosEvaluados.Select(c => c.ToString()).ToArray();
        public IReadOnlyList<string> Advertencias { get; init; } = Array.Empty<string>();
        public IReadOnlyList<FuenteAgricola> Fuentes { get; init; } = Array.Empty<FuenteAgricola>();
        public string EstadoValidacion { get; init; } =
            "Pendiente de validación agronómica documentada";

        public int Prioridad => (int)Nivel;
        public bool RequiereConfirmacion =>
            Nivel == NivelRecomendacionAgricola.NoRecomendado ||
            Nivel == NivelRecomendacionAgricola.SinInformacion;
    }

    public static class ZonificacionAgroclimaticaDominicana
    {
        public const string HumedaNorteNordeste = "Húmeda del Norte y Nordeste";
        public const string SubhumedaCibaoEste = "Subhúmeda del Cibao y Este";
        public const string SemiaridaSurNoroeste = "Semiárida del Sur y Noroeste";
        public const string MontanaHumeda = "Montañosa húmeda";
        public const string CosteraCalida = "Costera cálida";

        public static IReadOnlyList<string> Zonas { get; } = new[]
        {
            HumedaNorteNordeste,
            SubhumedaCibaoEste,
            SemiaridaSurNoroeste,
            MontanaHumeda,
            CosteraCalida
        };

        public static IReadOnlyList<string> Provincias { get; } = new[]
        {
            "Azua", "Bahoruco", "Barahona", "Dajabón", "Distrito Nacional", "Duarte",
            "Elías Piña", "El Seibo", "Espaillat", "Hato Mayor", "Hermanas Mirabal",
            "Independencia", "La Altagracia", "La Romana", "La Vega", "María Trinidad Sánchez",
            "Monseñor Nouel", "Monte Cristi", "Monte Plata", "Pedernales", "Peravia",
            "Puerto Plata", "Samaná", "San Cristóbal", "San José de Ocoa", "San Juan",
            "San Pedro de Macorís", "Sánchez Ramírez", "Santiago", "Santiago Rodríguez",
            "Santo Domingo", "Valverde"
        };

        public static string InferirZona(string provincia)
        {
            string valor = Normalizar(provincia);
            if (EstaEn(valor, "la vega", "monsenor nouel", "san jose de ocoa"))
                return MontanaHumeda;
            if (EstaEn(valor, "azua", "bahoruco", "barahona", "dajabon", "elias pina",
                "independencia", "monte cristi", "pedernales", "peravia", "san juan",
                "santiago rodriguez", "valverde"))
                return SemiaridaSurNoroeste;
            if (EstaEn(valor, "puerto plata", "espaillat", "hermanas mirabal", "duarte",
                "maria trinidad sanchez", "samana", "sanchez ramirez"))
                return HumedaNorteNordeste;
            if (EstaEn(valor, "distrito nacional", "la altagracia", "la romana",
                "san pedro de macoris", "santo domingo"))
                return CosteraCalida;
            return string.IsNullOrWhiteSpace(valor) ? string.Empty : SubhumedaCibaoEste;
        }

        private static bool EstaEn(string valor, params string[] opciones) => opciones.Contains(valor);

        internal static string Normalizar(string valor)
        {
            string descompuesto = (valor ?? string.Empty).Trim().ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);
            return new string(descompuesto
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
                .Normalize(NormalizationForm.FormC);
        }
    }

    public static class RecomendadorAgricola
    {
        private sealed record ReglaCultivo(
            string Nombre,
            string[] Suelos,
            decimal PhMin,
            decimal PhMax,
            decimal HumedadMin,
            decimal HumedadMax,
            decimal LluviaMin,
            decimal LluviaMax,
            decimal PendienteMax,
            string[] Drenajes,
            string[] Zonas,
            int[] MesesSiembra,
            string[] FuenteIds,
            bool ToleraPeriodoSeco = false);

        private static readonly IReadOnlyDictionary<string, ReglaCultivo> Reglas = CrearReglas();

        public static IReadOnlyList<string> CultivosSoportados { get; } = Reglas.Values
            .Select(r => r.Nombre)
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(n => n)
            .ToArray();

        public static EvaluacionAgricola Evaluar(
            string nombreCultivo,
            CondicionesAgroclimaticas condiciones)
        {
            if (string.IsNullOrWhiteSpace(nombreCultivo) || condiciones == null)
                return CrearSinInformacion("Falta seleccionar el cultivo o indicar las condiciones del terreno.");
            if (!Reglas.TryGetValue(Normalizar(nombreCultivo), out ReglaCultivo? regla))
                return CrearSinInformacion($"No hay una regla agrícola registrada para {nombreCultivo}.");

            string zona = string.IsNullOrWhiteSpace(condiciones.ZonaAgroclimatica)
                ? ZonificacionAgroclimaticaDominicana.InferirZona(condiciones.Provincia)
                : condiciones.ZonaAgroclimatica.Trim();
            string periodo = CalendarioAgricola.ObtenerPeriodoClimatico(condiciones.FechaSiembra, zona);
            List<CriterioEvaluadoAgricola> criterios = new()
            {
                EvaluarTexto("Zona agroclimática", zona, regla.Zonas,
                    "La provincia o zona no está indicada.", true),
                EvaluarTexto("Tipo de suelo", condiciones.TipoSuelo, regla.Suelos,
                    "No se registró el tipo de suelo.", true),
                EvaluarRango("pH del suelo", condiciones.PH, regla.PhMin, regla.PhMax, "pH", 0.7m, true),
                EvaluarRango("Humedad del suelo", condiciones.Humedad,
                    regla.HumedadMin, regla.HumedadMax, "%", 15m),
                EvaluarTexto("Drenaje", condiciones.Drenaje, regla.Drenajes,
                    "No se registró la capacidad de drenaje.", true),
                EvaluarMaximo("Pendiente", condiciones.Pendiente, regla.PendienteMax, "%"),
                EvaluarPrecipitacion(regla, condiciones),
                EvaluarRiego(regla, condiciones, periodo),
                EvaluarPeriodo(regla, condiciones.FechaSiembra, periodo, condiciones.DisponibilidadRiego),
                EvaluarVariedad(condiciones.Variedad)
            };

            int medidos = criterios.Count(c => c.Estado != EstadoCriterioAgricola.SinDatos);
            int faltantes = criterios.Count - medidos;
            int criticosNoFavorables = criterios.Count(c =>
                c.Estado == EstadoCriterioAgricola.NoFavorable && c.Critico);
            bool cualquierNoFavorable = criterios.Any(c => c.Estado == EstadoCriterioAgricola.NoFavorable);
            int puntuacion = CalcularPuntuacion(criterios);
            NivelRecomendacionAgricola nivel;

            if (medidos < 6 || faltantes >= 5)
                nivel = NivelRecomendacionAgricola.SinInformacion;
            else if (criticosNoFavorables > 0 || puntuacion < 45)
                nivel = NivelRecomendacionAgricola.NoRecomendado;
            else if (cualquierNoFavorable || puntuacion < 78 || faltantes > 1)
                nivel = NivelRecomendacionAgricola.Condicionado;
            else
                nivel = NivelRecomendacionAgricola.Recomendado;

            List<string> advertencias = new()
            {
                "Orientación basada en reglas agrícolas; no sustituye un análisis de suelo ni la recomendación de un técnico local.",
                "Los rangos son una síntesis de fuentes institucionales y permanecen pendientes de validación agronómica firmada."
            };
            if (faltantes > 0)
                advertencias.Add($"Faltan {faltantes} de 10 condiciones por medir o registrar.");
            if (string.IsNullOrWhiteSpace(condiciones.ZonaAgroclimatica) && !string.IsNullOrWhiteSpace(zona))
                advertencias.Add($"La zona {zona} fue inferida a partir de la provincia; puede ajustarla en el terreno.");

            return new EvaluacionAgricola
            {
                Nivel = nivel,
                Puntuacion = puntuacion,
                ZonaAgroclimatica = zona,
                PeriodoClimatico = periodo,
                TemporadaCompatible = criterios.First(c => c.Nombre == "Período de siembra").Estado == EstadoCriterioAgricola.Favorable,
                SueloCompatible = criterios.First(c => c.Nombre == "Tipo de suelo").Estado == EstadoCriterioAgricola.Favorable,
                ExisteReglaCombinada = true,
                Resumen = ObtenerResumen(nivel, puntuacion),
                CriteriosEvaluados = criterios,
                Advertencias = advertencias,
                Fuentes = BibliografiaAgricola.Obtener(regla.FuenteIds)
            };
        }

        public static EvaluacionAgricola Evaluar(string nombreCultivo, string periodo, string tipoSuelo)
        {
            int mes = Normalizar(periodo) switch
            {
                "periodo lluvioso" => 5,
                "periodo seco" => 2,
                "transicion" => 7,
                "primavera" => 4,
                "verano" => 7,
                "otono" => 10,
                "invierno" => 1,
                _ => DateTime.Today.Month
            };
            return Evaluar(nombreCultivo, new CondicionesAgroclimaticas
            {
                FechaSiembra = new DateTime(DateTime.Today.Year, mes, 1),
                TipoSuelo = tipoSuelo
            });
        }

        private static CriterioEvaluadoAgricola EvaluarTexto(
            string nombre, string valor, IReadOnlyCollection<string> aceptados,
            string mensajeSinDatos, bool critico)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return Criterio(nombre, EstadoCriterioAgricola.SinDatos, mensajeSinDatos, critico);
            bool compatible = aceptados.Any(a => Coincide(a, valor));
            return Criterio(nombre,
                compatible ? EstadoCriterioAgricola.Favorable : EstadoCriterioAgricola.NoFavorable,
                compatible
                    ? $"{valor}; está dentro de los valores admitidos."
                    : $"{valor}; se recomienda {string.Join(", ", aceptados)}.",
                critico);
        }

        private static CriterioEvaluadoAgricola EvaluarRango(
            string nombre, decimal? valor, decimal minimo, decimal maximo,
            string unidad, decimal tolerancia, bool critico = false)
        {
            if (!valor.HasValue || valor.Value <= 0)
                return Criterio(nombre, EstadoCriterioAgricola.SinDatos, "No se registró una medición.", critico);
            decimal numero = valor.Value;
            if (numero >= minimo && numero <= maximo)
                return Criterio(nombre, EstadoCriterioAgricola.Favorable,
                    $"{numero:0.##} {unidad}; rango recomendado {minimo:0.##}-{maximo:0.##} {unidad}.", critico);
            bool cercano = numero >= minimo - tolerancia && numero <= maximo + tolerancia;
            return Criterio(nombre,
                cercano ? EstadoCriterioAgricola.Condicionado : EstadoCriterioAgricola.NoFavorable,
                $"{numero:0.##} {unidad}; rango recomendado {minimo:0.##}-{maximo:0.##} {unidad}.",
                critico && !cercano);
        }

        private static CriterioEvaluadoAgricola EvaluarMaximo(
            string nombre, decimal? valor, decimal maximo, string unidad)
        {
            if (!valor.HasValue || valor.Value < 0)
                return Criterio(nombre, EstadoCriterioAgricola.SinDatos, "No se registró una medición.");
            if (valor.Value <= maximo)
                return Criterio(nombre, EstadoCriterioAgricola.Favorable,
                    $"{valor.Value:0.##} {unidad}; máximo orientativo {maximo:0.##} {unidad}.");
            return Criterio(nombre,
                valor.Value <= maximo + 5 ? EstadoCriterioAgricola.Condicionado : EstadoCriterioAgricola.NoFavorable,
                $"{valor.Value:0.##} {unidad}; supera el máximo orientativo de {maximo:0.##} {unidad}.");
        }

        private static CriterioEvaluadoAgricola EvaluarPrecipitacion(
            ReglaCultivo regla, CondicionesAgroclimaticas condiciones)
        {
            if (!condiciones.PrecipitacionAnual.HasValue || condiciones.PrecipitacionAnual.Value <= 0)
                return Criterio("Precipitación anual", EstadoCriterioAgricola.SinDatos,
                    "No se registró la lluvia anual estimada.");
            decimal lluvia = condiciones.PrecipitacionAnual.Value;
            if (lluvia >= regla.LluviaMin && lluvia <= regla.LluviaMax)
                return Criterio("Precipitación anual", EstadoCriterioAgricola.Favorable,
                    $"{lluvia:0} mm; rango orientativo {regla.LluviaMin:0}-{regla.LluviaMax:0} mm.");
            if (lluvia < regla.LluviaMin && condiciones.DisponibilidadRiego)
                return Criterio("Precipitación anual", EstadoCriterioAgricola.Condicionado,
                    $"{lluvia:0} mm; es inferior al rango, pero puede compensarse con un plan de riego.");
            return Criterio("Precipitación anual", EstadoCriterioAgricola.NoFavorable,
                $"{lluvia:0} mm; rango orientativo {regla.LluviaMin:0}-{regla.LluviaMax:0} mm.",
                lluvia < regla.LluviaMin);
        }

        private static CriterioEvaluadoAgricola EvaluarRiego(
            ReglaCultivo regla, CondicionesAgroclimaticas condiciones, string periodo)
        {
            bool lluviaBaja = condiciones.PrecipitacionAnual.HasValue &&
                condiciones.PrecipitacionAnual.Value > 0 &&
                condiciones.PrecipitacionAnual.Value < regla.LluviaMin;
            bool seNecesita = lluviaBaja ||
                (Coincide(periodo, "Período seco") && !regla.ToleraPeriodoSeco);
            if (!seNecesita)
                return Criterio("Disponibilidad de riego", EstadoCriterioAgricola.Favorable,
                    condiciones.DisponibilidadRiego
                        ? "Hay riego disponible como apoyo."
                        : "No es imprescindible con la lluvia y el período registrados.");
            return Criterio("Disponibilidad de riego",
                condiciones.DisponibilidadRiego ? EstadoCriterioAgricola.Favorable : EstadoCriterioAgricola.NoFavorable,
                condiciones.DisponibilidadRiego
                    ? "Hay riego disponible para compensar el déficit estimado."
                    : "El cultivo necesita una fuente de riego bajo estas condiciones.",
                !condiciones.DisponibilidadRiego);
        }

        private static CriterioEvaluadoAgricola EvaluarPeriodo(
            ReglaCultivo regla, DateTime fecha, string periodo, bool tieneRiego)
        {
            if (regla.MesesSiembra.Contains(fecha.Month))
                return Criterio("Período de siembra", EstadoCriterioAgricola.Favorable,
                    $"{fecha:MMMM}; corresponde a una ventana orientativa ({periodo.ToLowerInvariant()}).");
            return Criterio("Período de siembra",
                tieneRiego ? EstadoCriterioAgricola.Condicionado : EstadoCriterioAgricola.NoFavorable,
                tieneRiego
                    ? $"{fecha:MMMM} queda fuera de la ventana usual; requiere manejo técnico y riego."
                    : $"{fecha:MMMM} queda fuera de la ventana usual y no hay riego registrado.");
        }

        private static CriterioEvaluadoAgricola EvaluarVariedad(string variedad)
        {
            if (string.IsNullOrWhiteSpace(variedad))
                return Criterio("Variedad", EstadoCriterioAgricola.SinDatos,
                    "No se indicó la variedad; su ciclo y tolerancias pueden cambiar el resultado.");
            return Criterio("Variedad", EstadoCriterioAgricola.Condicionado,
                $"{variedad.Trim()}; confirme su ficha técnica, procedencia y adaptación local.");
        }

        private static CriterioEvaluadoAgricola Criterio(
            string nombre, EstadoCriterioAgricola estado, string detalle, bool critico = false) => new()
        {
            Nombre = nombre,
            Estado = estado,
            Detalle = detalle,
            Critico = critico
        };

        private static int CalcularPuntuacion(IEnumerable<CriterioEvaluadoAgricola> criterios)
        {
            CriterioEvaluadoAgricola[] medidos = criterios
                .Where(c => c.Estado != EstadoCriterioAgricola.SinDatos)
                .ToArray();
            if (medidos.Length == 0)
                return 0;
            decimal total = medidos.Sum(c => c.Estado switch
            {
                EstadoCriterioAgricola.Favorable => 100m,
                EstadoCriterioAgricola.Condicionado => 60m,
                _ => 0m
            });
            return (int)Math.Round(total / medidos.Length, MidpointRounding.AwayFromZero);
        }

        private static EvaluacionAgricola CrearSinInformacion(string advertencia) => new()
        {
            Nivel = NivelRecomendacionAgricola.SinInformacion,
            Resumen = "No hay información suficiente para emitir una recomendación agrícola.",
            Advertencias = new[] { advertencia }
        };

        private static string ObtenerResumen(NivelRecomendacionAgricola nivel, int puntuacion) => nivel switch
        {
            NivelRecomendacionAgricola.Recomendado =>
                $"Condiciones favorables según las reglas registradas ({puntuacion}/100).",
            NivelRecomendacionAgricola.Condicionado =>
                $"Siembra condicionada; revise los criterios señalados ({puntuacion}/100).",
            NivelRecomendacionAgricola.NoRecomendado =>
                $"No se recomienda sembrar sin corregir las condiciones críticas ({puntuacion}/100).",
            _ => "No hay información suficiente para emitir una recomendación agrícola."
        };

        private static IReadOnlyDictionary<string, ReglaCultivo> CrearReglas()
        {
            string[] todas = ZonificacionAgroclimaticaDominicana.Zonas.ToArray();
            string[] humedas =
            {
                ZonificacionAgroclimaticaDominicana.HumedaNorteNordeste,
                ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste,
                ZonificacionAgroclimaticaDominicana.MontanaHumeda
            };
            string[] calidas =
            {
                ZonificacionAgroclimaticaDominicana.HumedaNorteNordeste,
                ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste,
                ZonificacionAgroclimaticaDominicana.SemiaridaSurNoroeste,
                ZonificacionAgroclimaticaDominicana.CosteraCalida
            };
            string[] altas =
            {
                ZonificacionAgroclimaticaDominicana.MontanaHumeda,
                ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste
            };
            string[] secas =
            {
                ZonificacionAgroclimaticaDominicana.SemiaridaSurNoroeste,
                ZonificacionAgroclimaticaDominicana.CosteraCalida,
                ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste
            };
            int[] todoElAno = Enumerable.Range(1, 12).ToArray();
            List<ReglaCultivo> reglas = new();

            void Agregar(string[] nombres, string[] suelos, decimal phMin, decimal phMax,
                decimal humedadMin, decimal humedadMax, decimal lluviaMin, decimal lluviaMax,
                decimal pendienteMax, string[] drenajes, string[] zonas, int[] meses,
                bool toleraSeco = false)
            {
                string[] fuentes =
                {
                    BibliografiaAgricola.FaoEcocrop,
                    BibliografiaAgricola.MinisterioCalendario,
                    BibliografiaAgricola.IndometPrecipitacion,
                    BibliografiaAgricola.CedafGuias,
                    BibliografiaAgricola.DigeraGuias
                };

                foreach (string nombre in nombres)
                    reglas.Add(new ReglaCultivo(nombre, suelos, phMin, phMax, humedadMin, humedadMax,
                        lluviaMin, lluviaMax, pendienteMax, drenajes, zonas, meses, fuentes, toleraSeco));
            }

            Agregar(new[] { "Arroz" }, new[] { "Arcilloso", "Limoso" }, 5, 7, 70, 100, 1200, 2500, 5,
                new[] { "Deficiente", "Moderado" }, new[] { ZonificacionAgroclimaticaDominicana.HumedaNorteNordeste, ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste }, new[] { 11, 12, 1, 2, 3, 4, 6, 7, 8 });
            Agregar(new[] { "Café" }, new[] { "Franco", "Franco arcilloso" }, 5, 6.5m, 55, 80, 1200, 2200, 35,
                new[] { "Bueno", "Moderado" }, altas, todoElAno);
            Agregar(new[] { "Cacao", "Cacao Orgánico" }, new[] { "Franco", "Franco arcilloso", "Limoso" }, 5.5m, 6.5m, 60, 85, 1500, 2800, 20,
                new[] { "Bueno", "Moderado" }, humedas, todoElAno);
            Agregar(new[] { "Plátano", "Guineo" }, new[] { "Franco", "Franco arcilloso", "Limoso" }, 5.5m, 7.2m, 60, 90, 1400, 2800, 20,
                new[] { "Bueno", "Moderado" }, humedas, new[] { 3, 4, 5, 6, 9, 10 });
            Agregar(new[] { "Caña de Azúcar" }, new[] { "Franco", "Arcilloso", "Limoso" }, 5.5m, 7.8m, 50, 80, 1000, 2200, 12,
                new[] { "Bueno", "Moderado" }, calidas, new[] { 4, 5, 6, 7, 11, 12, 1 });
            Agregar(new[] { "Tabaco" }, new[] { "Franco", "Franco arenoso", "Arenoso" }, 5.5m, 7, 40, 65, 800, 1500, 12,
                new[] { "Bueno", "Excesivo" }, secas, new[] { 9, 10, 11 }, true);
            Agregar(new[] { "Aguacate" }, new[] { "Franco", "Franco arenoso" }, 5.5m, 7, 45, 75, 1000, 2000, 25,
                new[] { "Bueno", "Excesivo" }, todas, new[] { 3, 4, 5, 9, 10 });
            Agregar(new[] { "Piña" }, new[] { "Arenoso", "Franco arenoso", "Franco" }, 4.5m, 6.5m, 40, 75, 1000, 2500, 15,
                new[] { "Bueno", "Excesivo" }, calidas, new[] { 3, 4, 5, 6, 9, 10 });
            Agregar(new[] { "Coco" }, new[] { "Arenoso", "Franco arenoso", "Franco" }, 4.5m, 6.5m, 40, 75, 1000, 2500, 15,
                new[] { "Bueno", "Excesivo" }, calidas, todoElAno);
            Agregar(new[] { "Mango", "Guayaba", "Zapote", "Naranja", "Limón" }, new[] { "Franco", "Franco arenoso", "Limoso" }, 5.5m, 7.5m, 40, 75, 800, 2000, 25,
                new[] { "Bueno", "Moderado" }, calidas, new[] { 3, 4, 5, 9, 10 }, true);
            Agregar(new[] { "Habichuela", "Guandules" }, new[] { "Franco", "Franco arenoso", "Limoso", "Arcilloso" }, 5.5m, 7.5m, 40, 70, 600, 1600, 15,
                new[] { "Bueno", "Moderado" }, todas, new[] { 3, 4, 5, 9, 10, 11 }, true);
            Agregar(new[] { "Maíz" }, new[] { "Franco", "Franco arenoso", "Limoso", "Arcilloso" }, 5.5m, 7.5m, 40, 70, 600, 1600, 15,
                new[] { "Bueno", "Moderado" }, todas, new[] { 2, 3, 4, 6, 7 }, true);
            Agregar(new[] { "Yuca", "Batata", "Ñame" }, new[] { "Franco", "Franco arenoso", "Arenoso", "Limoso" }, 5, 7, 40, 75, 800, 1800, 20,
                new[] { "Bueno", "Excesivo" }, calidas, new[] { 3, 4, 5, 6, 9, 10 });
            Agregar(new[] { "Tomate", "Ají", "Berenjena", "Auyama" }, new[] { "Franco", "Franco arenoso", "Limoso" }, 5.5m, 7.2m, 45, 70, 600, 1500, 15,
                new[] { "Bueno", "Moderado" }, todas, new[] { 2, 3, 4, 9, 10, 11 }, true);
            Agregar(new[] { "Cebolla", "Zanahoria", "Repollo", "Lechuga" }, new[] { "Franco", "Franco arenoso", "Arenoso", "Limoso" }, 5.8m, 7.2m, 40, 65, 600, 1400, 15,
                new[] { "Bueno", "Excesivo" }, altas.Concat(secas).Distinct().ToArray(), new[] { 10, 11, 12, 1, 2 }, true);
            Agregar(new[] { "Chinola", "Lechosa", "Tayota" }, new[] { "Franco", "Franco arenoso", "Limoso" }, 5.5m, 7, 50, 80, 1000, 2200, 25,
                new[] { "Bueno", "Moderado" }, humedas.Concat(new[] { ZonificacionAgroclimaticaDominicana.CosteraCalida }).ToArray(), new[] { 3, 4, 5, 6, 9, 10 });
            Agregar(new[] { "Moringa", "Sábila" }, new[] { "Arenoso", "Franco arenoso", "Franco" }, 5.5m, 8, 25, 60, 400, 1200, 20,
                new[] { "Bueno", "Excesivo" }, secas, new[] { 2, 3, 4, 5, 9, 10 }, true);
            Agregar(new[] { "Jengibre", "Cúrcuma" }, new[] { "Franco", "Franco arenoso", "Limoso" }, 5.5m, 7, 55, 80, 1200, 2500, 20,
                new[] { "Bueno", "Moderado" }, humedas, new[] { 3, 4, 5, 9, 10 });

            return reglas.ToDictionary(r => Normalizar(r.Nombre), r => r);
        }

        private static bool Coincide(string valorA, string valorB) => Normalizar(valorA) == Normalizar(valorB);
        private static string Normalizar(string valor) => ZonificacionAgroclimaticaDominicana.Normalizar(valor);
    }
}

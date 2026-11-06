namespace Sistema_Agricola.Pr
{
    public static class CalendarioAgricola
    {
        public static string ObtenerTemporadaActual() =>
            ObtenerPeriodoClimatico(DateTime.Today, ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste);

        public static string ObtenerPeriodoClimatico(DateTime fecha, string zonaAgroclimatica)
        {
            int mes = fecha.Month;
            string zona = ZonificacionAgroclimaticaDominicana.Normalizar(zonaAgroclimatica);

            if (zona.Contains("norte") || zona.Contains("nordeste"))
            {
                if (mes is 11 or 12 or 1 or 2)
                    return "Período lluvioso";
                if (mes is 6 or 7 or 8)
                    return "Período seco";
                return "Transición";
            }

            if (zona.Contains("semiarida"))
            {
                if (mes is 5 or 9 or 10)
                    return "Transición";
                return "Período seco";
            }

            if (mes is 5 or 9 or 10)
                return "Período lluvioso";
            if (mes is 1 or 2 or 3)
                return "Período seco";
            return "Transición";
        }

        public static List<string> RecomendacionesPorMes(int mes)
        {
            DateTime fecha = new(DateTime.Today.Year, Math.Clamp(mes, 1, 12), 1);
            CondicionesAgroclimaticas condiciones = new()
            {
                FechaSiembra = fecha,
                ZonaAgroclimatica = ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste,
                DisponibilidadRiego = true
            };

            return RecomendadorAgricola.CultivosSoportados
                .Select(c => new { Cultivo = c, Evaluacion = RecomendadorAgricola.Evaluar(c, condiciones) })
                .Where(x => x.Evaluacion.TemporadaCompatible)
                .OrderByDescending(x => x.Evaluacion.Puntuacion)
                .ThenBy(x => x.Cultivo)
                .Select(x => x.Cultivo)
                .ToList();
        }

        public static (string MejorMesSiembra, string MejorMesCosecha, int DuracionAproxMeses)
            ObtenerCicloParaCultivo(string nombreCultivo)
        {
            return ZonificacionAgroclimaticaDominicana.Normalizar(nombreCultivo) switch
            {
                "arroz" => ("Marzo-Mayo o Septiembre-Octubre", "Julio-Agosto o Enero-Febrero", 4),
                "maiz" => ("Marzo-Mayo o Septiembre-Noviembre", "Junio-Agosto o Diciembre-Febrero", 3),
                "habichuela" or "frijol" => ("Marzo-Abril o Septiembre-Noviembre", "Junio-Julio o Diciembre-Febrero", 3),
                "guandules" => ("Mayo-Junio u Octubre-Noviembre", "Diciembre-Marzo", 6),
                "yuca" => ("Marzo-Junio o Septiembre-Octubre", "9-12 meses después", 10),
                "batata" => ("Abril-Junio o Septiembre-Octubre", "4-6 meses después", 5),
                "name" => ("Marzo-Mayo", "8-10 meses después", 9),
                "platano" or "guineo" => ("Preferible al inicio de lluvias", "12-18 meses después", 15),
                "cafe" => ("Abril-Mayo o Septiembre-Octubre", "Según edad y variedad", 36),
                "cacao" or "cacao organico" => ("Abril-Mayo o Septiembre-Noviembre", "Según edad y variedad", 30),
                "cana de azucar" => ("Abril-Julio o Noviembre-Enero", "12-18 meses después", 15),
                "tomate" or "aji" or "berenjena" => ("Febrero-Abril o Septiembre-Noviembre", "3-5 meses después", 4),
                "cebolla" or "zanahoria" or "repollo" or "lechuga" => ("Octubre-Febrero", "2-5 meses después", 4),
                _ => ("Depende de zona, variedad y disponibilidad de agua", "Depende de variedad y manejo", 0)
            };
        }

        public static bool EsBuenaTemporadaSiembra(string nombreCultivo) =>
            EsBuenaTemporadaSiembra(nombreCultivo, DateTime.Today);

        public static bool EsBuenaTemporadaSiembra(string nombreCultivo, DateTime fechaSiembra)
        {
            EvaluacionAgricola evaluacion = RecomendadorAgricola.Evaluar(nombreCultivo,
                new CondicionesAgroclimaticas
                {
                    FechaSiembra = fechaSiembra,
                    ZonaAgroclimatica = ZonificacionAgroclimaticaDominicana.SubhumedaCibaoEste,
                    DisponibilidadRiego = true
                });
            return evaluacion.TemporadaCompatible;
        }
    }
}

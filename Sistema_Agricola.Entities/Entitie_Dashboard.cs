using System;
namespace Sistema_Agricola.Entities
{
    public class Entitie_Dashboard
    {
        // Propiedades para resumen de cultivos
        public int TotalCultivosActivos { get; set; }
        public int TotalCosechasProximas { get; set; }
        public decimal CantidadTotalSembrada { get; set; }

        // Propiedades para estadísticas financieras
        public decimal CostoTotalSiembras { get; set; }
        public decimal IngresosPotenciales { get; set; }
        public decimal GananciaPotencial { get; set; }

        // Propiedades para tracking de seguimiento
        public int TotalSeguimientosRegistrados { get; set; }
        public DateTime UltimoSeguimiento { get; set; }

        // Propiedades para distribución de cultivos
        public Dictionary<string, int> CultivosPorTipo { get; set; }
        public Dictionary<string, int> CultivosPorCategoria { get; set; }

        // Propiedades para alertas
        public int AlertasCrecimientoBajo { get; set; }
        public int AlertasPlagasActivas { get; set; }
        public int AlertasInsumosBajos { get; set; }

        // Constructor
        public Entitie_Dashboard()
        {
            CultivosPorTipo = new Dictionary<string, int>();
            CultivosPorCategoria = new Dictionary<string, int>();
            UltimoSeguimiento = DateTime.MinValue;
        }
    }
}

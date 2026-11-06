using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Produccion
    {
        public int ID_Produccion { get; set; }
        public int ID_Cosecha { get; set; }
        public DateTime Fecha_Produccion { get; set; } = DateTime.Today;
        public string Unidad_Medida { get; set; } = string.Empty;
        public decimal Cantidad_Bruta { get; set; }
        public decimal Merma { get; set; }
        public decimal Cantidad_Neta => Cantidad_Bruta - Merma;
        public decimal Precio_Unitario { get; set; }
        public decimal Ingreso_Estimado => Cantidad_Neta * Precio_Unitario;
        public decimal Costo_Base_Asignado { get; set; }
        public decimal Costo_Postcosecha { get; set; }
        public decimal Beneficio_Neto =>
            Ingreso_Estimado - Costo_Base_Asignado - Costo_Postcosecha;
        public string Destino { get; set; } = string.Empty;
        public string Estado { get; set; } = "Registrada";
        public string Observaciones { get; set; } = string.Empty;
        public DateTime? Fecha_Venta { get; set; }
        public string Comprador { get; set; } = string.Empty;
        public string Metodo_Pago { get; set; } = string.Empty;
        public string Estado_Cobro { get; set; } = "Pendiente";
        public string Referencia_Venta { get; set; } = string.Empty;
    }

    public class Entitie_ResumenProduccion
    {
        public int Lotes { get; set; }
        public decimal CantidadBruta { get; set; }
        public decimal Merma { get; set; }
        public decimal CantidadNeta { get; set; }
        public decimal ValorProduccion { get; set; }
        public decimal Costos { get; set; }
        public decimal Beneficio { get; set; }
    }
}

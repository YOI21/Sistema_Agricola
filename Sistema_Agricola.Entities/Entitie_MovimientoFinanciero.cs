using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_MovimientoFinanciero
    {
        public int ID_Movimiento { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Metodo_Pago { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
    }

    public class Entitie_ResumenFinanciero
    {
        public decimal Ingresos { get; set; }
        public decimal Gastos { get; set; }
        public decimal Balance => Ingresos - Gastos;
        public decimal PendienteCobro { get; set; }
        public decimal PendientePago { get; set; }
    }
}

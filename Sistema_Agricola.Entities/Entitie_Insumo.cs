using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Insumo
    {
        public int Id_Insumo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad_Medida { get; set; } = string.Empty;
        public decimal Stock_Minimo { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime? Fecha_Vencimiento { get; set; }
        public string Estado { get; set; } = "Activo";

        // Constructor
        public Entitie_Insumo()
        {
            Fecha = DateTime.Now;
        }
    }

    public class Entitie_MovimientoInventario
    {
        public int ID_Movimiento { get; set; }
        public int Id_Insumo { get; set; }
        public string Tipo_Movimiento { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal Costo_Unitario { get; set; }
        public DateTime Fecha_Movimiento { get; set; } = DateTime.Today;
        public string Motivo { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
    }

    public class Entitie_ResumenInventario
    {
        public int ProductosActivos { get; set; }
        public int BajoStock { get; set; }
        public int PorVencer { get; set; }
        public decimal ValorInventario { get; set; }
    }
}

using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Equipo
    {
        public int Id_Equipo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public DateTime? Fecha_Adquisicion { get; set; }
        public DateTime? Proximo_Mantenimiento { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";
        public string Observaciones { get; set; } = string.Empty;
    }

    public class Entitie_MantenimientoEquipo
    {
        public int Id_Mantenimiento { get; set; }
        public int Id_Equipo { get; set; }
        public string Tipo_Mantenimiento { get; set; } = string.Empty;
        public DateTime Fecha_Mantenimiento { get; set; } = DateTime.Today;
        public int Cantidad_Atendida { get; set; } = 1;
        public decimal Costo { get; set; }
        public DateTime? Proximo_Mantenimiento { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class Entitie_ResumenEquipos
    {
        public int TotalActivos { get; set; }
        public int Unidades { get; set; }
        public int MantenimientoProximo { get; set; }
        public decimal ValorTotal { get; set; }
    }
}

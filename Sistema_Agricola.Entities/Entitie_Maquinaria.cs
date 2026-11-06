using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Maquinaria
    {
        public int Id_Maquina { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre_Maquina { get; set; } = string.Empty;
        public string Tipo_Maquina { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Numero_Serie { get; set; } = string.Empty;
        public decimal Costo_Maquina { get; set; }
        public DateTime? Fecha_Adquisicion { get; set; }
        public string Mantenimiento_Programado { get; set; } = string.Empty;
        public DateTime? Proximo_Mantenimiento { get; set; }
        public decimal Horometro { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Disponible";
        public string Observaciones { get; set; } = string.Empty;
    }

    public class Entitie_MantenimientoMaquinaria
    {
        public int ID_Mantenimiento { get; set; }
        public int ID_Maquina { get; set; }
        public string Tipo_Mantenimiento { get; set; } = string.Empty;
        public DateTime Fecha_Mantenimiento { get; set; } = DateTime.Today;
        public decimal Costo { get; set; }
        public decimal Horometro { get; set; }
        public DateTime? Proximo_Mantenimiento { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class Entitie_ResumenMaquinaria
    {
        public int TotalActivos { get; set; }
        public int Disponibles { get; set; }
        public int Asignadas { get; set; }
        public int MantenimientoProximo { get; set; }
        public decimal ValorActivos { get; set; }
    }
}

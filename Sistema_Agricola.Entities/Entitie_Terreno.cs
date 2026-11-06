using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema_Agricola.Entities
{
    public class Entitie_Terreno
    {
        public int ID_Terreno { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Area { get; set; }
        public string UnidadMedida { get; set; } = string.Empty; // hectáreas, metros cuadrados, etc.
        public string TipoSuelo { get; set; } = string.Empty;
        public decimal pH { get; set; }
        public decimal Pendiente { get; set; } // en porcentaje
        public decimal Humedad { get; set; } // en porcentaje
        public string Drenaje { get; set; } = string.Empty;
        public string FuenteAgua { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string ZonaAgroclimatica { get; set; } = string.Empty;
        public decimal PrecipitacionAnual { get; set; } // milímetros por año
        public bool DisponibilidadRiego { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string EstadoActual { get; set; } = string.Empty; // Disponible, En uso, En descanso
        public string Observaciones { get; set; } = string.Empty;

        // Constructor
        public Entitie_Terreno()
        {
            FechaRegistro = DateTime.Now;
            EstadoActual = "Disponible";
            Nombre = string.Empty;
            UnidadMedida = string.Empty;
            TipoSuelo = string.Empty;
            Ubicacion = string.Empty;
            Propietario = string.Empty;
            Observaciones = string.Empty;
        }
    }
}

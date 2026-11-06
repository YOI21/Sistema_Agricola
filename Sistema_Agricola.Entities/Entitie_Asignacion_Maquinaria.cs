using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Asignacion_Maquinaria
    {
        public int ID_Asignacion { get; set; }
        public int ID_Trabajador { get; set; }
        public int ID_Maquina { get; set; }
        public DateTime Fecha_Asignacion { get; set; }
        public DateTime? Fecha_Prevista_Devolucion { get; set; }
        public DateTime? Fecha_Devolucion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Actividad { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;

        // Propiedades adicionales para la interfaz de usuario
        public string Nombre_Trabajador { get; set; } = string.Empty;
        public string Posicion_Trabajador { get; set; } = string.Empty;
        public string Nombre_Maquina { get; set; } = string.Empty;
        public string Tipo_Maquinaria { get; set; } = string.Empty;
    }
}

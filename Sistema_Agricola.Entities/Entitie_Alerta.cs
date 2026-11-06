using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Alerta
    {
        public int ID_Alerta { get; set; }
        public int ID_Cultivo { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha_Programada { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Tipo_Alerta { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public bool Notificacion_Enviada { get; set; }
        public int Dias_Anticipacion { get; set; }

        // Propiedades adicionales de navegación
        public string Nombre_Cultivo { get; set; } = string.Empty;
        public int Dias_Restantes { get; set; }

        // Constructor por defecto
        public Entitie_Alerta()
        {
            Fecha_Creacion = DateTime.Now;
            Estado = "Pendiente";
            Prioridad = "Media";
            Notificacion_Enviada = false;
            Dias_Anticipacion = 7;
        }
    }
}

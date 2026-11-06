using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Data;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Siembra
    {
        public int ID_Cultivo { get; set; }
        public string Nombre_Cultivo { get; set; } = string.Empty;
        public string Variedad_Cultivo { get; set; } = string.Empty;
        public string Tipo_Cultivo { get; set; } = string.Empty;
        public string Categoria_Cultivo { get; set; } = string.Empty;
        public string Estacion_Siembra { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Tipo_Suelo { get; set; } = string.Empty;
        public DateTime Fecha_Plantacion { get; set; }
        public DateTime Fecha_Cosecha { get; set; }

        // Propiedades adicionales para los datos de fertilización
        public string Tipo_Fertilizante { get; set; } = string.Empty;
        public string Metodo_Fertilizacion { get; set; } = string.Empty;
        public int Cantidad_Fertilizante { get; set; }
        public decimal Costo_Fertilizacion { get; set; }
        public DateTime Fecha_Aplicacion { get; set; }

        // Propiedades adicionales para la siembra
        public string Metodo_Siembra { get; set; } = string.Empty;
        public string Profundidad { get; set; } = string.Empty;
        public decimal Humedad_Suelo { get; set; }
        public decimal Costo_Siembra { get; set; }
        public int ID_Terreno { get; set; }
        public string Nombre_Terreno { get; set; } = string.Empty;
        public decimal Hectareas_Sembradas { get; set; }

        // Propiedad para el estado del cultivo (En seguimiento, Terminado, Cosechado)
        public string Estado_Cultivo { get; set; } = string.Empty;

    }
}

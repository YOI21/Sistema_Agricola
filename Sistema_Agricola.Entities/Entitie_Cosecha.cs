using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Cosecha
    {
        public int ID_Cosecha { get; set; }
        public string Estado_Cosecha { get; set; } = string.Empty;
        public int ID_Cultivo { get; set; }

        // Propiedades adicionales para la información de la cosecha
        public decimal Cantidad_Cosechada { get; set; }
        public decimal Rendimiento { get; set; }  // Rendimiento calculado (cantidad cosechada / cantidad sembrada)
        public decimal Calidad { get; set; }      // Calificación de calidad (1-10)
        public string Observaciones { get; set; } = string.Empty;
        public DateTime Fecha_Cosecha { get; set; }
        public decimal Costo_Cosecha { get; set; }
        public decimal Precio_Venta { get; set; }
        public decimal Beneficio { get; set; }    // Precio de venta - costo total
        public decimal Costo_Siembra { get; set; }

        // Referencias a los datos del cultivo (para mostrar en la interfaz)
        public string Nombre_Cultivo { get; set; } = string.Empty;
        public string Tipo_Cultivo { get; set; } = string.Empty;
        public string Categoria_Cultivo { get; set; } = string.Empty;
        public int Cantidad_Sembrada { get; set; }
        public DateTime Fecha_Plantacion { get; set; }
    }
}   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.Entities
{
    public class Entitie_SeguimientoCrecimiento
    {
        public int ID_Seguimiento { get; set; }
        public int ID_Cultivo { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public string Etapa_Crecimiento { get; set; } = string.Empty;  // Germinación, Desarrollo vegetativo, Floración, Fructificación, etc.
        public decimal Altura_Planta { get; set; }     // En centímetros
        public int Num_Hojas { get; set; }             // Número de hojas desarrolladas
        public string Estado_Salud { get; set; } = string.Empty;       // Excelente, Bueno, Regular, Malo
        public string Observaciones { get; set; } = string.Empty;
        public decimal Porcentaje_Crecimiento { get; set; } // Porcentaje estimado del crecimiento total
        public string Problemas_Detectados { get; set; } = string.Empty;
        public string Acciones_Tomadas { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.Entities
{
    public class Entitie_RP
    {
        public int ID_RP;
        public string? Nombre_Cultivo { get; set; }
        public string? Tipo_Cultivo { get; set; }

        public string? Categoria_Cultivo { get; set; }
        public string? Estacion_Siembra { get; set; }
        public string? Tipo_Suelo {get; set;}
        public string? Duracion   {get; set;}
    }
}

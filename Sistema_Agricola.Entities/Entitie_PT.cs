using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.Entities
{

    //FI = Fecha inicio
    // TL = Tipo Labranza
    //FF = Fecha Fin
    //
    public class Entitie_PT
    {
        public int ID { get; set; }
        public DateTime FI { get; set; }
        public DateTime FF { get; set; }
        public string? TL { get; set; }
        public string ? MaquinariaU { get; set; }
        public int costoP { get; set; }
        public string? TS { get; set; }
        public string? CS { get; set;}
    }
}

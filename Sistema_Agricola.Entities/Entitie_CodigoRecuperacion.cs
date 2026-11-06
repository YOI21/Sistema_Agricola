using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_CodigoRecuperacion
    {
        public int ID_Recuperacion { get; set; }
        public int ID_Usuario { get; set; }
        public byte[] Codigo_Hash { get; set; } = Array.Empty<byte>();
        public string Codigo_Salt { get; set; } = string.Empty;
        public DateTime Fecha_Expiracion { get; set; }
        public bool Usado { get; set; }
        public int Intentos { get; set; }
    }
}

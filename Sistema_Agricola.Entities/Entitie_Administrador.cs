using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Administrador
    {
        public int ID_Administrador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;
        public int ID_Usuario { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}

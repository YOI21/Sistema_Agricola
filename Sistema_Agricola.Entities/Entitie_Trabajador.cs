using System;

namespace Sistema_Agricola.Entities
{
    public class Entitie_Trabajador
    {
        public int ID_Trabajador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Fecha_Nac { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Posicion { get; set; } = string.Empty;
        public decimal Sueldo { get; set; }
        public DateTime Fecha_Ingreso { get; set; }

        // Propiedades adicionales para la UI
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }

        public int Edad
        {
            get
            {
                int edad = DateTime.Today.Year - Fecha_Nac.Year;
                if (Fecha_Nac.Date > DateTime.Today.AddYears(-edad))
                    edad--;
                return edad;
            }
        }

        public int TiempoServicio
        {
            get
            {
                int anios = DateTime.Today.Year - Fecha_Ingreso.Year;
                if (Fecha_Ingreso.Date > DateTime.Today.AddYears(-anios))
                    anios--;
                return anios;
            }
        }
    }
}

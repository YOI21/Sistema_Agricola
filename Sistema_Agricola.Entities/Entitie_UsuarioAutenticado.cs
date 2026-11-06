namespace Sistema_Agricola.Entities
{
    public class Entitie_UsuarioAutenticado
    {
        public int ID { get; set; }
        public int ID_Usuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsAgricultor { get; set; }
        public string Rol { get; set; } = string.Empty;
        public bool DebeCambiarContrasena { get; set; }
    }

    public class Entitie_ResultadoAutenticacion
    {
        public bool Exitoso { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public int IntentosRestantes { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public Entitie_UsuarioAutenticado? Usuario { get; set; }
    }
}

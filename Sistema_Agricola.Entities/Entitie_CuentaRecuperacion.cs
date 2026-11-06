namespace Sistema_Agricola.Entities
{
    public class Entitie_CuentaRecuperacion
    {
        public int ID_Usuario { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
    }
}

using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Login
    {
        private static int idUsuarioPendienteCambio;
        public static bool ExistenUsuariosRegistrados()
        {
            DAL_Login dal = new DAL_Login();
            return dal.ExistenUsuariosRegistrados();
        }

        public static bool ExisteAdministradorRegistrado()
        {
            DAL_Login dal = new DAL_Login();
            return dal.ExisteAdministradorRegistrado();
        }

        public static Entitie_ResultadoAutenticacion Autenticar(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                return new Entitie_ResultadoAutenticacion
                {
                    Mensaje = "Ingrese su usuario y contraseña."
                };
            }

            DAL_Login dal = new DAL_Login();
            Entitie_ResultadoAutenticacion resultado = dal.Autenticar(usuario.Trim(), contrasena);
            idUsuarioPendienteCambio = resultado.Exitoso &&
                                       resultado.Usuario?.DebeCambiarContrasena == true
                ? resultado.Usuario.ID_Usuario
                : 0;
            return resultado;
        }

        public static void CambiarContrasenaInicial(
            int idUsuario,
            string contrasenaActual,
            string nuevaContrasena,
            string confirmacion)
        {
            if (idUsuario <= 0 || idUsuario != idUsuarioPendienteCambio)
            {
                throw new UnauthorizedAccessException(
                    "No existe un cambio inicial de contraseña autorizado.");
            }

            BLL_PoliticaContrasena.ValidarConfirmacion(nuevaContrasena, confirmacion);
            if (string.Equals(contrasenaActual, nuevaContrasena, StringComparison.Ordinal))
            {
                throw new ArgumentException(
                    "La nueva contraseña debe ser diferente de la contraseña temporal.");
            }

            new DAL_Login().CambiarContrasena(idUsuario, nuevaContrasena);
            idUsuarioPendienteCambio = 0;
        }
    }
}

using System;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.UI
{
    public static class Session
    {
        private static User? currentUser;

        public class User
        {
            public int ID { get; set; }
            public int IDUsuario { get; set; }
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public bool IsAgricultor { get; set; }
            public string Rol { get; set; } = string.Empty;
        }

        public static User? CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                if (currentUser == null)
                {
                    BLL_Seguridad.CerrarSesion();
                    return;
                }

                BLL_Seguridad.IniciarSesion(
                    currentUser.IDUsuario > 0 ? currentUser.IDUsuario : currentUser.ID,
                    currentUser.Username,
                    currentUser.FullName,
                    currentUser.Rol);
            }
        }

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool IsAgricultor()
        {
            return CurrentUser != null &&
                   (CurrentUser.IsAgricultor ||
                    string.Equals(CurrentUser.Rol, "Agricultor", StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsAdministrador()
        {
            return CurrentUser != null &&
                   string.Equals(CurrentUser.Rol, "Administrador", StringComparison.OrdinalIgnoreCase);
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}

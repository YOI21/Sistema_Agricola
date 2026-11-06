using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Administrador
    {
        public static bool ExisteAdministradorRegistrado()
        {
            DAL_Administrador dal = new DAL_Administrador();
            return dal.ExisteAdministradorRegistrado();
        }

        public static bool ExisteUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return false;
            }

            DAL_Administrador dal = new DAL_Administrador();
            return dal.ExisteUsuario(usuario.Trim());
        }

        public static int RegistrarAdministrador(
            string nombre,
            string apellido,
            string email,
            string telefono,
            string usuario,
            string contrasena,
            string confirmarContrasena)
        {
            Entitie_Administrador administrador = new Entitie_Administrador
            {
                Nombre = (nombre ?? string.Empty).Trim(),
                Apellido = (apellido ?? string.Empty).Trim(),
                Email = (email ?? string.Empty).Trim(),
                Telefono = (telefono ?? string.Empty).Trim(),
                Usuario = (usuario ?? string.Empty).Trim(),
                Contrasena = contrasena ?? string.Empty
            };

            DAL_Administrador dal = new DAL_Administrador();
            const string permisoInicial = "configuracion.inicial.administrador";
            string detalle = $"Usuario={administrador.Usuario}; Email={administrador.Email}";

            try
            {
                if (dal.ExisteAdministradorRegistrado())
                {
                    throw new InvalidOperationException(
                        "Ya existe un administrador. El sistema admite un unico administrador principal.");
                }

                ValidarAdministrador(administrador, confirmarContrasena ?? string.Empty);

                if (dal.ExisteUsuario(administrador.Usuario))
                {
                    throw new InvalidOperationException("Ya existe un usuario con ese nombre de usuario.");
                }

                if (dal.ExisteEmailAdministrador(administrador.Email))
                {
                    throw new InvalidOperationException("Ya existe un administrador registrado con ese correo.");
                }

                int idAdministrador = dal.InsertarAdministrador(administrador);
                BLL_Seguridad.Auditar(
                    permisoInicial,
                    "Administrador",
                    "ConfiguracionInicial",
                    detalle + $"; ID={idAdministrador}",
                    ResultadosAuditoria.Exito);
                return idAdministrador;
            }
            catch (Exception ex)
            {
                BLL_Seguridad.Auditar(
                    permisoInicial,
                    "Administrador",
                    "ConfiguracionInicial",
                    detalle + "; Error=" + ex.Message,
                    ResultadosAuditoria.Error);
                throw;
            }
        }

        public static string GenerarUsuarioDisponible(string nombre, string apellido)
        {
            string baseUsuario = CrearBaseUsuario(nombre, apellido);
            if (string.IsNullOrWhiteSpace(baseUsuario))
            {
                return string.Empty;
            }

            DAL_Administrador dal = new DAL_Administrador();

            if (!dal.ExisteUsuario(baseUsuario))
            {
                return baseUsuario;
            }

            for (int i = 2; i <= 999; i++)
            {
                string candidato = baseUsuario + i;
                if (!dal.ExisteUsuario(candidato))
                {
                    return candidato;
                }
            }

            return string.Empty;
        }

        public static string GenerarContrasenaTemporal()
        {
            return BLL_PoliticaContrasena.GenerarTemporal();
        }

        private static void ValidarAdministrador(Entitie_Administrador administrador, string confirmarContrasena)
        {
            if (string.IsNullOrWhiteSpace(administrador.Nombre))
            {
                throw new ArgumentException("Ingrese el nombre del administrador.");
            }

            if (string.IsNullOrWhiteSpace(administrador.Apellido))
            {
                throw new ArgumentException("Ingrese el apellido del administrador.");
            }

            if (!EsCorreoValido(administrador.Email))
            {
                throw new ArgumentException("Ingrese un correo valido para el administrador.");
            }

            if (string.IsNullOrWhiteSpace(administrador.Telefono))
            {
                throw new ArgumentException("Ingrese el telefono del administrador.");
            }

            if (string.IsNullOrWhiteSpace(administrador.Usuario))
            {
                throw new ArgumentException("Ingrese el usuario del administrador.");
            }

            if (administrador.Usuario.Length > 50)
            {
                throw new ArgumentException("El usuario no puede tener mas de 50 caracteres.");
            }

            BLL_PoliticaContrasena.ValidarConfirmacion(
                administrador.Contrasena,
                confirmarContrasena);
        }

        private static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return false;
            }

            try
            {
                MailAddress direccion = new MailAddress(correo);
                return direccion.Address.Equals(correo.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static string CrearBaseUsuario(string nombre, string apellido)
        {
            string nombreLimpio = NormalizarTexto(nombre);
            string apellidoLimpio = NormalizarTexto(apellido);

            if (string.IsNullOrWhiteSpace(nombreLimpio) || string.IsNullOrWhiteSpace(apellidoLimpio))
            {
                return string.Empty;
            }

            string baseUsuario = nombreLimpio.Substring(0, 1) + apellidoLimpio;
            return baseUsuario.Length > 45 ? baseUsuario.Substring(0, 45) : baseUsuario;
        }

        private static string NormalizarTexto(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return string.Empty;
            }

            string normalizado = valor.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char caracter in normalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark &&
                    char.IsLetterOrDigit(caracter))
                {
                    sb.Append(caracter);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

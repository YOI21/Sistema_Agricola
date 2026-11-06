using System;
using System.Text;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_RegistroAgricultor
    {
        public static bool RegistrarAgricultor(Entitie_RegistroAgricultor agricultor)
        {
            if (agricultor == null)
            {
                throw new ArgumentNullException(nameof(agricultor));
            }

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AgricultoresCrear,
                "Agricultor",
                "Crear",
                $"Cedula={agricultor.Cedula}; Usuario={agricultor.Usuario}",
                () =>
                {
                    BLL_PoliticaContrasena.Validar(agricultor.Contrasena);
                    return new DAL_RegistroAgricultor().RegistrarAgricultor(agricultor);
                },
                resultado => resultado);
        }

        public static bool ExisteUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return false;
            }

            DAL_RegistroAgricultor dal = new DAL_RegistroAgricultor();
            return dal.ExisteUsuario(usuario.Trim());
        }

        public static bool ExisteCedulaAgricultor(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return false;
            }

            DAL_RegistroAgricultor dal = new DAL_RegistroAgricultor();
            return dal.ExisteCedulaAgricultor(cedula.Trim());
        }

        public static Entitie_DatosCedula? ObtenerDatosCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula) || cedula.Trim().Length != 11)
            {
                return null;
            }

            DAL_RegistroAgricultor dal = new DAL_RegistroAgricultor();
            return dal.ObtenerDatosCedula(cedula.Trim());
        }

        public static string GenerarUsuarioDisponible(string nombre, string apellido)
        {
            string baseUsuario = CrearUsuarioBase(nombre, apellido);

            if (string.IsNullOrWhiteSpace(baseUsuario))
            {
                return string.Empty;
            }

            if (!ExisteUsuario(baseUsuario))
            {
                return baseUsuario;
            }

            for (int i = 2; i <= 999; i++)
            {
                string candidato = AjustarLongitudUsuario(baseUsuario, i);
                if (!ExisteUsuario(candidato))
                {
                    return candidato;
                }
            }

            return AjustarLongitudUsuario(baseUsuario, DateTime.Now.Millisecond);
        }

        public static string GenerarContrasenaTemporal()
        {
            return BLL_PoliticaContrasena.GenerarTemporal();
        }

        public static bool CoincideNombreCedula(Entitie_DatosCedula datos, string nombre, string apellido)
        {
            string nombreFormulario = NormalizarTexto(nombre);
            string apellidoFormulario = NormalizarTexto(apellido);
            string nombreCedula = NormalizarTexto(datos.Nombre);
            string apellidoCedula = NormalizarTexto(datos.Apellido);

            return nombreFormulario == nombreCedula && apellidoFormulario == apellidoCedula;
        }

        private static string CrearUsuarioBase(string nombre, string apellido)
        {
            string nombreLimpio = NormalizarParaUsuario(nombre);
            string apellidoLimpio = NormalizarParaUsuario(apellido);

            if (string.IsNullOrWhiteSpace(nombreLimpio) || string.IsNullOrWhiteSpace(apellidoLimpio))
            {
                return string.Empty;
            }

            string usuario = nombreLimpio.Substring(0, 1) + apellidoLimpio;
            return usuario.Length <= 50 ? usuario : usuario.Substring(0, 50);
        }

        private static string AjustarLongitudUsuario(string baseUsuario, int numero)
        {
            string sufijo = numero.ToString();
            int longitudBase = Math.Max(1, 50 - sufijo.Length);
            string prefijo = baseUsuario.Length > longitudBase ? baseUsuario.Substring(0, longitudBase) : baseUsuario;
            return prefijo + sufijo;
        }

        private static string NormalizarParaUsuario(string valor)
        {
            string normalizado = QuitarAcentos(valor ?? string.Empty).ToLowerInvariant();
            StringBuilder resultado = new StringBuilder();

            foreach (char caracter in normalizado)
            {
                if (char.IsLetterOrDigit(caracter))
                {
                    resultado.Append(caracter);
                }
            }

            return resultado.ToString();
        }

        private static string NormalizarTexto(string valor)
        {
            string sinAcentos = QuitarAcentos(valor ?? string.Empty).ToUpperInvariant();
            return string.Join(" ", sinAcentos.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        private static string QuitarAcentos(string texto)
        {
            string normalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder();

            foreach (char caracter in normalizado)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(caracter) !=
                    System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    resultado.Append(caracter);
                }
            }

            return resultado.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

using System;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Sistema_Agricola.BLL
{
    public class ResultadoSolicitudVerificacionCorreo
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public DateTime Expira { get; set; }
        public string CorreoNormalizado { get; set; } = string.Empty;
    }

    public class BLL_VerificacionCorreoAgricultor
    {
        private const int MinutosVigenciaCodigo = 10;

        public static ResultadoSolicitudVerificacionCorreo SolicitarCodigo(string correo, string nombreAgricultor)
        {
            return SolicitarCodigoCuenta(correo, nombreAgricultor, "agricultor");
        }

        public static ResultadoSolicitudVerificacionCorreo SolicitarCodigoAdministrador(
            string correo,
            string nombreAdministrador)
        {
            return SolicitarCodigoCuenta(correo, nombreAdministrador, "administrador");
        }

        private static ResultadoSolicitudVerificacionCorreo SolicitarCodigoCuenta(
            string correo,
            string nombreDestinatario,
            string tipoCuenta)
        {
            string correoNormalizado = NormalizarCorreo(correo);
            string? errorCorreo = ValidarCorreoGoogle(correoNormalizado, tipoCuenta);

            if (!string.IsNullOrEmpty(errorCorreo))
            {
                return CrearResultado(false, errorCorreo);
            }

            try
            {
                string codigo = GenerarCodigo();
                DateTime expira = DateTime.Now.AddMinutes(MinutosVigenciaCodigo);

                ServicioCorreoRecuperacion.EnviarCodigoVerificacion(
                    correoNormalizado,
                    nombreDestinatario.Trim(),
                    codigo,
                    expira,
                    tipoCuenta);

                return new ResultadoSolicitudVerificacionCorreo
                {
                    Exitoso = true,
                    Mensaje = $"Enviamos un codigo de verificacion a {MascararCorreo(correoNormalizado)}.",
                    Codigo = codigo,
                    Expira = expira,
                    CorreoNormalizado = correoNormalizado
                };
            }
            catch (InvalidOperationException ex)
            {
                return CrearResultado(false, ex.Message);
            }
            catch (SmtpException ex)
            {
                return CrearResultado(false, ServicioCorreoRecuperacion.ObtenerMensajeError(ex));
            }
            catch (Exception ex)
            {
                return CrearResultado(false, "Error al enviar el codigo de verificacion: " + ex.Message);
            }
        }

        public static ResultadoOperacionRecuperacion ValidarCodigo(
            string codigoIngresado,
            string codigoEsperado,
            DateTime expira)
        {
            if (string.IsNullOrWhiteSpace(codigoEsperado))
            {
                return CrearOperacion(false, "Primero envie un codigo de verificacion.");
            }

            if (DateTime.Now > expira)
            {
                return CrearOperacion(false, "El codigo vencio. Envie uno nuevo.");
            }

            if (string.IsNullOrWhiteSpace(codigoIngresado) || codigoIngresado.Trim().Length != 6)
            {
                return CrearOperacion(false, "Ingrese el codigo de 6 digitos recibido por correo.");
            }

            if (!string.Equals(codigoIngresado.Trim(), codigoEsperado, StringComparison.Ordinal))
            {
                return CrearOperacion(false, "El codigo ingresado no es correcto.");
            }

            return CrearOperacion(true, "Correo verificado correctamente.");
        }

        public static string NormalizarCorreo(string correo)
        {
            return (correo ?? string.Empty).Trim().ToLowerInvariant();
        }

        private static string? ValidarCorreoGoogle(string correo, string tipoCuenta)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return $"Ingrese el Gmail del {tipoCuenta}.";
            }

            try
            {
                MailAddress mail = new MailAddress(correo);
                string dominio = mail.Host.ToLowerInvariant();

                if (dominio != "gmail.com" && dominio != "googlemail.com")
                {
                    return "Debe ingresar una cuenta Gmail valida, por ejemplo usuario@gmail.com.";
                }

                return null;
            }
            catch
            {
                return "El formato del correo no es valido.";
            }
        }

        private static string GenerarCodigo()
        {
            return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        }

        private static string MascararCorreo(string correo)
        {
            int arroba = correo.IndexOf('@');
            if (arroba <= 1)
            {
                return "***" + correo.Substring(Math.Max(arroba, 0));
            }

            string usuario = correo.Substring(0, arroba);
            string dominio = correo.Substring(arroba);
            return usuario[0] + new string('*', Math.Max(usuario.Length - 2, 2)) + usuario[^1] + dominio;
        }

        private static ResultadoSolicitudVerificacionCorreo CrearResultado(bool exitoso, string mensaje)
        {
            return new ResultadoSolicitudVerificacionCorreo
            {
                Exitoso = exitoso,
                Mensaje = mensaje
            };
        }

        private static ResultadoOperacionRecuperacion CrearOperacion(bool exitoso, string mensaje)
        {
            return new ResultadoOperacionRecuperacion
            {
                Exitoso = exitoso,
                Mensaje = mensaje
            };
        }
    }
}

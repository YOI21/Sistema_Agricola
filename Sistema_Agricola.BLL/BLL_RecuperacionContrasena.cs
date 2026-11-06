using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using MailKit.Security;
using MimeKit;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;
using MailKitSmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Sistema_Agricola.BLL
{
    public class ResultadoSolicitudRecuperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int ID_Usuario { get; set; }
        public string EmailOculto { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }

    public class ResultadoOperacionRecuperacion
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }

    public class BLL_RecuperacionContrasena
    {
        private const int MinutosVigenciaCodigo = 10;
        private const int MaximoIntentosCodigo = 5;

        public static ResultadoSolicitudRecuperacion SolicitarCodigo(string usuarioOCorreo)
        {
            string identificador = usuarioOCorreo.Trim();
            if (string.IsNullOrWhiteSpace(identificador))
            {
                return CrearResultadoSolicitud(false, "Ingrese su usuario o correo registrado.");
            }

            try
            {
                DAL_RecuperacionContrasena dal = new DAL_RecuperacionContrasena();
                Entitie_CuentaRecuperacion? cuenta = dal.ObtenerCuentaPorUsuarioOCorreo(identificador);

                if (cuenta == null)
                {
                    return CrearResultadoSolicitud(false, "No se encontró una cuenta con ese usuario o correo.");
                }

                if (string.IsNullOrWhiteSpace(cuenta.Email) || !EsCorreoValido(cuenta.Email))
                {
                    return CrearResultadoSolicitud(false, "La cuenta encontrada no tiene un correo válido para recuperar la contraseña.");
                }

                string codigo = GenerarCodigo();
                string salt = GenerarSalt();
                byte[] codigoHash = CalcularHashCodigo(codigo, salt);
                DateTime expira = DateTime.Now.AddMinutes(MinutosVigenciaCodigo);

                dal.RegistrarCodigoRecuperacion(cuenta.ID_Usuario, codigoHash, salt, expira);
                ServicioCorreoRecuperacion.EnviarCodigo(cuenta.Email, cuenta.NombreCompleto, codigo, expira);

                return new ResultadoSolicitudRecuperacion
                {
                    Exitoso = true,
                    Mensaje = $"Enviamos un código a {MascararCorreo(cuenta.Email)}. El código vence en {MinutosVigenciaCodigo} minutos.",
                    ID_Usuario = cuenta.ID_Usuario,
                    EmailOculto = MascararCorreo(cuenta.Email),
                    Usuario = cuenta.Usuario
                };
            }
            catch (InvalidOperationException ex)
            {
                return CrearResultadoSolicitud(false, ex.Message);
            }
            catch (SmtpException ex)
            {
                return CrearResultadoSolicitud(false, ServicioCorreoRecuperacion.ObtenerMensajeError(ex));
            }
            catch (Exception ex)
            {
                return CrearResultadoSolicitud(false, "Error al solicitar el código: " + ex.Message);
            }
        }

        public static ResultadoOperacionRecuperacion RestablecerContrasena(
            int idUsuario,
            string codigoIngresado,
            string nuevaContrasena,
            string confirmarContrasena)
        {
            if (idUsuario <= 0)
            {
                return CrearResultadoOperacion(false, "Primero solicite un código de recuperación.");
            }

            if (string.IsNullOrWhiteSpace(codigoIngresado) || codigoIngresado.Trim().Length != 6)
            {
                return CrearResultadoOperacion(false, "Ingrese el código de 6 dígitos recibido por correo.");
            }

            string contrasena = nuevaContrasena ?? string.Empty;
            string confirmacion = confirmarContrasena ?? string.Empty;

            try
            {
                BLL_PoliticaContrasena.ValidarConfirmacion(contrasena, confirmacion);
            }
            catch (ArgumentException ex)
            {
                return CrearResultadoOperacion(false, ex.Message);
            }

            try
            {
                DAL_RecuperacionContrasena dal = new DAL_RecuperacionContrasena();
                Entitie_CodigoRecuperacion? codigoActivo = dal.ObtenerCodigoActivo(idUsuario);

                if (codigoActivo == null)
                {
                    return CrearResultadoOperacion(false, "No hay un código activo. Solicite uno nuevo.");
                }

                if (DateTime.Now > codigoActivo.Fecha_Expiracion)
                {
                    dal.MarcarCodigoUsado(codigoActivo.ID_Recuperacion);
                    return CrearResultadoOperacion(false, "El código venció. Solicite uno nuevo.");
                }

                if (codigoActivo.Intentos >= MaximoIntentosCodigo)
                {
                    dal.MarcarCodigoUsado(codigoActivo.ID_Recuperacion);
                    return CrearResultadoOperacion(false, "Se superó el límite de intentos. Solicite un código nuevo.");
                }

                byte[] hashIngresado = CalcularHashCodigo(codigoIngresado.Trim(), codigoActivo.Codigo_Salt);
                if (!CryptographicOperations.FixedTimeEquals(hashIngresado, codigoActivo.Codigo_Hash))
                {
                    dal.RegistrarIntentoFallido(codigoActivo.ID_Recuperacion);

                    int intentosRestantes = MaximoIntentosCodigo - (codigoActivo.Intentos + 1);
                    if (intentosRestantes <= 0)
                    {
                        dal.MarcarCodigoUsado(codigoActivo.ID_Recuperacion);
                        return CrearResultadoOperacion(false, "Código incorrecto. Se agotaron los intentos; solicite uno nuevo.");
                    }

                    return CrearResultadoOperacion(false, $"Código incorrecto. Intentos restantes: {intentosRestantes}.");
                }

                bool actualizado = dal.ActualizarContrasenaYMarcarCodigoUsado(
                    idUsuario,
                    contrasena,
                    codigoActivo.ID_Recuperacion);

                return actualizado
                    ? CrearResultadoOperacion(true, "Contraseña actualizada correctamente. Ya puede iniciar sesión.")
                    : CrearResultadoOperacion(false, "No se pudo actualizar la contraseña.");
            }
            catch (Exception ex)
            {
                return CrearResultadoOperacion(false, "Error al actualizar la contraseña: " + ex.Message);
            }
        }

        private static string GenerarCodigo()
        {
            return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        }

        private static string GenerarSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        private static byte[] CalcularHashCodigo(string codigo, string salt)
        {
            using SHA512 sha512 = SHA512.Create();
            byte[] datos = Encoding.UTF8.GetBytes($"{salt}:{codigo}");
            return sha512.ComputeHash(datos);
        }

        private static bool EsCorreoValido(string correo)
        {
            try
            {
                MailAddress _ = new MailAddress(correo);
                return true;
            }
            catch
            {
                return false;
            }
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

        private static ResultadoSolicitudRecuperacion CrearResultadoSolicitud(bool exitoso, string mensaje)
        {
            return new ResultadoSolicitudRecuperacion
            {
                Exitoso = exitoso,
                Mensaje = mensaje
            };
        }

        private static ResultadoOperacionRecuperacion CrearResultadoOperacion(bool exitoso, string mensaje)
        {
            return new ResultadoOperacionRecuperacion
            {
                Exitoso = exitoso,
                Mensaje = mensaje
            };
        }
    }

    internal static class ServicioCorreoRecuperacion
    {
        private const int TiempoEsperaSmtpMilisegundos = 20000;

        private readonly record struct IntentoSmtp(
            int Puerto,
            SecureSocketOptions Seguridad);

        public static void EnviarCodigo(string destinatario, string nombreDestinatario, string codigo, DateTime expira)
        {
            EnviarCorreoConCodigo(
                destinatario,
                nombreDestinatario,
                "Codigo de recuperacion - Sistema Agricola",
                CrearCuerpo(nombreDestinatario, codigo, expira));
        }

        public static void EnviarCodigoVerificacion(
            string destinatario,
            string nombreDestinatario,
            string codigo,
            DateTime expira,
            string tipoCuenta = "agricultor")
        {
            EnviarCorreoConCodigo(
                destinatario,
                nombreDestinatario,
                "Verificacion de correo - Sistema Agricola",
                CrearCuerpoVerificacion(nombreDestinatario, codigo, expira, tipoCuenta));
        }

        public static void EnviarAlerta(
            string destinatario,
            string nombreDestinatario,
            string titulo,
            string descripcion,
            string cultivo,
            DateTime fechaProgramada,
            string tipoAlerta,
            string prioridad,
            int diasRestantes)
        {
            string asunto = "Alerta agricola - " + (string.IsNullOrWhiteSpace(titulo) ? "Sistema Agricola" : titulo.Trim());
            if (asunto.Length > 150)
            {
                asunto = asunto.Substring(0, 150);
            }

            EnviarCorreoConCodigo(
                destinatario,
                nombreDestinatario,
                asunto,
                CrearCuerpoAlerta(nombreDestinatario, titulo, descripcion, cultivo, fechaProgramada, tipoAlerta, prioridad, diasRestantes));
        }

        public static void EnviarPrueba(
            string correoRemitente,
            string claveAplicacion,
            string nombreRemitente,
            string host,
            int puerto,
            bool ssl,
            string destinatario)
        {
            ValidarConfiguracion(
                correoRemitente,
                claveAplicacion,
                nombreRemitente,
                host,
                puerto,
                destinatario);

            EnviarCorreo(
                correoRemitente.Trim(),
                claveAplicacion.Replace(" ", string.Empty),
                string.IsNullOrWhiteSpace(nombreRemitente) ? "Sistema Agricola" : nombreRemitente.Trim(),
                host.Trim(),
                puerto,
                ssl,
                destinatario.Trim(),
                destinatario.Trim(),
                "Prueba de correo - Sistema Agricola",
                "Esta es una prueba de envio desde la configuracion del Sistema Agricola.\r\n\r\n" +
                $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
        }

        private static void EnviarCorreoConCodigo(
            string destinatario,
            string nombreDestinatario,
            string asunto,
            string cuerpo)
        {
            string correoRemitente = ObtenerVariableRequerida("SISTEMA_AGRICOLA_GMAIL");
            string claveAplicacion = ObtenerVariableRequerida("SISTEMA_AGRICOLA_GMAIL_APP_PASSWORD").Replace(" ", "");
            string host = ObtenerVariableOpcional("SISTEMA_AGRICOLA_SMTP_HOST", "smtp.gmail.com");
            int puerto = ObtenerPuerto();
            bool ssl = ObtenerSsl();
            string nombreRemitente = ObtenerVariableOpcional("SISTEMA_AGRICOLA_FROM_NAME", "Sistema Agricola");

            EnviarCorreo(
                correoRemitente,
                claveAplicacion,
                nombreRemitente,
                host,
                puerto,
                ssl,
                destinatario,
                nombreDestinatario,
                asunto,
                cuerpo);
        }

        private static void EnviarCorreo(
            string correoRemitente,
            string claveAplicacion,
            string nombreRemitente,
            string host,
            int puerto,
            bool ssl,
            string destinatario,
            string nombreDestinatario,
            string asunto,
            string cuerpo)
        {
            ValidarConfiguracion(
                correoRemitente,
                claveAplicacion,
                nombreRemitente,
                host,
                puerto,
                destinatario);

            List<IntentoSmtp> intentos = CrearIntentosSmtp(host, puerto, ssl);
            List<Exception> errores = new List<Exception>();

            foreach (IntentoSmtp intento in intentos)
            {
                try
                {
                    EnviarConMailKit(
                        correoRemitente,
                        claveAplicacion,
                        nombreRemitente,
                        host,
                        intento.Puerto,
                        intento.Seguridad,
                        destinatario,
                        nombreDestinatario,
                        asunto,
                        cuerpo);

                    return;
                }
                catch (Exception ex)
                {
                    Exception errorConContexto = new InvalidOperationException(
                        $"Fallo SMTP seguro en el puerto {intento.Puerto} ({intento.Seguridad}).",
                        ex);
                    errores.Add(errorConContexto);

                    if (EsErrorAutenticacion(ex))
                    {
                        throw new InvalidOperationException(ObtenerMensajeError(ex), ex);
                    }
                }
            }

            AggregateException errorFinal = new AggregateException(errores);
            throw new InvalidOperationException(ObtenerMensajeError(errorFinal), errorFinal);
        }

        private static List<IntentoSmtp> CrearIntentosSmtp(
            string host,
            int puerto,
            bool ssl)
        {
            List<IntentoSmtp> intentos = new List<IntentoSmtp>();
            AgregarIntento(intentos, puerto, ObtenerSeguridad(puerto, ssl));

            if (EsServidorGmail(host))
            {
                AgregarIntento(intentos, 587, SecureSocketOptions.StartTls);
                AgregarIntento(intentos, 465, SecureSocketOptions.SslOnConnect);
            }

            return intentos;
        }

        private static SecureSocketOptions ObtenerSeguridad(int puerto, bool ssl)
        {
            if (!ssl)
            {
                return SecureSocketOptions.None;
            }

            return puerto == 465
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTls;
        }

        private static void AgregarIntento(
            ICollection<IntentoSmtp> intentos,
            int puerto,
            SecureSocketOptions seguridad)
        {
            if (!intentos.Any(x => x.Puerto == puerto && x.Seguridad == seguridad))
            {
                intentos.Add(new IntentoSmtp(puerto, seguridad));
            }
        }

        private static void EnviarConMailKit(
            string correoRemitente,
            string claveAplicacion,
            string nombreRemitente,
            string host,
            int puerto,
            SecureSocketOptions seguridad,
            string destinatario,
            string nombreDestinatario,
            string asunto,
            string cuerpo)
        {
            MimeMessage mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(nombreRemitente, correoRemitente));
            mensaje.To.Add(new MailboxAddress(nombreDestinatario, destinatario));
            mensaje.Subject = asunto;
            mensaje.Body = new TextPart("plain") { Text = cuerpo };

            using MailKitSmtpClient smtp = new MailKitSmtpClient
            {
                Timeout = TiempoEsperaSmtpMilisegundos
            };

            smtp.Connect(host, puerto, seguridad);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(correoRemitente, claveAplicacion);
            smtp.Send(mensaje);

            try
            {
                smtp.Disconnect(true);
            }
            catch
            {
                // El servidor ya acepto el mensaje; un fallo al cerrar no invalida el envio.
            }
        }

        private static bool EsServidorGmail(string host)
        {
            return host.Equals("smtp.gmail.com", StringComparison.OrdinalIgnoreCase) ||
                   host.EndsWith(".gmail.com", StringComparison.OrdinalIgnoreCase);
        }

        private static bool EsErrorAutenticacion(Exception excepcion)
        {
            string detalle = ObtenerDetalleExcepcion(excepcion);
            return ContieneAlguno(
                detalle,
                "authentication required",
                "username and password not accepted",
                "authentication failed",
                "invalid credentials",
                "5.7.0",
                "5.7.8");
        }

        public static string ObtenerMensajeError(Exception excepcion)
        {
            string detalle = ObtenerDetalleExcepcion(excepcion);

            if (ContieneAlguno(
                    detalle,
                    "authentication required",
                    "username and password not accepted",
                    "authentication failed",
                    "invalid credentials",
                    "5.7.0",
                    "5.7.8"))
            {
                return "Gmail rechazo las credenciales del Sistema Agricola. Genere una nueva clave de aplicacion para el mismo Gmail remitente y guardela en Configuracion > Correo y notificaciones.";
            }

            if (ContieneAlguno(
                    detalle,
                    "timed out",
                    "tiempo de espera",
                    "unable to connect",
                    "no se puede establecer",
                    "connection refused",
                    "actively refused",
                    "forcibly closed",
                     "net_io_connectionclosed",
                     "unexpected eof",
                     "connection reset",
                     "transport connection",
                     "conexion de transporte",
                    "se cerro la conexion",
                    "se anulo una conexion establecida"))
            {
                return "No fue posible completar la conexion segura con Gmail. El sistema probo automaticamente los puertos 587 y 465. Revise el firewall, el antivirus o pruebe temporalmente con otra red.";
            }

            if (ContieneAlguno(
                    detalle,
                    "no such host is known",
                    "name or service not known",
                    "host desconocido",
                    "no se conoce el host"))
            {
                return "No fue posible localizar smtp.gmail.com. Revise la conexion a Internet y la configuracion DNS de la PC.";
            }

            if (ContieneAlguno(
                    detalle,
                    "certificate",
                    "certificado",
                    "ssl",
                    "tls"))
            {
                return "No se pudo establecer la conexion segura TLS con Gmail. Verifique la fecha y hora de Windows, las actualizaciones y que el antivirus no intercepte el correo SMTP.";
            }

            if (ContieneAlguno(detalle, "mailbox unavailable", "recipient", "destinatario"))
            {
                return "Gmail rechazo el correo destinatario. Revise que la direccion este escrita correctamente.";
            }

            if (ContieneAlguno(detalle, "failure sending mail", "error al enviar correo"))
            {
                return "El equipo no pudo completar el envio SMTP. Use la publicacion actualizada del Sistema Agricola, que incluye MailKit y prueba automaticamente los puertos seguros 587 y 465.";
            }

            return "Gmail no acepto el envio. Revise el remitente y confirme que la clave de aplicacion fue creada en esa misma cuenta de Google.";
        }

        private static void ValidarConfiguracion(
            string correoRemitente,
            string claveAplicacion,
            string nombreRemitente,
            string host,
            int puerto,
            string destinatario)
        {
            if (string.IsNullOrWhiteSpace(correoRemitente) ||
                string.IsNullOrWhiteSpace(claveAplicacion))
            {
                throw new InvalidOperationException(
                    "Configure el Gmail remitente y su clave de aplicacion para el Sistema Agricola.");
            }

            if (string.IsNullOrWhiteSpace(host) || puerto < 1 || puerto > 65535)
            {
                throw new InvalidOperationException("La configuracion del servidor SMTP no es valida.");
            }

            _ = new MailAddress(correoRemitente);
            _ = new MailAddress(destinatario);
        }

        private static string ObtenerDetalleExcepcion(Exception excepcion)
        {
            StringBuilder detalle = new StringBuilder();
            AgregarDetalleExcepcion(detalle, excepcion);

            return detalle.ToString();
        }

        private static void AgregarDetalleExcepcion(
            StringBuilder detalle,
            Exception excepcion)
        {
            detalle.Append(' ').Append(excepcion.Message);

            if (excepcion is AggregateException agregado)
            {
                foreach (Exception error in agregado.Flatten().InnerExceptions)
                {
                    AgregarDetalleExcepcion(detalle, error);
                }

                return;
            }

            if (excepcion.InnerException != null)
            {
                AgregarDetalleExcepcion(detalle, excepcion.InnerException);
            }
        }

        private static bool ContieneAlguno(string texto, params string[] valores)
        {
            return valores.Any(valor => texto.Contains(valor, StringComparison.OrdinalIgnoreCase));
        }

        private static string CrearCuerpo(string nombreDestinatario, string codigo, DateTime expira)
        {
            string saludo = string.IsNullOrWhiteSpace(nombreDestinatario)
                ? "Hola"
                : $"Hola {nombreDestinatario}";

            return
                $"{saludo},\r\n\r\n" +
                "Recibimos una solicitud para restablecer la contraseña del Sistema Agrícola.\r\n\r\n" +
                $"Código de recuperación: {codigo}\r\n" +
                $"Válido hasta: {expira:dd/MM/yyyy hh:mm tt}\r\n\r\n" +
                "Si usted no solicitó este cambio, puede ignorar este correo.";
        }

        private static string CrearCuerpoVerificacion(
            string nombreDestinatario,
            string codigo,
            DateTime expira,
            string tipoCuenta)
        {
            string saludo = string.IsNullOrWhiteSpace(nombreDestinatario)
                ? "Hola"
                : $"Hola {nombreDestinatario}";

            return
                $"{saludo},\r\n\r\n" +
                $"Use este codigo para verificar el correo del {tipoCuenta} en el Sistema Agricola.\r\n\r\n" +
                $"Codigo de verificacion: {codigo}\r\n" +
                $"Valido hasta: {expira:dd/MM/yyyy hh:mm tt}\r\n\r\n" +
                "Si usted no esta registrando esta cuenta, puede ignorar este correo.";
        }

        private static string CrearCuerpoAlerta(
            string nombreDestinatario,
            string titulo,
            string descripcion,
            string cultivo,
            DateTime fechaProgramada,
            string tipoAlerta,
            string prioridad,
            int diasRestantes)
        {
            string saludo = string.IsNullOrWhiteSpace(nombreDestinatario)
                ? "Hola"
                : $"Hola {nombreDestinatario}";

            string tiempo = diasRestantes < 0
                ? $"Esta alerta esta vencida por {Math.Abs(diasRestantes)} dia(s)."
                : diasRestantes == 0
                    ? "Esta alerta esta programada para hoy."
                    : $"Faltan {diasRestantes} dia(s) para esta actividad.";

            return
                $"{saludo},\r\n\r\n" +
                "Tiene una alerta pendiente del Sistema Agricola.\r\n\r\n" +
                $"Titulo: {titulo}\r\n" +
                $"Cultivo: {cultivo}\r\n" +
                $"Tipo: {tipoAlerta}\r\n" +
                $"Prioridad: {prioridad}\r\n" +
                $"Fecha programada: {fechaProgramada:dd/MM/yyyy}\r\n" +
                $"{tiempo}\r\n\r\n" +
                $"Detalle: {descripcion}\r\n\r\n" +
                "Revise el sistema para dar seguimiento a esta actividad.";
        }

        private static string ObtenerVariableRequerida(string nombre)
        {
            string? valor = Environment.GetEnvironmentVariable(nombre);
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new InvalidOperationException(
                    "Para enviar el código por Gmail configure las variables SISTEMA_AGRICOLA_GMAIL y SISTEMA_AGRICOLA_GMAIL_APP_PASSWORD.");
            }

            return valor;
        }

        private static string ObtenerVariableOpcional(string nombre, string valorPorDefecto)
        {
            string? valor = Environment.GetEnvironmentVariable(nombre);
            return string.IsNullOrWhiteSpace(valor) ? valorPorDefecto : valor;
        }

        private static int ObtenerPuerto()
        {
            string? valor = Environment.GetEnvironmentVariable("SISTEMA_AGRICOLA_SMTP_PORT");
            return int.TryParse(valor, out int puerto) ? puerto : 587;
        }

        private static bool ObtenerSsl()
        {
            string? valor = Environment.GetEnvironmentVariable("SISTEMA_AGRICOLA_SMTP_SSL");
            return !bool.TryParse(valor, out bool ssl) || ssl;
        }
    }
}

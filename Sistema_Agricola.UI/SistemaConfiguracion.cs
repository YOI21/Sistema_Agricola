using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Sistema_Agricola.UI
{
    internal class SistemaConfiguracion
    {
        public string NombreSistema { get; set; } = "Sistema Agricola";
        public string NombreFinca { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Moneda { get; set; } = "RD$";
        public int DiasAlertaAnticipacion { get; set; } = 7;
        public int StockMinimoGeneral { get; set; } = 10;
        public bool NotificarAlertasGmail { get; set; } = true;
        public bool CrearAlertasAutomaticas { get; set; } = true;
        public string GmailRemitente { get; set; } = string.Empty;
        public string GmailClaveAplicacion { get; set; } = string.Empty;
        public string GmailClaveAplicacionCifrada { get; set; } = string.Empty;
        public string NombreRemitente { get; set; } = "Sistema Agricola";
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPuerto { get; set; } = 587;
        public bool SmtpSsl { get; set; } = true;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }

    internal static class SistemaConfiguracionServicio
    {
        private static readonly byte[] EntropiaCifrado =
            Encoding.UTF8.GetBytes("Sistema_Agricola.GmailClaveAplicacion.v1");
        private static readonly byte[] EntropiaConexion =
            Encoding.UTF8.GetBytes("Sistema_Agricola.ConexionBaseDatos.v1");

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static string RutaArchivo
        {
            get
            {
                string carpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Sistema_Agricola");
                return Path.Combine(carpeta, "configuracion_sistema.json");
            }
        }

        public static string RutaConexionBaseDatos
        {
            get
            {
                string carpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Sistema_Agricola");
                return Path.Combine(carpeta, "conexion_bd.dat");
            }
        }

        public static bool AplicarConexionBaseDatos()
        {
            if (!File.Exists(RutaConexionBaseDatos))
            {
                Environment.SetEnvironmentVariable(
                    "SISTEMA_AGRICOLA_CONNECTION_STRING",
                    null,
                    EnvironmentVariableTarget.Process);
                return false;
            }

            try
            {
                byte[] cifrado = Convert.FromBase64String(
                    File.ReadAllText(RutaConexionBaseDatos).Trim());
                byte[] datos = ProtectedData.Unprotect(
                    cifrado,
                    EntropiaConexion,
                    DataProtectionScope.CurrentUser);
                string cadenaConexion = Encoding.UTF8.GetString(datos);
                Array.Clear(datos, 0, datos.Length);

                if (string.IsNullOrWhiteSpace(cadenaConexion))
                {
                    return false;
                }

                Environment.SetEnvironmentVariable(
                    "SISTEMA_AGRICOLA_CONNECTION_STRING",
                    cadenaConexion,
                    EnvironmentVariableTarget.Process);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SistemaConfiguracion Cargar()
        {
            try
            {
                if (!File.Exists(RutaArchivo))
                {
                    SistemaConfiguracion nueva = new SistemaConfiguracion();
                    Guardar(nueva);
                    return nueva;
                }

                string json = File.ReadAllText(RutaArchivo);
                SistemaConfiguracion? config =
                    JsonSerializer.Deserialize<SistemaConfiguracion>(json, JsonOptions);
                config ??= new SistemaConfiguracion();
                bool requiereMigracion = PrepararConfigCargada(config);
                AplicarVariablesCorreo(config);

                if (requiereMigracion)
                {
                    Guardar(config);
                }

                return config;
            }
            catch
            {
                return new SistemaConfiguracion();
            }
        }

        public static void Guardar(SistemaConfiguracion config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config.FechaActualizacion = DateTime.Now;
            SistemaConfiguracion configParaArchivo = CrearConfigParaArchivo(config);
            string? carpeta = Path.GetDirectoryName(RutaArchivo);
            if (!string.IsNullOrWhiteSpace(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            File.WriteAllText(RutaArchivo, JsonSerializer.Serialize(configParaArchivo, JsonOptions));
            AplicarVariablesCorreo(config);
        }

        public static void AplicarVariablesCorreo(SistemaConfiguracion config)
        {
            if (config == null)
            {
                return;
            }

            Set("SISTEMA_AGRICOLA_GMAIL", config.GmailRemitente);
            Set("SISTEMA_AGRICOLA_GMAIL_APP_PASSWORD", config.GmailClaveAplicacion);
            Set("SISTEMA_AGRICOLA_FROM_NAME", config.NombreRemitente);
            Set("SISTEMA_AGRICOLA_SMTP_HOST", config.SmtpHost);
            Set("SISTEMA_AGRICOLA_SMTP_PORT", config.SmtpPuerto.ToString());
            Set("SISTEMA_AGRICOLA_SMTP_SSL", config.SmtpSsl.ToString());
        }

        private static bool PrepararConfigCargada(SistemaConfiguracion config)
        {
            if (!string.IsNullOrWhiteSpace(config.GmailClaveAplicacionCifrada))
            {
                config.GmailClaveAplicacion = Descifrar(config.GmailClaveAplicacionCifrada);
                return false;
            }

            return !string.IsNullOrWhiteSpace(config.GmailClaveAplicacion);
        }

        private static SistemaConfiguracion CrearConfigParaArchivo(SistemaConfiguracion config)
        {
            return new SistemaConfiguracion
            {
                NombreSistema = config.NombreSistema,
                NombreFinca = config.NombreFinca,
                Propietario = config.Propietario,
                Telefono = config.Telefono,
                Direccion = config.Direccion,
                Moneda = config.Moneda,
                DiasAlertaAnticipacion = config.DiasAlertaAnticipacion,
                StockMinimoGeneral = config.StockMinimoGeneral,
                NotificarAlertasGmail = config.NotificarAlertasGmail,
                CrearAlertasAutomaticas = config.CrearAlertasAutomaticas,
                GmailRemitente = config.GmailRemitente,
                GmailClaveAplicacion = string.Empty,
                GmailClaveAplicacionCifrada = Cifrar(config.GmailClaveAplicacion),
                NombreRemitente = config.NombreRemitente,
                SmtpHost = config.SmtpHost,
                SmtpPuerto = config.SmtpPuerto,
                SmtpSsl = config.SmtpSsl,
                FechaActualizacion = config.FechaActualizacion
            };
        }

        private static string Cifrar(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return string.Empty;
            }

            byte[] datos = Encoding.UTF8.GetBytes(valor.Trim());
            byte[] cifrado = ProtectedData.Protect(
                datos,
                EntropiaCifrado,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(cifrado);
        }

        private static string Descifrar(string valorCifrado)
        {
            if (string.IsNullOrWhiteSpace(valorCifrado))
            {
                return string.Empty;
            }

            try
            {
                byte[] datosCifrados = Convert.FromBase64String(valorCifrado);
                byte[] datos = ProtectedData.Unprotect(
                    datosCifrados,
                    EntropiaCifrado,
                    DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(datos);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static void Set(string nombre, string? valor)
        {
            // Si la configuracion local aun esta vacia, conserve las variables
            // heredadas de CMD o PowerShell para permitir la instalacion inicial.
            if (string.IsNullOrWhiteSpace(valor))
            {
                return;
            }

            Environment.SetEnvironmentVariable(
                nombre,
                valor.Trim(),
                EnvironmentVariableTarget.Process);
        }
    }
}

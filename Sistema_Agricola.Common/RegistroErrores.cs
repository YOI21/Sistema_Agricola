using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sistema_Agricola.Common
{
    public static class RegistroErrores
    {
        private const string ClaveCodigo = "SistemaAgricola.CodigoError";
        private static readonly object Sincronizacion = new object();

        public static string DirectorioLogs => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Sistema_Agricola",
            "Logs");

        public static string Registrar(
            Exception excepcion,
            string contexto = "Aplicacion",
            IReadOnlyDictionary<string, string>? datos = null)
        {
            ArgumentNullException.ThrowIfNull(excepcion);

            if (excepcion.Data[ClaveCodigo] is string codigoExistente &&
                !string.IsNullOrWhiteSpace(codigoExistente))
            {
                return codigoExistente;
            }

            string codigo = $"ERR-{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid():N}"[..27].ToUpperInvariant();
            excepcion.Data[ClaveCodigo] = codigo;

            try
            {
                Directory.CreateDirectory(DirectorioLogs);
                string ruta = Path.Combine(DirectorioLogs, $"errores-{DateTime.Now:yyyy-MM-dd}.jsonl");
                var entrada = new
                {
                    codigo,
                    fechaLocal = DateTimeOffset.Now,
                    contexto,
                    equipo = Environment.MachineName,
                    usuarioWindows = Environment.UserName,
                    proceso = Environment.ProcessPath,
                    version = typeof(RegistroErrores).Assembly.GetName().Version?.ToString(),
                    tipo = excepcion.GetType().FullName,
                    detalle = LimpiarDatosSensibles(excepcion.ToString()),
                    datos
                };

                string linea = JsonSerializer.Serialize(entrada);
                lock (Sincronizacion)
                {
                    File.AppendAllText(ruta, linea + Environment.NewLine);
                }
            }
            catch (Exception errorLog)
            {
                Trace.TraceError($"No se pudo registrar {codigo}: {errorLog}");
            }

            return codigo;
        }

        public static string ObtenerMensajeSeguro(
            Exception excepcion,
            string contexto = "Aplicacion")
        {
            string codigo = Registrar(excepcion, contexto);
            return $"Ocurrio un inconveniente. Codigo de seguimiento: {codigo}.";
        }

        public static string ObtenerReferencia(
            Exception excepcion,
            string contexto = "Aplicacion")
        {
            return "Detalle registrado con el codigo " + Registrar(excepcion, contexto) + ".";
        }

        private static string LimpiarDatosSensibles(string detalle)
        {
            if (string.IsNullOrWhiteSpace(detalle))
            {
                return string.Empty;
            }

            string limpio = Regex.Replace(
                detalle,
                @"(?i)(password|pwd|token|secret|api[_-]?key)\s*=\s*[^;\s\r\n]+",
                "$1=[PROTEGIDO]");
            limpio = Regex.Replace(
                limpio,
                @"(?i)(Authorization:\s*Bearer\s+)[^\s\r\n]+",
                "$1[PROTEGIDO]");
            return limpio;
        }
    }
}

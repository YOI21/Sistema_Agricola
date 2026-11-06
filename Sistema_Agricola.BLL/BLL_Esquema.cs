using System.Diagnostics;
using Sistema_Agricola.DAL;

namespace Sistema_Agricola.BLL
{
    public sealed class ResultadoVerificacionEsquema
    {
        public bool EsCompatible { get; init; }
        public int VersionEncontrada { get; init; }
        public int VersionRequerida { get; init; }
        public string Mensaje { get; init; } = string.Empty;
    }

    public static class BLL_Esquema
    {
        public const int VersionRequerida = 12;

        public static ResultadoVerificacionEsquema VerificarCompatibilidad()
        {
            try
            {
                EstadoEsquemaDatos estado = new DAL_Esquema().ObtenerEstado();

                if (!estado.ExisteTablaVersion)
                {
                    return CrearError(
                        estado.VersionActual,
                        "La base de datos no tiene control de versión. Ejecute las migraciones 000 a 012 antes de abrir el sistema.");
                }

                if (estado.VersionActual != VersionRequerida)
                {
                    return CrearError(
                        estado.VersionActual,
                        $"La base de datos está en la versión {estado.VersionActual} y el programa requiere la versión {VersionRequerida}. Ejecute las migraciones pendientes.");
                }

                if (estado.ObjetosFaltantes.Count > 0)
                {
                    return CrearError(
                        estado.VersionActual,
                        "La base de datos está incompleta. Faltan: " + string.Join(", ", estado.ObjetosFaltantes) + ".");
                }

                return new ResultadoVerificacionEsquema
                {
                    EsCompatible = true,
                    VersionEncontrada = estado.VersionActual,
                    VersionRequerida = VersionRequerida,
                    Mensaje = "La base de datos está actualizada."
                };
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error al verificar el esquema de la base de datos: {ex}");
                return CrearError(
                    -1,
                    "No fue posible comprobar la base de datos. Verifique que SQL Server esté iniciado y que la conexión esté configurada correctamente.");
            }
        }

        private static ResultadoVerificacionEsquema CrearError(int versionEncontrada, string mensaje)
        {
            return new ResultadoVerificacionEsquema
            {
                EsCompatible = false,
                VersionEncontrada = versionEncontrada,
                VersionRequerida = VersionRequerida,
                Mensaje = mensaje
            };
        }
    }
}

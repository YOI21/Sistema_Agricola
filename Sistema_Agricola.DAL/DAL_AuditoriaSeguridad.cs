using System;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace Sistema_Agricola.DAL
{
    public class DAL_AuditoriaSeguridad
    {
        private const string ExtensionPendiente = ".audit";
        private static readonly object Sincronizacion = new object();
        private static readonly byte[] Entropia = Encoding.UTF8.GetBytes(
            "Sistema_Agricola.AuditoriaPendiente.v1");

        private static string DirectorioPendientes => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Sistema_Agricola",
            "AuditoriaPendiente");

        public void Registrar(
            int? idUsuario,
            string usuario,
            string rol,
            string permiso,
            string entidad,
            string accion,
            string detalle,
            string resultado)
        {
            RegistrarDurable(
                idUsuario,
                usuario,
                rol,
                permiso,
                entidad,
                accion,
                detalle,
                resultado);
        }

        public bool RegistrarDurable(
            int? idUsuario,
            string usuario,
            string rol,
            string permiso,
            string entidad,
            string accion,
            string detalle,
            string resultado)
        {
            RegistroAuditoria registro = new RegistroAuditoria
            {
                IdUsuario = idUsuario,
                Usuario = usuario,
                Rol = rol,
                Permiso = permiso,
                Entidad = entidad,
                Accion = accion,
                Detalle = detalle,
                Resultado = resultado,
                FechaOperacion = DateTimeOffset.Now
            };

            lock (Sincronizacion)
            {
                ReenviarPendientes();

                try
                {
                    RegistrarEnBase(registro);
                    return true;
                }
                catch (Exception errorBaseDatos)
                {
                    try
                    {
                        GuardarPendiente(registro);
                        Trace.TraceWarning(
                            "La auditoria no llego a SQL Server y se guardo en la cola local cifrada: " +
                            errorBaseDatos.Message);
                        return false;
                    }
                    catch (Exception errorCola)
                    {
                        throw new InvalidOperationException(
                            "No fue posible registrar ni conservar localmente el evento de auditoria.",
                            new AggregateException(errorBaseDatos, errorCola));
                    }
                }
            }
        }

        private static void RegistrarEnBase(RegistroAuditoria registro)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = new SqlCommand(
                "Registrar_Auditoria_Seguridad",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            string detalleConFecha =
                $"FechaOperacion={registro.FechaOperacion:O}; {registro.Detalle}";

            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value =
                registro.IdUsuario.HasValue ? registro.IdUsuario.Value : DBNull.Value;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 80).Value =
                Cortar(registro.Usuario, 80);
            cmd.Parameters.Add("@Rol", SqlDbType.VarChar, 30).Value =
                Cortar(registro.Rol, 30);
            cmd.Parameters.Add("@Permiso", SqlDbType.VarChar, 80).Value =
                Cortar(registro.Permiso, 80);
            cmd.Parameters.Add("@Entidad", SqlDbType.VarChar, 80).Value =
                Cortar(registro.Entidad, 80);
            cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 40).Value =
                Cortar(registro.Accion, 40);
            cmd.Parameters.Add("@Detalle", SqlDbType.VarChar, 1000).Value =
                Cortar(detalleConFecha, 1000);
            cmd.Parameters.Add("@Resultado", SqlDbType.VarChar, 20).Value =
                Cortar(registro.Resultado, 20);
            cmd.ExecuteNonQuery();
        }

        private static void GuardarPendiente(RegistroAuditoria registro)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException(
                    "La cola cifrada de auditoria requiere Windows.");
            }

            Directory.CreateDirectory(DirectorioPendientes);

            byte[] contenido = JsonSerializer.SerializeToUtf8Bytes(registro);
            byte[] cifrado = ProtectedData.Protect(
                contenido,
                Entropia,
                DataProtectionScope.CurrentUser);

            string nombre =
                $"{registro.FechaOperacion:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{ExtensionPendiente}";
            string destino = Path.Combine(DirectorioPendientes, nombre);
            string temporal = destino + ".tmp";

            try
            {
                File.WriteAllBytes(temporal, cifrado);
                File.Move(temporal, destino);
            }
            finally
            {
                if (File.Exists(temporal))
                {
                    File.Delete(temporal);
                }
            }
        }

        private static void ReenviarPendientes()
        {
            if (!Directory.Exists(DirectorioPendientes))
            {
                return;
            }

            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException(
                    "La cola cifrada de auditoria requiere Windows.");
            }

            foreach (string archivo in Directory.GetFiles(
                         DirectorioPendientes,
                         "*" + ExtensionPendiente)
                     .OrderBy(ruta => ruta, StringComparer.OrdinalIgnoreCase))
            {
                RegistroAuditoria? registro;
                try
                {
                    byte[] cifrado = File.ReadAllBytes(archivo);
                    byte[] contenido = ProtectedData.Unprotect(
                        cifrado,
                        Entropia,
                        DataProtectionScope.CurrentUser);
                    registro = JsonSerializer.Deserialize<RegistroAuditoria>(contenido);
                    if (registro == null)
                    {
                        throw new InvalidDataException("El registro pendiente esta vacio.");
                    }
                }
                catch (Exception ex)
                {
                    string invalido = archivo + ".invalido";
                    File.Move(archivo, invalido, overwrite: true);
                    Trace.TraceError(
                        $"Se aislo un archivo de auditoria pendiente invalido ({invalido}): {ex}");
                    continue;
                }

                try
                {
                    RegistrarEnBase(registro);
                    File.Delete(archivo);
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning(
                        "La cola de auditoria sigue pendiente porque SQL Server no esta disponible: " +
                        ex.Message);
                    break;
                }
            }
        }

        private static string Cortar(string? valor, int maximo)
        {
            string texto = valor?.Trim() ?? string.Empty;
            return texto.Length <= maximo ? texto : texto.Substring(0, maximo);
        }

        private sealed class RegistroAuditoria
        {
            public int? IdUsuario { get; set; }
            public string Usuario { get; set; } = string.Empty;
            public string Rol { get; set; } = string.Empty;
            public string Permiso { get; set; } = string.Empty;
            public string Entidad { get; set; } = string.Empty;
            public string Accion { get; set; } = string.Empty;
            public string Detalle { get; set; } = string.Empty;
            public string Resultado { get; set; } = string.Empty;
            public DateTimeOffset FechaOperacion { get; set; }
        }
    }
}

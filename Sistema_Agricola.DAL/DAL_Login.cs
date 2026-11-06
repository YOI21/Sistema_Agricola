using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Login
    {
        private const int MaximoIntentos = 5;
        private const int MinutosBloqueo = 15;

        public bool ExistenUsuariosRegistrados()
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Existe_Usuario_Registrado", conexion);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool ExisteAdministradorRegistrado()
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Existe_Administrador_Seguro", conexion);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public Entitie_ResultadoAutenticacion Autenticar(string usuario, string contrasena)
        {
            using SqlConnection conexion = Conexion.conectardb();
            CuentaAutenticacion? cuenta = ObtenerCuenta(conexion, usuario);

            if (cuenta == null)
            {
                SeguridadContrasena.EjecutarVerificacionSimulada(contrasena);
                Entitie_ResultadoAutenticacion desconocido = CrearFallido();
                AuditarIntento(null, usuario, "Sin rol", desconocido);
                return desconocido;
            }

            if (cuenta.BloqueadoHasta.HasValue && cuenta.BloqueadoHasta.Value > DateTime.Now)
            {
                Entitie_ResultadoAutenticacion bloqueado = new Entitie_ResultadoAutenticacion
                {
                    Bloqueado = true,
                    BloqueadoHasta = cuenta.BloqueadoHasta,
                    Mensaje = $"Acceso bloqueado temporalmente hasta {cuenta.BloqueadoHasta:dd/MM/yyyy HH:mm}."
                };
                AuditarIntento(cuenta.IDUsuario, cuenta.Usuario, cuenta.Rol, bloqueado);
                return bloqueado;
            }

            bool contrasenaValida = SeguridadContrasena.Verificar(
                contrasena,
                cuenta.Hash,
                cuenta.Salt,
                cuenta.Iteraciones);

            if (!contrasenaValida)
            {
                EstadoIntentoFallido estado = RegistrarIntentoFallido(conexion, cuenta.IDUsuario);
                Entitie_ResultadoAutenticacion fallido = estado.BloqueadoHasta.HasValue
                    ? new Entitie_ResultadoAutenticacion
                    {
                        Bloqueado = true,
                        BloqueadoHasta = estado.BloqueadoHasta,
                        Mensaje = $"Se alcanzó el límite de intentos. Acceso bloqueado hasta {estado.BloqueadoHasta:dd/MM/yyyy HH:mm}."
                    }
                    : new Entitie_ResultadoAutenticacion
                    {
                        IntentosRestantes = Math.Max(0, MaximoIntentos - estado.Intentos),
                        Mensaje = $"Usuario o contraseña incorrectos. Intentos restantes: {Math.Max(0, MaximoIntentos - estado.Intentos)}."
                    };

                AuditarIntento(cuenta.IDUsuario, cuenta.Usuario, cuenta.Rol, fallido);
                return fallido;
            }

            RegistrarLoginExitoso(conexion, cuenta.IDUsuario);
            Entitie_UsuarioAutenticado usuarioAutenticado = new Entitie_UsuarioAutenticado
            {
                ID = cuenta.IDPerfil,
                ID_Usuario = cuenta.IDUsuario,
                Username = cuenta.Usuario,
                FullName = (cuenta.Nombre + " " + cuenta.Apellido).Trim(),
                IsAgricultor = string.Equals(cuenta.Rol, "Agricultor", StringComparison.OrdinalIgnoreCase),
                Rol = cuenta.Rol,
                DebeCambiarContrasena = cuenta.DebeCambiarContrasena
            };

            Entitie_ResultadoAutenticacion exitoso = new Entitie_ResultadoAutenticacion
            {
                Exitoso = true,
                Mensaje = "Inicio de sesión correcto.",
                Usuario = usuarioAutenticado,
                IntentosRestantes = MaximoIntentos
            };
            AuditarIntento(cuenta.IDUsuario, cuenta.Usuario, cuenta.Rol, exitoso);
            return exitoso;
        }

        public void CambiarContrasena(int idUsuario, string nuevaContrasena)
        {
            using SqlConnection conexion = Conexion.conectardb();
            SeguridadContrasena.GuardarHash(conexion, idUsuario, nuevaContrasena);
        }

        private static CuentaAutenticacion? ObtenerCuenta(SqlConnection conexion, string usuario)
        {
            using SqlCommand cmd = CrearProcedimiento("Obtener_Cuenta_Autenticacion", conexion);
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.Trim();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            if (reader["Contrasena_Hash"] == DBNull.Value ||
                reader["Contrasena_Salt"] == DBNull.Value ||
                reader["Contrasena_Iteraciones"] == DBNull.Value)
            {
                throw new InvalidOperationException(
                    "La cuenta no tiene una contraseña segura. Use Olvidé mi contraseña para restablecerla.");
            }

            return new CuentaAutenticacion
            {
                IDUsuario = Convert.ToInt32(reader["ID_Usuario"]),
                IDPerfil = Convert.ToInt32(reader["ID_Perfil"]),
                Usuario = Convert.ToString(reader["Usuario"]) ?? string.Empty,
                Rol = Convert.ToString(reader["Rol"]) ?? string.Empty,
                Nombre = Convert.ToString(reader["Nombre"]) ?? string.Empty,
                Apellido = Convert.ToString(reader["Apellido"]) ?? string.Empty,
                Hash = (byte[])reader["Contrasena_Hash"],
                Salt = Convert.ToString(reader["Contrasena_Salt"]) ?? string.Empty,
                Iteraciones = Convert.ToInt32(reader["Contrasena_Iteraciones"]),
                DebeCambiarContrasena = Convert.ToBoolean(reader["Debe_Cambiar_Contrasena"]),
                BloqueadoHasta = reader["Bloqueado_Hasta"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["Bloqueado_Hasta"])
            };
        }

        private static EstadoIntentoFallido RegistrarIntentoFallido(
            SqlConnection conexion,
            int idUsuario)
        {
            using SqlCommand cmd = CrearProcedimiento("Registrar_Intento_Login_Fallido", conexion);
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@Maximo_Intentos", SqlDbType.Int).Value = MaximoIntentos;
            cmd.Parameters.Add("@Minutos_Bloqueo", SqlDbType.Int).Value = MinutosBloqueo;
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("No se pudo registrar el intento de acceso.");
            }

            return new EstadoIntentoFallido
            {
                Intentos = Convert.ToInt32(reader["Intentos_Fallidos"]),
                BloqueadoHasta = reader["Bloqueado_Hasta"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["Bloqueado_Hasta"])
            };
        }

        private static void RegistrarLoginExitoso(SqlConnection conexion, int idUsuario)
        {
            using SqlCommand cmd = CrearProcedimiento("Registrar_Login_Exitoso", conexion);
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.ExecuteNonQuery();
        }

        private static SqlCommand CrearProcedimiento(string nombre, SqlConnection conexion)
        {
            return new SqlCommand(nombre, conexion) { CommandType = CommandType.StoredProcedure };
        }

        private static Entitie_ResultadoAutenticacion CrearFallido()
        {
            return new Entitie_ResultadoAutenticacion
            {
                Mensaje = "Usuario o contraseña incorrectos."
            };
        }

        private static void AuditarIntento(
            int? idUsuario,
            string usuario,
            string rol,
            Entitie_ResultadoAutenticacion resultado)
        {
            try
            {
                string estado = resultado.Exitoso
                    ? "Exito"
                    : resultado.Bloqueado ? "Denegado" : "Fallido";
                new DAL_AuditoriaSeguridad().Registrar(
                    idUsuario,
                    usuario,
                    rol,
                    "autenticacion.iniciar_sesion",
                    "Usuario",
                    "Iniciar sesion",
                    resultado.Bloqueado ? "Cuenta bloqueada temporalmente." : resultado.Mensaje,
                    estado);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"No se pudo auditar el intento de autenticacion: {ex}");
            }
        }

        private sealed class CuentaAutenticacion
        {
            public int IDUsuario { get; init; }
            public int IDPerfil { get; init; }
            public string Usuario { get; init; } = string.Empty;
            public string Rol { get; init; } = string.Empty;
            public string Nombre { get; init; } = string.Empty;
            public string Apellido { get; init; } = string.Empty;
            public byte[] Hash { get; init; } = Array.Empty<byte>();
            public string Salt { get; init; } = string.Empty;
            public int Iteraciones { get; init; }
            public bool DebeCambiarContrasena { get; init; }
            public DateTime? BloqueadoHasta { get; init; }
        }

        private sealed class EstadoIntentoFallido
        {
            public int Intentos { get; init; }
            public DateTime? BloqueadoHasta { get; init; }
        }
    }
}

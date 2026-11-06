using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_RecuperacionContrasena
    {
        public Entitie_CuentaRecuperacion? ObtenerCuentaPorUsuarioOCorreo(string identificador)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Obtener_Cuenta_Recuperacion", conexion);
            cmd.Parameters.Add("@Identificador", SqlDbType.VarChar, 100).Value = identificador.Trim();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new Entitie_CuentaRecuperacion
            {
                ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                Usuario = Convert.ToString(reader["Usuario"]) ?? string.Empty,
                Email = Convert.ToString(reader["Email"]) ?? string.Empty,
                NombreCompleto = Convert.ToString(reader["NombreCompleto"]) ?? string.Empty,
                TipoUsuario = Convert.ToString(reader["TipoUsuario"]) ?? string.Empty
            };
        }

        public bool ActualizarContrasena(int idUsuario, string nuevaContrasena)
        {
            using SqlConnection conexion = Conexion.conectardb();
            SeguridadContrasena.GuardarHash(conexion, idUsuario, nuevaContrasena);
            return true;
        }

        public void RegistrarCodigoRecuperacion(
            int idUsuario,
            byte[] codigoHash,
            string codigoSalt,
            DateTime fechaExpiracion)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Registrar_Codigo_Recuperacion", conexion);
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@Codigo_Hash", SqlDbType.VarBinary, 64).Value = codigoHash;
            cmd.Parameters.Add("@Codigo_Salt", SqlDbType.VarChar, 50).Value = codigoSalt;
            cmd.Parameters.Add("@Fecha_Expiracion", SqlDbType.DateTime).Value = fechaExpiracion;
            cmd.ExecuteNonQuery();
        }

        public Entitie_CodigoRecuperacion? ObtenerCodigoActivo(int idUsuario)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Obtener_Codigo_Recuperacion_Activo", conexion);
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new Entitie_CodigoRecuperacion
            {
                ID_Recuperacion = Convert.ToInt32(reader["ID_Recuperacion"]),
                ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                Codigo_Hash = (byte[])reader["Codigo_Hash"],
                Codigo_Salt = Convert.ToString(reader["Codigo_Salt"]) ?? string.Empty,
                Fecha_Expiracion = Convert.ToDateTime(reader["Fecha_Expiracion"]),
                Usado = Convert.ToBoolean(reader["Usado"]),
                Intentos = Convert.ToInt32(reader["Intentos"])
            };
        }

        public void RegistrarIntentoFallido(int idRecuperacion)
        {
            EjecutarPorId("Registrar_Intento_Recuperacion_Fallido", idRecuperacion);
        }

        public void MarcarCodigoUsado(int idRecuperacion)
        {
            EjecutarPorId("Marcar_Codigo_Recuperacion_Usado", idRecuperacion);
        }

        public bool ActualizarContrasenaYMarcarCodigoUsado(
            int idUsuario,
            string nuevaContrasena,
            int idRecuperacion)
        {
            DatosHashContrasena datos = SeguridadContrasena.CrearHash(nuevaContrasena);
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Restablecer_Contrasena_Recuperacion", conexion);
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@ID_Recuperacion", SqlDbType.Int).Value = idRecuperacion;
            cmd.Parameters.Add("@Hash", SqlDbType.VarBinary, 64).Value = datos.Hash;
            cmd.Parameters.Add("@Salt", SqlDbType.VarChar, 64).Value = datos.Salt;
            cmd.Parameters.Add("@Iteraciones", SqlDbType.Int).Value = datos.Iteraciones;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private static void EjecutarPorId(string procedimiento, int idRecuperacion)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento(procedimiento, conexion);
            cmd.Parameters.Add("@ID_Recuperacion", SqlDbType.Int).Value = idRecuperacion;
            cmd.ExecuteNonQuery();
        }

        private static SqlCommand CrearProcedimiento(string nombre, SqlConnection conexion)
        {
            return new SqlCommand(nombre, conexion) { CommandType = CommandType.StoredProcedure };
        }
    }
}

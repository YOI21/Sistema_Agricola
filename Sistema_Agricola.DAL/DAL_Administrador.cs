using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Administrador
    {
        public bool ExisteAdministradorRegistrado()
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Existe_Administrador_Seguro", conexion);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool ExisteUsuario(string usuario)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Existe_Usuario_Seguro", conexion);
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.Trim();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool ExisteEmailAdministrador(string email)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Existe_Email_Administrador_Seguro", conexion);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email.Trim();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public int InsertarAdministrador(Entitie_Administrador administrador)
        {
            DatosHashContrasena datos = SeguridadContrasena.CrearHash(administrador.Contrasena);
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = CrearProcedimiento("Registrar_Administrador_Seguro", conexion);
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = administrador.Nombre.Trim();
            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = administrador.Apellido.Trim();
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = administrador.Email.Trim();
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = administrador.Telefono.Trim();
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = administrador.Usuario.Trim();
            cmd.Parameters.Add("@Contrasena_Hash", SqlDbType.VarBinary, 64).Value = datos.Hash;
            cmd.Parameters.Add("@Contrasena_Salt", SqlDbType.VarChar, 64).Value = datos.Salt;
            cmd.Parameters.Add("@Contrasena_Iteraciones", SqlDbType.Int).Value = datos.Iteraciones;
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static SqlCommand CrearProcedimiento(string nombre, SqlConnection conexion)
        {
            return new SqlCommand(nombre, conexion) { CommandType = CommandType.StoredProcedure };
        }
    }
}

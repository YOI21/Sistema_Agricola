using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_RegistroAgricultor
    {
        public bool RegistrarAgricultor(Entitie_RegistroAgricultor agricultor)
        {
            using SqlConnection conexion = Conexion.conectardb();
            try
            {
                DatosHashContrasena datos = SeguridadContrasena.CrearHash(agricultor.Contrasena);
                using SqlCommand cmd = new SqlCommand(
                    "Insertar_Agricultor",
                    conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value =
                    agricultor.Nombre.Trim();
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value =
                    agricultor.Apellido.Trim();
                cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value =
                    agricultor.Cedula.Trim();
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 11).Value =
                    agricultor.Telefono.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value =
                    agricultor.Email.Trim();
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value =
                    agricultor.Direccion.Trim();
                cmd.Parameters.Add("@Ciudad", SqlDbType.VarChar, 50).Value =
                    agricultor.Ciudad.Trim();
                cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 50).Value =
                    agricultor.Sector.Trim();
                cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value =
                    agricultor.Usuario.Trim();
                cmd.Parameters.Add("@Contrasena_Hash", SqlDbType.VarBinary, 64).Value = datos.Hash;
                cmd.Parameters.Add("@Contrasena_Salt", SqlDbType.VarChar, 64).Value = datos.Salt;
                cmd.Parameters.Add("@Contrasena_Iteraciones", SqlDbType.Int).Value = datos.Iteraciones;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                return false;
            }
        }

        public bool ExisteUsuario(string usuario)
        {
            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = new SqlCommand("Existe_Usuario_Seguro", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.Trim();

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool ExisteCedulaAgricultor(string cedula)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM Agricultor
                WHERE Cedula = @Cedula;";

            using SqlConnection conexion = Conexion.conectardb();
            using SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value = cedula.Trim();

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public Entitie_DatosCedula? ObtenerDatosCedula(string cedula)
        {
            using SqlConnection conexion = Conexion.conectardb();

            if (!ExisteTabla(conexion, "Cedulas_Verificadas"))
            {
                return null;
            }

            const string sql = @"
                SELECT TOP 1
                    Cedula,
                    Nombre,
                    Apellido
                FROM Cedulas_Verificadas
                WHERE Cedula = @Cedula;";

            using SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value = cedula.Trim();

            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new Entitie_DatosCedula
            {
                Cedula = reader["Cedula"].ToString() ?? string.Empty,
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Apellido = reader["Apellido"].ToString() ?? string.Empty
            };
        }

        private bool ExisteTabla(SqlConnection conexion, string tabla)
        {
            const string sql = "SELECT COUNT(*) FROM sys.tables WHERE name = @Tabla;";
            using SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Tabla", SqlDbType.VarChar, 128).Value = tabla;
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}

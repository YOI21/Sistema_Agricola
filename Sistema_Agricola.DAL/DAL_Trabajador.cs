using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Trabajador
    {
        // Listar todos los trabajadores
        public DataTable Listar_Trabajadores()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Listar_Trabajadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        // Buscar trabajadores
        public DataTable Buscar_Trabajador(string buscar)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Buscar_Trabajador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Buscador", SqlDbType.VarChar).Value = buscar;
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        // Insertar nuevo trabajador
        public string Insertar_Trabajador(Entitie_Trabajador obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();

                string duplicado = ObtenerCampoDuplicado(conexion, obj, null);
                if (!string.IsNullOrEmpty(duplicado))
                {
                    return duplicado;
                }

                SqlCommand cmd = new SqlCommand("Insertar_Trabajador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = obj.Nombre;
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = obj.Apellido;
                cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value = obj.Cedula;
                cmd.Parameters.Add("@Sexo", SqlDbType.VarChar, 10).Value = obj.Sexo;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = obj.Email;
                cmd.Parameters.Add("@Fecha_Nac", SqlDbType.Date).Value = obj.Fecha_Nac;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = obj.Direccion;
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 11).Value = obj.Telefono;
                cmd.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = obj.Ciudad;
                cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 50).Value = obj.Sector;
                cmd.Parameters.Add("@Posicion", SqlDbType.VarChar, 50).Value = obj.Posicion;
                cmd.Parameters.Add("@Sueldo", SqlDbType.Decimal).Value = obj.Sueldo;
                cmd.Parameters["@Sueldo"].Precision = 12;
                cmd.Parameters["@Sueldo"].Scale = 2;
                cmd.Parameters.Add("@Fecha_Ingreso", SqlDbType.Date).Value = obj.Fecha_Ingreso;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Mensaje = "Trabajador registrado exitosamente";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al guardar los datos del trabajador";
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                Mensaje = "No se pudo guardar: la cédula, el correo o el teléfono ya pertenece a otro trabajador.";
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }

        // Actualizar trabajador existente
        public string Actualizar_Trabajador(Entitie_Trabajador obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();

                string duplicado = ObtenerCampoDuplicado(conexion, obj, obj.ID_Trabajador);
                if (!string.IsNullOrEmpty(duplicado))
                {
                    return duplicado;
                }

                SqlCommand cmd = new SqlCommand("Actualizar_Trabajador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Trabajador", SqlDbType.Int).Value = obj.ID_Trabajador;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = obj.Nombre;
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = obj.Apellido;
                cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value = obj.Cedula;
                cmd.Parameters.Add("@Sexo", SqlDbType.VarChar, 10).Value = obj.Sexo;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = obj.Email;
                cmd.Parameters.Add("@Fecha_Nac", SqlDbType.Date).Value = obj.Fecha_Nac;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = obj.Direccion;
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 11).Value = obj.Telefono;
                cmd.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = obj.Ciudad;
                cmd.Parameters.Add("@Sector", SqlDbType.VarChar, 50).Value = obj.Sector;
                cmd.Parameters.Add("@Posicion", SqlDbType.VarChar, 50).Value = obj.Posicion;
                cmd.Parameters.Add("@Sueldo", SqlDbType.Decimal).Value = obj.Sueldo;
                cmd.Parameters["@Sueldo"].Precision = 12;
                cmd.Parameters["@Sueldo"].Scale = 2;
                cmd.Parameters.Add("@Fecha_Ingreso", SqlDbType.Date).Value = obj.Fecha_Ingreso;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Mensaje = "Datos del trabajador actualizados correctamente";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al actualizar los datos del trabajador";
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                Mensaje = "No se pudo actualizar: la cédula, el correo o el teléfono ya pertenece a otro trabajador.";
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }

        // Eliminar trabajador
        public string Eliminar_Trabajador(int idTrabajador)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Eliminar_Trabajador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Trabajador", SqlDbType.Int).Value = idTrabajador;

                object resultado = cmd.ExecuteScalar();
                Mensaje = Convert.ToString(resultado) ?? "Trabajador eliminado correctamente";
            }
            catch (SqlException ex) when (ex.Number == 51020 || ex.Number == 547)
            {
                Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }

        // Obtener trabajador por ID
        public Entitie_Trabajador Obtener_Trabajador(int idTrabajador)
        {
            Entitie_Trabajador trabajador = new Entitie_Trabajador();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Obtener_Trabajador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Trabajador", SqlDbType.Int).Value = idTrabajador;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    trabajador.ID_Trabajador = Convert.ToInt32(reader["ID_Trabajador"]);
                    trabajador.Nombre = reader["Nombre"].ToString() ?? string.Empty;
                    trabajador.Apellido = reader["Apellido"].ToString() ?? string.Empty;
                    trabajador.Cedula = reader["Cedula"].ToString() ?? string.Empty;
                    trabajador.Sexo = reader["Sexo"].ToString() ?? string.Empty;
                    trabajador.Email = reader["Email"].ToString() ?? string.Empty;
                    trabajador.Fecha_Nac = Convert.ToDateTime(reader["Fecha_Nac"]);
                    trabajador.Direccion = reader["Direccion"].ToString() ?? string.Empty;
                    trabajador.Telefono = reader["Telefono"].ToString() ?? string.Empty;
                    trabajador.Ciudad = reader["Ciudad"].ToString() ?? string.Empty;
                    trabajador.Sector = reader["Sector"].ToString() ?? string.Empty;
                    trabajador.Posicion = reader["Posicion"].ToString() ?? string.Empty;
                    trabajador.Sueldo = reader["Sueldo"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(reader["Sueldo"]);
                    trabajador.Fecha_Ingreso = reader["Fecha_Ingreso"] == DBNull.Value
                        ? DateTime.Today
                        : Convert.ToDateTime(reader["Fecha_Ingreso"]);
                }

                return trabajador;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        private string ObtenerCampoDuplicado(
            SqlConnection conexion,
            Entitie_Trabajador trabajador,
            int? idExcluir)
        {
            const string sql = @"
                DECLARE @CedulaNueva VARCHAR(11) = LTRIM(RTRIM(@Cedula));
                DECLARE @EmailNuevo VARCHAR(100) = LOWER(LTRIM(RTRIM(@Email)));
                DECLARE @TelefonoNuevo VARCHAR(11) = LTRIM(RTRIM(@Telefono));
                DECLARE @CedulaActual VARCHAR(11) = NULL;
                DECLARE @EmailActual VARCHAR(100) = NULL;
                DECLARE @TelefonoActual VARCHAR(11) = NULL;

                IF @ID_Excluir IS NOT NULL
                BEGIN
                    SELECT
                        @CedulaActual = LTRIM(RTRIM(Cedula)),
                        @EmailActual = LOWER(LTRIM(RTRIM(Email))),
                        @TelefonoActual = LTRIM(RTRIM(Telefono))
                    FROM Trabajador
                    WHERE ID_Trabajador = @ID_Excluir;
                END;

                SELECT TOP 1
                    CASE
                        WHEN LTRIM(RTRIM(Cedula)) = @CedulaNueva THEN 'cedula'
                        WHEN LOWER(LTRIM(RTRIM(Email))) = @EmailNuevo THEN 'correo'
                        WHEN LTRIM(RTRIM(Telefono)) = @TelefonoNuevo THEN 'telefono'
                    END AS Campo
                FROM Trabajador
                WHERE (@ID_Excluir IS NULL OR ID_Trabajador <> @ID_Excluir)
                  AND (
                        ((@ID_Excluir IS NULL OR @CedulaNueva <> ISNULL(@CedulaActual, ''))
                         AND LTRIM(RTRIM(Cedula)) = @CedulaNueva)
                     OR ((@ID_Excluir IS NULL OR @EmailNuevo <> ISNULL(@EmailActual, ''))
                         AND LOWER(LTRIM(RTRIM(Email))) = @EmailNuevo)
                     OR ((@ID_Excluir IS NULL OR @TelefonoNuevo <> ISNULL(@TelefonoActual, ''))
                         AND LTRIM(RTRIM(Telefono)) = @TelefonoNuevo)
                  );";

            using SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Cedula", SqlDbType.VarChar, 11).Value = trabajador.Cedula;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = trabajador.Email;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 11).Value = trabajador.Telefono;
            cmd.Parameters.Add("@ID_Excluir", SqlDbType.Int).Value =
                idExcluir.HasValue ? idExcluir.Value : DBNull.Value;

            object resultado = cmd.ExecuteScalar();
            string campo = resultado?.ToString() ?? string.Empty;

            return campo switch
            {
                "cedula" => "Ya existe un trabajador registrado con esa cédula.",
                "correo" => "Ya existe un trabajador registrado con ese correo electrónico.",
                "telefono" => "Ya existe un trabajador registrado con ese teléfono.",
                _ => string.Empty
            };
        }
    }
}

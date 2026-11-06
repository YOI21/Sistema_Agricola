using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Insumos
    {
        // Método para insertar un nuevo insumo
        public string InsertarInsumo(Entitie_Insumo insumo)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Insertar_Insumo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = insumo.Nombre;
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = insumo.Tipo;
                cmd.Parameters.Add("@Costo", SqlDbType.Int).Value = insumo.Costo;
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = insumo.Fecha;
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = insumo.Cantidad;

                // Parámetro de salida para obtener el ID generado
                SqlParameter paramOutput = new SqlParameter("@Id_Insumo_Out", SqlDbType.Int);
                paramOutput.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramOutput);

                cmd.ExecuteNonQuery();
                insumo.Id_Insumo = Convert.ToInt32(paramOutput.Value);

                Mensaje = "Insumo registrado con éxito. ID: " + insumo.Id_Insumo;
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar el insumo: " + ex.Message;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return Mensaje;
        }

        // Método para actualizar un insumo existente
        public string ActualizarInsumo(Entitie_Insumo insumo)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Actualizar_Insumo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = insumo.Id_Insumo;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = insumo.Nombre;
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = insumo.Tipo;
                cmd.Parameters.Add("@Costo", SqlDbType.Int).Value = insumo.Costo;
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = insumo.Fecha;
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = insumo.Cantidad;

                cmd.ExecuteNonQuery();

                Mensaje = "Insumo actualizado con éxito.";
            }
            catch (Exception ex)
            {
                Mensaje = "Error al actualizar el insumo: " + ex.Message;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return Mensaje;
        }

        // Método para listar todos los insumos
        public DataTable ListarInsumos()
        {
            DataTable dt = new DataTable();
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Listar_Insumos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar insumos: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }

        // Método para obtener un insumo por ID
        public Entitie_Insumo? ObtenerInsumo(int idInsumo)
        {
            Entitie_Insumo? insumo = null;
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Obtener_Insumo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = idInsumo;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    insumo = new Entitie_Insumo();
                    insumo.Id_Insumo = Convert.ToInt32(reader["Id_Insumo"]);
                    insumo.Nombre = reader["Nombre"].ToString() ?? string.Empty;
                    insumo.Tipo = reader["Tipo"].ToString() ?? string.Empty;
                    insumo.Costo = Convert.ToInt32(reader["Costo"]);
                    insumo.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    insumo.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener insumo: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return insumo;
        }

        // Método para eliminar un insumo
        public string EliminarInsumo(int idInsumo)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Eliminar_Insumo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = idInsumo;
                cmd.ExecuteNonQuery();

                Mensaje = "Insumo eliminado con éxito.";
            }
            catch (Exception ex)
            {
                Mensaje = "Error al eliminar el insumo: " + ex.Message;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return Mensaje;
        }

        // Método para buscar insumos por nombre o tipo
        public DataTable BuscarInsumos(string termino)
        {
            DataTable dt = new DataTable();
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Buscar_Insumo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Termino", SqlDbType.VarChar, 50).Value = termino;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar insumos: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }

        // Método para obtener insumos con bajo stock
        public DataTable ObtenerInsumosBajoStock(int stockMinimo)
        {
            DataTable dt = new DataTable();
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("SELECT Id_Insumo, Nombre, Tipo, Cantidad FROM Insumos WHERE Cantidad < @StockMinimo", conexion);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@StockMinimo", SqlDbType.Int).Value = stockMinimo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener insumos de bajo stock: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return dt;
        }
    }
}

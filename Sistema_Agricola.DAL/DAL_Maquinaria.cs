using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.DAL
{

    #region Ingresar_Maquinaria
    public class DAL_Maquinaria
    {


        public string Ingresar_Maquinaria(Entitie_Maquinaria obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Ingresar_Maquinaria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@nombre_maquina", SqlDbType.VarChar).Value = obj.Nombre_Maquina;
                cmd.Parameters.Add("@tipo_maquina", SqlDbType.VarChar).Value = obj.Tipo_Maquina;
                cmd.Parameters.Add("@costo_maquina", SqlDbType.Money).Value = obj.Costo_Maquina;
                cmd.Parameters.Add("@fecha_adquisicion", SqlDbType.DateTime).Value = obj.Fecha_Adquisicion;
                cmd.Parameters.Add("@Mantenimiento_Programado", SqlDbType.VarChar).Value = obj.Mantenimiento_Programado;

                // Ejecutar el procedimiento y comprobar el resultado directamente
                int filasAfectadas = cmd.ExecuteNonQuery();

                // Si se afectó al menos una fila, la operación fue exitosa
                if (filasAfectadas > 0)
                {
                    Mensaje = "Ha ocurrido un problema al guardar los datos del registro";
                    
                }
                else
                {
                    Mensaje = "Registro de Maquinaria exitoso";
                }
            }
            catch (Exception ex)
            {
                Mensaje = "Error: " + ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }
        #endregion

        #region Consulta De Maquinarias
        private void LogError(string metodo, Exception ex)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt");
                string mensaje = $"{DateTime.Now} - Error en {metodo}: {ex.Message}\n{ex.StackTrace}\n\n";

                File.AppendAllText(logPath, mensaje);
                Console.WriteLine(mensaje);
            }
            catch
            {
                // Si no podemos escribir en el archivo, al menos intentamos loguear en consola
                Console.WriteLine($"Error al registrar log: {ex.Message}");
            }
        }
        //Mostrar por tipo de maquinaria
        public DataTable Tractor()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Tractor";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Pulverizadora()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Pulverizadora";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Cosechadora()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Cosechadora";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable CargadorFrontal()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Cargador Frontal";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Equipodeforraje()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Equipo de forraje";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Labranza()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Labranza";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Sembradora()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Sembradora";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable Desmalezadora()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Reporte_TipoMA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Variable", SqlDbType.VarChar).Value = "Desmalezadora";
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public DataTable BuscarMaquinaria(string buscar)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("BuscarMaquinaria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Buscador", SqlDbType.VarChar).Value = buscar;
                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }



        }


        #endregion
    }
}

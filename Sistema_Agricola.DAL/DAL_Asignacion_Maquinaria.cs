using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Asignacion_Maquinaria
    {
        // Método para asignar maquinaria a un trabajador
        public string Asignar_Maquinaria(Entitie_Asignacion_Maquinaria obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Asignar_Maquinaria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Trabajador", SqlDbType.Int).Value = obj.ID_Trabajador;
                cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = obj.ID_Maquina;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value =
                    (object)obj.Observaciones ?? DBNull.Value;

                // Configurar parámetro de retorno
                SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                // Ejecutar procedimiento
                cmd.ExecuteNonQuery();

                // Verificar el valor de retorno
                int result = (int)returnValue.Value;

                if (result == -1)
                {
                    Mensaje = "La máquina ya no está disponible para asignar.";
                }
                else if (result > 0)
                {
                    obj.ID_Asignacion = result;
                    Mensaje = "Maquinaria asignada exitosamente al operador. ID: " + obj.ID_Asignacion;
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al asignar la maquinaria";
                }
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

        // Método para finalizar una asignación
        public string Finalizar_Asignacion(int idAsignacion)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Finalizar_Asignacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Asignacion", SqlDbType.Int).Value = idAsignacion;

                // Configurar parámetro de retorno
                SqlParameter returnValue = new SqlParameter("@ReturnVal", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                // Ejecutar procedimiento
                cmd.ExecuteNonQuery();

                // Verificar el valor de retorno
                int result = (int)returnValue.Value;

                if (result == -1)
                {
                    Mensaje = "La asignación no existe o ya ha sido finalizada.";
                }
                else if (result == 1)
                {
                    Mensaje = "Asignación finalizada exitosamente. La máquina está disponible nuevamente.";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al finalizar la asignación";
                }
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

        // Método para listar asignaciones activas
        public DataTable Listar_Asignaciones_Activas()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Listar_Asignaciones_Activas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

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

        // Método para listar operadores
        public DataTable Listar_Operadores()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Listar_Operadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

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

        // Método para listar maquinaria disponible (activa)
        public DataTable Listar_Maquinaria_Disponible()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Listar_Maquinaria_Disponible", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

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
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_SeguimientoCrecimiento
    {

        public string Insertar_SeguimientoCrecimiento(Entitie_SeguimientoCrecimiento obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Insertar_SeguimientoCrecimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = obj.ID_Cultivo;
                cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = obj.Fecha_Registro;
                cmd.Parameters.Add("@Etapa_Crecimiento", SqlDbType.VarChar).Value = obj.Etapa_Crecimiento;
                cmd.Parameters.Add("@Altura_Planta", SqlDbType.Decimal).Value = obj.Altura_Planta;
                cmd.Parameters.Add("@Num_Hojas", SqlDbType.Int).Value = obj.Num_Hojas;
                cmd.Parameters.Add("@Estado_Salud", SqlDbType.VarChar).Value = obj.Estado_Salud;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = obj.Observaciones;
                cmd.Parameters.Add("@Porcentaje_Crecimiento", SqlDbType.Decimal).Value = obj.Porcentaje_Crecimiento;
                cmd.Parameters.Add("@Problemas_Detectados", SqlDbType.VarChar).Value = obj.Problemas_Detectados;
                cmd.Parameters.Add("@Acciones_Tomadas", SqlDbType.VarChar).Value = obj.Acciones_Tomadas;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Mensaje = "Registro de seguimiento exitoso";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al guardar los datos del seguimiento";
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

        public DataTable Mostrar_SeguimientoCrecimiento(int idCultivo)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Mostrar_SeguimientoCrecimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
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

        public string Actualizar_SeguimientoCrecimiento(Entitie_SeguimientoCrecimiento obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Actualizar_SeguimientoCrecimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Seguimiento", SqlDbType.Int).Value = obj.ID_Seguimiento;
                cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = obj.Fecha_Registro;
                cmd.Parameters.Add("@Etapa_Crecimiento", SqlDbType.VarChar).Value = obj.Etapa_Crecimiento;
                cmd.Parameters.Add("@Altura_Planta", SqlDbType.Decimal).Value = obj.Altura_Planta;
                cmd.Parameters.Add("@Num_Hojas", SqlDbType.Int).Value = obj.Num_Hojas;
                cmd.Parameters.Add("@Estado_Salud", SqlDbType.VarChar).Value = obj.Estado_Salud;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = obj.Observaciones;
                cmd.Parameters.Add("@Porcentaje_Crecimiento", SqlDbType.Decimal).Value = obj.Porcentaje_Crecimiento;
                cmd.Parameters.Add("@Problemas_Detectados", SqlDbType.VarChar).Value = obj.Problemas_Detectados;
                cmd.Parameters.Add("@Acciones_Tomadas", SqlDbType.VarChar).Value = obj.Acciones_Tomadas;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Mensaje = "Actualización de seguimiento exitosa";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al actualizar los datos del seguimiento";
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

        public string Eliminar_SeguimientoCrecimiento(int idSeguimiento)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Eliminar_SeguimientoCrecimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Seguimiento", SqlDbType.Int).Value = idSeguimiento;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Mensaje = "Eliminación de seguimiento exitosa";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al eliminar el seguimiento";
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

        public DataTable Obtener_Reporte_Crecimiento()
        {
            DataTable tabla = new DataTable();

            try
            {
                using SqlConnection conexion = Conexion.conectardb();
                using SqlCommand verificarTerrenos = new SqlCommand(
                    "SELECT CASE WHEN OBJECT_ID('dbo.Terrenos', 'U') IS NULL THEN 0 ELSE 1 END;",
                    conexion);

                bool existeTablaTerrenos = Convert.ToInt32(verificarTerrenos.ExecuteScalar()) == 1;
                string consulta = existeTablaTerrenos
                    ? @"
                        SELECT
                            sc.ID_Seguimiento,
                            s.ID_Cultivo,
                            CONCAT(s.Nombre_Cultivo, ' #', s.ID_Cultivo) AS Siembra,
                            s.Nombre_Cultivo AS Cultivo,
                            s.Tipo_Cultivo,
                            s.Categoria_Cultivo AS Categoria,
                            ISNULL(t.Nombre_Terreno, 'Sin terreno asignado') AS Terreno,
                            s.Fecha_Plantacion,
                            s.Fecha_Cosecha AS Fecha_Cosecha_Estimada,
                            ISNULL(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
                            sc.Fecha_Registro,
                            sc.Etapa_Crecimiento,
                            sc.Altura_Planta,
                            sc.Num_Hojas,
                            sc.Estado_Salud,
                            sc.Porcentaje_Crecimiento,
                            sc.Problemas_Detectados,
                            sc.Acciones_Tomadas,
                            sc.Observaciones AS Observaciones_Seguimiento
                        FROM dbo.Siembra s
                        LEFT JOIN dbo.SeguimientoCrecimiento sc
                            ON sc.ID_Cultivo = s.ID_Cultivo
                        LEFT JOIN dbo.Terrenos t
                            ON t.ID_Terreno = s.ID_Terreno
                        ORDER BY s.Nombre_Cultivo, s.ID_Cultivo, sc.Fecha_Registro;"
                    : @"
                        SELECT
                            sc.ID_Seguimiento,
                            s.ID_Cultivo,
                            CONCAT(s.Nombre_Cultivo, ' #', s.ID_Cultivo) AS Siembra,
                            s.Nombre_Cultivo AS Cultivo,
                            s.Tipo_Cultivo,
                            s.Categoria_Cultivo AS Categoria,
                            'Sin terreno asignado' AS Terreno,
                            s.Fecha_Plantacion,
                            s.Fecha_Cosecha AS Fecha_Cosecha_Estimada,
                            ISNULL(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
                            sc.Fecha_Registro,
                            sc.Etapa_Crecimiento,
                            sc.Altura_Planta,
                            sc.Num_Hojas,
                            sc.Estado_Salud,
                            sc.Porcentaje_Crecimiento,
                            sc.Problemas_Detectados,
                            sc.Acciones_Tomadas,
                            sc.Observaciones AS Observaciones_Seguimiento
                        FROM dbo.Siembra s
                        LEFT JOIN dbo.SeguimientoCrecimiento sc
                            ON sc.ID_Cultivo = s.ID_Cultivo
                        ORDER BY s.Nombre_Cultivo, s.ID_Cultivo, sc.Fecha_Registro;";

                using SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabla);
                return tabla;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    "No fue posible consultar los datos del reporte de crecimiento. " +
                    "Verifique las tablas Siembra y SeguimientoCrecimiento.", ex);
            }
        }
    }
}

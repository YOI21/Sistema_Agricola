using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Cosecha
    {
        public string Registrar_Cosecha(Entitie_Cosecha obj)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                using SqlCommand cmd = new SqlCommand("Guardar_Cosecha_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = obj.ID_Cultivo;
                cmd.Parameters.Add("@Estado_Cosecha", SqlDbType.VarChar, 25).Value =
                    obj.Estado_Cosecha ?? "Completada";
                AgregarDecimal(cmd, "@Cantidad_Cosechada", obj.Cantidad_Cosechada, 18, 3);
                AgregarDecimal(cmd, "@Rendimiento", obj.Rendimiento, 18, 3);
                AgregarDecimal(cmd, "@Calidad", obj.Calidad, 10, 2);
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value =
                    obj.Observaciones ?? string.Empty;
                cmd.Parameters.Add("@Fecha_Cosecha", SqlDbType.Date).Value = obj.Fecha_Cosecha;
                AgregarDecimal(cmd, "@Costo_Cosecha", obj.Costo_Cosecha, 18, 2);
                AgregarDecimal(cmd, "@Precio_Venta", obj.Precio_Venta, 18, 2);
                AgregarDecimal(cmd, "@Beneficio", obj.Beneficio, 18, 2);

                string accion = Convert.ToString(cmd.ExecuteScalar()) ?? string.Empty;
                return accion == "Actualizada"
                    ? "Datos de cosecha actualizados correctamente"
                    : "La cosecha se ha registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

        }
        // Método para registrar una nueva cosecha

        // Método para listar todas las cosechas
        public DataTable Listar_Cosechas()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT
                        C.ID_Cosecha AS [ID],
                        S.Nombre_Cultivo AS [Cultivo],
                        S.Tipo_Cultivo AS [Tipo],
                        S.Categoria_Cultivo AS [Categoria],
                        C.Estado_Cosecha AS [Estado],
                        ISNULL(C.Cantidad_Cosechada, 0) AS [Cantidad Cosechada],
                        S.Cantidad AS [Cantidad Sembrada],
                        ISNULL(C.Rendimiento, 0) AS [Rendimiento %],
                        ISNULL(C.Calidad, 0) AS [Calidad],
                        ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) AS [Fecha Cosecha],
                        ISNULL(C.Costo_Cosecha, 0) AS [Costo Cosecha],
                        ISNULL(C.Precio_Venta, 0) AS [Precio Venta],
                        ISNULL(C.Beneficio, 0) AS [Beneficio]
                    FROM Cosecha C
                    INNER JOIN Siembra S ON C.id_cultivo = S.ID_Cultivo
                    ORDER BY ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) DESC, C.ID_Cosecha DESC", conexion);

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

        // Método para obtener los detalles de una cosecha específica
        public Entitie_Cosecha Obtener_Cosecha(int idCosecha)
        {
            Entitie_Cosecha cosecha = new Entitie_Cosecha();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        C.ID_Cosecha,
                        C.id_cultivo AS ID_Cultivo,
                        C.Estado_Cosecha,
                        ISNULL(C.Cantidad_Cosechada, 0) AS Cantidad_Cosechada,
                        ISNULL(C.Rendimiento, 0) AS Rendimiento,
                        ISNULL(C.Calidad, 0) AS Calidad,
                        ISNULL(C.Observaciones, '') AS Observaciones,
                        ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) AS Fecha_Cosecha,
                        ISNULL(C.Costo_Cosecha, 0) AS Costo_Cosecha,
                        ISNULL(C.Precio_Venta, 0) AS Precio_Venta,
                        ISNULL(C.Beneficio, 0) AS Beneficio,
                        S.Nombre_Cultivo,
                        S.Tipo_Cultivo,
                        S.Categoria_Cultivo,
                        S.Cantidad AS Cantidad_Sembrada,
                        S.Fecha_Plantacion,
                        ISNULL(S.Costo_Siembra, 0) AS Costo_Siembra
                    FROM Cosecha C
                    INNER JOIN Siembra S ON C.id_cultivo = S.ID_Cultivo
                    WHERE C.ID_Cosecha = @ID_Cosecha", conexion);
                cmd.Parameters.Add("@ID_Cosecha", SqlDbType.Int).Value = idCosecha;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cosecha.ID_Cosecha = Convert.ToInt32(reader["ID_Cosecha"]);
                    cosecha.ID_Cultivo = Convert.ToInt32(reader["ID_Cultivo"]);
                    cosecha.Estado_Cosecha = reader["Estado_Cosecha"].ToString() ?? string.Empty;
                    cosecha.Nombre_Cultivo = reader["Nombre_Cultivo"].ToString() ?? string.Empty;
                    cosecha.Tipo_Cultivo = reader["Tipo_Cultivo"].ToString() ?? string.Empty;
                    cosecha.Categoria_Cultivo = reader["Categoria_Cultivo"].ToString() ?? string.Empty;
                    cosecha.Cantidad_Sembrada = Convert.ToInt32(reader["Cantidad_Sembrada"]);
                    cosecha.Fecha_Plantacion = Convert.ToDateTime(reader["Fecha_Plantacion"]);
                    cosecha.Costo_Siembra = Convert.ToDecimal(reader["Costo_Siembra"]);

                    // Datos específicos de la cosecha
                    if (reader["Cantidad_Cosechada"] != DBNull.Value)
                        cosecha.Cantidad_Cosechada = Convert.ToDecimal(reader["Cantidad_Cosechada"]);

                    if (reader["Rendimiento"] != DBNull.Value)
                        cosecha.Rendimiento = Convert.ToDecimal(reader["Rendimiento"]);

                    if (reader["Calidad"] != DBNull.Value)
                        cosecha.Calidad = Convert.ToDecimal(reader["Calidad"]);

                    if (reader["Observaciones"] != DBNull.Value)
                        cosecha.Observaciones = reader["Observaciones"].ToString() ?? string.Empty;

                    if (reader["Fecha_Cosecha"] != DBNull.Value)
                        cosecha.Fecha_Cosecha = Convert.ToDateTime(reader["Fecha_Cosecha"]);
                    else
                        cosecha.Fecha_Cosecha = DateTime.Now;

                    if (reader["Costo_Cosecha"] != DBNull.Value)
                        cosecha.Costo_Cosecha = Convert.ToDecimal(reader["Costo_Cosecha"]);

                    if (reader["Precio_Venta"] != DBNull.Value)
                        cosecha.Precio_Venta = Convert.ToDecimal(reader["Precio_Venta"]);

                    if (reader["Beneficio"] != DBNull.Value)
                        cosecha.Beneficio = Convert.ToDecimal(reader["Beneficio"]);
                }

                return cosecha;
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

        // Método para eliminar una cosecha
        public string Eliminar_Cosecha(int idCosecha, int idCultivo)
        {
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                SqlCommand cmd = new SqlCommand("Eliminar_Cosecha_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Cosecha", SqlDbType.Int).Value = idCosecha;
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
                return Convert.ToBoolean(cmd.ExecuteScalar())
                    ? "La cosecha se ha eliminado correctamente"
                    : "No se pudo eliminar la cosecha";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

        }

        private static void AgregarDecimal(
            SqlCommand cmd,
            string nombre,
            decimal valor,
            byte precision,
            byte escala)
        {
            SqlParameter parametro = cmd.Parameters.Add(nombre, SqlDbType.Decimal);
            parametro.Precision = precision;
            parametro.Scale = escala;
            parametro.Value = valor;
        }

        // Método para listar cultivos disponibles para cosechar
        public DataTable Listar_Cultivos_Disponibles()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // Esta consulta modificada incluye cultivos que ya fueron marcados como 'Cosechado'
                // pero que aún no tienen registro en la tabla Cosecha
                SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                S.ID_Cultivo,
                S.Nombre_Cultivo,
                S.Tipo_Cultivo,
                S.Categoria_Cultivo,
                S.Cantidad,
                S.Tipo_Suelo,
                S.Fecha_Plantacion,
                S.Fecha_Cosecha,
                S.Costo_Siembra
            FROM Siembra S
            LEFT JOIN Cosecha C ON S.ID_Cultivo = C.id_cultivo
            WHERE C.id_cultivo IS NULL
            ORDER BY S.Fecha_Plantacion DESC", conexion);

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

    }
}

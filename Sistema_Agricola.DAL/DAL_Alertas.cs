using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Alertas
    {
        private const string OrdenAlertas = @"
            ORDER BY
                CASE WHEN A.Estado = 'Pendiente' THEN 0 WHEN A.Estado = 'Completada' THEN 1 ELSE 2 END,
                CASE WHEN DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) < 0 THEN 0 ELSE 1 END,
                A.Fecha_Programada ASC,
                CASE A.Prioridad WHEN 'Alta' THEN 0 WHEN 'Media' THEN 1 ELSE 2 END,
                A.ID_Alerta DESC;";

        private string ObtenerConsultaBaseAlertas(SqlConnection conexion)
        {
            bool puedeResolverAgricultor = ExisteTabla(conexion, "Agricultor_Siembra") &&
                                           ExisteTabla(conexion, "Agricultor");

            string columnasAgricultor = puedeResolverAgricultor
                ? @"
                Responsable.ID_Agricultor,
                COALESCE(Responsable.Agricultor, 'Sin agricultor asignado') AS Agricultor,
                Responsable.Email_Agricultor,"
                : @"
                CAST(NULL AS INT) AS ID_Agricultor,
                CAST('Sin agricultor asignado' AS VARCHAR(120)) AS Agricultor,
                CAST(NULL AS VARCHAR(120)) AS Email_Agricultor,";

            string relacionAgricultor = puedeResolverAgricultor
                ? @"
            OUTER APPLY
            (
                SELECT TOP 1
                    AGR.ID_Agricultor,
                    LTRIM(RTRIM(AGR.Nombre + ' ' + AGR.Apellido)) AS Agricultor,
                    AGR.Email AS Email_Agricultor
                FROM Agricultor_Siembra ASIG
                INNER JOIN Agricultor AGR ON AGR.ID_Agricultor = ASIG.ID_Agricultor
                WHERE ASIG.ID_Cultivo = A.ID_Cultivo
                ORDER BY ASIG.Fecha_Asignacion DESC, ASIG.ID DESC
            ) Responsable"
                : string.Empty;

            return @"
            SELECT
                A.ID_Alerta,
                A.ID_Cultivo,
                COALESCE(S.Nombre_Cultivo, 'Sin cultivo') AS Nombre_Cultivo,"
                + columnasAgricultor + @"
                A.Titulo,
                A.Descripcion,
                A.Fecha_Programada,
                A.Fecha_Creacion,
                A.Tipo_Alerta,
                A.Estado,
                A.Prioridad,
                A.Notificacion_Enviada,
                A.Dias_Anticipacion,
                DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) AS Dias_Restantes,
                CASE
                    WHEN A.Estado <> 'Pendiente' THEN A.Estado
                    WHEN DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) < 0 THEN 'Vencida'
                    WHEN DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) = 0 THEN 'Hoy'
                    WHEN DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) <= A.Dias_Anticipacion THEN 'Proxima'
                    ELSE 'Programada'
                END AS Situacion
            FROM Alertas A
            LEFT JOIN Siembra S ON A.ID_Cultivo = S.ID_Cultivo"
                + relacionAgricultor;
        }

        private bool ExisteTabla(SqlConnection conexion, string nombreTabla)
        {
            using SqlCommand cmd = new SqlCommand(
                "SELECT CASE WHEN OBJECT_ID(@NombreTabla, 'U') IS NULL THEN 0 ELSE 1 END;",
                conexion);
            cmd.Parameters.Add("@NombreTabla", SqlDbType.NVarChar, 128).Value = nombreTabla;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private void AgregarParametrosAlerta(SqlCommand cmd, Entitie_Alerta alerta)
        {
            cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = alerta.ID_Cultivo;
            cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 150).Value = (alerta.Titulo ?? string.Empty).Trim();
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 500).Value = (alerta.Descripcion ?? string.Empty).Trim();
            cmd.Parameters.Add("@Fecha_Programada", SqlDbType.Date).Value = alerta.Fecha_Programada.Date;
            cmd.Parameters.Add("@Fecha_Creacion", SqlDbType.DateTime).Value = alerta.Fecha_Creacion == DateTime.MinValue ? DateTime.Now : alerta.Fecha_Creacion;
            cmd.Parameters.Add("@Tipo_Alerta", SqlDbType.VarChar, 50).Value = (alerta.Tipo_Alerta ?? "Otro").Trim();
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(alerta.Estado) ? "Pendiente" : alerta.Estado.Trim();
            cmd.Parameters.Add("@Prioridad", SqlDbType.VarChar, 10).Value = string.IsNullOrWhiteSpace(alerta.Prioridad) ? "Media" : alerta.Prioridad.Trim();
            cmd.Parameters.Add("@Notificacion_Enviada", SqlDbType.Bit).Value = alerta.Notificacion_Enviada;
            cmd.Parameters.Add("@Dias_Anticipacion", SqlDbType.Int).Value = alerta.Dias_Anticipacion <= 0 ? 7 : alerta.Dias_Anticipacion;
        }

        private DataTable ConsultarAlertas(string condicion, Action<SqlCommand>? configurarParametros = null)
        {
            DataTable dt = new DataTable();
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand(ObtenerConsultaBaseAlertas(conexion) + Environment.NewLine + condicion + Environment.NewLine + OrdenAlertas, conexion);
                configurarParametros?.Invoke(cmd);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar alertas: " + ex.Message);
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

        public int Insertar_Alerta(Entitie_Alerta alerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Insertar_Alerta_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                AgregarParametrosAlerta(cmd, alerta);
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar alerta: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public bool Actualizar_Alerta(Entitie_Alerta alerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Actualizar_Alerta_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@ID_Alerta", SqlDbType.Int).Value = alerta.ID_Alerta;
                AgregarParametrosAlerta(cmd, alerta);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar alerta: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public bool Completar_Alerta(int idAlerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Completar_Alerta_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Alerta", SqlDbType.Int).Value = idAlerta;
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al completar alerta: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public bool Eliminar_Alerta(int idAlerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Eliminar_Alerta_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Alerta", SqlDbType.Int).Value = idAlerta;
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar alerta: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public DataTable Obtener_Todas_Alertas()
        {
            return ConsultarAlertas(string.Empty);
        }

        public DataTable Obtener_Alertas_Por_Cultivo(int idCultivo)
        {
            return ConsultarAlertas("WHERE A.ID_Cultivo = @ID_Cultivo", cmd =>
            {
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
            });
        }

        public DataTable Obtener_Alertas_Pendientes()
        {
            return ConsultarAlertas("WHERE A.Estado = 'Pendiente'");
        }

        public DataTable Obtener_Alertas_Proximas()
        {
            return ConsultarAlertas(@"
                WHERE A.Estado = 'Pendiente'
                  AND A.Notificacion_Enviada = 0
                  AND DATEDIFF(DAY, CAST(GETDATE() AS DATE), A.Fecha_Programada) <= A.Dias_Anticipacion");
        }

        public bool Marcar_Alerta_Notificada(int idAlerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Marcar_Alerta_Notificada_Segura", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Alerta", SqlDbType.Int).Value = idAlerta;
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar alerta como notificada: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public bool Existe_Alerta_Programada(int idCultivo, string titulo, DateTime fechaProgramada, string tipoAlerta)
        {
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(1)
                    FROM Alertas
                    WHERE ID_Cultivo = @ID_Cultivo
                      AND Titulo COLLATE Latin1_General_CI_AI = @Titulo
                      AND Fecha_Programada = @Fecha_Programada
                      AND Tipo_Alerta COLLATE Latin1_General_CI_AI = @Tipo_Alerta
                      AND Estado <> 'Cancelada';", conexion);

                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
                cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 150).Value = titulo ?? string.Empty;
                cmd.Parameters.Add("@Fecha_Programada", SqlDbType.Date).Value = fechaProgramada.Date;
                cmd.Parameters.Add("@Tipo_Alerta", SqlDbType.VarChar, 50).Value = tipoAlerta ?? string.Empty;

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar alerta duplicada: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}

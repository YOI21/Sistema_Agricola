using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Sistema_Agricola.Common;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Siembra
    {
        private bool ExisteTabla(SqlConnection conexion, string nombreTabla)
        {
            SqlCommand cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID(@NombreTabla, 'U') IS NULL THEN 0 ELSE 1 END", conexion);
            cmd.Parameters.Add("@NombreTabla", SqlDbType.VarChar).Value = nombreTabla;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private bool TieneColumna(SqlDataReader reader, string nombreColumna)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> ObtenerTerminosBusqueda(string buscar)
        {
            if (string.IsNullOrWhiteSpace(buscar))
            {
                return new List<string>();
            }

            char[] separadores = { ' ', ',', ';', '.', ':', '-', '/', '\\', '\t', '\r', '\n' };
            return buscar
                .Split(separadores, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => t.Length >= 3)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Take(8)
                .ToList();
        }

        private string LeerTexto(SqlDataReader reader, string columna)
        {
            return TieneColumna(reader, columna) && reader[columna] != DBNull.Value
                ? reader[columna].ToString() ?? string.Empty
                : string.Empty;
        }

        private int LeerEntero(SqlDataReader reader, string columna)
        {
            return TieneColumna(reader, columna) && reader[columna] != DBNull.Value
                ? Convert.ToInt32(reader[columna])
                : 0;
        }

        private decimal LeerDecimal(SqlDataReader reader, string columna)
        {
            return TieneColumna(reader, columna) && reader[columna] != DBNull.Value
                ? Convert.ToDecimal(reader[columna])
                : 0;
        }

        private DateTime LeerFecha(SqlDataReader reader, string columna)
        {
            return TieneColumna(reader, columna) && reader[columna] != DBNull.Value
                ? Convert.ToDateTime(reader[columna])
                : DateTime.Now;
        }

        public Entitie_Siembra Obtener_Cultivo(int idCultivo)
        {
            Entitie_Siembra siembra = new Entitie_Siembra();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                string consulta = ExisteTabla(conexion, "Terrenos")
                    ? @"
                        SELECT
                            s.*,
                            t.Nombre_Terreno
                        FROM Siembra s
                        LEFT JOIN Terrenos t ON t.ID_Terreno = s.ID_Terreno
                        WHERE s.ID_Cultivo = @ID_Cultivo"
                    : @"
                        SELECT
                            s.*,
                            CAST(NULL AS VARCHAR(100)) AS Nombre_Terreno
                        FROM Siembra s
                        WHERE s.ID_Cultivo = @ID_Cultivo";

                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    siembra.ID_Cultivo = LeerEntero(reader, "ID_Cultivo");
                    siembra.Nombre_Cultivo = LeerTexto(reader, "Nombre_Cultivo");
                    siembra.Variedad_Cultivo = LeerTexto(reader, "Variedad_Cultivo");
                    siembra.Tipo_Cultivo = LeerTexto(reader, "Tipo_Cultivo");
                    siembra.Categoria_Cultivo = LeerTexto(reader, "Categoria_Cultivo");
                    siembra.Estacion_Siembra = LeerTexto(reader, "Estacion_Siembra");
                    siembra.Cantidad = LeerEntero(reader, "Cantidad");
                    siembra.Tipo_Suelo = LeerTexto(reader, "Tipo_Suelo");
                    siembra.Fecha_Plantacion = LeerFecha(reader, "Fecha_Plantacion");
                    siembra.Fecha_Cosecha = LeerFecha(reader, "Fecha_Cosecha");
                    siembra.ID_Terreno = LeerEntero(reader, "ID_Terreno");
                    siembra.Nombre_Terreno = LeerTexto(reader, "Nombre_Terreno");
                    siembra.Hectareas_Sembradas = LeerDecimal(reader, "Hectareas_Sembradas");

                    // Propiedades opcionales
                    siembra.Estado_Cultivo = LeerTexto(reader, "Estado_Cultivo");
                    siembra.Metodo_Siembra = LeerTexto(reader, "Metodo_Siembra");
                    siembra.Profundidad = LeerTexto(reader, "Profundidad");
                    siembra.Humedad_Suelo = LeerDecimal(reader, "Humedad_Suelo");
                    siembra.Costo_Siembra = LeerDecimal(reader, "Costo_Siembra");
                }

                return siembra;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "DAL_Siembra.Obtener_Cultivo");
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }
        public DataTable Obtener_Reporte_Siembras()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                // Si existe el procedimiento almacenado, usarlo
                // SqlCommand cmd = new SqlCommand("Obtener_Reporte_Siembras", conexion);
                // cmd.CommandType = CommandType.StoredProcedure;

                // En caso de que no exista el procedimiento, usar una consulta SQL directa
                string consultaReporte = ExisteTabla(conexion, "Terrenos")
                    ? @"
                        SELECT 
                            s.ID_Cultivo,
                            s.Nombre_Cultivo,
                            s.Variedad_Cultivo,
                            s.Tipo_Cultivo,
                            s.Categoria_Cultivo,
                            s.Estacion_Siembra,
                            s.Cantidad,
                            s.Tipo_Suelo,
                            s.Fecha_Plantacion,
                            s.Fecha_Cosecha,
                            COALESCE(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
                            s.Metodo_Siembra,
                            s.Profundidad,
                            s.Humedad_Suelo,
                            s.Costo_Siembra,
                            s.ID_Terreno,
                            t.Nombre_Terreno,
                            s.Hectareas_Sembradas,
                            f.Tipo_Fertilizante,
                            f.Metodo_Fertilizacion,
                            f.Cantidad_Fertilizante,
                            f.Costo_Fertilizacion,
                            f.Fecha_Aplicacion
                        FROM 
                            Siembra s
                        LEFT JOIN 
                            Terrenos t ON t.ID_Terreno = s.ID_Terreno
                        LEFT JOIN 
                            Fertilizacion f ON s.ID_Cultivo = f.ID_Cultivo
                        ORDER BY 
                            s.Fecha_Plantacion DESC"
                    : @"
                        SELECT 
                            s.ID_Cultivo,
                            s.Nombre_Cultivo,
                            s.Variedad_Cultivo,
                            s.Tipo_Cultivo,
                            s.Categoria_Cultivo,
                            s.Estacion_Siembra,
                            s.Cantidad,
                            s.Tipo_Suelo,
                            s.Fecha_Plantacion,
                            s.Fecha_Cosecha,
                            COALESCE(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
                            s.Metodo_Siembra,
                            s.Profundidad,
                            s.Humedad_Suelo,
                            s.Costo_Siembra,
                            s.ID_Terreno,
                            CAST(NULL AS VARCHAR(100)) AS Nombre_Terreno,
                            s.Hectareas_Sembradas,
                            f.Tipo_Fertilizante,
                            f.Metodo_Fertilizacion,
                            f.Cantidad_Fertilizante,
                            f.Costo_Fertilizacion,
                            f.Fecha_Aplicacion
                        FROM 
                            Siembra s
                        LEFT JOIN 
                            Fertilizacion f ON s.ID_Cultivo = f.ID_Cultivo
                        ORDER BY 
                            s.Fecha_Plantacion DESC";

                SqlCommand cmd = new SqlCommand(consultaReporte, conexion);

                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);

                // Agregar columnas calculadas si no existen
                if (!tabla.Columns.Contains("Dias_Transcurridos"))
                {
                    tabla.Columns.Add("Dias_Transcurridos", typeof(int));
                }

                if (!tabla.Columns.Contains("Dias_Restantes"))
                {
                    tabla.Columns.Add("Dias_Restantes", typeof(int));
                }

                if (!tabla.Columns.Contains("Duracion_Total"))
                {
                    tabla.Columns.Add("Duracion_Total", typeof(int));
                }

                // Calcular valores para las columnas adicionales
                foreach (DataRow row in tabla.Rows)
                {
                    DateTime fechaPlantacion = Convert.ToDateTime(row["Fecha_Plantacion"]);
                    DateTime fechaCosecha = Convert.ToDateTime(row["Fecha_Cosecha"]);
                    DateTime fechaActual = DateTime.Now;

                    // Días totales del ciclo
                    int diasTotales = (int)(fechaCosecha - fechaPlantacion).TotalDays;
                    row["Duracion_Total"] = diasTotales;

                    // Días transcurridos desde la plantación
                    int diasTranscurridos = (int)(fechaActual - fechaPlantacion).TotalDays;
                    row["Dias_Transcurridos"] = Math.Max(0, diasTranscurridos);

                    // Días restantes hasta la cosecha
                    int diasRestantes = (int)(fechaCosecha - fechaActual).TotalDays;
                    row["Dias_Restantes"] = Math.Max(0, diasRestantes);

                    // Si la fecha de cosecha ya pasó, actualizar estado si es necesario
                    if (fechaActual > fechaCosecha &&
                        row["Estado_Cultivo"].ToString() != "Cosechado")
                    {
                        row["Estado_Cultivo"] = "Por cosechar";
                    }
                }

                return tabla;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "DAL_Siembra.Obtener_Reporte_Siembras");
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }
        public DataTable Obtener_Estadisticas_Siembras()
        {
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();

                // Consulta para obtener estadísticas básicas
                SqlCommand cmd = new SqlCommand(@"
                    SELECT
                        COUNT(*) AS Total_Siembras,
                        SUM(CASE WHEN Estado_Cultivo = 'Cosechado' THEN 1 ELSE 0 END) AS Total_Cosechados,
                        SUM(CASE WHEN Estado_Cultivo IS NULL OR Estado_Cultivo != 'Cosechado' THEN 1 ELSE 0 END) AS Total_En_Crecimiento,
                        AVG(DATEDIFF(DAY, Fecha_Plantacion, Fecha_Cosecha)) AS Promedio_Dias_Ciclo,
                        SUM(Costo_Siembra) AS Costo_Total_Siembras
                    FROM
                        Siembra
                ", conexion);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tabla);

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

        public DataTable Buscar_Plaga(string buscar)
        {
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                string textoBusqueda = (buscar ?? string.Empty).Trim();
                List<string> terminos = ObtenerTerminosBusqueda(textoBusqueda);

                List<string> partesPuntaje = new List<string>
                {
                    "CASE WHEN @q <> '' AND Nombre_Comun COLLATE Latin1_General_CI_AI = @q THEN 100 ELSE 0 END",
                    "CASE WHEN @q <> '' AND Nombre_Cientifico COLLATE Latin1_General_CI_AI = @q THEN 95 ELSE 0 END",
                    "CASE WHEN @q <> '' AND Nombre_Comun COLLATE Latin1_General_CI_AI LIKE @qLike THEN 70 ELSE 0 END",
                    "CASE WHEN @q <> '' AND Nombre_Cientifico COLLATE Latin1_General_CI_AI LIKE @qLike THEN 65 ELSE 0 END",
                    "CASE WHEN @q <> '' AND Razgos COLLATE Latin1_General_CI_AI LIKE @qLike THEN 55 ELSE 0 END",
                    "CASE WHEN @q <> '' AND CAST(Huesped AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE @qLike THEN 50 ELSE 0 END",
                    "CASE WHEN @q <> '' AND Tipo COLLATE Latin1_General_CI_AI LIKE @qLike THEN 45 ELSE 0 END",
                    "CASE WHEN @q <> '' AND CAST(Cura AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE @qLike THEN 20 ELSE 0 END"
                };

                List<string> filtros = new List<string>
                {
                    "Nombre_Comun COLLATE Latin1_General_CI_AI LIKE @qLike",
                    "Nombre_Cientifico COLLATE Latin1_General_CI_AI LIKE @qLike",
                    "Tipo COLLATE Latin1_General_CI_AI LIKE @qLike",
                    "Razgos COLLATE Latin1_General_CI_AI LIKE @qLike",
                    "CAST(Huesped AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE @qLike",
                    "CAST(Cura AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE @qLike"
                };

                for (int i = 0; i < terminos.Count; i++)
                {
                    string parametro = "@t" + i;
                    partesPuntaje.Add("CASE WHEN Nombre_Comun COLLATE Latin1_General_CI_AI LIKE " + parametro + " THEN 22 ELSE 0 END");
                    partesPuntaje.Add("CASE WHEN Nombre_Cientifico COLLATE Latin1_General_CI_AI LIKE " + parametro + " THEN 20 ELSE 0 END");
                    partesPuntaje.Add("CASE WHEN Razgos COLLATE Latin1_General_CI_AI LIKE " + parametro + " THEN 18 ELSE 0 END");
                    partesPuntaje.Add("CASE WHEN CAST(Huesped AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE " + parametro + " THEN 18 ELSE 0 END");
                    partesPuntaje.Add("CASE WHEN Tipo COLLATE Latin1_General_CI_AI LIKE " + parametro + " THEN 15 ELSE 0 END");

                    filtros.Add("Nombre_Comun COLLATE Latin1_General_CI_AI LIKE " + parametro);
                    filtros.Add("Nombre_Cientifico COLLATE Latin1_General_CI_AI LIKE " + parametro);
                    filtros.Add("Tipo COLLATE Latin1_General_CI_AI LIKE " + parametro);
                    filtros.Add("Razgos COLLATE Latin1_General_CI_AI LIKE " + parametro);
                    filtros.Add("CAST(Huesped AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE " + parametro);
                    filtros.Add("CAST(Cura AS VARCHAR(MAX)) COLLATE Latin1_General_CI_AI LIKE " + parametro);
                }

                string puntaje = string.Join(" + ", partesPuntaje);
                string condicion = string.IsNullOrWhiteSpace(textoBusqueda)
                    ? "1 = 1"
                    : string.Join(" OR ", filtros);

                StringBuilder consulta = new StringBuilder();
                consulta.Append(@"
                    SELECT TOP 50
                        ID_CP,
                        Tipo,
                        Nombre_Comun,
                        Nombre_Cientifico,
                        Razgos,
                        CAST(Huesped AS VARCHAR(MAX)) AS Huesped,
                        CAST(Cura AS VARCHAR(MAX)) AS Cura,
                        CAST(CASE
                            WHEN ");
                consulta.Append(puntaje);
                consulta.Append(@" > 100 THEN 100
                            ELSE ");
                consulta.Append(puntaje);
                consulta.Append(@"
                        END AS INT) AS Coincidencia
                    FROM Enfermedades
                    WHERE ");
                consulta.Append(condicion);
                consulta.Append(@"
                    ORDER BY Coincidencia DESC, Nombre_Comun ASC, Nombre_Cientifico ASC;");

                SqlCommand cmd = new SqlCommand(consulta.ToString(), conexion);
                cmd.Parameters.Add("@q", SqlDbType.VarChar, 200).Value = textoBusqueda;
                cmd.Parameters.Add("@qLike", SqlDbType.VarChar, 220).Value = "%" + textoBusqueda + "%";

                for (int i = 0; i < terminos.Count; i++)
                {
                    cmd.Parameters.Add("@t" + i, SqlDbType.VarChar, 120).Value = "%" + terminos[i] + "%";
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tabla);
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



        public string Insertar_Siembra(
            Entitie_Siembra obj,
            int idAgricultor = 0,
            string notasAsignacion = "")
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Registrar_Siembra_Atomica", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                AgregarParametrosSiembra(cmd, obj, idAgricultor, notasAsignacion);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    obj.ID_Cultivo = Convert.ToInt32(result);
                    Mensaje = "Siembra registrada con éxito. ID: " + obj.ID_Cultivo;
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al guardar los datos de la siembra";
                }
            }
            catch (Exception ex)
            {
                Mensaje = RegistroErrores.ObtenerMensajeSeguro(ex, "DAL_Siembra.Insertar_Siembra");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }
        public DataTable Mostrar_Siembras()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Mostrar_Siembras", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);

                return tabla;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "DAL_Siembra.Mostrar_Siembras");
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public string Actualizar_Siembra(
            Entitie_Siembra obj,
            int idAgricultor = 0,
            string notasAsignacion = "")
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Actualizar_Siembra_Atomica", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = obj.ID_Cultivo;
                AgregarParametrosSiembra(cmd, obj, idAgricultor, notasAsignacion);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value && Convert.ToBoolean(result))
                {
                    Mensaje = "Actualización de siembra exitosa";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al actualizar los datos de la siembra";
                }
            }
            catch (Exception ex)
            {
                Mensaje = RegistroErrores.ObtenerMensajeSeguro(ex, "DAL_Siembra.Actualizar_Siembra");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }

            return Mensaje;
        }

        private static void AgregarParametrosSiembra(
            SqlCommand cmd,
            Entitie_Siembra obj,
            int idAgricultor,
            string notasAsignacion)
        {
            cmd.Parameters.Add("@Nombre_Cultivo", SqlDbType.VarChar, 100).Value = obj.Nombre_Cultivo;
            cmd.Parameters.Add("@Variedad_Cultivo", SqlDbType.VarChar, 80).Value =
                ValorTextoOpcional(obj.Variedad_Cultivo);
            cmd.Parameters.Add("@Tipo_Cultivo", SqlDbType.VarChar, 50).Value = obj.Tipo_Cultivo;
            cmd.Parameters.Add("@Categoria_Cultivo", SqlDbType.VarChar, 50).Value = obj.Categoria_Cultivo;
            cmd.Parameters.Add("@Estacion_Siembra", SqlDbType.VarChar, 50).Value = obj.Estacion_Siembra;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = obj.Cantidad;
            cmd.Parameters.Add("@Tipo_Suelo", SqlDbType.VarChar, 50).Value = obj.Tipo_Suelo;
            cmd.Parameters.Add("@Fecha_Plantacion", SqlDbType.Date).Value = obj.Fecha_Plantacion.Date;
            cmd.Parameters.Add("@Fecha_Cosecha", SqlDbType.Date).Value = obj.Fecha_Cosecha.Date;
            cmd.Parameters.Add("@ID_Terreno", SqlDbType.Int).Value = obj.ID_Terreno;

            AgregarDecimal(cmd, "@Hectareas_Sembradas", obj.Hectareas_Sembradas, false);
            cmd.Parameters.Add("@Metodo_Siembra", SqlDbType.VarChar, 50).Value =
                ValorTextoOpcional(obj.Metodo_Siembra);
            cmd.Parameters.Add("@Profundidad", SqlDbType.VarChar, 50).Value =
                ValorTextoOpcional(obj.Profundidad);
            AgregarDecimal(cmd, "@Humedad_Suelo", obj.Humedad_Suelo, true);
            AgregarDecimal(cmd, "@Costo_Siembra", obj.Costo_Siembra, true);

            cmd.Parameters.Add("@Tipo_Fertilizante", SqlDbType.VarChar, 50).Value =
                ValorTextoOpcional(obj.Tipo_Fertilizante);
            cmd.Parameters.Add("@Metodo_Fertilizacion", SqlDbType.VarChar, 50).Value =
                ValorTextoOpcional(obj.Metodo_Fertilizacion);
            cmd.Parameters.Add("@Cantidad_Fertilizante", SqlDbType.Int).Value =
                obj.Cantidad_Fertilizante > 0 ? obj.Cantidad_Fertilizante : DBNull.Value;
            AgregarDecimal(cmd, "@Costo_Fertilizacion", obj.Costo_Fertilizacion, true);
            cmd.Parameters.Add("@Fecha_Aplicacion", SqlDbType.Date).Value =
                obj.Fecha_Aplicacion == default ? DBNull.Value : obj.Fecha_Aplicacion.Date;

            cmd.Parameters.Add("@ID_Agricultor", SqlDbType.Int).Value =
                idAgricultor > 0 ? idAgricultor : DBNull.Value;
            cmd.Parameters.Add("@Notas", SqlDbType.VarChar, 500).Value =
                ValorTextoOpcional(notasAsignacion);
        }

        private static void AgregarDecimal(
            SqlCommand cmd,
            string nombre,
            decimal valor,
            bool opcional)
        {
            SqlParameter parametro = cmd.Parameters.Add(nombre, SqlDbType.Decimal);
            parametro.Precision = 10;
            parametro.Scale = 2;
            parametro.Value = opcional && valor == 0 ? DBNull.Value : valor;
        }

        private static object ValorTextoOpcional(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? DBNull.Value : valor.Trim();
        }

        public decimal Obtener_Hectareas_Ocupadas_Terreno(int idTerreno, int idCultivoExcluir = 0)
        {
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(ISNULL(Hectareas_Sembradas, 0)), 0)
                    FROM Siembra
                    WHERE ID_Terreno = @ID_Terreno
                      AND ID_Cultivo <> @ID_Cultivo_Excluir
                      AND (
                            Estado_Cultivo IS NULL
                            OR Estado_Cultivo NOT IN ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado')
                          );", conexion);

                cmd.Parameters.Add("@ID_Terreno", SqlDbType.Int).Value = idTerreno;
                cmd.Parameters.Add("@ID_Cultivo_Excluir", SqlDbType.Int).Value = idCultivoExcluir;

                object resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value ? Convert.ToDecimal(resultado) : 0;
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

        public string Eliminar_Siembra(int idCultivo)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Eliminar_Siembra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;

                cmd.ExecuteNonQuery();
                Mensaje = "Eliminación de siembra exitosa";
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

        public string Cambiar_Estado_Cultivo(int idCultivo, string nuevoEstado)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Cambiar_Estado_Cultivo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
                cmd.Parameters.Add("@Estado_Cultivo", SqlDbType.VarChar).Value = nuevoEstado;

                cmd.ExecuteNonQuery();
                Mensaje = "Estado del cultivo actualizado a: " + nuevoEstado;
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

        public string Finalizar_Cultivo_Atomico(int idCultivo)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Finalizar_Siembra_Atomica", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@ID_Cultivo", SqlDbType.Int).Value = idCultivo;
                object resultado = cmd.ExecuteScalar();
                Mensaje = resultado != null && resultado != DBNull.Value && Convert.ToBoolean(resultado)
                    ? "Cultivo finalizado y pasado a cosecha exitosamente."
                    : "No fue posible finalizar el cultivo.";
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

        public DataTable Mostrar_Cultivos()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion = Conexion.conectardb();

                // Usamos CommandType.StoredProcedure para llamar al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("Mostrar_Cultivos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                resultado = cmd.ExecuteReader();
                tabla.Load(resultado);

                // Verificar las columnas que se devuelven (solo para depuración)
                string columnas = "Columnas en el resultado: ";
                foreach (DataColumn col in tabla.Columns)
                {
                    columnas += col.ColumnName + ", ";
                }
                Console.WriteLine(columnas);

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en DAL_Siembra.Mostrar_Cultivos: " + ex.Message);
                throw;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
        }

    }
}

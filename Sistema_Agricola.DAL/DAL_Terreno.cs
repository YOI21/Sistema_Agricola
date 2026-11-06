using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Common;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Terreno
    {
        private string LeerTexto(SqlDataReader reader, string columna)
        {
            return reader[columna] != DBNull.Value
                ? reader[columna].ToString() ?? string.Empty
                : string.Empty;
        }

        private decimal LeerDecimal(SqlDataReader reader, string columna)
        {
            return reader[columna] != DBNull.Value ? Convert.ToDecimal(reader[columna]) : 0;
        }

        private DateTime LeerFecha(SqlDataReader reader, string columna)
        {
            return reader[columna] != DBNull.Value ? Convert.ToDateTime(reader[columna]) : DateTime.Now;
        }

        private bool LeerBooleano(SqlDataReader reader, string columna)
        {
            return reader[columna] != DBNull.Value && Convert.ToBoolean(reader[columna]);
        }

        private static object TextoOpcional(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? DBNull.Value : valor.Trim();
        }

        // Método para insertar un nuevo terreno
        public string Insertar_Terreno(Entitie_Terreno terreno)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();

                SqlCommand cmd = new SqlCommand("Insertar_Terreno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = terreno.Nombre;
                cmd.Parameters.Add("@Area", SqlDbType.Decimal).Value = terreno.Area;
                cmd.Parameters.Add("@UnidadMedida", SqlDbType.VarChar, 20).Value = terreno.UnidadMedida;
                cmd.Parameters.Add("@TipoSuelo", SqlDbType.VarChar, 50).Value = terreno.TipoSuelo;
                cmd.Parameters.Add("@pH", SqlDbType.Decimal).Value = terreno.pH;
                cmd.Parameters.Add("@Pendiente", SqlDbType.Decimal).Value = terreno.Pendiente;
                cmd.Parameters.Add("@Humedad", SqlDbType.Decimal).Value = terreno.Humedad;
                cmd.Parameters.Add("@Drenaje", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.Drenaje);
                cmd.Parameters.Add("@FuenteAgua", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.FuenteAgua);
                cmd.Parameters.Add("@Provincia", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.Provincia);
                cmd.Parameters.Add("@ZonaAgroclimatica", SqlDbType.VarChar, 80).Value = TextoOpcional(terreno.ZonaAgroclimatica);
                cmd.Parameters.Add("@PrecipitacionAnual", SqlDbType.Decimal).Value =
                    terreno.PrecipitacionAnual > 0 ? terreno.PrecipitacionAnual : DBNull.Value;
                cmd.Parameters.Add("@DisponibilidadRiego", SqlDbType.Bit).Value = terreno.DisponibilidadRiego;
                cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 200).Value = terreno.Ubicacion;
                cmd.Parameters.Add("@Propietario", SqlDbType.VarChar, 100).Value = terreno.Propietario;
                cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = terreno.FechaRegistro;
                cmd.Parameters.Add("@EstadoActual", SqlDbType.VarChar, 20).Value = terreno.EstadoActual;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 500).Value = terreno.Observaciones ?? "";

                // Parámetro de salida para obtener el ID generado
                SqlParameter paramOutput = new SqlParameter("@ID_Terreno_Out", SqlDbType.Int);
                paramOutput.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramOutput);

                cmd.ExecuteNonQuery();

                terreno.ID_Terreno = Convert.ToInt32(paramOutput.Value);
                Mensaje = "Terreno registrado con éxito. ID: " + terreno.ID_Terreno;
            }
            catch (Exception ex)
            {
                Mensaje = RegistroErrores.ObtenerMensajeSeguro(ex, "DAL_Terreno.Insertar_Terreno");
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

        // Método para actualizar un terreno existente
        public string Actualizar_Terreno(Entitie_Terreno terreno)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();

                SqlCommand cmd = new SqlCommand("Actualizar_Terreno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_Terreno", SqlDbType.Int).Value = terreno.ID_Terreno;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = terreno.Nombre;
                cmd.Parameters.Add("@Area", SqlDbType.Decimal).Value = terreno.Area;
                cmd.Parameters.Add("@UnidadMedida", SqlDbType.VarChar, 20).Value = terreno.UnidadMedida;
                cmd.Parameters.Add("@TipoSuelo", SqlDbType.VarChar, 50).Value = terreno.TipoSuelo;
                cmd.Parameters.Add("@pH", SqlDbType.Decimal).Value = terreno.pH;
                cmd.Parameters.Add("@Pendiente", SqlDbType.Decimal).Value = terreno.Pendiente;
                cmd.Parameters.Add("@Humedad", SqlDbType.Decimal).Value = terreno.Humedad;
                cmd.Parameters.Add("@Drenaje", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.Drenaje);
                cmd.Parameters.Add("@FuenteAgua", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.FuenteAgua);
                cmd.Parameters.Add("@Provincia", SqlDbType.VarChar, 50).Value = TextoOpcional(terreno.Provincia);
                cmd.Parameters.Add("@ZonaAgroclimatica", SqlDbType.VarChar, 80).Value = TextoOpcional(terreno.ZonaAgroclimatica);
                cmd.Parameters.Add("@PrecipitacionAnual", SqlDbType.Decimal).Value =
                    terreno.PrecipitacionAnual > 0 ? terreno.PrecipitacionAnual : DBNull.Value;
                cmd.Parameters.Add("@DisponibilidadRiego", SqlDbType.Bit).Value = terreno.DisponibilidadRiego;
                cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 200).Value = terreno.Ubicacion;
                cmd.Parameters.Add("@Propietario", SqlDbType.VarChar, 100).Value = terreno.Propietario;
                cmd.Parameters.Add("@EstadoActual", SqlDbType.VarChar, 20).Value = terreno.EstadoActual;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 500).Value = terreno.Observaciones ?? "";

                cmd.ExecuteNonQuery();

                Mensaje = "Terreno actualizado con éxito.";
            }
            catch (Exception ex)
            {
                Mensaje = RegistroErrores.ObtenerMensajeSeguro(ex, "DAL_Terreno.Actualizar_Terreno");
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

        // Método para listar todos los terrenos
        public DataTable Listar_Terrenos()
        {
            DataTable dt = new DataTable();
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT
                        ID_Terreno AS ID,
                        ID_Terreno,
                        Nombre_Terreno AS Nombre,
                        Superficie_Total AS Area,
                        Unidad_Medida AS UnidadMedida,
                        Tipo_Suelo AS TipoSuelo,
                        pH_Suelo AS pH,
                        TRY_CONVERT(DECIMAL(10, 2), Pendiente) AS Pendiente,
                        ISNULL(Humedad, 0) AS Humedad,
                        ISNULL(Drenaje, '') AS Drenaje,
                        ISNULL(Fuente_Agua, '') AS FuenteAgua,
                        ISNULL(Provincia, '') AS Provincia,
                        ISNULL(Zona_Agroclimatica, '') AS ZonaAgroclimatica,
                        ISNULL(Precipitacion_Anual, 0) AS PrecipitacionAnual,
                        ISNULL(Disponibilidad_Riego, 0) AS DisponibilidadRiego,
                        Ubicacion,
                        ISNULL(Propietario, '') AS Propietario,
                        Fecha_Registro AS FechaRegistro,
                        Estado AS EstadoActual,
                        ISNULL(Observaciones, '') AS Observaciones
                    FROM Terrenos
                    ORDER BY Nombre_Terreno ASC;", conexion);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "DAL_Terreno.Listar_Terrenos");
                throw;
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

        // Método para obtener un terreno por ID
        public Entitie_Terreno? Obtener_Terreno(int ID_Terreno)
        {
            Entitie_Terreno? terreno = null;
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT
                        ID_Terreno,
                        Nombre_Terreno AS Nombre,
                        Superficie_Total AS Area,
                        Unidad_Medida AS UnidadMedida,
                        Tipo_Suelo AS TipoSuelo,
                        pH_Suelo AS pH,
                        TRY_CONVERT(DECIMAL(10, 2), Pendiente) AS Pendiente,
                        ISNULL(Humedad, 0) AS Humedad,
                        ISNULL(Drenaje, '') AS Drenaje,
                        ISNULL(Fuente_Agua, '') AS FuenteAgua,
                        ISNULL(Provincia, '') AS Provincia,
                        ISNULL(Zona_Agroclimatica, '') AS ZonaAgroclimatica,
                        ISNULL(Precipitacion_Anual, 0) AS PrecipitacionAnual,
                        ISNULL(Disponibilidad_Riego, 0) AS DisponibilidadRiego,
                        Ubicacion,
                        ISNULL(Propietario, '') AS Propietario,
                        Fecha_Registro AS FechaRegistro,
                        Estado AS EstadoActual,
                        ISNULL(Observaciones, '') AS Observaciones
                    FROM Terrenos
                    WHERE ID_Terreno = @ID_Terreno;", conexion);
                cmd.Parameters.Add("@ID_Terreno", SqlDbType.Int).Value = ID_Terreno;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    terreno = new Entitie_Terreno();
                    terreno.ID_Terreno = Convert.ToInt32(reader["ID_Terreno"]);
                    terreno.Nombre = LeerTexto(reader, "Nombre");
                    terreno.Area = LeerDecimal(reader, "Area");
                    terreno.UnidadMedida = LeerTexto(reader, "UnidadMedida");
                    terreno.TipoSuelo = LeerTexto(reader, "TipoSuelo");
                    terreno.pH = LeerDecimal(reader, "pH");
                    terreno.Pendiente = LeerDecimal(reader, "Pendiente");
                    terreno.Humedad = LeerDecimal(reader, "Humedad");
                    terreno.Drenaje = LeerTexto(reader, "Drenaje");
                    terreno.FuenteAgua = LeerTexto(reader, "FuenteAgua");
                    terreno.Provincia = LeerTexto(reader, "Provincia");
                    terreno.ZonaAgroclimatica = LeerTexto(reader, "ZonaAgroclimatica");
                    terreno.PrecipitacionAnual = LeerDecimal(reader, "PrecipitacionAnual");
                    terreno.DisponibilidadRiego = LeerBooleano(reader, "DisponibilidadRiego");
                    terreno.Ubicacion = LeerTexto(reader, "Ubicacion");
                    terreno.Propietario = LeerTexto(reader, "Propietario");
                    terreno.FechaRegistro = LeerFecha(reader, "FechaRegistro");
                    terreno.EstadoActual = LeerTexto(reader, "EstadoActual");
                    terreno.Observaciones = LeerTexto(reader, "Observaciones");
                }
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "DAL_Terreno.Obtener_Terreno");
                throw;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return terreno;
        }

        // Método para eliminar un terreno
        public string Eliminar_Terreno(int ID_Terreno)
        {
            string Mensaje = "";
            SqlConnection? conexion = null;

            try
            {
                conexion = Conexion.conectardb();

                SqlCommand cmd = new SqlCommand("Eliminar_Terreno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Terreno", SqlDbType.Int).Value = ID_Terreno;

                cmd.ExecuteNonQuery();

                Mensaje = "Terreno eliminado con éxito.";
            }
            catch (Exception ex)
            {
                Mensaje = RegistroErrores.ObtenerMensajeSeguro(ex, "DAL_Terreno.Eliminar_Terreno");
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

        // Método para validar la compatibilidad del cultivo con el terreno
        public bool ValidarCompatibilidadCultivo(int ID_Terreno, string nombreCultivo, string tipoCultivo, string categoriaCultivo)
        {
            bool esCompatible = false;
            try
            {
                // Obtener los datos del terreno
                Entitie_Terreno? terreno = Obtener_Terreno(ID_Terreno);

                if (terreno == null)
                    return false;

                // Aquí implementamos la lógica de validación según tipo de suelo y cultivo
                switch (terreno.TipoSuelo.ToLower())
                {
                    case "arcilloso":
                        // Cultivos compatibles con suelo arcilloso
                        if (nombreCultivo.ToLower() == "arroz" ||
                            nombreCultivo.ToLower() == "maíz" ||
                            nombreCultivo.ToLower() == "trigo" ||
                            nombreCultivo.ToLower() == "caña de azúcar")
                        {
                            esCompatible = true;
                        }
                        break;

                    case "arenoso":
                        // Cultivos compatibles con suelo arenoso
                        if (nombreCultivo.ToLower() == "zanahoria" ||
                            nombreCultivo.ToLower() == "patata" ||
                            nombreCultivo.ToLower() == "yuca" ||
                            nombreCultivo.ToLower() == "piña" ||
                            nombreCultivo.ToLower() == "batata")
                        {
                            esCompatible = true;
                        }
                        break;

                    case "franco":
                        // Suelo franco es versátil y compatible con muchos cultivos
                        if (nombreCultivo.ToLower() == "tomate" ||
                            nombreCultivo.ToLower() == "pimiento" ||
                            nombreCultivo.ToLower() == "lechuga" ||
                            nombreCultivo.ToLower() == "café" ||
                            nombreCultivo.ToLower() == "cacao" ||
                            nombreCultivo.ToLower() == "plátano" ||
                            nombreCultivo.ToLower() == "aguacate")
                        {
                            esCompatible = true;
                        }
                        break;

                    case "limoso":
                        // Cultivos compatibles con suelo limoso
                        if (nombreCultivo.ToLower() == "maíz" ||
                            nombreCultivo.ToLower() == "soja" ||
                            nombreCultivo.ToLower() == "trigo" ||
                            nombreCultivo.ToLower() == "cebada")
                        {
                            esCompatible = true;
                        }
                        break;
                }

                // Validación adicional por tamaño del terreno
                decimal areaMinima = 0;

                switch (nombreCultivo.ToLower())
                {
                    case "arroz":
                        areaMinima = 1.0m; // 1 hectárea mínimo
                        break;
                    case "maíz":
                        areaMinima = 0.5m; // 0.5 hectáreas mínimo
                        break;
                    case "café":
                        areaMinima = 0.25m; // 0.25 hectáreas mínimo
                        break;
                    // Añadir más cultivos según sea necesario
                    default:
                        areaMinima = 0.1m; // 0.1 hectáreas por defecto
                        break;
                }

                // Convertir área a hectáreas si es necesario
                decimal areaEnHectareas = terreno.Area;
                if (terreno.UnidadMedida.ToLower() == "metros cuadrados" || terreno.UnidadMedida.ToLower() == "m²")
                {
                    areaEnHectareas = terreno.Area / 10000; // 1 hectárea = 10,000 m²
                }

                // Verificar si el área es suficiente
                if (areaEnHectareas < areaMinima)
                {
                    esCompatible = false;
                }

                // También podemos incluir validaciones por pH, humedad, pendiente, etc.
                // ...
            }
            catch (Exception)
            {
                esCompatible = false;
            }

            return esCompatible;
        }
    }
}

using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Siembra
    {
        // Asegúrate de que la clase BLL_Siembra tenga este método implementado correctamente
        public static Entitie_Siembra? Obtener_Cultivo(int idCultivo)
        {
            try
            {
                DAL_Siembra dal = new DAL_Siembra();
                Entitie_Siembra cultivo = dal.Obtener_Cultivo(idCultivo);

                // Verificar que el cultivo no sea nulo
                if (cultivo == null || cultivo.ID_Cultivo <= 0)
                {
                    Console.WriteLine("No se encontró el cultivo con ID: " + idCultivo);
                    return null;
                }

                return cultivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL_Siembra.Obtener_Cultivo: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }
        }

        public static DataTable Obtener_Reporte_Siembras()
        {
            try
            {
                DAL_Siembra dal = new DAL_Siembra();
                DataTable dt = dal.Obtener_Reporte_Siembras();

                // Verificar que el DataTable  las columnas necesarias
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    //  agregar columnas calculadas
                    if (!dt.Columns.Contains("Progreso"))
                    {
                        dt.Columns.Add("Progreso", typeof(double));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime fechaPlantacion = Convert.ToDateTime(row["Fecha_Plantacion"]);
                            DateTime fechaCosecha = Convert.ToDateTime(row["Fecha_Cosecha"]);
                            DateTime fechaActual = DateTime.Now;

                            // Calcular días transcurridos y totales
                            double diasTotales = (fechaCosecha - fechaPlantacion).TotalDays;
                            double diasTranscurridos = (fechaActual - fechaPlantacion).TotalDays;

                            // Calcular porcentaje de progreso
                            double progreso = 0;
                            if (diasTotales > 0)
                            {
                                progreso = Math.Min(100, Math.Max(0, (diasTranscurridos / diasTotales) * 100));
                            }

                            row["Progreso"] = Math.Round(progreso, 2);
                        }
                    }
                }

                return dt ?? new DataTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL_Siembra.Obtener_Reporte_Siembras: " + ex.Message);
                throw;
            }
        }
        public static DataTable Obtener_Estadisticas_Siembras()
        {
            try
            {
                DAL_Siembra dal = new DAL_Siembra();
                return dal.Obtener_Estadisticas_Siembras();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL_Siembra.Obtener_Estadisticas_Siembras: " + ex.Message);
                throw;
            }
        }
        public static DataTable Buscar_P(string valor)
        {
            DAL_Siembra dn = new DAL_Siembra();
            return dn.Buscar_Plaga(valor);
        }

        public static string Insertar_Siembra_Corregido(string nombreCultivo, string tipoCultivo,
string categoriaCultivo, string estacionSiembra, int cantidad, string tipoSuelo,
DateTime fechaPlantacion, DateTime fechaCosecha, string metodoSiembra = "",
string profundidad = "", decimal humedadSuelo = 0, decimal costoSiembra = 0,
int idTerreno = 0, decimal hectareasSembradas = 0, int idAgricultor = 0,
string variedadCultivo = "")
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SiembrasCrear,
                "Siembra",
                "Crear",
                $"Cultivo={nombreCultivo}; Terreno={idTerreno}; Hectareas={hectareasSembradas}",
                () =>
                {
                    DAL_Siembra dal = new DAL_Siembra();
                    Entitie_Siembra obj = new Entitie_Siembra
                    {
                        Nombre_Cultivo = nombreCultivo,
                        Variedad_Cultivo = variedadCultivo,
                        Tipo_Cultivo = tipoCultivo,
                        Categoria_Cultivo = categoriaCultivo,
                        Estacion_Siembra = estacionSiembra,
                        Cantidad = cantidad,
                        Tipo_Suelo = tipoSuelo,
                        Fecha_Plantacion = fechaPlantacion,
                        Fecha_Cosecha = fechaCosecha,
                        Metodo_Siembra = metodoSiembra,
                        Profundidad = profundidad,
                        Humedad_Suelo = humedadSuelo,
                        Costo_Siembra = costoSiembra,
                        ID_Terreno = idTerreno,
                        Hectareas_Sembradas = hectareasSembradas
                    };

                    return dal.Insertar_Siembra(
                        obj,
                        idAgricultor,
                        "Asignado automaticamente al registrar la siembra.");
                },
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static string Actualizar_Siembra(int idCultivo, string nombreCultivo, string tipoCultivo,
string categoriaCultivo, string estacionSiembra, int cantidad, string tipoSuelo,
DateTime fechaPlantacion, DateTime fechaCosecha, string metodoSiembra = "",
string profundidad = "", decimal humedadSuelo = 0, decimal costoSiembra = 0,
int idTerreno = 0, decimal hectareasSembradas = 0, int idAgricultor = 0,
string variedadCultivo = "")
        {
            if (idCultivo <= 0)
            {
                return "Error: debe seleccionar una siembra válida para actualizar.";
            }

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SiembrasActualizar,
                "Siembra",
                "Actualizar",
                $"ID={idCultivo}; Cultivo={nombreCultivo}; Terreno={idTerreno}; Hectareas={hectareasSembradas}",
                () =>
                {
                    DAL_Siembra dal = new DAL_Siembra();
                    Entitie_Siembra obj = new Entitie_Siembra
                    {
                        ID_Cultivo = idCultivo,
                        Nombre_Cultivo = nombreCultivo,
                        Variedad_Cultivo = variedadCultivo,
                        Tipo_Cultivo = tipoCultivo,
                        Categoria_Cultivo = categoriaCultivo,
                        Estacion_Siembra = estacionSiembra,
                        Cantidad = cantidad,
                        Tipo_Suelo = tipoSuelo,
                        Fecha_Plantacion = fechaPlantacion,
                        Fecha_Cosecha = fechaCosecha,
                        Metodo_Siembra = metodoSiembra,
                        Profundidad = profundidad,
                        Humedad_Suelo = humedadSuelo,
                        Costo_Siembra = costoSiembra,
                        ID_Terreno = idTerreno,
                        Hectareas_Sembradas = hectareasSembradas
                    };

                    return dal.Actualizar_Siembra(
                        obj,
                        idAgricultor,
                        "Asignado automaticamente al actualizar la siembra.");
                },
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static decimal Obtener_Hectareas_Ocupadas_Terreno(int idTerreno, int idCultivoExcluir = 0)
        {
            DAL_Siembra dal = new DAL_Siembra();
            return dal.Obtener_Hectareas_Ocupadas_Terreno(idTerreno, idCultivoExcluir);
        }

        public static string Finalizar_Cultivo(int idCultivo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SiembrasFinalizar,
                "Siembra",
                "Finalizar",
                $"ID={idCultivo}",
                () => new DAL_Siembra().Finalizar_Cultivo_Atomico(idCultivo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static string Eliminar_Siembra(int idCultivo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SiembrasEliminar,
                "Siembra",
                "Eliminar",
                $"ID={idCultivo}",
                () => new DAL_Siembra().Eliminar_Siembra(idCultivo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static string Anular_Siembra(int idCultivo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SiembrasAnular,
                "Siembra",
                "Anular",
                $"ID={idCultivo}",
                () => new DAL_Siembra().Cambiar_Estado_Cultivo(idCultivo, "Cancelado"),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }
        public static DataTable Mostrar_Cultivos()
        {
            try
            {
                DAL_Siembra dal = new DAL_Siembra();
                // Este método debe llamar al procedimiento almacenado actualizado
                DataTable dt = dal.Mostrar_Cultivos();

                // Verificar que el DataTable tenga al menos las columnas necesarias
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Opcionalmente, podemos verificar si tiene las columnas esenciales
                    string[] columnasEsenciales = {
                        "ID_Cultivo", "Cultivos", "Fecha de Cosecha"
                    };

                    foreach (string columna in columnasEsenciales)
                    {
                        if (!dt.Columns.Contains(columna))
                        {
                            Console.WriteLine($"Advertencia: El DataTable no contiene la columna '{columna}'");
                        }
                    }
                }

                return dt ?? new DataTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL_Siembra.Mostrar_Cultivos: " + ex.Message);
                throw;
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    /// <summary>
    /// Clase auxiliar para el Dashboard con métodos de utilidad y exportación
    /// </summary>
    public static class DashboardHelper
    {
        /// <summary>
        /// Genera un resumen de las estadísticas del sistema
        /// </summary>
        public static Entitie_Dashboard GenerarResumenEstadisticas()
        {
            return BLL_Dashboard.ObtenerEstadisticas();
        }

        /// <summary>
        /// Obtiene los datos de cultivos y los filtra para mostrar solo los activos
        /// </summary>
        public static DataTable ObtenerCultivosActivos()
        {
            DataTable dtCultivos = BLL_Siembra.Mostrar_Cultivos();

            if (dtCultivos != null && dtCultivos.Rows.Count > 0)
            {
                // Filtrar solo cultivos activos (fecha de cosecha mayor a hoy)
                DataView dv = dtCultivos.DefaultView;
                dv.RowFilter = "Convert([Fecha de Cosecha], 'System.DateTime') > #" +
                              DateTime.Now.ToString("yyyy-MM-dd") + "#";
                return dv.ToTable();
            }

            return new DataTable();
        }

        /// <summary>
        /// Obtiene las cosechas próximas (en los siguientes días)
        /// </summary>
        public static DataTable ObtenerProximasCosechas(DataTable dtCultivos, int dias = 30)
        {
            if (dtCultivos != null && dtCultivos.Rows.Count > 0)
            {
                DataView dv = dtCultivos.DefaultView;
                DateTime fechaLimite = DateTime.Now.AddDays(dias);

                dv.RowFilter = "Convert([Fecha de Cosecha], 'System.DateTime') <= #" +
                              fechaLimite.ToString("yyyy-MM-dd") + "# AND " +
                              "Convert([Fecha de Cosecha], 'System.DateTime') >= #" +
                              DateTime.Now.ToString("yyyy-MM-dd") + "#";

                return dv.ToTable();
            }

            return new DataTable();
        }

        /// <summary>
        /// Obtiene el último seguimiento realizado para un cultivo
        /// </summary>
        public static DataRow? ObtenerUltimoSeguimiento(int idCultivo)
        {
            DataTable dt = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Ordenar por fecha descendente
                DataView dv = dt.DefaultView;
                dv.Sort = "Fecha_Registro DESC";
                DataTable dtOrdenado = dv.ToTable();

                // Devolver el primer registro (el más reciente)
                return dtOrdenado.Rows[0];
            }

            return null;
        }

        /// <summary>
        /// Obtiene las alertas pendientes
        /// </summary>
        public static DataTable ObtenerAlertasPendientes()
        {
            try
            {
                return BLL_Alertas.ObtenerAlertasPendientes();
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Genera un informe de rendimientos de los cultivos
        /// </summary>
        public static DataTable GenerarInformeRendimientos()
        {
            DataTable dt = BLL_Cosecha.Listar_Cosechas();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Ordenar por rendimiento
                DataView dv = dt.DefaultView;
                dv.Sort = "[Rendimiento %] DESC";
                return dv.ToTable();
            }

            return new DataTable();
        }

        /// <summary>
        /// Devuelve una lista de los tipos de cultivo con su cantidad
        /// </summary>
        public static List<KeyValuePair<string, int>> ObtenerDistribucionTiposCultivo(DataTable dtCultivos)
        {
            List<KeyValuePair<string, int>> resultado = new List<KeyValuePair<string, int>>();

            if (dtCultivos != null && dtCultivos.Rows.Count > 0)
            {
                // Agrupar por tipo de cultivo
                var tiposCultivo = dtCultivos.AsEnumerable()
                    .GroupBy(r => r.Field<string>("Tipo De Cultivos"))
                    .Select(g => new KeyValuePair<string, int>(
                        g.Key ?? "No especificado",
                        g.Count()
                    ));

                resultado.AddRange(tiposCultivo);
            }

            return resultado;
        }

        /// <summary>
        /// Exporta los datos de una DataTable a un archivo CSV
        /// </summary>
        public static bool ExportarDatosCSV(DataTable dt, string rutaArchivo)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                    return false;

                System.IO.StreamWriter sw = new System.IO.StreamWriter(rutaArchivo, false, System.Text.Encoding.UTF8);

                // Escribir encabezados
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i < dt.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.WriteLine();

                // Escribir datos
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(row[i]))
                        {
                            string value = row[i].ToString() ?? string.Empty;
                            // Si el valor contiene comas, lo encapsulamos entre comillas
                            if (value.Contains(","))
                                value = "\"" + value + "\"";
                            sw.Write(value);
                        }

                        if (i < dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }

                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Calcula la eficiencia de los cultivos basado en rendimiento y costo
        /// </summary>
        public static decimal CalcularEficienciaCultivo(decimal rendimiento, decimal costo, decimal precioVenta)
        {
            if (costo == 0)
                return 0;

            // Índice de eficiencia = (Rendimiento * Precio de Venta) / Costo
            return (rendimiento * precioVenta) / costo;
        }

        /// <summary>
        /// Predice la fecha óptima de cosecha basada en los seguimientos
        /// </summary>
        public static DateTime? PredecirFechaOptimaCosecha(int idCultivo, DateTime fechaPlantacion, DateTime fechaCosechaEstimada)
        {
            try
            {
                DataTable dtSeguimientos = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);

                if (dtSeguimientos != null && dtSeguimientos.Rows.Count >= 3)
                {
                    // Ordenar por fecha para ver la progresión
                    DataView dv = dtSeguimientos.DefaultView;
                    dv.Sort = "Fecha_Registro ASC";
                    DataTable dtOrdenado = dv.ToTable();

                    // Obtener el último seguimiento
                    DataRow ultimoSeguimiento = dtOrdenado.Rows[dtOrdenado.Rows.Count - 1];
                    decimal porcentajeCrecimiento = Convert.ToDecimal(ultimoSeguimiento["Porcentaje_Crecimiento"]);

                    // Si el crecimiento es mayor al 95%, considerar adelantar la cosecha
                    if (porcentajeCrecimiento >= 95)
                    {
                        // Calcular fecha estimada adelantada
                        int diasRestantes = (fechaCosechaEstimada - DateTime.Now).Days;
                        if (diasRestantes > 5)
                            return fechaCosechaEstimada.AddDays(-3); // Adelantar 3 días
                    }
                    // Si el crecimiento es menor al 75%, considerar retrasar la cosecha
                    else if (porcentajeCrecimiento < 75)
                    {
                        // Retrasar la cosecha si es necesario
                        return fechaCosechaEstimada.AddDays(5); // Retrasar 5 días
                    }
                }

                // Si no hay suficientes datos o el crecimiento es normal, mantener la fecha original
                return fechaCosechaEstimada;
            }
            catch
            {
                // En caso de error, devolver null
                return null;
            }
        }
    }
}

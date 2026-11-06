using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Dashboard
    {
        /// <summary>
        /// Obtiene un resumen de todas las estadísticas para el dashboard
        /// </summary>
        /// <returns>Entidad con todos los datos del dashboard</returns>
        public static Entitie_Dashboard ObtenerEstadisticas()
        {
            Entitie_Dashboard dashboard = new Entitie_Dashboard();

            try
            {
                // Obtener datos de cultivos
                DataTable dtCultivos = BLL_Siembra.Mostrar_Cultivos();
                if (dtCultivos != null && dtCultivos.Rows.Count > 0)
                {
                    // Filtrar cultivos activos (fecha de cosecha mayor a hoy)
                    DataView dv = dtCultivos.DefaultView;
                    dv.RowFilter = "Convert([Fecha de Cosecha], 'System.DateTime') > #" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "#";
                    DataTable dtActivos = dv.ToTable();

                    // Cultivos activos
                    dashboard.TotalCultivosActivos = dtActivos.Rows.Count;

                    // Próximas cosechas (30 días)
                    DateTime fechaLimite = DateTime.Now.AddDays(30);
                    dv.RowFilter = "Convert([Fecha de Cosecha], 'System.DateTime') <= #" +
                        fechaLimite.ToString("yyyy-MM-dd") + "# AND " +
                        "Convert([Fecha de Cosecha], 'System.DateTime') >= #" +
                        DateTime.Now.ToString("yyyy-MM-dd") + "#";
                    DataTable dtProximas = dv.ToTable();
                    dashboard.TotalCosechasProximas = dtProximas.Rows.Count;

                    // Cantidad total sembrada
                    foreach (DataRow row in dtActivos.Rows)
                    {
                        if (row["Cantidad Sembrada"] != DBNull.Value)
                        {
                            dashboard.CantidadTotalSembrada += Convert.ToDecimal(row["Cantidad Sembrada"]);
                        }
                    }

                    // Distribución por tipo de cultivo
                    var tiposCultivo = dtActivos.AsEnumerable()
                        .GroupBy(r => r.Field<string>("Tipo De Cultivos"))
                        .Select(g => new {
                            Tipo = g.Key,
                            Cantidad = g.Count()
                        });

                    foreach (var tipo in tiposCultivo)
                    {
                        if (!string.IsNullOrEmpty(tipo.Tipo))
                        {
                            dashboard.CultivosPorTipo[tipo.Tipo] = tipo.Cantidad;
                        }
                    }

                    // Distribución por categoría
                    var categoriasCultivo = dtActivos.AsEnumerable()
                        .GroupBy(r => r.Field<string>("Categoria Cultivos"))
                        .Select(g => new {
                            Categoria = g.Key,
                            Cantidad = g.Count()
                        });

                    foreach (var categoria in categoriasCultivo)
                    {
                        if (!string.IsNullOrEmpty(categoria.Categoria))
                        {
                            dashboard.CultivosPorCategoria[categoria.Categoria] = categoria.Cantidad;
                        }
                    }
                }

                // En una versión más completa, se agregarían aquí otras estadísticas:
                // - Datos financieros
                // - Alertas
                // - Seguimientos
                // - etc.
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener estadísticas: " + ex.Message);
                // En una versión más robusta, se manejaría el error de mejor manera
            }

            return dashboard;
        }

        /// <summary>
        /// Obtiene los datos de seguimiento más recientes para un cultivo específico o para todos
        /// </summary>
        /// <param name="idCultivo">ID del cultivo o -1 para todos</param>
        /// <param name="limite">Cantidad máxima de registros a devolver</param>
        /// <returns>Tabla con los datos de seguimiento</returns>
        public static DataTable ObtenerUltimosSeguimientos(int idCultivo = -1, int limite = 10)
        {
            try
            {
                DataTable dtSeguimientos;

                if (idCultivo > 0)
                {
                    // Obtener seguimientos de un cultivo específico
                    dtSeguimientos = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);
                }
                else
                {
                    // En una implementación completa, aquí se obtendría los seguimientos de todos los cultivos
                    // Como ejemplo, usaremos el ID 1 si existe
                    dtSeguimientos = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(1);
                }

                if (dtSeguimientos != null && dtSeguimientos.Rows.Count > 0)
                {
                    // Ordenar por fecha más reciente
                    DataView dv = dtSeguimientos.DefaultView;
                    dv.Sort = "Fecha_Registro DESC";
                    DataTable dtOrdenado = dv.ToTable();

                    // Limitar la cantidad de registros
                    if (dtOrdenado.Rows.Count > limite)
                    {
                        DataTable dtLimitado = dtOrdenado.Clone();
                        for (int i = 0; i < limite && i < dtOrdenado.Rows.Count; i++)
                        {
                            dtLimitado.ImportRow(dtOrdenado.Rows[i]);
                        }
                        return dtLimitado;
                    }

                    return dtOrdenado;
                }

                return new DataTable(); // Devolver tabla vacía si no hay datos
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener seguimientos: " + ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// Obtiene las alertas activas del sistema
        /// </summary>
        /// <returns>DataTable con las alertas</returns>
        public static DataTable ObtenerAlertas()
        {
            DataTable dtAlertas = new DataTable();
            dtAlertas.Columns.Add("ID", typeof(int));
            dtAlertas.Columns.Add("Tipo", typeof(string));
            dtAlertas.Columns.Add("Descripcion", typeof(string));
            dtAlertas.Columns.Add("Fecha", typeof(DateTime));
            dtAlertas.Columns.Add("Prioridad", typeof(string));

            try
            {
                DataTable alertas = BLL_Alertas.ObtenerAlertasPendientes();
                foreach (DataRow row in alertas.Rows)
                {
                    dtAlertas.Rows.Add(
                        Convert.ToInt32(row["ID_Alerta"]),
                        row["Tipo_Alerta"].ToString(),
                        row["Titulo"].ToString(),
                        Convert.ToDateTime(row["Fecha_Programada"]),
                        row["Prioridad"].ToString());
                }
            }
            catch
            {
                // Si la base no esta disponible, el dashboard queda sin alertas falsas.
            }

            return dtAlertas;
        }
    }
}

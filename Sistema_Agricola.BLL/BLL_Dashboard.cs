using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Agricola.Common;
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
                RegistroErrores.Registrar(ex, "BLL_Dashboard.ObtenerEstadisticas");
                throw;
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
            return ObtenerUltimosSeguimientos(
                idCultivo,
                limite,
                BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento,
                BLL_Siembra.Mostrar_Cultivos);
        }

        internal static DataTable ObtenerUltimosSeguimientos(
            int idCultivo,
            int limite,
            Func<int, DataTable> cargarSeguimientos,
            Func<DataTable> cargarCultivos)
        {
            ArgumentNullException.ThrowIfNull(cargarSeguimientos);
            ArgumentNullException.ThrowIfNull(cargarCultivos);

            if (limite <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(limite),
                    "El límite debe ser mayor que cero.");
            }

            DataTable seguimientos = idCultivo > 0
                ? cargarSeguimientos(idCultivo)
                : CargarSeguimientosDeTodosLosCultivos(
                    cargarCultivos(),
                    cargarSeguimientos);

            return OrdenarYLimitarSeguimientos(seguimientos, limite);
        }

        private static DataTable CargarSeguimientosDeTodosLosCultivos(
            DataTable cultivos,
            Func<int, DataTable> cargarSeguimientos)
        {
            if (cultivos == null || !cultivos.Columns.Contains("ID_Cultivo"))
            {
                return new DataTable();
            }

            DataTable acumulado = new DataTable();
            IEnumerable<int> idsCultivos = cultivos.AsEnumerable()
                .Where(fila => fila["ID_Cultivo"] != DBNull.Value)
                .Select(fila => Convert.ToInt32(fila["ID_Cultivo"]))
                .Where(id => id > 0)
                .Distinct();

            foreach (int id in idsCultivos)
            {
                DataTable datosCultivo = cargarSeguimientos(id);
                if (datosCultivo == null || datosCultivo.Columns.Count == 0)
                {
                    continue;
                }

                if (acumulado.Columns.Count == 0)
                {
                    acumulado = datosCultivo.Clone();
                }

                foreach (DataRow fila in datosCultivo.Rows)
                {
                    acumulado.ImportRow(fila);
                }
            }

            return acumulado;
        }

        private static DataTable OrdenarYLimitarSeguimientos(
            DataTable seguimientos,
            int limite)
        {
            if (seguimientos == null || seguimientos.Rows.Count == 0)
            {
                return seguimientos?.Clone() ?? new DataTable();
            }

            if (!seguimientos.Columns.Contains("Fecha_Registro"))
            {
                throw new InvalidOperationException(
                    "Los seguimientos no incluyen la columna Fecha_Registro.");
            }

            DataView vista = seguimientos.DefaultView;
            vista.Sort = "Fecha_Registro DESC";
            DataTable ordenados = vista.ToTable();

            if (ordenados.Rows.Count <= limite)
            {
                return ordenados;
            }

            DataTable limitados = ordenados.Clone();
            for (int indice = 0; indice < limite; indice++)
            {
                limitados.ImportRow(ordenados.Rows[indice]);
            }

            return limitados;
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

            return dtAlertas;
        }
    }
}

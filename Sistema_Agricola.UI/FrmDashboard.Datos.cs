using Timer = System.Windows.Forms.Timer;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmDashboard : Form
    {
        private void CargarCultivosActivos()
        {
            try
            {
                // Obtener los cultivos activos
                dtCultivosActivos = BLL_Siembra.Mostrar_Cultivos() ?? new DataTable();

                if (dtCultivosActivos != null && dtCultivosActivos.Rows.Count > 0)
                {
                    // Mostrar nombres de columnas para depuración (solo en desarrollo)
                    string columnasDisponibles = "Columnas disponibles en la tabla:\n\n";
                    foreach (DataColumn col in dtCultivosActivos.Columns)
                    {
                        columnasDisponibles += "- " + col.ColumnName + "\n";
                    }

                    // Descomentar esta línea para ver las columnas disponibles
                    // MessageBox.Show(columnasDisponibles, "Depuración de columnas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Buscar la columna de fecha de cosecha con nombres específicos
                    string? nombreColumnaFechaCosecha = BuscarColumnaFechaCosecha(dtCultivosActivos);

                    if (string.IsNullOrEmpty(nombreColumnaFechaCosecha))
                    {
                        MessageBox.Show("No se pudo encontrar la columna de fecha de cosecha. " +
                            "Revise los nombres de columnas en la tabla de cultivos.\n\n" +
                            columnasDisponibles,
                            "Error de columna", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Mostrar todos los cultivos activos (sin filtro) para verificar
                    dgvCultivosActivos.DataSource = dtCultivosActivos;
                    FormatearGrid(dgvCultivosActivos);

                    try
                    {
                        // Intentar filtrar cultivos con fecha de cosecha futura
                        DataView dv = dtCultivosActivos.DefaultView;
                        string dateFormat = DateTime.Now.ToString("yyyy-MM-dd");

                        string filterQuery = $"Convert([{nombreColumnaFechaCosecha}], 'System.DateTime') > #{dateFormat}#";

                        // Aplicar el filtro
                        dv.RowFilter = filterQuery;
                        DataTable dtFiltrado = dv.ToTable();

                        // Usar la tabla filtrada
                        dgvCultivosActivos.DataSource = dtFiltrado;
                        FormatearGrid(dgvCultivosActivos);

                        // Actualizar contador
                        lblCultivosActivosValor.Text = dtFiltrado.Rows.Count.ToString();
                    }
                    catch (Exception filterEx)
                    {
                        ManejadorErroresUI.ObtenerReferencia(
                            filterEx,
                            "filtrar cultivos activos del dashboard");
                        lblCultivosActivosValor.Text = dtCultivosActivos.Rows.Count.ToString();
                    }
                }
                else
                {
                    lblCultivosActivosValor.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "cargar los cultivos activos");
            }
        }

        private string? BuscarColumnaFechaCosecha(DataTable dt)
        {
            // 1. Búsqueda más flexible por texto contenido
            foreach (DataColumn col in dt.Columns)
            {
                string nombreCol = col.ColumnName.ToLower();
                if (nombreCol.Contains("fecha") && nombreCol.Contains("cosecha"))
                {
                    return col.ColumnName;
                }
            }

            // 2. Búsqueda de cualquier columna de tipo DateTime que pueda ser fecha de cosecha
            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(DateTime) &&
                    (col.ColumnName.ToLower().Contains("fecha") ||
                     col.ColumnName.ToLower().Contains("date")))
                {
                    return col.ColumnName;
                }
            }

            // 3. Si todo falla, buscar cualquier columna con "fecha" en el nombre
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.ToLower().Contains("fecha"))
                {
                    return col.ColumnName;
                }
            }

            return null;
        }

        private void CargarProximasCosechas()
        {
            try
            {
                // Si no hay datos de cultivos, intentar cargarlos primero
                if (dtCultivosActivos == null || dtCultivosActivos.Rows.Count == 0)
                {
                    CargarCultivosActivos();
                }

                if (dtCultivosActivos != null && dtCultivosActivos.Rows.Count > 0)
                {
                    // Buscar el nombre de la columna de fecha de cosecha
                    string? nombreColumnaFechaCosecha = BuscarColumnaFechaCosecha(dtCultivosActivos);

                    if (string.IsNullOrEmpty(nombreColumnaFechaCosecha))
                    {
                        // Creamos una copia del DataTable original
                        DataTable dtTemp = dtCultivosActivos.Copy();

                        // Si existe alguna columna de tipo DateTime, usarla como fecha de cosecha
                        bool encontradaFecha = false;
                        foreach (DataColumn col in dtTemp.Columns)
                        {
                            if (col.DataType == typeof(DateTime))
                            {
                                nombreColumnaFechaCosecha = col.ColumnName;
                                encontradaFecha = true;
                                break;
                            }
                        }

                        if (!encontradaFecha)
                        {
                            // Si no hay columnas de tipo DateTime, usamos el DataTable completo
                            dgvProximasCosechas.DataSource = dtCultivosActivos;
                            lblProximasCosechasValor.Text = dtCultivosActivos.Rows.Count.ToString();
                            return;
                        }
                    }

                    // Crear un nuevo DataView a partir de una copia para evitar problemas
                    DataTable dtCopia = dtCultivosActivos.Copy();
                    DataView dv = dtCopia.DefaultView;

                    try
                    {
                        DateTime fechaLimite = DateTime.Now.AddDays(30);
                        string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                        string fechaLimiteStr = fechaLimite.ToString("yyyy-MM-dd");

                        // Intentar primero con una versión más simple del filtro
                        dv.RowFilter = $"Convert([{nombreColumnaFechaCosecha}], 'System.DateTime') >= #{fechaActual}# AND " +
                                       $"Convert([{nombreColumnaFechaCosecha}], 'System.DateTime') <= #{fechaLimiteStr}#";

                        DataTable dtProximas = dv.ToTable();

                        // Si el filtro no devuelve filas, usar la tabla original
                        if (dtProximas.Rows.Count == 0)
                        {
                            dtProximas = dtCultivosActivos.Copy();
                        }

                        dgvProximasCosechas.DataSource = dtProximas;
                        FormatearGrid(dgvProximasCosechas);

                        // Actualizar contador
                        lblProximasCosechasValor.Text = dtProximas.Rows.Count.ToString();
                        dtProximasCosechas = dtProximas;
                    }
                    catch (Exception filterEx)
                    {
                        ManejadorErroresUI.ObtenerReferencia(
                            filterEx,
                            "filtrar próximas cosechas del dashboard");
                        dgvProximasCosechas.DataSource = dtCultivosActivos;
                        FormatearGrid(dgvProximasCosechas);
                        lblProximasCosechasValor.Text = dtCultivosActivos.Rows.Count.ToString();
                        dtProximasCosechas = dtCultivosActivos;
                    }
                }
                else
                {
                    lblProximasCosechasValor.Text = "0";
                }
            }
            catch (Exception ex)
            {
                string mensaje = ManejadorErroresUI.ObtenerMensajeSeguro(
                    ex,
                    "cargar próximas cosechas del dashboard");
                lblProximasCosechasValor.Text = "--";
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Mensaje", typeof(string));
                dtError.Rows.Add(mensaje);
                dgvProximasCosechas.DataSource = dtError;
            }
        }

        private void CargarSeguimientos()
        {
            try
            {
                dtSeguimientos = BLL_Dashboard.ObtenerUltimosSeguimientos(-1, 10);

                if (dtSeguimientos.Rows.Count > 0)
                {
                    dgvUltimosSeguimientos.DataSource = dtSeguimientos;
                    FormatearGrid(dgvUltimosSeguimientos);
                }
                else
                {
                    DataTable dtVacio = new DataTable();
                    dtVacio.Columns.Add("Mensaje", typeof(string));
                    dtVacio.Rows.Add("No hay seguimientos disponibles");
                    dgvUltimosSeguimientos.DataSource = dtVacio;
                    FormatearGrid(dgvUltimosSeguimientos);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ManejadorErroresUI.ObtenerMensajeSeguro(
                    ex,
                    "cargar seguimientos del dashboard");
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Mensaje", typeof(string));
                dtError.Rows.Add(mensaje);
                dgvUltimosSeguimientos.DataSource = dtError;
                FormatearGrid(dgvUltimosSeguimientos);
            }
        }

        private void CargarAlertas()
        {
            try
            {
                dtAlertas = BLL_Alertas.ObtenerAlertasProximas() ?? new DataTable();

                if (dtAlertas.Rows.Count > 0)
                {
                    dgvAlertas.DataSource = dtAlertas;
                    FormatearGrid(dgvAlertas);
                    lblAlertasValor.Text = dtAlertas.Rows.Count.ToString();
                }
                else
                {
                    lblAlertasValor.Text = "0";
                    DataTable dtVacio = new DataTable();
                    dtVacio.Columns.Add("Mensaje", typeof(string));
                    dtVacio.Rows.Add("No hay alertas pendientes");
                    dgvAlertas.DataSource = dtVacio;
                    FormatearGrid(dgvAlertas);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ManejadorErroresUI.ObtenerMensajeSeguro(
                    ex,
                    "cargar alertas del dashboard");
                lblAlertasValor.Text = "--";
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Mensaje", typeof(string));
                dtError.Rows.Add(mensaje);
                dgvAlertas.DataSource = dtError;
                FormatearGrid(dgvAlertas);
            }
        }

        private void CargarEstadisticas()
        {
            // Calcular y mostrar estadísticas generales
            try
            {
                // Cantidad total sembrada
                decimal cantidadTotal = 0;
                if (dtCultivosActivos != null && dtCultivosActivos.Rows.Count > 0)
                {
                    // Buscar la columna de cantidad sembrada
                    string? columnaCantidad = null;
                    string[] posiblesNombres = {
                        "Cantidad Sembrada",
                        "Cantidad_Sembrada",
                        "CantidadSembrada",
                        "Cantidad"
                    };

                    foreach (string nombre in posiblesNombres)
                    {
                        if (dtCultivosActivos.Columns.Contains(nombre))
                        {
                            columnaCantidad = nombre;
                            break;
                        }
                    }

                    // Si no encontramos ninguna, buscar la primera columna que contenga "cantidad"
                    if (columnaCantidad == null)
                    {
                        foreach (DataColumn col in dtCultivosActivos.Columns)
                        {
                            if (col.ColumnName.ToLower().Contains("cantidad"))
                            {
                                columnaCantidad = col.ColumnName;
                                break;
                            }
                        }
                    }

                    // Si encontramos la columna, sumamos los valores
                    if (columnaCantidad != null)
                    {
                        foreach (DataRow row in dtCultivosActivos.Rows)
                        {
                            if (row[columnaCantidad] != DBNull.Value)
                            {
                                try
                                {
                                    cantidadTotal += Convert.ToDecimal(row[columnaCantidad]);
                                }
                                catch
                                {
                                    // Ignorar si no se puede convertir
                                }
                            }
                        }
                    }
                }

                lblCantidadSembradaValor.Text = cantidadTotal.ToString("N0");

                // Cargar datos de cosechas para el gráfico de rendimientos
                try
                {
                    dtCosechas = BLL_Cosecha.Listar_Cosechas() ?? new DataTable();
                }
                catch (Exception cosechasEx)
                {
                    ManejadorErroresUI.ObtenerReferencia(
                        cosechasEx,
                        "cargar cosechas para estadísticas del dashboard");
                    dtCosechas = new DataTable();
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.ObtenerReferencia(
                    ex,
                    "calcular estadísticas del dashboard");
            }
        }

    }
}

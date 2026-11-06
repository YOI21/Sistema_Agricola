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

                        // Mostrar la consulta de filtro para depuración
                        string filterQuery = $"Convert([{nombreColumnaFechaCosecha}], 'System.DateTime') > #{dateFormat}#";
                        Console.WriteLine("Filter Query: " + filterQuery);

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
                        // Si falla el filtrado, mostramos todos los cultivos
                        MessageBox.Show("Error al filtrar: " + filterEx.Message,
                            "Error de filtro", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Usar todos los cultivos
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
                MessageBox.Show("Error en CargarCultivosActivos: " + ex.Message +
                    "\n\nStackTrace: " + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        // Si no encontramos la columna, mostrar un mensaje de depuración y crear una
                        Console.WriteLine("No se pudo encontrar la columna de fecha de cosecha.");

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
                        Console.WriteLine("Error al filtrar próximas cosechas: " + filterEx.Message);

                        // Usar todos los cultivos como alternativa
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
                Console.WriteLine("Error en CargarProximasCosechas: " + ex.Message);
                lblProximasCosechasValor.Text = "0";

                // Crear una tabla vacía con mensaje de error
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Mensaje", typeof(string));
                dtError.Rows.Add("No hay próximas cosechas disponibles");
                dgvProximasCosechas.DataSource = dtError;
            }
        }

        private void CargarSeguimientos()
        {
            try
            {
                // Intentar obtener el ID del primer cultivo disponible
                int cultivo = -1; // Valor por defecto

                // Intentar usar el DataTable de cultivos activos si existe
                if (dtCultivosActivos != null && dtCultivosActivos.Rows.Count > 0)
                {
                    // Buscar alguna columna que pueda contener el ID del cultivo
                    string? columnaId = null;

                    // Primero buscar columnas específicas que suelen contener IDs
                    string[] posiblesNombres = {
                "ID_Cultivo", "Id_Cultivo", "IdCultivo", "ID Cultivo", "ID", "Id"
            };

                    foreach (string nombre in posiblesNombres)
                    {
                        if (dtCultivosActivos.Columns.Contains(nombre))
                        {
                            columnaId = nombre;
                            break;
                        }
                    }

                    // Si no encontramos ninguna de las anteriores, buscar la primera columna con "ID" o "Id"
                    if (columnaId == null)
                    {
                        foreach (DataColumn col in dtCultivosActivos.Columns)
                        {
                            if (col.ColumnName.ToLower().Contains("id"))
                            {
                                columnaId = col.ColumnName;
                                break;
                            }
                        }
                    }

                    if (columnaId != null && dtCultivosActivos.Rows.Count > 0)
                    {
                        try
                        {
                            // Intentar convertir el valor a entero
                            cultivo = Convert.ToInt32(dtCultivosActivos.Rows[0][columnaId]);
                        }
                        catch
                        {
                            // Si falla, usar valor por defecto
                            cultivo = 1;
                        }
                    }
                }

                // Si no pudimos obtener un ID válido, usar valor por defecto
                if (cultivo <= 0)
                {
                    cultivo = 1; // ID por defecto
                }

                // Ahora intentamos obtener el seguimiento de ese cultivo
                try
                {
                    dtSeguimientos =
                        BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(cultivo) ??
                        new DataTable();

                    if (dtSeguimientos != null && dtSeguimientos.Rows.Count > 0)
                    {
                        // Ordenar por fecha más reciente
                        DataView dv = dtSeguimientos.DefaultView;

                        // Buscar la columna de fecha para ordenar
                        string? columnaFecha = null;
                        foreach (DataColumn col in dtSeguimientos.Columns)
                        {
                            if (col.DataType == typeof(DateTime) ||
                                col.ColumnName.ToLower().Contains("fecha"))
                            {
                                columnaFecha = col.ColumnName;
                                break;
                            }
                        }

                        if (columnaFecha != null)
                        {
                            dv.Sort = $"{columnaFecha} DESC";
                        }
                        else
                        {
                            // Si no hay columna de fecha, intentar con ID o la primera columna
                            if (dtSeguimientos.Columns.Contains("ID_Seguimiento"))
                            {
                                dv.Sort = "ID_Seguimiento DESC";
                            }
                            else if (dtSeguimientos.Columns.Count > 0)
                            {
                                dv.Sort = dtSeguimientos.Columns[0].ColumnName + " DESC";
                            }
                        }

                        DataTable dtOrdenado = dv.ToTable();

                        // Limitar a los 10 primeros registros
                        DataTable dtLimitado = dtOrdenado.Clone();
                        for (int i = 0; i < Math.Min(10, dtOrdenado.Rows.Count); i++)
                        {
                            dtLimitado.ImportRow(dtOrdenado.Rows[i]);
                        }

                        dgvUltimosSeguimientos.DataSource = dtLimitado;
                        FormatearGrid(dgvUltimosSeguimientos);
                    }
                    else
                    {
                        // Crear una tabla vacía con mensaje informativo
                        DataTable dtVacio = new DataTable();
                        dtVacio.Columns.Add("Mensaje", typeof(string));
                        dtVacio.Rows.Add("No hay seguimientos disponibles");
                        dgvUltimosSeguimientos.DataSource = dtVacio;
                        FormatearGrid(dgvUltimosSeguimientos);
                    }
                }
                catch (Exception methodEx)
                {
                    Console.WriteLine("Error al llamar a Mostrar_SeguimientoCrecimiento: " + methodEx.Message);

                    // Crear tabla con mensaje de error
                    DataTable dtError = new DataTable();
                    dtError.Columns.Add("Error", typeof(string));
                    dtError.Rows.Add("Error al cargar seguimientos: " + methodEx.Message);
                    dgvUltimosSeguimientos.DataSource = dtError;
                    FormatearGrid(dgvUltimosSeguimientos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general en CargarSeguimientos: " + ex.Message);

                // Crear tabla con mensaje de error
                DataTable dtError = new DataTable();
                dtError.Columns.Add("Error", typeof(string));
                dtError.Rows.Add("Error al cargar seguimientos: " + ex.Message);
                dgvUltimosSeguimientos.DataSource = dtError;
                FormatearGrid(dgvUltimosSeguimientos);
            }
        }

        private void CargarAlertas()
        {
            try
            {
                // Cargar alertas pendientes - usar try/catch por si falla el método
                try
                {
                    dtAlertas = BLL_Alertas.ObtenerAlertasProximas() ?? new DataTable();

                    if (dtAlertas != null && dtAlertas.Rows.Count > 0)
                    {
                        // Mostrar en el grid
                        dgvAlertas.DataSource = dtAlertas;
                        FormatearGrid(dgvAlertas);

                        // Actualizar contador
                        lblAlertasValor.Text = dtAlertas.Rows.Count.ToString();
                    }
                    else
                    {
                        lblAlertasValor.Text = "0";

                        // Crear tabla vacía con mensaje
                        DataTable dtVacio = new DataTable();
                        dtVacio.Columns.Add("Mensaje", typeof(string));
                        dtVacio.Rows.Add("No hay alertas pendientes");
                        dgvAlertas.DataSource = dtVacio;
                        FormatearGrid(dgvAlertas);
                    }
                }
                catch (Exception methodEx)
                {
                    Console.WriteLine("Error al llamar a ObtenerAlertasProximas: " + methodEx.Message);
                    lblAlertasValor.Text = "0";

                    // Crear tabla vacía con mensaje de error
                    DataTable dtError = new DataTable();
                    dtError.Columns.Add("Error", typeof(string));
                    dtError.Rows.Add("Error al cargar alertas: " + methodEx.Message);
                    dgvAlertas.DataSource = dtError;
                    FormatearGrid(dgvAlertas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general en CargarAlertas: " + ex.Message);
                lblAlertasValor.Text = "0";
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
                    Console.WriteLine("Error al cargar cosechas: " + cosechasEx.Message);
                    dtCosechas = new DataTable();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular estadísticas: " + ex.Message);
            }
        }

    }
}

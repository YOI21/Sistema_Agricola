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
        private void ConfigurarGraficoCultivos()
        {
            // Configurar el gráfico de tipos de cultivos
            chartTiposCultivo.Series.Clear();
            Series serie = new Series("Tipos de Cultivo");
            serie.ChartType = SeriesChartType.Pie;
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "#;0;";
            serie.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            chartTiposCultivo.Series.Add(serie);

            // Configurar leyenda
            chartTiposCultivo.Legends.Clear();
            Legend legend = new Legend("Leyenda");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            chartTiposCultivo.Legends.Add(legend);

            // Configurar áreas
            chartTiposCultivo.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartTiposCultivo.ChartAreas[0].Area3DStyle.Inclination = 20;
        }

        private void ActualizarGraficoCultivos()
        {
            try
            {
                // Actualizar el gráfico con datos reales
                if (dtCultivosActivos != null && dtCultivosActivos.Rows.Count > 0)
                {
                    // Buscar columna de tipo de cultivo
                    string? columnaTipoCultivo = null;
                    string[] posiblesNombres = {
                        "Tipo De Cultivos",
                        "Tipo_Cultivo",
                        "Tipo de Cultivo",
                        "TipoCultivo",
                        "Tipo_Cultivos"
                    };

                    foreach (string nombre in posiblesNombres)
                    {
                        if (dtCultivosActivos.Columns.Contains(nombre))
                        {
                            columnaTipoCultivo = nombre;
                            break;
                        }
                    }

                    // Si no encontramos ninguna de las anteriores, buscar columna que contenga "tipo" y "cultivo"
                    if (columnaTipoCultivo == null)
                    {
                        foreach (DataColumn col in dtCultivosActivos.Columns)
                        {
                            if (col.ColumnName.ToLower().Contains("tipo") && col.ColumnName.ToLower().Contains("cultivo"))
                            {
                                columnaTipoCultivo = col.ColumnName;
                                break;
                            }
                        }
                    }

                    // Si todavía no encontramos una columna, buscar cualquier columna que contenga "tipo"
                    if (columnaTipoCultivo == null)
                    {
                        foreach (DataColumn col in dtCultivosActivos.Columns)
                        {
                            if (col.ColumnName.ToLower().Contains("tipo"))
                            {
                                columnaTipoCultivo = col.ColumnName;
                                break;
                            }
                        }
                    }

                    if (columnaTipoCultivo == null)
                    {
                        // Si aún no encontramos la columna, no podemos actualizar el gráfico
                        Console.WriteLine("No se pudo encontrar la columna de tipo de cultivo");
                        return;
                    }

                    // Agrupar por tipo de cultivo
                    var tiposCultivo = dtCultivosActivos.AsEnumerable()
                        .GroupBy(r => r.Field<string>(columnaTipoCultivo))
                        .Select(g => new
                        {
                            Tipo = g.Key ?? "No especificado",
                            Cantidad = g.Count()
                        });

                    // Limpiar serie existente
                    chartTiposCultivo.Series[0].Points.Clear();

                    // Agregar datos al gráfico
                    foreach (var tipo in tiposCultivo)
                    {
                        int idx = chartTiposCultivo.Series[0].Points.AddY(tipo.Cantidad);
                        chartTiposCultivo.Series[0].Points[idx].LegendText = tipo.Tipo;
                        chartTiposCultivo.Series[0].Points[idx].Label = tipo.Cantidad.ToString();

                        // Asignar colores
                        chartTiposCultivo.Series[0].Points[idx].Color = GetRandomChartColor(idx);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar gráfico de cultivos: " + ex.Message);
            }
        }

        private void ConfigurarGraficoSeguimiento()
        {
            // Configurar el gráfico de seguimiento de crecimiento
            chartSeguimiento.Series.Clear();
            Series serieAltura = new Series("Altura (cm)");
            serieAltura.ChartType = SeriesChartType.Line;
            serieAltura.BorderWidth = 3;
            serieAltura.MarkerStyle = MarkerStyle.Circle;
            serieAltura.MarkerSize = 8;
            serieAltura.Color = Color.FromArgb(46, 204, 113);

            Series seriePorcentaje = new Series("% Crecimiento");
            seriePorcentaje.ChartType = SeriesChartType.Line;
            seriePorcentaje.BorderWidth = 3;
            seriePorcentaje.MarkerStyle = MarkerStyle.Diamond;
            seriePorcentaje.MarkerSize = 8;
            seriePorcentaje.Color = Color.FromArgb(52, 152, 219);

            chartSeguimiento.Series.Add(serieAltura);
            chartSeguimiento.Series.Add(seriePorcentaje);

            // Configurar ejes
            chartSeguimiento.ChartAreas[0].AxisX.Title = "Fecha";
            chartSeguimiento.ChartAreas[0].AxisY.Title = "Valores";
            chartSeguimiento.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartSeguimiento.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            chartSeguimiento.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartSeguimiento.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Configurar leyenda
            chartSeguimiento.Legends.Clear();
            Legend legend = new Legend("Leyenda");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            chartSeguimiento.Legends.Add(legend);
        }

        private void ActualizarGraficoSeguimiento()
        {
            try
            {
                // Actualizar el gráfico con datos reales
                if (dtSeguimientos != null && dtSeguimientos.Rows.Count > 0)
                {
                    // Verificar que existan las columnas necesarias
                    if (!dtSeguimientos.Columns.Contains("Fecha_Registro") ||
                        !dtSeguimientos.Columns.Contains("Altura_Planta") ||
                        !dtSeguimientos.Columns.Contains("Porcentaje_Crecimiento"))
                    {
                        Console.WriteLine("No se encuentran todas las columnas necesarias para el gráfico de seguimiento");
                        return;
                    }

                    // Limpiar series existentes
                    chartSeguimiento.Series[0].Points.Clear();
                    chartSeguimiento.Series[1].Points.Clear();

                    // Ordenar por fecha
                    DataView dv = dtSeguimientos.DefaultView;
                    dv.Sort = "Fecha_Registro ASC";

                    DataTable dtOrdenado = dv.ToTable();

                    // Mostrar solo los últimos 10 registros si hay más
                    int startIndex = Math.Max(0, dtOrdenado.Rows.Count - 10);
                    int endIndex = dtOrdenado.Rows.Count;

                    // Agregar datos al gráfico
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        DataRow row = dtOrdenado.Rows[i];

                        try
                        {
                            DateTime fecha = Convert.ToDateTime(row["Fecha_Registro"]);
                            decimal altura = Convert.ToDecimal(row["Altura_Planta"]);
                            decimal porcentaje = Convert.ToDecimal(row["Porcentaje_Crecimiento"]);

                            // Ajustar el formato de fecha para el eje X
                            string fechaFormateada = fecha.ToString("dd/MM");

                            // Añadir puntos a ambas series
                            chartSeguimiento.Series[0].Points.AddXY(fechaFormateada, altura);
                            chartSeguimiento.Series[1].Points.AddXY(fechaFormateada, porcentaje);
                        }
                        catch (Exception rowEx)
                        {
                            Console.WriteLine("Error al procesar fila de seguimiento: " + rowEx.Message);
                            // Continuar con la siguiente fila
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar gráfico de seguimiento: " + ex.Message);
            }
        }

        private void ConfigurarGraficoRendimientos()
        {
            // Configurar el gráfico de rendimientos
            chartRendimientos.Series.Clear();
            Series serie = new Series("Rendimiento (%)");
            serie.ChartType = SeriesChartType.Column;
            serie.Color = Color.FromArgb(230, 126, 34);
            serie.LabelFormat = "0.0 %";
            serie.IsValueShownAsLabel = true;
            chartRendimientos.Series.Add(serie);

            // Configurar ejes
            chartRendimientos.ChartAreas[0].AxisX.Title = "Cultivo";
            chartRendimientos.ChartAreas[0].AxisY.Title = "Rendimiento (%)";
            chartRendimientos.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartRendimientos.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            chartRendimientos.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartRendimientos.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void ActualizarGraficoRendimientos()
        {
            try
            {
                // Actualizar el gráfico con datos reales
                if (dtCosechas != null && dtCosechas.Rows.Count > 0)
                {
                    // Limpiar serie existente
                    chartRendimientos.Series[0].Points.Clear();

                    // Buscar columnas necesarias para el gráfico
                    string? columnaRendimiento = BuscarColumnaRendimiento(dtCosechas);
                    string? columnaCultivo = BuscarColumnaCultivo(dtCosechas);

                    if (string.IsNullOrEmpty(columnaRendimiento) || string.IsNullOrEmpty(columnaCultivo))
                    {
                        Console.WriteLine("No se encuentran las columnas necesarias para el gráfico de rendimientos");
                        return;
                    }

                    try
                    {
                        // Ordenar por rendimiento
                        DataView dv = dtCosechas.DefaultView;
                        dv.Sort = $"[{columnaRendimiento}] DESC";

                        DataTable dtOrdenado = dv.ToTable();

                        // Mostrar solo los 5 mejores rendimientos
                        int count = Math.Min(5, dtOrdenado.Rows.Count);
                        for (int i = 0; i < count; i++)
                        {
                            DataRow row = dtOrdenado.Rows[i];
                            string cultivo = row[columnaCultivo].ToString() ?? string.Empty;

                            // Verificar si hay valor de rendimiento
                            if (row[columnaRendimiento] != DBNull.Value)
                            {
                                try
                                {
                                    decimal rendimiento = Convert.ToDecimal(row[columnaRendimiento]);
                                    // Añadir al gráfico
                                    chartRendimientos.Series[0].Points.AddXY(cultivo, rendimiento);
                                }
                                catch
                                {
                                    // Ignorar si no se puede convertir
                                    continue;
                                }
                            }
                        }
                    }
                    catch (Exception sortEx)
                    {
                        Console.WriteLine("Error al ordenar datos de rendimiento: " + sortEx.Message);

                        // Alternativa: mostrar sin ordenar
                        int count = Math.Min(5, dtCosechas.Rows.Count);
                        for (int i = 0; i < count; i++)
                        {
                            DataRow row = dtCosechas.Rows[i];
                            string cultivo = row[columnaCultivo].ToString() ?? string.Empty;

                            if (row[columnaRendimiento] != DBNull.Value)
                            {
                                try
                                {
                                    decimal rendimiento = Convert.ToDecimal(row[columnaRendimiento]);
                                    chartRendimientos.Series[0].Points.AddXY(cultivo, rendimiento);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar gráfico de rendimientos: " + ex.Message);
            }
        }

        private string? BuscarColumnaRendimiento(DataTable dt)
        {
            string[] posiblesNombres = {
                "Rendimiento %",
                "Rendimiento",
                "% Rendimiento",
                "Rendimiento_Porcentaje"
            };

            foreach (string nombre in posiblesNombres)
            {
                if (dt.Columns.Contains(nombre))
                {
                    return nombre;
                }
            }

            // Buscar columna que contenga "rendimiento"
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.ToLower().Contains("rendimiento"))
                {
                    return col.ColumnName;
                }
            }

            return null;
        }

        private string? BuscarColumnaCultivo(DataTable dt)
        {
            string[] posiblesNombres = {
                "Cultivo",
                "Nombre_Cultivo",
                "Nombre Cultivo",
                "NombreCultivo"
            };

            foreach (string nombre in posiblesNombres)
            {
                if (dt.Columns.Contains(nombre))
                {
                    return nombre;
                }
            }

            // Buscar columna que contenga "cultivo" o "nombre"
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.ToLower().Contains("cultivo") ||
                    col.ColumnName.ToLower().Contains("nombre"))
                {
                    return col.ColumnName;
                }
            }

            return null;
        }

        private void FormatearGrid(DataGridView? dgv)
        {
            if (dgv == null || dgv.IsDisposed)
            {
                return;
            }

            // Dar formato al DataGridView
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 204, 113);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Font fuenteBase = dgv.Font ?? SystemFonts.MessageBoxFont ?? Control.DefaultFont;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(fuenteBase, FontStyle.Bold);
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 204, 113);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.RowTemplate.Height = 30;
        }

        private Color GetRandomChartColor(int index)
        {
            // Lista de colores predefinidos para el gráfico
            Color[] colores = new Color[] {
                Color.FromArgb(46, 204, 113),   // Verde
                Color.FromArgb(52, 152, 219),   // Azul
                Color.FromArgb(230, 126, 34),   // Naranja
                Color.FromArgb(155, 89, 182),   // Púrpura
                Color.FromArgb(231, 76, 60),    // Rojo
                Color.FromArgb(241, 196, 15),   // Amarillo
                Color.FromArgb(26, 188, 156),   // Turquesa
                Color.FromArgb(149, 165, 166)   // Gris
            };

            return colores[index % colores.Length];
        }

    }
}

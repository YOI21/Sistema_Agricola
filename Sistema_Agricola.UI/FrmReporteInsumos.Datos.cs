using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteInsumos : Form
    {
        private void CargarDatos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                todosLosProductos = BLL_Inventario.ListarProductos(string.Empty, "Todos", false);
                CargarTipos();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                todosLosProductos = new DataTable();
                productosFiltrados = new DataTable();
                movimientosFiltrados = new DataTable();
                dgvInsumos.DataSource = productosFiltrados;
                dgvMovimientos.DataSource = movimientosFiltrados;
                MostrarError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarTipos()
        {
            string seleccionado = cmbTipo.SelectedItem?.ToString() ?? "Todos";
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Todos");

            foreach (string tipo in todosLosProductos.AsEnumerable()
                         .Select(f => Texto(f, "Tipo"))
                         .Where(t => !string.IsNullOrWhiteSpace(t))
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .OrderBy(t => t))
            {
                cmbTipo.Items.Add(tipo);
            }

            int indice = cmbTipo.Items.IndexOf(seleccionado);
            cmbTipo.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private void AplicarFiltros()
        {
            if (todosLosProductos.Columns.Count == 0)
            {
                return;
            }

            IEnumerable<DataRow> consulta = todosLosProductos.AsEnumerable();
            string busqueda = Normalizar(txtBuscar.Text);
            string tipo = cmbTipo.SelectedItem?.ToString() ?? "Todos";
            string nivel = cmbNivel.SelectedItem?.ToString() ?? "Todos";

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = consulta.Where(f =>
                    Normalizar(Texto(f, "Codigo", "Código", "CÃ³digo")).Contains(busqueda) ||
                    Normalizar(Texto(f, "Nombre")).Contains(busqueda) ||
                    Normalizar(Texto(f, "Tipo")).Contains(busqueda) ||
                    Normalizar(Texto(f, "Ubicacion", "Ubicación", "UbicaciÃ³n")).Contains(busqueda));
            }

            if (tipo != "Todos")
            {
                consulta = consulta.Where(f =>
                    Texto(f, "Tipo").Equals(tipo, StringComparison.OrdinalIgnoreCase));
            }

            if (nivel != "Todos")
            {
                consulta = consulta.Where(f =>
                    Texto(f, "Nivel").Equals(nivel, StringComparison.OrdinalIgnoreCase));
            }

            if (chkSoloActivos.Checked)
            {
                consulta = consulta.Where(f =>
                    !Texto(f, "Estado").Equals("Inactivo", StringComparison.OrdinalIgnoreCase));
            }

            if (chkSoloBajoStock.Checked)
            {
                consulta = consulta.Where(f =>
                    Texto(f, "Nivel").Equals("Bajo", StringComparison.OrdinalIgnoreCase) ||
                    Texto(f, "Nivel").Equals("Agotado", StringComparison.OrdinalIgnoreCase));
            }

            productosFiltrados = consulta.Any() ? consulta.CopyToDataTable() : todosLosProductos.Clone();
            dgvInsumos.DataSource = productosFiltrados;
            FormatearGridProductos();
            CargarMovimientosYResumen();
        }

        private void CargarMovimientosYResumen()
        {
            try
            {
                if (dtpDesde.Value.Date > dtpHasta.Value.Date)
                {
                    dtpHasta.Value = dtpDesde.Value.Date;
                    return;
                }

                DataTable movimientos = chkConMovimientos.Checked
                    ? BLL_Inventario.ListarMovimientos(null, dtpDesde.Value.Date, dtpHasta.Value.Date)
                    : CrearTablaMovimientosVacia();

                if (productosFiltrados.Columns.Count > 0 && movimientos.Columns.Count > 0)
                {
                    HashSet<int> ids = productosFiltrados.AsEnumerable()
                        .Select(f => Entero(f, "ID"))
                        .Where(id => id > 0)
                        .ToHashSet();

                    IEnumerable<DataRow> consulta = movimientos.AsEnumerable()
                        .Where(f => ids.Contains(Entero(f, "ID Insumo")));
                    movimientosFiltrados = consulta.Any() ? consulta.CopyToDataTable() : movimientos.Clone();
                }
                else
                {
                    movimientosFiltrados = movimientos;
                }

                dgvMovimientos.DataSource = movimientosFiltrados;
                FormatearGridMovimientos();
                ActualizarResumen();
                ActualizarGrafico();
            }
            catch (Exception ex)
            {
                movimientosFiltrados = CrearTablaMovimientosVacia();
                dgvMovimientos.DataSource = movimientosFiltrados;
                MostrarError(ex.Message);
            }
        }

        private DataTable CrearTablaMovimientosVacia()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("ID Insumo", typeof(int));
            tabla.Columns.Add("Producto", typeof(string));
            tabla.Columns.Add("Movimiento", typeof(string));
            tabla.Columns.Add("Cantidad", typeof(decimal));
            tabla.Columns.Add("Unidad", typeof(string));
            tabla.Columns.Add("Costo unitario", typeof(decimal));
            tabla.Columns.Add("Total", typeof(decimal));
            tabla.Columns.Add("Fecha", typeof(DateTime));
            tabla.Columns.Add("Motivo", typeof(string));
            tabla.Columns.Add("Referencia", typeof(string));
            return tabla;
        }

        private void ActualizarResumen()
        {
            Entitie_ResumenInventario resumen = BLL_Inventario.CalcularResumen(productosFiltrados);
            decimal entradas = SumarMovimientos("Entrada");
            decimal salidas = SumarMovimientos("Salida");

            lblProductos.Text = resumen.ProductosActivos.ToString("N0");
            lblBajoStock.Text = resumen.BajoStock.ToString("N0");
            lblValorInventario.Text = Moneda(resumen.ValorInventario);
            lblPorVencer.Text = resumen.PorVencer.ToString("N0");
            lblEntradas.Text = entradas.ToString("N2");
            lblSalidas.Text = salidas.ToString("N2");
            lblRegistros.Text =
                $"{productosFiltrados.Rows.Count:N0} insumos | {movimientosFiltrados.Rows.Count:N0} movimientos";
        }

        private decimal SumarMovimientos(string tipo)
        {
            if (!movimientosFiltrados.Columns.Contains("Movimiento"))
            {
                return 0;
            }

            return movimientosFiltrados.AsEnumerable()
                .Where(f => Texto(f, "Movimiento").Equals(tipo, StringComparison.OrdinalIgnoreCase))
                .Sum(f => Decimal(f, "Cantidad"));
        }

        private void ActualizarGrafico()
        {
            chartInsumos.Series.Clear();
            chartInsumos.Titles.Clear();
            ChartArea area = chartInsumos.ChartAreas["AreaInsumos"];
            area.AxisX.CustomLabels.Clear();
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -25;
            area.AxisY.LabelStyle.Format = "N0";

            string vista = cmbVistaGrafico.SelectedItem?.ToString() ?? "Valor por tipo";
            if (vista == "Stock por nivel")
            {
                GraficarStockPorNivel();
            }
            else if (vista == "Movimientos por producto")
            {
                GraficarMovimientosPorProducto();
            }
            else
            {
                GraficarValorPorTipo();
            }
        }

        private void GraficarValorPorTipo()
        {
            var datos = productosFiltrados.AsEnumerable()
                .GroupBy(f => string.IsNullOrWhiteSpace(Texto(f, "Tipo")) ? "Sin tipo" : Texto(f, "Tipo"))
                .Select(g => new
                {
                    Tipo = g.Key,
                    Valor = g.Sum(f => Decimal(f, "Valor stock"))
                })
                .Where(x => x.Valor > 0)
                .OrderByDescending(x => x.Valor)
                .Take(8)
                .ToList();

            if (datos.Count == 0)
            {
                MostrarGraficoVacio("No hay valor de inventario para graficar.");
                return;
            }

            Series serie = CrearSerie("Valor", SeriesChartType.Column, Color.FromArgb(45, 125, 94));
            foreach (var item in datos)
            {
                int punto = serie.Points.AddXY(item.Tipo, item.Valor);
                serie.Points[punto].ToolTip = $"{item.Tipo}: {Moneda(item.Valor)}";
            }

            chartInsumos.Series.Add(serie);
            chartInsumos.ChartAreas["AreaInsumos"].AxisY.LabelStyle.Format = "RD$ #,##0;-RD$ #,##0;RD$ 0";
            lblDetalleGrafico.Text = "Valor actual del inventario agrupado por tipo de insumo.";
        }

        private void GraficarStockPorNivel()
        {
            var datos = productosFiltrados.AsEnumerable()
                .GroupBy(f => string.IsNullOrWhiteSpace(Texto(f, "Nivel")) ? "Sin nivel" : Texto(f, "Nivel"))
                .Select(g => new { Nivel = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .ToList();

            if (datos.Count == 0)
            {
                MostrarGraficoVacio("No hay niveles de stock para graficar.");
                return;
            }

            Series serie = CrearSerie("Productos", SeriesChartType.Doughnut, Color.FromArgb(52, 101, 164));
            serie["PieLabelStyle"] = "Outside";
            serie["DoughnutRadius"] = "62";

            foreach (var item in datos)
            {
                int punto = serie.Points.AddXY(item.Nivel, item.Total);
                serie.Points[punto].Label = $"{item.Nivel}: {item.Total:N0}";
                serie.Points[punto].ToolTip = $"{item.Nivel}: {item.Total:N0} productos";
                serie.Points[punto].Color = ColorNivel(item.Nivel);
            }

            chartInsumos.Series.Add(serie);
            lblDetalleGrafico.Text = "Distribucion de productos por nivel de stock.";
        }

        private void GraficarMovimientosPorProducto()
        {
            var datos = movimientosFiltrados.AsEnumerable()
                .GroupBy(f => Texto(f, "Producto"))
                .Select(g => new
                {
                    Producto = string.IsNullOrWhiteSpace(g.Key) ? "Sin producto" : g.Key,
                    Entradas = g.Where(f => Texto(f, "Movimiento") == "Entrada").Sum(f => Decimal(f, "Cantidad")),
                    Salidas = g.Where(f => Texto(f, "Movimiento") == "Salida").Sum(f => Decimal(f, "Cantidad"))
                })
                .OrderByDescending(x => x.Entradas + x.Salidas)
                .Take(8)
                .ToList();

            if (datos.Count == 0)
            {
                MostrarGraficoVacio("No hay movimientos en el periodo seleccionado.");
                return;
            }

            Series entradas = CrearSerie("Entradas", SeriesChartType.Column, Color.FromArgb(45, 125, 94));
            Series salidas = CrearSerie("Salidas", SeriesChartType.Column, Color.FromArgb(190, 70, 58));
            foreach (var item in datos)
            {
                entradas.Points.AddXY(Abreviar(item.Producto, 16), item.Entradas);
                salidas.Points.AddXY(Abreviar(item.Producto, 16), item.Salidas);
            }

            chartInsumos.Series.Add(entradas);
            chartInsumos.Series.Add(salidas);
            chartInsumos.Legends["Leyenda"].Enabled = true;
            lblDetalleGrafico.Text = "Entradas y salidas del periodo para los productos con mas movimiento.";
        }

        private void MostrarGraficoVacio(string mensaje)
        {
            chartInsumos.Series.Clear();
            chartInsumos.Legends["Leyenda"].Enabled = false;
            lblDetalleGrafico.Text = mensaje;
            chartInsumos.Invalidate();
        }

        private void ConfigurarGrafico()
        {
            chartInsumos.Dock = DockStyle.Fill;
            chartInsumos.BackColor = Color.White;
            chartInsumos.ChartAreas.Add(new ChartArea("AreaInsumos")
            {
                BackColor = Color.White
            });
            chartInsumos.Legends.Add(new Legend("Leyenda")
            {
                Docking = Docking.Bottom,
                Font = new Font("Segoe UI", 8F)
            });
            chartInsumos.ChartAreas["AreaInsumos"].AxisX.MajorGrid.Enabled = false;
            chartInsumos.ChartAreas["AreaInsumos"].AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 238);
            chartInsumos.ChartAreas["AreaInsumos"].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartInsumos.ChartAreas["AreaInsumos"].AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
        }

        private static Series CrearSerie(string nombre, SeriesChartType tipo, Color color)
        {
            return new Series(nombre)
            {
                ChartArea = "AreaInsumos",
                ChartType = tipo,
                Color = color,
                Font = new Font("Segoe UI", 8F),
                YValueType = ChartValueType.Double
            };
        }

        private void FormatearGridProductos()
        {
            if (dgvInsumos.Columns.Contains("ID"))
            {
                dgvInsumos.Columns["ID"].Visible = false;
            }

            foreach (string columna in new[] { "Cantidad", "Stock mínimo", "Stock mÃ­nimo" })
            {
                if (dgvInsumos.Columns.Contains(columna))
                {
                    dgvInsumos.Columns[columna].DefaultCellStyle.Format = "N2";
                    dgvInsumos.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            foreach (string columna in new[] { "Costo unitario", "Valor stock" })
            {
                if (dgvInsumos.Columns.Contains(columna))
                {
                    dgvInsumos.Columns[columna].DefaultCellStyle.Format = "C2";
                    dgvInsumos.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            foreach (string columna in new[] { "Fecha vencimiento", "Fecha" })
            {
                if (dgvInsumos.Columns.Contains(columna))
                {
                    dgvInsumos.Columns[columna].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
        }

        private void FormatearGridMovimientos()
        {
            foreach (string columna in new[] { "ID", "ID Insumo" })
            {
                if (dgvMovimientos.Columns.Contains(columna))
                {
                    dgvMovimientos.Columns[columna].Visible = false;
                }
            }

            foreach (string columna in new[] { "Cantidad", "Costo unitario", "Total" })
            {
                if (dgvMovimientos.Columns.Contains(columna))
                {
                    dgvMovimientos.Columns[columna].DefaultCellStyle.Format =
                        columna == "Cantidad" ? "N2" : "C2";
                    dgvMovimientos.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvMovimientos.Columns.Contains("Fecha"))
            {
                dgvMovimientos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private static void ConfigurarGrid(DataGridView grid)
        {
            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 88, 104);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
        }

        private static Panel CrearPanelBase()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };
        }

        private static Panel CrearTarjeta(string titulo, Label valor)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(10, 8, 10, 8),
                BackColor = Color.White
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = Color.FromArgb(92, 104, 116),
                Font = new Font("Segoe UI", 8.5F)
            };

            valor.Dock = DockStyle.Fill;
            valor.TextAlign = ContentAlignment.MiddleLeft;
            valor.ForeColor = Color.FromArgb(38, 92, 74);
            valor.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            valor.Text = "0";

            panel.Controls.Add(valor);
            panel.Controls.Add(lblTitulo);
            return panel;
        }

        private static Control CrearCampo(string etiqueta, Control control, int ancho)
        {
            Panel panel = new Panel
            {
                Width = ancho,
                Height = 76,
                Margin = new Padding(0, 0, 10, 0)
            };

            Label label = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 77, 91)
            };

            control.Dock = DockStyle.Top;
            control.Height = 31;
            panel.Controls.Add(control);
            panel.Controls.Add(label);
            return panel;
        }

        private static void ConfigurarCombo(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Height = 31;
            combo.Font = new Font("Segoe UI", 9F);
        }

        private static void ConfigurarFecha(DateTimePicker fecha)
        {
            fecha.Format = DateTimePickerFormat.Short;
            fecha.Height = 31;
            fecha.Font = new Font("Segoe UI", 9F);
        }

        private static Button CrearBoton(string texto, Color color, int ancho)
        {
            return new Button
            {
                Text = texto,
                Width = ancho,
                Height = 32,
                Margin = new Padding(0, 22, 8, 0),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
        }

    }
}

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
    public partial class FrmReporteFinanciero : Form
    {
        private DataTable movimientosBase = new DataTable();
        private DataTable movimientosFiltrados = new DataTable();
        private bool cargandoFiltros;
        private bool formularioCargado;
        private int filaImpresion;

        public FrmReporteFinanciero()
        {
            InitializeComponent();
            ConfigurarGrid();
            ConfigurarEventos();
            Load += FrmReporteFinanciero_Load;
        }

        private void ConfigurarEventos()
        {
            btnCerrar.Click += (_, _) => Close();
            btnVolver.Click += (_, _) => Close();
            btnAplicar.Click += (_, _) => CargarDatos();
            btnRecargar.Click += (_, _) => CargarDatos();
            btnLimpiar.Click += (_, _) => LimpiarFiltros();
            btnExportar.Click += (_, _) => ExportarCsv();
            btnImprimir.Click += (_, _) => MostrarVistaImpresion();

            cmbPeriodo.SelectedIndexChanged += (_, _) =>
            {
                ActualizarRangoDesdePeriodo();
                if (formularioCargado && !cargandoFiltros)
                {
                    CargarDatos();
                }
            };

            cmbTipo.SelectedIndexChanged += FiltroCambiado;
            cmbEstado.SelectedIndexChanged += FiltroCambiado;
            cmbOrigen.SelectedIndexChanged += FiltroCambiado;
            cmbCategoria.SelectedIndexChanged += FiltroCambiado;
            cmbVistaGrafico.SelectedIndexChanged += (_, _) => ActualizarGrafico();
            dgvMovimientos.DataBindingComplete += (_, _) => FormatearColumnas();
            dgvMovimientos.CellFormatting += dgvMovimientos_CellFormatting;
            tableResumen.Resize += (_, _) => ActualizarFormatoTarjetas();

            txtBuscar.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CargarDatos();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void FiltroCambiado(object? sender, EventArgs e)
        {
            if (!cargandoFiltros)
            {
                AplicarFiltros();
            }
        }

        private void FrmReporteFinanciero_Load(object? sender, EventArgs e)
        {
            ConfigurarCombos();
            CargarDatos();
            formularioCargado = true;
        }

        private void ConfigurarCombos()
        {
            cargandoFiltros = true;
            try
            {
                cmbPeriodo.Items.Clear();
                cmbPeriodo.Items.AddRange(new object[]
                {
                    "Este mes",
                    "Mes anterior",
                    "Últimos 30 días",
                    "Este año",
                    "Todo el historial",
                    "Personalizado"
                });

                cmbTipo.Items.Clear();
                cmbTipo.Items.AddRange(new object[] { "Todos", "Ingreso", "Gasto" });
                cmbEstado.Items.Clear();
                cmbEstado.Items.AddRange(new object[] { "Todos", "Pagado", "Pendiente" });
                cmbOrigen.Items.Clear();
                cmbOrigen.Items.Add("Todos");
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.Add("Todos");
                cmbVistaGrafico.Items.Clear();
                cmbVistaGrafico.Items.AddRange(new object[]
                {
                    "Flujo por período",
                    "Gastos por categoría",
                    "Ingresos por categoría"
                });

                cmbPeriodo.SelectedItem = "Este año";
                cmbTipo.SelectedIndex = 0;
                cmbEstado.SelectedIndex = 0;
                cmbOrigen.SelectedIndex = 0;
                cmbCategoria.SelectedIndex = 0;
                cmbVistaGrafico.SelectedIndex = 0;
                ActualizarRangoDesdePeriodo();
            }
            finally
            {
                cargandoFiltros = false;
            }
        }

        private void ActualizarRangoDesdePeriodo()
        {
            string periodo = cmbPeriodo.SelectedItem?.ToString() ?? "Este año";
            DateTime hoy = DateTime.Today;

            switch (periodo)
            {
                case "Este mes":
                    dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
                    dtpHasta.Value = hoy;
                    break;
                case "Mes anterior":
                    DateTime mesAnterior = hoy.AddMonths(-1);
                    dtpDesde.Value = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
                    dtpHasta.Value = new DateTime(hoy.Year, hoy.Month, 1).AddDays(-1);
                    break;
                case "Últimos 30 días":
                    dtpDesde.Value = hoy.AddDays(-29);
                    dtpHasta.Value = hoy;
                    break;
                case "Todo el historial":
                    dtpDesde.Value = new DateTime(1900, 1, 1);
                    dtpHasta.Value = new DateTime(2100, 12, 31);
                    break;
                case "Personalizado":
                    break;
                default:
                    dtpDesde.Value = new DateTime(hoy.Year, 1, 1);
                    dtpHasta.Value = hoy;
                    break;
            }

            bool personalizado = periodo == "Personalizado";
            dtpDesde.Enabled = personalizado;
            dtpHasta.Enabled = personalizado;
        }

        private void ConfigurarGrid()
        {
            dgvMovimientos.AutoGenerateColumns = true;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 76, 94);
            dgvMovimientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvMovimientos.DefaultCellStyle.Font = new Font("Segoe UI", 8.8F);
            dgvMovimientos.DefaultCellStyle.ForeColor = Color.FromArgb(50, 60, 70);
            dgvMovimientos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 126, 148);
            dgvMovimientos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMovimientos.RowsDefaultCellStyle.BackColor = Color.White;
            dgvMovimientos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 252);
            dgvMovimientos.GridColor = Color.FromArgb(225, 230, 235);
        }

        private void CargarDatos()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                    "Reporte financiero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                movimientosBase = BLL_Finanzas.ListarMovimientos(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    string.Empty);
                CargarFiltrosDinamicos();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                movimientosBase = new DataTable();
                movimientosFiltrados = new DataTable();
                dgvMovimientos.DataSource = null;
                LimpiarResumen();
                LimpiarGrafico("No fue posible cargar los datos financieros.");
                MessageBox.Show("Error al cargar el reporte financiero: " + ex.Message,
                    "Reporte financiero", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarFiltrosDinamicos()
        {
            cargandoFiltros = true;
            try
            {
                CargarComboDinamico(cmbOrigen, "Origen");
                CargarComboDinamico(cmbCategoria, "Categoria");
            }
            finally
            {
                cargandoFiltros = false;
            }
        }

        private void CargarComboDinamico(ComboBox combo, string columna)
        {
            string seleccion = combo.SelectedItem?.ToString() ?? "Todos";
            combo.Items.Clear();
            combo.Items.Add("Todos");

            if (movimientosBase.Columns.Contains(columna))
            {
                IEnumerable<string> valores = movimientosBase.AsEnumerable()
                    .Select(fila => ObtenerTexto(fila, columna))
                    .Where(valor => !string.IsNullOrWhiteSpace(valor))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(valor => valor);

                foreach (string valor in valores)
                {
                    combo.Items.Add(valor);
                }
            }

            combo.SelectedItem = combo.Items.Contains(seleccion) ? seleccion : "Todos";
        }

        private void AplicarFiltros()
        {
            if (movimientosBase == null || movimientosBase.Columns.Count == 0)
            {
                return;
            }

            IEnumerable<DataRow> filas = movimientosBase.AsEnumerable();
            filas = FiltrarPorCombo(filas, cmbTipo, "Tipo");
            filas = FiltrarPorCombo(filas, cmbEstado, "Estado");
            filas = FiltrarPorCombo(filas, cmbOrigen, "Origen");
            filas = FiltrarPorCombo(filas, cmbCategoria, "Categoria");

            string busqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                filas = filas.Where(fila =>
                    Contiene(fila, "Categoria", busqueda) ||
                    Contiene(fila, "Descripcion", busqueda) ||
                    Contiene(fila, "Metodo", busqueda) ||
                    Contiene(fila, "Referencia", busqueda) ||
                    Contiene(fila, "Origen", busqueda));
            }

            movimientosFiltrados = movimientosBase.Clone();
            foreach (DataRow fila in filas)
            {
                movimientosFiltrados.ImportRow(fila);
            }

            dgvMovimientos.DataSource = movimientosFiltrados;
            FormatearColumnas();
            ActualizarResumen();
            ActualizarGrafico();
        }

        private static IEnumerable<DataRow> FiltrarPorCombo(
            IEnumerable<DataRow> filas,
            ComboBox combo,
            string columna)
        {
            string seleccion = combo.SelectedItem?.ToString() ?? "Todos";
            return seleccion.Equals("Todos", StringComparison.OrdinalIgnoreCase)
                ? filas
                : filas.Where(fila => ObtenerTexto(fila, columna)
                    .Equals(seleccion, StringComparison.OrdinalIgnoreCase));
        }

        private void LimpiarFiltros()
        {
            cargandoFiltros = true;
            try
            {
                txtBuscar.Clear();
                cmbPeriodo.SelectedItem = "Este año";
                cmbTipo.SelectedIndex = cmbTipo.Items.Count > 0 ? 0 : -1;
                cmbEstado.SelectedIndex = cmbEstado.Items.Count > 0 ? 0 : -1;
                cmbOrigen.SelectedIndex = cmbOrigen.Items.Count > 0 ? 0 : -1;
                cmbCategoria.SelectedIndex = cmbCategoria.Items.Count > 0 ? 0 : -1;
                cmbVistaGrafico.SelectedIndex = cmbVistaGrafico.Items.Count > 0 ? 0 : -1;
                ActualizarRangoDesdePeriodo();
            }
            finally
            {
                cargandoFiltros = false;
            }

            CargarDatos();
        }

        private void ActualizarResumen()
        {
            Entitie_ResumenFinanciero resumen = BLL_Finanzas.CalcularResumen(movimientosFiltrados);
            decimal margen = resumen.Ingresos > 0m
                ? Math.Round(resumen.Balance / resumen.Ingresos * 100m, 1)
                : 0m;

            EstablecerValorMoneda(lblIngresos, resumen.Ingresos);
            EstablecerValorMoneda(lblGastos, resumen.Gastos);
            EstablecerValorMoneda(lblResultado, resumen.Balance);
            EstablecerValorMoneda(lblCobrar, resumen.PendienteCobro);
            EstablecerValorMoneda(lblPagar, resumen.PendientePago);
            lblMargen.Text = resumen.Ingresos > 0m ? margen.ToString("N1") + " %" : "-";
            lblResultado.ForeColor = resumen.Balance < 0m
                ? Color.FromArgb(176, 58, 46)
                : Color.FromArgb(35, 115, 70);
            lblMargen.ForeColor = margen < 0m
                ? Color.FromArgb(176, 58, 46)
                : Color.FromArgb(44, 76, 94);

            int manuales = movimientosFiltrados.AsEnumerable()
                .Count(fila => ObtenerTexto(fila, "Origen") == "Manual");
            int automaticos = movimientosFiltrados.Rows.Count - manuales;
            lblTotalResultados.Text =
                $"{movimientosFiltrados.Rows.Count:N0} movimientos | {manuales:N0} manuales | {automaticos:N0} automáticos";
        }

        private void LimpiarResumen()
        {
            EstablecerValorMoneda(lblIngresos, 0m);
            EstablecerValorMoneda(lblGastos, 0m);
            EstablecerValorMoneda(lblResultado, 0m);
            EstablecerValorMoneda(lblCobrar, 0m);
            EstablecerValorMoneda(lblPagar, 0m);
            lblMargen.Text = "-";
            lblTotalResultados.Text = "0 movimientos";
        }

        private void FormatearColumnas()
        {
            if (dgvMovimientos.Columns.Count == 0)
            {
                return;
            }

            OcultarColumna("ID");
            OcultarColumna("Editable");
            ConfigurarColumna("Fecha", "Fecha", 84, "dd/MM/yyyy");
            ConfigurarColumna("Tipo", "Tipo", 72);
            ConfigurarColumna("Categoria", "Categoría", 120);
            ConfigurarColumna("Descripcion", "Descripción", 205);
            ConfigurarColumna("Monto", "Monto", 112, "RD$ #,##0.00");
            ConfigurarColumna("Metodo", "Método", 98);
            ConfigurarColumna("Estado", "Estado", 82);
            ConfigurarColumna("Origen", "Origen", 98);
            ConfigurarColumna("Referencia", "Referencia", 125);

            foreach (DataGridViewColumn columna in dgvMovimientos.Columns)
            {
                columna.DefaultCellStyle.NullValue = "-";
                columna.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void OcultarColumna(string nombre)
        {
            DataGridViewColumn? columna = dgvMovimientos.Columns[nombre];
            if (columna != null)
            {
                columna.Visible = false;
            }
        }

        private void ConfigurarColumna(string nombre, string titulo, int ancho, string? formato = null)
        {
            DataGridViewColumn? columna = dgvMovimientos.Columns[nombre];
            if (columna == null)
            {
                return;
            }

            columna.HeaderText = titulo;
            columna.Width = ancho;
            columna.Visible = true;
            if (!string.IsNullOrWhiteSpace(formato))
            {
                columna.DefaultCellStyle.Format = formato;
            }
            if (nombre == "Monto")
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dgvMovimientos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewCellStyle? estilo = e.CellStyle;
            if (estilo == null)
            {
                return;
            }

            DataGridViewRow fila = dgvMovimientos.Rows[e.RowIndex];
            string tipo = Convert.ToString(fila.Cells["Tipo"].Value) ?? string.Empty;
            string estado = Convert.ToString(fila.Cells["Estado"].Value) ?? string.Empty;
            string origen = Convert.ToString(fila.Cells["Origen"].Value) ?? string.Empty;
            string columna = dgvMovimientos.Columns[e.ColumnIndex].Name;

            if (columna == "Tipo" || columna == "Monto")
            {
                estilo.ForeColor = tipo == "Ingreso"
                    ? Color.FromArgb(35, 115, 70)
                    : Color.FromArgb(176, 58, 46);
            }
            if (columna == "Estado" && estado == "Pendiente")
            {
                estilo.BackColor = Color.FromArgb(249, 222, 160);
                estilo.ForeColor = Color.FromArgb(117, 76, 15);
            }
            if (columna == "Origen" && origen != "Manual")
            {
                estilo.ForeColor = Color.FromArgb(52, 101, 164);
            }
        }

        private void ActualizarGrafico()
        {
            if (movimientosFiltrados == null || movimientosFiltrados.Columns.Count == 0)
            {
                LimpiarGrafico("No hay datos para graficar.");
                return;
            }

            string vista = cmbVistaGrafico.SelectedItem?.ToString() ?? "Flujo por período";
            if (vista == "Gastos por categoría")
            {
                CrearGraficoCategorias("Gasto");
            }
            else if (vista == "Ingresos por categoría")
            {
                CrearGraficoCategorias("Ingreso");
            }
            else
            {
                CrearGraficoFlujo();
            }
        }

        private void PrepararGrafico()
        {
            chartFinanciero.Series.Clear();
            ChartArea area = chartFinanciero.ChartAreas["AreaFinanciera"];
            area.AxisX.CustomLabels.Clear();
            area.AxisX.Minimum = double.NaN;
            area.AxisX.Maximum = double.NaN;
            area.AxisX.Interval = 0D;
            area.AxisX.LabelStyle.Angle = 0;
            area.AxisX.LabelStyle.Format = string.Empty;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.Minimum = double.NaN;
            area.AxisY.Maximum = double.NaN;
            area.AxisY.Interval = 0D;
            area.AxisY.LabelStyle.Format = "RD$ #,##0;-RD$ #,##0;RD$ 0";
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            area.AxisY.IsStartedFromZero = true;
            chartFinanciero.Legends["Leyenda"].Enabled = true;
        }

        private void CrearGraficoFlujo()
        {
            PrepararGrafico();
            int dias = Math.Max(0, (dtpHasta.Value.Date - dtpDesde.Value.Date).Days);
            Func<DateTime, DateTime> agrupar;
            Func<DateTime, string> etiquetar;

            if (dias <= 62)
            {
                agrupar = fecha => fecha.Date;
                etiquetar = fecha => fecha.ToString("dd/MM");
            }
            else if (dias <= 730)
            {
                agrupar = fecha => new DateTime(fecha.Year, fecha.Month, 1);
                etiquetar = fecha => fecha.ToString("MMM yyyy", CultureInfo.CurrentCulture);
            }
            else
            {
                agrupar = fecha => new DateTime(fecha.Year, 1, 1);
                etiquetar = fecha => fecha.ToString("yyyy");
            }

            var grupos = movimientosFiltrados.AsEnumerable()
                .Where(fila => ObtenerTexto(fila, "Estado") == "Pagado" &&
                               ObtenerFecha(fila, "Fecha").HasValue &&
                               ObtenerDecimal(fila, "Monto").HasValue)
                .GroupBy(fila => agrupar(ObtenerFecha(fila, "Fecha").GetValueOrDefault()))
                .OrderBy(grupo => grupo.Key)
                .Select(grupo => new
                {
                    Fecha = grupo.Key,
                    Ingresos = grupo.Where(fila => ObtenerTexto(fila, "Tipo") == "Ingreso")
                        .Sum(fila => ObtenerDecimal(fila, "Monto").GetValueOrDefault()),
                    Gastos = grupo.Where(fila => ObtenerTexto(fila, "Tipo") == "Gasto")
                        .Sum(fila => ObtenerDecimal(fila, "Monto").GetValueOrDefault())
                })
                .ToList();

            Series ingresos = CrearSerie("Ingresos", SeriesChartType.Column, Color.FromArgb(45, 125, 94));
            Series gastos = CrearSerie("Gastos", SeriesChartType.Column, Color.FromArgb(190, 70, 58));
            Series resultado = CrearSerie("Resultado", SeriesChartType.Line, Color.FromArgb(52, 101, 164));
            resultado.BorderWidth = 3;
            resultado.MarkerStyle = MarkerStyle.Circle;
            resultado.MarkerSize = 6;

            foreach (var grupo in grupos)
            {
                string etiqueta = etiquetar(grupo.Fecha);
                ingresos.Points.AddXY(etiqueta, grupo.Ingresos);
                gastos.Points.AddXY(etiqueta, grupo.Gastos);
                resultado.Points.AddXY(etiqueta, grupo.Ingresos - grupo.Gastos);
            }

            chartFinanciero.Series.Add(ingresos);
            chartFinanciero.Series.Add(gastos);
            chartFinanciero.Series.Add(resultado);
            ChartArea area = chartFinanciero.ChartAreas["AreaFinanciera"];
            area.AxisX.Interval = 1D;
            area.AxisX.LabelStyle.Angle = grupos.Count > 6 ? -45 : 0;
            bool hayResultadoNegativo = grupos.Any(grupo => grupo.Ingresos - grupo.Gastos < 0m);
            area.AxisY.IsStartedFromZero = !hayResultadoNegativo;
            if (!hayResultadoNegativo)
            {
                area.AxisY.Minimum = 0D;
            }

            decimal totalIngresos = grupos.Sum(grupo => grupo.Ingresos);
            decimal totalGastos = grupos.Sum(grupo => grupo.Gastos);
            lblDetalleGrafico.Text = grupos.Count == 0
                ? "No hay movimientos pagados en el filtro actual."
                : $"Pagado: {FormatearMoneda(totalIngresos)} ingresos | {FormatearMoneda(totalGastos)} gastos";
        }

        private void CrearGraficoCategorias(string tipo)
        {
            PrepararGrafico();
            var categorias = movimientosFiltrados.AsEnumerable()
                .Where(fila => ObtenerTexto(fila, "Tipo") == tipo &&
                               ObtenerTexto(fila, "Estado") == "Pagado" &&
                               ObtenerDecimal(fila, "Monto").HasValue)
                .GroupBy(fila => ObtenerTexto(fila, "Categoria"))
                .Select(grupo => new
                {
                    Categoria = string.IsNullOrWhiteSpace(grupo.Key) ? "Sin categoría" : grupo.Key,
                    Total = grupo.Sum(fila => ObtenerDecimal(fila, "Monto").GetValueOrDefault())
                })
                .OrderByDescending(item => item.Total)
                .Take(8)
                .OrderBy(item => item.Total)
                .ToList();

            Series serie = CrearSerie(
                tipo == "Gasto" ? "Gastos" : "Ingresos",
                SeriesChartType.Bar,
                tipo == "Gasto" ? Color.FromArgb(190, 70, 58) : Color.FromArgb(45, 125, 94));
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "N0";

            Color[] colores =
            {
                Color.FromArgb(45, 125, 94),
                Color.FromArgb(52, 101, 164),
                Color.FromArgb(190, 70, 58),
                Color.FromArgb(176, 126, 43),
                Color.FromArgb(96, 85, 128),
                Color.FromArgb(74, 117, 121),
                Color.FromArgb(151, 88, 105),
                Color.FromArgb(91, 99, 111)
            };

            for (int i = 0; i < categorias.Count; i++)
            {
                int indice = serie.Points.AddXY(categorias[i].Categoria, categorias[i].Total);
                serie.Points[indice].Color = colores[i % colores.Length];
                serie.Points[indice].ToolTip =
                    categorias[i].Categoria + ": " + FormatearMoneda(categorias[i].Total);
            }

            chartFinanciero.Series.Add(serie);
            chartFinanciero.Legends["Leyenda"].Enabled = false;
            ChartArea area = chartFinanciero.ChartAreas["AreaFinanciera"];
            area.AxisX.LabelStyle.Format = "RD$ #,##0;-RD$ #,##0;RD$ 0";
            area.AxisX.MajorGrid.Enabled = true;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            area.AxisY.MajorGrid.Enabled = false;

            decimal total = categorias.Sum(item => item.Total);
            lblDetalleGrafico.Text = categorias.Count == 0
                ? $"No hay {tipo.ToLowerInvariant()}s pagados en el filtro actual."
                : $"{tipo}s pagados por categoría: {FormatearMoneda(total)}";
        }

        private static Series CrearSerie(string nombre, SeriesChartType tipo, Color color)
        {
            return new Series(nombre)
            {
                ChartArea = "AreaFinanciera",
                ChartType = tipo,
                Color = color,
                BorderWidth = 2,
                YValueType = ChartValueType.Double
            };
        }

        private void LimpiarGrafico(string mensaje)
        {
            chartFinanciero.Series.Clear();
            lblDetalleGrafico.Text = mensaje;
            chartFinanciero.Invalidate();
        }

        private void ExportarCsv()
        {
            if (movimientosFiltrados.Rows.Count == 0)
            {
                MessageBox.Show("No hay movimientos para exportar.", "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                Title = "Guardar reporte financiero",
                FileName = "Reporte_Financiero_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv"
            };

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                (string Columna, string Titulo)[] columnas = ObtenerColumnasReporte();
                StringBuilder contenido = new StringBuilder();
                contenido.AppendLine(string.Join(";", columnas.Select(item => EscaparCsv(item.Titulo))));
                foreach (DataRow fila in movimientosFiltrados.Rows)
                {
                    contenido.AppendLine(string.Join(";", columnas.Select(item =>
                        EscaparCsv(FormatearValor(fila, item.Columna, false)))));
                }

                File.WriteAllText(dialogo.FileName, contenido.ToString(), new UTF8Encoding(true));
                if (MessageBox.Show("Reporte exportado correctamente. ¿Desea abrir el archivo?",
                    "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el reporte: " + ex.Message,
                    "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarVistaImpresion()
        {
            if (movimientosFiltrados.Rows.Count == 0)
            {
                MessageBox.Show("No hay movimientos para imprimir.", "Imprimir",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using PrintDocument documento = new PrintDocument
            {
                DocumentName = "Reporte financiero"
            };
            documento.DefaultPageSettings.Landscape = true;
            documento.DefaultPageSettings.Margins =
                new System.Drawing.Printing.Margins(40, 40, 45, 45);
            documento.BeginPrint += (_, _) => filaImpresion = 0;
            documento.PrintPage += Documento_PrintPage;

            using PrintPreviewDialog vista = new PrintPreviewDialog
            {
                Document = documento,
                Width = 1100,
                Height = 720,
                StartPosition = FormStartPosition.CenterScreen
            };
            vista.ShowDialog(this);
        }

        private void Documento_PrintPage(object? sender, PrintPageEventArgs e)
        {
            using Font titulo = new Font("Segoe UI", 16F, FontStyle.Bold);
            using Font subtitulo = new Font("Segoe UI", 9F);
            using Font encabezado = new Font("Segoe UI", 8F, FontStyle.Bold);
            using Font normal = new Font("Segoe UI", 7.5F);
            using Brush tintaTitulo = new SolidBrush(Color.FromArgb(44, 76, 94));
            using Brush tinta = new SolidBrush(Color.Black);
            using Brush fondoEncabezado = new SolidBrush(Color.FromArgb(224, 234, 240));
            using Pen borde = new Pen(Color.FromArgb(185, 193, 199));
            using StringFormat formato = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                LineAlignment = StringAlignment.Center
            };

            Graphics? grafico = e.Graphics;
            if (grafico == null)
            {
                e.HasMorePages = false;
                return;
            }

            Entitie_ResumenFinanciero resumen = BLL_Finanzas.CalcularResumen(movimientosFiltrados);
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            grafico.DrawString("Reporte financiero", titulo, tintaTitulo, x, y);
            y += 34;
            grafico.DrawString(
                $"Período: {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy} | " +
                $"Ingresos: {FormatearMoneda(resumen.Ingresos)} | " +
                $"Gastos: {FormatearMoneda(resumen.Gastos)} | " +
                $"Resultado: {FormatearMoneda(resumen.Balance)}",
                subtitulo, tinta, x, y);
            y += 26;

            (string Columna, string Titulo, int Ancho)[] columnas =
            {
                ("Fecha", "Fecha", 70),
                ("Tipo", "Tipo", 65),
                ("Categoria", "Categoría", 110),
                ("Descripcion", "Descripción", 230),
                ("Monto", "Monto", 105),
                ("Estado", "Estado", 75),
                ("Origen", "Origen", 90),
                ("Referencia", "Referencia", 130)
            };

            const int alturaFila = 30;
            int columnaX = x;
            foreach (var columna in columnas)
            {
                Rectangle rectangulo = new Rectangle(columnaX, y, columna.Ancho, alturaFila);
                grafico.FillRectangle(fondoEncabezado, rectangulo);
                grafico.DrawRectangle(borde, rectangulo);
                grafico.DrawString(columna.Titulo, encabezado, tinta, rectangulo, formato);
                columnaX += columna.Ancho;
            }
            y += alturaFila;

            while (filaImpresion < movimientosFiltrados.Rows.Count &&
                   y + alturaFila <= e.MarginBounds.Bottom)
            {
                DataRow fila = movimientosFiltrados.Rows[filaImpresion];
                columnaX = x;
                foreach (var columna in columnas)
                {
                    Rectangle rectangulo = new Rectangle(columnaX, y, columna.Ancho, alturaFila);
                    grafico.DrawRectangle(borde, rectangulo);
                    Rectangle texto = new Rectangle(
                        rectangulo.X + 3, rectangulo.Y, rectangulo.Width - 6, rectangulo.Height);
                    grafico.DrawString(FormatearValor(fila, columna.Columna, true),
                        normal, tinta, texto, formato);
                    columnaX += columna.Ancho;
                }
                filaImpresion++;
                y += alturaFila;
            }

            e.HasMorePages = filaImpresion < movimientosFiltrados.Rows.Count;
        }

        private static (string Columna, string Titulo)[] ObtenerColumnasReporte()
        {
            return new[]
            {
                ("Fecha", "Fecha"),
                ("Tipo", "Tipo"),
                ("Categoria", "Categoría"),
                ("Descripcion", "Descripción"),
                ("Monto", "Monto (RD$)"),
                ("Metodo", "Método"),
                ("Estado", "Estado"),
                ("Origen", "Origen"),
                ("Referencia", "Referencia")
            };
        }

        private static string FormatearValor(DataRow fila, string columna, bool monedaConSimbolo)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return string.Empty;
            }

            object? valor = fila[columna];
            if (valor is DateTime fecha)
            {
                return fecha.ToString("dd/MM/yyyy");
            }
            if (columna == "Monto")
            {
                decimal monto = Convert.ToDecimal(valor);
                return monedaConSimbolo
                    ? FormatearMoneda(monto)
                    : monto.ToString("0.00", CultureInfo.CurrentCulture);
            }
            return Convert.ToString(valor, CultureInfo.CurrentCulture) ?? string.Empty;
        }

        private static string EscaparCsv(string? valor)
        {
            string texto = (valor ?? string.Empty).Replace("\r", " ").Replace("\n", " ");
            return "\"" + texto.Replace("\"", "\"\"") + "\"";
        }

        private static bool Contiene(DataRow fila, string columna, string busqueda)
        {
            return ObtenerTexto(fila, columna)
                .IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string ObtenerTexto(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToString(fila[columna])?.Trim() ?? string.Empty
                : string.Empty;
        }

        private static DateTime? ObtenerFecha(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return null;
            }

            try
            {
                return Convert.ToDateTime(fila[columna]);
            }
            catch
            {
                return null;
            }
        }

        private static decimal? ObtenerDecimal(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return null;
            }

            try
            {
                return Convert.ToDecimal(fila[columna]);
            }
            catch
            {
                return null;
            }
        }

        private static string FormatearMoneda(decimal valor)
        {
            return $"RD$ {valor:N2}";
        }

        private void EstablecerValorMoneda(Label etiqueta, decimal valor)
        {
            etiqueta.Tag = valor;
            etiqueta.Text = FormatearMonedaTarjeta(valor, etiqueta.ClientSize.Width);
            toolTip.SetToolTip(etiqueta, FormatearMoneda(valor));
        }

        private void ActualizarFormatoTarjetas()
        {
            Label[] etiquetas = { lblIngresos, lblGastos, lblResultado, lblCobrar, lblPagar };
            foreach (Label etiqueta in etiquetas)
            {
                if (etiqueta.Tag is decimal valor)
                {
                    etiqueta.Text = FormatearMonedaTarjeta(valor, etiqueta.ClientSize.Width);
                }
            }
        }

        private static string FormatearMonedaTarjeta(decimal valor, int anchoDisponible)
        {
            decimal absoluto = Math.Abs(valor);
            if (anchoDisponible > 0 && anchoDisponible < 140)
            {
                if (absoluto >= 1000000m)
                {
                    return $"RD$ {valor / 1000000m:N2} M";
                }
                if (absoluto >= 100000m)
                {
                    return $"RD$ {valor / 1000m:N1} mil";
                }
            }

            return FormatearMoneda(valor);
        }
    }
}

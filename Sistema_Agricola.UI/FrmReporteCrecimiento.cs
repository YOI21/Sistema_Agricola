using Sistema_Agricola.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteCrecimiento : Form
    {
        private DataTable datosCrecimiento = new DataTable();
        private DataTable datosFiltrados = new DataTable();
        private bool cargandoFiltros;
        private bool actualizandoGrid;
        private int filaImpresion;

        public FrmReporteCrecimiento()
        {
            InitializeComponent();
            ConfigurarGrid();
            ConfigurarGrafico();
            ConfigurarEventos();
            Load += FrmReporteCrecimiento_Load;
        }

        private void ConfigurarEventos()
        {
            btnCerrar.Click += (_, _) => Close();
            btnVolver.Click += (_, _) => Close();
            btnRecargar.Click += (_, _) => CargarDatos();
            btnAplicar.Click += (_, _) => AplicarFiltros();
            btnLimpiar.Click += (_, _) => LimpiarFiltros();
            btnExportar.Click += (_, _) => ExportarCsv();
            btnImprimir.Click += (_, _) => MostrarVistaImpresion();

            chkRangoFechas.CheckedChanged += (_, _) =>
            {
                dtpDesde.Enabled = chkRangoFechas.Checked;
                dtpHasta.Enabled = chkRangoFechas.Checked;
                if (!cargandoFiltros)
                {
                    AplicarFiltros(false);
                }
            };

            cmbCultivo.SelectedIndexChanged += FiltroCambiado;
            cmbEtapa.SelectedIndexChanged += FiltroCambiado;
            cmbSalud.SelectedIndexChanged += FiltroCambiado;
            cmbEvaluacion.SelectedIndexChanged += FiltroCambiado;
            dgvCrecimiento.SelectionChanged += (_, _) => ActualizarGraficoSeleccionado();
            dgvCrecimiento.DataBindingComplete += (_, _) => FormatearColumnas();
            dgvCrecimiento.CellFormatting += dgvCrecimiento_CellFormatting;

            txtBuscar.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AplicarFiltros();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void FiltroCambiado(object? sender, EventArgs e)
        {
            if (!cargandoFiltros)
            {
                AplicarFiltros(false);
            }
        }

        private void FrmReporteCrecimiento_Load(object? sender, EventArgs e)
        {
            CargarDatos();
        }

        private void ConfigurarGrid()
        {
            dgvCrecimiento.AutoGenerateColumns = true;
            dgvCrecimiento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCrecimiento.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(38, 92, 74);
            dgvCrecimiento.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCrecimiento.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvCrecimiento.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvCrecimiento.DefaultCellStyle.Font = new Font("Segoe UI", 8.8F);
            dgvCrecimiento.DefaultCellStyle.ForeColor = Color.FromArgb(50, 60, 70);
            dgvCrecimiento.DefaultCellStyle.SelectionBackColor = Color.FromArgb(83, 145, 117);
            dgvCrecimiento.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCrecimiento.RowsDefaultCellStyle.BackColor = Color.White;
            dgvCrecimiento.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 252);
            dgvCrecimiento.GridColor = Color.FromArgb(225, 230, 235);
        }

        private void ConfigurarGrafico()
        {
            chartCrecimiento.Series.Clear();

            Series real = new Series("Crecimiento real")
            {
                ChartArea = "AreaCrecimiento",
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(45, 125, 94),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                XValueType = ChartValueType.DateTime,
                YValueType = ChartValueType.Double
            };

            Series esperado = new Series("Avance esperado")
            {
                ChartArea = "AreaCrecimiento",
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                BorderDashStyle = ChartDashStyle.Dash,
                Color = Color.FromArgb(52, 101, 164),
                MarkerStyle = MarkerStyle.Square,
                MarkerSize = 5,
                XValueType = ChartValueType.DateTime,
                YValueType = ChartValueType.Double
            };

            chartCrecimiento.Series.Add(real);
            chartCrecimiento.Series.Add(esperado);
        }

        private void CargarDatos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                datosCrecimiento = BLL_SeguimientoCrecimiento.Obtener_Reporte_Crecimiento();
                PrepararFiltros();
                AplicarFiltros(false);
            }
            catch (Exception ex)
            {
                datosCrecimiento = new DataTable();
                datosFiltrados = new DataTable();
                dgvCrecimiento.DataSource = null;
                LimpiarResumen();
                LimpiarGrafico("No fue posible cargar los datos.");
                MessageBox.Show("Error al cargar el reporte de crecimiento: " + ex.Message,
                    "Reporte de crecimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void PrepararFiltros()
        {
            cargandoFiltros = true;
            try
            {
                CargarCultivos();
                CargarComboTexto(cmbEtapa, "Etapa_Crecimiento");
                CargarComboTexto(cmbSalud, "Estado_Salud");
                CargarComboTexto(cmbEvaluacion, "Evaluacion");
                PrepararRangoFechas();
                txtBuscar.Clear();
                chkRangoFechas.Checked = false;
                dtpDesde.Enabled = false;
                dtpHasta.Enabled = false;
            }
            finally
            {
                cargandoFiltros = false;
            }
        }

        private void CargarCultivos()
        {
            cmbCultivo.Items.Clear();
            cmbCultivo.Items.Add(new OpcionCultivo(null, "Todos los cultivos"));

            if (datosCrecimiento.Columns.Contains("ID_Cultivo"))
            {
                IEnumerable<IGrouping<int, DataRow>> grupos = datosCrecimiento.AsEnumerable()
                    .Where(fila => ObtenerEntero(fila, "ID_Cultivo").HasValue)
                    .GroupBy(fila => ObtenerEntero(fila, "ID_Cultivo").GetValueOrDefault())
                    .OrderBy(grupo => ObtenerTexto(grupo.First(), "Cultivo"))
                    .ThenBy(grupo => grupo.Key);

                foreach (IGrouping<int, DataRow> grupo in grupos)
                {
                    DataRow fila = grupo.First();
                    string cultivo = ObtenerTexto(fila, "Cultivo");
                    string terreno = ObtenerTexto(fila, "Terreno");
                    string fecha = ObtenerFecha(fila, "Fecha_Plantacion")?.ToString("dd/MM/yyyy") ?? "Sin fecha";
                    string texto = $"{cultivo} | {fecha}";

                    if (!string.IsNullOrWhiteSpace(terreno) &&
                        !terreno.Equals("Sin terreno asignado", StringComparison.OrdinalIgnoreCase))
                    {
                        texto += $" | {terreno}";
                    }

                    cmbCultivo.Items.Add(new OpcionCultivo(grupo.Key, texto));
                }
            }

            cmbCultivo.SelectedIndex = 0;
        }

        private void CargarComboTexto(ComboBox combo, string columna)
        {
            combo.Items.Clear();
            combo.Items.Add("Todos");

            if (datosCrecimiento.Columns.Contains(columna))
            {
                IEnumerable<string> valores = datosCrecimiento.AsEnumerable()
                    .Select(fila => ObtenerTexto(fila, columna))
                    .Where(valor => !string.IsNullOrWhiteSpace(valor))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(valor => valor);

                foreach (string valor in valores)
                {
                    combo.Items.Add(valor);
                }
            }

            combo.SelectedIndex = 0;
        }

        private void PrepararRangoFechas()
        {
            List<DateTime> fechas = datosCrecimiento.AsEnumerable()
                .Select(fila => ObtenerFecha(fila, "Fecha_Registro"))
                .Where(fecha => fecha.HasValue)
                .Select(fecha => fecha.GetValueOrDefault().Date)
                .ToList();

            dtpDesde.Value = fechas.Count > 0 ? fechas.Min() : DateTime.Today.AddMonths(-6);
            dtpHasta.Value = fechas.Count > 0 ? fechas.Max() : DateTime.Today;
        }

        private void AplicarFiltros(bool mostrarMensajes = true)
        {
            if (datosCrecimiento == null || datosCrecimiento.Columns.Count == 0)
            {
                return;
            }

            if (chkRangoFechas.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                if (mostrarMensajes)
                {
                    MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                        "Filtros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            int? idSeleccionado = ObtenerIdCultivoSeleccionadoEnGrid();
            IEnumerable<DataRow> filas = datosCrecimiento.AsEnumerable();

            if (cmbCultivo.SelectedItem is OpcionCultivo cultivo && cultivo.Id.HasValue)
            {
                int idCultivo = cultivo.Id.GetValueOrDefault();
                filas = filas.Where(fila => ObtenerEntero(fila, "ID_Cultivo") == idCultivo);
            }

            filas = FiltrarPorTexto(filas, cmbEtapa, "Etapa_Crecimiento");
            filas = FiltrarPorTexto(filas, cmbSalud, "Estado_Salud");
            filas = FiltrarPorTexto(filas, cmbEvaluacion, "Evaluacion");

            if (chkRangoFechas.Checked)
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;
                filas = filas.Where(fila =>
                {
                    DateTime? fecha = ObtenerFecha(fila, "Fecha_Registro");
                    return fecha.HasValue && fecha.Value.Date >= desde && fecha.Value.Date <= hasta;
                });
            }

            string busqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                filas = filas.Where(fila =>
                    Contiene(fila, "Siembra", busqueda) ||
                    Contiene(fila, "Cultivo", busqueda) ||
                    Contiene(fila, "Terreno", busqueda) ||
                    Contiene(fila, "Tipo_Cultivo", busqueda) ||
                    Contiene(fila, "Categoria", busqueda) ||
                    Contiene(fila, "Etapa_Crecimiento", busqueda) ||
                    Contiene(fila, "Estado_Salud", busqueda) ||
                    Contiene(fila, "Problemas_Detectados", busqueda) ||
                    Contiene(fila, "Acciones_Tomadas", busqueda));
            }

            datosFiltrados = datosCrecimiento.Clone();
            foreach (DataRow fila in filas)
            {
                datosFiltrados.ImportRow(fila);
            }

            actualizandoGrid = true;
            dgvCrecimiento.DataSource = datosFiltrados;
            FormatearColumnas();
            RestaurarSeleccion(idSeleccionado);
            actualizandoGrid = false;

            ActualizarResumen();
            ActualizarGraficoSeleccionado();
        }

        private static IEnumerable<DataRow> FiltrarPorTexto(
            IEnumerable<DataRow> filas, ComboBox combo, string columna)
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
                cmbCultivo.SelectedIndex = cmbCultivo.Items.Count > 0 ? 0 : -1;
                cmbEtapa.SelectedIndex = cmbEtapa.Items.Count > 0 ? 0 : -1;
                cmbSalud.SelectedIndex = cmbSalud.Items.Count > 0 ? 0 : -1;
                cmbEvaluacion.SelectedIndex = cmbEvaluacion.Items.Count > 0 ? 0 : -1;
                chkRangoFechas.Checked = false;
                dtpDesde.Enabled = false;
                dtpHasta.Enabled = false;
                PrepararRangoFechas();
            }
            finally
            {
                cargandoFiltros = false;
            }

            AplicarFiltros(false);
        }

        private void ActualizarResumen()
        {
            List<DataRow> filas = datosFiltrados.AsEnumerable().ToList();
            int cultivos = filas.Select(fila => ObtenerEntero(fila, "ID_Cultivo"))
                .Where(id => id.HasValue)
                .Select(id => id.GetValueOrDefault())
                .Distinct()
                .Count();
            int seguimientos = filas.Count(fila => ObtenerEntero(fila, "ID_Seguimiento").HasValue);

            List<decimal> crecimientos = filas
                .Select(fila => ObtenerDecimal(fila, "Porcentaje_Crecimiento"))
                .Where(valor => valor.HasValue)
                .Select(valor => valor.GetValueOrDefault())
                .ToList();
            List<decimal> alturas = filas
                .Select(fila => ObtenerDecimal(fila, "Altura_Planta"))
                .Where(valor => valor.HasValue)
                .Select(valor => valor.GetValueOrDefault())
                .ToList();

            List<DataRow> ultimos = filas
                .Where(fila => ObtenerEntero(fila, "ID_Cultivo").HasValue)
                .GroupBy(fila => ObtenerEntero(fila, "ID_Cultivo").GetValueOrDefault())
                .Select(grupo => grupo
                    .OrderByDescending(fila => ObtenerFecha(fila, "Fecha_Registro") ?? DateTime.MinValue)
                    .First())
                .ToList();

            int porRevisar = ultimos.Count(fila =>
            {
                string estado = ObtenerTexto(fila, "Evaluacion");
                return estado == "Requiere atención" || estado == "En seguimiento" ||
                       estado == "Fecha inconsistente";
            });
            int sinSeguimiento = ultimos.Count(fila =>
                ObtenerTexto(fila, "Evaluacion") == "Sin seguimiento");

            lblCultivos.Text = cultivos.ToString("N0");
            lblSeguimientos.Text = seguimientos.ToString("N0");
            lblPromedio.Text = crecimientos.Count > 0 ? crecimientos.Average().ToString("N1") + " %" : "-";
            lblAltura.Text = alturas.Count > 0 ? alturas.Average().ToString("N1") + " cm" : "-";
            lblRevisar.Text = porRevisar.ToString("N0");
            lblSinSeguimiento.Text = sinSeguimiento.ToString("N0");
            lblTotalResultados.Text = $"{filas.Count:N0} filas | {cultivos:N0} cultivos";
        }

        private void LimpiarResumen()
        {
            lblCultivos.Text = "0";
            lblSeguimientos.Text = "0";
            lblPromedio.Text = "-";
            lblAltura.Text = "-";
            lblRevisar.Text = "0";
            lblSinSeguimiento.Text = "0";
            lblTotalResultados.Text = "0 resultados";
        }

        private void FormatearColumnas()
        {
            if (dgvCrecimiento.Columns.Count == 0)
            {
                return;
            }

            OcultarColumna("ID_Seguimiento");
            OcultarColumna("ID_Cultivo");
            OcultarColumna("Tipo_Cultivo");
            OcultarColumna("Categoria");
            OcultarColumna("Fecha_Plantacion");
            OcultarColumna("Fecha_Cosecha_Estimada");
            OcultarColumna("Estado_Cultivo");
            OcultarColumna("Observaciones_Seguimiento");
            OcultarColumna("Dias_Desde_Siembra");

            OcultarColumna("Cultivo");
            ConfigurarColumna("Siembra", "Siembra", 125);
            ConfigurarColumna("Terreno", "Terreno", 115);
            ConfigurarColumna("Fecha_Registro", "Fecha", 82, "dd/MM/yyyy");
            ConfigurarColumna("Etapa_Crecimiento", "Etapa", 130);
            ConfigurarColumna("Altura_Planta", "Altura (cm)", 82, "N1");
            ConfigurarColumna("Num_Hojas", "Hojas", 58, "N0");
            ConfigurarColumna("Estado_Salud", "Salud", 85);
            ConfigurarColumna("Porcentaje_Crecimiento", "Real (%)", 78, "N1");
            ConfigurarColumna("Crecimiento_Esperado", "Esperado (%)", 92, "N1");
            ConfigurarColumna("Desviacion", "Diferencia", 82, "+0.0;-0.0;0.0");
            ConfigurarColumna("Evaluacion", "Evaluación", 126);
            ConfigurarColumna("Problemas_Detectados", "Problemas detectados", 170);
            ConfigurarColumna("Acciones_Tomadas", "Acciones tomadas", 170);

            foreach (DataGridViewColumn columna in dgvCrecimiento.Columns)
            {
                columna.DefaultCellStyle.NullValue = "-";
                columna.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void OcultarColumna(string nombre)
        {
            DataGridViewColumn? columna = dgvCrecimiento.Columns[nombre];
            if (columna != null)
            {
                columna.Visible = false;
            }
        }

        private void ConfigurarColumna(string nombre, string titulo, int ancho, string? formato = null)
        {
            DataGridViewColumn? columna = dgvCrecimiento.Columns[nombre];
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
        }

        private void dgvCrecimiento_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string nombre = dgvCrecimiento.Columns[e.ColumnIndex].Name;
            string valor = e.Value?.ToString() ?? string.Empty;
            DataGridViewCellStyle? estilo = e.CellStyle;
            if (estilo == null)
            {
                return;
            }

            if (nombre == "Evaluacion")
            {
                switch (valor)
                {
                    case "Requiere atención":
                    case "Fecha inconsistente":
                        estilo.BackColor = Color.FromArgb(190, 60, 55);
                        estilo.ForeColor = Color.White;
                        break;
                    case "En seguimiento":
                        estilo.BackColor = Color.FromArgb(245, 183, 79);
                        estilo.ForeColor = Color.FromArgb(45, 45, 45);
                        break;
                    case "En objetivo":
                        estilo.BackColor = Color.FromArgb(218, 239, 226);
                        estilo.ForeColor = Color.FromArgb(28, 104, 65);
                        break;
                    case "Adelantado":
                        estilo.BackColor = Color.FromArgb(218, 231, 247);
                        estilo.ForeColor = Color.FromArgb(40, 83, 137);
                        break;
                    case "Sin seguimiento":
                    case "Sin evaluación":
                    case "Sin referencia":
                        estilo.BackColor = Color.FromArgb(232, 235, 238);
                        estilo.ForeColor = Color.FromArgb(85, 91, 98);
                        break;
                }
            }

            if (nombre == "Estado_Salud" &&
                (valor.Equals("Regular", StringComparison.OrdinalIgnoreCase) ||
                 valor.Equals("Malo", StringComparison.OrdinalIgnoreCase)))
            {
                estilo.ForeColor = Color.FromArgb(176, 58, 46);
            }
        }

        private void RestaurarSeleccion(int? idCultivo)
        {
            dgvCrecimiento.ClearSelection();
            DataGridViewRow? filaSeleccionada = null;

            if (idCultivo.HasValue)
            {
                filaSeleccionada = dgvCrecimiento.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(fila => ObtenerIdCultivo(fila) == idCultivo.Value);
            }

            filaSeleccionada ??= dgvCrecimiento.Rows.Cast<DataGridViewRow>().FirstOrDefault();
            if (filaSeleccionada != null)
            {
                filaSeleccionada.Selected = true;
                DataGridViewCell? celda = filaSeleccionada.Cells
                    .Cast<DataGridViewCell>()
                    .FirstOrDefault(celda => celda.Visible);
                if (celda != null)
                {
                    dgvCrecimiento.CurrentCell = celda;
                }
            }
        }

        private int? ObtenerIdCultivoSeleccionadoEnGrid()
        {
            return ObtenerIdCultivo(dgvCrecimiento.CurrentRow);
        }

        private static int? ObtenerIdCultivo(DataGridViewRow? fila)
        {
            if (fila?.DataBoundItem is DataRowView vista &&
                vista.Row.Table.Columns.Contains("ID_Cultivo"))
            {
                return ObtenerEntero(vista.Row, "ID_Cultivo");
            }

            return null;
        }

        private void ActualizarGraficoSeleccionado()
        {
            if (actualizandoGrid)
            {
                return;
            }

            int? idCultivo = ObtenerIdCultivoSeleccionadoEnGrid();
            if (!idCultivo.HasValue)
            {
                LimpiarGrafico("No hay un cultivo seleccionado.");
                return;
            }

            int id = idCultivo.GetValueOrDefault();
            List<DataRow> filas = datosFiltrados.AsEnumerable()
                .Where(fila => ObtenerEntero(fila, "ID_Cultivo") == id &&
                               ObtenerFecha(fila, "Fecha_Registro").HasValue &&
                               ObtenerDecimal(fila, "Porcentaje_Crecimiento").HasValue)
                .OrderBy(fila => ObtenerFecha(fila, "Fecha_Registro") ?? DateTime.MinValue)
                .ToList();

            chartCrecimiento.Series["Crecimiento real"].Points.Clear();
            chartCrecimiento.Series["Avance esperado"].Points.Clear();

            string cultivo = datosFiltrados.AsEnumerable()
                .Where(fila => ObtenerEntero(fila, "ID_Cultivo") == id)
                .Select(fila => ObtenerTexto(fila, "Cultivo"))
                .FirstOrDefault() ?? "Cultivo";

            if (filas.Count == 0)
            {
                lblDetalleGrafico.Text = cultivo + ": sin seguimientos en el filtro actual.";
                chartCrecimiento.Invalidate();
                return;
            }

            decimal maximo = 100m;
            foreach (DataRow fila in filas)
            {
                DateTime fecha = ObtenerFecha(fila, "Fecha_Registro").GetValueOrDefault();
                decimal real = ObtenerDecimal(fila, "Porcentaje_Crecimiento").GetValueOrDefault();
                chartCrecimiento.Series["Crecimiento real"].Points.AddXY(fecha, real);
                maximo = Math.Max(maximo, real);

                decimal? esperado = ObtenerDecimal(fila, "Crecimiento_Esperado");
                if (esperado.HasValue)
                {
                    chartCrecimiento.Series["Avance esperado"].Points.AddXY(fecha, esperado.Value);
                    maximo = Math.Max(maximo, esperado.Value);
                }
            }

            ChartArea area = chartCrecimiento.ChartAreas["AreaCrecimiento"];
            area.RecalculateAxesScale();
            area.AxisY.Minimum = 0D;
            area.AxisY.Maximum = (double)(Math.Ceiling(maximo / 10m) * 10m);

            DateTime primeraFecha = ObtenerFecha(filas.First(), "Fecha_Registro").GetValueOrDefault().Date;
            DateTime ultimaFecha = ObtenerFecha(filas.Last(), "Fecha_Registro").GetValueOrDefault().Date;
            int diasRango = Math.Max(0, (ultimaFecha - primeraFecha).Days);
            area.AxisX.IntervalType = DateTimeIntervalType.Days;
            area.AxisX.Interval = Math.Max(1D, Math.Ceiling(Math.Max(1, diasRango) / 5D));
            area.AxisX.Minimum = primeraFecha.AddDays(-1).ToOADate();
            area.AxisX.Maximum = ultimaFecha.AddDays(1).ToOADate();

            DataRow ultimo = filas.Last();
            string evaluacion = ObtenerTexto(ultimo, "Evaluacion");
            decimal ultimoValor = ObtenerDecimal(ultimo, "Porcentaje_Crecimiento") ?? 0m;
            lblDetalleGrafico.Text = $"{cultivo} | Último: {ultimoValor:N1} % | {evaluacion}";
            chartCrecimiento.Invalidate();
        }

        private void LimpiarGrafico(string mensaje)
        {
            if (chartCrecimiento.Series.Count > 0)
            {
                foreach (Series serie in chartCrecimiento.Series)
                {
                    serie.Points.Clear();
                }
            }
            ChartArea area = chartCrecimiento.ChartAreas["AreaCrecimiento"];
            area.AxisX.Minimum = double.NaN;
            area.AxisX.Maximum = double.NaN;
            area.AxisX.Interval = 0D;
            area.AxisY.Minimum = 0D;
            area.AxisY.Maximum = 100D;
            lblDetalleGrafico.Text = mensaje;
            chartCrecimiento.Invalidate();
        }

        private void ExportarCsv()
        {
            if (datosFiltrados.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                Title = "Guardar reporte de crecimiento",
                FileName = "Reporte_Crecimiento_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv"
            };

            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                (string Columna, string Titulo)[] columnas = ObtenerColumnasReporte();
                StringBuilder contenido = new StringBuilder();
                contenido.AppendLine(string.Join(";", columnas.Select(columna => EscaparCsv(columna.Titulo))));

                foreach (DataRow fila in datosFiltrados.Rows)
                {
                    contenido.AppendLine(string.Join(";", columnas.Select(columna =>
                        EscaparCsv(FormatearValor(fila, columna.Columna)))));
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
            if (datosFiltrados.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para imprimir.", "Imprimir",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using PrintDocument documento = new PrintDocument
            {
                DocumentName = "Reporte de crecimiento"
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
            using Font subtitulo = new Font("Segoe UI", 9F, FontStyle.Regular);
            using Font encabezado = new Font("Segoe UI", 8F, FontStyle.Bold);
            using Font normal = new Font("Segoe UI", 7.5F, FontStyle.Regular);
            using Brush tintaTitulo = new SolidBrush(Color.FromArgb(38, 92, 74));
            using Brush tinta = new SolidBrush(Color.Black);
            using Brush fondoEncabezado = new SolidBrush(Color.FromArgb(226, 237, 232));
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
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            grafico.DrawString("Reporte de crecimiento", titulo, tintaTitulo, x, y);
            y += 34;
            grafico.DrawString("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                               $" | Registros: {datosFiltrados.Rows.Count:N0}",
                subtitulo, tinta, x, y);
            y += 26;

            (string Columna, string Titulo, int Ancho)[] columnas =
            {
                ("Siembra", "Siembra", 115),
                ("Terreno", "Terreno", 110),
                ("Fecha_Registro", "Fecha", 68),
                ("Etapa_Crecimiento", "Etapa", 118),
                ("Estado_Salud", "Salud", 70),
                ("Porcentaje_Crecimiento", "Real %", 58),
                ("Crecimiento_Esperado", "Esperado %", 70),
                ("Evaluacion", "Evaluación", 118),
                ("Problemas_Detectados", "Problemas", 145)
            };

            int alturaFila = 30;
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

            while (filaImpresion < datosFiltrados.Rows.Count &&
                   y + alturaFila <= e.MarginBounds.Bottom)
            {
                DataRow fila = datosFiltrados.Rows[filaImpresion];
                columnaX = x;
                foreach (var columna in columnas)
                {
                    Rectangle rectangulo = new Rectangle(columnaX, y, columna.Ancho, alturaFila);
                    grafico.DrawRectangle(borde, rectangulo);
                    Rectangle texto = new Rectangle(
                        rectangulo.X + 3, rectangulo.Y, rectangulo.Width - 6, rectangulo.Height);
                    grafico.DrawString(FormatearValor(fila, columna.Columna),
                        normal, tinta, texto, formato);
                    columnaX += columna.Ancho;
                }
                filaImpresion++;
                y += alturaFila;
            }

            e.HasMorePages = filaImpresion < datosFiltrados.Rows.Count;
        }

        private static (string Columna, string Titulo)[] ObtenerColumnasReporte()
        {
            return new[]
            {
                ("ID_Cultivo", "ID cultivo"),
                ("Siembra", "Siembra"),
                ("Cultivo", "Cultivo"),
                ("Tipo_Cultivo", "Tipo de cultivo"),
                ("Categoria", "Categoría"),
                ("Terreno", "Terreno"),
                ("Fecha_Plantacion", "Fecha de plantación"),
                ("Fecha_Cosecha_Estimada", "Cosecha estimada"),
                ("Fecha_Registro", "Fecha del seguimiento"),
                ("Etapa_Crecimiento", "Etapa"),
                ("Altura_Planta", "Altura (cm)"),
                ("Num_Hojas", "Número de hojas"),
                ("Estado_Salud", "Estado de salud"),
                ("Porcentaje_Crecimiento", "Crecimiento real (%)"),
                ("Crecimiento_Esperado", "Avance esperado (%)"),
                ("Desviacion", "Diferencia (puntos)"),
                ("Evaluacion", "Evaluación"),
                ("Problemas_Detectados", "Problemas detectados"),
                ("Acciones_Tomadas", "Acciones tomadas"),
                ("Observaciones_Seguimiento", "Observaciones")
            };
        }

        private static string FormatearValor(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return string.Empty;
            }

            object? valor = fila[columna];
            if (valor == null)
            {
                return string.Empty;
            }
            if (valor is DateTime fecha)
            {
                return fecha.ToString("dd/MM/yyyy");
            }
            if (valor is decimal numero)
            {
                return numero.ToString("N2");
            }
            return valor.ToString() ?? string.Empty;
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

        private static int? ObtenerEntero(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return null;
            }

            try
            {
                return Convert.ToInt32(fila[columna]);
            }
            catch
            {
                return null;
            }
        }

        private sealed class OpcionCultivo
        {
            public OpcionCultivo(int? id, string texto)
            {
                Id = id;
                Texto = texto;
            }

            public int? Id { get; }
            public string Texto { get; }

            public override string ToString()
            {
                return Texto;
            }
        }
    }
}

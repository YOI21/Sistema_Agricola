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

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteProduccion : Form
    {
        private readonly TextBox txtBuscar = new TextBox();
        private readonly ComboBox cmbEstado = new ComboBox();
        private readonly ComboBox cmbTipo = new ComboBox();
        private readonly CheckBox chkRangoFechas = new CheckBox();
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly DateTimePicker dtpHasta = new DateTimePicker();
        private readonly Label lblTotalRegistros = new Label();
        private readonly Label lblCantidadTotal = new Label();
        private readonly Label lblRendimientoPromedio = new Label();
        private readonly Label lblCalidadPromedio = new Label();
        private readonly Label lblValorVenta = new Label();
        private readonly Label lblBeneficio = new Label();
        private readonly Label lblCultivoPrincipal = new Label();
        private readonly DataGridView dgvProduccion = new DataGridView();

        private DataTable datosProduccion = new DataTable();
        private DataTable datosFiltrados = new DataTable();
        private int filaImpresion;

        public FrmReporteProduccion()
        {
            InitializeComponent();
            ConstruirInterfaz();
            ConfigurarEventos();
            Load += FrmReporteProduccion_Load;
        }

        private void ConstruirInterfaz()
        {
            Text = "Reporte de Producción";
            FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1120, 680);
            MinimumSize = new Size(920, 600);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 250);

            Panel encabezado = CrearEncabezado();
            TableLayoutPanel contenido = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(14),
                BackColor = BackColor
            };
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 98));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            contenido.Controls.Add(CrearPanelFiltros(), 0, 0);
            contenido.Controls.Add(CrearPanelResumen(), 0, 1);
            contenido.Controls.Add(CrearPanelTabla(), 0, 2);
            contenido.Controls.Add(CrearPanelAcciones(), 0, 3);

            Controls.Add(contenido);
            Controls.Add(encabezado);
        }

        private Panel CrearEncabezado()
        {
            Panel encabezado = new Panel
            {
                Dock = DockStyle.Top,
                Height = 58,
                BackColor = Color.FromArgb(38, 92, 74)
            };

            Label titulo = new Label
            {
                Text = "Reporte de Producción",
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(18, 15)
            };

            Button cerrar = CrearBoton("X", Color.FromArgb(176, 58, 46), 36);
            cerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cerrar.Location = new Point(ClientSize.Width - 50, 11);
            cerrar.Click += (_, _) => Close();
            encabezado.Resize += (_, _) =>
                cerrar.Location = new Point(encabezado.Width - cerrar.Width - 14, 11);

            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(cerrar);
            return encabezado;
        }

        private Control CrearPanelFiltros()
        {
            Panel panel = CrearPanelBase();

            FlowLayoutPanel filtros = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 10, 12, 6),
                WrapContents = true,
                AutoScroll = false
            };

            ConfigurarCombo(cmbEstado);
            ConfigurarCombo(cmbTipo);

            txtBuscar.Width = 190;
            txtBuscar.PlaceholderText = "Cultivo, tipo o categoría";

            chkRangoFechas.Text = "Filtrar por fecha";
            chkRangoFechas.AutoSize = true;
            chkRangoFechas.Margin = new Padding(8, 27, 10, 0);

            ConfigurarFecha(dtpDesde);
            ConfigurarFecha(dtpHasta);
            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;

            Button btnAplicar = CrearBoton("Buscar", Color.FromArgb(45, 125, 94), 92);
            Button btnLimpiar = CrearBoton("Limpiar", Color.FromArgb(95, 106, 106), 92);
            Button btnRecargar = CrearBoton("Recargar", Color.FromArgb(52, 101, 164), 92);

            btnAplicar.Click += (_, _) => AplicarFiltros();
            btnLimpiar.Click += (_, _) => LimpiarFiltros();
            btnRecargar.Click += (_, _) => CargarDatos();

            filtros.Controls.Add(CrearCampo("Buscar", txtBuscar, 210));
            filtros.Controls.Add(CrearCampo("Estado", cmbEstado, 150));
            filtros.Controls.Add(CrearCampo("Tipo", cmbTipo, 150));
            filtros.Controls.Add(chkRangoFechas);
            filtros.Controls.Add(CrearCampo("Desde", dtpDesde, 126));
            filtros.Controls.Add(CrearCampo("Hasta", dtpHasta, 126));
            filtros.Controls.Add(btnAplicar);
            filtros.Controls.Add(btnLimpiar);
            filtros.Controls.Add(btnRecargar);

            panel.Controls.Add(filtros);
            return panel;
        }

        private Control CrearPanelResumen()
        {
            TableLayoutPanel resumen = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 1,
                BackColor = BackColor,
                Padding = new Padding(0, 6, 0, 8)
            };

            for (int i = 0; i < 6; i++)
            {
                resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            }

            resumen.Controls.Add(CrearTarjeta("Cantidad neta", lblCantidadTotal), 0, 0);
            resumen.Controls.Add(CrearTarjeta("Merma", lblRendimientoPromedio), 1, 0);
            resumen.Controls.Add(CrearTarjeta("Calidad promedio", lblCalidadPromedio), 2, 0);
            resumen.Controls.Add(CrearTarjeta("Valor de producción", lblValorVenta), 3, 0);
            resumen.Controls.Add(CrearTarjeta("Beneficio neto", lblBeneficio), 4, 0);
            resumen.Controls.Add(CrearTarjeta("Cultivo principal", lblCultivoPrincipal), 5, 0);

            return resumen;
        }

        private Control CrearPanelTabla()
        {
            Panel panel = CrearPanelBase();
            panel.Padding = new Padding(8);

            ConfigurarGrid();
            panel.Controls.Add(dgvProduccion);
            return panel;
        }

        private Control CrearPanelAcciones()
        {
            Panel acciones = new Panel { Dock = DockStyle.Fill, BackColor = BackColor };

            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTotalRegistros.ForeColor = Color.FromArgb(62, 73, 88);
            lblTotalRegistros.Location = new Point(2, 12);

            Button btnExportar = CrearBoton("Exportar CSV", Color.FromArgb(45, 125, 94), 116);
            Button btnImprimir = CrearBoton("Imprimir", Color.FromArgb(52, 101, 164), 96);
            Button btnVolver = CrearBoton("Volver", Color.FromArgb(176, 58, 46), 86);

            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImprimir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnVolver.Click += (_, _) => Close();
            btnExportar.Click += (_, _) => ExportarCsv();
            btnImprimir.Click += (_, _) => MostrarVistaImpresion();

            acciones.Resize += (_, _) =>
            {
                btnVolver.Location = new Point(acciones.Width - btnVolver.Width, 5);
                btnImprimir.Location = new Point(btnVolver.Left - btnImprimir.Width - 8, 5);
                btnExportar.Location = new Point(btnImprimir.Left - btnExportar.Width - 8, 5);
            };

            acciones.Controls.Add(lblTotalRegistros);
            acciones.Controls.Add(btnExportar);
            acciones.Controls.Add(btnImprimir);
            acciones.Controls.Add(btnVolver);
            return acciones;
        }

        private static Panel CrearPanelBase()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private static Panel CrearCampo(string texto, Control control, int ancho)
        {
            Panel panel = new Panel
            {
                Width = ancho,
                Height = 74,
                Margin = new Padding(4, 0, 8, 0)
            };

            Label etiqueta = new Label
            {
                Text = texto,
                AutoSize = true,
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(0, 2)
            };

            control.Location = new Point(0, 28);
            control.Width = ancho - 4;
            control.Height = 30;

            panel.Controls.Add(etiqueta);
            panel.Controls.Add(control);
            return panel;
        }

        private static Panel CrearTarjeta(string titulo, Label valor)
        {
            Panel tarjeta = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(4, 0, 4, 0),
                Padding = new Padding(10)
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = Color.FromArgb(91, 99, 111),
                Font = new Font("Segoe UI", 8.6F)
            };

            valor.Dock = DockStyle.Fill;
            valor.TextAlign = ContentAlignment.MiddleLeft;
            valor.Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold);
            valor.ForeColor = Color.FromArgb(35, 52, 70);
            valor.AutoEllipsis = true;
            valor.Text = "0";

            tarjeta.Controls.Add(valor);
            tarjeta.Controls.Add(lblTitulo);
            return tarjeta;
        }

        private static Button CrearBoton(string texto, Color color, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                Width = ancho,
                Height = 32,
                Margin = new Padding(4, 28, 4, 0),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 8.8F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private static void ConfigurarCombo(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Height = 30;
        }

        private static void ConfigurarFecha(DateTimePicker fecha)
        {
            fecha.Format = DateTimePickerFormat.Short;
            fecha.Height = 30;
        }

        private void ConfigurarGrid()
        {
            dgvProduccion.Dock = DockStyle.Fill;
            dgvProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProduccion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduccion.MultiSelect = false;
            dgvProduccion.ReadOnly = true;
            dgvProduccion.AllowUserToAddRows = false;
            dgvProduccion.AllowUserToDeleteRows = false;
            dgvProduccion.AllowUserToOrderColumns = true;
            dgvProduccion.RowHeadersVisible = false;
            dgvProduccion.BackgroundColor = Color.White;
            dgvProduccion.BorderStyle = BorderStyle.None;
            dgvProduccion.EnableHeadersVisualStyles = false;
            dgvProduccion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(38, 92, 74);
            dgvProduccion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProduccion.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvProduccion.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProduccion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 252);
            dgvProduccion.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(83, 145, 117);
            dgvProduccion.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvProduccion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProduccion.DataBindingComplete += (_, _) => FormatearGrid();
            dgvProduccion.CellFormatting += dgvProduccion_CellFormatting;
        }

        private void ConfigurarEventos()
        {
            chkRangoFechas.CheckedChanged += (_, _) =>
            {
                dtpDesde.Enabled = chkRangoFechas.Checked;
                dtpHasta.Enabled = chkRangoFechas.Checked;
                AplicarFiltros();
            };
            cmbEstado.SelectedIndexChanged += (_, _) => AplicarFiltros();
            cmbTipo.SelectedIndexChanged += (_, _) => AplicarFiltros();
            txtBuscar.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AplicarFiltros();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void FrmReporteProduccion_Load(object? sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                datosProduccion = BLL_Produccion.Listar(
                    new DateTime(2000, 1, 1),
                    DateTime.Today);
                PrepararRangoFechas();
                CargarFiltros();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de producción: " + ex.Message,
                    "Reporte de producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void PrepararRangoFechas()
        {
            DateTime? minima = null;
            DateTime? maxima = null;

            foreach (DataRow fila in datosProduccion.Rows)
            {
                DateTime? fecha = ObtenerFecha(fila, "Fecha");
                if (!fecha.HasValue) continue;

                minima = !minima.HasValue || fecha.Value < minima.Value ? fecha.Value : minima;
                maxima = !maxima.HasValue || fecha.Value > maxima.Value ? fecha.Value : maxima;
            }

            dtpDesde.Value = minima ?? DateTime.Today.AddYears(-1);
            dtpHasta.Value = maxima ?? DateTime.Today;
            chkRangoFechas.Checked = false;
            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;
        }

        private void CargarFiltros()
        {
            LlenarCombo(cmbEstado, "Estado");
            LlenarCombo(cmbTipo, "Tipo");
        }

        private void LlenarCombo(ComboBox combo, string columna)
        {
            string seleccionActual = combo.SelectedItem?.ToString() ?? "Todos";
            combo.Items.Clear();
            combo.Items.Add("Todos");

            if (datosProduccion.Columns.Contains(columna))
            {
                IEnumerable<string> valores = datosProduccion.AsEnumerable()
                    .Select(fila => ObtenerTexto(fila, columna))
                    .Where(valor => !string.IsNullOrWhiteSpace(valor))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(valor => valor);

                foreach (string valor in valores)
                {
                    combo.Items.Add(valor);
                }
            }

            combo.SelectedItem = combo.Items.Contains(seleccionActual)
                ? seleccionActual
                : "Todos";
        }

        private void AplicarFiltros()
        {
            if (datosProduccion == null)
            {
                return;
            }

            try
            {
                if (chkRangoFechas.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
                {
                    MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                        "Filtros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IEnumerable<DataRow> filas = datosProduccion.AsEnumerable();

                string estado = cmbEstado.SelectedItem?.ToString() ?? "Todos";
                if (estado != "Todos")
                {
                    filas = filas.Where(fila =>
                        ObtenerTexto(fila, "Estado").Equals(estado, StringComparison.OrdinalIgnoreCase));
                }

                string tipo = cmbTipo.SelectedItem?.ToString() ?? "Todos";
                if (tipo != "Todos")
                {
                    filas = filas.Where(fila =>
                        ObtenerTexto(fila, "Tipo").Equals(tipo, StringComparison.OrdinalIgnoreCase));
                }

                if (chkRangoFechas.Checked)
                {
                    DateTime desde = dtpDesde.Value.Date;
                    DateTime hasta = dtpHasta.Value.Date;
                    filas = filas.Where(fila =>
                    {
                        DateTime? fecha = ObtenerFecha(fila, "Fecha");
                        return fecha.HasValue && fecha.Value.Date >= desde && fecha.Value.Date <= hasta;
                    });
                }

                string busqueda = txtBuscar.Text.Trim();
                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    filas = filas.Where(fila =>
                        Contiene(fila, "Cultivo", busqueda) ||
                        Contiene(fila, "Tipo", busqueda) ||
                        Contiene(fila, "Categoria", busqueda) ||
                        Contiene(fila, "Estado", busqueda));
                }

                List<DataRow> lista = filas.ToList();
                datosFiltrados = lista.Count > 0
                    ? lista.CopyToDataTable()
                    : datosProduccion.Clone();

                dgvProduccion.DataSource = datosFiltrados;
                ActualizarResumen();
                lblTotalRegistros.Text = $"Total: {datosFiltrados.Rows.Count} registros";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar filtros: " + ex.Message,
                    "Filtros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFiltros()
        {
            txtBuscar.Clear();
            cmbEstado.SelectedItem = "Todos";
            cmbTipo.SelectedItem = "Todos";
            chkRangoFechas.Checked = false;
            AplicarFiltros();
        }

        private void ActualizarResumen()
        {
            IEnumerable<DataRow> filas = datosFiltrados.AsEnumerable();
            List<DataRow> lista = filas.ToList();

            decimal cantidadTotal = lista.Sum(fila => ObtenerDecimal(fila, "Cantidad neta"));
            decimal merma = lista.Sum(fila => ObtenerDecimal(fila, "Merma"));
            decimal valorVenta = lista.Sum(fila => ObtenerDecimal(fila, "Valor produccion"));
            decimal beneficio = lista.Sum(fila => ObtenerDecimal(fila, "Beneficio neto"));
            decimal calidadPromedio = Promedio(lista, "Calidad");
            string cultivoPrincipal = ObtenerCultivoPrincipal(lista);

            lblCantidadTotal.Text = cantidadTotal.ToString("N2");
            lblRendimientoPromedio.Text = merma.ToString("N2");
            lblCalidadPromedio.Text = calidadPromedio > 0 ? $"{calidadPromedio:N2}" : "N/A";
            lblValorVenta.Text = $"$ {valorVenta:N2}";
            lblBeneficio.Text = $"$ {beneficio:N2}";
            lblBeneficio.ForeColor = beneficio < 0
                ? Color.FromArgb(176, 58, 46)
                : Color.FromArgb(34, 128, 88);
            lblCultivoPrincipal.Text = cultivoPrincipal;
        }

        private static decimal Promedio(List<DataRow> filas, string columna)
        {
            List<decimal> valores = filas
                .Select(fila => ObtenerDecimal(fila, columna))
                .Where(valor => valor > 0)
                .ToList();

            return valores.Count == 0 ? 0 : valores.Average();
        }

        private static string ObtenerCultivoPrincipal(List<DataRow> filas)
        {
            var cultivo = filas
                .GroupBy(fila => ObtenerTexto(fila, "Cultivo"))
                .Where(grupo => !string.IsNullOrWhiteSpace(grupo.Key))
                .Select(grupo => new
                {
                    Nombre = grupo.Key,
                    Cantidad = grupo.Sum(fila => ObtenerDecimal(fila, "Cantidad neta"))
                })
                .OrderByDescending(grupo => grupo.Cantidad)
                .FirstOrDefault();

            return cultivo == null ? "N/A" : cultivo.Nombre;
        }

        private void FormatearGrid()
        {
            if (dgvProduccion.Columns.Count == 0)
            {
                return;
            }

            if (dgvProduccion.Columns.Contains("ID"))
            {
                dgvProduccion.Columns["ID"].Visible = false;
            }

            string[] columnasDecimal =
            {
                "Cantidad cosechada",
                "Cantidad bruta",
                "Cantidad neta",
                "Merma",
                "Rendimiento %",
                "Calidad",
                "Precio unitario",
                "Valor produccion",
                "Costo base",
                "Costo postcosecha",
                "Costo total",
                "Beneficio neto"
            };

            foreach (string columna in columnasDecimal)
            {
                if (dgvProduccion.Columns.Contains(columna))
                {
                    dgvProduccion.Columns[columna].DefaultCellStyle.Format = "N2";
                    dgvProduccion.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvProduccion.Columns.Contains("Fecha"))
            {
                dgvProduccion.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            PintarFilas();
        }

        private void PintarFilas()
        {
            if (!dgvProduccion.Columns.Contains("Estado"))
            {
                return;
            }

            foreach (DataGridViewRow fila in dgvProduccion.Rows)
            {
                string estado = Convert.ToString(fila.Cells["Estado"]?.Value) ?? string.Empty;
                if (estado.Equals("Completada", StringComparison.OrdinalIgnoreCase))
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(236, 252, 243);
                }
                else if (estado.Equals("En Proceso", StringComparison.OrdinalIgnoreCase))
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 249, 219);
                }
                else if (estado.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                }
            }
        }

        private void dgvProduccion_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }

            if (dgvProduccion.Columns[e.ColumnIndex].Name == "Beneficio neto" &&
                decimal.TryParse(Convert.ToString(e.Value), out decimal beneficio))
            {
                if (e.CellStyle != null)
                {
                    e.CellStyle.ForeColor = beneficio < 0
                        ? Color.FromArgb(176, 58, 46)
                        : Color.FromArgb(34, 128, 88);
                    e.CellStyle.Font = new Font(dgvProduccion.Font, FontStyle.Bold);
                }
            }
        }

        private void ExportarCsv()
        {
            if (datosFiltrados.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.",
                    "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                Title = "Guardar reporte de producción",
                FileName = "Reporte_Produccion_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv"
            };

            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                StringBuilder contenido = new StringBuilder();
                contenido.AppendLine(string.Join(",",
                    datosFiltrados.Columns.Cast<DataColumn>().Select(columna => Csv(columna.ColumnName))));

                foreach (DataRow fila in datosFiltrados.Rows)
                {
                    contenido.AppendLine(string.Join(",",
                        fila.ItemArray.Select(valor => Csv(valor))));
                }

                File.WriteAllText(dialogo.FileName, contenido.ToString(), Encoding.UTF8);

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
                MessageBox.Show("No hay datos para imprimir.",
                    "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            filaImpresion = 0;
            PrintDocument documento = new PrintDocument
            {
                DocumentName = "Reporte de Producción"
            };
            documento.PrintPage += Documento_PrintPage;

            using PrintPreviewDialog vista = new PrintPreviewDialog
            {
                Document = documento,
                Width = 1000,
                Height = 720,
                StartPosition = FormStartPosition.CenterParent
            };
            vista.ShowDialog(this);
        }

        private void Documento_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null)
            {
                e.HasMorePages = false;
                return;
            }

            Font titulo = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            Font normal = new Font("Segoe UI", 9F);
            Font encabezado = new Font("Segoe UI Semibold", 8.6F, FontStyle.Bold);
            Brush tinta = Brushes.Black;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            graphics.DrawString("Reporte de Producción", titulo, tinta, x, y);
            y += 32;
            graphics.DrawString("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                normal, tinta, x, y);
            y += 24;
            graphics.DrawString(
                $"Cantidad: {lblCantidadTotal.Text}   Venta: {lblValorVenta.Text}   Beneficio: {lblBeneficio.Text}",
                normal, tinta, x, y);
            y += 34;

            string[] columnas = { "Cultivo", "Tipo", "Estado", "Cantidad neta", "Merma", "Beneficio neto" };
            int[] anchos = { 155, 105, 100, 130, 105, 105 };

            int xActual = x;
            for (int i = 0; i < columnas.Length; i++)
            {
                graphics.DrawString(columnas[i], encabezado, tinta, xActual, y);
                xActual += anchos[i];
            }

            y += 22;
            graphics.DrawLine(Pens.DarkGray, x, y, x + anchos.Sum(), y);
            y += 8;

            while (filaImpresion < datosFiltrados.Rows.Count)
            {
                DataRow fila = datosFiltrados.Rows[filaImpresion];
                xActual = x;

                string[] valores =
                {
                    Recortar(ObtenerTexto(fila, "Cultivo"), 24),
                    Recortar(ObtenerTexto(fila, "Tipo"), 16),
                    Recortar(ObtenerTexto(fila, "Estado"), 16),
                    ObtenerDecimal(fila, "Cantidad neta").ToString("N2"),
                    ObtenerDecimal(fila, "Merma").ToString("N2"),
                    "$ " + ObtenerDecimal(fila, "Beneficio neto").ToString("N2")
                };

                for (int i = 0; i < valores.Length; i++)
                {
                    graphics.DrawString(valores[i], normal, tinta, xActual, y);
                    xActual += anchos[i];
                }

                y += 22;
                filaImpresion++;

                if (y > e.MarginBounds.Bottom - 28)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
        }

        private static string Recortar(string valor, int maximo)
        {
            if (string.IsNullOrWhiteSpace(valor) || valor.Length <= maximo)
            {
                return valor;
            }

            return valor.Substring(0, Math.Max(0, maximo - 3)) + "...";
        }

        private static string Csv(object? valor)
        {
            string texto = valor is DateTime fecha
                ? fecha.ToString("dd/MM/yyyy")
                : Convert.ToString(valor) ?? string.Empty;

            return "\"" + texto.Replace("\"", "\"\"") + "\"";
        }

        private static bool Contiene(DataRow fila, string columna, string busqueda)
        {
            return ObtenerTexto(fila, columna)
                .IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string ObtenerTexto(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return string.Empty;
            }

            return Convert.ToString(fila[columna]) ?? string.Empty;
        }

        private static decimal ObtenerDecimal(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(fila[columna]);
        }

        private static DateTime? ObtenerFecha(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila[columna] == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(fila[columna]);
        }
    }
}

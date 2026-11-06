#nullable disable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    partial class FrmReporteCrecimiento
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ChartArea chartArea = new ChartArea();
            Legend legend = new Legend();
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            btnCerrar = new Button();
            tablePrincipal = new TableLayoutPanel();
            panelFiltros = new Panel();
            tableFiltros = new TableLayoutPanel();
            panelBuscar = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            panelCultivo = new Panel();
            lblCultivo = new Label();
            cmbCultivo = new ComboBox();
            panelEtapa = new Panel();
            lblEtapa = new Label();
            cmbEtapa = new ComboBox();
            panelSalud = new Panel();
            lblSalud = new Label();
            cmbSalud = new ComboBox();
            panelEvaluacion = new Panel();
            lblEvaluacion = new Label();
            cmbEvaluacion = new ComboBox();
            panelRecargar = new Panel();
            btnRecargar = new Button();
            panelRango = new Panel();
            chkRangoFechas = new CheckBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            panelBotonesFiltro = new FlowLayoutPanel();
            btnAplicar = new Button();
            btnLimpiar = new Button();
            tableResumen = new TableLayoutPanel();
            panelCultivos = new Panel();
            lblTituloCultivos = new Label();
            lblCultivos = new Label();
            panelSeguimientos = new Panel();
            lblTituloSeguimientos = new Label();
            lblSeguimientos = new Label();
            panelPromedio = new Panel();
            lblTituloPromedio = new Label();
            lblPromedio = new Label();
            panelAltura = new Panel();
            lblTituloAltura = new Label();
            lblAltura = new Label();
            panelRevisar = new Panel();
            lblTituloRevisar = new Label();
            lblRevisar = new Label();
            panelSinSeguimiento = new Panel();
            lblTituloSinSeguimiento = new Label();
            lblSinSeguimiento = new Label();
            tableContenido = new TableLayoutPanel();
            panelGrafico = new Panel();
            lblTituloGrafico = new Label();
            chartCrecimiento = new Chart();
            lblDetalleGrafico = new Label();
            panelTabla = new Panel();
            lblTituloTabla = new Label();
            dgvCrecimiento = new DataGridView();
            panelAcciones = new Panel();
            lblTotalResultados = new Label();
            flowAcciones = new FlowLayoutPanel();
            btnVolver = new Button();
            btnImprimir = new Button();
            btnExportar = new Button();
            toolTip = new ToolTip(components);
            panelEncabezado.SuspendLayout();
            tablePrincipal.SuspendLayout();
            panelFiltros.SuspendLayout();
            tableFiltros.SuspendLayout();
            panelBuscar.SuspendLayout();
            panelCultivo.SuspendLayout();
            panelEtapa.SuspendLayout();
            panelSalud.SuspendLayout();
            panelEvaluacion.SuspendLayout();
            panelRecargar.SuspendLayout();
            panelRango.SuspendLayout();
            panelBotonesFiltro.SuspendLayout();
            tableResumen.SuspendLayout();
            tableContenido.SuspendLayout();
            panelGrafico.SuspendLayout();
            ((ISupportInitialize)chartCrecimiento).BeginInit();
            panelTabla.SuspendLayout();
            ((ISupportInitialize)dgvCrecimiento).BeginInit();
            panelAcciones.SuspendLayout();
            flowAcciones.SuspendLayout();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(38, 92, 74);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(btnCerrar);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Height = 58;
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1180, 58);

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(18, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Reporte de crecimiento";

            ConfigurarBoton(btnCerrar, "X", Color.FromArgb(176, 58, 46));
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Location = new Point(1128, 11);
            btnCerrar.Size = new Size(38, 36);
            btnCerrar.Name = "btnCerrar";
            toolTip.SetToolTip(btnCerrar, "Cerrar reporte");

            tablePrincipal.BackColor = Color.FromArgb(244, 247, 250);
            tablePrincipal.ColumnCount = 1;
            tablePrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablePrincipal.Controls.Add(panelFiltros, 0, 0);
            tablePrincipal.Controls.Add(tableResumen, 0, 1);
            tablePrincipal.Controls.Add(tableContenido, 0, 2);
            tablePrincipal.Controls.Add(panelAcciones, 0, 3);
            tablePrincipal.Dock = DockStyle.Fill;
            tablePrincipal.Location = new Point(0, 58);
            tablePrincipal.Padding = new Padding(14);
            tablePrincipal.RowCount = 4;
            tablePrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            tablePrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tablePrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablePrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tablePrincipal.Name = "tablePrincipal";
            tablePrincipal.Size = new Size(1180, 662);

            panelFiltros.BackColor = Color.White;
            panelFiltros.BorderStyle = BorderStyle.FixedSingle;
            panelFiltros.Controls.Add(tableFiltros);
            panelFiltros.Dock = DockStyle.Fill;
            panelFiltros.Margin = new Padding(0, 0, 0, 8);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(1152, 100);

            tableFiltros.ColumnCount = 6;
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            tableFiltros.Controls.Add(panelBuscar, 0, 0);
            tableFiltros.Controls.Add(panelCultivo, 1, 0);
            tableFiltros.Controls.Add(panelEtapa, 2, 0);
            tableFiltros.Controls.Add(panelSalud, 3, 0);
            tableFiltros.Controls.Add(panelEvaluacion, 4, 0);
            tableFiltros.Controls.Add(panelRecargar, 5, 0);
            tableFiltros.Controls.Add(panelRango, 0, 1);
            tableFiltros.SetColumnSpan(panelRango, 4);
            tableFiltros.Controls.Add(panelBotonesFiltro, 4, 1);
            tableFiltros.SetColumnSpan(panelBotonesFiltro, 2);
            tableFiltros.Dock = DockStyle.Fill;
            tableFiltros.RowCount = 2;
            tableFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            tableFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tableFiltros.Name = "tableFiltros";
            tableFiltros.Size = new Size(1150, 98);

            ConfigurarCampo(panelBuscar, lblBuscar, txtBuscar, "Buscar");
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Cultivo, terreno o problema";

            ConfigurarCampo(panelCultivo, lblCultivo, cmbCultivo, "Cultivo");
            cmbCultivo.Name = "cmbCultivo";
            ConfigurarCombo(cmbCultivo);

            ConfigurarCampo(panelEtapa, lblEtapa, cmbEtapa, "Etapa");
            cmbEtapa.Name = "cmbEtapa";
            ConfigurarCombo(cmbEtapa);

            ConfigurarCampo(panelSalud, lblSalud, cmbSalud, "Salud");
            cmbSalud.Name = "cmbSalud";
            ConfigurarCombo(cmbSalud);

            ConfigurarCampo(panelEvaluacion, lblEvaluacion, cmbEvaluacion, "Evaluación");
            cmbEvaluacion.Name = "cmbEvaluacion";
            ConfigurarCombo(cmbEvaluacion);
            toolTip.SetToolTip(cmbEvaluacion,
                "Compara el crecimiento registrado con el avance esperado del calendario del cultivo.");

            panelRecargar.Dock = DockStyle.Fill;
            panelRecargar.Padding = new Padding(8, 15, 8, 7);
            panelRecargar.Controls.Add(btnRecargar);
            ConfigurarBoton(btnRecargar, "Recargar", Color.FromArgb(52, 101, 164));
            btnRecargar.Dock = DockStyle.Fill;
            btnRecargar.Name = "btnRecargar";

            panelRango.Controls.Add(chkRangoFechas);
            panelRango.Controls.Add(lblDesde);
            panelRango.Controls.Add(dtpDesde);
            panelRango.Controls.Add(lblHasta);
            panelRango.Controls.Add(dtpHasta);
            panelRango.Dock = DockStyle.Fill;
            panelRango.Name = "panelRango";

            chkRangoFechas.AutoSize = true;
            chkRangoFechas.Location = new Point(9, 14);
            chkRangoFechas.Name = "chkRangoFechas";
            chkRangoFechas.Text = "Filtrar por fecha";

            lblDesde.AutoSize = true;
            lblDesde.ForeColor = Color.FromArgb(75, 85, 99);
            lblDesde.Location = new Point(145, 15);
            lblDesde.Text = "Desde";
            dtpDesde.Enabled = false;
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(191, 10);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(112, 23);

            lblHasta.AutoSize = true;
            lblHasta.ForeColor = Color.FromArgb(75, 85, 99);
            lblHasta.Location = new Point(319, 15);
            lblHasta.Text = "Hasta";
            dtpHasta.Enabled = false;
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(365, 10);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(112, 23);

            panelBotonesFiltro.Controls.Add(btnAplicar);
            panelBotonesFiltro.Controls.Add(btnLimpiar);
            panelBotonesFiltro.Dock = DockStyle.Fill;
            panelBotonesFiltro.FlowDirection = FlowDirection.RightToLeft;
            panelBotonesFiltro.Padding = new Padding(4, 6, 8, 4);
            panelBotonesFiltro.WrapContents = false;
            panelBotonesFiltro.Name = "panelBotonesFiltro";
            ConfigurarBoton(btnAplicar, "Aplicar", Color.FromArgb(45, 125, 94));
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(96, 32);
            ConfigurarBoton(btnLimpiar, "Limpiar", Color.FromArgb(95, 106, 106));
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(96, 32);

            tableResumen.BackColor = Color.FromArgb(244, 247, 250);
            tableResumen.ColumnCount = 6;
            for (int i = 0; i < 6; i++)
            {
                tableResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6667F));
            }
            tableResumen.Controls.Add(panelCultivos, 0, 0);
            tableResumen.Controls.Add(panelSeguimientos, 1, 0);
            tableResumen.Controls.Add(panelPromedio, 2, 0);
            tableResumen.Controls.Add(panelAltura, 3, 0);
            tableResumen.Controls.Add(panelRevisar, 4, 0);
            tableResumen.Controls.Add(panelSinSeguimiento, 5, 0);
            tableResumen.Dock = DockStyle.Fill;
            tableResumen.Margin = new Padding(0);
            tableResumen.RowCount = 1;
            tableResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableResumen.Name = "tableResumen";
            tableResumen.Size = new Size(1152, 92);

            ConfigurarTarjeta(panelCultivos, lblTituloCultivos, lblCultivos, "Cultivos");
            ConfigurarTarjeta(panelSeguimientos, lblTituloSeguimientos, lblSeguimientos, "Seguimientos");
            ConfigurarTarjeta(panelPromedio, lblTituloPromedio, lblPromedio, "Crecimiento prom.");
            ConfigurarTarjeta(panelAltura, lblTituloAltura, lblAltura, "Altura promedio");
            ConfigurarTarjeta(panelRevisar, lblTituloRevisar, lblRevisar, "Por revisar");
            ConfigurarTarjeta(panelSinSeguimiento, lblTituloSinSeguimiento, lblSinSeguimiento, "Sin seguimiento");
            lblRevisar.ForeColor = Color.FromArgb(176, 92, 26);
            lblSinSeguimiento.ForeColor = Color.FromArgb(161, 57, 57);
            toolTip.SetToolTip(panelRevisar,
                "Cuenta cultivos cuyo último registro requiere atención, seguimiento o tiene fechas inconsistentes.");

            tableContenido.BackColor = Color.FromArgb(244, 247, 250);
            tableContenido.ColumnCount = 2;
            tableContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableContenido.Controls.Add(panelGrafico, 0, 0);
            tableContenido.Controls.Add(panelTabla, 1, 0);
            tableContenido.Dock = DockStyle.Fill;
            tableContenido.Margin = new Padding(0, 0, 0, 4);
            tableContenido.RowCount = 1;
            tableContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableContenido.Name = "tableContenido";
            tableContenido.Size = new Size(1152, 400);

            panelGrafico.BackColor = Color.White;
            panelGrafico.BorderStyle = BorderStyle.FixedSingle;
            panelGrafico.Controls.Add(chartCrecimiento);
            panelGrafico.Controls.Add(lblDetalleGrafico);
            panelGrafico.Controls.Add(lblTituloGrafico);
            panelGrafico.Dock = DockStyle.Fill;
            panelGrafico.Margin = new Padding(0, 0, 5, 0);
            panelGrafico.MinimumSize = new Size(1, 1);
            panelGrafico.Name = "panelGrafico";
            panelGrafico.Size = new Size(393, 400);

            lblTituloGrafico.Dock = DockStyle.Top;
            lblTituloGrafico.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTituloGrafico.ForeColor = Color.FromArgb(42, 58, 73);
            lblTituloGrafico.Height = 34;
            lblTituloGrafico.Padding = new Padding(10, 8, 0, 0);
            lblTituloGrafico.Text = "Evolución del cultivo seleccionado";

            chartArea.AxisX.LabelStyle.Format = "dd/MM";
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 234, 238);
            chartArea.AxisX.Title = "Fecha";
            chartArea.AxisY.Interval = 20D;
            chartArea.AxisY.Maximum = 100D;
            chartArea.AxisY.Minimum = 0D;
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            chartArea.AxisY.Title = "Crecimiento (%)";
            chartArea.Name = "AreaCrecimiento";
            chartCrecimiento.ChartAreas.Add(chartArea);
            chartCrecimiento.Dock = DockStyle.Fill;
            legend.Docking = Docking.Bottom;
            legend.Name = "Leyenda";
            chartCrecimiento.Legends.Add(legend);
            chartCrecimiento.Location = new Point(1, 34);
            chartCrecimiento.MinimumSize = new Size(1, 1);
            chartCrecimiento.Name = "chartCrecimiento";
            chartCrecimiento.Palette = ChartColorPalette.None;
            chartCrecimiento.Size = new Size(391, 330);

            lblDetalleGrafico.Dock = DockStyle.Bottom;
            lblDetalleGrafico.ForeColor = Color.FromArgb(91, 99, 111);
            lblDetalleGrafico.Height = 34;
            lblDetalleGrafico.Padding = new Padding(10, 2, 10, 0);
            lblDetalleGrafico.Text = "Seleccione un cultivo para ver su evolución.";

            panelTabla.BackColor = Color.White;
            panelTabla.BorderStyle = BorderStyle.FixedSingle;
            panelTabla.Controls.Add(dgvCrecimiento);
            panelTabla.Controls.Add(lblTituloTabla);
            panelTabla.Dock = DockStyle.Fill;
            panelTabla.Margin = new Padding(5, 0, 0, 0);
            panelTabla.Name = "panelTabla";
            panelTabla.Size = new Size(749, 400);

            lblTituloTabla.Dock = DockStyle.Top;
            lblTituloTabla.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTituloTabla.ForeColor = Color.FromArgb(42, 58, 73);
            lblTituloTabla.Height = 34;
            lblTituloTabla.Padding = new Padding(10, 8, 0, 0);
            lblTituloTabla.Text = "Detalle de seguimientos";

            dgvCrecimiento.AllowUserToAddRows = false;
            dgvCrecimiento.AllowUserToDeleteRows = false;
            dgvCrecimiento.AllowUserToOrderColumns = true;
            dgvCrecimiento.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvCrecimiento.BackgroundColor = Color.White;
            dgvCrecimiento.BorderStyle = BorderStyle.None;
            dgvCrecimiento.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCrecimiento.ColumnHeadersHeight = 36;
            dgvCrecimiento.Dock = DockStyle.Fill;
            dgvCrecimiento.EnableHeadersVisualStyles = false;
            dgvCrecimiento.MultiSelect = false;
            dgvCrecimiento.Name = "dgvCrecimiento";
            dgvCrecimiento.ReadOnly = true;
            dgvCrecimiento.RowHeadersVisible = false;
            dgvCrecimiento.RowTemplate.Height = 30;
            dgvCrecimiento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCrecimiento.Size = new Size(747, 364);

            panelAcciones.BackColor = Color.FromArgb(244, 247, 250);
            panelAcciones.Controls.Add(lblTotalResultados);
            panelAcciones.Controls.Add(flowAcciones);
            panelAcciones.Dock = DockStyle.Fill;
            panelAcciones.Margin = new Padding(0);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(1152, 48);

            lblTotalResultados.AutoSize = true;
            lblTotalResultados.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTotalResultados.ForeColor = Color.FromArgb(62, 73, 88);
            lblTotalResultados.Location = new Point(2, 14);
            lblTotalResultados.Text = "0 resultados";

            flowAcciones.Controls.Add(btnVolver);
            flowAcciones.Controls.Add(btnImprimir);
            flowAcciones.Controls.Add(btnExportar);
            flowAcciones.Dock = DockStyle.Right;
            flowAcciones.FlowDirection = FlowDirection.RightToLeft;
            flowAcciones.Padding = new Padding(0, 5, 0, 0);
            flowAcciones.Size = new Size(350, 48);
            flowAcciones.WrapContents = false;

            ConfigurarBoton(btnVolver, "Cerrar", Color.FromArgb(176, 58, 46));
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(82, 34);
            ConfigurarBoton(btnImprimir, "Imprimir", Color.FromArgb(52, 101, 164));
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(92, 34);
            ConfigurarBoton(btnExportar, "Exportar CSV", Color.FromArgb(45, 125, 94));
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(112, 34);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            ClientSize = new Size(1180, 720);
            Controls.Add(tablePrincipal);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(900, 600);
            Name = "FrmReporteCrecimiento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte de crecimiento";
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            tablePrincipal.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            tableFiltros.ResumeLayout(false);
            panelBuscar.ResumeLayout(false);
            panelBuscar.PerformLayout();
            panelCultivo.ResumeLayout(false);
            panelCultivo.PerformLayout();
            panelEtapa.ResumeLayout(false);
            panelEtapa.PerformLayout();
            panelSalud.ResumeLayout(false);
            panelSalud.PerformLayout();
            panelEvaluacion.ResumeLayout(false);
            panelEvaluacion.PerformLayout();
            panelRecargar.ResumeLayout(false);
            panelRango.ResumeLayout(false);
            panelRango.PerformLayout();
            panelBotonesFiltro.ResumeLayout(false);
            tableResumen.ResumeLayout(false);
            tableContenido.ResumeLayout(false);
            panelGrafico.ResumeLayout(false);
            ((ISupportInitialize)chartCrecimiento).EndInit();
            panelTabla.ResumeLayout(false);
            ((ISupportInitialize)dgvCrecimiento).EndInit();
            panelAcciones.ResumeLayout(false);
            panelAcciones.PerformLayout();
            flowAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void ConfigurarCampo(Panel panel, Label etiqueta, Control control, string texto)
        {
            panel.Controls.Add(control);
            panel.Controls.Add(etiqueta);
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(8, 3, 8, 4);
            etiqueta.Dock = DockStyle.Top;
            etiqueta.Font = new Font("Segoe UI", 8.5F);
            etiqueta.ForeColor = Color.FromArgb(75, 85, 99);
            etiqueta.Height = 18;
            etiqueta.Text = texto;
            control.Dock = DockStyle.Bottom;
        }

        private static void ConfigurarCombo(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.IntegralHeight = false;
            combo.MaxDropDownItems = 12;
        }

        private static void ConfigurarTarjeta(Panel panel, Label titulo, Label valor, string texto)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(valor);
            panel.Controls.Add(titulo);
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(4, 4, 4, 8);
            panel.Padding = new Padding(10, 8, 10, 8);
            titulo.Dock = DockStyle.Top;
            titulo.Font = new Font("Segoe UI", 8.5F);
            titulo.ForeColor = Color.FromArgb(91, 99, 111);
            titulo.Height = 22;
            titulo.Text = texto;
            titulo.AutoEllipsis = true;
            valor.Dock = DockStyle.Fill;
            valor.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            valor.ForeColor = Color.FromArgb(35, 52, 70);
            valor.Text = "0";
            valor.TextAlign = ContentAlignment.MiddleLeft;
            valor.AutoEllipsis = true;
        }

        private static void ConfigurarBoton(Button boton, string texto, Color color)
        {
            boton.BackColor = color;
            boton.Cursor = Cursors.Hand;
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatStyle = FlatStyle.Flat;
            boton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            boton.ForeColor = Color.White;
            boton.Margin = new Padding(4, 0, 4, 0);
            boton.Text = texto;
            boton.UseVisualStyleBackColor = false;
        }

        private Panel panelEncabezado;
        private Label lblTitulo;
        private Button btnCerrar;
        private TableLayoutPanel tablePrincipal;
        private Panel panelFiltros;
        private TableLayoutPanel tableFiltros;
        private Panel panelBuscar;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Panel panelCultivo;
        private Label lblCultivo;
        private ComboBox cmbCultivo;
        private Panel panelEtapa;
        private Label lblEtapa;
        private ComboBox cmbEtapa;
        private Panel panelSalud;
        private Label lblSalud;
        private ComboBox cmbSalud;
        private Panel panelEvaluacion;
        private Label lblEvaluacion;
        private ComboBox cmbEvaluacion;
        private Panel panelRecargar;
        private Button btnRecargar;
        private Panel panelRango;
        private CheckBox chkRangoFechas;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private FlowLayoutPanel panelBotonesFiltro;
        private Button btnAplicar;
        private Button btnLimpiar;
        private TableLayoutPanel tableResumen;
        private Panel panelCultivos;
        private Label lblTituloCultivos;
        private Label lblCultivos;
        private Panel panelSeguimientos;
        private Label lblTituloSeguimientos;
        private Label lblSeguimientos;
        private Panel panelPromedio;
        private Label lblTituloPromedio;
        private Label lblPromedio;
        private Panel panelAltura;
        private Label lblTituloAltura;
        private Label lblAltura;
        private Panel panelRevisar;
        private Label lblTituloRevisar;
        private Label lblRevisar;
        private Panel panelSinSeguimiento;
        private Label lblTituloSinSeguimiento;
        private Label lblSinSeguimiento;
        private TableLayoutPanel tableContenido;
        private Panel panelGrafico;
        private Label lblTituloGrafico;
        private Chart chartCrecimiento;
        private Label lblDetalleGrafico;
        private Panel panelTabla;
        private Label lblTituloTabla;
        private DataGridView dgvCrecimiento;
        private Panel panelAcciones;
        private Label lblTotalResultados;
        private FlowLayoutPanel flowAcciones;
        private Button btnVolver;
        private Button btnImprimir;
        private Button btnExportar;
        private ToolTip toolTip;
    }
}

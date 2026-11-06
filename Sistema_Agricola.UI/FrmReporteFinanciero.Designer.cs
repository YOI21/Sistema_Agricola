#nullable disable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    partial class FrmReporteFinanciero
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
            panelPeriodo = new Panel();
            lblPeriodo = new Label();
            cmbPeriodo = new ComboBox();
            panelBuscar = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            panelTipo = new Panel();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            panelEstado = new Panel();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            panelOrigen = new Panel();
            lblOrigen = new Label();
            cmbOrigen = new ComboBox();
            panelRecargar = new Panel();
            btnRecargar = new Button();
            panelRango = new Panel();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            panelCategoria = new Panel();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            panelBotonesFiltro = new FlowLayoutPanel();
            btnAplicar = new Button();
            btnLimpiar = new Button();
            tableResumen = new TableLayoutPanel();
            panelIngresos = new Panel();
            lblTituloIngresos = new Label();
            lblIngresos = new Label();
            panelGastos = new Panel();
            lblTituloGastos = new Label();
            lblGastos = new Label();
            panelResultado = new Panel();
            lblTituloResultado = new Label();
            lblResultado = new Label();
            panelCobrar = new Panel();
            lblTituloCobrar = new Label();
            lblCobrar = new Label();
            panelPagar = new Panel();
            lblTituloPagar = new Label();
            lblPagar = new Label();
            panelMargen = new Panel();
            lblTituloMargen = new Label();
            lblMargen = new Label();
            tableContenido = new TableLayoutPanel();
            panelGrafico = new Panel();
            panelCabeceraGrafico = new Panel();
            lblTituloGrafico = new Label();
            cmbVistaGrafico = new ComboBox();
            chartFinanciero = new Chart();
            lblDetalleGrafico = new Label();
            panelTabla = new Panel();
            lblTituloTabla = new Label();
            dgvMovimientos = new DataGridView();
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
            panelPeriodo.SuspendLayout();
            panelBuscar.SuspendLayout();
            panelTipo.SuspendLayout();
            panelEstado.SuspendLayout();
            panelOrigen.SuspendLayout();
            panelRecargar.SuspendLayout();
            panelRango.SuspendLayout();
            panelCategoria.SuspendLayout();
            panelBotonesFiltro.SuspendLayout();
            tableResumen.SuspendLayout();
            tableContenido.SuspendLayout();
            panelGrafico.SuspendLayout();
            panelCabeceraGrafico.SuspendLayout();
            ((ISupportInitialize)chartFinanciero).BeginInit();
            panelTabla.SuspendLayout();
            ((ISupportInitialize)dgvMovimientos).BeginInit();
            panelAcciones.SuspendLayout();
            flowAcciones.SuspendLayout();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(44, 76, 94);
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
            lblTitulo.Text = "Reporte financiero";

            ConfigurarBoton(btnCerrar, "X", Color.FromArgb(176, 58, 46));
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Location = new Point(1128, 11);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(38, 36);
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
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableFiltros.Controls.Add(panelPeriodo, 0, 0);
            tableFiltros.Controls.Add(panelBuscar, 1, 0);
            tableFiltros.Controls.Add(panelTipo, 2, 0);
            tableFiltros.Controls.Add(panelEstado, 3, 0);
            tableFiltros.Controls.Add(panelOrigen, 4, 0);
            tableFiltros.Controls.Add(panelRecargar, 5, 0);
            tableFiltros.Controls.Add(panelRango, 0, 1);
            tableFiltros.SetColumnSpan(panelRango, 3);
            tableFiltros.Controls.Add(panelCategoria, 3, 1);
            tableFiltros.Controls.Add(panelBotonesFiltro, 4, 1);
            tableFiltros.SetColumnSpan(panelBotonesFiltro, 2);
            tableFiltros.Dock = DockStyle.Fill;
            tableFiltros.RowCount = 2;
            tableFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            tableFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tableFiltros.Name = "tableFiltros";
            tableFiltros.Size = new Size(1150, 98);

            ConfigurarCampo(panelPeriodo, lblPeriodo, cmbPeriodo, "Período");
            cmbPeriodo.Name = "cmbPeriodo";
            ConfigurarCombo(cmbPeriodo);

            ConfigurarCampo(panelBuscar, lblBuscar, txtBuscar, "Buscar");
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Categoría, descripción o referencia";

            ConfigurarCampo(panelTipo, lblTipo, cmbTipo, "Tipo");
            cmbTipo.Name = "cmbTipo";
            ConfigurarCombo(cmbTipo);

            ConfigurarCampo(panelEstado, lblEstado, cmbEstado, "Estado");
            cmbEstado.Name = "cmbEstado";
            ConfigurarCombo(cmbEstado);

            ConfigurarCampo(panelOrigen, lblOrigen, cmbOrigen, "Origen");
            cmbOrigen.Name = "cmbOrigen";
            ConfigurarCombo(cmbOrigen);

            panelRecargar.Dock = DockStyle.Fill;
            panelRecargar.Padding = new Padding(8, 15, 8, 7);
            panelRecargar.Controls.Add(btnRecargar);
            ConfigurarBoton(btnRecargar, "Recargar", Color.FromArgb(52, 101, 164));
            btnRecargar.Dock = DockStyle.Fill;
            btnRecargar.Name = "btnRecargar";

            panelRango.Controls.Add(lblDesde);
            panelRango.Controls.Add(dtpDesde);
            panelRango.Controls.Add(lblHasta);
            panelRango.Controls.Add(dtpHasta);
            panelRango.Dock = DockStyle.Fill;
            panelRango.Name = "panelRango";

            lblDesde.AutoSize = true;
            lblDesde.ForeColor = Color.FromArgb(75, 85, 99);
            lblDesde.Location = new Point(9, 15);
            lblDesde.Text = "Desde";
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(57, 10);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(112, 23);

            lblHasta.AutoSize = true;
            lblHasta.ForeColor = Color.FromArgb(75, 85, 99);
            lblHasta.Location = new Point(188, 15);
            lblHasta.Text = "Hasta";
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(234, 10);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(112, 23);

            ConfigurarCampo(panelCategoria, lblCategoria, cmbCategoria, "Categoría");
            cmbCategoria.Name = "cmbCategoria";
            ConfigurarCombo(cmbCategoria);

            panelBotonesFiltro.Controls.Add(btnAplicar);
            panelBotonesFiltro.Controls.Add(btnLimpiar);
            panelBotonesFiltro.Dock = DockStyle.Fill;
            panelBotonesFiltro.FlowDirection = FlowDirection.RightToLeft;
            panelBotonesFiltro.Padding = new Padding(4, 6, 8, 4);
            panelBotonesFiltro.WrapContents = false;
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
            tableResumen.Controls.Add(panelIngresos, 0, 0);
            tableResumen.Controls.Add(panelGastos, 1, 0);
            tableResumen.Controls.Add(panelResultado, 2, 0);
            tableResumen.Controls.Add(panelCobrar, 3, 0);
            tableResumen.Controls.Add(panelPagar, 4, 0);
            tableResumen.Controls.Add(panelMargen, 5, 0);
            tableResumen.Dock = DockStyle.Fill;
            tableResumen.Margin = new Padding(0);
            tableResumen.RowCount = 1;
            tableResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableResumen.Name = "tableResumen";
            tableResumen.Size = new Size(1152, 92);

            ConfigurarTarjeta(panelIngresos, lblTituloIngresos, lblIngresos, "Ingresos pagados");
            ConfigurarTarjeta(panelGastos, lblTituloGastos, lblGastos, "Gastos pagados");
            ConfigurarTarjeta(panelResultado, lblTituloResultado, lblResultado, "Resultado");
            ConfigurarTarjeta(panelCobrar, lblTituloCobrar, lblCobrar, "Por cobrar");
            ConfigurarTarjeta(panelPagar, lblTituloPagar, lblPagar, "Por pagar");
            ConfigurarTarjeta(panelMargen, lblTituloMargen, lblMargen, "Margen");
            panelIngresos.Name = "panelIngresos";
            panelGastos.Name = "panelGastos";
            panelResultado.Name = "panelResultado";
            panelCobrar.Name = "panelCobrar";
            panelPagar.Name = "panelPagar";
            panelMargen.Name = "panelMargen";
            lblTituloIngresos.Name = "lblTituloIngresos";
            lblTituloGastos.Name = "lblTituloGastos";
            lblTituloResultado.Name = "lblTituloResultado";
            lblTituloCobrar.Name = "lblTituloCobrar";
            lblTituloPagar.Name = "lblTituloPagar";
            lblTituloMargen.Name = "lblTituloMargen";
            lblIngresos.Name = "lblIngresos";
            lblGastos.Name = "lblGastos";
            lblResultado.Name = "lblResultado";
            lblCobrar.Name = "lblCobrar";
            lblPagar.Name = "lblPagar";
            lblMargen.Name = "lblMargen";
            lblIngresos.ForeColor = Color.FromArgb(35, 115, 70);
            lblGastos.ForeColor = Color.FromArgb(176, 58, 46);
            lblCobrar.ForeColor = Color.FromArgb(52, 101, 164);
            lblPagar.ForeColor = Color.FromArgb(176, 92, 26);
            toolTip.SetToolTip(panelResultado,
                "Ingresos pagados menos gastos pagados. Los pendientes se muestran por separado.");

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
            panelGrafico.Controls.Add(chartFinanciero);
            panelGrafico.Controls.Add(lblDetalleGrafico);
            panelGrafico.Controls.Add(panelCabeceraGrafico);
            panelGrafico.Dock = DockStyle.Fill;
            panelGrafico.Margin = new Padding(0, 0, 5, 0);
            panelGrafico.MinimumSize = new Size(1, 1);
            panelGrafico.Name = "panelGrafico";
            panelGrafico.Size = new Size(393, 400);

            panelCabeceraGrafico.BackColor = Color.White;
            panelCabeceraGrafico.Controls.Add(lblTituloGrafico);
            panelCabeceraGrafico.Controls.Add(cmbVistaGrafico);
            panelCabeceraGrafico.Dock = DockStyle.Top;
            panelCabeceraGrafico.Height = 42;
            panelCabeceraGrafico.Name = "panelCabeceraGrafico";
            panelCabeceraGrafico.Size = new Size(391, 42);

            lblTituloGrafico.AutoSize = true;
            lblTituloGrafico.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTituloGrafico.ForeColor = Color.FromArgb(42, 58, 73);
            lblTituloGrafico.Location = new Point(10, 11);
            lblTituloGrafico.Name = "lblTituloGrafico";
            lblTituloGrafico.Text = "Análisis";

            cmbVistaGrafico.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbVistaGrafico.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVistaGrafico.Location = new Point(215, 8);
            cmbVistaGrafico.Name = "cmbVistaGrafico";
            cmbVistaGrafico.Size = new Size(165, 23);

            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 234, 238);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            chartArea.Name = "AreaFinanciera";
            chartFinanciero.ChartAreas.Add(chartArea);
            chartFinanciero.Dock = DockStyle.Fill;
            chartFinanciero.Location = new Point(1, 42);
            legend.Docking = Docking.Bottom;
            legend.Name = "Leyenda";
            chartFinanciero.Legends.Add(legend);
            chartFinanciero.MinimumSize = new Size(1, 1);
            chartFinanciero.Name = "chartFinanciero";
            chartFinanciero.Palette = ChartColorPalette.None;
            chartFinanciero.Size = new Size(391, 322);

            lblDetalleGrafico.Dock = DockStyle.Bottom;
            lblDetalleGrafico.ForeColor = Color.FromArgb(91, 99, 111);
            lblDetalleGrafico.Height = 34;
            lblDetalleGrafico.Name = "lblDetalleGrafico";
            lblDetalleGrafico.Padding = new Padding(10, 2, 10, 0);
            lblDetalleGrafico.Text = "Los movimientos pendientes no forman parte del resultado pagado.";

            panelTabla.BackColor = Color.White;
            panelTabla.BorderStyle = BorderStyle.FixedSingle;
            panelTabla.Controls.Add(dgvMovimientos);
            panelTabla.Controls.Add(lblTituloTabla);
            panelTabla.Dock = DockStyle.Fill;
            panelTabla.Margin = new Padding(5, 0, 0, 0);
            panelTabla.Name = "panelTabla";
            panelTabla.Size = new Size(749, 400);

            lblTituloTabla.Dock = DockStyle.Top;
            lblTituloTabla.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTituloTabla.ForeColor = Color.FromArgb(42, 58, 73);
            lblTituloTabla.Height = 34;
            lblTituloTabla.Name = "lblTituloTabla";
            lblTituloTabla.Padding = new Padding(10, 8, 0, 0);
            lblTituloTabla.Text = "Detalle de movimientos";

            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.AllowUserToOrderColumns = true;
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMovimientos.ColumnHeadersHeight = 36;
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.EnableHeadersVisualStyles = false;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.RowTemplate.Height = 30;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.Size = new Size(747, 364);

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
            lblTotalResultados.Name = "lblTotalResultados";
            lblTotalResultados.Text = "0 movimientos";

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
            Name = "FrmReporteFinanciero";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte financiero";
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            tablePrincipal.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            tableFiltros.ResumeLayout(false);
            panelPeriodo.ResumeLayout(false);
            panelPeriodo.PerformLayout();
            panelBuscar.ResumeLayout(false);
            panelBuscar.PerformLayout();
            panelTipo.ResumeLayout(false);
            panelTipo.PerformLayout();
            panelEstado.ResumeLayout(false);
            panelEstado.PerformLayout();
            panelOrigen.ResumeLayout(false);
            panelOrigen.PerformLayout();
            panelRecargar.ResumeLayout(false);
            panelRango.ResumeLayout(false);
            panelRango.PerformLayout();
            panelCategoria.ResumeLayout(false);
            panelCategoria.PerformLayout();
            panelBotonesFiltro.ResumeLayout(false);
            tableResumen.ResumeLayout(false);
            tableContenido.ResumeLayout(false);
            panelGrafico.ResumeLayout(false);
            panelCabeceraGrafico.ResumeLayout(false);
            panelCabeceraGrafico.PerformLayout();
            ((ISupportInitialize)chartFinanciero).EndInit();
            panelTabla.ResumeLayout(false);
            ((ISupportInitialize)dgvMovimientos).EndInit();
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
            titulo.AutoEllipsis = true;
            titulo.Dock = DockStyle.Top;
            titulo.Font = new Font("Segoe UI", 8.5F);
            titulo.ForeColor = Color.FromArgb(91, 99, 111);
            titulo.Height = 22;
            titulo.Text = texto;
            valor.AutoEllipsis = true;
            valor.Dock = DockStyle.Fill;
            valor.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            valor.ForeColor = Color.FromArgb(35, 52, 70);
            valor.Text = "RD$ 0.00";
            valor.TextAlign = ContentAlignment.MiddleLeft;
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
        private Panel panelPeriodo;
        private Label lblPeriodo;
        private ComboBox cmbPeriodo;
        private Panel panelBuscar;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Panel panelTipo;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Panel panelEstado;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private Panel panelOrigen;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Panel panelRecargar;
        private Button btnRecargar;
        private Panel panelRango;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Panel panelCategoria;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private FlowLayoutPanel panelBotonesFiltro;
        private Button btnAplicar;
        private Button btnLimpiar;
        private TableLayoutPanel tableResumen;
        private Panel panelIngresos;
        private Label lblTituloIngresos;
        private Label lblIngresos;
        private Panel panelGastos;
        private Label lblTituloGastos;
        private Label lblGastos;
        private Panel panelResultado;
        private Label lblTituloResultado;
        private Label lblResultado;
        private Panel panelCobrar;
        private Label lblTituloCobrar;
        private Label lblCobrar;
        private Panel panelPagar;
        private Label lblTituloPagar;
        private Label lblPagar;
        private Panel panelMargen;
        private Label lblTituloMargen;
        private Label lblMargen;
        private TableLayoutPanel tableContenido;
        private Panel panelGrafico;
        private Panel panelCabeceraGrafico;
        private Label lblTituloGrafico;
        private ComboBox cmbVistaGrafico;
        private Chart chartFinanciero;
        private Label lblDetalleGrafico;
        private Panel panelTabla;
        private Label lblTituloTabla;
        private DataGridView dgvMovimientos;
        private Panel panelAcciones;
        private Label lblTotalResultados;
        private FlowLayoutPanel flowAcciones;
        private Button btnVolver;
        private Button btnImprimir;
        private Button btnExportar;
        private ToolTip toolTip;
    }
}

#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace Sistema_Agricola.UI
{
    partial class FrmProduccion
    {
        private System.ComponentModel.IContainer components = null;

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
            panelEncabezado = new Panel();
            btnCerrar = new Button();
            lblTitulo = new Label();
            splitPrincipal = new SplitContainer();
            tabRegistro = new TabControl();
            tabProduccion = new TabPage();
            tablaProduccion = new TableLayoutPanel();
            cmbCosecha = new ComboBox();
            lblDisponible = new Label();
            dtpFechaProduccion = new DateTimePicker();
            cmbUnidad = new ComboBox();
            nudCantidadBruta = new NumericUpDown();
            nudMerma = new NumericUpDown();
            lblCantidadNeta = new Label();
            nudPrecioUnitario = new NumericUpDown();
            lblValorProduccion = new Label();
            nudCostoPostcosecha = new NumericUpDown();
            lblCostoBase = new Label();
            lblBeneficio = new Label();
            txtDestino = new TextBox();
            cmbEstado = new ComboBox();
            txtObservaciones = new TextBox();
            tabVenta = new TabPage();
            tablaVenta = new TableLayoutPanel();
            dtpFechaVenta = new DateTimePicker();
            txtComprador = new TextBox();
            cmbMetodoPago = new ComboBox();
            cmbEstadoCobro = new ComboBox();
            txtReferenciaVenta = new TextBox();
            panelAcciones = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnNuevo = new Button();
            btnAnular = new Button();
            btnEliminar = new Button();
            panelFiltros = new Panel();
            btnRecargar = new Button();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            cmbFiltroEstado = new ComboBox();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            dgvProduccion = new DataGridView();
            tablaResumen = new TableLayoutPanel();
            lblLotesResumen = new Label();
            lblNetaResumen = new Label();
            lblValorResumen = new Label();
            lblBeneficioResumen = new Label();
            lblEstadoFormulario = new Label();
            panelEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.Panel2.SuspendLayout();
            splitPrincipal.SuspendLayout();
            tabRegistro.SuspendLayout();
            tabProduccion.SuspendLayout();
            tablaProduccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadBruta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMerma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioUnitario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCostoPostcosecha).BeginInit();
            tabVenta.SuspendLayout();
            tablaVenta.SuspendLayout();
            panelAcciones.SuspendLayout();
            panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProduccion).BeginInit();
            tablaResumen.SuspendLayout();
            SuspendLayout();
            // 
            // panelEncabezado
            // 
            panelEncabezado.BackColor = Color.FromArgb(38, 92, 74);
            panelEncabezado.Controls.Add(btnCerrar);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1180, 58);
            panelEncabezado.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(176, 58, 46);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(1128, 11);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(38, 34);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "X";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(18, 13);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(310, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de producción";
            // 
            // splitPrincipal
            // 
            splitPrincipal.Dock = DockStyle.Fill;
            splitPrincipal.FixedPanel = FixedPanel.Panel1;
            splitPrincipal.Location = new Point(12, 70);
            splitPrincipal.Name = "splitPrincipal";
            splitPrincipal.Panel1.Controls.Add(tabRegistro);
            splitPrincipal.Panel1.Controls.Add(panelAcciones);
            splitPrincipal.Panel1MinSize = 350;
            splitPrincipal.Panel2.Controls.Add(dgvProduccion);
            splitPrincipal.Panel2.Controls.Add(tablaResumen);
            splitPrincipal.Panel2.Controls.Add(panelFiltros);
            splitPrincipal.Panel2MinSize = 500;
            splitPrincipal.Size = new Size(1156, 588);
            splitPrincipal.SplitterDistance = 390;
            splitPrincipal.SplitterWidth = 8;
            splitPrincipal.TabIndex = 1;
            // 
            // tabRegistro
            // 
            tabRegistro.Controls.Add(tabProduccion);
            tabRegistro.Controls.Add(tabVenta);
            tabRegistro.Dock = DockStyle.Fill;
            tabRegistro.Font = new Font("Segoe UI Semibold", 9.5F);
            tabRegistro.Location = new Point(0, 0);
            tabRegistro.Name = "tabRegistro";
            tabRegistro.SelectedIndex = 0;
            tabRegistro.Size = new Size(390, 530);
            tabRegistro.TabIndex = 0;
            // 
            // tabProduccion
            // 
            tabProduccion.AutoScroll = true;
            tabProduccion.Controls.Add(tablaProduccion);
            tabProduccion.Location = new Point(4, 30);
            tabProduccion.Name = "tabProduccion";
            tabProduccion.Padding = new Padding(10);
            tabProduccion.Size = new Size(382, 496);
            tabProduccion.TabIndex = 0;
            tabProduccion.Text = "Producción";
            tabProduccion.UseVisualStyleBackColor = true;
            // 
            // tablaProduccion
            // 
            tablaProduccion.AutoSize = true;
            tablaProduccion.ColumnCount = 2;
            tablaProduccion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            tablaProduccion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablaProduccion.Controls.Add(CrearEtiqueta("Cosecha"), 0, 0);
            tablaProduccion.Controls.Add(cmbCosecha, 1, 0);
            tablaProduccion.Controls.Add(CrearEtiqueta("Disponible"), 0, 1);
            tablaProduccion.Controls.Add(lblDisponible, 1, 1);
            tablaProduccion.Controls.Add(CrearEtiqueta("Fecha"), 0, 2);
            tablaProduccion.Controls.Add(dtpFechaProduccion, 1, 2);
            tablaProduccion.Controls.Add(CrearEtiqueta("Unidad"), 0, 3);
            tablaProduccion.Controls.Add(cmbUnidad, 1, 3);
            tablaProduccion.Controls.Add(CrearEtiqueta("Cantidad bruta"), 0, 4);
            tablaProduccion.Controls.Add(nudCantidadBruta, 1, 4);
            tablaProduccion.Controls.Add(CrearEtiqueta("Merma"), 0, 5);
            tablaProduccion.Controls.Add(nudMerma, 1, 5);
            tablaProduccion.Controls.Add(CrearEtiqueta("Cantidad neta"), 0, 6);
            tablaProduccion.Controls.Add(lblCantidadNeta, 1, 6);
            tablaProduccion.Controls.Add(CrearEtiqueta("Precio unitario"), 0, 7);
            tablaProduccion.Controls.Add(nudPrecioUnitario, 1, 7);
            tablaProduccion.Controls.Add(CrearEtiqueta("Valor producción"), 0, 8);
            tablaProduccion.Controls.Add(lblValorProduccion, 1, 8);
            tablaProduccion.Controls.Add(CrearEtiqueta("Costo poscosecha"), 0, 9);
            tablaProduccion.Controls.Add(nudCostoPostcosecha, 1, 9);
            tablaProduccion.Controls.Add(CrearEtiqueta("Costo base"), 0, 10);
            tablaProduccion.Controls.Add(lblCostoBase, 1, 10);
            tablaProduccion.Controls.Add(CrearEtiqueta("Beneficio"), 0, 11);
            tablaProduccion.Controls.Add(lblBeneficio, 1, 11);
            tablaProduccion.Controls.Add(CrearEtiqueta("Destino"), 0, 12);
            tablaProduccion.Controls.Add(txtDestino, 1, 12);
            tablaProduccion.Controls.Add(CrearEtiqueta("Estado"), 0, 13);
            tablaProduccion.Controls.Add(cmbEstado, 1, 13);
            tablaProduccion.Controls.Add(CrearEtiqueta("Observaciones"), 0, 14);
            tablaProduccion.Controls.Add(txtObservaciones, 1, 14);
            tablaProduccion.Dock = DockStyle.Top;
            tablaProduccion.Location = new Point(10, 10);
            tablaProduccion.Name = "tablaProduccion";
            tablaProduccion.RowCount = 15;
            tablaProduccion.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            for (int i = 1; i < 14; i++)
                tablaProduccion.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tablaProduccion.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tablaProduccion.Size = new Size(345, 628);
            tablaProduccion.TabIndex = 0;
            // 
            // cmbCosecha
            // 
            cmbCosecha.Dock = DockStyle.Fill;
            cmbCosecha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCosecha.Margin = new Padding(3, 4, 3, 22);
            cmbCosecha.SelectedIndexChanged += cmbCosecha_SelectedIndexChanged;
            // 
            // labels de calculo
            // 
            ConfigurarValor(lblDisponible);
            ConfigurarValor(lblCantidadNeta);
            ConfigurarValor(lblValorProduccion);
            ConfigurarValor(lblCostoBase);
            ConfigurarValor(lblBeneficio);
            // 
            // fechas y combos
            // 
            dtpFechaProduccion.Dock = DockStyle.Fill;
            dtpFechaProduccion.Format = DateTimePickerFormat.Short;
            cmbUnidad.Dock = DockStyle.Fill;
            cmbUnidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Dock = DockStyle.Fill;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // numericos
            // 
            ConfigurarNumerico(nudCantidadBruta, 3);
            ConfigurarNumerico(nudMerma, 3);
            ConfigurarNumerico(nudPrecioUnitario, 2);
            ConfigurarNumerico(nudCostoPostcosecha, 2);
            nudCantidadBruta.ValueChanged += Calculo_ValueChanged;
            nudMerma.ValueChanged += Calculo_ValueChanged;
            nudPrecioUnitario.ValueChanged += Calculo_ValueChanged;
            nudCostoPostcosecha.ValueChanged += Calculo_ValueChanged;
            // 
            // textos
            // 
            txtDestino.Dock = DockStyle.Fill;
            txtDestino.MaxLength = 80;
            txtObservaciones.Dock = DockStyle.Fill;
            txtObservaciones.MaxLength = 250;
            txtObservaciones.Multiline = true;
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            // 
            // tabVenta
            // 
            tabVenta.Controls.Add(tablaVenta);
            tabVenta.Location = new Point(4, 30);
            tabVenta.Name = "tabVenta";
            tabVenta.Padding = new Padding(12);
            tabVenta.Size = new Size(382, 496);
            tabVenta.TabIndex = 1;
            tabVenta.Text = "Venta y cobro";
            tabVenta.UseVisualStyleBackColor = true;
            // 
            // tablaVenta
            // 
            tablaVenta.ColumnCount = 2;
            tablaVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            tablaVenta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablaVenta.Controls.Add(CrearEtiqueta("Fecha de venta"), 0, 0);
            tablaVenta.Controls.Add(dtpFechaVenta, 1, 0);
            tablaVenta.Controls.Add(CrearEtiqueta("Comprador"), 0, 1);
            tablaVenta.Controls.Add(txtComprador, 1, 1);
            tablaVenta.Controls.Add(CrearEtiqueta("Método de pago"), 0, 2);
            tablaVenta.Controls.Add(cmbMetodoPago, 1, 2);
            tablaVenta.Controls.Add(CrearEtiqueta("Estado del cobro"), 0, 3);
            tablaVenta.Controls.Add(cmbEstadoCobro, 1, 3);
            tablaVenta.Controls.Add(CrearEtiqueta("Referencia"), 0, 4);
            tablaVenta.Controls.Add(txtReferenciaVenta, 1, 4);
            tablaVenta.Dock = DockStyle.Top;
            tablaVenta.Location = new Point(12, 12);
            tablaVenta.Name = "tablaVenta";
            tablaVenta.RowCount = 5;
            for (int i = 0; i < 5; i++)
                tablaVenta.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tablaVenta.Size = new Size(358, 240);
            tablaVenta.TabIndex = 0;
            dtpFechaVenta.Dock = DockStyle.Fill;
            dtpFechaVenta.Format = DateTimePickerFormat.Short;
            txtComprador.Dock = DockStyle.Fill;
            txtComprador.MaxLength = 120;
            cmbMetodoPago.Dock = DockStyle.Fill;
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoCobro.Dock = DockStyle.Fill;
            cmbEstadoCobro.DropDownStyle = ComboBoxStyle.DropDownList;
            txtReferenciaVenta.Dock = DockStyle.Fill;
            txtReferenciaVenta.MaxLength = 80;
            // 
            // panelAcciones
            // 
            panelAcciones.Controls.Add(btnGuardar);
            panelAcciones.Controls.Add(btnNuevo);
            panelAcciones.Controls.Add(btnAnular);
            panelAcciones.Controls.Add(btnEliminar);
            panelAcciones.Dock = DockStyle.Bottom;
            panelAcciones.FlowDirection = FlowDirection.LeftToRight;
            panelAcciones.Location = new Point(0, 530);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Padding = new Padding(4, 10, 0, 0);
            panelAcciones.Size = new Size(390, 58);
            panelAcciones.TabIndex = 1;
            ConfigurarBoton(btnGuardar, "Guardar", Color.FromArgb(45, 125, 94), 96);
            ConfigurarBoton(btnNuevo, "Nuevo", Color.FromArgb(52, 101, 164), 82);
            ConfigurarBoton(btnAnular, "Anular", Color.FromArgb(190, 120, 35), 82);
            ConfigurarBoton(btnEliminar, "Eliminar", Color.FromArgb(176, 58, 46), 88);
            btnGuardar.Click += btnGuardar_Click;
            btnNuevo.Click += btnNuevo_Click;
            btnAnular.Click += btnAnular_Click;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // panelFiltros
            // 
            panelFiltros.BackColor = Color.White;
            panelFiltros.Controls.Add(btnRecargar);
            panelFiltros.Controls.Add(btnBuscar);
            panelFiltros.Controls.Add(txtBuscar);
            panelFiltros.Controls.Add(cmbFiltroEstado);
            panelFiltros.Controls.Add(dtpHasta);
            panelFiltros.Controls.Add(dtpDesde);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Padding = new Padding(10);
            panelFiltros.Size = new Size(758, 64);
            panelFiltros.TabIndex = 0;
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(10, 17);
            dtpDesde.Size = new Size(118, 27);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(136, 17);
            dtpHasta.Size = new Size(118, 27);
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Location = new Point(262, 17);
            cmbFiltroEstado.Size = new Size(130, 28);
            txtBuscar.Location = new Point(400, 17);
            txtBuscar.PlaceholderText = "Cultivo, destino o comprador";
            txtBuscar.Size = new Size(190, 27);
            ConfigurarBoton(btnBuscar, "Buscar", Color.FromArgb(45, 125, 94), 72);
            btnBuscar.Location = new Point(598, 14);
            ConfigurarBoton(btnRecargar, "↻", Color.FromArgb(52, 101, 164), 42);
            btnRecargar.Font = new Font("Segoe UI Symbol", 13F, FontStyle.Bold);
            btnRecargar.Location = new Point(678, 14);
            btnBuscar.Click += btnBuscar_Click;
            btnRecargar.Click += btnRecargar_Click;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // dgvProduccion
            // 
            dgvProduccion.AllowUserToAddRows = false;
            dgvProduccion.AllowUserToDeleteRows = false;
            dgvProduccion.AllowUserToOrderColumns = true;
            dgvProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProduccion.BackgroundColor = Color.White;
            dgvProduccion.BorderStyle = BorderStyle.Fixed3D;
            dgvProduccion.ColumnHeadersHeight = 34;
            dgvProduccion.Dock = DockStyle.Fill;
            dgvProduccion.Location = new Point(0, 64);
            dgvProduccion.MultiSelect = false;
            dgvProduccion.Name = "dgvProduccion";
            dgvProduccion.ReadOnly = true;
            dgvProduccion.RowHeadersVisible = false;
            dgvProduccion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduccion.Size = new Size(758, 456);
            dgvProduccion.TabIndex = 1;
            dgvProduccion.CellDoubleClick += dgvProduccion_CellDoubleClick;
            dgvProduccion.DataBindingComplete += dgvProduccion_DataBindingComplete;
            // 
            // tablaResumen
            // 
            tablaResumen.BackColor = Color.FromArgb(238, 243, 246);
            tablaResumen.ColumnCount = 4;
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.Controls.Add(lblLotesResumen, 0, 0);
            tablaResumen.Controls.Add(lblNetaResumen, 1, 0);
            tablaResumen.Controls.Add(lblValorResumen, 2, 0);
            tablaResumen.Controls.Add(lblBeneficioResumen, 3, 0);
            tablaResumen.Dock = DockStyle.Bottom;
            tablaResumen.Location = new Point(0, 520);
            tablaResumen.Name = "tablaResumen";
            tablaResumen.RowCount = 1;
            tablaResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaResumen.Size = new Size(758, 68);
            tablaResumen.TabIndex = 2;
            ConfigurarResumen(lblLotesResumen);
            ConfigurarResumen(lblNetaResumen);
            ConfigurarResumen(lblValorResumen);
            ConfigurarResumen(lblBeneficioResumen);
            // 
            // lblEstadoFormulario
            // 
            lblEstadoFormulario.Dock = DockStyle.Bottom;
            lblEstadoFormulario.ForeColor = Color.FromArgb(70, 80, 90);
            lblEstadoFormulario.Location = new Point(12, 658);
            lblEstadoFormulario.Name = "lblEstadoFormulario";
            lblEstadoFormulario.Padding = new Padding(4, 5, 0, 0);
            lblEstadoFormulario.Size = new Size(1156, 30);
            lblEstadoFormulario.TabIndex = 2;
            lblEstadoFormulario.Text = "Listo";
            // 
            // FrmProduccion
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 247, 250);
            ClientSize = new Size(1180, 688);
            Controls.Add(splitPrincipal);
            Controls.Add(lblEstadoFormulario);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(980, 640);
            Name = "FrmProduccion";
            Padding = new Padding(12, 12, 12, 0);
            Text = "Gestión de producción";
            Load += FrmProduccion_Load;
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            splitPrincipal.Panel1.ResumeLayout(false);
            splitPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            tabRegistro.ResumeLayout(false);
            tabProduccion.ResumeLayout(false);
            tabProduccion.PerformLayout();
            tablaProduccion.ResumeLayout(false);
            tablaProduccion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadBruta).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMerma).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioUnitario).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCostoPostcosecha).EndInit();
            tabVenta.ResumeLayout(false);
            tablaVenta.ResumeLayout(false);
            tablaVenta.PerformLayout();
            panelAcciones.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProduccion).EndInit();
            tablaResumen.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static Label CrearEtiqueta(string texto)
        {
            return new Label
            {
                Text = texto,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(70, 80, 90),
                Margin = new Padding(3, 3, 3, 3)
            };
        }

        private static void ConfigurarValor(Label label)
        {
            label.Dock = DockStyle.Fill;
            label.Text = "0";
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(35, 82, 65);
        }

        private static void ConfigurarNumerico(NumericUpDown control, int decimales)
        {
            control.DecimalPlaces = decimales;
            control.Dock = DockStyle.Fill;
            control.Maximum = 1000000000M;
            control.ThousandsSeparator = true;
        }

        private static void ConfigurarBoton(
            Button boton,
            string texto,
            Color color,
            int ancho)
        {
            boton.Text = texto;
            boton.BackColor = color;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            boton.Size = new Size(ancho, 34);
            boton.Margin = new Padding(3);
            boton.Cursor = Cursors.Hand;
        }

        private static void ConfigurarResumen(Label label)
        {
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(48, 67, 82);
            label.Margin = new Padding(4);
        }

        private Panel panelEncabezado;
        private Button btnCerrar;
        private Label lblTitulo;
        private SplitContainer splitPrincipal;
        private TabControl tabRegistro;
        private TabPage tabProduccion;
        private TabPage tabVenta;
        private TableLayoutPanel tablaProduccion;
        private ComboBox cmbCosecha;
        private Label lblDisponible;
        private DateTimePicker dtpFechaProduccion;
        private ComboBox cmbUnidad;
        private NumericUpDown nudCantidadBruta;
        private NumericUpDown nudMerma;
        private Label lblCantidadNeta;
        private NumericUpDown nudPrecioUnitario;
        private Label lblValorProduccion;
        private NumericUpDown nudCostoPostcosecha;
        private Label lblCostoBase;
        private Label lblBeneficio;
        private TextBox txtDestino;
        private ComboBox cmbEstado;
        private TextBox txtObservaciones;
        private TableLayoutPanel tablaVenta;
        private DateTimePicker dtpFechaVenta;
        private TextBox txtComprador;
        private ComboBox cmbMetodoPago;
        private ComboBox cmbEstadoCobro;
        private TextBox txtReferenciaVenta;
        private FlowLayoutPanel panelAcciones;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnAnular;
        private Button btnEliminar;
        private Panel panelFiltros;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private ComboBox cmbFiltroEstado;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnRecargar;
        private DataGridView dgvProduccion;
        private TableLayoutPanel tablaResumen;
        private Label lblLotesResumen;
        private Label lblNetaResumen;
        private Label lblValorResumen;
        private Label lblBeneficioResumen;
        private Label lblEstadoFormulario;
    }
}

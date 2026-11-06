namespace Sistema_Agricola.UI
{
    partial class FrmInventario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitulo = new Label();
            btnVolver = new Button();
            panelContenido = new Panel();
            splitPrincipal = new SplitContainer();
            tabsOperacion = new TabControl();
            tabProducto = new TabPage();
            lblModoProducto = new Label();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblUnidad = new Label();
            cmbUnidad = new ComboBox();
            lblCosto = new Label();
            nudCosto = new NumericUpDown();
            lblStockMinimo = new Label();
            nudStockMinimo = new NumericUpDown();
            lblStockInicial = new Label();
            nudStockInicial = new NumericUpDown();
            lblUbicacion = new Label();
            txtUbicacion = new TextBox();
            chkVencimiento = new CheckBox();
            dtpVencimiento = new DateTimePicker();
            btnGuardarProducto = new Button();
            btnNuevoProducto = new Button();
            btnEstadoProducto = new Button();
            tabMovimiento = new TabPage();
            lblProductoMovimiento = new Label();
            cmbProductoMovimiento = new ComboBox();
            lblTipoMovimiento = new Label();
            cmbTipoMovimiento = new ComboBox();
            lblCantidadMovimiento = new Label();
            nudCantidadMovimiento = new NumericUpDown();
            lblCostoMovimiento = new Label();
            nudCostoMovimiento = new NumericUpDown();
            lblFechaMovimiento = new Label();
            dtpFechaMovimiento = new DateTimePicker();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            lblReferenciaMovimiento = new Label();
            txtReferenciaMovimiento = new TextBox();
            btnRegistrarMovimiento = new Button();
            panelListado = new Panel();
            tabsDatos = new TabControl();
            tabExistencias = new TabPage();
            dgvProductos = new DataGridView();
            tabHistorial = new TabPage();
            dgvMovimientos = new DataGridView();
            panelFiltroMovimientos = new Panel();
            chkSoloProducto = new CheckBox();
            dtpMovDesde = new DateTimePicker();
            dtpMovHasta = new DateTimePicker();
            btnCargarMovimientos = new Button();
            lblEstado = new Label();
            tablaResumen = new TableLayoutPanel();
            panelProductos = new Panel();
            lblProductos = new Label();
            lblProductosValor = new Label();
            panelBajoStock = new Panel();
            lblBajoStock = new Label();
            lblBajoStockValor = new Label();
            panelValor = new Panel();
            lblValorInventario = new Label();
            lblValorInventarioValor = new Label();
            panelVencimiento = new Panel();
            lblVencimiento = new Label();
            lblVencimientoValor = new Label();
            panelFiltros = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            lblFiltroTipo = new Label();
            cmbFiltroTipo = new ComboBox();
            chkBajoStock = new CheckBox();
            btnBuscar = new Button();

            panelHeader.SuspendLayout();
            panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.Panel2.SuspendLayout();
            splitPrincipal.SuspendLayout();
            tabsOperacion.SuspendLayout();
            tabProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCosto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).BeginInit();
            tabMovimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMovimiento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCostoMovimiento).BeginInit();
            panelListado.SuspendLayout();
            tabsDatos.SuspendLayout();
            tabExistencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tabHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            panelFiltroMovimientos.SuspendLayout();
            tablaResumen.SuspendLayout();
            panelProductos.SuspendLayout();
            panelBajoStock.SuspendLayout();
            panelValor.SuspendLayout();
            panelVencimiento.SuspendLayout();
            panelFiltros.SuspendLayout();
            SuspendLayout();

            panelHeader.BackColor = Color.FromArgb(245, 247, 250);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Controls.Add(btnVolver);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Size = new Size(1250, 68);

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(22, 15);
            lblTitulo.Text = "Inventario agrícola";

            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(192, 57, 43);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1192, 14);
            btnVolver.Size = new Size(42, 40);
            btnVolver.Text = "X";
            btnVolver.Click += btnVolver_Click;

            panelContenido.BackColor = Color.FromArgb(238, 242, 245);
            panelContenido.Controls.Add(splitPrincipal);
            panelContenido.Controls.Add(tablaResumen);
            panelContenido.Controls.Add(panelFiltros);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Padding = new Padding(18);
            panelContenido.Size = new Size(1250, 732);

            panelFiltros.BackColor = Color.White;
            panelFiltros.Size = new Size(1214, 70);
            panelFiltros.Controls.Add(lblBuscar);
            panelFiltros.Controls.Add(txtBuscar);
            panelFiltros.Controls.Add(lblFiltroTipo);
            panelFiltros.Controls.Add(cmbFiltroTipo);
            panelFiltros.Controls.Add(chkBajoStock);
            panelFiltros.Controls.Add(btnBuscar);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Height = 70;

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(16, 7);
            lblBuscar.Text = "Buscar";
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(16, 29);
            txtBuscar.PlaceholderText = "Código, nombre, tipo o ubicación";
            txtBuscar.Size = new Size(520, 30);

            lblFiltroTipo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFiltroTipo.AutoSize = true;
            lblFiltroTipo.Location = new Point(554, 7);
            lblFiltroTipo.Text = "Tipo";
            cmbFiltroTipo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbFiltroTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroTipo.Font = new Font("Segoe UI", 10F);
            cmbFiltroTipo.Location = new Point(554, 28);
            cmbFiltroTipo.Size = new Size(230, 31);

            chkBajoStock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkBajoStock.AutoSize = true;
            chkBajoStock.Location = new Point(810, 33);
            chkBajoStock.Text = "Solo bajo stock";

            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.BackColor = Color.FromArgb(46, 125, 50);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnBuscar.Location = new Point(1080, 25);
            btnBuscar.Size = new Size(118, 36);
            btnBuscar.Text = "Aplicar";
            btnBuscar.Click += btnBuscar_Click;

            tablaResumen.ColumnCount = 4;
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.Controls.Add(panelProductos, 0, 0);
            tablaResumen.Controls.Add(panelBajoStock, 1, 0);
            tablaResumen.Controls.Add(panelValor, 2, 0);
            tablaResumen.Controls.Add(panelVencimiento, 3, 0);
            tablaResumen.Dock = DockStyle.Top;
            tablaResumen.Height = 96;

            ConfigurarPanelResumen(panelProductos, lblProductos, lblProductosValor,
                "Productos activos", "0", Color.FromArgb(52, 73, 94));
            ConfigurarPanelResumen(panelBajoStock, lblBajoStock, lblBajoStockValor,
                "Bajo stock", "0", Color.FromArgb(192, 57, 43));
            ConfigurarPanelResumen(panelValor, lblValorInventario, lblValorInventarioValor,
                "Valor del inventario", "RD$ 0.00", Color.FromArgb(35, 115, 70));
            ConfigurarPanelResumen(panelVencimiento, lblVencimiento, lblVencimientoValor,
                "Vencidos o próximos (30 días)", "0", Color.FromArgb(178, 112, 0));

            splitPrincipal.Dock = DockStyle.Fill;
            splitPrincipal.FixedPanel = FixedPanel.Panel1;
            splitPrincipal.Size = new Size(1214, 548);
            splitPrincipal.SplitterDistance = 380;
            splitPrincipal.SplitterWidth = 5;
            splitPrincipal.Panel1MinSize = 350;
            splitPrincipal.Panel2MinSize = 500;
            splitPrincipal.Panel1.Padding = new Padding(0, 7, 8, 0);
            splitPrincipal.Panel2.Padding = new Padding(8, 7, 0, 0);
            splitPrincipal.Panel1.Controls.Add(tabsOperacion);
            splitPrincipal.Panel2.Controls.Add(panelListado);

            tabsOperacion.Controls.Add(tabProducto);
            tabsOperacion.Controls.Add(tabMovimiento);
            tabsOperacion.Dock = DockStyle.Fill;
            tabsOperacion.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            tabProducto.BackColor = Color.White;
            tabProducto.Text = "Producto";
            tabProducto.Controls.AddRange(new Control[]
            {
                lblModoProducto, lblCodigo, txtCodigo, lblNombre, txtNombre,
                lblTipo, cmbTipo, lblUnidad, cmbUnidad, lblCosto, nudCosto,
                lblStockMinimo, nudStockMinimo, lblStockInicial, nudStockInicial,
                lblUbicacion, txtUbicacion, chkVencimiento, dtpVencimiento,
                btnGuardarProducto, btnNuevoProducto, btnEstadoProducto
            });

            lblModoProducto.AutoSize = true;
            lblModoProducto.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblModoProducto.Location = new Point(18, 16);
            lblModoProducto.Text = "Nuevo producto";
            CrearEtiqueta(lblCodigo, "Código", 18, 54);
            CrearTexto(txtCodigo, 18, 76, 150);
            txtCodigo.MaxLength = 30;
            CrearEtiqueta(lblNombre, "Nombre", 180, 54);
            CrearTexto(txtNombre, 180, 76, 170);
            txtNombre.MaxLength = 100;
            CrearEtiqueta(lblTipo, "Tipo", 18, 116);
            CrearCombo(cmbTipo, 18, 138, 150);
            CrearEtiqueta(lblUnidad, "Unidad", 180, 116);
            CrearCombo(cmbUnidad, 180, 138, 170);
            CrearEtiqueta(lblCosto, "Costo unitario RD$", 18, 178);
            CrearNumero(nudCosto, 18, 200, 150, 2);
            CrearEtiqueta(lblStockMinimo, "Stock mínimo", 180, 178);
            CrearNumero(nudStockMinimo, 180, 200, 170, 3);
            CrearEtiqueta(lblStockInicial, "Stock inicial", 18, 240);
            CrearNumero(nudStockInicial, 18, 262, 150, 3);
            CrearEtiqueta(lblUbicacion, "Ubicación", 180, 240);
            CrearTexto(txtUbicacion, 180, 262, 170);
            txtUbicacion.MaxLength = 80;
            chkVencimiento.AutoSize = true;
            chkVencimiento.Location = new Point(18, 310);
            chkVencimiento.Text = "Tiene vencimiento";
            chkVencimiento.CheckedChanged += chkVencimiento_CheckedChanged;
            dtpVencimiento.Enabled = false;
            dtpVencimiento.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Location = new Point(190, 306);
            dtpVencimiento.Size = new Size(160, 30);
            CrearBoton(btnGuardarProducto, "Guardar", 18, 358, 100, Color.FromArgb(46, 125, 50));
            CrearBoton(btnNuevoProducto, "Nuevo", 128, 358, 100, Color.FromArgb(110, 120, 130));
            CrearBoton(btnEstadoProducto, "Desactivar", 238, 358, 112, Color.FromArgb(192, 57, 43));
            btnGuardarProducto.Click += btnGuardarProducto_Click;
            btnNuevoProducto.Click += btnNuevoProducto_Click;
            btnEstadoProducto.Click += btnEstadoProducto_Click;

            tabMovimiento.BackColor = Color.White;
            tabMovimiento.Text = "Entrada / salida";
            tabMovimiento.Controls.AddRange(new Control[]
            {
                lblProductoMovimiento, cmbProductoMovimiento, lblTipoMovimiento,
                cmbTipoMovimiento, lblCantidadMovimiento, nudCantidadMovimiento,
                lblCostoMovimiento, nudCostoMovimiento, lblFechaMovimiento,
                dtpFechaMovimiento, lblMotivo, txtMotivo, lblReferenciaMovimiento,
                txtReferenciaMovimiento, btnRegistrarMovimiento
            });

            CrearEtiqueta(lblProductoMovimiento, "Producto", 18, 24);
            CrearCombo(cmbProductoMovimiento, 18, 46, 332);
            cmbProductoMovimiento.SelectedIndexChanged += cmbProductoMovimiento_SelectedIndexChanged;
            CrearEtiqueta(lblTipoMovimiento, "Movimiento", 18, 88);
            CrearCombo(cmbTipoMovimiento, 18, 110, 150);
            cmbTipoMovimiento.SelectedIndexChanged += cmbTipoMovimiento_SelectedIndexChanged;
            CrearEtiqueta(lblCantidadMovimiento, "Cantidad", 180, 88);
            CrearNumero(nudCantidadMovimiento, 180, 110, 170, 3);
            CrearEtiqueta(lblCostoMovimiento, "Costo unitario RD$", 18, 150);
            CrearNumero(nudCostoMovimiento, 18, 172, 150, 2);
            CrearEtiqueta(lblFechaMovimiento, "Fecha", 180, 150);
            dtpFechaMovimiento.Format = DateTimePickerFormat.Short;
            dtpFechaMovimiento.Location = new Point(180, 172);
            dtpFechaMovimiento.Size = new Size(170, 30);
            CrearEtiqueta(lblMotivo, "Motivo", 18, 212);
            CrearTexto(txtMotivo, 18, 234, 332);
            txtMotivo.MaxLength = 150;
            CrearEtiqueta(lblReferenciaMovimiento, "Referencia", 18, 274);
            CrearTexto(txtReferenciaMovimiento, 18, 296, 332);
            txtReferenciaMovimiento.MaxLength = 80;
            CrearBoton(btnRegistrarMovimiento, "Registrar movimiento", 18, 350, 190,
                Color.FromArgb(46, 125, 50));
            btnRegistrarMovimiento.Click += btnRegistrarMovimiento_Click;

            panelListado.BackColor = Color.White;
            panelListado.Controls.Add(tabsDatos);
            panelListado.Controls.Add(lblEstado);
            panelListado.Dock = DockStyle.Fill;

            tabsDatos.Controls.Add(tabExistencias);
            tabsDatos.Controls.Add(tabHistorial);
            tabsDatos.Dock = DockStyle.Fill;
            tabsDatos.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            tabExistencias.Controls.Add(dgvProductos);
            tabExistencias.Text = "Existencias";
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.CellDoubleClick += dgvProductos_CellDoubleClick;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;

            tabHistorial.Controls.Add(dgvMovimientos);
            tabHistorial.Controls.Add(panelFiltroMovimientos);
            tabHistorial.Text = "Historial de movimientos";
            panelFiltroMovimientos.Controls.Add(chkSoloProducto);
            panelFiltroMovimientos.Controls.Add(dtpMovDesde);
            panelFiltroMovimientos.Controls.Add(dtpMovHasta);
            panelFiltroMovimientos.Controls.Add(btnCargarMovimientos);
            panelFiltroMovimientos.Dock = DockStyle.Top;
            panelFiltroMovimientos.Height = 50;
            chkSoloProducto.AutoSize = true;
            chkSoloProducto.Location = new Point(10, 15);
            chkSoloProducto.Text = "Solo producto seleccionado";
            dtpMovDesde.Format = DateTimePickerFormat.Short;
            dtpMovDesde.Location = new Point(230, 10);
            dtpMovDesde.Size = new Size(130, 30);
            dtpMovHasta.Format = DateTimePickerFormat.Short;
            dtpMovHasta.Location = new Point(370, 10);
            dtpMovHasta.Size = new Size(130, 30);
            CrearBoton(btnCargarMovimientos, "Aplicar", 515, 9, 90, Color.FromArgb(91, 124, 153));
            btnCargarMovimientos.Click += btnCargarMovimientos_Click;
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.CellFormatting += dgvMovimientos_CellFormatting;

            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(80, 90, 100);
            lblEstado.Height = 30;
            lblEstado.Padding = new Padding(8, 5, 0, 0);
            lblEstado.Text = "Preparando inventario...";

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 800);
            Controls.Add(panelContenido);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1000, 700);
            Name = "FrmInventario";
            Text = "Inventario agrícola";
            Load += FrmInventario_Load;

            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContenido.ResumeLayout(false);
            splitPrincipal.Panel1.ResumeLayout(false);
            splitPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            tabsOperacion.ResumeLayout(false);
            tabProducto.ResumeLayout(false);
            tabProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCosto).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).EndInit();
            tabMovimiento.ResumeLayout(false);
            tabMovimiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMovimiento).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCostoMovimiento).EndInit();
            panelListado.ResumeLayout(false);
            tabsDatos.ResumeLayout(false);
            tabExistencias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tabHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            panelFiltroMovimientos.ResumeLayout(false);
            panelFiltroMovimientos.PerformLayout();
            tablaResumen.ResumeLayout(false);
            panelProductos.ResumeLayout(false);
            panelBajoStock.ResumeLayout(false);
            panelValor.ResumeLayout(false);
            panelVencimiento.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ResumeLayout(false);
        }

        private static void ConfigurarPanelResumen(
            Panel panel, Label titulo, Label valor, string textoTitulo, string textoValor, Color color)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(6, 7, 6, 7);
            titulo.Dock = DockStyle.Top;
            titulo.Height = 25;
            titulo.Padding = new Padding(14, 6, 0, 0);
            titulo.ForeColor = Color.FromArgb(80, 90, 100);
            titulo.Text = textoTitulo;
            valor.Dock = DockStyle.Fill;
            valor.Padding = new Padding(14, 0, 8, 0);
            valor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            valor.ForeColor = color;
            valor.Text = textoValor;
            panel.Controls.Add(valor);
            panel.Controls.Add(titulo);
        }

        private static void CrearEtiqueta(Label label, string texto, int x, int y)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 9F);
            label.Location = new Point(x, y);
            label.Text = texto;
        }

        private static void CrearTexto(TextBox texto, int x, int y, int ancho)
        {
            texto.BorderStyle = BorderStyle.FixedSingle;
            texto.Font = new Font("Segoe UI", 10F);
            texto.Location = new Point(x, y);
            texto.Size = new Size(ancho, 30);
        }

        private static void CrearCombo(ComboBox combo, int x, int y, int ancho)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Font = new Font("Segoe UI", 10F);
            combo.Location = new Point(x, y);
            combo.Size = new Size(ancho, 31);
        }

        private static void CrearNumero(
            NumericUpDown numero, int x, int y, int ancho, int decimales)
        {
            numero.DecimalPlaces = decimales;
            numero.Font = new Font("Segoe UI", 10F);
            numero.Location = new Point(x, y);
            numero.Maximum = 999999999;
            numero.Size = new Size(ancho, 30);
            numero.ThousandsSeparator = true;
        }

        private static void CrearBoton(
            Button boton, string texto, int x, int y, int ancho, Color color)
        {
            boton.BackColor = color;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            boton.ForeColor = Color.White;
            boton.Location = new Point(x, y);
            boton.Size = new Size(ancho, 38);
            boton.Text = texto;
            boton.Cursor = Cursors.Hand;
        }

        private Panel panelHeader;
        private Label lblTitulo;
        private Button btnVolver;
        private Panel panelContenido;
        private Panel panelFiltros;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Label lblFiltroTipo;
        private ComboBox cmbFiltroTipo;
        private CheckBox chkBajoStock;
        private Button btnBuscar;
        private TableLayoutPanel tablaResumen;
        private Panel panelProductos;
        private Label lblProductos;
        private Label lblProductosValor;
        private Panel panelBajoStock;
        private Label lblBajoStock;
        private Label lblBajoStockValor;
        private Panel panelValor;
        private Label lblValorInventario;
        private Label lblValorInventarioValor;
        private Panel panelVencimiento;
        private Label lblVencimiento;
        private Label lblVencimientoValor;
        private SplitContainer splitPrincipal;
        private TabControl tabsOperacion;
        private TabPage tabProducto;
        private Label lblModoProducto;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblUnidad;
        private ComboBox cmbUnidad;
        private Label lblCosto;
        private NumericUpDown nudCosto;
        private Label lblStockMinimo;
        private NumericUpDown nudStockMinimo;
        private Label lblStockInicial;
        private NumericUpDown nudStockInicial;
        private Label lblUbicacion;
        private TextBox txtUbicacion;
        private CheckBox chkVencimiento;
        private DateTimePicker dtpVencimiento;
        private Button btnGuardarProducto;
        private Button btnNuevoProducto;
        private Button btnEstadoProducto;
        private TabPage tabMovimiento;
        private Label lblProductoMovimiento;
        private ComboBox cmbProductoMovimiento;
        private Label lblTipoMovimiento;
        private ComboBox cmbTipoMovimiento;
        private Label lblCantidadMovimiento;
        private NumericUpDown nudCantidadMovimiento;
        private Label lblCostoMovimiento;
        private NumericUpDown nudCostoMovimiento;
        private Label lblFechaMovimiento;
        private DateTimePicker dtpFechaMovimiento;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Label lblReferenciaMovimiento;
        private TextBox txtReferenciaMovimiento;
        private Button btnRegistrarMovimiento;
        private Panel panelListado;
        private TabControl tabsDatos;
        private TabPage tabExistencias;
        private DataGridView dgvProductos;
        private TabPage tabHistorial;
        private Panel panelFiltroMovimientos;
        private CheckBox chkSoloProducto;
        private DateTimePicker dtpMovDesde;
        private DateTimePicker dtpMovHasta;
        private Button btnCargarMovimientos;
        private DataGridView dgvMovimientos;
        private Label lblEstado;
    }
}

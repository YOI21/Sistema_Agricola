namespace Sistema_Agricola.UI
{
    partial class FrmFinanzas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            btnVolver = new Button();
            lblTitulo = new Label();
            panelContenido = new Panel();
            splitPrincipal = new SplitContainer();
            grupoRegistro = new GroupBox();
            btnEliminar = new Button();
            btnNuevo = new Button();
            btnGuardar = new Button();
            txtReferencia = new TextBox();
            lblReferencia = new Label();
            cmbEstado = new ComboBox();
            lblEstadoPago = new Label();
            cmbMetodo = new ComboBox();
            lblMetodo = new Label();
            dtpFecha = new DateTimePicker();
            lblFecha = new Label();
            nudMonto = new NumericUpDown();
            lblMonto = new Label();
            txtDescripcion = new TextBox();
            lblDescripcion = new Label();
            cmbCategoria = new ComboBox();
            lblCategoria = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            lblModo = new Label();
            grupoListado = new GroupBox();
            dgvMovimientos = new DataGridView();
            lblEstado = new Label();
            tablaResumen = new TableLayoutPanel();
            panelIngresos = new Panel();
            lblIngresosValor = new Label();
            lblIngresos = new Label();
            panelGastos = new Panel();
            lblGastosValor = new Label();
            lblGastos = new Label();
            panelBalance = new Panel();
            lblBalanceValor = new Label();
            lblBalance = new Label();
            panelPendientes = new Panel();
            lblPendientesValor = new Label();
            lblPendientes = new Label();
            panelFiltros = new Panel();
            btnExportar = new Button();
            btnAplicar = new Button();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            dtpHasta = new DateTimePicker();
            lblHasta = new Label();
            dtpDesde = new DateTimePicker();
            lblDesde = new Label();
            cmbPeriodo = new ComboBox();
            lblPeriodo = new Label();
            panelHeader.SuspendLayout();
            panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.Panel2.SuspendLayout();
            splitPrincipal.SuspendLayout();
            grupoRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).BeginInit();
            grupoListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            tablaResumen.SuspendLayout();
            panelIngresos.SuspendLayout();
            panelGastos.SuspendLayout();
            panelBalance.SuspendLayout();
            panelPendientes.SuspendLayout();
            panelFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(245, 247, 250);
            panelHeader.Controls.Add(btnVolver);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1250, 68);
            panelHeader.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(192, 57, 43);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1192, 14);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(42, 40);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "X";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(22, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(241, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Control financiero";
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.FromArgb(238, 242, 245);
            panelContenido.Controls.Add(splitPrincipal);
            panelContenido.Controls.Add(tablaResumen);
            panelContenido.Controls.Add(panelFiltros);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 68);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(18);
            panelContenido.Size = new Size(1250, 732);
            panelContenido.TabIndex = 1;
            // 
            // splitPrincipal
            // 
            splitPrincipal.Dock = DockStyle.Fill;
            splitPrincipal.FixedPanel = FixedPanel.Panel1;
            splitPrincipal.Location = new Point(18, 184);
            splitPrincipal.Name = "splitPrincipal";
            // 
            // splitPrincipal.Panel1
            // 
            splitPrincipal.Panel1.Controls.Add(grupoRegistro);
            splitPrincipal.Panel1.Padding = new Padding(0, 6, 8, 0);
            splitPrincipal.Panel1MinSize = 330;
            // 
            // splitPrincipal.Panel2
            // 
            splitPrincipal.Panel2.Controls.Add(grupoListado);
            splitPrincipal.Panel2.Padding = new Padding(8, 6, 0, 0);
            splitPrincipal.Panel2MinSize = 500;
            splitPrincipal.Size = new Size(1214, 530);
            splitPrincipal.SplitterDistance = 360;
            splitPrincipal.SplitterWidth = 4;
            splitPrincipal.TabIndex = 2;
            // 
            // grupoRegistro
            // 
            grupoRegistro.BackColor = Color.White;
            grupoRegistro.Controls.Add(btnEliminar);
            grupoRegistro.Controls.Add(btnNuevo);
            grupoRegistro.Controls.Add(btnGuardar);
            grupoRegistro.Controls.Add(txtReferencia);
            grupoRegistro.Controls.Add(lblReferencia);
            grupoRegistro.Controls.Add(cmbEstado);
            grupoRegistro.Controls.Add(lblEstadoPago);
            grupoRegistro.Controls.Add(cmbMetodo);
            grupoRegistro.Controls.Add(lblMetodo);
            grupoRegistro.Controls.Add(dtpFecha);
            grupoRegistro.Controls.Add(lblFecha);
            grupoRegistro.Controls.Add(nudMonto);
            grupoRegistro.Controls.Add(lblMonto);
            grupoRegistro.Controls.Add(txtDescripcion);
            grupoRegistro.Controls.Add(lblDescripcion);
            grupoRegistro.Controls.Add(cmbCategoria);
            grupoRegistro.Controls.Add(lblCategoria);
            grupoRegistro.Controls.Add(cmbTipo);
            grupoRegistro.Controls.Add(lblTipo);
            grupoRegistro.Controls.Add(lblModo);
            grupoRegistro.Dock = DockStyle.Fill;
            grupoRegistro.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grupoRegistro.ForeColor = Color.FromArgb(52, 73, 94);
            grupoRegistro.Location = new Point(0, 6);
            grupoRegistro.Name = "grupoRegistro";
            grupoRegistro.Padding = new Padding(16);
            grupoRegistro.Size = new Size(352, 524);
            grupoRegistro.TabIndex = 0;
            grupoRegistro.TabStop = false;
            grupoRegistro.Text = "Movimiento manual";
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.BackColor = Color.FromArgb(192, 57, 43);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Enabled = false;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(232, 406);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 38);
            btnEliminar.TabIndex = 19;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(110, 120, 130);
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(126, 406);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(94, 38);
            btnNuevo.TabIndex = 18;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 125, 50);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 406);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 38);
            btnGuardar.TabIndex = 17;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtReferencia
            // 
            txtReferencia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtReferencia.BorderStyle = BorderStyle.FixedSingle;
            txtReferencia.Font = new Font("Segoe UI", 10F);
            txtReferencia.Location = new Point(20, 357);
            txtReferencia.MaxLength = 80;
            txtReferencia.Name = "txtReferencia";
            txtReferencia.PlaceholderText = "Factura, comprobante o nota";
            txtReferencia.Size = new Size(306, 30);
            txtReferencia.TabIndex = 16;
            // 
            // lblReferencia
            // 
            lblReferencia.AutoSize = true;
            lblReferencia.Font = new Font("Segoe UI", 9F);
            lblReferencia.Location = new Point(20, 334);
            lblReferencia.Name = "lblReferencia";
            lblReferencia.Size = new Size(81, 20);
            lblReferencia.TabIndex = 15;
            lblReferencia.Text = "Referencia";
            // 
            // cmbEstado
            // 
            cmbEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Font = new Font("Segoe UI", 10F);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(181, 293);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(145, 31);
            cmbEstado.TabIndex = 14;
            // 
            // lblEstadoPago
            // 
            lblEstadoPago.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEstadoPago.AutoSize = true;
            lblEstadoPago.Font = new Font("Segoe UI", 9F);
            lblEstadoPago.Location = new Point(181, 270);
            lblEstadoPago.Name = "lblEstadoPago";
            lblEstadoPago.Size = new Size(54, 20);
            lblEstadoPago.TabIndex = 13;
            lblEstadoPago.Text = "Estado";
            // 
            // cmbMetodo
            // 
            cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodo.Font = new Font("Segoe UI", 10F);
            cmbMetodo.FormattingEnabled = true;
            cmbMetodo.Location = new Point(20, 293);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(145, 31);
            cmbMetodo.TabIndex = 12;
            // 
            // lblMetodo
            // 
            lblMetodo.AutoSize = true;
            lblMetodo.Font = new Font("Segoe UI", 9F);
            lblMetodo.Location = new Point(20, 270);
            lblMetodo.Name = "lblMetodo";
            lblMetodo.Size = new Size(119, 20);
            lblMetodo.TabIndex = 11;
            lblMetodo.Text = "Método de pago";
            // 
            // dtpFecha
            // 
            dtpFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpFecha.Font = new Font("Segoe UI", 10F);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(181, 229);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(145, 30);
            dtpFecha.TabIndex = 10;
            // 
            // lblFecha
            // 
            lblFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F);
            lblFecha.Location = new Point(181, 206);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 20);
            lblFecha.TabIndex = 9;
            lblFecha.Text = "Fecha";
            // 
            // nudMonto
            // 
            nudMonto.DecimalPlaces = 2;
            nudMonto.Font = new Font("Segoe UI", 10F);
            nudMonto.Location = new Point(20, 229);
            nudMonto.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudMonto.Name = "nudMonto";
            nudMonto.Size = new Size(145, 30);
            nudMonto.TabIndex = 8;
            nudMonto.ThousandsSeparator = true;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Segoe UI", 9F);
            lblMonto.Location = new Point(20, 206);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(90, 20);
            lblMonto.TabIndex = 7;
            lblMonto.Text = "Monto RD$";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            txtDescripcion.Font = new Font("Segoe UI", 10F);
            txtDescripcion.Location = new Point(20, 165);
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Detalle del movimiento";
            txtDescripcion.Size = new Size(306, 30);
            txtDescripcion.TabIndex = 6;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9F);
            lblDescripcion.Location = new Point(20, 142);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(87, 20);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "Descripción";
            // 
            // cmbCategoria
            // 
            cmbCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Font = new Font("Segoe UI", 10F);
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(150, 101);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(176, 31);
            cmbCategoria.TabIndex = 4;
            // 
            // lblCategoria
            // 
            lblCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F);
            lblCategoria.Location = new Point(150, 78);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(74, 20);
            lblCategoria.TabIndex = 3;
            lblCategoria.Text = "Categoría";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Font = new Font("Segoe UI", 10F);
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(20, 101);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(114, 31);
            cmbTipo.TabIndex = 2;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI", 9F);
            lblTipo.Location = new Point(20, 78);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(38, 20);
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo";
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblModo.ForeColor = Color.FromArgb(70, 88, 104);
            lblModo.Location = new Point(20, 38);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(211, 23);
            lblModo.TabIndex = 0;
            lblModo.Text = "Nuevo movimiento manual";
            // 
            // grupoListado
            // 
            grupoListado.BackColor = Color.White;
            grupoListado.Controls.Add(dgvMovimientos);
            grupoListado.Controls.Add(lblEstado);
            grupoListado.Dock = DockStyle.Fill;
            grupoListado.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grupoListado.ForeColor = Color.FromArgb(52, 73, 94);
            grupoListado.Location = new Point(8, 6);
            grupoListado.Name = "grupoListado";
            grupoListado.Padding = new Padding(12);
            grupoListado.Size = new Size(842, 524);
            grupoListado.TabIndex = 0;
            grupoListado.TabStop = false;
            grupoListado.Text = "Libro de movimientos";
            // 
            // dgvMovimientos
            // 
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.ColumnHeadersHeight = 34;
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.Location = new Point(12, 35);
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.RowHeadersWidth = 51;
            dgvMovimientos.RowTemplate.Height = 30;
            dgvMovimientos.Size = new Size(818, 447);
            dgvMovimientos.TabIndex = 0;
            dgvMovimientos.CellDoubleClick += dgvMovimientos_CellDoubleClick;
            dgvMovimientos.CellFormatting += dgvMovimientos_CellFormatting;
            // 
            // lblEstado
            // 
            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(80, 90, 100);
            lblEstado.Location = new Point(12, 482);
            lblEstado.Name = "lblEstado";
            lblEstado.Padding = new Padding(4, 5, 0, 0);
            lblEstado.Size = new Size(818, 30);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Preparando información financiera...";
            // 
            // tablaResumen
            // 
            tablaResumen.ColumnCount = 4;
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tablaResumen.Controls.Add(panelIngresos, 0, 0);
            tablaResumen.Controls.Add(panelGastos, 1, 0);
            tablaResumen.Controls.Add(panelBalance, 2, 0);
            tablaResumen.Controls.Add(panelPendientes, 3, 0);
            tablaResumen.Dock = DockStyle.Top;
            tablaResumen.Location = new Point(18, 88);
            tablaResumen.Name = "tablaResumen";
            tablaResumen.RowCount = 1;
            tablaResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaResumen.Size = new Size(1214, 96);
            tablaResumen.TabIndex = 1;
            // 
            // panelIngresos
            // 
            panelIngresos.BackColor = Color.White;
            panelIngresos.BorderStyle = BorderStyle.FixedSingle;
            panelIngresos.Controls.Add(lblIngresosValor);
            panelIngresos.Controls.Add(lblIngresos);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(6, 7);
            panelIngresos.Margin = new Padding(6, 7, 6, 7);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(291, 82);
            panelIngresos.TabIndex = 0;
            // 
            // lblIngresosValor
            // 
            lblIngresosValor.AutoEllipsis = true;
            lblIngresosValor.Dock = DockStyle.Fill;
            lblIngresosValor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblIngresosValor.ForeColor = Color.FromArgb(35, 115, 70);
            lblIngresosValor.Location = new Point(0, 25);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Padding = new Padding(14, 0, 10, 0);
            lblIngresosValor.Size = new Size(289, 55);
            lblIngresosValor.TabIndex = 1;
            lblIngresosValor.Text = "RD$ 0.00";
            // 
            // lblIngresos
            // 
            lblIngresos.Dock = DockStyle.Top;
            lblIngresos.Font = new Font("Segoe UI", 9F);
            lblIngresos.ForeColor = Color.FromArgb(80, 90, 100);
            lblIngresos.Location = new Point(0, 0);
            lblIngresos.Name = "lblIngresos";
            lblIngresos.Padding = new Padding(14, 6, 0, 0);
            lblIngresos.Size = new Size(289, 25);
            lblIngresos.TabIndex = 0;
            lblIngresos.Text = "Ingresos cobrados";
            // 
            // panelGastos
            // 
            panelGastos.BackColor = Color.White;
            panelGastos.BorderStyle = BorderStyle.FixedSingle;
            panelGastos.Controls.Add(lblGastosValor);
            panelGastos.Controls.Add(lblGastos);
            panelGastos.Dock = DockStyle.Fill;
            panelGastos.Location = new Point(309, 7);
            panelGastos.Margin = new Padding(6, 7, 6, 7);
            panelGastos.Name = "panelGastos";
            panelGastos.Size = new Size(291, 82);
            panelGastos.TabIndex = 1;
            // 
            // lblGastosValor
            // 
            lblGastosValor.AutoEllipsis = true;
            lblGastosValor.Dock = DockStyle.Fill;
            lblGastosValor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblGastosValor.ForeColor = Color.FromArgb(192, 57, 43);
            lblGastosValor.Location = new Point(0, 25);
            lblGastosValor.Name = "lblGastosValor";
            lblGastosValor.Padding = new Padding(14, 0, 10, 0);
            lblGastosValor.Size = new Size(289, 55);
            lblGastosValor.TabIndex = 1;
            lblGastosValor.Text = "RD$ 0.00";
            // 
            // lblGastos
            // 
            lblGastos.Dock = DockStyle.Top;
            lblGastos.Font = new Font("Segoe UI", 9F);
            lblGastos.ForeColor = Color.FromArgb(80, 90, 100);
            lblGastos.Location = new Point(0, 0);
            lblGastos.Name = "lblGastos";
            lblGastos.Padding = new Padding(14, 6, 0, 0);
            lblGastos.Size = new Size(289, 25);
            lblGastos.TabIndex = 0;
            lblGastos.Text = "Gastos pagados";
            // 
            // panelBalance
            // 
            panelBalance.BackColor = Color.White;
            panelBalance.BorderStyle = BorderStyle.FixedSingle;
            panelBalance.Controls.Add(lblBalanceValor);
            panelBalance.Controls.Add(lblBalance);
            panelBalance.Dock = DockStyle.Fill;
            panelBalance.Location = new Point(612, 7);
            panelBalance.Margin = new Padding(6, 7, 6, 7);
            panelBalance.Name = "panelBalance";
            panelBalance.Size = new Size(291, 82);
            panelBalance.TabIndex = 2;
            // 
            // lblBalanceValor
            // 
            lblBalanceValor.AutoEllipsis = true;
            lblBalanceValor.Dock = DockStyle.Fill;
            lblBalanceValor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBalanceValor.ForeColor = Color.FromArgb(35, 115, 70);
            lblBalanceValor.Location = new Point(0, 25);
            lblBalanceValor.Name = "lblBalanceValor";
            lblBalanceValor.Padding = new Padding(14, 0, 10, 0);
            lblBalanceValor.Size = new Size(289, 55);
            lblBalanceValor.TabIndex = 1;
            lblBalanceValor.Text = "RD$ 0.00";
            // 
            // lblBalance
            // 
            lblBalance.Dock = DockStyle.Top;
            lblBalance.Font = new Font("Segoe UI", 9F);
            lblBalance.ForeColor = Color.FromArgb(80, 90, 100);
            lblBalance.Location = new Point(0, 0);
            lblBalance.Name = "lblBalance";
            lblBalance.Padding = new Padding(14, 6, 0, 0);
            lblBalance.Size = new Size(289, 25);
            lblBalance.TabIndex = 0;
            lblBalance.Text = "Balance realizado";
            // 
            // panelPendientes
            // 
            panelPendientes.BackColor = Color.White;
            panelPendientes.BorderStyle = BorderStyle.FixedSingle;
            panelPendientes.Controls.Add(lblPendientesValor);
            panelPendientes.Controls.Add(lblPendientes);
            panelPendientes.Dock = DockStyle.Fill;
            panelPendientes.Location = new Point(915, 7);
            panelPendientes.Margin = new Padding(6, 7, 6, 7);
            panelPendientes.Name = "panelPendientes";
            panelPendientes.Size = new Size(293, 82);
            panelPendientes.TabIndex = 3;
            // 
            // lblPendientesValor
            // 
            lblPendientesValor.AutoEllipsis = true;
            lblPendientesValor.Dock = DockStyle.Fill;
            lblPendientesValor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPendientesValor.ForeColor = Color.FromArgb(178, 112, 0);
            lblPendientesValor.Location = new Point(0, 25);
            lblPendientesValor.Name = "lblPendientesValor";
            lblPendientesValor.Padding = new Padding(14, 7, 8, 0);
            lblPendientesValor.Size = new Size(291, 55);
            lblPendientesValor.TabIndex = 1;
            lblPendientesValor.Text = "Cobrar RD$ 0.00 | Pagar RD$ 0.00";
            // 
            // lblPendientes
            // 
            lblPendientes.Dock = DockStyle.Top;
            lblPendientes.Font = new Font("Segoe UI", 9F);
            lblPendientes.ForeColor = Color.FromArgb(80, 90, 100);
            lblPendientes.Location = new Point(0, 0);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Padding = new Padding(14, 6, 0, 0);
            lblPendientes.Size = new Size(291, 25);
            lblPendientes.TabIndex = 0;
            lblPendientes.Text = "Movimientos pendientes";
            // 
            // panelFiltros
            // 
            panelFiltros.BackColor = Color.White;
            panelFiltros.Controls.Add(btnExportar);
            panelFiltros.Controls.Add(btnAplicar);
            panelFiltros.Controls.Add(txtBuscar);
            panelFiltros.Controls.Add(lblBuscar);
            panelFiltros.Controls.Add(dtpHasta);
            panelFiltros.Controls.Add(lblHasta);
            panelFiltros.Controls.Add(dtpDesde);
            panelFiltros.Controls.Add(lblDesde);
            panelFiltros.Controls.Add(cmbPeriodo);
            panelFiltros.Controls.Add(lblPeriodo);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(18, 18);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(1214, 70);
            panelFiltros.TabIndex = 0;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.BackColor = Color.FromArgb(91, 124, 153);
            btnExportar.Cursor = Cursors.Hand;
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.FlatStyle = FlatStyle.Flat;
            btnExportar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnExportar.ForeColor = Color.White;
            btnExportar.Location = new Point(1091, 24);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(108, 34);
            btnExportar.TabIndex = 9;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = false;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAplicar.BackColor = Color.FromArgb(46, 125, 50);
            btnAplicar.Cursor = Cursors.Hand;
            btnAplicar.FlatAppearance.BorderSize = 0;
            btnAplicar.FlatStyle = FlatStyle.Flat;
            btnAplicar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnAplicar.ForeColor = Color.White;
            btnAplicar.Location = new Point(985, 24);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(94, 34);
            btnAplicar.TabIndex = 8;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(675, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Categoría, descripción, referencia...";
            txtBuscar.Size = new Size(294, 30);
            txtBuscar.TabIndex = 7;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 8.5F);
            lblBuscar.ForeColor = Color.FromArgb(70, 80, 90);
            lblBuscar.Location = new Point(675, 5);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(50, 20);
            lblBuscar.TabIndex = 6;
            lblBuscar.Text = "Buscar";
            // 
            // dtpHasta
            // 
            dtpHasta.Enabled = false;
            dtpHasta.Font = new Font("Segoe UI", 9.5F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(520, 28);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(137, 29);
            dtpHasta.TabIndex = 5;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 8.5F);
            lblHasta.ForeColor = Color.FromArgb(70, 80, 90);
            lblHasta.Location = new Point(520, 5);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(43, 20);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta";
            // 
            // dtpDesde
            // 
            dtpDesde.Enabled = false;
            dtpDesde.Font = new Font("Segoe UI", 9.5F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(365, 28);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(137, 29);
            dtpDesde.TabIndex = 3;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 8.5F);
            lblDesde.ForeColor = Color.FromArgb(70, 80, 90);
            lblDesde.Location = new Point(365, 5);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(48, 20);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde";
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.Font = new Font("Segoe UI", 10F);
            cmbPeriodo.FormattingEnabled = true;
            cmbPeriodo.Location = new Point(16, 27);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(330, 31);
            cmbPeriodo.TabIndex = 1;
            cmbPeriodo.SelectedIndexChanged += cmbPeriodo_SelectedIndexChanged;
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Font = new Font("Segoe UI", 8.5F);
            lblPeriodo.ForeColor = Color.FromArgb(70, 80, 90);
            lblPeriodo.Location = new Point(16, 5);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size(57, 20);
            lblPeriodo.TabIndex = 0;
            lblPeriodo.Text = "Período";
            // 
            // FrmFinanzas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1250, 800);
            Controls.Add(panelContenido);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1000, 700);
            Name = "FrmFinanzas";
            Text = "Control financiero";
            Load += FrmFinanzas_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContenido.ResumeLayout(false);
            splitPrincipal.Panel1.ResumeLayout(false);
            splitPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            grupoRegistro.ResumeLayout(false);
            grupoRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).EndInit();
            grupoListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            tablaResumen.ResumeLayout(false);
            panelIngresos.ResumeLayout(false);
            panelGastos.ResumeLayout(false);
            panelBalance.ResumeLayout(false);
            panelPendientes.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitulo;
        private Button btnVolver;
        private Panel panelContenido;
        private Panel panelFiltros;
        private ComboBox cmbPeriodo;
        private Label lblPeriodo;
        private DateTimePicker dtpDesde;
        private Label lblDesde;
        private DateTimePicker dtpHasta;
        private Label lblHasta;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Button btnAplicar;
        private Button btnExportar;
        private TableLayoutPanel tablaResumen;
        private Panel panelIngresos;
        private Label lblIngresos;
        private Label lblIngresosValor;
        private Panel panelGastos;
        private Label lblGastos;
        private Label lblGastosValor;
        private Panel panelBalance;
        private Label lblBalance;
        private Label lblBalanceValor;
        private Panel panelPendientes;
        private Label lblPendientes;
        private Label lblPendientesValor;
        private SplitContainer splitPrincipal;
        private GroupBox grupoRegistro;
        private Label lblModo;
        private ComboBox cmbTipo;
        private Label lblTipo;
        private ComboBox cmbCategoria;
        private Label lblCategoria;
        private TextBox txtDescripcion;
        private Label lblDescripcion;
        private NumericUpDown nudMonto;
        private Label lblMonto;
        private DateTimePicker dtpFecha;
        private Label lblFecha;
        private ComboBox cmbMetodo;
        private Label lblMetodo;
        private ComboBox cmbEstado;
        private Label lblEstadoPago;
        private TextBox txtReferencia;
        private Label lblReferencia;
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEliminar;
        private GroupBox grupoListado;
        private DataGridView dgvMovimientos;
        private Label lblEstado;
    }
}

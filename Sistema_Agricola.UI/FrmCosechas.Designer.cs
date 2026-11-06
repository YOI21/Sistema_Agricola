namespace Sistema_Agricola.UI
{
    partial class FrmCosechas
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            btnCerrar = new Button();
            tabControl = new TabControl();
            tabDatos = new TabPage();
            pnlDatos = new Panel();
            tlpDatos = new TableLayoutPanel();
            label2 = new Label();
            txtIDCosecha = new TextBox();
            label3 = new Label();
            cbxCultivo = new ComboBox();
            label4 = new Label();
            dtpFechaCosecha = new DateTimePicker();
            label5 = new Label();
            cbxEstadoCosecha = new ComboBox();
            label6 = new Label();
            numCantidadCosechada = new NumericUpDown();
            lblCantidadSembrada = new Label();
            label7 = new Label();
            numCalidad = new NumericUpDown();
            label8 = new Label();
            numRendimiento = new NumericUpDown();
            label9 = new Label();
            txtObservaciones = new TextBox();
            tabEstadisticas = new TabPage();
            pnlEstadisticas = new Panel();
            chartRendimiento = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabHistorial = new TabPage();
            pnlHistorial = new Panel();
            dgvCosechas = new DataGridView();
            tabFinanzas = new TabPage();
            pnlFinanzas = new Panel();
            tlpFinanzas = new TableLayoutPanel();
            label10 = new Label();
            numCostoCosecha = new NumericUpDown();
            lblCostoSiembra = new Label();
            label12 = new Label();
            numPrecioVenta = new NumericUpDown();
            label13 = new Label();
            lblBeneficio = new Label();
            pnlBotones = new Panel();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnVolver = new Button();
            pnlHeader.SuspendLayout();
            tabControl.SuspendLayout();
            tabDatos.SuspendLayout();
            pnlDatos.SuspendLayout();
            tlpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadCosechada).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCalidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRendimiento).BeginInit();
            tabEstadisticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRendimiento).BeginInit();
            tabHistorial.SuspendLayout();
            pnlHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCosechas).BeginInit();
            tabFinanzas.SuspendLayout();
            pnlFinanzas.SuspendLayout();
            tlpFinanzas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCostoCosecha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioVenta).BeginInit();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(245, 247, 250);
            pnlHeader.BorderStyle = BorderStyle.None;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnCerrar);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1109, 70);
            pnlHeader.TabIndex = 2;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(62, 79, 98);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Padding = new Padding(30, 0, 0, 0);
            lblTitulo.Size = new Size(1049, 70);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de cosechas";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(231, 76, 60);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Dock = DockStyle.Right;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(1049, 0);
            btnCerrar.Margin = new Padding(3, 4, 3, 4);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(60, 70);
            btnCerrar.TabIndex = 1;
            btnCerrar.TabStop = false;
            btnCerrar.Text = "X";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnVolver_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDatos);
            tabControl.Controls.Add(tabEstadisticas);
            tabControl.Controls.Add(tabHistorial);
            tabControl.Controls.Add(tabFinanzas);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.ItemSize = new Size(140, 38);
            tabControl.Location = new Point(0, 70);
            tabControl.Margin = new Padding(3, 4, 3, 4);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1109, 637);
            tabControl.SizeMode = TabSizeMode.Fixed;
            tabControl.TabIndex = 0;
            // 
            // tabDatos
            // 
            tabDatos.BackColor = Color.FromArgb(250, 250, 250);
            tabDatos.Controls.Add(pnlDatos);
            tabDatos.Location = new Point(4, 42);
            tabDatos.Margin = new Padding(3, 4, 3, 4);
            tabDatos.Name = "tabDatos";
            tabDatos.Padding = new Padding(15, 15, 15, 15);
            tabDatos.Size = new Size(1101, 591);
            tabDatos.TabIndex = 0;
            tabDatos.Text = "Datos de cosecha";
            // 
            // pnlDatos
            // 
            pnlDatos.AutoScroll = true;
            pnlDatos.BackColor = Color.White;
            pnlDatos.BorderStyle = BorderStyle.None;
            pnlDatos.Controls.Add(tlpDatos);
            pnlDatos.Dock = DockStyle.Fill;
            pnlDatos.Location = new Point(15, 15);
            pnlDatos.Margin = new Padding(3, 4, 3, 4);
            pnlDatos.Name = "pnlDatos";
            pnlDatos.Padding = new Padding(20, 20, 20, 20);
            pnlDatos.Size = new Size(1071, 561);
            pnlDatos.TabIndex = 0;
            // 
            // tlpDatos
            // 
            tlpDatos.AutoSize = true;
            tlpDatos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDatos.ColumnCount = 2;
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDatos.Controls.Add(label2, 0, 0);
            tlpDatos.Controls.Add(txtIDCosecha, 1, 0);
            tlpDatos.Controls.Add(label3, 0, 1);
            tlpDatos.Controls.Add(cbxCultivo, 1, 1);
            tlpDatos.Controls.Add(label4, 0, 2);
            tlpDatos.Controls.Add(dtpFechaCosecha, 1, 2);
            tlpDatos.Controls.Add(label5, 0, 3);
            tlpDatos.Controls.Add(cbxEstadoCosecha, 1, 3);
            tlpDatos.Controls.Add(label6, 0, 4);
            tlpDatos.Controls.Add(numCantidadCosechada, 1, 4);
            tlpDatos.Controls.Add(lblCantidadSembrada, 0, 5);
            tlpDatos.Controls.Add(label7, 0, 6);
            tlpDatos.Controls.Add(numCalidad, 1, 6);
            tlpDatos.Controls.Add(label8, 0, 7);
            tlpDatos.Controls.Add(numRendimiento, 1, 7);
            tlpDatos.Controls.Add(label9, 0, 8);
            tlpDatos.Controls.Add(txtObservaciones, 1, 8);
            tlpDatos.Dock = DockStyle.Top;
            tlpDatos.Location = new Point(20, 20);
            tlpDatos.Name = "tlpDatos";
            tlpDatos.Padding = new Padding(8);
            tlpDatos.RowCount = 9;
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDatos.Size = new Size(1031, 420);
            tlpDatos.TabIndex = 0;
            tlpDatos.SetColumnSpan(lblCantidadSembrada, 2);
            // 
            // Controles de datos de cosecha
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(62, 79, 98);
            label2.Margin = new Padding(8);
            label2.Name = "label2";
            label2.Text = "ID de cosecha:";
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(62, 79, 98);
            label3.Margin = new Padding(8);
            label3.Name = "label3";
            label3.Text = "Cultivo:";
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(62, 79, 98);
            label4.Margin = new Padding(8);
            label4.Name = "label4";
            label4.Text = "Fecha de cosecha:";
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(62, 79, 98);
            label5.Margin = new Padding(8);
            label5.Name = "label5";
            label5.Text = "Estado:";
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(62, 79, 98);
            label6.Margin = new Padding(8);
            label6.Name = "label6";
            label6.Text = "Cantidad cosechada:";
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(62, 79, 98);
            label7.Margin = new Padding(8);
            label7.Name = "label7";
            label7.Text = "Calidad (1-10):";
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(62, 79, 98);
            label8.Margin = new Padding(8);
            label8.Name = "label8";
            label8.Text = "Rendimiento (%):";
            label9.Anchor = AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(62, 79, 98);
            label9.Margin = new Padding(8);
            label9.Name = "label9";
            label9.Text = "Observaciones:";
            txtIDCosecha.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIDCosecha.BackColor = Color.FromArgb(240, 245, 249);
            txtIDCosecha.BorderStyle = BorderStyle.FixedSingle;
            txtIDCosecha.Font = new Font("Segoe UI", 10F);
            txtIDCosecha.Margin = new Padding(8);
            txtIDCosecha.Name = "txtIDCosecha";
            txtIDCosecha.ReadOnly = true;
            txtIDCosecha.TabIndex = 0;
            cbxCultivo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbxCultivo.BackColor = Color.FromArgb(240, 245, 249);
            cbxCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxCultivo.Font = new Font("Segoe UI", 10F);
            cbxCultivo.Margin = new Padding(8);
            cbxCultivo.Name = "cbxCultivo";
            cbxCultivo.TabIndex = 1;
            cbxCultivo.SelectedIndexChanged += cbxCultivo_SelectedIndexChanged;
            dtpFechaCosecha.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpFechaCosecha.Font = new Font("Segoe UI", 10F);
            dtpFechaCosecha.Format = DateTimePickerFormat.Short;
            dtpFechaCosecha.Margin = new Padding(8);
            dtpFechaCosecha.Name = "dtpFechaCosecha";
            dtpFechaCosecha.TabIndex = 2;
            cbxEstadoCosecha.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbxEstadoCosecha.BackColor = Color.FromArgb(240, 245, 249);
            cbxEstadoCosecha.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstadoCosecha.Font = new Font("Segoe UI", 10F);
            cbxEstadoCosecha.Items.AddRange(new object[] { "Completada", "Parcial", "En proceso" });
            cbxEstadoCosecha.Margin = new Padding(8);
            cbxEstadoCosecha.Name = "cbxEstadoCosecha";
            cbxEstadoCosecha.TabIndex = 3;
            numCantidadCosechada.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numCantidadCosechada.BackColor = Color.FromArgb(240, 245, 249);
            numCantidadCosechada.DecimalPlaces = 2;
            numCantidadCosechada.Font = new Font("Segoe UI", 10F);
            numCantidadCosechada.Margin = new Padding(8);
            numCantidadCosechada.Maximum = 100000;
            numCantidadCosechada.Name = "numCantidadCosechada";
            numCantidadCosechada.TabIndex = 4;
            numCantidadCosechada.ValueChanged += numCantidadCosechada_ValueChanged;
            lblCantidadSembrada.AutoSize = true;
            lblCantidadSembrada.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCantidadSembrada.ForeColor = Color.FromArgb(100, 100, 100);
            lblCantidadSembrada.Margin = new Padding(8);
            lblCantidadSembrada.Name = "lblCantidadSembrada";
            lblCantidadSembrada.Text = "Cantidad sembrada: 0";
            numCalidad.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numCalidad.BackColor = Color.FromArgb(240, 245, 249);
            numCalidad.DecimalPlaces = 1;
            numCalidad.Font = new Font("Segoe UI", 10F);
            numCalidad.Margin = new Padding(8);
            numCalidad.Maximum = 10;
            numCalidad.Name = "numCalidad";
            numCalidad.TabIndex = 5;
            numCalidad.Value = 8;
            numRendimiento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numRendimiento.BackColor = Color.FromArgb(240, 245, 249);
            numRendimiento.DecimalPlaces = 2;
            numRendimiento.Font = new Font("Segoe UI", 10F);
            numRendimiento.Margin = new Padding(8);
            numRendimiento.Maximum = 200;
            numRendimiento.Name = "numRendimiento";
            numRendimiento.TabIndex = 6;
            numRendimiento.Value = 100;
            txtObservaciones.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtObservaciones.BackColor = Color.FromArgb(240, 245, 249);
            txtObservaciones.BorderStyle = BorderStyle.FixedSingle;
            txtObservaciones.Font = new Font("Segoe UI", 10F);
            txtObservaciones.Margin = new Padding(8);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(760, 70);
            txtObservaciones.TabIndex = 7;
            // 
            // tabEstadisticas
            // 
            tabEstadisticas.BackColor = Color.FromArgb(250, 250, 250);
            tabEstadisticas.Controls.Add(pnlEstadisticas);
            tabEstadisticas.Location = new Point(4, 42);
            tabEstadisticas.Margin = new Padding(3, 4, 3, 4);
            tabEstadisticas.Name = "tabEstadisticas";
            tabEstadisticas.Padding = new Padding(15, 15, 15, 15);
            tabEstadisticas.Size = new Size(1101, 591);
            tabEstadisticas.TabIndex = 1;
            tabEstadisticas.Text = "Estadísticas";
            // 
            // pnlEstadisticas
            // 
            pnlEstadisticas.BackColor = Color.White;
            pnlEstadisticas.BorderStyle = BorderStyle.None;
            pnlEstadisticas.Controls.Add(chartRendimiento);
            pnlEstadisticas.Dock = DockStyle.Fill;
            pnlEstadisticas.Location = new Point(15, 15);
            pnlEstadisticas.Margin = new Padding(3, 4, 3, 4);
            pnlEstadisticas.Name = "pnlEstadisticas";
            pnlEstadisticas.Padding = new Padding(20, 20, 20, 20);
            pnlEstadisticas.Size = new Size(1071, 561);
            pnlEstadisticas.TabIndex = 0;
            // 
            // chartRendimiento
            // 
            chartRendimiento.BackColor = Color.FromArgb(250, 250, 250);
            chartArea1.Name = "ChartArea1";
            chartRendimiento.ChartAreas.Add(chartArea1);
            chartRendimiento.Dock = DockStyle.Fill;
            chartRendimiento.Location = new Point(20, 20);
            chartRendimiento.Name = "chartRendimiento";
            chartRendimiento.Size = new Size(1031, 521);
            chartRendimiento.TabIndex = 0;
            chartRendimiento.Text = "chartRendimiento";
            // 
            // tabHistorial
            // 
            tabHistorial.BackColor = Color.FromArgb(250, 250, 250);
            tabHistorial.Controls.Add(pnlHistorial);
            tabHistorial.Location = new Point(4, 42);
            tabHistorial.Margin = new Padding(3, 4, 3, 4);
            tabHistorial.Name = "tabHistorial";
            tabHistorial.Padding = new Padding(15, 15, 15, 15);
            tabHistorial.Size = new Size(1101, 591);
            tabHistorial.TabIndex = 2;
            tabHistorial.Text = "Historial";
            // 
            // pnlHistorial
            // 
            pnlHistorial.BackColor = Color.White;
            pnlHistorial.BorderStyle = BorderStyle.None;
            pnlHistorial.Controls.Add(dgvCosechas);
            pnlHistorial.Dock = DockStyle.Fill;
            pnlHistorial.Location = new Point(15, 15);
            pnlHistorial.Margin = new Padding(3, 4, 3, 4);
            pnlHistorial.Name = "pnlHistorial";
            pnlHistorial.Padding = new Padding(20, 20, 20, 20);
            pnlHistorial.Size = new Size(1071, 561);
            pnlHistorial.TabIndex = 0;
            // 
            // dgvCosechas
            // 
            dgvCosechas.AllowUserToAddRows = false;
            dgvCosechas.AllowUserToDeleteRows = false;
            dgvCosechas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCosechas.BackgroundColor = Color.White;
            dgvCosechas.BorderStyle = BorderStyle.None;
            dgvCosechas.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(62, 79, 98),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 5, 5, 5)
            };
            dgvCosechas.ColumnHeadersHeight = 40;
            dgvCosechas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCosechas.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                Font = new Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(52, 152, 219),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 5, 5)
            };
            dgvCosechas.Dock = DockStyle.Fill;
            dgvCosechas.EnableHeadersVisualStyles = false;
            dgvCosechas.Location = new Point(20, 20);
            dgvCosechas.Margin = new Padding(3, 4, 3, 4);
            dgvCosechas.Name = "dgvCosechas";
            dgvCosechas.ReadOnly = true;
            dgvCosechas.RowHeadersWidth = 51;
            dgvCosechas.RowTemplate.Height = 35;
            dgvCosechas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCosechas.Size = new Size(1031, 521);
            dgvCosechas.TabIndex = 0;
            dgvCosechas.CellClick += dgvCosechas_CellClick;
            // 
            // tabFinanzas
            // 
            tabFinanzas.BackColor = Color.FromArgb(250, 250, 250);
            tabFinanzas.Controls.Add(pnlFinanzas);
            tabFinanzas.Location = new Point(4, 42);
            tabFinanzas.Margin = new Padding(3, 4, 3, 4);
            tabFinanzas.Name = "tabFinanzas";
            tabFinanzas.Padding = new Padding(15, 15, 15, 15);
            tabFinanzas.Size = new Size(1101, 591);
            tabFinanzas.TabIndex = 3;
            tabFinanzas.Text = "Finanzas";
            // 
            // pnlFinanzas
            // 
            pnlFinanzas.AutoScroll = true;
            pnlFinanzas.BackColor = Color.White;
            pnlFinanzas.BorderStyle = BorderStyle.None;
            pnlFinanzas.Controls.Add(tlpFinanzas);
            pnlFinanzas.Dock = DockStyle.Fill;
            pnlFinanzas.Location = new Point(15, 15);
            pnlFinanzas.Margin = new Padding(3, 4, 3, 4);
            pnlFinanzas.Name = "pnlFinanzas";
            pnlFinanzas.Padding = new Padding(20, 20, 20, 20);
            pnlFinanzas.Size = new Size(1071, 561);
            pnlFinanzas.TabIndex = 0;
            // 
            // tlpFinanzas
            // 
            tlpFinanzas.AutoSize = true;
            tlpFinanzas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFinanzas.ColumnCount = 2;
            tlpFinanzas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            tlpFinanzas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFinanzas.Controls.Add(label10, 0, 0);
            tlpFinanzas.Controls.Add(numCostoCosecha, 1, 0);
            tlpFinanzas.Controls.Add(lblCostoSiembra, 0, 1);
            tlpFinanzas.Controls.Add(label12, 0, 2);
            tlpFinanzas.Controls.Add(numPrecioVenta, 1, 2);
            tlpFinanzas.Controls.Add(label13, 0, 3);
            tlpFinanzas.Controls.Add(lblBeneficio, 1, 3);
            tlpFinanzas.Dock = DockStyle.Top;
            tlpFinanzas.Location = new Point(20, 20);
            tlpFinanzas.Name = "tlpFinanzas";
            tlpFinanzas.Padding = new Padding(8);
            tlpFinanzas.RowCount = 4;
            tlpFinanzas.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFinanzas.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFinanzas.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFinanzas.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFinanzas.Size = new Size(1031, 210);
            tlpFinanzas.TabIndex = 0;
            tlpFinanzas.SetColumnSpan(lblCostoSiembra, 2);
            // 
            // Controles financieros
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(62, 79, 98);
            label10.Margin = new Padding(8);
            label10.Name = "label10";
            label10.Text = "Costo de cosecha ($):";
            numCostoCosecha.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numCostoCosecha.BackColor = Color.FromArgb(240, 245, 249);
            numCostoCosecha.DecimalPlaces = 2;
            numCostoCosecha.Font = new Font("Segoe UI", 10F);
            numCostoCosecha.Margin = new Padding(8);
            numCostoCosecha.Maximum = 1000000;
            numCostoCosecha.Name = "numCostoCosecha";
            numCostoCosecha.TabIndex = 0;
            numCostoCosecha.ValueChanged += numCostoCosecha_ValueChanged;
            lblCostoSiembra.AutoSize = true;
            lblCostoSiembra.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCostoSiembra.ForeColor = Color.FromArgb(100, 100, 100);
            lblCostoSiembra.Margin = new Padding(8);
            lblCostoSiembra.Name = "lblCostoSiembra";
            lblCostoSiembra.Text = "Costo de siembra ($): 0";
            label12.Anchor = AnchorStyles.Left;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label12.ForeColor = Color.FromArgb(62, 79, 98);
            label12.Margin = new Padding(8);
            label12.Name = "label12";
            label12.Text = "Precio de venta ($):";
            numPrecioVenta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numPrecioVenta.BackColor = Color.FromArgb(240, 245, 249);
            numPrecioVenta.DecimalPlaces = 2;
            numPrecioVenta.Font = new Font("Segoe UI", 10F);
            numPrecioVenta.Margin = new Padding(8);
            numPrecioVenta.Maximum = 1000000;
            numPrecioVenta.Name = "numPrecioVenta";
            numPrecioVenta.TabIndex = 1;
            numPrecioVenta.ValueChanged += numPrecioVenta_ValueChanged;
            label13.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label13.BackColor = Color.FromArgb(62, 79, 98);
            label13.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label13.ForeColor = Color.White;
            label13.Margin = new Padding(8);
            label13.Name = "label13";
            label13.Padding = new Padding(10, 0, 0, 0);
            label13.Size = new Size(174, 35);
            label13.Text = "Beneficio ($):";
            label13.TextAlign = ContentAlignment.MiddleLeft;
            lblBeneficio.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblBeneficio.BackColor = Color.FromArgb(240, 245, 249);
            lblBeneficio.BorderStyle = BorderStyle.FixedSingle;
            lblBeneficio.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBeneficio.Margin = new Padding(8);
            lblBeneficio.Name = "lblBeneficio";
            lblBeneficio.Padding = new Padding(0, 0, 10, 0);
            lblBeneficio.Size = new Size(790, 35);
            lblBeneficio.Text = "0.00";
            lblBeneficio.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(245, 247, 250);
            pnlBotones.BorderStyle = BorderStyle.None;
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnActualizar);
            pnlBotones.Controls.Add(btnEliminar);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Controls.Add(btnVolver);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 707);
            pnlBotones.Margin = new Padding(3, 4, 3, 4);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 15, 20, 15);
            pnlBotones.Size = new Size(1109, 80);
            pnlBotones.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 17);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 45);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "&Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(170, 17);
            btnActualizar.Margin = new Padding(3, 4, 3, 4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(140, 45);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "&Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(320, 17);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(140, 45);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "&Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(255, 193, 7);
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.Location = new Point(470, 17);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(140, 45);
            btnLimpiar.TabIndex = 3;
            btnLimpiar.Text = "&Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(149, 165, 166);
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(949, 17);
            btnVolver.Margin = new Padding(3, 4, 3, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(140, 45);
            btnVolver.TabIndex = 4;
            btnVolver.Text = "&Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmCosechas
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnVolver;
            ClientSize = new Size(1109, 650);
            Controls.Add(tabControl);
            Controls.Add(pnlBotones);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmCosechas";
            KeyPreview = true;
            Text = "Gestión de cosechas";
            Load += FrmCosechas_Load;
            pnlHeader.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabDatos.ResumeLayout(false);
            pnlDatos.ResumeLayout(false);
            pnlDatos.PerformLayout();
            tlpDatos.ResumeLayout(false);
            tlpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadCosechada).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCalidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRendimiento).EndInit();
            tabEstadisticas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartRendimiento).EndInit();
            tabHistorial.ResumeLayout(false);
            pnlHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCosechas).EndInit();
            tabFinanzas.ResumeLayout(false);
            pnlFinanzas.ResumeLayout(false);
            pnlFinanzas.PerformLayout();
            tlpFinanzas.ResumeLayout(false);
            tlpFinanzas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCostoCosecha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioVenta).EndInit();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Componentes principales
        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnCerrar;
        private TabControl tabControl;
        private TabPage tabDatos;
        private TabPage tabEstadisticas;
        private TabPage tabHistorial;
        private TabPage tabFinanzas;
        private Panel pnlDatos;
        private TableLayoutPanel tlpDatos;
        private Panel pnlEstadisticas;
        private Panel pnlHistorial;
        private Panel pnlFinanzas;
        private TableLayoutPanel tlpFinanzas;
        private Panel pnlBotones;

        // Componentes de Datos de Cosecha
        public Label label2;
        public TextBox txtIDCosecha;
        public Label label3;
        public ComboBox cbxCultivo;
        public Label label4;
        public DateTimePicker dtpFechaCosecha;
        public Label label5;
        public ComboBox cbxEstadoCosecha;
        public Label label6;
        public NumericUpDown numCantidadCosechada;
        public Label lblCantidadSembrada;
        public Label label7;
        public NumericUpDown numCalidad;
        public Label label8;
        public NumericUpDown numRendimiento;
        public Label label9;
        public TextBox txtObservaciones;

        // Componentes de Finanzas
        public Label label10;
        public NumericUpDown numCostoCosecha;
        public Label lblCostoSiembra;
        public Label label12;
        public NumericUpDown numPrecioVenta;
        public Label label13;
        public Label lblBeneficio;

        // Componentes especiales
        public System.Windows.Forms.DataVisualization.Charting.Chart chartRendimiento;
        public DataGridView dgvCosechas;

        // Botones principales
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnVolver;
    }
}

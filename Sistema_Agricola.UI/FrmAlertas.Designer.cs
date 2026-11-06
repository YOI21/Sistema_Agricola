namespace Sistema_Agricola.UI
{
    partial class FrmAlertas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControlAlertas = new System.Windows.Forms.TabControl();
            this.tabPageTodasAlertas = new System.Windows.Forms.TabPage();
            this.dgvTodasAlertas = new System.Windows.Forms.DataGridView();
            this.tabPageAlertasPendientes = new System.Windows.Forms.TabPage();
            this.dgvAlertasPendientes = new System.Windows.Forms.DataGridView();
            this.tabPageAlertasProximas = new System.Windows.Forms.TabPage();
            this.dgvAlertasProximas = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFechaCreacion = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numDiasAnticipacion = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPrioridad = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTipoAlerta = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaProgramada = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCultivo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdAlerta = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnGenerarAlertas = new System.Windows.Forms.Button();
            this.btnVerificarAlertas = new System.Windows.Forms.Button();
            this.timerAlertas = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlAlertas.SuspendLayout();
            this.tabPageTodasAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodasAlertas)).BeginInit();
            this.tabPageAlertasPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertasPendientes)).BeginInit();
            this.tabPageAlertasProximas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertasProximas)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasAnticipacion)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1333, 60);
            this.panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(1275, 15);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(45, 30);
            this.btnVolver.TabIndex = 8;
            this.btnVolver.Text = "✕";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alertas y Notificaciones";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panel2.Controls.Add(this.tabControlAlertas);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnLimpiar);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.btnActualizar);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Controls.Add(this.btnGenerarAlertas);
            this.panel2.Controls.Add(this.btnVerificarAlertas);
            this.panel2.Location = new System.Drawing.Point(13, 70);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(1307, 867);
            this.panel2.TabIndex = 1;
            // 
            // tabControlAlertas
            // 
            this.tabControlAlertas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAlertas.Controls.Add(this.tabPageTodasAlertas);
            this.tabControlAlertas.Controls.Add(this.tabPageAlertasPendientes);
            this.tabControlAlertas.Controls.Add(this.tabPageAlertasProximas);
            this.tabControlAlertas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlAlertas.Location = new System.Drawing.Point(17, 470);
            this.tabControlAlertas.Name = "tabControlAlertas";
            this.tabControlAlertas.SelectedIndex = 0;
            this.tabControlAlertas.Size = new System.Drawing.Size(1273, 355);
            this.tabControlAlertas.TabIndex = 14;
            this.tabControlAlertas.SelectedIndexChanged += new System.EventHandler(this.tabControlAlertas_SelectedIndexChanged);
            // 
            // tabPageTodasAlertas
            // 
            this.tabPageTodasAlertas.Controls.Add(this.dgvTodasAlertas);
            this.tabPageTodasAlertas.Location = new System.Drawing.Point(4, 37);
            this.tabPageTodasAlertas.Name = "tabPageTodasAlertas";
            this.tabPageTodasAlertas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTodasAlertas.Size = new System.Drawing.Size(1265, 314);
            this.tabPageTodasAlertas.TabIndex = 0;
            this.tabPageTodasAlertas.Text = "Todas las Alertas";
            this.tabPageTodasAlertas.UseVisualStyleBackColor = true;
            // 
            // dgvTodasAlertas
            // 
            this.dgvTodasAlertas.AllowUserToAddRows = false;
            this.dgvTodasAlertas.AllowUserToDeleteRows = false;
            this.dgvTodasAlertas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTodasAlertas.BackgroundColor = System.Drawing.Color.White;
            this.dgvTodasAlertas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTodasAlertas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTodasAlertas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTodasAlertas.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.True
            };
            this.dgvTodasAlertas.ColumnHeadersHeight = 40;
            this.dgvTodasAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTodasAlertas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvTodasAlertas.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9.75F),
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvTodasAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTodasAlertas.EnableHeadersVisualStyles = false;
            this.dgvTodasAlertas.GridColor = System.Drawing.Color.FromArgb(229, 229, 229);
            this.dgvTodasAlertas.Location = new System.Drawing.Point(3, 3);
            this.dgvTodasAlertas.Name = "dgvTodasAlertas";
            this.dgvTodasAlertas.ReadOnly = true;
            this.dgvTodasAlertas.RowHeadersVisible = false;
            this.dgvTodasAlertas.RowHeadersWidth = 62;
            this.dgvTodasAlertas.RowTemplate.Height = 35;
            this.dgvTodasAlertas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTodasAlertas.Size = new System.Drawing.Size(1259, 308);
            this.dgvTodasAlertas.TabIndex = 0;
            this.dgvTodasAlertas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTodasAlertas_CellClick);
            // 
            // tabPageAlertasPendientes
            // 
            this.tabPageAlertasPendientes.Controls.Add(this.dgvAlertasPendientes);
            this.tabPageAlertasPendientes.Location = new System.Drawing.Point(4, 37);
            this.tabPageAlertasPendientes.Name = "tabPageAlertasPendientes";
            this.tabPageAlertasPendientes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAlertasPendientes.Size = new System.Drawing.Size(1265, 314);
            this.tabPageAlertasPendientes.TabIndex = 1;
            this.tabPageAlertasPendientes.Text = "Alertas Pendientes";
            this.tabPageAlertasPendientes.UseVisualStyleBackColor = true;
            // 
            // dgvAlertasPendientes
            // 
            this.dgvAlertasPendientes.AllowUserToAddRows = false;
            this.dgvAlertasPendientes.AllowUserToDeleteRows = false;
            this.dgvAlertasPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlertasPendientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlertasPendientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlertasPendientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAlertasPendientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAlertasPendientes.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.True
            };
            this.dgvAlertasPendientes.ColumnHeadersHeight = 40;
            this.dgvAlertasPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAlertasPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvAlertasPendientes.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9.75F),
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvAlertasPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertasPendientes.EnableHeadersVisualStyles = false;
            this.dgvAlertasPendientes.GridColor = System.Drawing.Color.FromArgb(229, 229, 229);
            this.dgvAlertasPendientes.Location = new System.Drawing.Point(3, 3);
            this.dgvAlertasPendientes.Name = "dgvAlertasPendientes";
            this.dgvAlertasPendientes.ReadOnly = true;
            this.dgvAlertasPendientes.RowHeadersVisible = false;
            this.dgvAlertasPendientes.RowHeadersWidth = 62;
            this.dgvAlertasPendientes.RowTemplate.Height = 35;
            this.dgvAlertasPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlertasPendientes.Size = new System.Drawing.Size(1259, 308);
            this.dgvAlertasPendientes.TabIndex = 1;
            this.dgvAlertasPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlertasPendientes_CellClick);
            // 
            // tabPageAlertasProximas
            // 
            this.tabPageAlertasProximas.Controls.Add(this.dgvAlertasProximas);
            this.tabPageAlertasProximas.Location = new System.Drawing.Point(4, 37);
            this.tabPageAlertasProximas.Name = "tabPageAlertasProximas";
            this.tabPageAlertasProximas.Size = new System.Drawing.Size(1265, 314);
            this.tabPageAlertasProximas.TabIndex = 2;
            this.tabPageAlertasProximas.Text = "Alertas Próximas";
            this.tabPageAlertasProximas.UseVisualStyleBackColor = true;
            // 
            // dgvAlertasProximas
            // 
            this.dgvAlertasProximas.AllowUserToAddRows = false;
            this.dgvAlertasProximas.AllowUserToDeleteRows = false;
            this.dgvAlertasProximas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlertasProximas.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlertasProximas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlertasProximas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAlertasProximas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAlertasProximas.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.True
            };
            this.dgvAlertasProximas.ColumnHeadersHeight = 40;
            this.dgvAlertasProximas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAlertasProximas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvAlertasProximas.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9.75F),
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvAlertasProximas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertasProximas.EnableHeadersVisualStyles = false;
            this.dgvAlertasProximas.GridColor = System.Drawing.Color.FromArgb(229, 229, 229);
            this.dgvAlertasProximas.Location = new System.Drawing.Point(0, 0);
            this.dgvAlertasProximas.Name = "dgvAlertasProximas";
            this.dgvAlertasProximas.ReadOnly = true;
            this.dgvAlertasProximas.RowHeadersVisible = false;
            this.dgvAlertasProximas.RowHeadersWidth = 62;
            this.dgvAlertasProximas.RowTemplate.Height = 35;
            this.dgvAlertasProximas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlertasProximas.Size = new System.Drawing.Size(1265, 314);
            this.dgvAlertasProximas.TabIndex = 1;
            this.dgvAlertasProximas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlertasProximas_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFechaCreacion);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.numDiasAnticipacion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbPrioridad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbEstado);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbTipoAlerta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpFechaProgramada);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTitulo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCultivo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIdAlerta);
            this.groupBox1.Controls.Add(this.lblId);
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox1.Location = new System.Drawing.Point(17, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.groupBox1.Size = new System.Drawing.Size(986, 355);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "📋 Datos de Alerta";
            // 
            // lblFechaCreacion
            // 
            this.lblFechaCreacion.AutoSize = true;
            this.lblFechaCreacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFechaCreacion.Location = new System.Drawing.Point(662, 54);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(113, 28);
            this.lblFechaCreacion.TabIndex = 19;
            this.lblFechaCreacion.Text = "00/00/0000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label10.Location = new System.Drawing.Point(509, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(147, 28);
            this.label10.TabIndex = 18;
            this.label10.Text = "Fecha Creación:";
            // 
            // numDiasAnticipacion
            // 
            this.numDiasAnticipacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numDiasAnticipacion.Location = new System.Drawing.Point(698, 295);
            this.numDiasAnticipacion.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numDiasAnticipacion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDiasAnticipacion.Name = "numDiasAnticipacion";
            this.numDiasAnticipacion.Size = new System.Drawing.Size(77, 34);
            this.numDiasAnticipacion.TabIndex = 17;
            this.numDiasAnticipacion.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(509, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 28);
            this.label9.TabIndex = 16;
            this.label9.Text = "Días de Anticipación:";
            // 
            // cmbPrioridad
            // 
            this.cmbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPrioridad.FormattingEnabled = true;
            this.cmbPrioridad.Items.AddRange(new object[] {
            "Alta",
            "Media",
            "Baja"});
            this.cmbPrioridad.Location = new System.Drawing.Point(582, 245);
            this.cmbPrioridad.Name = "cmbPrioridad";
            this.cmbPrioridad.Size = new System.Drawing.Size(193, 36);
            this.cmbPrioridad.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(509, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 28);
            this.label8.TabIndex = 14;
            this.label8.Text = "Prioridad:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Completada",
            "Cancelada"});
            this.cmbEstado.Location = new System.Drawing.Point(582, 195);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(193, 36);
            this.cmbEstado.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(509, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 28);
            this.label7.TabIndex = 12;
            this.label7.Text = "Estado:";
            // 
            // cmbTipoAlerta
            // 
            this.cmbTipoAlerta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoAlerta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoAlerta.FormattingEnabled = true;
            this.cmbTipoAlerta.Items.AddRange(new object[] {
            "Cosecha",
            "Fertilización",
            "Riego",
            "Control de Plagas",
            "Mantenimiento",
            "Otro"});
            this.cmbTipoAlerta.Location = new System.Drawing.Point(582, 145);
            this.cmbTipoAlerta.Name = "cmbTipoAlerta";
            this.cmbTipoAlerta.Size = new System.Drawing.Size(193, 36);
            this.cmbTipoAlerta.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(509, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 28);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tipo:";
            // 
            // dtpFechaProgramada
            // 
            this.dtpFechaProgramada.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaProgramada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaProgramada.Location = new System.Drawing.Point(189, 295);
            this.dtpFechaProgramada.Name = "dtpFechaProgramada";
            this.dtpFechaProgramada.Size = new System.Drawing.Size(143, 34);
            this.dtpFechaProgramada.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(22, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "Fecha Programada:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new System.Drawing.Point(118, 195);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(366, 86);
            this.txtDescripcion.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(22, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "Descripción:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitulo.Location = new System.Drawing.Point(118, 145);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(366, 34);
            this.txtTitulo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(22, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "Título:";
            // 
            // cmbCultivo
            // 
            this.cmbCultivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCultivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCultivo.FormattingEnabled = true;
            this.cmbCultivo.Location = new System.Drawing.Point(118, 95);
            this.cmbCultivo.Name = "cmbCultivo";
            this.cmbCultivo.Size = new System.Drawing.Size(366, 36);
            this.cmbCultivo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(22, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cultivo:";
            // 
            // txtIdAlerta
            // 
            this.txtIdAlerta.Enabled = false;
            this.txtIdAlerta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIdAlerta.Location = new System.Drawing.Point(118, 45);
            this.txtIdAlerta.Name = "txtIdAlerta";
            this.txtIdAlerta.ReadOnly = true;
            this.txtIdAlerta.Size = new System.Drawing.Size(116, 34);
            this.txtIdAlerta.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblId.Location = new System.Drawing.Point(22, 45);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(34, 28);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "ID:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Snow;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Image = global::Sistema_Agricola.UI.Properties.Resources.borrador;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(1157, 338);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(133, 42);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(1157, 250);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(140, 42);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(1157, 194);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(140, 42);
            this.btnActualizar.TabIndex = 8;
            this.btnActualizar.Text = "🔄 Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(1157, 138);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 42);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnGenerarAlertas
            // 
            this.btnGenerarAlertas.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnGenerarAlertas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarAlertas.FlatAppearance.BorderSize = 0;
            this.btnGenerarAlertas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarAlertas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerarAlertas.ForeColor = System.Drawing.Color.White;
            this.btnGenerarAlertas.Location = new System.Drawing.Point(1157, 70);
            this.btnGenerarAlertas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerarAlertas.Name = "btnGenerarAlertas";
            this.btnGenerarAlertas.Size = new System.Drawing.Size(140, 42);
            this.btnGenerarAlertas.TabIndex = 13;
            this.btnGenerarAlertas.Text = "⚡ Generar Automáticas";
            this.btnGenerarAlertas.UseVisualStyleBackColor = false;
            this.btnGenerarAlertas.Click += new System.EventHandler(this.btnGenerarAlertas_Click);
            // 
            // btnVerificarAlertas
            // 
            this.btnVerificarAlertas.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnVerificarAlertas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificarAlertas.FlatAppearance.BorderSize = 0;
            this.btnVerificarAlertas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificarAlertas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVerificarAlertas.ForeColor = System.Drawing.Color.White;
            this.btnVerificarAlertas.Location = new System.Drawing.Point(1157, 25);
            this.btnVerificarAlertas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnVerificarAlertas.Name = "btnVerificarAlertas";
            this.btnVerificarAlertas.Size = new System.Drawing.Size(140, 42);
            this.btnVerificarAlertas.TabIndex = 12;
            this.btnVerificarAlertas.Text = "🔔 Verificar";
            this.btnVerificarAlertas.UseVisualStyleBackColor = false;
            this.btnVerificarAlertas.Click += new System.EventHandler(this.btnVerificarAlertas_Click);
            // 
            // timerAlertas
            // 
            this.timerAlertas.Interval = 60000;
            this.timerAlertas.Tick += new System.EventHandler(this.timerAlertas_Tick);
            // 
            // FrmAlertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 245);
            this.ClientSize = new System.Drawing.Size(1333, 952);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAlertas";
            this.Text = "Alertas y Notificaciones";
            this.Load += new System.EventHandler(this.FrmAlertas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControlAlertas.ResumeLayout(false);
            this.tabPageTodasAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodasAlertas)).EndInit();
            this.tabPageAlertasPendientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertasPendientes)).EndInit();
            this.tabPageAlertasProximas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertasProximas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasAnticipacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFechaCreacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numDiasAnticipacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPrioridad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTipoAlerta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaProgramada;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCultivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdAlerta;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVerificarAlertas;
        private System.Windows.Forms.Button btnGenerarAlertas;
        private System.Windows.Forms.TabControl tabControlAlertas;
        private System.Windows.Forms.TabPage tabPageTodasAlertas;
        private System.Windows.Forms.DataGridView dgvTodasAlertas;
        private System.Windows.Forms.TabPage tabPageAlertasPendientes;
        private System.Windows.Forms.DataGridView dgvAlertasPendientes;
        private System.Windows.Forms.TabPage tabPageAlertasProximas;
        private System.Windows.Forms.DataGridView dgvAlertasProximas;
        private System.Windows.Forms.Timer timerAlertas;
    }
}
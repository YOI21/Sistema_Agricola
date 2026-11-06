namespace Sistema_Agricola.UI
{
    partial class FrmRegistroTerreno
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
            panel1 = new Panel();
            btnVolver = new Button();
            label1 = new Label();
            panel2 = new Panel();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnActualizar = new Button();
            btnGuardar = new Button();
            dgvTerrenos = new DataGridView();
            groupBox2 = new GroupBox();
            chkDisponibilidadRiego = new CheckBox();
            cmbFuenteAgua = new ComboBox();
            label17 = new Label();
            cmbDrenaje = new ComboBox();
            label16 = new Label();
            numPrecipitacionAnual = new NumericUpDown();
            label15 = new Label();
            cmbZonaAgroclimatica = new ComboBox();
            label14 = new Label();
            cmbProvincia = new ComboBox();
            label13 = new Label();
            btnVerRecomendaciones = new Button();
            btnComprobarCompatibilidad = new Button();
            groupBox1 = new GroupBox();
            lblAreaEquivalente = new Label();
            btnGenerarID = new Button();
            txtObservaciones = new TextBox();
            label12 = new Label();
            cmbEstadoActual = new ComboBox();
            label11 = new Label();
            txtPropietario = new TextBox();
            label10 = new Label();
            txtUbicacion = new TextBox();
            label9 = new Label();
            numHumedad = new NumericUpDown();
            label8 = new Label();
            numPendiente = new NumericUpDown();
            label7 = new Label();
            numPH = new NumericUpDown();
            label6 = new Label();
            cmbTipoSuelo = new ComboBox();
            label5 = new Label();
            cmbUnidadMedida = new ComboBox();
            label4 = new Label();
            numArea = new NumericUpDown();
            label3 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtIDTerreno = new TextBox();
            lblID = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTerrenos).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrecipitacionAnual).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numHumedad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPendiente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numArea).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1333, 70);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            btnVolver.Location = new Point(1282, 15);
            btnVolver.Margin = new Padding(4, 6, 4, 6);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(40, 40);
            btnVolver.TabIndex = 8;
            btnVolver.Text = "X";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(317, 48);
            label1.TabIndex = 0;
            label1.Text = "Registro de Terrenos";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.White;
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnActualizar);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(dgvTerrenos);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Location = new Point(13, 82);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1307, 855);
            panel2.TabIndex = 1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLimpiar.Location = new Point(794, 463);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(133, 42);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnEliminar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnEliminar.Location = new Point(623, 463);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(133, 42);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            btnActualizar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnActualizar.Location = new Point(429, 462);
            btnActualizar.Margin = new Padding(3, 4, 3, 4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(159, 42);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGuardar.Location = new Point(245, 463);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(156, 42);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvTerrenos
            // 
            dgvTerrenos.AllowUserToAddRows = false;
            dgvTerrenos.AllowUserToDeleteRows = false;
            dgvTerrenos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTerrenos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTerrenos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTerrenos.Location = new Point(17, 512);
            dgvTerrenos.Margin = new Padding(3, 4, 3, 4);
            dgvTerrenos.Name = "dgvTerrenos";
            dgvTerrenos.ReadOnly = true;
            dgvTerrenos.RowHeadersWidth = 62;
            dgvTerrenos.RowTemplate.Height = 28;
            dgvTerrenos.Size = new Size(1273, 312);
            dgvTerrenos.TabIndex = 2;
            dgvTerrenos.CellClick += dgvTerrenos_CellClick;
            dgvTerrenos.CellContentClick += dgvTerrenos_CellContentClick;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(chkDisponibilidadRiego);
            groupBox2.Controls.Add(cmbFuenteAgua);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(cmbDrenaje);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(numPrecipitacionAnual);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(cmbZonaAgroclimatica);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(cmbProvincia);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(btnVerRecomendaciones);
            groupBox2.Controls.Add(btnComprobarCompatibilidad);
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            groupBox2.Location = new Point(847, 25);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(443, 430);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Condiciones agroclimáticas";
            // 
            // chkDisponibilidadRiego
            // 
            chkDisponibilidadRiego.AutoSize = true;
            chkDisponibilidadRiego.Font = new Font("Segoe UI", 9F);
            chkDisponibilidadRiego.Location = new Point(174, 286);
            chkDisponibilidadRiego.Name = "chkDisponibilidadRiego";
            chkDisponibilidadRiego.Size = new Size(210, 29);
            chkDisponibilidadRiego.TabIndex = 12;
            chkDisponibilidadRiego.Text = "Riego disponible";
            chkDisponibilidadRiego.UseVisualStyleBackColor = true;
            // 
            // cmbFuenteAgua
            // 
            cmbFuenteAgua.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFuenteAgua.Font = new Font("Segoe UI", 9F);
            cmbFuenteAgua.FormattingEnabled = true;
            cmbFuenteAgua.Location = new Point(174, 243);
            cmbFuenteAgua.Name = "cmbFuenteAgua";
            cmbFuenteAgua.Size = new Size(244, 33);
            cmbFuenteAgua.TabIndex = 11;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F);
            label17.Location = new Point(18, 247);
            label17.Name = "label17";
            label17.Size = new Size(143, 25);
            label17.TabIndex = 10;
            label17.Text = "Fuente de agua:";
            // 
            // cmbDrenaje
            // 
            cmbDrenaje.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDrenaje.Font = new Font("Segoe UI", 9F);
            cmbDrenaje.FormattingEnabled = true;
            cmbDrenaje.Location = new Point(174, 201);
            cmbDrenaje.Name = "cmbDrenaje";
            cmbDrenaje.Size = new Size(244, 33);
            cmbDrenaje.TabIndex = 9;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F);
            label16.Location = new Point(18, 205);
            label16.Name = "label16";
            label16.Size = new Size(78, 25);
            label16.TabIndex = 8;
            label16.Text = "Drenaje:";
            // 
            // numPrecipitacionAnual
            // 
            numPrecipitacionAnual.DecimalPlaces = 0;
            numPrecipitacionAnual.Font = new Font("Segoe UI", 9F);
            numPrecipitacionAnual.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numPrecipitacionAnual.Location = new Point(174, 159);
            numPrecipitacionAnual.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numPrecipitacionAnual.Name = "numPrecipitacionAnual";
            numPrecipitacionAnual.Size = new Size(126, 31);
            numPrecipitacionAnual.TabIndex = 7;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(18, 163);
            label15.Name = "label15";
            label15.Size = new Size(137, 25);
            label15.TabIndex = 6;
            label15.Text = "Lluvia anual (mm):";
            // 
            // cmbZonaAgroclimatica
            // 
            cmbZonaAgroclimatica.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZonaAgroclimatica.Font = new Font("Segoe UI", 9F);
            cmbZonaAgroclimatica.FormattingEnabled = true;
            cmbZonaAgroclimatica.Location = new Point(174, 117);
            cmbZonaAgroclimatica.Name = "cmbZonaAgroclimatica";
            cmbZonaAgroclimatica.Size = new Size(244, 33);
            cmbZonaAgroclimatica.TabIndex = 5;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(18, 121);
            label14.Name = "label14";
            label14.Size = new Size(51, 25);
            label14.TabIndex = 4;
            label14.Text = "Zona:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProvincia.Font = new Font("Segoe UI", 9F);
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new Point(174, 75);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(244, 33);
            cmbProvincia.TabIndex = 3;
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(18, 79);
            label13.Name = "label13";
            label13.Size = new Size(89, 25);
            label13.TabIndex = 2;
            label13.Text = "Provincia:";
            // 
            // btnVerRecomendaciones
            // 
            btnVerRecomendaciones.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            btnVerRecomendaciones.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnVerRecomendaciones.ForeColor = Color.White;
            btnVerRecomendaciones.FlatStyle = FlatStyle.Flat;
            btnVerRecomendaciones.FlatAppearance.BorderSize = 0;
            btnVerRecomendaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            btnVerRecomendaciones.Location = new Point(225, 348);
            btnVerRecomendaciones.Margin = new Padding(3, 4, 3, 4);
            btnVerRecomendaciones.Name = "btnVerRecomendaciones";
            btnVerRecomendaciones.Size = new Size(193, 54);
            btnVerRecomendaciones.TabIndex = 1;
            btnVerRecomendaciones.Text = "Ver recomendaciones";
            btnVerRecomendaciones.UseVisualStyleBackColor = false;
            btnVerRecomendaciones.Click += btnVerRecomendaciones_Click;
            // 
            // btnComprobarCompatibilidad
            // 
            btnComprobarCompatibilidad.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            btnComprobarCompatibilidad.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnComprobarCompatibilidad.ForeColor = Color.White;
            btnComprobarCompatibilidad.FlatStyle = FlatStyle.Flat;
            btnComprobarCompatibilidad.FlatAppearance.BorderSize = 0;
            btnComprobarCompatibilidad.Cursor = System.Windows.Forms.Cursors.Hand;
            btnComprobarCompatibilidad.Location = new Point(18, 348);
            btnComprobarCompatibilidad.Margin = new Padding(3, 4, 3, 4);
            btnComprobarCompatibilidad.Name = "btnComprobarCompatibilidad";
            btnComprobarCompatibilidad.Size = new Size(193, 54);
            btnComprobarCompatibilidad.TabIndex = 0;
            btnComprobarCompatibilidad.Text = "Evaluar cultivo";
            btnComprobarCompatibilidad.UseVisualStyleBackColor = false;
            btnComprobarCompatibilidad.Click += btnComprobarCompatibilidad_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblAreaEquivalente);
            groupBox1.Controls.Add(btnGenerarID);
            groupBox1.Controls.Add(txtObservaciones);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(cmbEstadoActual);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(txtPropietario);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(txtUbicacion);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(numHumedad);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(numPendiente);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(numPH);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmbTipoSuelo);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cmbUnidadMedida);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(numArea);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtIDTerreno);
            groupBox1.Controls.Add(lblID);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            groupBox1.Location = new Point(17, 25);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(824, 430);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Terreno";
            // 
            // lblAreaEquivalente
            // 
            lblAreaEquivalente.AutoSize = true;
            lblAreaEquivalente.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAreaEquivalente.ForeColor = Color.DarkGreen;
            lblAreaEquivalente.Location = new Point(473, 155);
            lblAreaEquivalente.Name = "lblAreaEquivalente";
            lblAreaEquivalente.Size = new Size(0, 25);
            lblAreaEquivalente.TabIndex = 25;
            // 
            // btnGenerarID
            // 
            btnGenerarID.BackColor = Color.Transparent;
            btnGenerarID.BackgroundImageLayout = ImageLayout.Zoom;
            btnGenerarID.Cursor = Cursors.Hand;
            btnGenerarID.FlatStyle = FlatStyle.Flat;
            btnGenerarID.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnGenerarID.Location = new Point(305, 49);
            btnGenerarID.Name = "btnGenerarID";
            btnGenerarID.Size = new Size(40, 32);
            btnGenerarID.TabIndex = 24;
            btnGenerarID.Text = "⟳";
            btnGenerarID.UseVisualStyleBackColor = false;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Font = new Font("Segoe UI", 10F);
            txtObservaciones.Location = new Point(183, 354);
            txtObservaciones.Margin = new Padding(3, 4, 3, 4);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(483, 68);
            txtObservaciones.TabIndex = 23;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F);
            label12.Location = new Point(22, 354);
            label12.Name = "label12";
            label12.Size = new Size(143, 28);
            label12.TabIndex = 22;
            label12.Text = "Observaciones:";
            // 
            // cmbEstadoActual
            // 
            cmbEstadoActual.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoActual.Font = new Font("Segoe UI", 10F);
            cmbEstadoActual.FormattingEnabled = true;
            cmbEstadoActual.Items.AddRange(new object[] { "Disponible", "En uso", "En descanso" });
            cmbEstadoActual.Location = new Point(183, 310);
            cmbEstadoActual.Margin = new Padding(3, 4, 3, 4);
            cmbEstadoActual.Name = "cmbEstadoActual";
            cmbEstadoActual.Size = new Size(183, 36);
            cmbEstadoActual.TabIndex = 21;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F);
            label11.Location = new Point(22, 313);
            label11.Name = "label11";
            label11.Size = new Size(135, 28);
            label11.TabIndex = 20;
            label11.Text = "Estado Actual:";
            // 
            // txtPropietario
            // 
            txtPropietario.Font = new Font("Segoe UI", 10F);
            txtPropietario.Location = new Point(466, 267);
            txtPropietario.Margin = new Padding(3, 4, 3, 4);
            txtPropietario.Name = "txtPropietario";
            txtPropietario.Size = new Size(200, 34);
            txtPropietario.TabIndex = 19;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(347, 271);
            label10.Name = "label10";
            label10.Size = new Size(114, 28);
            label10.TabIndex = 18;
            label10.Text = "Propietario:";
            // 
            // txtUbicacion
            // 
            txtUbicacion.Font = new Font("Segoe UI", 10F);
            txtUbicacion.Location = new Point(140, 267);
            txtUbicacion.Margin = new Padding(3, 4, 3, 4);
            txtUbicacion.Name = "txtUbicacion";
            txtUbicacion.Size = new Size(183, 34);
            txtUbicacion.TabIndex = 17;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(22, 271);
            label9.Name = "label9";
            label9.Size = new Size(103, 28);
            label9.TabIndex = 16;
            label9.Text = "Ubicación:";
            // 
            // numHumedad
            // 
            numHumedad.DecimalPlaces = 1;
            numHumedad.Font = new Font("Segoe UI", 10F);
            numHumedad.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numHumedad.Location = new Point(483, 224);
            numHumedad.Margin = new Padding(3, 4, 3, 4);
            numHumedad.Name = "numHumedad";
            numHumedad.Size = new Size(87, 34);
            numHumedad.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(347, 226);
            label8.Name = "label8";
            label8.Size = new Size(135, 28);
            label8.TabIndex = 14;
            label8.Text = "Humedad (%):";
            // 
            // numPendiente
            // 
            numPendiente.DecimalPlaces = 1;
            numPendiente.Font = new Font("Segoe UI", 10F);
            numPendiente.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numPendiente.Location = new Point(244, 224);
            numPendiente.Margin = new Padding(3, 4, 3, 4);
            numPendiente.Name = "numPendiente";
            numPendiente.Size = new Size(87, 34);
            numPendiente.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(22, 226);
            label7.Name = "label7";
            label7.Size = new Size(140, 28);
            label7.TabIndex = 12;
            label7.Text = "Pendiente (%): ";
            // 
            // numPH
            // 
            numPH.DecimalPlaces = 1;
            numPH.Font = new Font("Segoe UI", 10F);
            numPH.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numPH.Location = new Point(483, 183);
            numPH.Margin = new Padding(3, 4, 3, 4);
            numPH.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
            numPH.Name = "numPH";
            numPH.Size = new Size(87, 34);
            numPH.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(411, 185);
            label6.Name = "label6";
            label6.Size = new Size(42, 28);
            label6.TabIndex = 10;
            label6.Text = "pH:";
            // 
            // cmbTipoSuelo
            // 
            cmbTipoSuelo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoSuelo.Font = new Font("Segoe UI", 10F);
            cmbTipoSuelo.FormattingEnabled = true;
            cmbTipoSuelo.Items.AddRange(new object[] { "Arcilloso", "Arenoso", "Franco", "Limoso" });
            cmbTipoSuelo.Location = new Point(140, 183);
            cmbTipoSuelo.Margin = new Padding(3, 4, 3, 4);
            cmbTipoSuelo.Name = "cmbTipoSuelo";
            cmbTipoSuelo.Size = new Size(183, 36);
            cmbTipoSuelo.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(22, 185);
            label5.Name = "label5";
            label5.Size = new Size(109, 28);
            label5.TabIndex = 8;
            label5.Text = "Tipo Suelo:";
            // 
            // cmbUnidadMedida
            // 
            cmbUnidadMedida.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnidadMedida.Font = new Font("Segoe UI", 10F);
            cmbUnidadMedida.FormattingEnabled = true;
            cmbUnidadMedida.Items.AddRange(new object[] { "Metros Cuadrados", "Hectáreas", "Tareas" });
            cmbUnidadMedida.Location = new Point(244, 140);
            cmbUnidadMedida.Margin = new Padding(3, 4, 3, 4);
            cmbUnidadMedida.Name = "cmbUnidadMedida";
            cmbUnidadMedida.Size = new Size(183, 36);
            cmbUnidadMedida.TabIndex = 7;
            cmbUnidadMedida.SelectedIndexChanged += cmbUnidadMedida_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(196, 140);
            label4.Name = "label4";
            label4.Size = new Size(42, 28);
            label4.TabIndex = 6;
            label4.Text = "en: ";
            // 
            // numArea
            // 
            numArea.DecimalPlaces = 2;
            numArea.Font = new Font("Segoe UI", 10F);
            numArea.Location = new Point(86, 140);
            numArea.Margin = new Padding(3, 4, 3, 4);
            numArea.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numArea.Name = "numArea";
            numArea.Size = new Size(100, 34);
            numArea.TabIndex = 5;
            numArea.ValueChanged += numArea_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(22, 140);
            label3.Name = "label3";
            label3.Size = new Size(56, 28);
            label3.TabIndex = 4;
            label3.Text = "Área:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(140, 97);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(287, 34);
            txtNombre.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(22, 97);
            label2.Name = "label2";
            label2.Size = new Size(89, 28);
            label2.TabIndex = 2;
            label2.Text = "Nombre:";
            // 
            // txtIDTerreno
            // 
            txtIDTerreno.Enabled = false;
            txtIDTerreno.Font = new Font("Segoe UI", 10F);
            txtIDTerreno.Location = new Point(140, 51);
            txtIDTerreno.Margin = new Padding(3, 4, 3, 4);
            txtIDTerreno.Name = "txtIDTerreno";
            txtIDTerreno.ReadOnly = true;
            txtIDTerreno.Size = new Size(159, 34);
            txtIDTerreno.TabIndex = 1;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Segoe UI", 10F);
            lblID.Location = new Point(22, 51);
            lblID.Name = "lblID";
            lblID.Size = new Size(35, 28);
            lblID.TabIndex = 0;
            lblID.Text = "ID:";
            // 
            // FrmRegistroTerreno
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 952);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmRegistroTerreno";
            Text = "Registro de Terrenos";
            Load += FrmRegistroTerreno_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTerrenos).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrecipitacionAnual).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numHumedad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPendiente).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPH).EndInit();
            ((System.ComponentModel.ISupportInitialize)numArea).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvTerrenos;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtIDTerreno;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbUnidadMedida;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoSuelo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numPH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numPendiente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numHumedad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPropietario;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbEstadoActual;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGenerarID;
        private System.Windows.Forms.Button btnComprobarCompatibilidad;
        private System.Windows.Forms.Button btnVerRecomendaciones;
        private System.Windows.Forms.Label lblAreaEquivalente;
        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.ComboBox cmbZonaAgroclimatica;
        private System.Windows.Forms.NumericUpDown numPrecipitacionAnual;
        private System.Windows.Forms.ComboBox cmbDrenaje;
        private System.Windows.Forms.ComboBox cmbFuenteAgua;
        private System.Windows.Forms.CheckBox chkDisponibilidadRiego;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}

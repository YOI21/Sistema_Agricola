namespace Sistema_Agricola.UI
{
    partial class FrmSiembra
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
            pnlHeader = new Panel();
            lblTitulo = new Label();
            btnCerrar = new Button();
            tabControl = new TabControl();
            tabDatos = new TabPage();
            pnlDatos = new Panel();
            label1 = new Label();
            txtIDSiembra = new TextBox();
            label3 = new Label();
            dtpFechadeSiembra = new DateTimePicker();
            label5 = new Label();
            cbMetododeSiem = new ComboBox();
            label8 = new Label();
            comboBox2 = new ComboBox();
            label6 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label2 = new Label();
            textBox4 = new TextBox();
            lblTerrenoSiembra = new Label();
            cmbTerrenoSiembra = new ComboBox();
            lblHectareasSiembra = new Label();
            txtHectareasSiembra = new TextBox();
            lblDisponibilidadTerreno = new Label();
            tabCultivo = new TabPage();
            pnlCultivo = new Panel();
            lblVariedadCultivo = new Label();
            txtVariedadCultivo = new TextBox();
            lblTituloCultivo = new Label();
            lblSeleccionCultivo = new Label();
            cmbCultivosDisponibles = new ComboBox();
            lblCultivo = new Label();
            txtCultivo = new TextBox();
            lblTipoCultivo = new Label();
            cmbTipoCultivo = new ComboBox();
            lblCategoria = new Label();
            cmbCategoriaCultivo = new ComboBox();
            lblEstacion = new Label();
            cmbEstaciones = new ComboBox();
            lblTipoSuelo = new Label();
            cmbTipoSuelo = new ComboBox();
            btnEvaluarFactibilidad = new Button();
            btnSugerirCultivos = new Button();
            btnCalendarioAgricola = new Button();
            lblFactibilidad = new Label();
            lblDetalleRecomendacion = new Label();
            tabFertilizacion = new TabPage();
            pnlFertilizacion = new Panel();
            groupBox1 = new GroupBox();
            lblTipoFertilizante = new Label();
            comboBox3 = new ComboBox();
            lblMetodoAplicacion = new Label();
            comboBox1 = new ComboBox();
            lblUnidad = new Label();
            cmbUnidad = new ComboBox();
            lblCantidadFertilizacion = new Label();
            txtCantidadUtilizada = new TextBox();
            lblCostoFertilizacion = new Label();
            txtCostodeFertilizacion = new TextBox();
            lblFechaAplicacion = new Label();
            dtpFechadeAplicacion = new DateTimePicker();
            label14 = new Label();
            dtpFechaCosecha = new DateTimePicker();
            tabAdicional = new TabPage();
            pnlAdicional = new Panel();
            btnControldePlagas = new Button();
            button2 = new Button();
            pnlBotones = new Panel();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnLimpiar = new Button();
            btnVolver = new Button();
            pnlHeader.SuspendLayout();
            tabControl.SuspendLayout();
            tabDatos.SuspendLayout();
            pnlDatos.SuspendLayout();
            tabCultivo.SuspendLayout();
            pnlCultivo.SuspendLayout();
            tabFertilizacion.SuspendLayout();
            pnlFertilizacion.SuspendLayout();
            groupBox1.SuspendLayout();
            tabAdicional.SuspendLayout();
            pnlAdicional.SuspendLayout();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(245, 247, 250);
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnCerrar);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(970, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(62, 79, 98);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Padding = new Padding(20, 0, 0, 0);
            lblTitulo.Size = new Size(918, 58);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de siembras";
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
            btnCerrar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(918, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(50, 58);
            btnCerrar.TabIndex = 1;
            btnCerrar.TabStop = false;
            btnCerrar.Text = "X";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += button1_Click_1;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDatos);
            tabControl.Controls.Add(tabCultivo);
            tabControl.Controls.Add(tabFertilizacion);
            tabControl.Controls.Add(tabAdicional);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 9F);
            tabControl.ItemSize = new Size(140, 35);
            tabControl.Location = new Point(0, 60);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(970, 380);
            tabControl.TabIndex = 1;
            // 
            // tabDatos
            // 
            tabDatos.BackColor = Color.FromArgb(236, 240, 245);
            tabDatos.Controls.Add(pnlDatos);
            tabDatos.Location = new Point(4, 39);
            tabDatos.Name = "tabDatos";
            tabDatos.Padding = new Padding(10);
            tabDatos.Size = new Size(962, 337);
            tabDatos.TabIndex = 0;
            tabDatos.Text = "Datos básicos";
            // 
            // pnlDatos
            // 
            pnlDatos.AutoScroll = true;
            pnlDatos.BackColor = Color.White;
            pnlDatos.Controls.Add(label1);
            pnlDatos.Controls.Add(txtIDSiembra);
            pnlDatos.Controls.Add(label3);
            pnlDatos.Controls.Add(dtpFechadeSiembra);
            pnlDatos.Controls.Add(label5);
            pnlDatos.Controls.Add(cbMetododeSiem);
            pnlDatos.Controls.Add(label8);
            pnlDatos.Controls.Add(comboBox2);
            pnlDatos.Controls.Add(label6);
            pnlDatos.Controls.Add(textBox2);
            pnlDatos.Controls.Add(label4);
            pnlDatos.Controls.Add(textBox3);
            pnlDatos.Controls.Add(label2);
            pnlDatos.Controls.Add(textBox4);
            pnlDatos.Controls.Add(lblTerrenoSiembra);
            pnlDatos.Controls.Add(cmbTerrenoSiembra);
            pnlDatos.Controls.Add(lblHectareasSiembra);
            pnlDatos.Controls.Add(txtHectareasSiembra);
            pnlDatos.Controls.Add(lblDisponibilidadTerreno);
            pnlDatos.Dock = DockStyle.Fill;
            pnlDatos.Location = new Point(10, 10);
            pnlDatos.Name = "pnlDatos";
            pnlDatos.Size = new Size(942, 317);
            pnlDatos.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(150, 22);
            label1.TabIndex = 0;
            label1.Text = "ID de siembra:";
            // 
            // txtIDSiembra
            // 
            txtIDSiembra.Font = new Font("Segoe UI", 10F);
            txtIDSiembra.Location = new Point(180, 15);
            txtIDSiembra.Name = "txtIDSiembra";
            txtIDSiembra.ReadOnly = true;
            txtIDSiembra.Size = new Size(250, 25);
            txtIDSiembra.TabIndex = 1;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(20, 60);
            label3.Name = "label3";
            label3.Size = new Size(150, 22);
            label3.TabIndex = 2;
            label3.Text = "Fecha de siembra:";
            // 
            // dtpFechadeSiembra
            // 
            dtpFechadeSiembra.Format = DateTimePickerFormat.Short;
            dtpFechadeSiembra.Location = new Point(180, 60);
            dtpFechadeSiembra.Name = "dtpFechadeSiembra";
            dtpFechadeSiembra.Size = new Size(250, 23);
            dtpFechadeSiembra.TabIndex = 3;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(20, 105);
            label5.Name = "label5";
            label5.Size = new Size(150, 22);
            label5.TabIndex = 4;
            label5.Text = "Método de siembra:";
            // 
            // cbMetododeSiem
            // 
            cbMetododeSiem.Items.AddRange(new object[] { "Manual", "Mecánica" });
            cbMetododeSiem.Location = new Point(180, 105);
            cbMetododeSiem.Name = "cbMetododeSiem";
            cbMetododeSiem.Size = new Size(250, 23);
            cbMetododeSiem.TabIndex = 5;
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label8.Location = new Point(20, 150);
            label8.Name = "label8";
            label8.Size = new Size(150, 22);
            label8.TabIndex = 6;
            label8.Text = "Tipo aplicado:";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Enabled = false;
            comboBox2.Items.AddRange(new object[] { "Híbrida", "Orgánica", "Transgénica" });
            comboBox2.Location = new Point(180, 150);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(250, 23);
            comboBox2.TabIndex = 7;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.Location = new Point(20, 195);
            label6.Name = "label6";
            label6.Size = new Size(150, 22);
            label6.TabIndex = 8;
            label6.Text = "Profundidad:";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 10F);
            textBox2.Location = new Point(180, 195);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(250, 25);
            textBox2.TabIndex = 9;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(20, 240);
            label4.Name = "label4";
            label4.Size = new Size(150, 22);
            label4.TabIndex = 10;
            label4.Text = "Cantidad a sembrar:";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 10F);
            textBox3.Location = new Point(180, 240);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 25);
            textBox3.TabIndex = 11;
            textBox3.TextAlign = HorizontalAlignment.Right;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(20, 285);
            label2.Name = "label2";
            label2.Size = new Size(150, 22);
            label2.TabIndex = 12;
            label2.Text = "Costo de siembra:";
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI", 10F);
            textBox4.Location = new Point(180, 285);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 25);
            textBox4.TabIndex = 13;
            textBox4.TextAlign = HorizontalAlignment.Right;
            textBox4.KeyPress += textBox4_KeyPress;
            // 
            // lblTerrenoSiembra
            // 
            lblTerrenoSiembra.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTerrenoSiembra.Location = new Point(500, 15);
            lblTerrenoSiembra.Name = "lblTerrenoSiembra";
            lblTerrenoSiembra.Size = new Size(145, 22);
            lblTerrenoSiembra.TabIndex = 14;
            lblTerrenoSiembra.Text = "Terreno:";
            // 
            // cmbTerrenoSiembra
            // 
            cmbTerrenoSiembra.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTerrenoSiembra.Font = new Font("Segoe UI", 10F);
            cmbTerrenoSiembra.Location = new Point(650, 15);
            cmbTerrenoSiembra.Name = "cmbTerrenoSiembra";
            cmbTerrenoSiembra.Size = new Size(260, 25);
            cmbTerrenoSiembra.TabIndex = 15;
            cmbTerrenoSiembra.SelectedIndexChanged += cmbTerrenoSiembra_SelectedIndexChanged;
            // 
            // lblHectareasSiembra
            // 
            lblHectareasSiembra.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHectareasSiembra.Location = new Point(482, 61);
            lblHectareasSiembra.Name = "lblHectareasSiembra";
            lblHectareasSiembra.Size = new Size(162, 22);
            lblHectareasSiembra.TabIndex = 16;
            lblHectareasSiembra.Text = "Hectáreas a sembrar:";
            // 
            // txtHectareasSiembra
            // 
            txtHectareasSiembra.Font = new Font("Segoe UI", 10F);
            txtHectareasSiembra.Location = new Point(650, 60);
            txtHectareasSiembra.Name = "txtHectareasSiembra";
            txtHectareasSiembra.Size = new Size(120, 25);
            txtHectareasSiembra.TabIndex = 17;
            txtHectareasSiembra.TextAlign = HorizontalAlignment.Right;
            txtHectareasSiembra.TextChanged += txtHectareasSiembra_TextChanged;
            txtHectareasSiembra.KeyPress += txtCantidadUtilizada_KeyPress;
            // 
            // lblDisponibilidadTerreno
            // 
            lblDisponibilidadTerreno.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDisponibilidadTerreno.ForeColor = Color.FromArgb(91, 124, 153);
            lblDisponibilidadTerreno.Location = new Point(500, 105);
            lblDisponibilidadTerreno.Name = "lblDisponibilidadTerreno";
            lblDisponibilidadTerreno.Size = new Size(410, 80);
            lblDisponibilidadTerreno.TabIndex = 18;
            lblDisponibilidadTerreno.Text = "Seleccione un terreno para ver disponibilidad.";
            // 
            // tabCultivo
            // 
            tabCultivo.BackColor = Color.FromArgb(236, 240, 245);
            tabCultivo.Controls.Add(pnlCultivo);
            tabCultivo.Location = new Point(4, 39);
            tabCultivo.Name = "tabCultivo";
            tabCultivo.Padding = new Padding(10);
            tabCultivo.Size = new Size(962, 337);
            tabCultivo.TabIndex = 1;
            tabCultivo.Text = "Datos del cultivo";
            // 
            // pnlCultivo
            // 
            pnlCultivo.AutoScroll = true;
            pnlCultivo.BackColor = Color.White;
            pnlCultivo.Controls.Add(lblVariedadCultivo);
            pnlCultivo.Controls.Add(txtVariedadCultivo);
            pnlCultivo.Controls.Add(lblTituloCultivo);
            pnlCultivo.Controls.Add(lblSeleccionCultivo);
            pnlCultivo.Controls.Add(cmbCultivosDisponibles);
            pnlCultivo.Controls.Add(lblCultivo);
            pnlCultivo.Controls.Add(txtCultivo);
            pnlCultivo.Controls.Add(lblTipoCultivo);
            pnlCultivo.Controls.Add(cmbTipoCultivo);
            pnlCultivo.Controls.Add(lblCategoria);
            pnlCultivo.Controls.Add(cmbCategoriaCultivo);
            pnlCultivo.Controls.Add(lblEstacion);
            pnlCultivo.Controls.Add(cmbEstaciones);
            pnlCultivo.Controls.Add(lblTipoSuelo);
            pnlCultivo.Controls.Add(cmbTipoSuelo);
            pnlCultivo.Controls.Add(btnEvaluarFactibilidad);
            pnlCultivo.Controls.Add(btnSugerirCultivos);
            pnlCultivo.Controls.Add(btnCalendarioAgricola);
            pnlCultivo.Controls.Add(lblFactibilidad);
            pnlCultivo.Controls.Add(lblDetalleRecomendacion);
            pnlCultivo.Dock = DockStyle.Fill;
            pnlCultivo.Location = new Point(10, 10);
            pnlCultivo.Name = "pnlCultivo";
            pnlCultivo.Size = new Size(942, 317);
            pnlCultivo.TabIndex = 0;
            // 
            // lblVariedadCultivo
            // 
            lblVariedadCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblVariedadCultivo.Location = new Point(520, 15);
            lblVariedadCultivo.Name = "lblVariedadCultivo";
            lblVariedadCultivo.Size = new Size(105, 22);
            lblVariedadCultivo.TabIndex = 18;
            lblVariedadCultivo.Text = "Variedad:";
            // 
            // txtVariedadCultivo
            // 
            txtVariedadCultivo.Font = new Font("Segoe UI", 10F);
            txtVariedadCultivo.Location = new Point(625, 15);
            txtVariedadCultivo.MaxLength = 80;
            txtVariedadCultivo.Name = "txtVariedadCultivo";
            txtVariedadCultivo.Size = new Size(285, 25);
            txtVariedadCultivo.TabIndex = 19;
            // 
            // lblTituloCultivo
            // 
            lblTituloCultivo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTituloCultivo.ForeColor = Color.FromArgb(62, 79, 98);
            lblTituloCultivo.Location = new Point(20, 15);
            lblTituloCultivo.Name = "lblTituloCultivo";
            lblTituloCultivo.Size = new Size(400, 25);
            lblTituloCultivo.TabIndex = 0;
            lblTituloCultivo.Text = "Datos del cultivo y calendario agrícola";
            // 
            // lblSeleccionCultivo
            // 
            lblSeleccionCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSeleccionCultivo.Location = new Point(20, 55);
            lblSeleccionCultivo.Name = "lblSeleccionCultivo";
            lblSeleccionCultivo.Size = new Size(150, 22);
            lblSeleccionCultivo.TabIndex = 1;
            lblSeleccionCultivo.Text = "Seleccionar cultivo:";
            // 
            // cmbCultivosDisponibles
            // 
            cmbCultivosDisponibles.Location = new Point(180, 55);
            cmbCultivosDisponibles.Name = "cmbCultivosDisponibles";
            cmbCultivosDisponibles.Size = new Size(300, 23);
            cmbCultivosDisponibles.TabIndex = 2;
            cmbCultivosDisponibles.SelectedIndexChanged += cmbCultivosDisponibles_SelectedIndexChanged;
            // 
            // lblCultivo
            // 
            lblCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCultivo.Location = new Point(20, 100);
            lblCultivo.Name = "lblCultivo";
            lblCultivo.Size = new Size(150, 22);
            lblCultivo.TabIndex = 3;
            lblCultivo.Text = "Cultivo:";
            // 
            // txtCultivo
            // 
            txtCultivo.Font = new Font("Segoe UI", 10F);
            txtCultivo.Location = new Point(180, 100);
            txtCultivo.Name = "txtCultivo";
            txtCultivo.Size = new Size(300, 25);
            txtCultivo.TabIndex = 4;
            txtCultivo.TextChanged += txtCultivo_TextChanged;
            // 
            // lblTipoCultivo
            // 
            lblTipoCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTipoCultivo.Location = new Point(20, 145);
            lblTipoCultivo.Name = "lblTipoCultivo";
            lblTipoCultivo.Size = new Size(150, 22);
            lblTipoCultivo.TabIndex = 5;
            lblTipoCultivo.Text = "Tipo de cultivo:";
            // 
            // cmbTipoCultivo
            // 
            cmbTipoCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoCultivo.Enabled = false;
            cmbTipoCultivo.Items.AddRange(new object[] { "Orgánica", "Híbrida", "Transgénica" });
            cmbTipoCultivo.Location = new Point(180, 145);
            cmbTipoCultivo.Name = "cmbTipoCultivo";
            cmbTipoCultivo.Size = new Size(300, 23);
            cmbTipoCultivo.TabIndex = 6;
            // 
            // lblCategoria
            // 
            lblCategoria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategoria.Location = new Point(20, 190);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(150, 22);
            lblCategoria.TabIndex = 7;
            lblCategoria.Text = "Categoría:";
            // 
            // cmbCategoriaCultivo
            // 
            cmbCategoriaCultivo.Items.AddRange(new object[] { "Comestible", "Ornamental", "Industrial", "Medicinal" });
            cmbCategoriaCultivo.Location = new Point(180, 190);
            cmbCategoriaCultivo.Name = "cmbCategoriaCultivo";
            cmbCategoriaCultivo.Size = new Size(300, 23);
            cmbCategoriaCultivo.TabIndex = 8;
            // 
            // lblEstacion
            // 
            lblEstacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEstacion.Location = new Point(20, 235);
            lblEstacion.Name = "lblEstacion";
            lblEstacion.Size = new Size(150, 22);
            lblEstacion.TabIndex = 9;
            lblEstacion.Text = "Período climático:";
            // 
            // cmbEstaciones
            // 
            cmbEstaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstaciones.Items.AddRange(new object[] { "Período lluvioso", "Período seco", "Transición" });
            cmbEstaciones.Location = new Point(180, 235);
            cmbEstaciones.Name = "cmbEstaciones";
            cmbEstaciones.Size = new Size(300, 23);
            cmbEstaciones.TabIndex = 10;
            // 
            // lblTipoSuelo
            // 
            lblTipoSuelo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTipoSuelo.Location = new Point(20, 280);
            lblTipoSuelo.Name = "lblTipoSuelo";
            lblTipoSuelo.Size = new Size(150, 22);
            lblTipoSuelo.TabIndex = 11;
            lblTipoSuelo.Text = "Tipo de suelo:";
            // 
            // cmbTipoSuelo
            // 
            cmbTipoSuelo.Items.AddRange(new object[] { "Arcilloso", "Arenoso", "Franco", "Limoso", "Franco arenoso", "Franco arcilloso" });
            cmbTipoSuelo.Location = new Point(180, 280);
            cmbTipoSuelo.Name = "cmbTipoSuelo";
            cmbTipoSuelo.Size = new Size(300, 23);
            cmbTipoSuelo.TabIndex = 12;
            // 
            // btnEvaluarFactibilidad
            // 
            btnEvaluarFactibilidad.BackColor = Color.FromArgb(91, 124, 153);
            btnEvaluarFactibilidad.Cursor = Cursors.Hand;
            btnEvaluarFactibilidad.FlatAppearance.BorderSize = 0;
            btnEvaluarFactibilidad.FlatStyle = FlatStyle.Flat;
            btnEvaluarFactibilidad.Font = new Font("Segoe UI", 9F);
            btnEvaluarFactibilidad.ForeColor = Color.White;
            btnEvaluarFactibilidad.Location = new Point(520, 55);
            btnEvaluarFactibilidad.Name = "btnEvaluarFactibilidad";
            btnEvaluarFactibilidad.Size = new Size(180, 38);
            btnEvaluarFactibilidad.TabIndex = 13;
            btnEvaluarFactibilidad.Text = "Evaluar condiciones";
            btnEvaluarFactibilidad.UseVisualStyleBackColor = false;
            btnEvaluarFactibilidad.Click += btnEvaluarFactibilidad_Click;
            // 
            // btnSugerirCultivos
            // 
            btnSugerirCultivos.BackColor = Color.FromArgb(46, 125, 50);
            btnSugerirCultivos.Cursor = Cursors.Hand;
            btnSugerirCultivos.FlatAppearance.BorderSize = 0;
            btnSugerirCultivos.FlatStyle = FlatStyle.Flat;
            btnSugerirCultivos.Font = new Font("Segoe UI", 9F);
            btnSugerirCultivos.ForeColor = Color.White;
            btnSugerirCultivos.Location = new Point(520, 105);
            btnSugerirCultivos.Name = "btnSugerirCultivos";
            btnSugerirCultivos.Size = new Size(180, 38);
            btnSugerirCultivos.TabIndex = 14;
            btnSugerirCultivos.Text = "Recomendar cultivos";
            btnSugerirCultivos.UseVisualStyleBackColor = false;
            btnSugerirCultivos.Click += btnSugerirCultivos_Click;
            // 
            // btnCalendarioAgricola
            // 
            btnCalendarioAgricola.BackColor = Color.FromArgb(255, 152, 0);
            btnCalendarioAgricola.Cursor = Cursors.Hand;
            btnCalendarioAgricola.FlatAppearance.BorderSize = 0;
            btnCalendarioAgricola.FlatStyle = FlatStyle.Flat;
            btnCalendarioAgricola.Font = new Font("Segoe UI", 9F);
            btnCalendarioAgricola.ForeColor = Color.White;
            btnCalendarioAgricola.Location = new Point(520, 155);
            btnCalendarioAgricola.Name = "btnCalendarioAgricola";
            btnCalendarioAgricola.Size = new Size(180, 38);
            btnCalendarioAgricola.TabIndex = 15;
            btnCalendarioAgricola.Text = "Calendario agrícola";
            btnCalendarioAgricola.UseVisualStyleBackColor = false;
            btnCalendarioAgricola.Click += btnCalendarioAgricola_Click;
            // 
            // lblFactibilidad
            // 
            lblFactibilidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFactibilidad.ForeColor = Color.Black;
            lblFactibilidad.Location = new Point(520, 220);
            lblFactibilidad.Name = "lblFactibilidad";
            lblFactibilidad.Size = new Size(360, 25);
            lblFactibilidad.TabIndex = 16;
            lblFactibilidad.Text = "Recomendación: Sin evaluar";
            // 
            // lblDetalleRecomendacion
            // 
            lblDetalleRecomendacion.AutoEllipsis = true;
            lblDetalleRecomendacion.Font = new Font("Segoe UI", 9F);
            lblDetalleRecomendacion.Location = new Point(520, 252);
            lblDetalleRecomendacion.Name = "lblDetalleRecomendacion";
            lblDetalleRecomendacion.Size = new Size(390, 42);
            lblDetalleRecomendacion.TabIndex = 17;
            // 
            // tabFertilizacion
            // 
            tabFertilizacion.BackColor = Color.FromArgb(236, 240, 245);
            tabFertilizacion.Controls.Add(pnlFertilizacion);
            tabFertilizacion.Location = new Point(4, 39);
            tabFertilizacion.Name = "tabFertilizacion";
            tabFertilizacion.Padding = new Padding(10);
            tabFertilizacion.Size = new Size(962, 337);
            tabFertilizacion.TabIndex = 2;
            tabFertilizacion.Text = "Fertilización";
            // 
            // pnlFertilizacion
            // 
            pnlFertilizacion.AutoScroll = true;
            pnlFertilizacion.BackColor = Color.White;
            pnlFertilizacion.Controls.Add(groupBox1);
            pnlFertilizacion.Dock = DockStyle.Fill;
            pnlFertilizacion.Location = new Point(10, 10);
            pnlFertilizacion.Name = "pnlFertilizacion";
            pnlFertilizacion.Size = new Size(942, 317);
            pnlFertilizacion.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(lblTipoFertilizante);
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(lblMetodoAplicacion);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(lblUnidad);
            groupBox1.Controls.Add(cmbUnidad);
            groupBox1.Controls.Add(lblCantidadFertilizacion);
            groupBox1.Controls.Add(txtCantidadUtilizada);
            groupBox1.Controls.Add(lblCostoFertilizacion);
            groupBox1.Controls.Add(txtCostodeFertilizacion);
            groupBox1.Controls.Add(lblFechaAplicacion);
            groupBox1.Controls.Add(dtpFechadeAplicacion);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(dtpFechaCosecha);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(942, 317);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información de fertilización";
            // 
            // lblTipoFertilizante
            // 
            lblTipoFertilizante.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTipoFertilizante.Location = new Point(20, 30);
            lblTipoFertilizante.Name = "lblTipoFertilizante";
            lblTipoFertilizante.Size = new Size(150, 22);
            lblTipoFertilizante.TabIndex = 0;
            lblTipoFertilizante.Text = "Tipo de fertilizante:";
            // 
            // comboBox3
            // 
            comboBox3.Font = new Font("Segoe UI", 10F);
            comboBox3.Items.AddRange(new object[] { "Químico", "Orgánico" });
            comboBox3.Location = new Point(180, 30);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(200, 25);
            comboBox3.TabIndex = 1;
            // 
            // lblMetodoAplicacion
            // 
            lblMetodoAplicacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMetodoAplicacion.Location = new Point(20, 75);
            lblMetodoAplicacion.Name = "lblMetodoAplicacion";
            lblMetodoAplicacion.Size = new Size(150, 22);
            lblMetodoAplicacion.TabIndex = 2;
            lblMetodoAplicacion.Text = "Método:";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 10F);
            comboBox1.Items.AddRange(new object[] { "Aplicación al suelo", "Aplicación foliar", "Aplicación al agua" });
            comboBox1.Location = new Point(180, 75);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 25);
            comboBox1.TabIndex = 3;
            // 
            // lblUnidad
            // 
            lblUnidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUnidad.Location = new Point(20, 120);
            lblUnidad.Name = "lblUnidad";
            lblUnidad.Size = new Size(150, 22);
            lblUnidad.TabIndex = 4;
            lblUnidad.Text = "Unidad:";
            // 
            // cmbUnidad
            // 
            cmbUnidad.Font = new Font("Segoe UI", 10F);
            cmbUnidad.Items.AddRange(new object[] { "lb", "kg", "t" });
            cmbUnidad.Location = new Point(180, 120);
            cmbUnidad.Name = "cmbUnidad";
            cmbUnidad.Size = new Size(200, 25);
            cmbUnidad.TabIndex = 5;
            cmbUnidad.SelectedIndexChanged += cmbUnidad_SelectedIndexChanged;
            // 
            // lblCantidadFertilizacion
            // 
            lblCantidadFertilizacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCantidadFertilizacion.Location = new Point(20, 165);
            lblCantidadFertilizacion.Name = "lblCantidadFertilizacion";
            lblCantidadFertilizacion.Size = new Size(150, 22);
            lblCantidadFertilizacion.TabIndex = 6;
            lblCantidadFertilizacion.Text = "Cantidad:";
            // 
            // txtCantidadUtilizada
            // 
            txtCantidadUtilizada.Font = new Font("Segoe UI", 10F);
            txtCantidadUtilizada.Location = new Point(180, 165);
            txtCantidadUtilizada.Name = "txtCantidadUtilizada";
            txtCantidadUtilizada.Size = new Size(200, 25);
            txtCantidadUtilizada.TabIndex = 7;
            txtCantidadUtilizada.KeyPress += txtCantidadUtilizada_KeyPress;
            // 
            // lblCostoFertilizacion
            // 
            lblCostoFertilizacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCostoFertilizacion.Location = new Point(20, 210);
            lblCostoFertilizacion.Name = "lblCostoFertilizacion";
            lblCostoFertilizacion.Size = new Size(150, 22);
            lblCostoFertilizacion.TabIndex = 8;
            lblCostoFertilizacion.Text = "Costo:";
            // 
            // txtCostodeFertilizacion
            // 
            txtCostodeFertilizacion.Font = new Font("Segoe UI", 10F);
            txtCostodeFertilizacion.Location = new Point(180, 210);
            txtCostodeFertilizacion.Name = "txtCostodeFertilizacion";
            txtCostodeFertilizacion.Size = new Size(200, 25);
            txtCostodeFertilizacion.TabIndex = 9;
            txtCostodeFertilizacion.TextAlign = HorizontalAlignment.Right;
            txtCostodeFertilizacion.KeyPress += txtCostodeFertilizacion_KeyPress;
            // 
            // lblFechaAplicacion
            // 
            lblFechaAplicacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaAplicacion.Location = new Point(430, 30);
            lblFechaAplicacion.Name = "lblFechaAplicacion";
            lblFechaAplicacion.Size = new Size(170, 22);
            lblFechaAplicacion.TabIndex = 10;
            lblFechaAplicacion.Text = "Fecha de aplicación:";
            // 
            // dtpFechadeAplicacion
            // 
            dtpFechadeAplicacion.Font = new Font("Segoe UI", 10F);
            dtpFechadeAplicacion.Format = DateTimePickerFormat.Short;
            dtpFechadeAplicacion.Location = new Point(610, 30);
            dtpFechadeAplicacion.Name = "dtpFechadeAplicacion";
            dtpFechadeAplicacion.Size = new Size(200, 25);
            dtpFechadeAplicacion.TabIndex = 11;
            // 
            // label14
            // 
            label14.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label14.Location = new Point(430, 75);
            label14.Name = "label14";
            label14.Size = new Size(170, 22);
            label14.TabIndex = 12;
            label14.Text = "Fecha de cosecha:";
            // 
            // dtpFechaCosecha
            // 
            dtpFechaCosecha.Font = new Font("Segoe UI", 10F);
            dtpFechaCosecha.Format = DateTimePickerFormat.Short;
            dtpFechaCosecha.Location = new Point(610, 75);
            dtpFechaCosecha.Name = "dtpFechaCosecha";
            dtpFechaCosecha.Size = new Size(200, 25);
            dtpFechaCosecha.TabIndex = 13;
            // 
            // tabAdicional
            // 
            tabAdicional.BackColor = Color.FromArgb(236, 240, 245);
            tabAdicional.Controls.Add(pnlAdicional);
            tabAdicional.Location = new Point(4, 39);
            tabAdicional.Name = "tabAdicional";
            tabAdicional.Padding = new Padding(10);
            tabAdicional.Size = new Size(962, 337);
            tabAdicional.TabIndex = 3;
            tabAdicional.Text = "Opciones";
            // 
            // pnlAdicional
            // 
            pnlAdicional.AutoScroll = true;
            pnlAdicional.BackColor = Color.White;
            pnlAdicional.Controls.Add(btnControldePlagas);
            pnlAdicional.Controls.Add(button2);
            pnlAdicional.Dock = DockStyle.Fill;
            pnlAdicional.Location = new Point(10, 10);
            pnlAdicional.Name = "pnlAdicional";
            pnlAdicional.Size = new Size(942, 317);
            pnlAdicional.TabIndex = 0;
            // 
            // btnControldePlagas
            // 
            btnControldePlagas.BackColor = Color.FromArgb(156, 39, 176);
            btnControldePlagas.Cursor = Cursors.Hand;
            btnControldePlagas.FlatAppearance.BorderSize = 0;
            btnControldePlagas.FlatStyle = FlatStyle.Flat;
            btnControldePlagas.Font = new Font("Segoe UI", 10F);
            btnControldePlagas.ForeColor = Color.White;
            btnControldePlagas.Location = new Point(30, 20);
            btnControldePlagas.Name = "btnControldePlagas";
            btnControldePlagas.Size = new Size(250, 50);
            btnControldePlagas.TabIndex = 0;
            btnControldePlagas.Text = "Control de plagas";
            btnControldePlagas.UseVisualStyleBackColor = false;
            btnControldePlagas.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(91, 124, 153);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 10F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(30, 90);
            button2.Name = "button2";
            button2.Size = new Size(250, 50);
            button2.TabIndex = 1;
            button2.Text = "Generar nuevo ID";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(245, 247, 250);
            pnlBotones.BorderStyle = BorderStyle.FixedSingle;
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnActualizar);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Controls.Add(btnVolver);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 440);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(10);
            pnlBotones.Size = new Size(970, 70);
            pnlBotones.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 125, 50);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(10, 12);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(130, 45);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "&Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(91, 124, 153);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 10F);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(150, 12);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(130, 45);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "&Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(189, 195, 199);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(290, 12);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(130, 45);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "&Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(231, 76, 60);
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(830, 12);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(130, 45);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "&Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmSiembra
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnVolver;
            ClientSize = new Size(970, 510);
            Controls.Add(tabControl);
            Controls.Add(pnlBotones);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "FrmSiembra";
            Text = "Gestión de siembras";
            Load += FrmSiembra_Load;
            pnlHeader.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabDatos.ResumeLayout(false);
            pnlDatos.ResumeLayout(false);
            pnlDatos.PerformLayout();
            tabCultivo.ResumeLayout(false);
            pnlCultivo.ResumeLayout(false);
            pnlCultivo.PerformLayout();
            tabFertilizacion.ResumeLayout(false);
            pnlFertilizacion.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabAdicional.ResumeLayout(false);
            pnlAdicional.ResumeLayout(false);
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnCerrar;
        private TabControl tabControl;
        private TabPage tabDatos;
        private TabPage tabCultivo;
        private TabPage tabFertilizacion;
        private TabPage tabAdicional;
        private Panel pnlDatos;
        private Panel pnlCultivo;
        private Panel pnlFertilizacion;
        private Panel pnlAdicional;
        private Panel pnlBotones;

        private Label label1;
        private TextBox txtIDSiembra;
        private Label label3;
        private DateTimePicker dtpFechadeSiembra;
        private Label label5;
        private ComboBox cbMetododeSiem;
        private Label label8;
        private ComboBox comboBox2;
        private Label label6;
        private TextBox textBox2;
        private Label label4;
        private TextBox textBox3;
        private Label label2;
        private TextBox textBox4;
        private Label lblTerrenoSiembra;
        private ComboBox cmbTerrenoSiembra;
        private Label lblHectareasSiembra;
        private TextBox txtHectareasSiembra;
        private Label lblDisponibilidadTerreno;

        private Label lblTituloCultivo;
        private Label lblSeleccionCultivo;
        private ComboBox cmbCultivosDisponibles;
        private Label lblCultivo;
        private TextBox txtCultivo;
        private Label lblTipoCultivo;
        private ComboBox cmbTipoCultivo;
        private Label lblCategoria;
        private ComboBox cmbCategoriaCultivo;
        private Label lblEstacion;
        private ComboBox cmbEstaciones;
        private Label lblTipoSuelo;
        private ComboBox cmbTipoSuelo;
        private Button btnEvaluarFactibilidad;
        private Button btnSugerirCultivos;
        private Button btnCalendarioAgricola;
        private Label lblFactibilidad;
        private Label lblDetalleRecomendacion;
        private Label lblVariedadCultivo;
        private TextBox txtVariedadCultivo;

        private GroupBox groupBox1;
        private Label lblTipoFertilizante;
        private ComboBox comboBox3;
        private Label lblMetodoAplicacion;
        private ComboBox comboBox1;
        private Label lblUnidad;
        private ComboBox cmbUnidad;
        private Label lblCantidadFertilizacion;
        private TextBox txtCantidadUtilizada;
        private Label lblCostoFertilizacion;
        private TextBox txtCostodeFertilizacion;
        private Label lblFechaAplicacion;
        private DateTimePicker dtpFechadeAplicacion;
        private Label label14;
        private DateTimePicker dtpFechaCosecha;

        private Button btnControldePlagas;
        private Button button2;

        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnLimpiar;
        private Button btnVolver;
    }
}

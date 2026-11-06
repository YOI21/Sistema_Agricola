namespace Sistema_Agricola.UI
{
    partial class FrmTrabajadores
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
            panelHeader = new Panel();
            labelTitle = new Label();
            btnVolver = new Button();
            panelContent = new Panel();
            cardDatosTrabajador = new Panel();
            panelForm = new Panel();
            nudSueldo = new NumericUpDown();
            lblSueldo = new Label();
            cmbPosicion = new ComboBox();
            label13 = new Label();
            dtpFechaIngreso = new DateTimePicker();
            label11 = new Label();
            txtSector = new TextBox();
            label10 = new Label();
            txtCiudad = new TextBox();
            label9 = new Label();
            txtTelefono = new TextBox();
            label8 = new Label();
            txtDireccion = new TextBox();
            label7 = new Label();
            dtpFechaNac = new DateTimePicker();
            label6 = new Label();
            txtEmail = new TextBox();
            label5 = new Label();
            cmbSexo = new ComboBox();
            label4 = new Label();
            txtCedula = new TextBox();
            label3 = new Label();
            txtApellido = new TextBox();
            label2 = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            txtID = new TextBox();
            lblID = new Label();
            labelDatosTrabajador = new Label();
            panelAcciones = new Panel();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnActualizar = new Button();
            btnGuardar = new Button();
            cardTrabajadores = new Panel();
            panelBusqueda = new Panel();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            labelBuscar = new Label();
            panelListado = new Panel();
            dgvTrabajadores = new DataGridView();
            labelListaTrabajadores = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            cardDatosTrabajador.SuspendLayout();
            panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSueldo).BeginInit();
            panelAcciones.SuspendLayout();
            cardTrabajadores.SuspendLayout();
            panelBusqueda.SuspendLayout();
            panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTrabajadores).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(245, 247, 250);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Controls.Add(btnVolver);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 70);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(62, 79, 98);
            labelTitle.Location = new Point(20, 17);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(295, 41);
            labelTitle.TabIndex = 13;
            labelTitle.Text = "Registro de trabajadores";
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(231, 76, 60);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1145, 15);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(40, 40);
            btnVolver.TabIndex = 14;
            btnVolver.Text = "X";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(240, 245, 249);
            panelContent.Controls.Add(cardDatosTrabajador);
            panelContent.Controls.Add(panelAcciones);
            panelContent.Controls.Add(cardTrabajadores);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 70);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1200, 720);
            panelContent.TabIndex = 1;
            // 
            // cardDatosTrabajador
            // 
            cardDatosTrabajador.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardDatosTrabajador.BackColor = Color.White;
            cardDatosTrabajador.Controls.Add(panelForm);
            cardDatosTrabajador.Controls.Add(labelDatosTrabajador);
            cardDatosTrabajador.Location = new Point(20, 20);
            cardDatosTrabajador.Name = "cardDatosTrabajador";
            cardDatosTrabajador.Size = new Size(1160, 300);
            cardDatosTrabajador.TabIndex = 0;
            // 
            // panelForm
            // 
            panelForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelForm.Controls.Add(nudSueldo);
            panelForm.Controls.Add(lblSueldo);
            panelForm.Controls.Add(cmbPosicion);
            panelForm.Controls.Add(label13);
            panelForm.Controls.Add(dtpFechaIngreso);
            panelForm.Controls.Add(label11);
            panelForm.Controls.Add(txtSector);
            panelForm.Controls.Add(label10);
            panelForm.Controls.Add(txtCiudad);
            panelForm.Controls.Add(label9);
            panelForm.Controls.Add(txtTelefono);
            panelForm.Controls.Add(label8);
            panelForm.Controls.Add(txtDireccion);
            panelForm.Controls.Add(label7);
            panelForm.Controls.Add(dtpFechaNac);
            panelForm.Controls.Add(label6);
            panelForm.Controls.Add(txtEmail);
            panelForm.Controls.Add(label5);
            panelForm.Controls.Add(cmbSexo);
            panelForm.Controls.Add(label4);
            panelForm.Controls.Add(txtCedula);
            panelForm.Controls.Add(label3);
            panelForm.Controls.Add(txtApellido);
            panelForm.Controls.Add(label2);
            panelForm.Controls.Add(txtNombre);
            panelForm.Controls.Add(lblNombre);
            panelForm.Controls.Add(txtID);
            panelForm.Controls.Add(lblID);
            panelForm.Location = new Point(15, 50);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(1130, 240);
            panelForm.TabIndex = 1;
            // 
            // nudSueldo
            // 
            nudSueldo.BackColor = Color.FromArgb(240, 245, 249);
            nudSueldo.BorderStyle = BorderStyle.FixedSingle;
            nudSueldo.DecimalPlaces = 2;
            nudSueldo.Font = new Font("Segoe UI", 10F);
            nudSueldo.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudSueldo.Location = new Point(785, 188);
            nudSueldo.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudSueldo.Name = "nudSueldo";
            nudSueldo.Size = new Size(230, 30);
            nudSueldo.TabIndex = 27;
            nudSueldo.ThousandsSeparator = true;
            // 
            // lblSueldo
            // 
            lblSueldo.AutoSize = true;
            lblSueldo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSueldo.ForeColor = Color.FromArgb(62, 79, 98);
            lblSueldo.Location = new Point(705, 190);
            lblSueldo.Name = "lblSueldo";
            lblSueldo.Size = new Size(65, 23);
            lblSueldo.TabIndex = 26;
            lblSueldo.Text = "Sueldo:";
            // 
            // cmbPosicion
            // 
            cmbPosicion.BackColor = Color.FromArgb(240, 245, 249);
            cmbPosicion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPosicion.FlatStyle = FlatStyle.Flat;
            cmbPosicion.Font = new Font("Segoe UI", 10F);
            cmbPosicion.FormattingEnabled = true;
            cmbPosicion.Items.AddRange(new object[] { "Trabajador agrícola", "Capataz", "Tractorista", "Fumigador", "Operador de maquinaria", "Encargado de riego", "Recolector", "Supervisor", "Técnico agrícola", "Otro" });
            cmbPosicion.Location = new Point(110, 188);
            cmbPosicion.Name = "cmbPosicion";
            cmbPosicion.Size = new Size(230, 31);
            cmbPosicion.TabIndex = 25;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label13.ForeColor = Color.FromArgb(62, 79, 98);
            label13.Location = new Point(20, 190);
            label13.Name = "label13";
            label13.Size = new Size(76, 23);
            label13.TabIndex = 24;
            label13.Text = "Rol laboral:";
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.CalendarFont = new Font("Segoe UI", 10F);
            dtpFechaIngreso.CalendarForeColor = Color.FromArgb(51, 51, 51);
            dtpFechaIngreso.CalendarMonthBackground = Color.FromArgb(240, 245, 249);
            dtpFechaIngreso.CalendarTitleBackColor = Color.FromArgb(62, 79, 98);
            dtpFechaIngreso.CalendarTitleForeColor = Color.White;
            dtpFechaIngreso.Font = new Font("Segoe UI", 10F);
            dtpFechaIngreso.Format = DateTimePickerFormat.Short;
            dtpFechaIngreso.Location = new Point(445, 188);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.Size = new Size(230, 30);
            dtpFechaIngreso.TabIndex = 23;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label11.ForeColor = Color.FromArgb(62, 79, 98);
            label11.Location = new Point(370, 190);
            label11.Name = "label11";
            label11.Size = new Size(78, 23);
            label11.TabIndex = 22;
            label11.Text = "Fec. Ing.:";
            // 
            // txtSector
            // 
            txtSector.BackColor = Color.FromArgb(240, 245, 249);
            txtSector.BorderStyle = BorderStyle.FixedSingle;
            txtSector.Font = new Font("Segoe UI", 10F);
            txtSector.Location = new Point(785, 108);
            txtSector.Name = "txtSector";
            txtSector.Size = new Size(230, 30);
            txtSector.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(62, 79, 98);
            label10.Location = new Point(705, 110);
            label10.Name = "label10";
            label10.Size = new Size(63, 23);
            label10.TabIndex = 20;
            label10.Text = "Sector:";
            // 
            // txtCiudad
            // 
            txtCiudad.BackColor = Color.FromArgb(240, 245, 249);
            txtCiudad.BorderStyle = BorderStyle.FixedSingle;
            txtCiudad.Font = new Font("Segoe UI", 10F);
            txtCiudad.Location = new Point(785, 68);
            txtCiudad.Name = "txtCiudad";
            txtCiudad.Size = new Size(230, 30);
            txtCiudad.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(62, 79, 98);
            label9.Location = new Point(705, 70);
            label9.Name = "label9";
            label9.Size = new Size(68, 23);
            label9.TabIndex = 18;
            label9.Text = "Ciudad:";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(240, 245, 249);
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.Location = new Point(785, 28);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(230, 30);
            txtTelefono.TabIndex = 17;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(62, 79, 98);
            label8.Location = new Point(705, 30);
            label8.Name = "label8";
            label8.Size = new Size(79, 23);
            label8.TabIndex = 16;
            label8.Text = "Teléfono:";
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.FromArgb(240, 245, 249);
            txtDireccion.BorderStyle = BorderStyle.FixedSingle;
            txtDireccion.Font = new Font("Segoe UI", 10F);
            txtDireccion.Location = new Point(110, 148);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(905, 30);
            txtDireccion.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(62, 79, 98);
            label7.Location = new Point(20, 150);
            label7.Name = "label7";
            label7.Size = new Size(85, 23);
            label7.TabIndex = 14;
            label7.Text = "Dirección:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.CalendarFont = new Font("Segoe UI", 10F);
            dtpFechaNac.CalendarForeColor = Color.FromArgb(51, 51, 51);
            dtpFechaNac.CalendarMonthBackground = Color.FromArgb(240, 245, 249);
            dtpFechaNac.CalendarTitleBackColor = Color.FromArgb(62, 79, 98);
            dtpFechaNac.CalendarTitleForeColor = Color.White;
            dtpFechaNac.Font = new Font("Segoe UI", 10F);
            dtpFechaNac.Format = DateTimePickerFormat.Short;
            dtpFechaNac.Location = new Point(445, 108);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(230, 30);
            dtpFechaNac.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(62, 79, 98);
            label6.Location = new Point(370, 110);
            label6.Name = "label6";
            label6.Size = new Size(83, 23);
            label6.TabIndex = 12;
            label6.Text = "Fec. Nac.:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(240, 245, 249);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(445, 68);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(230, 30);
            txtEmail.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(62, 79, 98);
            label5.Location = new Point(370, 70);
            label5.Name = "label5";
            label5.Size = new Size(55, 23);
            label5.TabIndex = 10;
            label5.Text = "Email:";
            // 
            // cmbSexo
            // 
            cmbSexo.BackColor = Color.FromArgb(240, 245, 249);
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSexo.FlatStyle = FlatStyle.Flat;
            cmbSexo.Font = new Font("Segoe UI", 10F);
            cmbSexo.FormattingEnabled = true;
            cmbSexo.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cmbSexo.Location = new Point(110, 108);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(230, 31);
            cmbSexo.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(62, 79, 98);
            label4.Location = new Point(20, 110);
            label4.Name = "label4";
            label4.Size = new Size(51, 23);
            label4.TabIndex = 8;
            label4.Text = "Sexo:";
            // 
            // txtCedula
            // 
            txtCedula.BackColor = Color.FromArgb(240, 245, 249);
            txtCedula.BorderStyle = BorderStyle.FixedSingle;
            txtCedula.Font = new Font("Segoe UI", 10F);
            txtCedula.Location = new Point(445, 28);
            txtCedula.MaxLength = 11;
            txtCedula.Name = "txtCedula";
            txtCedula.Size = new Size(230, 30);
            txtCedula.TabIndex = 7;
            txtCedula.KeyPress += txtCedula_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(62, 79, 98);
            label3.Location = new Point(370, 30);
            label3.Name = "label3";
            label3.Size = new Size(67, 23);
            label3.TabIndex = 6;
            label3.Text = "Cédula:";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(240, 245, 249);
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.Font = new Font("Segoe UI", 10F);
            txtApellido.Location = new Point(110, 68);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(230, 30);
            txtApellido.TabIndex = 5;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(62, 79, 98);
            label2.Location = new Point(20, 70);
            label2.Name = "label2";
            label2.Size = new Size(76, 23);
            label2.TabIndex = 4;
            label2.Text = "Apellido:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(240, 245, 249);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(110, 28);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(230, 30);
            txtNombre.TabIndex = 3;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(62, 79, 98);
            lblNombre.Location = new Point(20, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(77, 23);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtID
            // 
            txtID.BackColor = Color.FromArgb(240, 245, 249);
            txtID.BorderStyle = BorderStyle.FixedSingle;
            txtID.Font = new Font("Segoe UI", 9F);
            txtID.Location = new Point(1067, 5);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(60, 27);
            txtID.TabIndex = 1;
            txtID.Visible = false;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblID.ForeColor = Color.FromArgb(15, 76, 117);
            lblID.Location = new Point(1033, 7);
            lblID.Name = "lblID";
            lblID.Size = new Size(28, 20);
            lblID.TabIndex = 0;
            lblID.Text = "ID:";
            lblID.Visible = false;
            // 
            // labelDatosTrabajador
            // 
            labelDatosTrabajador.AutoSize = true;
            labelDatosTrabajador.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelDatosTrabajador.ForeColor = Color.FromArgb(62, 79, 98);
            labelDatosTrabajador.Location = new Point(15, 15);
            labelDatosTrabajador.Name = "labelDatosTrabajador";
            labelDatosTrabajador.Size = new Size(188, 28);
            labelDatosTrabajador.TabIndex = 0;
            labelDatosTrabajador.Text = "Datos personales y laborales";
            // 
            // panelAcciones
            // 
            panelAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelAcciones.Controls.Add(btnLimpiar);
            panelAcciones.Controls.Add(btnEliminar);
            panelAcciones.Controls.Add(btnActualizar);
            panelAcciones.Controls.Add(btnGuardar);
            panelAcciones.Location = new Point(20, 330);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(1160, 60);
            panelAcciones.TabIndex = 1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.None;
            btnLimpiar.BackColor = Color.FromArgb(189, 195, 199);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Image = Properties.Resources.borrador;
            btnLimpiar.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiar.Location = new Point(850, 10);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(140, 40);
            btnLimpiar.TabIndex = 50;
            btnLimpiar.Text = "  Limpiar";
            btnLimpiar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.None;
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Image = Properties.Resources.basura__2_;
            btnEliminar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminar.Location = new Point(670, 10);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(140, 40);
            btnEliminar.TabIndex = 49;
            btnEliminar.Text = "  Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.None;
            btnActualizar.BackColor = Color.FromArgb(33, 150, 243);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 10F);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Image = Properties.Resources.actualizar_flecha;
            btnActualizar.ImageAlign = ContentAlignment.MiddleLeft;
            btnActualizar.Location = new Point(490, 10);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(140, 40);
            btnActualizar.TabIndex = 48;
            btnActualizar.Text = "  Actualizar";
            btnActualizar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.FromArgb(46, 125, 50);
            btnGuardar.BackgroundImageLayout = ImageLayout.None;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Image = Properties.Resources.disquete;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(310, 10);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 40);
            btnGuardar.TabIndex = 47;
            btnGuardar.Text = "  Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // cardTrabajadores
            // 
            cardTrabajadores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cardTrabajadores.BackColor = Color.White;
            cardTrabajadores.Controls.Add(panelBusqueda);
            cardTrabajadores.Controls.Add(panelListado);
            cardTrabajadores.Controls.Add(labelListaTrabajadores);
            cardTrabajadores.Location = new Point(20, 400);
            cardTrabajadores.Name = "cardTrabajadores";
            cardTrabajadores.Size = new Size(1160, 300);
            cardTrabajadores.TabIndex = 2;
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(labelBuscar);
            panelBusqueda.Location = new Point(15, 50);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(1130, 40);
            panelBusqueda.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(33, 150, 243);
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 10F);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(485, 5);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(240, 245, 249);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.Location = new Point(99, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(380, 32);
            txtBuscar.TabIndex = 2;
            // 
            // labelBuscar
            // 
            labelBuscar.AutoSize = true;
            labelBuscar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            labelBuscar.ForeColor = Color.FromArgb(62, 79, 98);
            labelBuscar.Location = new Point(23, 10);
            labelBuscar.Name = "labelBuscar";
            labelBuscar.Size = new Size(74, 25);
            labelBuscar.TabIndex = 1;
            labelBuscar.Text = "Buscar:";
            // 
            // panelListado
            // 
            panelListado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelListado.Controls.Add(dgvTrabajadores);
            panelListado.Location = new Point(15, 96);
            panelListado.Name = "panelListado";
            panelListado.Size = new Size(1130, 219);
            panelListado.TabIndex = 2;
            // 
            // dgvTrabajadores
            // 
            dgvTrabajadores.AllowUserToAddRows = false;
            dgvTrabajadores.AllowUserToDeleteRows = false;
            dgvTrabajadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrabajadores.BackgroundColor = Color.FromArgb(240, 245, 249);
            dgvTrabajadores.BorderStyle = BorderStyle.None;
            dgvTrabajadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrabajadores.Dock = DockStyle.Fill;
            dgvTrabajadores.Location = new Point(0, 0);
            dgvTrabajadores.Name = "dgvTrabajadores";
            dgvTrabajadores.ReadOnly = true;
            dgvTrabajadores.RowHeadersWidth = 62;
            dgvTrabajadores.RowTemplate.Height = 28;
            dgvTrabajadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrabajadores.Size = new Size(1130, 219);
            dgvTrabajadores.TabIndex = 0;
            dgvTrabajadores.CellDoubleClick += dgvTrabajadores_CellDoubleClick;
            // 
            // labelListaTrabajadores
            // 
            labelListaTrabajadores.AutoSize = true;
            labelListaTrabajadores.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelListaTrabajadores.ForeColor = Color.FromArgb(62, 79, 98);
            labelListaTrabajadores.Location = new Point(15, 15);
            labelListaTrabajadores.Name = "labelListaTrabajadores";
            labelListaTrabajadores.Size = new Size(177, 28);
            labelListaTrabajadores.TabIndex = 0;
            labelListaTrabajadores.Text = "Trabajadores registrados";
            // 
            // FrmTrabajadores
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 790);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmTrabajadores";
            Text = "Gestión de Personal";
            Load += FrmTrabajadores_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            cardDatosTrabajador.ResumeLayout(false);
            cardDatosTrabajador.PerformLayout();
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSueldo).EndInit();
            panelAcciones.ResumeLayout(false);
            cardTrabajadores.ResumeLayout(false);
            cardTrabajadores.PerformLayout();
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTrabajadores).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Panel cardDatosTrabajador;
        private System.Windows.Forms.Label labelDatosTrabajador;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSexo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaNac;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbPosicion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudSueldo;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel cardTrabajadores;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.Panel panelListado;
        private System.Windows.Forms.DataGridView dgvTrabajadores;
        private System.Windows.Forms.Label labelListaTrabajadores;
    }
}

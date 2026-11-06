namespace Sistema_Agricola.UI
{
    partial class FrmAgricultor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSector = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEnviarCodigoEmail = new System.Windows.Forms.Button();
            this.txtCodigoEmail = new System.Windows.Forms.TextBox();
            this.btnConfirmarCodigoEmail = new System.Windows.Forms.Button();
            this.lblEstadoEmail = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGenerarCredenciales = new System.Windows.Forms.Button();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new System.Drawing.Point(15, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(276, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Registro Agricultor";
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(752, 12);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(36, 36);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "✕";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.BackColor = System.Drawing.Color.LightYellow;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblInfo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblInfo.Location = new System.Drawing.Point(12, 89);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lblInfo.Size = new System.Drawing.Size(776, 42);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "No hay usuarios registrados en el sistema. Por favor, complete el registro inicial.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblEstadoEmail);
            this.groupBox1.Controls.Add(this.btnConfirmarCodigoEmail);
            this.groupBox1.Controls.Add(this.txtCodigoEmail);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnEnviarCodigoEmail);
            this.groupBox1.Controls.Add(this.txtSector);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCiudad);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDireccion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCedula);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtApellido);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox1.Location = new System.Drawing.Point(12, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.groupBox1.Size = new System.Drawing.Size(776, 280);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "👤 Datos Personales";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(148, 38);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(213, 34);
            this.txtNombre.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(533, 38);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(213, 34);
            this.txtApellido.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Apellido:";
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(148, 87);
            this.txtCedula.MaxLength = 11;
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(213, 34);
            this.txtCedula.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cédula:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(533, 87);
            this.txtTelefono.MaxLength = 11;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(213, 34);
            this.txtTelefono.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "Teléfono:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(148, 136);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(213, 34);
            this.txtEmail.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "Gmail:";
            // 
            // btnEnviarCodigoEmail
            // 
            this.btnEnviarCodigoEmail.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEnviarCodigoEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarCodigoEmail.FlatAppearance.BorderSize = 0;
            this.btnEnviarCodigoEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarCodigoEmail.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnEnviarCodigoEmail.ForeColor = System.Drawing.Color.White;
            this.btnEnviarCodigoEmail.Location = new System.Drawing.Point(367, 136);
            this.btnEnviarCodigoEmail.Name = "btnEnviarCodigoEmail";
            this.btnEnviarCodigoEmail.Size = new System.Drawing.Size(54, 34);
            this.btnEnviarCodigoEmail.TabIndex = 15;
            this.btnEnviarCodigoEmail.Text = "Enviar";
            this.btnEnviarCodigoEmail.UseVisualStyleBackColor = false;
            this.btnEnviarCodigoEmail.Click += new System.EventHandler(this.btnEnviarCodigoEmail_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 188);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 28);
            this.label11.TabIndex = 16;
            this.label11.Text = "Código:";
            // 
            // txtCodigoEmail
            // 
            this.txtCodigoEmail.Location = new System.Drawing.Point(148, 185);
            this.txtCodigoEmail.MaxLength = 6;
            this.txtCodigoEmail.Name = "txtCodigoEmail";
            this.txtCodigoEmail.Size = new System.Drawing.Size(100, 34);
            this.txtCodigoEmail.TabIndex = 17;
            this.txtCodigoEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConfirmarCodigoEmail
            // 
            this.btnConfirmarCodigoEmail.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnConfirmarCodigoEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarCodigoEmail.FlatAppearance.BorderSize = 0;
            this.btnConfirmarCodigoEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarCodigoEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarCodigoEmail.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarCodigoEmail.Location = new System.Drawing.Point(260, 185);
            this.btnConfirmarCodigoEmail.Name = "btnConfirmarCodigoEmail";
            this.btnConfirmarCodigoEmail.Size = new System.Drawing.Size(120, 34);
            this.btnConfirmarCodigoEmail.TabIndex = 18;
            this.btnConfirmarCodigoEmail.Text = "Confirmar";
            this.btnConfirmarCodigoEmail.UseVisualStyleBackColor = false;
            this.btnConfirmarCodigoEmail.Click += new System.EventHandler(this.btnConfirmarCodigoEmail_Click);
            // 
            // lblEstadoEmail
            // 
            this.lblEstadoEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoEmail.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEstadoEmail.Location = new System.Drawing.Point(395, 185);
            this.lblEstadoEmail.Name = "lblEstadoEmail";
            this.lblEstadoEmail.Size = new System.Drawing.Size(351, 34);
            this.lblEstadoEmail.TabIndex = 19;
            this.lblEstadoEmail.Text = "Gmail no verificado";
            this.lblEstadoEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(533, 136);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(213, 34);
            this.txtDireccion.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(427, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 28);
            this.label6.TabIndex = 10;
            this.label6.Text = "Dirección:";
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(148, 235);
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(213, 34);
            this.txtCiudad.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 28);
            this.label7.TabIndex = 12;
            this.label7.Text = "Ciudad:";
            // 
            // txtSector
            // 
            this.txtSector.Location = new System.Drawing.Point(533, 235);
            this.txtSector.Name = "txtSector";
            this.txtSector.Size = new System.Drawing.Size(213, 34);
            this.txtSector.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(427, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 28);
            this.label8.TabIndex = 14;
            this.label8.Text = "Sector:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnGenerarCredenciales);
            this.groupBox2.Controls.Add(this.txtContraseña);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtUsuario);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox2.Location = new System.Drawing.Point(12, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.groupBox2.Size = new System.Drawing.Size(776, 130);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "🔐 Credenciales de Acceso";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(148, 45);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(213, 34);
            this.txtUsuario.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 28);
            this.label9.TabIndex = 0;
            this.label9.Text = "Usuario:";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(533, 45);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.ReadOnly = true;
            this.txtContraseña.Size = new System.Drawing.Size(213, 34);
            this.txtContraseña.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(414, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 28);
            this.label10.TabIndex = 2;
            this.label10.Text = "Contraseña:";
            // 
            // btnGenerarCredenciales
            // 
            this.btnGenerarCredenciales.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnGenerarCredenciales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarCredenciales.FlatAppearance.BorderSize = 0;
            this.btnGenerarCredenciales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarCredenciales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerarCredenciales.ForeColor = System.Drawing.Color.White;
            this.btnGenerarCredenciales.Location = new System.Drawing.Point(239, 85);
            this.btnGenerarCredenciales.Name = "btnGenerarCredenciales";
            this.btnGenerarCredenciales.Size = new System.Drawing.Size(295, 39);
            this.btnGenerarCredenciales.TabIndex = 10;
            this.btnGenerarCredenciales.Text = "⚡ Generar Credenciales";
            this.btnGenerarCredenciales.UseVisualStyleBackColor = false;
            this.btnGenerarCredenciales.Click += new System.EventHandler(this.btnGenerarCredenciales_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(239, 588);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 50);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(394, 588);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 50);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "❌ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmRegistroAgricultor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 245);
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRegistroAgricultor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Agricultor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEnviarCodigoEmail;
        private System.Windows.Forms.TextBox txtCodigoEmail;
        private System.Windows.Forms.Button btnConfirmarCodigoEmail;
        private System.Windows.Forms.Label lblEstadoEmail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerarCredenciales;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

namespace Sistema_Agricola.UI
{
    partial class FrmRecuperarContrasena
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
            panelSuperior = new Panel();
            lblTitulo = new Label();
            lblUsuarioCorreo = new Label();
            txtUsuarioCorreo = new TextBox();
            btnEnviarCodigo = new Button();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNuevaContrasena = new Label();
            txtNuevaContrasena = new TextBox();
            lblConfirmarContrasena = new Label();
            txtConfirmarContrasena = new TextBox();
            chkMostrar = new CheckBox();
            btnActualizar = new Button();
            btnCancelar = new Button();
            lblMensaje = new Label();
            lblAyuda = new Label();
            panelSuperior.SuspendLayout();
            SuspendLayout();
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(55, 135, 78);
            panelSuperior.Controls.Add(lblTitulo);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(548, 64);
            panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(24, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(247, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Recuperar contraseña";
            // 
            // lblUsuarioCorreo
            // 
            lblUsuarioCorreo.AutoSize = true;
            lblUsuarioCorreo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuarioCorreo.Location = new Point(32, 94);
            lblUsuarioCorreo.Name = "lblUsuarioCorreo";
            lblUsuarioCorreo.Size = new Size(122, 19);
            lblUsuarioCorreo.TabIndex = 1;
            lblUsuarioCorreo.Text = "Usuario o correo";
            // 
            // txtUsuarioCorreo
            // 
            txtUsuarioCorreo.BorderStyle = BorderStyle.FixedSingle;
            txtUsuarioCorreo.Font = new Font("Segoe UI", 10F);
            txtUsuarioCorreo.Location = new Point(32, 119);
            txtUsuarioCorreo.MaxLength = 100;
            txtUsuarioCorreo.Name = "txtUsuarioCorreo";
            txtUsuarioCorreo.Size = new Size(318, 25);
            txtUsuarioCorreo.TabIndex = 2;
            // 
            // btnEnviarCodigo
            // 
            btnEnviarCodigo.BackColor = Color.FromArgb(55, 135, 78);
            btnEnviarCodigo.FlatStyle = FlatStyle.Flat;
            btnEnviarCodigo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEnviarCodigo.ForeColor = Color.White;
            btnEnviarCodigo.Location = new Point(366, 116);
            btnEnviarCodigo.Name = "btnEnviarCodigo";
            btnEnviarCodigo.Size = new Size(142, 31);
            btnEnviarCodigo.TabIndex = 3;
            btnEnviarCodigo.Text = "Enviar código";
            btnEnviarCodigo.UseVisualStyleBackColor = false;
            btnEnviarCodigo.Click += btnEnviarCodigo_Click;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCodigo.Location = new Point(32, 189);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(55, 19);
            lblCodigo.TabIndex = 4;
            lblCodigo.Text = "Código";
            // 
            // txtCodigo
            // 
            txtCodigo.BorderStyle = BorderStyle.FixedSingle;
            txtCodigo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtCodigo.Location = new Point(32, 214);
            txtCodigo.MaxLength = 6;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(146, 29);
            txtCodigo.TabIndex = 5;
            txtCodigo.TextAlign = HorizontalAlignment.Center;
            txtCodigo.KeyPress += txtCodigo_KeyPress;
            // 
            // lblNuevaContrasena
            // 
            lblNuevaContrasena.AutoSize = true;
            lblNuevaContrasena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNuevaContrasena.Location = new Point(32, 264);
            lblNuevaContrasena.Name = "lblNuevaContrasena";
            lblNuevaContrasena.Size = new Size(129, 19);
            lblNuevaContrasena.TabIndex = 6;
            lblNuevaContrasena.Text = "Nueva contraseña";
            // 
            // txtNuevaContrasena
            // 
            txtNuevaContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtNuevaContrasena.Font = new Font("Segoe UI", 10F);
            txtNuevaContrasena.Location = new Point(32, 289);
            txtNuevaContrasena.MaxLength = 128;
            txtNuevaContrasena.Name = "txtNuevaContrasena";
            txtNuevaContrasena.PasswordChar = '*';
            txtNuevaContrasena.Size = new Size(218, 25);
            txtNuevaContrasena.TabIndex = 7;
            // 
            // lblConfirmarContrasena
            // 
            lblConfirmarContrasena.AutoSize = true;
            lblConfirmarContrasena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmarContrasena.Location = new Point(290, 264);
            lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            lblConfirmarContrasena.Size = new Size(152, 19);
            lblConfirmarContrasena.TabIndex = 8;
            lblConfirmarContrasena.Text = "Confirmar contraseña";
            // 
            // txtConfirmarContrasena
            // 
            txtConfirmarContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmarContrasena.Font = new Font("Segoe UI", 10F);
            txtConfirmarContrasena.Location = new Point(290, 289);
            txtConfirmarContrasena.MaxLength = 128;
            txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            txtConfirmarContrasena.PasswordChar = '*';
            txtConfirmarContrasena.Size = new Size(218, 25);
            txtConfirmarContrasena.TabIndex = 9;
            // 
            // chkMostrar
            // 
            chkMostrar.AutoSize = true;
            chkMostrar.Font = new Font("Segoe UI", 9F);
            chkMostrar.Location = new Point(32, 326);
            chkMostrar.Name = "chkMostrar";
            chkMostrar.Size = new Size(137, 19);
            chkMostrar.TabIndex = 10;
            chkMostrar.Text = "Mostrar contraseña";
            chkMostrar.UseVisualStyleBackColor = true;
            chkMostrar.CheckedChanged += chkMostrar_CheckedChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(55, 135, 78);
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(300, 367);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(116, 34);
            btnActualizar.TabIndex = 11;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(232, 235, 238);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(422, 367);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(86, 34);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 9F);
            lblMensaje.Location = new Point(32, 151);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(476, 32);
            lblMensaje.TabIndex = 13;
            lblMensaje.Text = "Mensaje";
            lblMensaje.Visible = false;
            // 
            // lblAyuda
            // 
            lblAyuda.Font = new Font("Segoe UI", 9F);
            lblAyuda.ForeColor = Color.DimGray;
            lblAyuda.Location = new Point(190, 207);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(318, 42);
            lblAyuda.TabIndex = 14;
            lblAyuda.Text = "Revise su correo Gmail e ingrese el código de 6 dígitos recibido.";
            // 
            // FrmRecuperarContrasena
            // 
            AcceptButton = btnEnviarCodigo;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancelar;
            ClientSize = new Size(548, 424);
            Controls.Add(lblAyuda);
            Controls.Add(lblMensaje);
            Controls.Add(btnCancelar);
            Controls.Add(btnActualizar);
            Controls.Add(chkMostrar);
            Controls.Add(txtConfirmarContrasena);
            Controls.Add(lblConfirmarContrasena);
            Controls.Add(txtNuevaContrasena);
            Controls.Add(lblNuevaContrasena);
            Controls.Add(txtCodigo);
            Controls.Add(lblCodigo);
            Controls.Add(btnEnviarCodigo);
            Controls.Add(txtUsuarioCorreo);
            Controls.Add(lblUsuarioCorreo);
            Controls.Add(panelSuperior);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRecuperarContrasena";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Recuperar contraseña";
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelSuperior;
        private Label lblTitulo;
        private Label lblUsuarioCorreo;
        private TextBox txtUsuarioCorreo;
        private Button btnEnviarCodigo;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblNuevaContrasena;
        private TextBox txtNuevaContrasena;
        private Label lblConfirmarContrasena;
        private TextBox txtConfirmarContrasena;
        private CheckBox chkMostrar;
        private Button btnActualizar;
        private Button btnCancelar;
        private Label lblMensaje;
        private Label lblAyuda;
    }
}

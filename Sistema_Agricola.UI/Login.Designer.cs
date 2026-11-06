namespace Sistema_Agricola.UI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            BtnCerrar = new PictureBox();
            BtnMinimizar = new PictureBox();
            label1 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            txtuser = new TextBox();
            txtpass = new TextBox();
            btningresar = new Button();
            linkLabel1 = new LinkLabel();
            btnvista = new PictureBox();
            lblMessageError = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnMinimizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnvista).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(232, 332);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(120, 250, 115);
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(232, 287);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            // 
            // BtnCerrar
            // 
            BtnCerrar.BackColor = SystemColors.Control;
            BtnCerrar.Cursor = Cursors.Hand;
            BtnCerrar.Image = (Image)resources.GetObject("BtnCerrar.Image");
            BtnCerrar.Location = new Point(582, 0);
            BtnCerrar.Name = "BtnCerrar";
            BtnCerrar.Size = new Size(27, 20);
            BtnCerrar.SizeMode = PictureBoxSizeMode.Zoom;
            BtnCerrar.TabIndex = 1;
            BtnCerrar.TabStop = false;
            BtnCerrar.Click += BtnCerrar_Click;
            // 
            // BtnMinimizar
            // 
            BtnMinimizar.Cursor = Cursors.Hand;
            BtnMinimizar.Image = (Image)resources.GetObject("BtnMinimizar.Image");
            BtnMinimizar.Location = new Point(544, 0);
            BtnMinimizar.Name = "BtnMinimizar";
            BtnMinimizar.Size = new Size(32, 20);
            BtnMinimizar.SizeMode = PictureBoxSizeMode.Zoom;
            BtnMinimizar.TabIndex = 2;
            BtnMinimizar.TabStop = false;
            BtnMinimizar.Click += BtnMinimizar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(305, 9);
            label1.Name = "label1";
            label1.Size = new Size(180, 27);
            label1.TabIndex = 3;
            label1.Text = "INICIAR SESIÓN";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Location = new Point(276, 145);
            panel2.Name = "panel2";
            panel2.Size = new Size(241, 2);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(276, 89);
            panel3.Name = "panel3";
            panel3.Size = new Size(241, 2);
            panel3.TabIndex = 5;
            // 
            // txtuser
            // 
            txtuser.BorderStyle = BorderStyle.None;
            txtuser.Cursor = Cursors.IBeam;
            txtuser.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            txtuser.ForeColor = SystemColors.WindowFrame;
            txtuser.Location = new Point(276, 69);
            txtuser.Name = "txtuser";
            txtuser.Size = new Size(241, 20);
            txtuser.TabIndex = 1;
            txtuser.Text = "Usuario";
            txtuser.Enter += txtuser_Enter;
            txtuser.Leave += txtuser_Leave;
            // 
            // txtpass
            // 
            txtpass.BorderStyle = BorderStyle.None;
            txtpass.Cursor = Cursors.IBeam;
            txtpass.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            txtpass.ForeColor = SystemColors.WindowFrame;
            txtpass.Location = new Point(276, 125);
            txtpass.Name = "txtpass";
            txtpass.Size = new Size(241, 20);
            txtpass.TabIndex = 2;
            txtpass.Text = "Contraseña";
            txtpass.Enter += txtpass_Enter;
            txtpass.Leave += txtpass_Leave;
            // 
            // btningresar
            // 
            btningresar.BackColor = Color.FromArgb(224, 224, 224);
            btningresar.Cursor = Cursors.Hand;
            btningresar.FlatAppearance.BorderColor = Color.Red;
            btningresar.FlatAppearance.MouseDownBackColor = Color.Red;
            btningresar.FlatAppearance.MouseOverBackColor = Color.Red;
            btningresar.FlatStyle = FlatStyle.Popup;
            btningresar.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            btningresar.ForeColor = SystemColors.ActiveCaptionText;
            btningresar.Location = new Point(353, 196);
            btningresar.Name = "btningresar";
            btningresar.Size = new Size(88, 34);
            btningresar.TabIndex = 3;
            btningresar.Text = "Ingresar";
            btningresar.UseVisualStyleBackColor = false;
            btningresar.Click += btningresar_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Century Gothic", 8.25F);
            linkLabel1.LinkColor = Color.Black;
            linkLabel1.Location = new Point(335, 256);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(137, 16);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Olvidé mi contraseña";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // btnvista
            // 
            btnvista.Cursor = Cursors.Hand;
            btnvista.Image = Properties.Resources.ojo;
            btnvista.Location = new Point(536, 125);
            btnvista.Name = "btnvista";
            btnvista.Size = new Size(50, 22);
            btnvista.SizeMode = PictureBoxSizeMode.Zoom;
            btnvista.TabIndex = 10;
            btnvista.TabStop = false;
            btnvista.Click += btnvista_Click;
            // 
            // lblMessageError
            // 
            lblMessageError.AutoSize = true;
            lblMessageError.Font = new Font("MS Reference Sans Serif", 10.5F);
            lblMessageError.ForeColor = SystemColors.ActiveCaptionText;
            lblMessageError.Location = new Point(276, 157);
            lblMessageError.Name = "lblMessageError";
            lblMessageError.Size = new Size(109, 18);
            lblMessageError.TabIndex = 11;
            lblMessageError.Text = "Error Message";
            lblMessageError.Visible = false;
            // 
            // Login
            // 
            AcceptButton = btningresar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(613, 287);
            Controls.Add(lblMessageError);
            Controls.Add(btnvista);
            Controls.Add(linkLabel1);
            Controls.Add(btningresar);
            Controls.Add(txtpass);
            Controls.Add(txtuser);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(BtnMinimizar);
            Controls.Add(BtnCerrar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            Opacity = 0.9D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Login_Load;
            MouseDown += Login_MouseDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnMinimizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnvista).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox BtnCerrar;
        private PictureBox BtnMinimizar;
        private Label label1;
        private Panel panel2;
        private Panel panel3;
        private TextBox txtuser;
        private TextBox txtpass;
        private Button btningresar;
        private LinkLabel linkLabel1;
        private PictureBox btnvista;
        private Label lblMessageError;
    }
}

namespace Sistema_Agricola.UI
{
    partial class FrmComprobarCompatibilidad
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
            btnCerrar = new Button();
            lblTituloTerreno = new Label();
            panel2 = new Panel();
            btnComprobar = new Button();
            groupBox2 = new GroupBox();
            dtpFechaSiembra = new DateTimePicker();
            label5 = new Label();
            txtVariedad = new TextBox();
            label4 = new Label();
            cmbCategoriaCultivo = new ComboBox();
            label3 = new Label();
            cmbTipoCultivo = new ComboBox();
            label2 = new Label();
            cmbCultivos = new ComboBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            lblPendiente = new Label();
            lblHumedad = new Label();
            lblPH = new Label();
            lblArea = new Label();
            lblTipoSuelo = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(245, 247, 250);
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(lblTituloTerreno);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2, 2, 2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(622, 60);
            panel1.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(231, 76, 60);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(575, 15);
            btnCerrar.Margin = new Padding(2, 2, 2, 2);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(35, 30);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "✕";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblTituloTerreno
            // 
            lblTituloTerreno.AutoSize = true;
            lblTituloTerreno.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloTerreno.ForeColor = Color.FromArgb(52, 73, 94);
            lblTituloTerreno.Location = new Point(15, 17);
            lblTituloTerreno.Margin = new Padding(2, 0, 2, 0);
            lblTituloTerreno.Name = "lblTituloTerreno";
            lblTituloTerreno.Size = new Size(285, 25);
            lblTituloTerreno.TabIndex = 0;
            lblTituloTerreno.Text = "Terreno: [Nombre del Terreno]";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(250, 250, 250);
            panel2.Controls.Add(btnComprobar);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 60);
            panel2.Margin = new Padding(2, 2, 2, 2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15);
            panel2.Size = new Size(622, 390);
            panel2.TabIndex = 1;
            // 
            // btnComprobar
            // 
            btnComprobar.BackColor = Color.FromArgb(46, 204, 113);
            btnComprobar.Cursor = Cursors.Hand;
            btnComprobar.FlatAppearance.BorderSize = 0;
            btnComprobar.FlatStyle = FlatStyle.Flat;
            btnComprobar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnComprobar.ForeColor = Color.White;
            btnComprobar.Location = new Point(170, 334);
            btnComprobar.Margin = new Padding(2, 2, 2, 2);
            btnComprobar.Name = "btnComprobar";
            btnComprobar.Size = new Size(280, 42);
            btnComprobar.TabIndex = 2;
            btnComprobar.Text = "Evaluar condiciones";
            btnComprobar.UseVisualStyleBackColor = false;
            btnComprobar.Click += btnComprobar_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(dtpFechaSiembra);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtVariedad);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(cmbCategoriaCultivo);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cmbTipoCultivo);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(cmbCultivos);
            groupBox2.Controls.Add(label1);
            groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.ForeColor = Color.FromArgb(52, 73, 94);
            groupBox2.Location = new Point(309, 14);
            groupBox2.Margin = new Padding(2, 2, 2, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(10);
            groupBox2.Size = new Size(303, 306);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Información del Cultivo";
            // 
            // dtpFechaSiembra
            // 
            dtpFechaSiembra.Font = new Font("Segoe UI", 9F);
            dtpFechaSiembra.Format = DateTimePickerFormat.Short;
            dtpFechaSiembra.Location = new Point(129, 132);
            dtpFechaSiembra.Name = "dtpFechaSiembra";
            dtpFechaSiembra.Size = new Size(171, 27);
            dtpFechaSiembra.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(15, 135);
            label5.Name = "label5";
            label5.Size = new Size(52, 19);
            label5.TabIndex = 8;
            label5.Text = "Fecha:";
            // 
            // txtVariedad
            // 
            txtVariedad.Font = new Font("Segoe UI", 9F);
            txtVariedad.Location = new Point(129, 92);
            txtVariedad.MaxLength = 80;
            txtVariedad.Name = "txtVariedad";
            txtVariedad.Size = new Size(171, 27);
            txtVariedad.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(15, 95);
            label4.Name = "label4";
            label4.Size = new Size(67, 19);
            label4.TabIndex = 6;
            label4.Text = "Variedad:";
            // 
            // cmbCategoriaCultivo
            // 
            cmbCategoriaCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoriaCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbCategoriaCultivo.FormattingEnabled = true;
            cmbCategoriaCultivo.Items.AddRange(new object[] { "Comestible", "Ornamental", "Industrial", "Medicinal" });
            cmbCategoriaCultivo.Location = new Point(129, 242);
            cmbCategoriaCultivo.Margin = new Padding(2, 2, 2, 2);
            cmbCategoriaCultivo.Name = "cmbCategoriaCultivo";
            cmbCategoriaCultivo.Size = new Size(171, 25);
            cmbCategoriaCultivo.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(15, 244);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(71, 19);
            label3.TabIndex = 4;
            label3.Text = "Categoría:";
            // 
            // cmbTipoCultivo
            // 
            cmbTipoCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbTipoCultivo.FormattingEnabled = true;
            cmbTipoCultivo.Items.AddRange(new object[] { "Orgánica", "Hibrida", "Transgenica" });
            cmbTipoCultivo.Location = new Point(129, 192);
            cmbTipoCultivo.Margin = new Padding(2, 2, 2, 2);
            cmbTipoCultivo.Name = "cmbTipoCultivo";
            cmbTipoCultivo.Size = new Size(171, 25);
            cmbTipoCultivo.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 194);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 19);
            label2.TabIndex = 2;
            label2.Text = "Tipo de cultivo:";
            // 
            // cmbCultivos
            // 
            cmbCultivos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCultivos.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbCultivos.FormattingEnabled = true;
            cmbCultivos.Location = new Point(129, 52);
            cmbCultivos.Margin = new Padding(2, 2, 2, 2);
            cmbCultivos.Name = "cmbCultivos";
            cmbCultivos.Size = new Size(171, 25);
            cmbCultivos.TabIndex = 1;
            cmbCultivos.SelectedIndexChanged += cmbCultivos_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 55);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 19);
            label1.TabIndex = 0;
            label1.Text = "Cultivo:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(lblPendiente);
            groupBox1.Controls.Add(lblHumedad);
            groupBox1.Controls.Add(lblPH);
            groupBox1.Controls.Add(lblArea);
            groupBox1.Controls.Add(lblTipoSuelo);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.FromArgb(52, 73, 94);
            groupBox1.Location = new Point(9, 14);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(10);
            groupBox1.Size = new Size(295, 306);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información del Terreno";
            // 
            // lblPendiente
            // 
            lblPendiente.AutoSize = true;
            lblPendiente.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPendiente.Location = new Point(16, 152);
            lblPendiente.Margin = new Padding(2, 0, 2, 0);
            lblPendiente.Name = "lblPendiente";
            lblPendiente.Size = new Size(106, 19);
            lblPendiente.TabIndex = 4;
            lblPendiente.Text = "Pendiente: 0.0%";
            // 
            // lblHumedad
            // 
            lblHumedad.AutoSize = true;
            lblHumedad.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHumedad.Location = new Point(16, 123);
            lblHumedad.Margin = new Padding(2, 0, 2, 0);
            lblHumedad.Name = "lblHumedad";
            lblHumedad.Size = new Size(106, 19);
            lblHumedad.TabIndex = 3;
            lblHumedad.Text = "Humedad: 0.0%";
            // 
            // lblPH
            // 
            lblPH.AutoSize = true;
            lblPH.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPH.Location = new Point(16, 94);
            lblPH.Margin = new Padding(2, 0, 2, 0);
            lblPH.Name = "lblPH";
            lblPH.Size = new Size(53, 19);
            lblPH.TabIndex = 2;
            lblPH.Text = "pH: 7.0";
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblArea.Location = new Point(16, 66);
            lblArea.Margin = new Padding(2, 0, 2, 0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(209, 19);
            lblArea.TabIndex = 1;
            lblArea.Text = "Área: 0.00 m² (0.0000 hectáreas)";
            // 
            // lblTipoSuelo
            // 
            lblTipoSuelo.AutoSize = true;
            lblTipoSuelo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTipoSuelo.Location = new Point(16, 38);
            lblTipoSuelo.Margin = new Padding(2, 0, 2, 0);
            lblTipoSuelo.Name = "lblTipoSuelo";
            lblTipoSuelo.Size = new Size(112, 19);
            lblTipoSuelo.TabIndex = 0;
            lblTipoSuelo.Text = "Tipo de Suelo: [?]";
            // 
            // FrmComprobarCompatibilidad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 2, 2, 2);
            Name = "FrmComprobarCompatibilidad";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Comprobar Compatibilidad";
            Load += FrmComprobarCompatibilidad_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblTituloTerreno;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnComprobar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbCategoriaCultivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipoCultivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCultivos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPendiente;
        private System.Windows.Forms.Label lblHumedad;
        private System.Windows.Forms.Label lblPH;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblTipoSuelo;
        private System.Windows.Forms.TextBox txtVariedad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaSiembra;
        private System.Windows.Forms.Label label5;
    }
}

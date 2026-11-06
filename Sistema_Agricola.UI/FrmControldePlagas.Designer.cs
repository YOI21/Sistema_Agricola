namespace Sistema_Agricola.UI
{
    partial class FrmControldePlagas
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
            DGV_Control_Plagas = new DataGridView();
            panel1 = new Panel();
            btnVolver = new Button();
            label1 = new Label();
            panel2 = new Panel();
            button1 = new Button();
            btnActualizar = new Button();
            btnGuardar = new Button();
            label16 = new Label();
            txtCura = new TextBox();
            txtNombreC = new TextBox();
            txtHuesped = new TextBox();
            label10 = new Label();
            txtRazgos = new TextBox();
            label9 = new Label();
            txtNombre_Cienti = new TextBox();
            label8 = new Label();
            txtBuscarP = new TextBox();
            btnLimpiar = new Button();
            txtTipodePlaga = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)DGV_Control_Plagas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // DGV_Control_Plagas
            // 
            DGV_Control_Plagas.AllowUserToAddRows = false;
            DGV_Control_Plagas.AllowUserToDeleteRows = false;
            DGV_Control_Plagas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Control_Plagas.BackgroundColor = Color.White;
            DGV_Control_Plagas.BorderStyle = BorderStyle.None;
            DGV_Control_Plagas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DGV_Control_Plagas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DGV_Control_Plagas.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(52, 73, 94),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(52, 73, 94),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            DGV_Control_Plagas.ColumnHeadersHeight = 40;
            DGV_Control_Plagas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGV_Control_Plagas.Cursor = Cursors.Hand;
            DGV_Control_Plagas.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9.75F),
                ForeColor = Color.FromArgb(52, 73, 94),
                SelectionBackColor = Color.FromArgb(52, 152, 219),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };
            DGV_Control_Plagas.EnableHeadersVisualStyles = false;
            DGV_Control_Plagas.GridColor = Color.FromArgb(229, 229, 229);
            DGV_Control_Plagas.Location = new Point(26, 136);
            DGV_Control_Plagas.Name = "DGV_Control_Plagas";
            DGV_Control_Plagas.ReadOnly = true;
            DGV_Control_Plagas.RowHeadersVisible = false;
            DGV_Control_Plagas.RowHeadersWidth = 62;
            DGV_Control_Plagas.RowTemplate.Height = 35;
            DGV_Control_Plagas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Control_Plagas.Size = new Size(1100, 134);
            DGV_Control_Plagas.TabIndex = 67;
            DGV_Control_Plagas.CellContentClick += DGV_Control_Plagas_CellContentClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(245, 247, 250);
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 60);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(231, 76, 60);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1130, 15);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(50, 30);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "✕";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(52, 73, 94);
            label1.Location = new Point(20, 17);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(195, 30);
            label1.TabIndex = 0;
            label1.Text = "Control de Plagas";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.None;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(btnActualizar);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(txtCura);
            panel2.Controls.Add(DGV_Control_Plagas);
            panel2.Controls.Add(txtNombreC);
            panel2.Controls.Add(txtHuesped);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(txtRazgos);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(txtNombre_Cienti);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(txtBuscarP);
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(txtTipodePlaga);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(20, 80);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20);
            panel2.Size = new Size(1160, 570);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.FromArgb(241, 196, 15);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(943, 68);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(135, 43);
            button1.TabIndex = 72;
            button1.Text = "Limpiar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.None;
            btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(796, 68);
            btnActualizar.Margin = new Padding(2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(135, 43);
            btnActualizar.TabIndex = 71;
            btnActualizar.Text = "Recomendar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.BackgroundImageLayout = ImageLayout.None;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(652, 68);
            btnGuardar.Margin = new Padding(2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(135, 43);
            btnGuardar.TabIndex = 70;
            btnGuardar.Text = "Buscar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click_1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label16.Location = new Point(26, 274);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new Size(49, 21);
            label16.TabIndex = 69;
            label16.Text = "Cura:";
            // 
            // txtCura
            // 
            txtCura.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCura.Location = new Point(19, 305);
            txtCura.Margin = new Padding(2);
            txtCura.Multiline = true;
            txtCura.Name = "txtCura";
            txtCura.RightToLeft = RightToLeft.No;
            txtCura.ScrollBars = ScrollBars.Vertical;
            txtCura.Size = new Size(1060, 259);
            txtCura.TabIndex = 68;
            txtCura.TabStop = false;
            txtCura.WordWrap = true;
            // 
            // txtNombreC
            // 
            txtNombreC.Location = new Point(90, 102);
            txtNombreC.Margin = new Padding(2);
            txtNombreC.Name = "txtNombreC";
            txtNombreC.Size = new Size(143, 23);
            txtNombreC.TabIndex = 66;
            // 
            // txtHuesped
            // 
            txtHuesped.Location = new Point(424, 49);
            txtHuesped.Margin = new Padding(2);
            txtHuesped.Name = "txtHuesped";
            txtHuesped.Size = new Size(172, 23);
            txtHuesped.TabIndex = 65;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label10.Location = new Point(258, 47);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(81, 21);
            label10.TabIndex = 64;
            label10.Text = "Huésped:";
            // 
            // txtRazgos
            // 
            txtRazgos.Location = new Point(655, 13);
            txtRazgos.Margin = new Padding(2);
            txtRazgos.Name = "txtRazgos";
            txtRazgos.Size = new Size(205, 23);
            txtRazgos.TabIndex = 63;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.Location = new Point(579, 8);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(68, 21);
            label9.TabIndex = 62;
            label9.Text = "Rasgos:";
            // 
            // txtNombre_Cienti
            // 
            txtNombre_Cienti.Location = new Point(424, 102);
            txtNombre_Cienti.Margin = new Padding(2);
            txtNombre_Cienti.Name = "txtNombre_Cienti";
            txtNombre_Cienti.Size = new Size(172, 23);
            txtNombre_Cienti.TabIndex = 61;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label8.Location = new Point(258, 100);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(155, 21);
            label8.TabIndex = 60;
            label8.Text = "Nombre científico:";
            // 
            // txtBuscarP
            // 
            txtBuscarP.Location = new Point(249, 11);
            txtBuscarP.Margin = new Padding(2);
            txtBuscarP.Name = "txtBuscarP";
            txtBuscarP.Size = new Size(234, 23);
            txtBuscarP.TabIndex = 59;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(52, 152, 219);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(486, 10);
            btnLimpiar.Margin = new Padding(2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(78, 25);
            btnLimpiar.TabIndex = 45;
            btnLimpiar.Text = "🔍 Buscar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtTipodePlaga
            // 
            txtTipodePlaga.Location = new Point(55, 49);
            txtTipodePlaga.Margin = new Padding(2);
            txtTipodePlaga.Name = "txtTipodePlaga";
            txtTipodePlaga.Size = new Size(191, 23);
            txtTipodePlaga.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(6, 100);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 21);
            label4.TabIndex = 2;
            label4.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(3, 47);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(48, 21);
            label3.TabIndex = 1;
            label3.Text = "Tipo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(2, 11);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(231, 21);
            label2.TabIndex = 0;
            label2.Text = "Buscar:";
            // 
            // FrmControldePlagas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 670);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "FrmControldePlagas";
            Text = "FrmControldePlagas";
            ((System.ComponentModel.ISupportInitialize)DGV_Control_Plagas).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label3;
        private Label label2;
        private Label label4;
        private TextBox txtTipodePlaga;
        private Button btnVolver;
        private Button btnLimpiar;
        private TextBox txtBuscarP;
        private DataGridView DGV_Control_Plagas;
        private TextBox txtNombreC;
        private TextBox txtHuesped;
        private Label label10;
        private TextBox txtRazgos;
        private Label label9;
        private TextBox txtNombre_Cienti;
        private Label label8;
        private Label label16;
        private TextBox txtCura;
        private Button button1;
        private Button btnActualizar;
        private Button btnGuardar;
    }
}

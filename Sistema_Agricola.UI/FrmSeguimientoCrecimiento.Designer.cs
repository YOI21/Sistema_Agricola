namespace Sistema_Agricola.UI
{
    partial class FrmSeguimientoCrecimiento
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
            dgvSeguimiento = new DataGridView();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            cbxEstadoSalud = new ComboBox();
            cbxEtapaCrecimiento = new ComboBox();
            txtAccionesTomadas = new TextBox();
            label12 = new Label();
            txtProblemasDetectados = new TextBox();
            label11 = new Label();
            numPorcentajeCrecimiento = new NumericUpDown();
            label10 = new Label();
            txtObservaciones = new TextBox();
            label9 = new Label();
            label8 = new Label();
            numNumHojas = new NumericUpDown();
            label7 = new Label();
            numAlturaPlanta = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            dtpFechaRegistro = new DateTimePicker();
            label4 = new Label();
            label3 = new Label();
            cbxCultivo = new ComboBox();
            txtIDSeguimiento = new TextBox();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSeguimiento).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPorcentajeCrecimiento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNumHojas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAlturaPlanta).BeginInit();
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
            panel1.Size = new Size(1333, 88);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnVolver.BackgroundImage = Properties.Resources._61449;
            btnVolver.BackgroundImageLayout = ImageLayout.Zoom;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            btnVolver.Location = new Point(1289, 6);
            btnVolver.Margin = new Padding(4, 6, 4, 6);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(40, 41);
            btnVolver.TabIndex = 8;
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            label1.Location = new Point(468, 11);
            label1.Name = "label1";
            label1.Size = new Size(410, 48);
            label1.TabIndex = 0;
            label1.Text = "Seguimiento de Crecimiento";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnActualizar);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(dgvSeguimiento);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Location = new Point(13, 95);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1307, 842);
            panel2.TabIndex = 1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Image = Properties.Resources.borrador;
            btnLimpiar.ImageAlign = ContentAlignment.MiddleLeft;
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
            btnActualizar.Image = Properties.Resources.actualizar_flecha;
            btnActualizar.ImageAlign = ContentAlignment.MiddleLeft;
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
            btnGuardar.Image = Properties.Resources.disquete;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
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
            // dgvSeguimiento
            // 
            dgvSeguimiento.AllowUserToAddRows = false;
            dgvSeguimiento.AllowUserToDeleteRows = false;
            dgvSeguimiento.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSeguimiento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSeguimiento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSeguimiento.Location = new Point(17, 512);
            dgvSeguimiento.Margin = new Padding(3, 4, 3, 4);
            dgvSeguimiento.Name = "dgvSeguimiento";
            dgvSeguimiento.ReadOnly = true;
            dgvSeguimiento.RowHeadersWidth = 62;
            dgvSeguimiento.RowTemplate.Height = 28;
            dgvSeguimiento.Size = new Size(1273, 312);
            dgvSeguimiento.TabIndex = 2;
            dgvSeguimiento.CellClick += dgvSeguimiento_CellClick;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            groupBox2.Location = new Point(722, 25);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(568, 430);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gráfico de Crecimiento";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbxEstadoSalud);
            groupBox1.Controls.Add(cbxEtapaCrecimiento);
            groupBox1.Controls.Add(txtAccionesTomadas);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(txtProblemasDetectados);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(numPorcentajeCrecimiento);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(txtObservaciones);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(numNumHojas);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(numAlturaPlanta);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dtpFechaRegistro);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cbxCultivo);
            groupBox1.Controls.Add(txtIDSeguimiento);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            groupBox1.Location = new Point(17, 25);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(684, 430);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de Seguimiento";
            // 
            // cbxEstadoSalud
            // 
            cbxEstadoSalud.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstadoSalud.Font = new Font("Segoe UI", 10F);
            cbxEstadoSalud.FormattingEnabled = true;
            cbxEstadoSalud.Items.AddRange(new object[] { "Excelente", "Bueno", "Regular", "Malo" });
            cbxEstadoSalud.Location = new Point(201, 247);
            cbxEstadoSalud.Margin = new Padding(3, 4, 3, 4);
            cbxEstadoSalud.Name = "cbxEstadoSalud";
            cbxEstadoSalud.Size = new Size(183, 36);
            cbxEstadoSalud.TabIndex = 22;
            // 
            // cbxEtapaCrecimiento
            // 
            cbxEtapaCrecimiento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEtapaCrecimiento.Font = new Font("Segoe UI", 10F);
            cbxEtapaCrecimiento.FormattingEnabled = true;
            cbxEtapaCrecimiento.Items.AddRange(new object[] { "Germinación", "Desarrollo vegetativo", "Floración", "Fructificación", "Maduración" });
            cbxEtapaCrecimiento.Location = new Point(201, 150);
            cbxEtapaCrecimiento.Margin = new Padding(3, 4, 3, 4);
            cbxEtapaCrecimiento.Name = "cbxEtapaCrecimiento";
            cbxEtapaCrecimiento.Size = new Size(222, 36);
            cbxEtapaCrecimiento.TabIndex = 21;
            // 
            // txtAccionesTomadas
            // 
            txtAccionesTomadas.Font = new Font("Segoe UI", 10F);
            txtAccionesTomadas.Location = new Point(218, 382);
            txtAccionesTomadas.Margin = new Padding(3, 4, 3, 4);
            txtAccionesTomadas.Name = "txtAccionesTomadas";
            txtAccionesTomadas.Size = new Size(448, 34);
            txtAccionesTomadas.TabIndex = 20;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F);
            label12.Location = new Point(22, 388);
            label12.Name = "label12";
            label12.Size = new Size(175, 28);
            label12.TabIndex = 19;
            label12.Text = "Acciones Tomadas:";
            // 
            // txtProblemasDetectados
            // 
            txtProblemasDetectados.Font = new Font("Segoe UI", 10F);
            txtProblemasDetectados.Location = new Point(240, 332);
            txtProblemasDetectados.Margin = new Padding(3, 4, 3, 4);
            txtProblemasDetectados.Name = "txtProblemasDetectados";
            txtProblemasDetectados.Size = new Size(426, 34);
            txtProblemasDetectados.TabIndex = 18;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F);
            label11.Location = new Point(22, 338);
            label11.Name = "label11";
            label11.Size = new Size(212, 28);
            label11.TabIndex = 17;
            label11.Text = "Problemas Detectados:";
            // 
            // numPorcentajeCrecimiento
            // 
            numPorcentajeCrecimiento.Font = new Font("Segoe UI", 10F);
            numPorcentajeCrecimiento.Location = new Point(563, 248);
            numPorcentajeCrecimiento.Margin = new Padding(3, 4, 3, 4);
            numPorcentajeCrecimiento.Name = "numPorcentajeCrecimiento";
            numPorcentajeCrecimiento.Size = new Size(103, 34);
            numPorcentajeCrecimiento.TabIndex = 16;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(391, 250);
            label10.Name = "label10";
            label10.Size = new Size(142, 28);
            label10.TabIndex = 15;
            label10.Text = "% Crecimiento:";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Font = new Font("Segoe UI", 10F);
            txtObservaciones.Location = new Point(173, 285);
            txtObservaciones.Margin = new Padding(3, 4, 3, 4);
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(493, 34);
            txtObservaciones.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(22, 289);
            label9.Name = "label9";
            label9.Size = new Size(143, 28);
            label9.TabIndex = 13;
            label9.Text = "Observaciones:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(22, 250);
            label8.Name = "label8";
            label8.Size = new Size(156, 28);
            label8.TabIndex = 11;
            label8.Text = "Estado de Salud:";
            // 
            // numNumHojas
            // 
            numNumHojas.Font = new Font("Segoe UI", 10F);
            numNumHojas.Location = new Point(563, 200);
            numNumHojas.Margin = new Padding(3, 4, 3, 4);
            numNumHojas.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numNumHojas.Name = "numNumHojas";
            numNumHojas.Size = new Size(103, 34);
            numNumHojas.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(391, 202);
            label7.Name = "label7";
            label7.Size = new Size(142, 28);
            label7.TabIndex = 9;
            label7.Text = "Número Hojas:";
            // 
            // numAlturaPlanta
            // 
            numAlturaPlanta.DecimalPlaces = 2;
            numAlturaPlanta.Font = new Font("Segoe UI", 10F);
            numAlturaPlanta.Location = new Point(201, 200);
            numAlturaPlanta.Margin = new Padding(3, 4, 3, 4);
            numAlturaPlanta.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numAlturaPlanta.Name = "numAlturaPlanta";
            numAlturaPlanta.Size = new Size(133, 34);
            numAlturaPlanta.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(22, 202);
            label6.Name = "label6";
            label6.Size = new Size(171, 28);
            label6.TabIndex = 7;
            label6.Text = "Altura Planta (cm):";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(22, 154);
            label5.Name = "label5";
            label5.Size = new Size(175, 28);
            label5.TabIndex = 6;
            label5.Text = "Etapa Crecimiento:";
            // 
            // dtpFechaRegistro
            // 
            dtpFechaRegistro.Font = new Font("Segoe UI", 10F);
            dtpFechaRegistro.Format = DateTimePickerFormat.Short;
            dtpFechaRegistro.Location = new Point(523, 100);
            dtpFechaRegistro.Margin = new Padding(3, 4, 3, 4);
            dtpFechaRegistro.Name = "dtpFechaRegistro";
            dtpFechaRegistro.Size = new Size(143, 34);
            dtpFechaRegistro.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(391, 106);
            label4.Name = "label4";
            label4.Size = new Size(108, 28);
            label4.TabIndex = 4;
            label4.Text = "Fecha Reg.:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(22, 106);
            label3.Name = "label3";
            label3.Size = new Size(78, 28);
            label3.TabIndex = 3;
            label3.Text = "Cultivo:";
            // 
            // cbxCultivo
            // 
            cbxCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxCultivo.Font = new Font("Segoe UI", 10F);
            cbxCultivo.FormattingEnabled = true;
            cbxCultivo.Location = new Point(118, 100);
            cbxCultivo.Margin = new Padding(3, 4, 3, 4);
            cbxCultivo.Name = "cbxCultivo";
            cbxCultivo.Size = new Size(266, 36);
            cbxCultivo.TabIndex = 2;
            cbxCultivo.SelectedIndexChanged += cbxCultivo_SelectedIndexChanged;
            // 
            // txtIDSeguimiento
            // 
            txtIDSeguimiento.Enabled = false;
            txtIDSeguimiento.Font = new Font("Segoe UI", 10F);
            txtIDSeguimiento.Location = new Point(118, 50);
            txtIDSeguimiento.Margin = new Padding(3, 4, 3, 4);
            txtIDSeguimiento.Name = "txtIDSeguimiento";
            txtIDSeguimiento.ReadOnly = true;
            txtIDSeguimiento.Size = new Size(116, 34);
            txtIDSeguimiento.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(22, 54);
            label2.Name = "label2";
            label2.Size = new Size(77, 28);
            label2.TabIndex = 0;
            label2.Text = "ID Seg.:";
            // 
            // FrmSeguimientoCrecimiento
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 952);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmSeguimientoCrecimiento";
            Text = "Seguimiento de Crecimiento";
            Load += FrmSeguimientoCrecimiento_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSeguimiento).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPorcentajeCrecimiento).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNumHojas).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAlturaPlanta).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCrecimiento;
        private System.Windows.Forms.TextBox txtIDSeguimiento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxCultivo;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numAlturaPlanta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numNumHojas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numPorcentajeCrecimiento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtProblemasDetectados;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAccionesTomadas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbxEtapaCrecimiento;
        private System.Windows.Forms.ComboBox cbxEstadoSalud;
        private System.Windows.Forms.DataGridView dgvSeguimiento;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnGuardar;
    }
}

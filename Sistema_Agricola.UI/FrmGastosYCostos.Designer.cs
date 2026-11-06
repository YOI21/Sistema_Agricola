namespace Sistema_Agricola.UI
{
    partial class FrmGastosYCostos
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
            btnGeneralID = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnActualizar = new Button();
            btnGuardar = new Button();
            dataGridView1 = new DataGridView();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            txtIDGastos = new TextBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(245, 247, 250);
            panel1.BorderStyle = BorderStyle.None;
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2, 2, 2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 70);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            btnVolver.BackColor = Color.FromArgb(231, 76, 60);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1120, 15);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(60, 40);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "✕";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(62, 79, 98);
            label1.Location = new Point(30, 18);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(200, 32);
            label1.TabIndex = 0;
            label1.Text = "Gastos y Costos";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.None;
            panel2.Controls.Add(btnGeneralID);
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(btnActualizar);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(txtIDGastos);
            panel2.Controls.Add(comboBox3);
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 70);
            panel2.Margin = new Padding(2, 2, 2, 2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(30, 20, 30, 20);
            panel2.Size = new Size(1200, 630);
            panel2.TabIndex = 1;
            // 
            // btnGeneralID
            // 
            btnGeneralID.BackColor = Color.FromArgb(52, 152, 219);
            btnGeneralID.Cursor = Cursors.Hand;
            btnGeneralID.FlatAppearance.BorderSize = 0;
            btnGeneralID.FlatStyle = FlatStyle.Flat;
            btnGeneralID.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGeneralID.ForeColor = Color.White;
            btnGeneralID.Location = new Point(360, 28);
            btnGeneralID.Margin = new Padding(2, 2, 2, 2);
            btnGeneralID.Name = "btnGeneralID";
            btnGeneralID.Size = new Size(80, 25);
            btnGeneralID.TabIndex = 45;
            btnGeneralID.Text = "Generar";
            btnGeneralID.UseVisualStyleBackColor = false;
            btnGeneralID.Click += btnGeneralID_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(510, 260);
            btnEliminar.Margin = new Padding(2, 2, 2, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(140, 40);
            btnEliminar.TabIndex = 22;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(149, 165, 166);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(350, 260);
            btnLimpiar.Margin = new Padding(2, 2, 2, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(140, 40);
            btnLimpiar.TabIndex = 21;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(190, 260);
            btnActualizar.Margin = new Padding(2, 2, 2, 2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(140, 40);
            btnActualizar.TabIndex = 20;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(30, 260);
            btnGuardar.Margin = new Padding(2, 2, 2, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 40);
            btnGuardar.TabIndex = 19;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(62, 79, 98),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.True
            };
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                Font = new Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(52, 152, 219),
                SelectionForeColor = Color.White
            };
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(30, 320);
            dataGridView1.Margin = new Padding(2, 2, 2, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1140, 280);
            dataGridView1.TabIndex = 18;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.FromArgb(240, 245, 249);
            textBox5.BorderStyle = BorderStyle.FixedSingle;
            textBox5.Font = new Font("Segoe UI", 10F);
            textBox5.Location = new Point(170, 208);
            textBox5.Margin = new Padding(2, 2, 2, 2);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(140, 25);
            textBox5.TabIndex = 17;
            textBox5.KeyPress += textBox5_KeyPress;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(240, 245, 249);
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.Font = new Font("Segoe UI", 10F);
            textBox4.Location = new Point(770, 73);
            textBox4.Margin = new Padding(2, 2, 2, 2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(200, 25);
            textBox4.TabIndex = 16;
            textBox4.KeyPress += textBox4_KeyPress;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(240, 245, 249);
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Font = new Font("Segoe UI", 10F);
            textBox3.Location = new Point(770, 28);
            textBox3.Margin = new Padding(2, 2, 2, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(200, 25);
            textBox3.TabIndex = 15;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(240, 245, 249);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(170, 163);
            textBox2.Margin = new Padding(2, 2, 2, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(270, 25);
            textBox2.TabIndex = 14;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 10F);
            dateTimePicker1.Font = new Font("Segoe UI", 10F);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(170, 73);
            dateTimePicker1.Margin = new Padding(2, 2, 2, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(180, 25);
            dateTimePicker1.TabIndex = 13;
            // 
            // txtIDGastos
            // 
            txtIDGastos.BackColor = Color.FromArgb(240, 245, 249);
            txtIDGastos.BorderStyle = BorderStyle.FixedSingle;
            txtIDGastos.Font = new Font("Segoe UI", 10F);
            txtIDGastos.Location = new Point(170, 28);
            txtIDGastos.Margin = new Padding(2, 2, 2, 2);
            txtIDGastos.Name = "txtIDGastos";
            txtIDGastos.ReadOnly = true;
            txtIDGastos.Size = new Size(180, 25);
            txtIDGastos.TabIndex = 12;
            txtIDGastos.KeyPress += textBox1_KeyPress;
            // 
            // comboBox3
            // 
            comboBox3.BackColor = Color.FromArgb(240, 245, 249);
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FlatStyle = FlatStyle.Flat;
            comboBox3.Font = new Font("Segoe UI", 10F);
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Insumos", "Mano de Obra", "Maquinaria", "Mantenimiento", "Plantas" });
            comboBox3.Location = new Point(170, 118);
            comboBox3.Margin = new Padding(2, 2, 2, 2);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(180, 25);
            comboBox3.TabIndex = 11;
            // 
            // comboBox2
            // 
            comboBox2.BackColor = Color.FromArgb(240, 245, 249);
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FlatStyle = FlatStyle.Flat;
            comboBox2.Font = new Font("Segoe UI", 10F);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Targeta", "Cheque", "Efectivo", "Deposito", "Tranferencia", "Criptomeneda" });
            comboBox2.Location = new Point(770, 118);
            comboBox2.Margin = new Padding(2, 2, 2, 2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(200, 25);
            comboBox2.TabIndex = 10;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(240, 245, 249);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 10F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Pagado", "Pendiente", "Vencido" });
            comboBox1.Location = new Point(770, 163);
            comboBox1.Margin = new Padding(2, 2, 2, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 25);
            comboBox1.TabIndex = 9;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(62, 79, 98);
            label10.Location = new Point(600, 165);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(131, 20);
            label10.TabIndex = 8;
            label10.Text = "Estado del Pago:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(62, 79, 98);
            label9.Location = new Point(600, 120);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(133, 20);
            label9.TabIndex = 7;
            label9.Text = "Método de Pago:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(62, 79, 98);
            label8.Location = new Point(600, 75);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(126, 20);
            label8.TabIndex = 6;
            label8.Text = "Total del Gasto:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(62, 79, 98);
            label7.Location = new Point(600, 30);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(140, 20);
            label7.TabIndex = 5;
            label7.Text = "Costo por Unidad:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(62, 79, 98);
            label6.Location = new Point(30, 210);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(77, 20);
            label6.TabIndex = 4;
            label6.Text = "Cantidad:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(62, 79, 98);
            label5.Location = new Point(30, 165);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(86, 20);
            label5.TabIndex = 3;
            label5.Text = "Proveedor:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(62, 79, 98);
            label4.Location = new Point(30, 120);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(115, 20);
            label4.TabIndex = 2;
            label4.Text = "Tipo de Gasto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(62, 79, 98);
            label3.Location = new Point(30, 75);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(125, 20);
            label3.TabIndex = 1;
            label3.Text = "Fecha de Gasto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(62, 79, 98);
            label2.Location = new Point(30, 30);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 0;
            label2.Text = "ID Gastos:";
            // 
            // FrmGastosYCostos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 700);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 2, 2, 2);
            Name = "FrmGastosYCostos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gastos y Costos";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox textBox2;
        private DateTimePicker dateTimePicker1;
        private TextBox txtIDGastos;
        private DataGridView dataGridView1;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnActualizar;
        private Button btnGuardar;
        private Button btnGeneralID;
        private Button btnVolver;
    }
}
namespace Sistema_Agricola.UI
{
    partial class FrmReporte_Maquinarias
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btnVolver = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            dgvReporteM = new DataGridView();
            cbRM = new ComboBox();
            label2 = new Label();
            btnBuscarM = new Button();
            cbMaquinaria = new ComboBox();
            txtBuscarNombre = new TextBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporteM).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            panel1.BorderStyle = BorderStyle.None;
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1502, 70);
            panel1.TabIndex = 2;
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
            btnVolver.Location = new Point(1447, 15);
            btnVolver.Margin = new Padding(4, 5, 4, 5);
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
            label1.Size = new Size(408, 48);
            label1.TabIndex = 0;
            label1.Text = "Reporte de Maquinarias";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(dgvReporteM);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            groupBox1.Location = new Point(20, 190);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1462, 605);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Resultados de Búsqueda";
            // 
            // dgvReporteM
            // 
            dgvReporteM.AllowUserToAddRows = false;
            dgvReporteM.AllowUserToDeleteRows = false;
            dgvReporteM.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReporteM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporteM.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvReporteM.BackgroundColor = Color.White;
            dgvReporteM.BorderStyle = BorderStyle.None;
            dgvReporteM.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvReporteM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvReporteM.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporteM.Cursor = Cursors.Hand;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvReporteM.DefaultCellStyle = dataGridViewCellStyle6;
            dgvReporteM.EnableHeadersVisualStyles = false;
            dgvReporteM.Location = new Point(10, 35);
            dgvReporteM.Margin = new Padding(4, 5, 4, 5);
            dgvReporteM.Name = "dgvReporteM";
            dgvReporteM.ReadOnly = true;
            dgvReporteM.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvReporteM.RowHeadersVisible = false;
            dgvReporteM.RowHeadersWidth = 62;
            dgvReporteM.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvReporteM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporteM.Size = new Size(1442, 555);
            dgvReporteM.TabIndex = 0;
            // 
            // cbRM
            // 
            cbRM.Font = new Font("Segoe UI", 10F);
            cbRM.FormattingEnabled = true;
            cbRM.Items.AddRange(new object[] { "Nombre", "Tipo de Maquinaria", "Costo", "Fecha de adquisicion" });
            cbRM.Location = new Point(155, 92);
            cbRM.Margin = new Padding(4, 5, 4, 5);
            cbRM.Name = "cbRM";
            cbRM.Size = new Size(210, 36);
            cbRM.TabIndex = 4;
            cbRM.TextChanged += cbRM_TextChanged;
            cbRM.Enter += cbRM_Enter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            label2.Location = new Point(20, 95);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(127, 30);
            label2.TabIndex = 5;
            label2.Text = "Buscar Por:";
            // 
            // btnBuscarM
            // 
            btnBuscarM.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            btnBuscarM.FlatAppearance.BorderSize = 0;
            btnBuscarM.FlatStyle = FlatStyle.Flat;
            btnBuscarM.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBuscarM.ForeColor = Color.White;
            btnBuscarM.Cursor = System.Windows.Forms.Cursors.Hand;
            btnBuscarM.Location = new Point(620, 88);
            btnBuscarM.Margin = new Padding(4, 5, 4, 5);
            btnBuscarM.Name = "btnBuscarM";
            btnBuscarM.Size = new Size(110, 42);
            btnBuscarM.TabIndex = 6;
            btnBuscarM.Text = "Buscar";
            btnBuscarM.UseVisualStyleBackColor = false;
            btnBuscarM.Click += btnBuscarM_Click;
            // 
            // cbMaquinaria
            // 
            cbMaquinaria.Font = new Font("Segoe UI", 10F);
            cbMaquinaria.FormattingEnabled = true;
            cbMaquinaria.Items.AddRange(new object[] { "Tractor", "Cosechadora", "Cargador Frontal", "Equipo de forraje", "Labranza", "Sembradora", "Desmalezadora", "Pulverizadora" });
            cbMaquinaria.Location = new Point(388, 92);
            cbMaquinaria.Margin = new Padding(4, 5, 4, 5);
            cbMaquinaria.Name = "cbMaquinaria";
            cbMaquinaria.Size = new Size(210, 36);
            cbMaquinaria.TabIndex = 7;
            // 
            // txtBuscarNombre
            // 
            txtBuscarNombre.Font = new Font("Segoe UI", 10F);
            txtBuscarNombre.Location = new Point(388, 92);
            txtBuscarNombre.Margin = new Padding(4, 5, 4, 5);
            txtBuscarNombre.Name = "txtBuscarNombre";
            txtBuscarNombre.PlaceholderText = "Buscar Nombre";
            txtBuscarNombre.Size = new Size(210, 34);
            txtBuscarNombre.TabIndex = 8;
            txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            txtBuscarNombre.Leave += txtBuscarNombre_Leave;
            // 
            // FrmReporte_Maquinarias
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1502, 815);
            Controls.Add(txtBuscarNombre);
            Controls.Add(cbMaquinaria);
            Controls.Add(btnBuscarM);
            Controls.Add(label2);
            Controls.Add(cbRM);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmReporte_Maquinarias";
            Text = "FrmReporte_Maquinarias";
            Load += FrmReporte_Maquinarias_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReporteM).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private GroupBox groupBox1;
        private ComboBox cbRM;
        private Label label2;
        private Button btnBuscarM;
        private DataGridView dgvReporteM;
        private ComboBox cbMaquinaria;
        private TextBox txtBuscarNombre;
        private Button btnVolver;
    }
}
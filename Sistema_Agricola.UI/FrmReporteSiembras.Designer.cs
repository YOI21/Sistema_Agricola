namespace Sistema_Agricola.UI
{
    partial class FrmReporteSiembras
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
            lblTitulo = new Label();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            panel2 = new Panel();
            panelBotones = new Panel();
            btnImprimir = new Button();
            btnExportarExcel = new Button();
            panelEstadisticas = new Panel();
            lblPromedioCiclo = new Label();
            lblTiposCultivo = new Label();
            lblTotalRegistros = new Label();
            lblCosechadas = new Label();
            lblEnCrecimiento = new Label();
            label5 = new Label();
            dgvSiembras = new DataGridView();
            groupBox1 = new GroupBox();
            flujoFiltros = new FlowLayoutPanel();
            btnLimpiarFiltros = new Button();
            btnFiltrar = new Button();
            txtBusqueda = new TextBox();
            label4 = new Label();
            cmbValorFiltro = new ComboBox();
            label3 = new Label();
            cmbFiltro = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            dtpFechaFin = new DateTimePicker();
            dtpFechaInicio = new DateTimePicker();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelBotones.SuspendLayout();
            panelEstadisticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSiembras).BeginInit();
            groupBox1.SuspendLayout();
            flujoFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(245, 247, 250);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(lblTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1148, 69);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(231, 76, 60);
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1091, 13);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(41, 40);
            btnVolver.TabIndex = 1;
            btnVolver.TabStop = false;
            btnVolver.Text = "X";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.FromArgb(62, 79, 98);
            lblTitulo.Location = new Point(455, 15);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(310, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reporte de siembras";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(236, 240, 245);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.AutoScroll = true;
            panel2.Controls.Add(panelBotones);
            panel2.Controls.Add(panelEstadisticas);
            panel2.Controls.Add(dgvSiembras);
            panel2.Controls.Add(groupBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 69);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1148, 648);
            panel2.TabIndex = 1;
            // 
            // panelBotones
            // 
            panelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelBotones.Controls.Add(btnImprimir);
            panelBotones.Controls.Add(btnExportarExcel);
            panelBotones.Location = new Point(821, 544);
            panelBotones.Margin = new Padding(4, 3, 4, 3);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(310, 90);
            panelBotones.TabIndex = 3;
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.FromArgb(91, 124, 153);
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Image = Properties.Resources.impresora1;
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.Location = new Point(16, 48);
            btnImprimir.Margin = new Padding(4, 3, 4, 3);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(280, 40);
            btnImprimir.TabIndex = 1;
            btnImprimir.Text = "     &Imprimir reporte";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Cursor = Cursors.Hand;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.BackColor = Color.FromArgb(46, 125, 50);
            btnExportarExcel.FlatStyle = FlatStyle.Flat;
            btnExportarExcel.FlatAppearance.BorderSize = 0;
            btnExportarExcel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnExportarExcel.ForeColor = Color.White;
            btnExportarExcel.Image = Properties.Resources.Excel1;
            btnExportarExcel.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarExcel.Location = new Point(16, 6);
            btnExportarExcel.Margin = new Padding(4, 3, 4, 3);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(280, 40);
            btnExportarExcel.TabIndex = 0;
            btnExportarExcel.Text = "     &Exportar a Excel";
            btnExportarExcel.UseVisualStyleBackColor = false;
            btnExportarExcel.Cursor = Cursors.Hand;
            // 
            // panelEstadisticas
            // 
            panelEstadisticas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelEstadisticas.BackColor = Color.White;
            panelEstadisticas.BorderStyle = BorderStyle.FixedSingle;
            panelEstadisticas.Controls.Add(lblPromedioCiclo);
            panelEstadisticas.Controls.Add(lblTiposCultivo);
            panelEstadisticas.Controls.Add(lblTotalRegistros);
            panelEstadisticas.Controls.Add(lblCosechadas);
            panelEstadisticas.Controls.Add(lblEnCrecimiento);
            panelEstadisticas.Controls.Add(label5);
            panelEstadisticas.Location = new Point(14, 544);
            panelEstadisticas.Margin = new Padding(4, 3, 4, 3);
            panelEstadisticas.Name = "panelEstadisticas";
            panelEstadisticas.Size = new Size(800, 90);
            panelEstadisticas.TabIndex = 2;
            // 
            // lblPromedioCiclo
            // 
            lblPromedioCiclo.AutoSize = true;
            lblPromedioCiclo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblPromedioCiclo.ForeColor = Color.FromArgb(62, 79, 98);
            lblPromedioCiclo.Location = new Point(531, 57);
            lblPromedioCiclo.Margin = new Padding(4, 0, 4, 0);
            lblPromedioCiclo.Name = "lblPromedioCiclo";
            lblPromedioCiclo.Size = new Size(151, 15);
            lblPromedioCiclo.TabIndex = 5;
            lblPromedioCiclo.Text = "Ciclo promedio: 120.5 días";
            // 
            // lblTiposCultivo
            // 
            lblTiposCultivo.AutoSize = true;
            lblTiposCultivo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblTiposCultivo.ForeColor = Color.FromArgb(62, 79, 98);
            lblTiposCultivo.Location = new Point(271, 57);
            lblTiposCultivo.Margin = new Padding(4, 0, 4, 0);
            lblTiposCultivo.Name = "lblTiposCultivo";
            lblTiposCultivo.Size = new Size(208, 15);
            lblTiposCultivo.TabIndex = 4;
            lblTiposCultivo.Text = "Tipos: Orgánica, Híbrida, Transgénica";
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblTotalRegistros.ForeColor = Color.FromArgb(62, 79, 98);
            lblTotalRegistros.Location = new Point(18, 57);
            lblTotalRegistros.Margin = new Padding(4, 0, 4, 0);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(106, 15);
            lblTotalRegistros.TabIndex = 3;
            lblTotalRegistros.Text = "Total: 24 registros";
            // 
            // lblCosechadas
            // 
            lblCosechadas.AutoSize = true;
            lblCosechadas.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCosechadas.ForeColor = Color.FromArgb(46, 125, 50);
            lblCosechadas.Location = new Point(531, 29);
            lblCosechadas.Margin = new Padding(4, 0, 4, 0);
            lblCosechadas.Name = "lblCosechadas";
            lblCosechadas.Size = new Size(98, 19);
            lblCosechadas.TabIndex = 2;
            lblCosechadas.Text = "Cosechadas: 0";
            // 
            // lblEnCrecimiento
            // 
            lblEnCrecimiento.AutoSize = true;
            lblEnCrecimiento.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblEnCrecimiento.ForeColor = Color.FromArgb(33, 150, 243);
            lblEnCrecimiento.Location = new Point(271, 29);
            lblEnCrecimiento.Margin = new Padding(4, 0, 4, 0);
            lblEnCrecimiento.Name = "lblEnCrecimiento";
            lblEnCrecimiento.Size = new Size(121, 19);
            lblEnCrecimiento.TabIndex = 1;
            lblEnCrecimiento.Text = "En crecimiento: 18";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(62, 79, 98);
            label5.Location = new Point(16, 23);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 0;
            label5.Text = "Estadísticas:";
            // 
            // dgvSiembras
            // 
            dgvSiembras.AllowUserToAddRows = false;
            dgvSiembras.AllowUserToDeleteRows = false;
            dgvSiembras.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSiembras.BackgroundColor = Color.White;
            dgvSiembras.BorderStyle = BorderStyle.None;
            dgvSiembras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSiembras.Location = new Point(14, 141);
            dgvSiembras.Margin = new Padding(4, 3, 4, 3);
            dgvSiembras.MultiSelect = false;
            dgvSiembras.Name = "dgvSiembras";
            dgvSiembras.ReadOnly = true;
            dgvSiembras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSiembras.Size = new Size(1118, 397);
            dgvSiembras.TabIndex = 1;
            dgvSiembras.GridColor = Color.FromArgb(220, 226, 235);
            dgvSiembras.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(flujoFiltros);
            groupBox1.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(62, 79, 98);
            groupBox1.Location = new Point(14, 7);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(1118, 127);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtros";
            // 
            // flujoFiltros
            // 
            flujoFiltros.AutoScroll = true;
            flujoFiltros.Controls.Add(label1);
            flujoFiltros.Controls.Add(dtpFechaInicio);
            flujoFiltros.Controls.Add(label2);
            flujoFiltros.Controls.Add(dtpFechaFin);
            flujoFiltros.Controls.Add(label3);
            flujoFiltros.Controls.Add(cmbFiltro);
            flujoFiltros.Controls.Add(cmbValorFiltro);
            flujoFiltros.Controls.Add(label4);
            flujoFiltros.Controls.Add(txtBusqueda);
            flujoFiltros.Controls.Add(btnFiltrar);
            flujoFiltros.Controls.Add(btnLimpiarFiltros);
            flujoFiltros.Dock = DockStyle.Fill;
            flujoFiltros.FlowDirection = FlowDirection.LeftToRight;
            flujoFiltros.Font = new Font("Segoe UI", 9F);
            flujoFiltros.Location = new Point(4, 23);
            flujoFiltros.Name = "flujoFiltros";
            flujoFiltros.Padding = new Padding(8, 4, 8, 4);
            flujoFiltros.Size = new Size(1110, 101);
            flujoFiltros.TabIndex = 0;
            flujoFiltros.WrapContents = true;
            flujoFiltros.SetFlowBreak(cmbValorFiltro, true);
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.BackColor = Color.FromArgb(189, 195, 199);
            btnLimpiarFiltros.FlatStyle = FlatStyle.Flat;
            btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            btnLimpiarFiltros.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnLimpiarFiltros.ForeColor = Color.White;
            btnLimpiarFiltros.Location = new Point(966, 75);
            btnLimpiarFiltros.Margin = new Padding(6, 5, 4, 3);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new Size(145, 38);
            btnLimpiarFiltros.TabIndex = 6;
            btnLimpiarFiltros.Text = "&Limpiar filtros";
            btnLimpiarFiltros.UseVisualStyleBackColor = false;
            btnLimpiarFiltros.Cursor = Cursors.Hand;
            // 
            // btnFiltrar
            // 
            btnFiltrar.BackColor = Color.FromArgb(91, 124, 153);
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Location = new Point(966, 28);
            btnFiltrar.Margin = new Padding(6, 5, 4, 3);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(145, 38);
            btnFiltrar.TabIndex = 5;
            btnFiltrar.Text = "&Aplicar filtros";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Cursor = Cursors.Hand;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Font = new Font("Segoe UI", 10F);
            txtBusqueda.BackColor = Color.FromArgb(245, 247, 250);
            txtBusqueda.BorderStyle = BorderStyle.FixedSingle;
            txtBusqueda.ForeColor = Color.FromArgb(62, 79, 98);
            txtBusqueda.Location = new Point(583, 77);
            txtBusqueda.Margin = new Padding(4, 9, 4, 3);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(362, 25);
            txtBusqueda.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.FromArgb(62, 79, 98);
            label4.Location = new Point(490, 81);
            label4.Margin = new Padding(6, 13, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "Búsqueda:";
            // 
            // cmbValorFiltro
            // 
            cmbValorFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbValorFiltro.Font = new Font("Segoe UI", 10F);
            cmbValorFiltro.BackColor = Color.FromArgb(245, 247, 250);
            cmbValorFiltro.ForeColor = Color.FromArgb(62, 79, 98);
            cmbValorFiltro.FormattingEnabled = true;
            cmbValorFiltro.Location = new Point(797, 32);
            cmbValorFiltro.Margin = new Padding(4, 9, 4, 3);
            cmbValorFiltro.Name = "cmbValorFiltro";
            cmbValorFiltro.Size = new Size(149, 25);
            cmbValorFiltro.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.FromArgb(62, 79, 98);
            label3.Location = new Point(490, 36);
            label3.Margin = new Padding(6, 13, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 5;
            label3.Text = "Filtro:";
            // 
            // cmbFiltro
            // 
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.Font = new Font("Segoe UI", 10F);
            cmbFiltro.BackColor = Color.FromArgb(245, 247, 250);
            cmbFiltro.ForeColor = Color.FromArgb(62, 79, 98);
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Location = new Point(548, 32);
            cmbFiltro.Margin = new Padding(4, 9, 4, 3);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(241, 25);
            cmbFiltro.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.FromArgb(62, 79, 98);
            label2.Location = new Point(250, 33);
            label2.Margin = new Padding(6, 13, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 3;
            label2.Text = "Hasta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.FromArgb(62, 79, 98);
            label1.Location = new Point(21, 33);
            label1.Margin = new Padding(6, 13, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 2;
            label1.Text = "Desde:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Font = new Font("Segoe UI", 10F);
            dtpFechaFin.CalendarMonthBackground = Color.FromArgb(245, 247, 250);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(322, 30);
            dtpFechaFin.Margin = new Padding(4, 9, 4, 3);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(139, 25);
            dtpFechaFin.TabIndex = 1;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Font = new Font("Segoe UI", 10F);
            dtpFechaInicio.CalendarMonthBackground = Color.FromArgb(245, 247, 250);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(86, 30);
            dtpFechaInicio.Margin = new Padding(4, 9, 4, 3);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(139, 25);
            dtpFechaInicio.TabIndex = 0;
            // 
            // FrmReporteSiembras
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(1148, 650);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmReporteSiembras";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte de siembras";
            Load += FrmReporteSiembras_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            panelEstadisticas.ResumeLayout(false);
            panelEstadisticas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSiembras).EndInit();
            groupBox1.ResumeLayout(false);
            flujoFiltros.ResumeLayout(false);
            flujoFiltros.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flujoFiltros;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ComboBox cmbValorFiltro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFiltro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DataGridView dgvSiembras;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.Label lblPromedioCiclo;
        private System.Windows.Forms.Label lblTiposCultivo;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Label lblCosechadas;
        private System.Windows.Forms.Label lblEnCrecimiento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}

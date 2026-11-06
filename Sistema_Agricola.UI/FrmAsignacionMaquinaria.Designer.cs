using System.Windows.Forms;

namespace Sistema_Agricola.UI
{
    partial class FrmAsignacionMaquinaria
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

            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOperador = new System.Windows.Forms.Label();
            this.cmbOperadores = new System.Windows.Forms.ComboBox();
            this.lblMaquinaria = new System.Windows.Forms.Label();
            this.cmbMaquinaria = new System.Windows.Forms.ComboBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();

            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvAsignaciones = new System.Windows.Forms.DataGridView();
            this.btnFinalizar = new System.Windows.Forms.Button();

            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignaciones)).BeginInit();
            this.SuspendLayout();

            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 60);

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new System.Drawing.Point(20, 17);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(320, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignación de Maquinaria";

            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(1140, 15);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(45, 30);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "✕";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);

            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(1200, 630);

            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblOperador);
            this.groupBox1.Controls.Add(this.cmbOperadores);
            this.groupBox1.Controls.Add(this.lblMaquinaria);
            this.groupBox1.Controls.Add(this.cmbMaquinaria);
            this.groupBox1.Controls.Add(this.lblObservaciones);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.btnAsignar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox1.Size = new System.Drawing.Size(1170, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asignación de Maquinaria";

            // 
            // lblOperador
            // 
            this.lblOperador.AutoSize = true;
            this.lblOperador.Location = new System.Drawing.Point(20, 40);
            this.lblOperador.Name = "lblOperador";
            this.lblOperador.Size = new System.Drawing.Size(109, 32);
            this.lblOperador.TabIndex = 0;
            this.lblOperador.Text = "Operador:";

            // 
            // cmbOperadores
            // 
            this.cmbOperadores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperadores.FormattingEnabled = true;
            this.cmbOperadores.Location = new System.Drawing.Point(150, 37);
            this.cmbOperadores.Name = "cmbOperadores";
            this.cmbOperadores.Size = new System.Drawing.Size(400, 36);
            this.cmbOperadores.TabIndex = 1;

            // 
            // lblMaquinaria
            // 
            this.lblMaquinaria.AutoSize = true;
            this.lblMaquinaria.Location = new System.Drawing.Point(20, 90);
            this.lblMaquinaria.Name = "lblMaquinaria";
            this.lblMaquinaria.Size = new System.Drawing.Size(126, 32);
            this.lblMaquinaria.TabIndex = 2;
            this.lblMaquinaria.Text = "Maquinaria:";

            // 
            // cmbMaquinaria
            // 
            this.cmbMaquinaria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaquinaria.FormattingEnabled = true;
            this.cmbMaquinaria.Location = new System.Drawing.Point(150, 87);
            this.cmbMaquinaria.Name = "cmbMaquinaria";
            this.cmbMaquinaria.Size = new System.Drawing.Size(400, 36);
            this.cmbMaquinaria.TabIndex = 3;

            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(20, 140);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(164, 32);
            this.lblObservaciones.TabIndex = 4;
            this.lblObservaciones.Text = "Observaciones:";

            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(190, 137);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(500, 34);
            this.txtObservaciones.TabIndex = 5;

            // 
            // btnAsignar
            // 
            this.btnAsignar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.FlatAppearance.BorderSize = 0;
            this.btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAsignar.ForeColor = System.Drawing.Color.White;
            this.btnAsignar.Location = new System.Drawing.Point(800, 37);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(160, 50);
            this.btnAsignar.TabIndex = 6;
            this.btnAsignar.Text = "✔️ Asignar";
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);

            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(800, 97);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(160, 50);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "🧹 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.dgvAsignaciones);
            this.groupBox2.Controls.Add(this.btnFinalizar);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox2.Location = new System.Drawing.Point(15, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox2.Size = new System.Drawing.Size(1170, 380);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Asignaciones Activas";

            // 
            // dgvAsignaciones
            // 
            this.dgvAsignaciones.AllowUserToAddRows = false;
            this.dgvAsignaciones.AllowUserToDeleteRows = false;
            this.dgvAsignaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAsignaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAsignaciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvAsignaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAsignaciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAsignaciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAsignaciones.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.True
            };
            this.dgvAsignaciones.ColumnHeadersHeight = 40;
            this.dgvAsignaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAsignaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvAsignaciones.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9.75F),
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = System.Windows.Forms.DataGridViewTriState.False
            };
            this.dgvAsignaciones.EnableHeadersVisualStyles = false;
            this.dgvAsignaciones.GridColor = System.Drawing.Color.FromArgb(229, 229, 229);
            this.dgvAsignaciones.Location = new System.Drawing.Point(20, 35);
            this.dgvAsignaciones.Name = "dgvAsignaciones";
            this.dgvAsignaciones.ReadOnly = true;
            this.dgvAsignaciones.RowHeadersVisible = false;
            this.dgvAsignaciones.RowHeadersWidth = 62;
            this.dgvAsignaciones.RowTemplate.Height = 35;
            this.dgvAsignaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsignaciones.Size = new System.Drawing.Size(1130, 265);
            this.dgvAsignaciones.TabIndex = 8;
            this.dgvAsignaciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsignaciones_CellClick);

            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnFinalizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizar.FlatAppearance.BorderSize = 0;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(990, 315);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(160, 50);
            this.btnFinalizar.TabIndex = 9;
            this.btnFinalizar.Text = "⏹️ Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);

            // 
            // FrmAsignacionMaquinaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.ClientSize = new System.Drawing.Size(1200, 690);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAsignacionMaquinaria";
            this.Text = "Asignación de Maquinaria";
            this.Load += new System.EventHandler(this.FrmAsignacionMaquinaria_Load);

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignaciones)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnVolver;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblOperador;
        private System.Windows.Forms.ComboBox cmbOperadores;
        private System.Windows.Forms.Label lblMaquinaria;
        private System.Windows.Forms.ComboBox cmbMaquinaria;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnLimpiar;

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvAsignaciones;
        private System.Windows.Forms.Button btnFinalizar;
    }
}
namespace Sistema_Agricola.UI
{
    partial class FrmMantenimiento
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
            panel4 = new Panel();
            button2 = new Button();
            panel3 = new Panel();
            button1 = new Button();
            panel5 = new Panel();
            button3 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
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
            btnVolver.Margin = new Padding(4, 5, 4, 5);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(60, 40);
            btnVolver.TabIndex = 8;
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
            label1.Name = "label1";
            label1.Size = new Size(200, 32);
            label1.TabIndex = 0;
            label1.Text = "Mantenimiento";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(panel5);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 70);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(40, 30, 40, 30);
            panel2.Size = new Size(1200, 630);
            panel2.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(button2);
            panel4.Location = new Point(40, 210);
            panel4.Name = "panel4";
            panel4.Size = new Size(1120, 150);
            panel4.TabIndex = 1;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(46, 204, 113);
            button2.Cursor = Cursors.Hand;
            button2.Dock = DockStyle.Fill;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(0, 0);
            button2.Name = "button2";
            button2.Size = new Size(1120, 150);
            button2.TabIndex = 1;
            button2.Text = "📦 Gestión de Inventario";
            button2.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(button1);
            panel3.Location = new Point(40, 30);
            panel3.Name = "panel3";
            panel3.Size = new Size(1120, 150);
            panel3.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(52, 152, 219);
            button1.Cursor = Cursors.Hand;
            button1.Dock = DockStyle.Fill;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(1120, 150);
            button1.TabIndex = 0;
            button1.Text = "💰 Gestión de Finanzas";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(button3);
            panel5.Location = new Point(40, 390);
            panel5.Name = "panel5";
            panel5.Size = new Size(1120, 150);
            panel5.TabIndex = 1;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(230, 126, 34);
            button3.Cursor = Cursors.Hand;
            button3.Dock = DockStyle.Fill;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(0, 0);
            button3.Name = "button3";
            button3.Size = new Size(1120, 150);
            button3.TabIndex = 2;
            button3.Text = "☀ Gestión del Clima";
            button3.UseVisualStyleBackColor = false;
            // 
            // FrmMantenimiento
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 700);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMantenimiento";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mantenimiento del Sistema";
            Load += FrmMantenimiento_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel5;
        private Panel panel4;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnVolver;
    }
}
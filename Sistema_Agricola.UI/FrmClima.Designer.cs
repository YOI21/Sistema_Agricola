namespace Sistema_Agricola.UI
{
    partial class FrmClima
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
            btnVolver = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnVolver
            // 
            btnVolver.BackColor = SystemColors.ButtonFace;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 2;
            btnVolver.FlatAppearance.MouseDownBackColor = Color.Red;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            btnVolver.Location = new Point(1304, 945);
            btnVolver.Margin = new Padding(4, 5, 4, 5);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(139, 60);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources._61449;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatAppearance.MouseDownBackColor = Color.Red;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            button1.Location = new Point(614, 197);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(446, 312);
            button1.TabIndex = 14;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FrmClima
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.hol2_1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1440, 1003);
            Controls.Add(button1);
            Controls.Add(btnVolver);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmClima";
            Text = "CLIMA";
            ResumeLayout(false);
        }

        #endregion

        private Button btnVolver;
        private Button button1;
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.UI
{
    public sealed class FrmCambiarContrasenaInicial : Form
    {
        private readonly int idUsuario;
        private readonly string contrasenaActual;
        private readonly TextBox txtNueva = new TextBox();
        private readonly TextBox txtConfirmar = new TextBox();
        private readonly Label lblEstado = new Label();

        public FrmCambiarContrasenaInicial(
            int idUsuario,
            string usuario,
            string contrasenaActual)
        {
            this.idUsuario = idUsuario;
            this.contrasenaActual = contrasenaActual;
            Text = "Cambio obligatorio de contraseña";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(500, 310);
            Font = new Font("Segoe UI", 9.5F);
            BackColor = Color.White;

            ConstruirInterfaz(usuario);
        }

        private void ConstruirInterfaz(string usuario)
        {
            TableLayoutPanel tabla = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                Padding = new Padding(28, 22, 28, 18)
            };
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            tabla.Controls.Add(new Label
            {
                Text = "Cambie la contraseña temporal de " + usuario,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(38, 92, 74)
            }, 0, 0);

            tabla.Controls.Add(new Label
            {
                Text = "Use de 12 a 128 caracteres con mayúscula, minúscula, número y símbolo.",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(72, 82, 92)
            }, 0, 1);

            tabla.Controls.Add(CrearEtiqueta("Nueva contraseña"), 0, 2);
            ConfigurarContrasena(txtNueva);
            tabla.Controls.Add(txtNueva, 0, 3);
            tabla.Controls.Add(CrearEtiqueta("Confirmar contraseña"), 0, 4);
            ConfigurarContrasena(txtConfirmar);
            tabla.Controls.Add(txtConfirmar, 0, 5);

            lblEstado.Dock = DockStyle.Fill;
            lblEstado.ForeColor = Color.FromArgb(176, 42, 42);
            tabla.Controls.Add(lblEstado, 0, 6);

            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            Button guardar = CrearBoton("Guardar", Color.FromArgb(45, 125, 94));
            Button cancelar = CrearBoton("Cancelar", Color.FromArgb(95, 106, 106));
            CheckBox mostrar = new CheckBox
            {
                Text = "Mostrar",
                AutoSize = true,
                Margin = new Padding(0, 8, 18, 0)
            };

            guardar.Click += Guardar_Click;
            cancelar.Click += (_, _) => Close();
            mostrar.CheckedChanged += (_, _) =>
            {
                char caracter = mostrar.Checked ? '\0' : '*';
                txtNueva.PasswordChar = caracter;
                txtConfirmar.PasswordChar = caracter;
            };
            acciones.Controls.Add(guardar);
            acciones.Controls.Add(cancelar);
            acciones.Controls.Add(mostrar);
            tabla.Controls.Add(acciones, 0, 7);

            AcceptButton = guardar;
            CancelButton = cancelar;
            Controls.Add(tabla);
        }

        private void Guardar_Click(object? sender, EventArgs e)
        {
            try
            {
                BLL_Login.CambiarContrasenaInicial(
                    idUsuario,
                    contrasenaActual,
                    txtNueva.Text,
                    txtConfirmar.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblEstado.Text = ex.Message;
            }
        }

        private static void ConfigurarContrasena(TextBox texto)
        {
            texto.Dock = DockStyle.Fill;
            texto.PasswordChar = '*';
            texto.MaxLength = BLL_PoliticaContrasena.LongitudMaxima;
            texto.BorderStyle = BorderStyle.FixedSingle;
        }

        private static Label CrearEtiqueta(string texto)
        {
            return new Label
            {
                Text = texto,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
            };
        }

        private static Button CrearBoton(string texto, Color color)
        {
            return new Button
            {
                Text = texto,
                Width = 100,
                Height = 32,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
        }
    }
}

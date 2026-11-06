using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.UI
{
    public partial class FrmRecuperarContrasena : Form
    {
        private int idUsuarioRecuperacion;

        public FrmRecuperarContrasena(string usuarioSugerido = "")
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(usuarioSugerido) && usuarioSugerido != "Usuario")
            {
                txtUsuarioCorreo.Text = usuarioSugerido.Trim();
            }

            HabilitarCambioContrasena(false);
        }

        private void btnEnviarCodigo_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            btnEnviarCodigo.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoSolicitudRecuperacion resultado =
                    BLL_RecuperacionContrasena.SolicitarCodigo(txtUsuarioCorreo.Text);

                MostrarMensaje(resultado.Mensaje, resultado.Exitoso);

                if (resultado.Exitoso)
                {
                    idUsuarioRecuperacion = resultado.ID_Usuario;
                    HabilitarCambioContrasena(true);
                    txtCodigo.Focus();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEnviarCodigo.Enabled = true;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ResultadoOperacionRecuperacion resultado =
                BLL_RecuperacionContrasena.RestablecerContrasena(
                    idUsuarioRecuperacion,
                    txtCodigo.Text,
                    txtNuevaContrasena.Text,
                    txtConfirmarContrasena.Text);

            MostrarMensaje(resultado.Mensaje, resultado.Exitoso);

            if (resultado.Exitoso)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            char caracter = chkMostrar.Checked ? '\0' : '*';
            txtNuevaContrasena.PasswordChar = caracter;
            txtConfirmarContrasena.PasswordChar = caracter;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HabilitarCambioContrasena(bool habilitar)
        {
            txtCodigo.Enabled = habilitar;
            txtNuevaContrasena.Enabled = habilitar;
            txtConfirmarContrasena.Enabled = habilitar;
            chkMostrar.Enabled = habilitar;
            btnActualizar.Enabled = habilitar;
            AcceptButton = habilitar ? btnActualizar : btnEnviarCodigo;
        }

        private void MostrarMensaje(string mensaje, bool exitoso)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = exitoso ? Color.FromArgb(36, 110, 60) : Color.FromArgb(170, 40, 40);
            lblMensaje.Visible = true;
        }
    }
}

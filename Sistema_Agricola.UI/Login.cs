using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class Login : Form
    {
        private bool passvista = false;
        public Login()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private bool ExistenUsuariosRegistrados()
        {
            try
            {
                return BLL_Login.ExistenUsuariosRegistrados();
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar mensaje pero asumir que no hay usuarios
                MessageBox.Show("Error al verificar usuarios: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ExisteAdministradorRegistrado()
        {
            try
            {
                return BLL_Login.ExisteAdministradorRegistrado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar administrador: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return true;
            }
        }
        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.Black;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.Gray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Contraseña")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.Black;
                txtpass.PasswordChar = passvista ? '\0' : '*';
            }

        }
       
        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "Contraseña";
                txtpass.ForeColor = Color.Gray;
                txtpass.PasswordChar = '\0';
            }

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnvista_Click(object sender, EventArgs e)
        {
            if (txtpass.Text == "Contraseña")
            {
                return;
            }

            passvista = !passvista;
            if (passvista)
            {
                txtpass.PasswordChar = '\0';
                btnvista.Image = Sistema_Agricola.UI.Properties.Resources.visibilidad;


            }
            else
            {
                txtpass.PasswordChar = '*';
                btnvista.Image = Sistema_Agricola.UI.Properties.Resources.ojo;
            }

        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos o con valores por defecto
            if (txtuser.Text == "Usuario" || txtpass.Text == "Contraseña" ||
                string.IsNullOrEmpty(txtuser.Text) || string.IsNullOrEmpty(txtpass.Text))
            {
                MsgError("Por favor ingrese su usuario y contraseña");
                return;
            }

            try
            {
                Entitie_ResultadoAutenticacion resultado =
                    BLL_Login.Autenticar(txtuser.Text, txtpass.Text);

                if (!resultado.Exitoso || resultado.Usuario == null)
                {
                    MsgError(resultado.Mensaje);
                    return;
                }

                Entitie_UsuarioAutenticado usuario = resultado.Usuario;
                if (usuario.DebeCambiarContrasena)
                {
                    using FrmCambiarContrasenaInicial cambio = new FrmCambiarContrasenaInicial(
                        usuario.ID_Usuario,
                        usuario.Username,
                        txtpass.Text);
                    if (cambio.ShowDialog(this) != DialogResult.OK)
                    {
                        MsgError("Debe cambiar la contraseña temporal antes de ingresar.");
                        return;
                    }

                    usuario.DebeCambiarContrasena = false;
                    txtpass.Text = "Contraseña";
                    txtpass.PasswordChar = '\0';
                }

                Session.CurrentUser = new Session.User
                {
                    ID = usuario.ID,
                    IDUsuario = usuario.ID_Usuario,
                    Username = usuario.Username,
                    FullName = usuario.FullName,
                    IsAgricultor = usuario.IsAgricultor,
                    Rol = usuario.Rol
                };

                MessageBox.Show($"Bienvenido {usuario.FullName} al Sistema Agrícola",
                    "Ingreso exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MsgError(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string usuarioSugerido = txtuser.Text == "Usuario" ? string.Empty : txtuser.Text;

            using FrmRecuperarContrasena recuperar = new FrmRecuperarContrasena(usuarioSugerido);
            recuperar.ShowDialog(this);
        }

        private void MsgError(string msg)
        {
            lblMessageError.Text = "    " + msg;
            lblMessageError.Visible = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Verificar si existe el administrador inicial
            if (!ExisteAdministradorRegistrado())
            {
                FrmAdministrador frmRegistro = new FrmAdministrador(true);
                this.Hide();
                frmRegistro.ShowDialog();

                if (ExisteAdministradorRegistrado())
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }

        }
    }
}

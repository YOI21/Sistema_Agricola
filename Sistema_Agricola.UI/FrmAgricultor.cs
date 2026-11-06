using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmAgricultor : Form
    {
        private bool isFirstUser = false;
        private readonly ErrorProvider errorProvider1;
        private bool emailVerificado = false;
        private string emailVerificadoActual = string.Empty;
        private string codigoVerificacionEmail = string.Empty;
        private DateTime codigoVerificacionExpira;

        public FrmAgricultor(bool isFirst = false)
        {
            InitializeComponent();
            errorProvider1 = new ErrorProvider(this);
            isFirstUser = isFirst;
            ConfigurarComponentes();

            // Si es el primer usuario, mostrar mensaje informativo
            if (isFirstUser)
            {
                lblTitulo.Text = "REGISTRO INICIAL DE AGRICULTOR";
                lblInfo.Visible = true;
                lblInfo.Text = "No hay usuarios registrados en el sistema. Por favor, complete el registro inicial.";
            }
            else
            {
                lblTitulo.Text = "REGISTRO DE AGRICULTOR";
                lblInfo.Visible = false;
            }
        }
        private void ConfigurarComponentes()
        {
            // Inicializar el ErrorProvider
            this.errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // Asignar los manejadores de eventos para la validación de cédula
            this.txtCedula.Leave += new EventHandler(this.txtCedula_Leave);
            this.txtCedula.KeyPress += new KeyPressEventHandler(this.txtCedula_KeyPress);
            this.txtEmail.TextChanged += new EventHandler(this.txtEmail_TextChanged);
            this.txtCodigoEmail.KeyPress += new KeyPressEventHandler(this.txtCodigoEmail_KeyPress);

            btnGuardar.Enabled = false;
            HabilitarConfirmacionCorreo(false);
            ActualizarEstadoCorreo("Gmail no verificado", Color.FromArgb(170, 40, 40));
        }

        private void btnGenerarCredenciales_Click(object sender, EventArgs e)
        {
            // Verificar que se hayan ingresado nombre y apellido
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Debe ingresar al menos el nombre y apellido para generar las credenciales.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            string usuario = BLL_RegistroAgricultor.GenerarUsuarioDisponible(nombre, apellido);
            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("No se pudo generar un usuario con el nombre y apellido ingresados.",
                    "Credenciales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string contraseña = BLL_RegistroAgricultor.GenerarContrasenaTemporal();

            // Mostrar las credenciales generadas
            txtUsuario.Text = usuario;
            txtContraseña.Text = contraseña;
        }
        private void txtCedula_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control como backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limitar a 11 caracteres
            if (txtCedula.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtCedula_Leave(object? sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim();

            // Solo validar si el campo tiene contenido
            if (!string.IsNullOrEmpty(cedula))
            {
                // Verificar que tenga exactamente 11 dígitos
                if (cedula.Length != 11)
                {
                    errorProvider1.SetError(txtCedula, "La cédula debe tener 11 dígitos");
                    return;
                }

                // Verificar que solo contenga números
                foreach (char c in cedula)
                {
                    if (!char.IsDigit(c))
                    {
                        errorProvider1.SetError(txtCedula, "La cédula solo debe contener números");
                        return;
                    }
                }

                // Validar la cédula usando el algoritmo
                if (!Utilidades.ValidacionCedula.EsCedulaValida(cedula))
                {
                    errorProvider1.SetError(txtCedula, "El número de cédula no es válido");
                }
                else
                {
                    if (BLL_RegistroAgricultor.ExisteCedulaAgricultor(cedula))
                    {
                        errorProvider1.SetError(txtCedula, "Ya existe un agricultor registrado con esta cédula");
                        return;
                    }

                    Entitie_DatosCedula? datosCedula = BLL_RegistroAgricultor.ObtenerDatosCedula(cedula);
                    if (datosCedula != null)
                    {
                        txtNombre.Text = datosCedula.Nombre;
                        txtApellido.Text = datosCedula.Apellido;
                        errorProvider1.SetError(txtCedula, "Cédula verificada. Nombre y apellido cargados.");
                        return;
                    }

                    errorProvider1.SetError(txtCedula, "");
                }
            }
        }

        private void btnEnviarCodigoEmail_Click(object sender, EventArgs e)
        {
            string nombreCompleto = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}".Trim();

            btnEnviarCodigoEmail.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoSolicitudVerificacionCorreo resultado =
                    BLL_VerificacionCorreoAgricultor.SolicitarCodigo(txtEmail.Text, nombreCompleto);

                if (!resultado.Exitoso)
                {
                    ReiniciarVerificacionCorreo(resultado.Mensaje);
                    MessageBox.Show(resultado.Mensaje, "Verificacion de Gmail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                emailVerificado = false;
                emailVerificadoActual = resultado.CorreoNormalizado;
                codigoVerificacionEmail = resultado.Codigo;
                codigoVerificacionExpira = resultado.Expira;

                HabilitarConfirmacionCorreo(true);
                btnGuardar.Enabled = false;
                ActualizarEstadoCorreo("Codigo enviado. Revise el Gmail e ingreselo.", Color.FromArgb(178, 112, 0));
                MessageBox.Show(resultado.Mensaje, "Codigo enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoEmail.Focus();
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEnviarCodigoEmail.Enabled = true;
            }
        }

        private void btnConfirmarCodigoEmail_Click(object sender, EventArgs e)
        {
            ResultadoOperacionRecuperacion resultado =
                BLL_VerificacionCorreoAgricultor.ValidarCodigo(
                    txtCodigoEmail.Text,
                    codigoVerificacionEmail,
                    codigoVerificacionExpira);

            if (!resultado.Exitoso)
            {
                ActualizarEstadoCorreo(resultado.Mensaje, Color.FromArgb(170, 40, 40));
                MessageBox.Show(resultado.Mensaje, "Verificacion de Gmail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            emailVerificado = true;
            emailVerificadoActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);
            btnGuardar.Enabled = true;
            HabilitarConfirmacionCorreo(false);
            ActualizarEstadoCorreo("Gmail verificado correctamente", Color.FromArgb(36, 110, 60));
        }

        private void txtEmail_TextChanged(object? sender, EventArgs e)
        {
            string correoActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);

            if (emailVerificado && correoActual == emailVerificadoActual)
            {
                return;
            }

            ReiniciarVerificacionCorreo("Gmail no verificado");
        }

        private void txtCodigoEmail_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ReiniciarVerificacionCorreo(string mensaje)
        {
            emailVerificado = false;
            codigoVerificacionEmail = string.Empty;
            codigoVerificacionExpira = DateTime.MinValue;
            txtCodigoEmail.Text = "";
            btnGuardar.Enabled = false;
            HabilitarConfirmacionCorreo(false);
            ActualizarEstadoCorreo(mensaje, Color.FromArgb(170, 40, 40));
        }

        private void HabilitarConfirmacionCorreo(bool habilitar)
        {
            txtCodigoEmail.Enabled = habilitar;
            btnConfirmarCodigoEmail.Enabled = habilitar;
        }

        private void ActualizarEstadoCorreo(string mensaje, Color color)
        {
            lblEstadoEmail.Text = mensaje;
            lblEstadoEmail.ForeColor = color;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos obligatorios estén completos
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtTelefono.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(txtSector.Text) ||
                string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor complete la información.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string emailActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);
            if (!emailVerificado || emailActual != emailVerificadoActual)
            {
                MessageBox.Show("Debe verificar el Gmail del agricultor antes de guardar.",
                    "Gmail no verificado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Validar formato de cédula
            string cedula = txtCedula.Text.Trim();
            if (!Utilidades.ValidacionCedula.EsCedulaValida(cedula))
            {
                MessageBox.Show("El número de cédula ingresado no es válido. Por favor verifique.",
                    "Cédula inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return;
            }

            if (BLL_RegistroAgricultor.ExisteCedulaAgricultor(cedula))
            {
                MessageBox.Show("Ya existe un agricultor registrado con esta cédula.",
                    "Cédula duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCedula.Focus();
                return;
            }

            string usuario = txtUsuario.Text.Trim();
            if (BLL_RegistroAgricultor.ExisteUsuario(usuario))
            {
                MessageBox.Show("Ya existe un agricultor registrado con este usuario. Genere credenciales nuevamente.",
                    "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            Entitie_DatosCedula? datosCedula = BLL_RegistroAgricultor.ObtenerDatosCedula(cedula);
            if (datosCedula != null &&
                !BLL_RegistroAgricultor.CoincideNombreCedula(datosCedula, txtNombre.Text, txtApellido.Text))
            {
                txtNombre.Text = datosCedula.Nombre;
                txtApellido.Text = datosCedula.Apellido;
                MessageBox.Show("La cédula pertenece a otro nombre. Cargué el nombre y apellido verificados; revise y vuelva a guardar.",
                    "Nombre no coincide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                bool registrado = BLL_RegistroAgricultor.RegistrarAgricultor(
                    new Entitie_RegistroAgricultor
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Cedula = cedula,
                        Telefono = txtTelefono.Text,
                        Email = emailActual,
                        Direccion = txtDireccion.Text,
                        Ciudad = txtCiudad.Text,
                        Sector = txtSector.Text,
                        Usuario = usuario,
                        Contrasena = txtContraseña.Text
                    });

                if (!registrado)
                {
                    MessageBox.Show("No se pudo registrar porque el usuario o la cédula ya existen.",
                        "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mostrar mensaje de éxito
                MessageBox.Show("Agricultor registrado exitosamente. Use las credenciales generadas para iniciar sesión.",
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Si es el primer usuario, cerrar este formulario y mostrar el login
                if (isFirstUser)
                {
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    // Si no es el primer usuario, simplemente cerrar este formulario
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el agricultor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Si es el primer usuario, no permitir cancelar
            if (isFirstUser)
            {
                MessageBox.Show("Debe completar el registro inicial para usar el sistema.",
                    "Registro obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            // Si es el primer usuario, no permitir volver
            if (isFirstUser)
            {
                MessageBox.Show("Debe completar el registro inicial para usar el sistema.",
                    "Registro obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Close();
        }
    }
}

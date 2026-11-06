using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.UI
{
    public class FrmAdministrador : Form
    {
        private readonly bool esPrimerRegistro;
        private bool correoVerificado;
        private bool mostrandoContrasena;
        private string correoVerificadoActual = string.Empty;
        private string codigoVerificacion = string.Empty;
        private DateTime codigoExpira = DateTime.MinValue;

        private TextBox txtNombre = null!;
        private TextBox txtApellido = null!;
        private TextBox txtEmail = null!;
        private TextBox txtTelefono = null!;
        private TextBox txtCodigoEmail = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;
        private TextBox txtConfirmarContrasena = null!;
        private Label lblEstadoEmail = null!;
        private Label lblMensaje = null!;
        private Button btnEnviarCodigo = null!;
        private Button btnConfirmarCodigo = null!;
        private Button btnMostrarContrasena = null!;
        private Button btnConfigurarCorreo = null!;
        private Button btnGuardar = null!;

        public FrmAdministrador()
            : this(false)
        {
        }

        public FrmAdministrador(bool esPrimerRegistro)
        {
            this.esPrimerRegistro = esPrimerRegistro;
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            Text = esPrimerRegistro ? "Registro inicial de administrador" : "Registro de administrador";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(980, 650);
            MinimumSize = new Size(900, 620);
            BackColor = Color.FromArgb(244, 247, 245);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            AutoScaleMode = AutoScaleMode.Dpi;
            DoubleBuffered = true;
            KeyPreview = true;

            Panel encabezado = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.FromArgb(31, 92, 72),
                Padding = new Padding(28, 13, 24, 10)
            };

            Label lblTitulo = new Label
            {
                Text = esPrimerRegistro ? "Crear administrador del sistema" : "Registrar administrador",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(28, 12)
            };

            Label lblSubtitulo = new Label
            {
                Text = "Cuenta principal para administrar el Sistema Agrícola",
                ForeColor = Color.FromArgb(221, 238, 231),
                Font = new Font("Segoe UI", 9.5F),
                AutoSize = true,
                Location = new Point(30, 48)
            };

            encabezado.Controls.Add(lblTitulo);
            encabezado.Controls.Add(lblSubtitulo);

            Panel contenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 16, 24, 14),
                BackColor = Color.FromArgb(244, 247, 245),
                AutoScroll = true
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(0),
                BackColor = Color.FromArgb(244, 247, 245)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, esPrimerRegistro ? 50 : 0));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));

            Label lblInfo = new Label
            {
                Dock = DockStyle.Fill,
                Visible = esPrimerRegistro,
                Text = "No hay administrador registrado. Complete el registro para habilitar el acceso inicial.",
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 14, 0),
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.FromArgb(255, 248, 225),
                ForeColor = Color.FromArgb(121, 85, 0),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F)
            };

            TableLayoutPanel contenido = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            TableLayoutPanel columnaIzquierda = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(0)
            };
            columnaIzquierda.RowStyles.Add(new RowStyle(SizeType.Absolute, 174));
            columnaIzquierda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            GroupBox grupoDatos = CrearGrupo("Datos del administrador");
            TableLayoutPanel tablaDatos = CrearTabla(2, 2);
            tablaDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tablaDatos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            txtNombre = CrearTextBox("Nombre");
            txtApellido = CrearTextBox("Apellido");
            txtTelefono = CrearTextBox("Teléfono");
            txtTelefono.MaxLength = 15;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            txtNombre.TabIndex = 0;
            txtApellido.TabIndex = 1;
            txtTelefono.TabIndex = 2;

            AgregarCampo(tablaDatos, 0, 0, "Nombre", txtNombre);
            AgregarCampo(tablaDatos, 1, 0, "Apellido", txtApellido);
            Panel campoTelefono = AgregarCampo(tablaDatos, 0, 1, "Teléfono", txtTelefono);
            tablaDatos.SetColumnSpan(campoTelefono, 2);
            grupoDatos.Controls.Add(tablaDatos);

            GroupBox grupoCorreo = CrearGrupo("Confirmación del correo");
            TableLayoutPanel tablaCorreo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                Padding = new Padding(16, 8, 16, 8),
                Margin = new Padding(0)
            };

            tablaCorreo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tablaCorreo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            tablaCorreo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tablaCorreo.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            tablaCorreo.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            tablaCorreo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtEmail = CrearTextBox("correo@gmail.com");
            txtEmail.TextChanged += txtEmail_TextChanged;
            txtEmail.TabIndex = 3;
            Panel campoEmail = AgregarCampo(tablaCorreo, 0, 0, "Correo electrónico", txtEmail);
            tablaCorreo.SetColumnSpan(campoEmail, 2);

            btnEnviarCodigo = CrearBoton("Enviar código", Color.FromArgb(39, 110, 91), 124);
            btnEnviarCodigo.TabIndex = 4;
            btnEnviarCodigo.Click += btnEnviarCodigo_Click;
            tablaCorreo.Controls.Add(CrearPanelBoton(btnEnviarCodigo, 19), 2, 0);

            txtCodigoEmail = CrearTextBox("000000");
            txtCodigoEmail.MaxLength = 6;
            txtCodigoEmail.TextAlign = HorizontalAlignment.Center;
            txtCodigoEmail.KeyPress += txtCodigoEmail_KeyPress;
            txtCodigoEmail.TabIndex = 5;
            AgregarCampo(tablaCorreo, 0, 1, "Código recibido", txtCodigoEmail);

            btnConfirmarCodigo = CrearBoton("Confirmar", Color.FromArgb(46, 125, 50), 112);
            btnConfirmarCodigo.TabIndex = 6;
            btnConfirmarCodigo.Click += btnConfirmarCodigo_Click;
            tablaCorreo.Controls.Add(CrearPanelBoton(btnConfirmarCodigo, 19), 1, 1);

            lblEstadoEmail = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Gmail no verificado",
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(183, 28, 28),
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Padding = new Padding(2, 4, 0, 0),
                AutoEllipsis = true
            };

            tablaCorreo.Controls.Add(lblEstadoEmail, 0, 2);
            tablaCorreo.SetColumnSpan(lblEstadoEmail, 3);
            grupoCorreo.Controls.Add(tablaCorreo);

            columnaIzquierda.Controls.Add(grupoDatos, 0, 0);
            columnaIzquierda.Controls.Add(grupoCorreo, 0, 1);

            GroupBox grupoCredenciales = CrearGrupo("Credenciales de acceso");
            grupoCredenciales.Margin = new Padding(8, 0, 0, 0);
            TableLayoutPanel tablaCredenciales = CrearTabla(2, 5);
            tablaCredenciales.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            tablaCredenciales.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            tablaCredenciales.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            tablaCredenciales.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            tablaCredenciales.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtUsuario = CrearTextBox("Usuario");
            txtContrasena = CrearTextBox("Contraseña");
            txtConfirmarContrasena = CrearTextBox("Confirmar contraseña");
            txtContrasena.UseSystemPasswordChar = true;
            txtConfirmarContrasena.UseSystemPasswordChar = true;
            txtContrasena.MaxLength = BLL_PoliticaContrasena.LongitudMaxima;
            txtConfirmarContrasena.MaxLength = BLL_PoliticaContrasena.LongitudMaxima;
            txtUsuario.TabIndex = 7;
            txtContrasena.TabIndex = 9;
            txtConfirmarContrasena.TabIndex = 10;

            AgregarCampo(tablaCredenciales, 0, 0, "Usuario", txtUsuario);

            Button btnGenerar = CrearBoton("Generar credenciales", Color.FromArgb(73, 96, 112), 170);
            btnGenerar.TabIndex = 8;
            btnGenerar.Click += btnGenerar_Click;
            tablaCredenciales.Controls.Add(CrearPanelBoton(btnGenerar, 20), 1, 0);

            Panel campoContrasena = AgregarCampo(tablaCredenciales, 0, 1, "Contraseña temporal", txtContrasena);
            tablaCredenciales.SetColumnSpan(campoContrasena, 2);
            Panel campoConfirmacion = AgregarCampo(tablaCredenciales, 0, 2, "Confirmar contraseña", txtConfirmarContrasena);
            tablaCredenciales.SetColumnSpan(campoConfirmacion, 2);

            btnMostrarContrasena = CrearBoton("Mostrar contraseñas", Color.FromArgb(96, 106, 116), 174);
            btnMostrarContrasena.TabIndex = 11;
            btnMostrarContrasena.Click += btnMostrarContrasena_Click;
            Panel panelMostrar = CrearPanelBoton(btnMostrarContrasena, 4);
            tablaCredenciales.Controls.Add(panelMostrar, 0, 3);
            tablaCredenciales.SetColumnSpan(panelMostrar, 2);

            Label lblPolitica = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Use una contraseña segura. En el primer ingreso se solicitará cambiar la contraseña temporal.",
                ForeColor = Color.FromArgb(92, 103, 110),
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(2, 8, 12, 0),
                AutoEllipsis = true
            };
            tablaCredenciales.Controls.Add(lblPolitica, 0, 4);
            tablaCredenciales.SetColumnSpan(lblPolitica, 2);
            grupoCredenciales.Controls.Add(tablaCredenciales);

            contenido.Controls.Add(columnaIzquierda, 0, 0);
            contenido.Controls.Add(grupoCredenciales, 1, 0);

            lblMensaje = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(183, 28, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(4, 6, 10, 0),
                AutoEllipsis = true
            };

            FlowLayoutPanel panelAcciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0),
                Margin = new Padding(0)
            };

            btnGuardar = CrearBoton("Guardar administrador", Color.FromArgb(31, 111, 78), 184);
            btnGuardar.Enabled = false;
            Button btnCancelar = CrearBoton(esPrimerRegistro ? "Salir" : "Cancelar", Color.FromArgb(120, 120, 120), 120);
            btnConfigurarCorreo = CrearBoton("Configurar Gmail", Color.FromArgb(52, 101, 164), 158);
            btnConfigurarCorreo.Visible = esPrimerRegistro;
            btnConfigurarCorreo.TabIndex = 12;
            btnCancelar.TabIndex = 13;
            btnGuardar.TabIndex = 14;

            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += (sender, args) => Close();
            btnConfigurarCorreo.Click += btnConfigurarCorreo_Click;

            panelAcciones.Controls.Add(btnGuardar);
            panelAcciones.Controls.Add(btnCancelar);
            panelAcciones.Controls.Add(btnConfigurarCorreo);

            layout.Controls.Add(lblInfo, 0, 0);
            layout.Controls.Add(contenido, 0, 1);
            layout.Controls.Add(lblMensaje, 0, 2);
            layout.Controls.Add(panelAcciones, 0, 3);

            contenedor.Controls.Add(layout);
            Controls.Add(contenedor);
            Controls.Add(encabezado);

            HabilitarConfirmacionCorreo(false);
            if (esPrimerRegistro && !CorreoSalienteConfigurado())
            {
                ActualizarEstadoCorreo(
                    "Configure el Gmail remitente antes de enviar el código.",
                    Color.FromArgb(178, 112, 0));
            }

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private void btnConfigurarCorreo_Click(object? sender, EventArgs e)
        {
            using FrmConfiguracionSistema formulario = new FrmConfiguracionSistema(
                abrirPestanaCorreo: true,
                configuracionInicial: esPrimerRegistro)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            formulario.ShowDialog(this);

            if (CorreoSalienteConfigurado())
            {
                ActualizarEstadoCorreo(
                    "Gmail remitente configurado. Ya puede enviar el código.",
                    Color.FromArgb(36, 110, 60));
                MostrarMensaje("Configuración de Gmail aplicada correctamente.", false);
            }
            else
            {
                ActualizarEstadoCorreo(
                    "Falta configurar el Gmail remitente y su clave de aplicación.",
                    Color.FromArgb(178, 112, 0));
            }
        }

        private static bool CorreoSalienteConfigurado()
        {
            SistemaConfiguracion config = SistemaConfiguracionServicio.Cargar();
            return !string.IsNullOrWhiteSpace(config.GmailRemitente)
                && !string.IsNullOrWhiteSpace(config.GmailClaveAplicacion);
        }

        private GroupBox CrearGrupo(string titulo)
        {
            return new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = titulo,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(48, 70, 62),
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private TableLayoutPanel CrearTabla(int columnas, int filas)
        {
            TableLayoutPanel tabla = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = columnas,
                RowCount = filas,
                Padding = new Padding(16, 8, 16, 8),
                Margin = new Padding(0)
            };

            for (int i = 0; i < columnas; i++)
            {
                tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnas));
            }

            return tabla;
        }

        private TextBox CrearTextBox(string placeholder, bool esContrasena = false)
        {
            return new TextBox
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = placeholder,
                PasswordChar = esContrasena ? '*' : '\0',
                Font = new Font("Segoe UI", 10F),
                Height = 30,
                Margin = new Padding(0, 0, 8, 0)
            };
        }

        private Button CrearBoton(string texto, Color color, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = ancho,
                Height = 36,
                Margin = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };

            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(color);
            return boton;
        }

        private Panel AgregarCampo(
            TableLayoutPanel tabla,
            int columna,
            int fila,
            string etiqueta,
            Control control)
        {
            Panel panelCampo = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 10, 2),
                Padding = new Padding(0)
            };

            Label lbl = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                Height = 21,
                ForeColor = Color.FromArgb(70, 80, 90),
                Font = new Font("Segoe UI", 9F)
            };

            panelCampo.Controls.Add(control);
            panelCampo.Controls.Add(lbl);
            tabla.Controls.Add(panelCampo, columna, fila);
            return panelCampo;
        }

        private Panel CrearPanelBoton(Button boton, int margenSuperior)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, margenSuperior, 0, 0),
                Margin = new Padding(0)
            };

            boton.Dock = DockStyle.Left;
            panel.Controls.Add(boton);
            return panel;
        }

        private void btnGenerar_Click(object? sender, EventArgs e)
        {
            try
            {
                string usuario = BLL_Administrador.GenerarUsuarioDisponible(txtNombre.Text, txtApellido.Text);
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    MostrarMensaje("Ingrese nombre y apellido para generar el usuario.");
                    return;
                }

                string contrasena = BLL_Administrador.GenerarContrasenaTemporal();
                txtUsuario.Text = usuario;
                txtContrasena.Text = contrasena;
                txtConfirmarContrasena.Text = contrasena;
                MostrarMensaje("Credenciales generadas correctamente.", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudieron generar las credenciales: " + ex.Message);
            }
        }

        private void btnEnviarCodigo_Click(object? sender, EventArgs e)
        {
            string nombreCompleto = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}".Trim();
            btnEnviarCodigo.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                ResultadoSolicitudVerificacionCorreo resultado =
                    BLL_VerificacionCorreoAgricultor.SolicitarCodigoAdministrador(
                        txtEmail.Text,
                        nombreCompleto);

                if (!resultado.Exitoso)
                {
                    ReiniciarVerificacionCorreo(resultado.Mensaje);
                    MostrarMensaje(resultado.Mensaje);
                    return;
                }

                txtEmail.Text = resultado.CorreoNormalizado;
                correoVerificado = false;
                correoVerificadoActual = resultado.CorreoNormalizado;
                codigoVerificacion = resultado.Codigo;
                codigoExpira = resultado.Expira;

                HabilitarConfirmacionCorreo(true);
                btnGuardar.Enabled = false;
                ActualizarEstadoCorreo("Código enviado. Revise su Gmail.", Color.FromArgb(178, 112, 0));
                MostrarMensaje(resultado.Mensaje, false);
                txtCodigoEmail.Focus();
            }
            finally
            {
                Cursor = Cursors.Default;
                btnEnviarCodigo.Enabled = true;
            }
        }

        private void btnConfirmarCodigo_Click(object? sender, EventArgs e)
        {
            ResultadoOperacionRecuperacion resultado =
                BLL_VerificacionCorreoAgricultor.ValidarCodigo(
                    txtCodigoEmail.Text,
                    codigoVerificacion,
                    codigoExpira);

            if (!resultado.Exitoso)
            {
                ActualizarEstadoCorreo(resultado.Mensaje, Color.FromArgb(183, 28, 28));
                MostrarMensaje(resultado.Mensaje);
                return;
            }

            correoVerificado = true;
            correoVerificadoActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);
            HabilitarConfirmacionCorreo(false);
            btnGuardar.Enabled = true;
            ActualizarEstadoCorreo("Gmail verificado correctamente", Color.FromArgb(36, 110, 60));
            MostrarMensaje(resultado.Mensaje, false);
        }

        private void btnMostrarContrasena_Click(object? sender, EventArgs e)
        {
            mostrandoContrasena = !mostrandoContrasena;
            txtContrasena.UseSystemPasswordChar = !mostrandoContrasena;
            txtConfirmarContrasena.UseSystemPasswordChar = !mostrandoContrasena;
            btnMostrarContrasena.Text = mostrandoContrasena
                ? "Ocultar contraseñas"
                : "Mostrar contraseñas";
        }

        private void txtEmail_TextChanged(object? sender, EventArgs e)
        {
            string correoActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);
            if (correoVerificado &&
                string.Equals(correoActual, correoVerificadoActual, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!string.IsNullOrEmpty(codigoVerificacion) &&
                string.Equals(correoActual, correoVerificadoActual, StringComparison.OrdinalIgnoreCase))
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

        private void txtTelefono_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ReiniciarVerificacionCorreo(string mensaje)
        {
            correoVerificado = false;
            correoVerificadoActual = string.Empty;
            codigoVerificacion = string.Empty;
            codigoExpira = DateTime.MinValue;
            txtCodigoEmail.Text = string.Empty;
            btnGuardar.Enabled = false;
            HabilitarConfirmacionCorreo(false);
            ActualizarEstadoCorreo(mensaje, Color.FromArgb(183, 28, 28));
        }

        private void HabilitarConfirmacionCorreo(bool habilitar)
        {
            txtCodigoEmail.Enabled = habilitar;
            btnConfirmarCodigo.Enabled = habilitar;
        }

        private void ActualizarEstadoCorreo(string mensaje, Color color)
        {
            lblEstadoEmail.Text = mensaje;
            lblEstadoEmail.ForeColor = color;
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string correoActual = BLL_VerificacionCorreoAgricultor.NormalizarCorreo(txtEmail.Text);
            if (!correoVerificado ||
                !string.Equals(correoActual, correoVerificadoActual, StringComparison.OrdinalIgnoreCase))
            {
                MostrarMensaje("Debe verificar el Gmail del administrador antes de guardar.");
                txtEmail.Focus();
                return;
            }

            try
            {
                int idAdministrador = BLL_Administrador.RegistrarAdministrador(
                    txtNombre.Text,
                    txtApellido.Text,
                    correoActual,
                    txtTelefono.Text,
                    txtUsuario.Text,
                    txtContrasena.Text,
                    txtConfirmarContrasena.Text);

                MessageBox.Show(
                    "Administrador registrado correctamente. ID: " + idAdministrador,
                    "Registro completado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError = true)
        {
            lblMensaje.ForeColor = esError
                ? Color.FromArgb(183, 28, 28)
                : Color.FromArgb(46, 125, 50);
            lblMensaje.Text = mensaje;
        }
    }
}

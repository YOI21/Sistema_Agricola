using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;

namespace Sistema_Agricola.UI
{
    public partial class FrmConfiguracionSistema : Form
    {
        private readonly bool abrirPestanaCorreo;
        private readonly bool configuracionInicial;
        private readonly TextBox txtNombreSistema = new TextBox();
        private readonly TextBox txtNombreFinca = new TextBox();
        private readonly TextBox txtPropietario = new TextBox();
        private readonly TextBox txtTelefono = new TextBox();
        private readonly TextBox txtDireccion = new TextBox();
        private readonly ComboBox cmbMoneda = new ComboBox();
        private readonly NumericUpDown nudDiasAlerta = new NumericUpDown();
        private readonly NumericUpDown nudStockMinimo = new NumericUpDown();
        private readonly CheckBox chkAlertasGmail = new CheckBox();
        private readonly CheckBox chkAlertasAutomaticas = new CheckBox();
        private readonly TextBox txtGmail = new TextBox();
        private readonly TextBox txtClaveApp = new TextBox();
        private readonly TextBox txtNombreRemitente = new TextBox();
        private readonly TextBox txtSmtpHost = new TextBox();
        private readonly NumericUpDown nudSmtpPuerto = new NumericUpDown();
        private readonly CheckBox chkSmtpSsl = new CheckBox();
        private readonly TextBox txtCorreoPrueba = new TextBox();
        private readonly Label lblEstado = new Label();
        private readonly Label lblRuta = new Label();

        private SistemaConfiguracion configuracion = new SistemaConfiguracion();

        public FrmConfiguracionSistema(
            bool abrirPestanaCorreo = false,
            bool configuracionInicial = false)
        {
            this.abrirPestanaCorreo = abrirPestanaCorreo;
            this.configuracionInicial = configuracionInicial;
            InitializeComponent();
            ConstruirInterfaz();
            Load += FrmConfiguracionSistema_Load;
        }

        private void FrmConfiguracionSistema_Load(object? sender, EventArgs e)
        {
            configuracion = SistemaConfiguracionServicio.Cargar();
            CargarEnFormulario(configuracion);
        }

        private void ConstruirInterfaz()
        {
            FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1060, 660);
            MinimumSize = new Size(900, 580);
            BackColor = Color.FromArgb(244, 247, 250);
            Font = new Font("Segoe UI", 9F);

            TableLayoutPanel contenedor = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(14),
                BackColor = BackColor
            };
            contenedor.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            contenedor.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            contenedor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            contenedor.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            contenedor.Controls.Add(CrearEncabezado(), 0, 0);
            contenedor.Controls.Add(CrearTabs(), 0, 1);
            contenedor.Controls.Add(CrearPanelEstado(), 0, 2);
            contenedor.Controls.Add(CrearPanelAcciones(), 0, 3);
            Controls.Add(contenedor);
        }

        private Control CrearEncabezado()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(38, 92, 74)
            };

            Label titulo = new Label
            {
                Text = "Configuracion del Sistema",
                AutoSize = true,
                Location = new Point(16, 14),
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.White
            };

            Button cerrar = CrearBoton("X", Color.FromArgb(176, 58, 46), 36);
            cerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cerrar.Location = new Point(ClientSize.Width - 52, 11);
            cerrar.Click += (_, _) => Close();
            panel.Resize += (_, _) =>
                cerrar.Location = new Point(panel.Width - cerrar.Width - 14, 11);

            panel.Controls.Add(titulo);
            panel.Controls.Add(cerrar);
            return panel;
        }

        private Control CrearTabs()
        {
            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
            };

            tabs.TabPages.Add(CrearTabGeneral());
            tabs.TabPages.Add(CrearTabCorreo());
            tabs.TabPages.Add(CrearTabParametros());
            if (abrirPestanaCorreo)
            {
                tabs.SelectedIndex = 1;
            }

            return tabs;
        }

        private TabPage CrearTabGeneral()
        {
            TabPage tab = new TabPage("General") { BackColor = Color.White };
            TableLayoutPanel tabla = CrearTablaFormulario();

            ConfigurarTexto(txtNombreSistema, "Sistema Agricola");
            ConfigurarTexto(txtNombreFinca, "Nombre de la finca");
            ConfigurarTexto(txtPropietario, "Propietario o administrador");
            ConfigurarTexto(txtTelefono, "Telefono");
            ConfigurarTexto(txtDireccion, "Direccion o ubicacion");
            ConfigurarCombo(cmbMoneda);
            cmbMoneda.Items.AddRange(new object[] { "RD$", "$", "USD", "EUR" });

            AgregarCampo(tabla, "Nombre del sistema", txtNombreSistema, 0);
            AgregarCampo(tabla, "Nombre de finca", txtNombreFinca, 1);
            AgregarCampo(tabla, "Propietario", txtPropietario, 2);
            AgregarCampo(tabla, "Telefono", txtTelefono, 3);
            AgregarCampo(tabla, "Direccion", txtDireccion, 4);
            AgregarCampo(tabla, "Moneda", cmbMoneda, 5);
            tab.Controls.Add(tabla);
            return tab;
        }

        private TabPage CrearTabCorreo()
        {
            TabPage tab = new TabPage("Correo y notificaciones") { BackColor = Color.White };
            TableLayoutPanel tabla = CrearTablaFormulario();

            ConfigurarTexto(txtGmail, "correo@gmail.com");
            ConfigurarTexto(txtClaveApp, "Clave de aplicacion de Gmail");
            txtClaveApp.UseSystemPasswordChar = true;
            ConfigurarTexto(txtNombreRemitente, "Nombre remitente");
            ConfigurarTexto(txtSmtpHost, "smtp.gmail.com");
            ConfigurarTexto(txtCorreoPrueba, "correo de prueba");
            ConfigurarNumero(nudSmtpPuerto, 1, 65535, 587);
            txtSmtpHost.ReadOnly = true;
            txtSmtpHost.TabStop = false;
            nudSmtpPuerto.Enabled = false;

            chkSmtpSsl.Text = "Usar SSL";
            chkSmtpSsl.AutoSize = true;
            chkSmtpSsl.Checked = true;
            chkSmtpSsl.Enabled = false;
            chkAlertasGmail.Text = "Enviar alertas por Gmail";
            chkAlertasGmail.AutoSize = true;

            Button btnMostrarClave = CrearBoton("Mostrar", Color.FromArgb(95, 106, 106), 92);
            btnMostrarClave.Click += (_, _) =>
            {
                txtClaveApp.UseSystemPasswordChar = !txtClaveApp.UseSystemPasswordChar;
                btnMostrarClave.Text = txtClaveApp.UseSystemPasswordChar ? "Mostrar" : "Ocultar";
            };

            Button btnProbar = CrearBoton("Guardar y probar", Color.FromArgb(52, 101, 164), 138);
            btnProbar.Click += (_, _) => ProbarCorreo();

            AgregarCampo(tabla, "Gmail remitente", txtGmail, 0);
            AgregarCampoConBoton(tabla, "Clave de aplicacion", txtClaveApp, btnMostrarClave, 1);
            AgregarCampo(tabla, "Nombre remitente", txtNombreRemitente, 2);
            AgregarCampo(tabla, "Servidor SMTP", txtSmtpHost, 3);
            AgregarCampo(tabla, "Puerto SMTP", nudSmtpPuerto, 4);
            AgregarControlLinea(tabla, chkSmtpSsl, 5);
            AgregarControlLinea(tabla, chkAlertasGmail, 6);
            AgregarCampoConBoton(tabla, "Correo de prueba", txtCorreoPrueba, btnProbar, 7);

            Label nota = new Label
            {
                Text = "Use la clave de aplicacion de Google, no la contrasena normal de Gmail. Al probarla correctamente quedara guardada y cifrada en este equipo.",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(92, 104, 116),
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(8, 8, 8, 0)
            };
            tabla.Controls.Add(nota, 1, 8);
            tab.Controls.Add(tabla);
            return tab;
        }

        private TabPage CrearTabParametros()
        {
            TabPage tab = new TabPage("Parametros") { BackColor = Color.White };
            TableLayoutPanel tabla = CrearTablaFormulario();

            ConfigurarNumero(nudDiasAlerta, 0, 60, 7);
            ConfigurarNumero(nudStockMinimo, 0, 100000, 10);
            chkAlertasAutomaticas.Text = "Crear alertas automaticas al registrar siembras";
            chkAlertasAutomaticas.AutoSize = true;

            lblRuta.Text = SistemaConfiguracionServicio.RutaArchivo;
            lblRuta.Dock = DockStyle.Fill;
            lblRuta.ForeColor = Color.FromArgb(92, 104, 116);
            lblRuta.Font = new Font("Segoe UI", 8.5F);
            lblRuta.Padding = new Padding(8, 8, 8, 0);

            AgregarCampo(tabla, "Dias antes de alertar", nudDiasAlerta, 0);
            AgregarCampo(tabla, "Stock minimo general", nudStockMinimo, 1);
            AgregarControlLinea(tabla, chkAlertasAutomaticas, 2);
            AgregarEtiquetaValor(tabla, "Archivo local", lblRuta, 3);
            tab.Controls.Add(tabla);
            return tab;
        }

        private Control CrearPanelEstado()
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, BackColor = BackColor };
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(4, 8);
            lblEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(62, 73, 88);
            lblEstado.Text = "Listo";
            panel.Controls.Add(lblEstado);
            return panel;
        }

        private Control CrearPanelAcciones()
        {
            Panel panel = new Panel { Dock = DockStyle.Fill, BackColor = BackColor };
            Button btnGuardar = CrearBoton("Guardar", Color.FromArgb(45, 125, 94), 96);
            Button btnRestaurar = CrearBoton("Restaurar", Color.FromArgb(95, 106, 106), 96);
            Button btnVolver = CrearBoton("Volver", Color.FromArgb(176, 58, 46), 86);

            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRestaurar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnGuardar.Click += (_, _) => Guardar();
            btnRestaurar.Click += (_, _) => CargarEnFormulario(new SistemaConfiguracion());
            btnVolver.Click += (_, _) => Close();

            panel.Resize += (_, _) =>
            {
                btnVolver.Location = new Point(panel.Width - btnVolver.Width, 5);
                btnRestaurar.Location = new Point(btnVolver.Left - btnRestaurar.Width - 8, 5);
                btnGuardar.Location = new Point(btnRestaurar.Left - btnGuardar.Width - 8, 5);
            };

            panel.Controls.Add(btnGuardar);
            panel.Controls.Add(btnRestaurar);
            panel.Controls.Add(btnVolver);
            return panel;
        }

        private void CargarEnFormulario(SistemaConfiguracion config)
        {
            txtNombreSistema.Text = config.NombreSistema;
            txtNombreFinca.Text = config.NombreFinca;
            txtPropietario.Text = config.Propietario;
            txtTelefono.Text = config.Telefono;
            txtDireccion.Text = config.Direccion;
            SeleccionarCombo(cmbMoneda, config.Moneda);
            nudDiasAlerta.Value = Ajustar(config.DiasAlertaAnticipacion, nudDiasAlerta.Minimum, nudDiasAlerta.Maximum);
            nudStockMinimo.Value = Ajustar(config.StockMinimoGeneral, nudStockMinimo.Minimum, nudStockMinimo.Maximum);
            chkAlertasGmail.Checked = config.NotificarAlertasGmail;
            chkAlertasAutomaticas.Checked = config.CrearAlertasAutomaticas;
            txtGmail.Text = config.GmailRemitente;
            txtClaveApp.Text = config.GmailClaveAplicacion;
            txtNombreRemitente.Text = config.NombreRemitente;
            txtSmtpHost.Text = "smtp.gmail.com";
            nudSmtpPuerto.Value = 587;
            chkSmtpSsl.Checked = true;
            txtCorreoPrueba.Text = config.GmailRemitente;
            lblEstado.Text = "Configuracion cargada";
        }

        private void Guardar()
        {
            try
            {
                SistemaConfiguracion config = CrearDesdeFormulario();
                Validar(config);
                string detalle =
                    $"Sistema={config.NombreSistema}; Gmail={config.GmailRemitente}; SMTP={config.SmtpHost}:{config.SmtpPuerto}";
                Action persistir = () => SistemaConfiguracionServicio.Guardar(config);

                if (configuracionInicial)
                {
                    BLL_ConfiguracionSistema.GuardarInicial(detalle, persistir);
                }
                else
                {
                    BLL_ConfiguracionSistema.Guardar(detalle, persistir);
                }
                configuracion = config;
                lblEstado.Text = "Configuracion guardada y aplicada";
                lblEstado.ForeColor = Color.FromArgb(36, 110, 60);
                MessageBox.Show(
                    "La configuracion fue guardada correctamente.",
                    "Configuracion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private SistemaConfiguracion CrearDesdeFormulario()
        {
            return new SistemaConfiguracion
            {
                NombreSistema = txtNombreSistema.Text.Trim(),
                NombreFinca = txtNombreFinca.Text.Trim(),
                Propietario = txtPropietario.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Moneda = cmbMoneda.SelectedItem?.ToString() ?? "RD$",
                DiasAlertaAnticipacion = (int)nudDiasAlerta.Value,
                StockMinimoGeneral = (int)nudStockMinimo.Value,
                NotificarAlertasGmail = chkAlertasGmail.Checked,
                CrearAlertasAutomaticas = chkAlertasAutomaticas.Checked,
                GmailRemitente = txtGmail.Text.Trim(),
                GmailClaveAplicacion = txtClaveApp.Text.Replace(" ", string.Empty).Trim(),
                NombreRemitente = txtNombreRemitente.Text.Trim(),
                SmtpHost = "smtp.gmail.com",
                SmtpPuerto = 587,
                SmtpSsl = true
            };
        }

        private static void Validar(SistemaConfiguracion config)
        {
            if (string.IsNullOrWhiteSpace(config.NombreSistema))
            {
                throw new InvalidOperationException("Ingrese el nombre del sistema.");
            }

            if (!string.IsNullOrWhiteSpace(config.GmailRemitente) &&
                !config.GmailRemitente.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase) &&
                !config.GmailRemitente.EndsWith("@googlemail.com", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("El correo remitente debe ser una cuenta Gmail.");
            }

            if (!string.IsNullOrWhiteSpace(config.GmailRemitente) &&
                string.IsNullOrWhiteSpace(config.GmailClaveAplicacion))
            {
                throw new InvalidOperationException("Ingrese la clave de aplicacion de Gmail.");
            }

            if (!string.IsNullOrWhiteSpace(config.GmailClaveAplicacion) &&
                config.GmailClaveAplicacion.Length != 16)
            {
                throw new InvalidOperationException(
                    "La clave de aplicacion de Google debe tener 16 caracteres. Puede pegarla con o sin espacios.");
            }
        }

        private void ProbarCorreo()
        {
            try
            {
                SistemaConfiguracion config = CrearDesdeFormulario();
                Validar(config);

                if (string.IsNullOrWhiteSpace(config.GmailRemitente))
                {
                    throw new InvalidOperationException("Ingrese el Gmail remitente.");
                }

                string destinatario = string.IsNullOrWhiteSpace(txtCorreoPrueba.Text)
                    ? config.GmailRemitente
                    : txtCorreoPrueba.Text.Trim();
                string detalle =
                    $"Remitente={config.GmailRemitente}; Destinatario={destinatario}; SMTP={config.SmtpHost}:{config.SmtpPuerto}";
                Action enviar = () =>
                    BLL_ConfiguracionSistema.EnviarCorreoPrueba(
                        config.GmailRemitente,
                        config.GmailClaveAplicacion,
                        config.NombreRemitente,
                        config.SmtpHost,
                        config.SmtpPuerto,
                        config.SmtpSsl,
                        destinatario);

                if (configuracionInicial)
                {
                    BLL_ConfiguracionSistema.ProbarCorreoInicial(detalle, enviar);
                }
                else
                {
                    BLL_ConfiguracionSistema.ProbarCorreo(detalle, enviar);
                }

                Action persistir = () => SistemaConfiguracionServicio.Guardar(config);
                if (configuracionInicial)
                {
                    BLL_ConfiguracionSistema.GuardarInicial(detalle, persistir);
                }
                else
                {
                    BLL_ConfiguracionSistema.Guardar(detalle, persistir);
                }

                configuracion = config;
                lblEstado.Text = "Correo probado y configuracion guardada";
                lblEstado.ForeColor = Color.FromArgb(36, 110, 60);
                MessageBox.Show(
                    "Correo de prueba enviado. La clave quedo guardada y cifrada para este usuario de Windows.",
                    "Configuracion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo enviar el correo de prueba: " + ex.Message);
            }
        }

        private static TableLayoutPanel CrearTablaFormulario()
        {
            TableLayoutPanel tabla = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                ColumnCount = 2,
                RowCount = 12,
                Padding = new Padding(18, 16, 18, 16),
                BackColor = Color.White
            };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 12; i++)
            {
                tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            }

            return tabla;
        }

        private static void AgregarCampo(TableLayoutPanel tabla, string etiqueta, Control control, int fila)
        {
            AgregarEtiqueta(tabla, etiqueta, fila);
            control.Dock = DockStyle.Top;
            control.Height = 31;
            tabla.Controls.Add(control, 1, fila);
        }

        private static void AgregarCampoConBoton(TableLayoutPanel tabla, string etiqueta, Control control, Button boton, int fila)
        {
            AgregarEtiqueta(tabla, etiqueta, fila);
            TableLayoutPanel linea = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                Height = 34
            };
            linea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            linea.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, boton.Width + 8));
            control.Dock = DockStyle.Fill;
            boton.Dock = DockStyle.Fill;
            boton.Margin = new Padding(8, 0, 0, 0);
            linea.Controls.Add(control, 0, 0);
            linea.Controls.Add(boton, 1, 0);
            tabla.Controls.Add(linea, 1, fila);
        }

        private static void AgregarControlLinea(TableLayoutPanel tabla, Control control, int fila)
        {
            control.Dock = DockStyle.Top;
            control.Margin = new Padding(8, 9, 8, 0);
            tabla.Controls.Add(control, 1, fila);
        }

        private static void AgregarEtiquetaValor(TableLayoutPanel tabla, string etiqueta, Control control, int fila)
        {
            AgregarEtiqueta(tabla, etiqueta, fila);
            tabla.Controls.Add(control, 1, fila);
        }

        private static void AgregarEtiqueta(TableLayoutPanel tabla, string etiqueta, int fila)
        {
            Label label = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(64, 77, 91),
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
            };
            tabla.Controls.Add(label, 0, fila);
        }

        private static void ConfigurarTexto(TextBox texto, string placeholder)
        {
            texto.PlaceholderText = placeholder;
            texto.Font = new Font("Segoe UI", 9F);
            texto.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void ConfigurarCombo(ComboBox combo)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Font = new Font("Segoe UI", 9F);
        }

        private static void ConfigurarNumero(NumericUpDown numero, decimal minimo, decimal maximo, decimal valor)
        {
            numero.Minimum = minimo;
            numero.Maximum = maximo;
            numero.Value = valor;
            numero.Font = new Font("Segoe UI", 9F);
            numero.ThousandsSeparator = true;
        }

        private static Button CrearBoton(string texto, Color color, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                Width = ancho,
                Height = 32,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private static void SeleccionarCombo(ComboBox combo, string valor)
        {
            int indice = combo.Items.IndexOf(valor);
            combo.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private static decimal Ajustar(decimal valor, decimal minimo, decimal maximo)
        {
            return Math.Max(minimo, Math.Min(maximo, valor));
        }

        private void MostrarError(string mensaje)
        {
            lblEstado.Text = mensaje;
            lblEstado.ForeColor = Color.FromArgb(170, 40, 40);
            MessageBox.Show(mensaje, "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

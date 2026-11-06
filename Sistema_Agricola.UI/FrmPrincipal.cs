using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Timer = System.Windows.Forms.Timer;

namespace Sistema_Agricola.UI
{
    public partial class FrmPrincipal : Form
    {
        // Variables para controlar movimiento del formulario
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        // Variable para control de formulario activo
        private Form? activeForm;

        // Lista para controlar paneles de submenú
        private List<Panel> subMenus;

        public FrmPrincipal()
        {
            InitializeComponent();

            // Inicializar lista de submenús
            subMenus = new List<Panel>
            {
                panelSubMenuCultivos,
                panelSubMenuRecursos,
                panelSubMenuMaquinaria,
                panelSubMenuPersonal,
                panelSubMenuAdministracion,
                panelSubMenuMantenimiento,
                panelSubMenuReportes
            };

            // Ocultar todos los submenús al inicio
            OcultarSubMenus();

            // Configurar eventos para movimiento del formulario
            ConfigurarMovimientoFormulario();

            // Verificar alertas al iniciar el sistema
            VerificarAlertasInicio();
        }

        #region Métodos de Movimiento del Formulario

        private void ConfigurarMovimientoFormulario()
        {
            // Configurar evento de movimiento para el panel superior
            panelSuperior.MouseDown += PanelSuperior_MouseDown;
            panelSuperior.MouseMove += PanelSuperior_MouseMove;
            panelSuperior.MouseUp += PanelSuperior_MouseUp;

            // Asociar el evento también al título
            lblTituloPanel.MouseDown += PanelSuperior_MouseDown;
            lblTituloPanel.MouseMove += PanelSuperior_MouseMove;
            lblTituloPanel.MouseUp += PanelSuperior_MouseUp;
        }

        private void PanelSuperior_MouseDown(object? sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void PanelSuperior_MouseMove(object? sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void PanelSuperior_MouseUp(object? sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        #region Métodos de Menú y Navegación

        private void OcultarSubMenus()
        {
            foreach (Panel subMenu in subMenus)
            {
                subMenu.Visible = false;
            }
        }

        private void MostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                OcultarSubMenus();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void AbrirFormulario(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;

            Size originalClientSize = childForm.ClientSize;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScroll = true;
            childForm.AutoScrollMinSize = originalClientSize;

            panelCentral.SuspendLayout();
            panelCentral.Controls.Clear();
            panelCentral.Controls.Add(childForm);
            panelCentral.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            panelCentral.ResumeLayout();

            // Actualizar título del panel superior
            lblTituloPanel.Text = childForm.Text;
        }

        #endregion

        #region Configuración de Usuario e Interfaz

        private void ConfigurarInterfazSegunUsuario()
        {
            if (Session.IsLoggedIn())
            {
                Session.User usuario = Session.CurrentUser!;
                string rol = string.IsNullOrWhiteSpace(usuario.Rol)
                    ? "Usuario"
                    : usuario.Rol;
                string nombre = usuario.FullName;
                lblUsuarioActual.Text = $"{rol}: {nombre}";

                bool esAdministrador = Session.IsAdministrador();

                btnRecursos.Visible = esAdministrador;
                btnMaquinaria.Visible = esAdministrador;
                btnPersonal.Visible = esAdministrador;
                btnAdministracion.Visible = esAdministrador;
                btnMantenimiento.Visible = esAdministrador;
                btnReportes.Visible = esAdministrador;

                btnProcesoTerreno.Visible = esAdministrador;
                btnRegistroAgricultor.Visible = esAdministrador;
            }
        }

        private bool RequiereAdministrador()
        {
            if (Session.IsAdministrador())
            {
                return true;
            }

            MessageBox.Show(
                "Esta funcion solo esta disponible para el administrador.",
                "Permiso restringido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        #endregion

        #region Eventos Generales

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Iniciar el timer
            timer1.Enabled = true;

            // Configurar iconos de botones de ventana
            ConfigurarBotonesVentana();

            // Configurar interfaz según el usuario
            ConfigurarInterfazSegunUsuario();

            // Inicializar menú de Dashboard
            InitializeMenuDashboard();

            // Abrir Dashboard automáticamente al iniciar
            AbrirFormulario(new FrmDashboard());
            OcultarSubMenus();
        }

        private void ConfigurarBotonesVentana()
        {
            // Configurar botón Minimizar
            btnMinimizar.Paint += (s, e) =>
            {
                e.Graphics.Clear(Color.FromArgb(91, 124, 153));
                using (Pen pen = new Pen(Color.White, 2))
                {
                    // Dibujar guion (minimizar)
                    e.Graphics.DrawLine(pen, 8, 15, 22, 15);
                }
            };

            // Configurar botón Maximizar
            btnMaximizar.Paint += (s, e) =>
            {
                e.Graphics.Clear(Color.FromArgb(91, 124, 153));
                using (Pen pen = new Pen(Color.White, 2))
                {
                    // Dibujar cuadrado (maximizar)
                    e.Graphics.DrawRectangle(pen, 8, 8, 14, 14);
                }
            };

            // Configurar botón Cerrar
            btnCerrar.Paint += (s, e) =>
            {
                e.Graphics.Clear(Color.FromArgb(231, 76, 60));
                using (Pen pen = new Pen(Color.White, 2))
                {
                    // Dibujar X (cerrar)
                    e.Graphics.DrawLine(pen, 8, 8, 22, 22);
                    e.Graphics.DrawLine(pen, 22, 8, 8, 22);
                }
            };

            // Configurar botón Restaurar
            btnRestaurar.Paint += (s, e) =>
            {
                e.Graphics.Clear(Color.FromArgb(91, 124, 153));
                using (Pen pen = new Pen(Color.White, 2))
                {
                    // Dibujar dos cuadrados superpuestos (restaurar)
                    e.Graphics.DrawRectangle(pen, 10, 10, 12, 12);
                    e.Graphics.DrawRectangle(pen, 8, 8, 12, 12);
                }
            };

            // Efectos hover
            btnMinimizar.MouseEnter += (s, e) => { btnMinimizar.BackColor = Color.FromArgb(107, 140, 170); btnMinimizar.Invalidate(); };
            btnMinimizar.MouseLeave += (s, e) => { btnMinimizar.BackColor = Color.FromArgb(91, 124, 153); btnMinimizar.Invalidate(); };

            btnMaximizar.MouseEnter += (s, e) => { btnMaximizar.BackColor = Color.FromArgb(107, 140, 170); btnMaximizar.Invalidate(); };
            btnMaximizar.MouseLeave += (s, e) => { btnMaximizar.BackColor = Color.FromArgb(91, 124, 153); btnMaximizar.Invalidate(); };

            btnCerrar.MouseEnter += (s, e) => { btnCerrar.BackColor = Color.FromArgb(192, 57, 43); btnCerrar.Invalidate(); };
            btnCerrar.MouseLeave += (s, e) => { btnCerrar.BackColor = Color.FromArgb(231, 76, 60); btnCerrar.Invalidate(); };

            btnRestaurar.MouseEnter += (s, e) => { btnRestaurar.BackColor = Color.FromArgb(107, 140, 170); btnRestaurar.Invalidate(); };
            btnRestaurar.MouseLeave += (s, e) => { btnRestaurar.BackColor = Color.FromArgb(91, 124, 153); btnRestaurar.Invalidate(); };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Actualizar la hora en tiempo real
            lblFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #endregion

        #region Eventos de Controles de Ventana

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Preguntar si desea cerrar la aplicación
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            // Maximizar ventana y cambiar botones
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            // Restaurar ventana a tamaño normal y cambiar botones
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            // Minimizar ventana
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Eventos de Menú Principal

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDashboard());
            OcultarSubMenus();
        }

        private void btnCultivos_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuCultivos);
        }

        private void btnRecursos_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuRecursos);
        }

        private void btnGestionMaquinaria_Click_1(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuMaquinaria);
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuPersonal);
        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuAdministracion);
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuMantenimiento);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            MostrarSubMenu(panelSubMenuReportes);
        }

        #endregion

        #region Eventos de SubMenús

        // Submenú Cultivos
        private void btnSiembras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmSiembra());
            OcultarSubMenus();
        }

        private void btnCosechas_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCosechas());
            OcultarSubMenus();
        }

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProduccion());
            OcultarSubMenus();
        }

        private void btnSeguimiento_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmSeguimientoCrecimiento());
            OcultarSubMenus();
        }

        private void btnProcesoTe_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmRegistroTerreno());
            OcultarSubMenus();
        }

        private void btnControlPlagas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmControldePlagas());
            OcultarSubMenus();
        }

        private void btnAlertas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmAlertas());
            OcultarSubMenus();
        }

        // Submenú Recursos
        private void btnInsumos_Click_2(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmInventario());
            OcultarSubMenus();
        }

        // Submenú Maquinaria
        private void btnRegistroMaquinaria_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmGestionDeMaquinaria(0));
            OcultarSubMenus();
        }

        private void btnAsignacionMaquinaria_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmGestionDeMaquinaria(1));
            OcultarSubMenus();
        }

        // Submenú Personal
        private void btnRegistroTrabahjadores_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmTrabajadores());
            OcultarSubMenus();
        }

        private void btnAsignacionTrabajador_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmAgricultor());
            OcultarSubMenus();
        }

        // Submenú Administración
        private void btnFinanzas_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmFinanzas());
            OcultarSubMenus();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmInventario());
            OcultarSubMenus();
        }

        // Submenú Mantenimiento
        private void btnMantMaquinaria_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmGestionDeMaquinaria(2));
            OcultarSubMenus();
        }

        private void btnMantEquipos_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmMantenimientoEquipos());
            OcultarSubMenus();
        }

        private void btnConfigSistema_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmConfiguracionSistema());
            OcultarSubMenus();
        }

        // Submenú Reportes
        private void btnReportesCultivos_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmReporteSiembras());
            OcultarSubMenus();
        }

        private void btnReportProduccion_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmReporteProduccion());
            OcultarSubMenus();
        }

        private void btnReportCrecimiento_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmReporteCrecimiento());
            OcultarSubMenus();
        }

        private void btnReportFinanciero_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmReporteFinanciero());
            OcultarSubMenus();
        }

        private void btnReportInsumo_Click(object sender, EventArgs e)
        {
            if (!RequiereAdministrador()) return;
            AbrirFormulario(new FrmReporteInsumos());
            OcultarSubMenus();
        }

        #endregion

        #region Alertas y Sesión

        private void VerificarAlertasInicio()
        {
            try
            {
                // Verificar si hay alertas para hoy
                DataTable alertasHoy = BLL_Alertas.ObtenerAlertasProximas();

                if (alertasHoy != null && alertasHoy.Rows.Count > 0)
                {
                    // Preparar mensaje con las alertas para hoy
                    string mensaje = "Alertas pendientes para hoy:\n\n";

                    foreach (DataRow row in alertasHoy.Rows)
                    {
                        string nombreCultivo = row["Nombre_Cultivo"].ToString() ?? string.Empty;
                        string titulo = row["Titulo"].ToString() ?? string.Empty;
                        DateTime fechaProgramada = Convert.ToDateTime(row["Fecha_Programada"]);
                        int diasRestantes = Convert.ToInt32(row["Dias_Restantes"]);

                        mensaje += $"- {titulo} ({nombreCultivo})\n";
                        mensaje += $"  Fecha: {fechaProgramada.ToShortDateString()} ({diasRestantes} días)\n\n";
                    }

                    // Mostrar notificación con estilo moderno
                    ToastNotification notificacion = new ToastNotification(
                        "Alertas pendientes",
                        mensaje,
                        Color.FromArgb(231, 76, 60),
                        Color.White,
                        5000);

                    notificacion.Show();
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.ObtenerReferencia(
                    ex,
                    "verificar alertas al iniciar la sesión");
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Confirmar cierre de sesión
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Cerrar la sesión
                Session.Logout();

                // Mostrar el formulario de login
                Login loginForm = new Login();
                loginForm.Show();

                // Cerrar este formulario
                this.Close();
            }
        }

        #endregion

        #region Métodos Adicionales

        private void InitializeMenuDashboard()
        {
            // Ya tenemos configurado el botón de Dashboard en el diseño actualizado

            // Si es necesario, agregar botones adicionales programáticamente
        }

        #endregion

        // Clase auxiliar para notificaciones toast modernas
        public class ToastNotification : Form
        {
            private Timer closeTimer;

            public ToastNotification(string title, string message, Color backColor, Color foreColor, int displayDuration)
            {
                // Configurar apariencia
                this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = backColor;
                this.ForeColor = foreColor;
                this.Size = new Size(350, 150);
                this.StartPosition = FormStartPosition.Manual;
                this.TopMost = true;
                this.ShowInTaskbar = false;

                // Posicionar en la esquina inferior derecha
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(
                    workingArea.Right - this.Width - 20,
                    workingArea.Bottom - this.Height - 20);

                // Crear controles
                Label lblTitle = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = foreColor,
                    AutoSize = true,
                    Location = new Point(15, 15)
                };

                Label lblMessage = new Label
                {
                    Text = message,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = foreColor,
                    Location = new Point(15, 45),
                    Size = new Size(320, 90),
                    AutoEllipsis = true
                };

                Button btnClose = new Button
                {
                    Text = "×",
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = foreColor,
                    Size = new Size(25, 25),
                    Location = new Point(this.Width - 30, 5),
                    Cursor = Cursors.Hand
                };

                // Eliminar borde del botón
                btnClose.FlatAppearance.BorderSize = 0;

                // Agregar controlador de eventos para cerrar
                btnClose.Click += (s, e) => this.Close();

                // Agregar controles al formulario
                this.Controls.Add(lblTitle);
                this.Controls.Add(lblMessage);
                this.Controls.Add(btnClose);

                // Configurar timer para cerrar automáticamente
                closeTimer = new Timer();
                closeTimer.Interval = displayDuration;
                closeTimer.Tick += (s, e) =>
                {
                    closeTimer.Stop();
                    this.Close();
                };
                closeTimer.Start();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // Dibujar borde redondeado
                using (Pen pen = new Pen(Color.FromArgb(50, Color.Black), 1))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                }
            }
        }
    }
}

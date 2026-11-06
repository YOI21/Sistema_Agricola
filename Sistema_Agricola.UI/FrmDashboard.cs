using Timer = System.Windows.Forms.Timer;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmDashboard : Form
    {
        // Variables para los datos
        private DataTable dtCultivosActivos = new DataTable();
        private DataTable dtProximasCosechas = new DataTable();
        private DataTable dtSeguimientos = new DataTable();
        private DataTable dtAlertas = new DataTable();
        private DataTable dtCosechas = new DataTable();
        private Timer? refreshTimer;
        private readonly bool omitirCargaParaPruebaVisual;

        public FrmDashboard() : this(false)
        {
        }

        internal FrmDashboard(bool omitirCargaParaPruebaVisual)
        {
            this.omitirCargaParaPruebaVisual = omitirCargaParaPruebaVisual;
            InitializeComponent();
            MinimumSize = new Size(820, 480);
            AutoScroll = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            ConfigurarControlesSeguros();
            if (omitirCargaParaPruebaVisual)
            {
                return;
            }

            CargarDatos();
            ConfigurarRefreshTimer();
        }

        private void ConfigurarControlesSeguros()
        {
            this.BackColor = Color.WhiteSmoke;

            EjecutarConfiguracion("gráfico de cultivos", ConfigurarGraficoCultivos);
            EjecutarConfiguracion("gráfico de seguimiento", ConfigurarGraficoSeguimiento);
            EjecutarConfiguracion("gráfico de rendimientos", ConfigurarGraficoRendimientos);
            EjecutarConfiguracion("tablas del dashboard", FormatearDataGridViews);
        }

        private static void EjecutarConfiguracion(string componente, Action accion)
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo configurar {componente}: {ex.Message}");
            }
        }

        private void FormatearDataGridViews()
        {
            // Aplicar formato a todos los DataGridViews
            if (tabControlDatos == null)
            {
                return;
            }

            foreach (TabPage tab in tabControlDatos.TabPages)
            {
                foreach (Control control in tab.Controls)
                {
                    if (control is DataGridView dgv)
                    {
                        FormatearGrid(dgv);
                    }
                }
            }
        }

        private void ConfigurarRefreshTimer()
        {
            // Crear un timer para actualizar los datos cada cierto tiempo
            refreshTimer = new Timer();
            refreshTimer.Interval = 300000; // 5 minutos
            refreshTimer.Tick += (s, e) =>
            {
                if (!IsDisposed && !Disposing)
                {
                    CargarDatos();
                }
            };
            refreshTimer.Start();
        }

        // Mejorar el método CargarDatos para manejar mejor las excepciones
        private void CargarDatos()
        {
            if (IsDisposed || Disposing)
            {
                return;
            }

            // Actualizar la fecha y hora
            lblFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            try
            {
                // Cargar cultivos activos
                CargarCultivosActivos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar cultivos activos: " + ex.Message);
                lblCultivosActivosValor.Text = "Error";
            }

            try
            {
                // Cargar próximas cosechas
                CargarProximasCosechas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar próximas cosechas: " + ex.Message);
                lblProximasCosechasValor.Text = "Error";
            }

            try
            {
                // Cargar seguimientos
                CargarSeguimientos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar seguimientos: " + ex.Message);
            }

            try
            {
                // Cargar alertas
                CargarAlertas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar alertas: " + ex.Message);
                lblAlertasValor.Text = "Error";
            }

            try
            {
                // Cargar estadísticas
                CargarEstadisticas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar estadísticas: " + ex.Message);
            }

            try
            {
                // Actualizar gráficos
                ActualizarGraficoCultivos();
                ActualizarGraficoSeguimiento();
                ActualizarGraficoRendimientos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar gráficos: " + ex.Message);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Detener el timer al cerrar el formulario
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
                refreshTimer = null;
            }
        }
    }
}

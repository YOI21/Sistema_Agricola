using Sistema_Agricola.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Agricola.UI
{
    public partial class FrmControldePlagas : Form
    {
        public FrmControldePlagas()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            txtBuscarP.PlaceholderText = "Ej.: tomate manchas negras, maiz cogollero, pulgon, roya";
            txtTipodePlaga.ReadOnly = true;
            txtNombreC.ReadOnly = true;
            txtNombre_Cienti.ReadOnly = true;
            txtRazgos.ReadOnly = true;
            txtHuesped.ReadOnly = true;
            txtCura.ReadOnly = true;
            txtCura.WordWrap = true;
            txtCura.ScrollBars = ScrollBars.Vertical;

            btnGuardar.Text = "Buscar";
            btnActualizar.Text = "Recomendar";
            button1.Text = "Limpiar";

            btnActualizar.Click += btnActualizar_Click;
            DGV_Control_Plagas.CellClick += DGV_Control_Plagas_CellClick;
            txtBuscarP.KeyDown += txtBuscarP_KeyDown;
            this.Load += FrmControldePlagas_Load;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIDControldePlagas_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIDControldePlagas_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {

                e.Handled = true;
                return;
            }
        }

        private void txtxCostoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {

                e.Handled = true;
                return;
            }
        }
        private void LimpiarFormulario(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else if (c is ComboBox)
                    ((ComboBox)c).SelectedIndex = -1;
                else if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = ((NumericUpDown)c).Minimum;
                else if (c is CheckBox)
                    ((CheckBox)c).Checked = false;
                else if (c is RadioButton)
                    ((RadioButton)c).Checked = false;
                else if (c is DateTimePicker)
                    ((DateTimePicker)c).Value = DateTime.Now;
                else if (c.HasChildren)
                    LimpiarFormulario(c);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            BuscarP();
        }

        private void BuscarP()
        {
            try
            {
                string criterio = txtBuscarP.Text.Trim();
                if (string.IsNullOrWhiteSpace(criterio))
                {
                    PrepararEstadoInicial();
                    return;
                }

                DGV_Control_Plagas.DataSource = BLL_Siembra.Buscar_P(criterio);
                FormatearResultados();

                if (DGV_Control_Plagas.Rows.Count > 0)
                {
                    DGV_Control_Plagas.Rows[0].Selected = true;
                    SeleccionarFila(0);
                }
                else
                {
                    LimpiarDetalle();
                    txtCura.Text = "No se encontraron coincidencias. Prueba con el cultivo, el sintoma principal o el nombre de la plaga.";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar plagas o enfermedades: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            BuscarP();
        }

        private void ocultar()
        {
            FormatearResultados();
        }

        private void DGV_Control_Plagas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarFila(e.RowIndex);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string nuevoId = DateTime.Now.ToString("yyyyMMddHHmmss"); // Formato: AñoMesDíaHoraMinutoSegundo
            //txtIDControldePlagas.Text = nuevoId;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            BuscarP();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtBuscarP.Clear();
            PrepararEstadoInicial();
            txtBuscarP.Focus();
        }

        private void FrmControldePlagas_Load(object? sender, EventArgs e)
        {
            PrepararEstadoInicial();
        }

        private void txtBuscarP_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarP();
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e)
        {
            if (DGV_Control_Plagas.CurrentRow != null)
            {
                SeleccionarFila(DGV_Control_Plagas.CurrentRow.Index);
            }
            else
            {
                BuscarP();
            }
        }

        private void DGV_Control_Plagas_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            SeleccionarFila(e.RowIndex);
        }

        private void SeleccionarFila(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= DGV_Control_Plagas.Rows.Count)
            {
                return;
            }

            DataGridViewRow fila = DGV_Control_Plagas.Rows[rowIndex];
            if (fila.IsNewRow)
            {
                return;
            }

            txtTipodePlaga.Text = LeerCelda(fila, "Tipo");
            txtNombreC.Text = LeerCelda(fila, "Nombre_Comun");
            txtNombre_Cienti.Text = LeerCelda(fila, "Nombre_Cientifico");
            txtRazgos.Text = LeerCelda(fila, "Razgos");
            txtHuesped.Text = LeerCelda(fila, "Huesped");
            txtCura.Text = GenerarRecomendacion(fila);
        }

        private string LeerCelda(DataGridViewRow fila, string columna)
        {
            if (fila.DataGridView == null || !fila.DataGridView.Columns.Contains(columna))
            {
                return string.Empty;
            }

            object valor = fila.Cells[columna].Value;
            return valor == null || valor == DBNull.Value
                ? string.Empty
                : valor.ToString() ?? string.Empty;
        }

        private int LeerCoincidencia(DataGridViewRow fila)
        {
            int coincidencia;
            return int.TryParse(LeerCelda(fila, "Coincidencia"), out coincidencia) ? coincidencia : 0;
        }

        private string GenerarRecomendacion(DataGridViewRow fila)
        {
            string tipo = LeerCelda(fila, "Tipo");
            string nombre = LeerCelda(fila, "Nombre_Comun");
            string cientifico = LeerCelda(fila, "Nombre_Cientifico");
            string rasgos = LeerCelda(fila, "Razgos");
            string huesped = LeerCelda(fila, "Huesped");
            string manejo = LeerCelda(fila, "Cura");
            int coincidencia = LeerCoincidencia(fila);
            string riesgo = CalcularRiesgo(tipo, rasgos, txtBuscarP.Text, coincidencia);

            StringBuilder recomendacion = new StringBuilder();
            recomendacion.AppendLine("Diagnostico probable: " + nombre + (string.IsNullOrWhiteSpace(cientifico) ? "" : " (" + cientifico + ")"));
            recomendacion.AppendLine("Tipo: " + tipo + " | Coincidencia: " + coincidencia + "% | Nivel de riesgo: " + riesgo);
            recomendacion.AppendLine();
            recomendacion.AppendLine("Senales para confirmar:");
            recomendacion.AppendLine("- Rasgos: " + (string.IsNullOrWhiteSpace(rasgos) ? "No registrados." : rasgos));
            recomendacion.AppendLine("- Cultivos huespedes: " + (string.IsNullOrWhiteSpace(huesped) ? "No registrados." : huesped));
            recomendacion.AppendLine();
            recomendacion.AppendLine("Manejo recomendado:");
            recomendacion.AppendLine(string.IsNullOrWhiteSpace(manejo) ? "No hay manejo registrado para este caso." : manejo);
            recomendacion.AppendLine();
            recomendacion.AppendLine("Plan de accion:");
            recomendacion.AppendLine("- Revisar plantas en distintos puntos del terreno y comparar sintomas antes de aplicar productos.");
            recomendacion.AppendLine(ObtenerAccionPorRiesgo(riesgo));
            recomendacion.AppendLine("- Registrar fecha, cultivo afectado y producto utilizado para evaluar si el control funciono.");

            return recomendacion.ToString();
        }

        private string CalcularRiesgo(string tipo, string rasgos, string busqueda, int coincidencia)
        {
            string texto = (tipo + " " + rasgos + " " + busqueda).ToLowerInvariant();

            if (texto.Contains("marchitez") || texto.Contains("necrosis") || texto.Contains("tizon") ||
                texto.Contains("cogollero") || texto.Contains("bacter") || texto.Contains("nematodo") ||
                texto.Contains("pudricion") || texto.Contains("muerte"))
            {
                return "Alto";
            }

            if (coincidencia < 35)
            {
                return "Bajo - confirmar diagnostico";
            }

            if (texto.Contains("fung") || texto.Contains("insecto") || texto.Contains("acaro") ||
                texto.Contains("roya") || texto.Contains("oidio") || texto.Contains("mosca") ||
                texto.Contains("pulgon") || texto.Contains("trips"))
            {
                return "Medio";
            }

            return "Medio";
        }

        private string ObtenerAccionPorRiesgo(string riesgo)
        {
            if (riesgo.StartsWith("Alto"))
            {
                return "- Actuar de inmediato sobre los focos, retirar material muy afectado y evitar mover herramientas o restos contaminados sin desinfectar.";
            }

            if (riesgo.StartsWith("Bajo"))
            {
                return "- Mantener monitoreo por 3 a 5 dias y no aplicar productos fuertes hasta confirmar que el dano aumenta.";
            }

            return "- Aplicar manejo integrado: limpieza, monitoreo, control biologico o producto selectivo segun avance y cultivo.";
        }

        private void FormatearResultados()
        {
            if (DGV_Control_Plagas.DataSource == null || DGV_Control_Plagas.Columns.Count == 0)
            {
                return;
            }

            DGV_Control_Plagas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGV_Control_Plagas.MultiSelect = false;

            ConfigurarColumna("ID_CP", "ID", false);
            ConfigurarColumna("Tipo", "Tipo", true);
            ConfigurarColumna("Nombre_Comun", "Nombre", true);
            ConfigurarColumna("Nombre_Cientifico", "Nombre cientifico", true);
            ConfigurarColumna("Razgos", "Rasgos", true);
            ConfigurarColumna("Huesped", "Huesped/Cultivo", true);
            ConfigurarColumna("Cura", "Manejo", false);
            ConfigurarColumna("Coincidencia", "Coincidencia %", true);

            if (DGV_Control_Plagas.Columns.Contains("Coincidencia"))
            {
                DGV_Control_Plagas.Columns["Coincidencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DGV_Control_Plagas.Columns["Coincidencia"].Width = 95;
            }
        }

        private void ConfigurarColumna(string nombre, string encabezado, bool visible)
        {
            if (!DGV_Control_Plagas.Columns.Contains(nombre))
            {
                return;
            }

            DGV_Control_Plagas.Columns[nombre].HeaderText = encabezado;
            DGV_Control_Plagas.Columns[nombre].Visible = visible;
        }

        private void LimpiarDetalle()
        {
            txtTipodePlaga.Clear();
            txtNombreC.Clear();
            txtNombre_Cienti.Clear();
            txtRazgos.Clear();
            txtHuesped.Clear();
            txtCura.Clear();

        }

        private void PrepararEstadoInicial()
        {
            DGV_Control_Plagas.DataSource = null;
            DGV_Control_Plagas.Rows.Clear();
            DGV_Control_Plagas.Columns.Clear();
            DGV_Control_Plagas.ClearSelection();
            LimpiarDetalle();
        }
    }
}

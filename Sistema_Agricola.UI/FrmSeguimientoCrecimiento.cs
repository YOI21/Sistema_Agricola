
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmSeguimientoCrecimiento : Form
    {
        private int idCultivoSeleccionado = -1;
        private bool esNuevoRegistro = true;

        public FrmSeguimientoCrecimiento()
        {
            InitializeComponent();
            btnEliminar.Enabled = BLL_Seguridad.TienePermiso(PermisosSistema.SeguimientoEliminar);
            if (chartCrecimiento == null)
            {
                chartCrecimiento = new System.Windows.Forms.DataVisualization.Charting.Chart();
                chartCrecimiento.Dock = DockStyle.Fill;
                groupBox2.Controls.Add(chartCrecimiento);
            }

            // Agregar botón para finalizar cultivo
            if (!ControlsContainButtonWithName("btnFinalizarCultivo"))
            {
                Button btnFinalizarCultivo = new Button();
                btnFinalizarCultivo.Name = "btnFinalizarCultivo";
                btnFinalizarCultivo.Text = "Finalizar Cultivo";
                btnFinalizarCultivo.BackColor = Color.Gold;
                btnFinalizarCultivo.ForeColor = Color.Black;
                btnFinalizarCultivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnFinalizarCultivo.Size = new Size(150, 42);
                btnFinalizarCultivo.Location = new Point(btnLimpiar.Location.X + btnLimpiar.Width + 10, btnLimpiar.Location.Y);
                btnFinalizarCultivo.Click += new EventHandler(btnFinalizarCultivo_Click);
                this.panel2.Controls.Add(btnFinalizarCultivo);
            }
        }

        private bool ControlsContainButtonWithName(string buttonName)
        {
            foreach (Control control in this.panel2.Controls)
            {
                if (control is Button && control.Name == buttonName)
                    return true;
            }
            return false;
        }

        private void FrmSeguimientoCrecimiento_Load(object sender, EventArgs e)
        {
            CargarCultivos();
            ConfigurarGrafico();
            LimpiarFormulario();
        }

        private void CargarCultivos()
        {
            try
            {
                DataTable dtCultivos = BLL_Siembra.Mostrar_Cultivos();

                // Asegúrate que el DataTable tenga una columna ID_Cultivo
                if (!dtCultivos.Columns.Contains("ID_Cultivo"))
                {
                    MessageBox.Show("La tabla no contiene una columna ID_Cultivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Asegúrate que los valores no sean nulos
                foreach (DataRow row in dtCultivos.Rows)
                {
                    if (row["ID_Cultivo"] == DBNull.Value)
                    {
                        row["ID_Cultivo"] = -1; // O algún valor predeterminado
                    }
                }

                // Configura el ComboBox correctamente
                cbxCultivo.DataSource = null; // Limpia primero
                cbxCultivo.DisplayMember = "Cultivos";
                cbxCultivo.ValueMember = "ID_Cultivo";
                cbxCultivo.DataSource = dtCultivos;
                cbxCultivo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "cargar los cultivos para seguimiento");
            }
        }

        private void ConfigurarGrafico()
        {
            if (chartCrecimiento == null)
            {
                // Crear e inicializar el gráfico si no existe
                chartCrecimiento = new System.Windows.Forms.DataVisualization.Charting.Chart();
                chartCrecimiento.Dock = DockStyle.Fill;
                groupBox2.Controls.Add(chartCrecimiento);
            }

            chartCrecimiento.Series.Clear();

            // Asegurarnos de que exista al menos una ChartArea
            if (chartCrecimiento.ChartAreas.Count == 0)
            {
                chartCrecimiento.ChartAreas.Add(new ChartArea("Default"));
            }

            // Configurar serie para altura de planta
            Series serieAltura = new Series("Altura (cm)");
            serieAltura.ChartType = SeriesChartType.Line;
            serieAltura.BorderWidth = 3;
            chartCrecimiento.Series.Add(serieAltura);

            // Configurar serie para porcentaje de crecimiento
            Series seriePorcentaje = new Series("Crecimiento (%)");
            seriePorcentaje.ChartType = SeriesChartType.Line;
            seriePorcentaje.BorderWidth = 3;
            chartCrecimiento.Series.Add(seriePorcentaje);

            // Configurar los ejes
            chartCrecimiento.ChartAreas[0].AxisX.Title = "Fecha";
            chartCrecimiento.ChartAreas[0].AxisY.Title = "Valores";
            chartCrecimiento.ChartAreas[0].AxisY.Minimum = 0;

            // Configurar leyenda
            if (chartCrecimiento.Legends.Count == 0)
            {
                chartCrecimiento.Legends.Add(new Legend("Default"));
            }
            chartCrecimiento.Legends[0].Enabled = true;
            chartCrecimiento.Legends[0].Docking = Docking.Bottom;
        }

        private void ActualizarGrafico(int idCultivo)
        {
            if (chartCrecimiento == null)
            {
                // Mostrar mensaje al usuario
                MessageBox.Show("No se pudo inicializar el gráfico de crecimiento.", "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Limpiar datos actuales
                foreach (Series serie in chartCrecimiento.Series)
                {
                    serie.Points.Clear();
                }

                // Verificar que las series existan
                if (chartCrecimiento.Series.IndexOf("Altura (cm)") == -1 ||
                    chartCrecimiento.Series.IndexOf("Crecimiento (%)") == -1)
                {
                    // Reconfigura el gráfico si no existen las series
                    ConfigurarGrafico();
                }

                // Obtener datos de seguimiento
                DataTable dt = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);

                // Si no hay datos, salir
                if (dt == null || dt.Rows.Count == 0)
                    return;

                // Configurar el eje X para mostrar fechas correctamente
                chartCrecimiento.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
                chartCrecimiento.ChartAreas[0].AxisX.Interval = 7; // Mostrar cada 7 días
                chartCrecimiento.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;

                // Agregar puntos al gráfico
                foreach (DataRow row in dt.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["Fecha_Registro"]);
                    decimal altura = Convert.ToDecimal(row["Altura_Planta"]);
                    decimal porcentaje = Convert.ToDecimal(row["Porcentaje_Crecimiento"]);
                    int numHojas = Convert.ToInt32(row["Num_Hojas"]);

                    // Agregar los datos a las series respectivas
                    chartCrecimiento.Series["Altura (cm)"].Points.AddXY(fecha, altura);
                    chartCrecimiento.Series["Crecimiento (%)"].Points.AddXY(fecha, porcentaje);

                    // Opcional: Agregar serie para número de hojas
                    if (chartCrecimiento.Series.IndexOf("Número de Hojas") == -1)
                    {
                        Series serieHojas = new Series("Número de Hojas");
                        serieHojas.ChartType = SeriesChartType.Line;
                        serieHojas.BorderWidth = 3;
                        chartCrecimiento.Series.Add(serieHojas);
                    }

                    chartCrecimiento.Series["Número de Hojas"].Points.AddXY(fecha, numHojas);
                }

                // Añadir tooltips detallados
                foreach (Series serie in chartCrecimiento.Series)
                {
                    serie.ToolTip = "#VALX{dd/MM/yyyy}: #VALY";
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "actualizar el gráfico de crecimiento");
            }
        }

        private void CompararCultivos(List<int> idsCultivos)
        {
            try
            {
                // Limpiar datos actuales
                chartCrecimiento.Series.Clear();
                chartCrecimiento.Titles.Clear();

                // Agregar título al gráfico
                chartCrecimiento.Titles.Add("Comparativa de Crecimiento entre Cultivos");

                // Configurar el área del gráfico
                chartCrecimiento.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
                chartCrecimiento.ChartAreas[0].AxisX.Interval = 7;
                chartCrecimiento.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                chartCrecimiento.ChartAreas[0].AxisY.Title = "Altura (cm)";

                // Obtener datos para cada cultivo
                foreach (int idCultivo in idsCultivos)
                {
                    DataTable dt = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);

                    if (dt == null || dt.Rows.Count == 0)
                        continue;

                    // Obtener el nombre del cultivo
                    string nombreCultivo = dt.Rows[0]["Cultivo"].ToString() ?? string.Empty;

                    // Crear una serie para este cultivo
                    Series serieCultivo = new Series(nombreCultivo);
                    serieCultivo.ChartType = SeriesChartType.Line;
                    serieCultivo.BorderWidth = 3;

                    // Agregar puntos al gráfico
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime fecha = Convert.ToDateTime(row["Fecha_Registro"]);
                        decimal altura = Convert.ToDecimal(row["Altura_Planta"]);
                        serieCultivo.Points.AddXY(fecha, altura);
                    }

                    chartCrecimiento.Series.Add(serieCultivo);
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "comparar el crecimiento de los cultivos");
            }
        }

        private void PredecirCrecimiento(int idCultivo, int diasFuturos)
        {
            try
            {
                DataTable dt = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);

                if (dt == null || dt.Rows.Count < 2)
                {
                    MessageBox.Show("Se necesitan al menos dos registros para hacer una predicción",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Calcular la tasa de crecimiento promedio por día
                decimal alturaInicial = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["Altura_Planta"]);
                decimal alturaFinal = Convert.ToDecimal(dt.Rows[0]["Altura_Planta"]);
                DateTime fechaInicial = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["Fecha_Registro"]);
                DateTime fechaFinal = Convert.ToDateTime(dt.Rows[0]["Fecha_Registro"]);
                int diasTranscurridos = (int)(fechaFinal - fechaInicial).TotalDays;

                if (diasTranscurridos == 0)
                {
                    MessageBox.Show("No hay suficiente diferencia de tiempo entre los registros",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal tasaCrecimientoDiaria = (alturaFinal - alturaInicial) / diasTranscurridos;

                // Crear serie para predicción
                Series seriePrediccion = new Series("Predicción");
                seriePrediccion.ChartType = SeriesChartType.Line;
                seriePrediccion.BorderWidth = 2;
                seriePrediccion.BorderDashStyle = ChartDashStyle.Dash;
                seriePrediccion.Color = Color.Red;

                // Último punto conocido
                seriePrediccion.Points.AddXY(fechaFinal, alturaFinal);

                // Agregar puntos de predicción
                for (int i = 1; i <= diasFuturos; i++)
                {
                    DateTime fechaPrediccion = fechaFinal.AddDays(i);
                    decimal alturaPrediccion = alturaFinal + (tasaCrecimientoDiaria * i);
                    seriePrediccion.Points.AddXY(fechaPrediccion, alturaPrediccion);
                }

                // Añadir serie al gráfico
                if (chartCrecimiento.Series.IndexOf("Predicción") >= 0)
                    chartCrecimiento.Series.RemoveAt(chartCrecimiento.Series.IndexOf("Predicción"));

                chartCrecimiento.Series.Add(seriePrediccion);
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "calcular la proyección de crecimiento");
            }
        }

        private void CargarDatosSeguimiento(int idCultivo)
        {
            try
            {
                // Cargar datos en el DataGridView
                DataTable dt = BLL_SeguimientoCrecimiento.Mostrar_SeguimientoCrecimiento(idCultivo);
                dgvSeguimiento.DataSource = dt;

                // Formatear columnas
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Ocultar columnas de ID si es necesario
                    if (dgvSeguimiento.Columns.Contains("ID_Seguimiento"))
                        dgvSeguimiento.Columns["ID_Seguimiento"].Visible = false;

                    if (dgvSeguimiento.Columns.Contains("ID_Cultivo"))
                        dgvSeguimiento.Columns["ID_Cultivo"].Visible = false;

                    // Formatear columnas de fecha
                    if (dgvSeguimiento.Columns.Contains("Fecha_Registro"))
                        dgvSeguimiento.Columns["Fecha_Registro"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    // Formatear columnas numéricas
                    if (dgvSeguimiento.Columns.Contains("Altura_Planta"))
                        dgvSeguimiento.Columns["Altura_Planta"].DefaultCellStyle.Format = "N2";

                    if (dgvSeguimiento.Columns.Contains("Porcentaje_Crecimiento"))
                        dgvSeguimiento.Columns["Porcentaje_Crecimiento"].DefaultCellStyle.Format = "N1";
                }

                // Actualizar gráfico
                ActualizarGrafico(idCultivo);

                // Verificar si hay registros con porcentaje de crecimiento cercano al 100%
                bool cultivo100Porciento = false;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal porcentaje = Convert.ToDecimal(row["Porcentaje_Crecimiento"]);
                        if (porcentaje >= 95)
                        {
                            cultivo100Porciento = true;
                            break;
                        }
                    }
                }

                // Si hay un cultivo con crecimiento cercano al 100%, sugerir finalizarlo
                if (cultivo100Porciento)
                {
                    if (ControlsContainButtonWithName("btnFinalizarCultivo"))
                    {
                        Button? btnFinalizarCultivo = null;
                        foreach (Control c in panel2.Controls)
                        {
                            if (c is Button && c.Name == "btnFinalizarCultivo")
                            {
                                btnFinalizarCultivo = (Button)c;
                                break;
                            }
                        }

                        if (btnFinalizarCultivo != null)
                        {
                            btnFinalizarCultivo.BackColor = Color.LimeGreen;

                            DialogResult dr = MessageBox.Show(
                                "Este cultivo parece estar listo para ser cosechado (su porcentaje de crecimiento es mayor al 95%). " +
                                "¿Desea finalizarlo y pasarlo a la fase de cosecha?",
                                "Cultivo listo para cosecha",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (dr == DialogResult.Yes)
                            {
                                btnFinalizarCultivo_Click(btnFinalizarCultivo, new EventArgs());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "cargar los datos de seguimiento");
            }
        }

        private void LimpiarFormulario()
        {
            txtIDSeguimiento.Text = "";
            dtpFechaRegistro.Value = DateTime.Now;
            cbxEtapaCrecimiento.SelectedIndex = -1;
            numAlturaPlanta.Value = 0;
            numNumHojas.Value = 0;
            cbxEstadoSalud.SelectedIndex = -1;
            txtObservaciones.Text = "";
            numPorcentajeCrecimiento.Value = 0;
            txtProblemasDetectados.Text = "";
            txtAccionesTomadas.Text = "";
            esNuevoRegistro = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idCultivoSeleccionado == -1)
            {
                MessageBox.Show("Debe seleccionar un cultivo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxEtapaCrecimiento.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una etapa de crecimiento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxEstadoSalud.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado de salud", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string mensaje = BLL_SeguimientoCrecimiento.Insertar_SeguimientoCrecimiento(
                    idCultivoSeleccionado,
                    dtpFechaRegistro.Value,
                    cbxEtapaCrecimiento.Text,
                    numAlturaPlanta.Value,
                    (int)numNumHojas.Value,
                    cbxEstadoSalud.Text,
                    txtObservaciones.Text,
                    numPorcentajeCrecimiento.Value,
                    txtProblemasDetectados.Text,
                    txtAccionesTomadas.Text
                );

                MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosSeguimiento(idCultivoSeleccionado);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "guardar el seguimiento");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (esNuevoRegistro || string.IsNullOrEmpty(txtIDSeguimiento.Text))
            {
                MessageBox.Show("Debe seleccionar un registro para actualizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string mensaje = BLL_SeguimientoCrecimiento.Actualizar_SeguimientoCrecimiento(
                    Convert.ToInt32(txtIDSeguimiento.Text),
                    dtpFechaRegistro.Value,
                    cbxEtapaCrecimiento.Text,
                    numAlturaPlanta.Value,
                    (int)numNumHojas.Value,
                    cbxEstadoSalud.Text,
                    txtObservaciones.Text,
                    numPorcentajeCrecimiento.Value,
                    txtProblemasDetectados.Text,
                    txtAccionesTomadas.Text
                );

                MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosSeguimiento(idCultivoSeleccionado);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "actualizar el seguimiento");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (esNuevoRegistro || string.IsNullOrEmpty(txtIDSeguimiento.Text))
            {
                MessageBox.Show("Debe seleccionar un registro para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este registro de seguimiento?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string mensaje = BLL_SeguimientoCrecimiento.Eliminar_SeguimientoCrecimiento(
                        Convert.ToInt32(txtIDSeguimiento.Text)
                    );

                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosSeguimiento(idCultivoSeleccionado);
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    ManejadorErroresUI.Mostrar(this, ex, "eliminar el seguimiento");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void cbxCultivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxCultivo.SelectedIndex >= 0 && cbxCultivo.SelectedValue != null)
                {
                    // Solución para manejar DataRowView
                    if (cbxCultivo.SelectedValue is DataRowView drv)
                    {
                        // Asume que "ID_Cultivo" es el nombre de la columna en DataRowView
                        idCultivoSeleccionado = Convert.ToInt32(drv["ID_Cultivo"]);
                    }
                    else
                    {
                        // Intenta convertir directamente si no es DataRowView
                        idCultivoSeleccionado = Convert.ToInt32(cbxCultivo.SelectedValue);
                    }

                    CargarDatosSeguimiento(idCultivoSeleccionado);
                }
                else
                {
                    idCultivoSeleccionado = -1;
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "seleccionar el cultivo para seguimiento");
                idCultivoSeleccionado = -1;
            }
        }

        private void dgvSeguimiento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    esNuevoRegistro = false;
                    DataGridViewRow row = dgvSeguimiento.Rows[e.RowIndex];

                    txtIDSeguimiento.Text = row.Cells["ID_Seguimiento"].Value.ToString();
                    dtpFechaRegistro.Value = Convert.ToDateTime(row.Cells["Fecha_Registro"].Value);
                    cbxEtapaCrecimiento.Text = row.Cells["Etapa_Crecimiento"].Value.ToString();
                    numAlturaPlanta.Value = Convert.ToDecimal(row.Cells["Altura_Planta"].Value);
                    numNumHojas.Value = Convert.ToDecimal(row.Cells["Num_Hojas"].Value);
                    cbxEstadoSalud.Text = row.Cells["Estado_Salud"].Value.ToString();
                    txtObservaciones.Text = row.Cells["Observaciones"].Value.ToString();
                    numPorcentajeCrecimiento.Value = Convert.ToDecimal(row.Cells["Porcentaje_Crecimiento"].Value);
                    txtProblemasDetectados.Text = row.Cells["Problemas_Detectados"].Value.ToString();
                    txtAccionesTomadas.Text = row.Cells["Acciones_Tomadas"].Value.ToString();
                }
                catch (Exception ex)
                {
                    ManejadorErroresUI.Mostrar(this, ex, "cargar el registro de seguimiento");
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nuevoId = "SEG" + DateTime.Now.ToString("yyyyMMddHHmmss");
            txtIDSeguimiento.Text = nuevoId;
        }

        // Método para finalizar cultivo
        
        private void btnFinalizarCultivo_Click(object? sender, EventArgs e)
        {
            // Verificar que haya un cultivo seleccionado
            if (idCultivoSeleccionado == -1)
            {
                MessageBox.Show("Debe seleccionar un cultivo para finalizar",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Pedir confirmación al usuario
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro que desea finalizar este cultivo y pasarlo a cosecha?\n\n" +
                    "Esta acción marcará el cultivo como 'Cosechado' y ya no estará disponible para seguimiento.",
                    "Confirmar finalización de cultivo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    // Llamar al método de BLL para finalizar el cultivo
                    string resultado = BLL_Siembra.Finalizar_Cultivo(idCultivoSeleccionado);

                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar la lista de cultivos disponibles
                    CargarCultivos();

                    // Limpiar el formulario
                    LimpiarFormulario();

                    // Preguntar si desea ver la pantalla de cosecha
                    DialogResult verCosecha = MessageBox.Show(
                        "¿Desea ir a la pantalla de cosecha?",
                        "Ver cosecha",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (verCosecha == DialogResult.Yes)
                    {
                        // Abrir el formulario de cosecha y pasar el ID del cultivo
                        FrmCosechas frmCosecha = new FrmCosechas();
                        // Si queremos preseleccionar el cultivo para cosecha, necesitamos
                        // una propiedad o método en FrmCosechas que lo permita
                        frmCosecha.PreseleccionarCultivo(idCultivoSeleccionado);
                        frmCosecha.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "finalizar el cultivo");
            }
        }

    }
}

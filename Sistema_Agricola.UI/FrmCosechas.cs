using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmCosechas : Form
    {
        private int idCultivoPreseleccionado = -1;
        private int idCultivoSeleccionado = -1;
        private int idCosechaSeleccionada = -1;
        private decimal cantidadSembrada = 0;
        private decimal costoSiembra = 0;

        public FrmCosechas(int idCultivo = -1)
        {
            InitializeComponent();

            // Configurar el gráfico solo si el control existe
            if (chartRendimiento != null)
            {
                ConfigurarGrafico();
            }

            ConfigurarAccesibilidad();

            // Guardar el ID del cultivo a preseleccionar
            this.idCultivoPreseleccionado = idCultivo;
        }

        private void ConfigurarAccesibilidad()
        {
            txtIDCosecha.AccessibleName = "ID de cosecha";
            cbxCultivo.AccessibleName = "Cultivo que se cosechará";
            dtpFechaCosecha.AccessibleName = "Fecha de cosecha";
            cbxEstadoCosecha.AccessibleName = "Estado de la cosecha";
            numCantidadCosechada.AccessibleName = "Cantidad cosechada";
            numCalidad.AccessibleName = "Calidad de la cosecha";
            numRendimiento.AccessibleName = "Rendimiento de la cosecha";
            txtObservaciones.AccessibleName = "Observaciones de la cosecha";
            numCostoCosecha.AccessibleName = "Costo de la cosecha";
            numPrecioVenta.AccessibleName = "Precio de venta";
            dgvCosechas.AccessibleName = "Historial de cosechas";
            btnGuardar.AccessibleName = "Guardar cosecha";
            btnActualizar.AccessibleName = "Actualizar cosecha";
            btnEliminar.AccessibleName = "Eliminar cosecha";
            btnLimpiar.AccessibleName = "Limpiar formulario";
            btnVolver.AccessibleName = "Cerrar gestión de cosechas";
        }

        private void FrmCosechas_Load(object sender, EventArgs e)
        {
            CargarCultivosDisponibles();
            CargarCosechas();
            LimpiarFormulario();

            // Configurar el combo de estado solo si existe
            if (cbxEstadoCosecha != null)
            {
                cbxEstadoCosecha.SelectedIndex = 0; // "Completada" por defecto
            }

            // Si hay un cultivo preseleccionado, seleccionarlo en el combo
            if (idCultivoPreseleccionado != -1 && cbxCultivo != null)
            {
                SeleccionarCultivoPorId(idCultivoPreseleccionado);
            }
        }
        private void SeleccionarCultivoPorId(int idCultivo)
        {
            try
            {
                // Verificar que el control no sea null
                if (cbxCultivo == null)
                {
                    return;
                }

                // Recorrer los items del combo para encontrar el cultivo con el ID correspondiente
                for (int i = 0; i < cbxCultivo.Items.Count; i++)
                {
                    if (cbxCultivo.Items[i] is DataRowView item &&
                        Convert.ToInt32(item["ID_Cultivo"]) == idCultivo)
                    {
                        cbxCultivo.SelectedIndex = i;
                        return;
                    }
                }

                // Si llegamos aquí, es porque no se encontró el cultivo en la lista
                // Posiblemente ya fue cosechado o no está disponible
                MessageBox.Show("El cultivo seleccionado no está disponible para cosecha.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar el cultivo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrafico()
        {
            // Configurar el gráfico para mostrar estadísticas de cosecha
            chartRendimiento.Series.Clear();
            chartRendimiento.Titles.Clear();
            chartRendimiento.Titles.Add("Rendimiento y Calidad de Cosechas");

            // Asegurarnos de que exista al menos una ChartArea
            if (chartRendimiento.ChartAreas.Count == 0)
            {
                chartRendimiento.ChartAreas.Add(new ChartArea("Default"));
            }

            // Serie para rendimiento
            Series serieRendimiento = new Series("Rendimiento (%)");
            serieRendimiento.ChartType = SeriesChartType.Column;
            serieRendimiento.Color = Color.Green;
            chartRendimiento.Series.Add(serieRendimiento);

            // Serie para calidad
            Series serieCalidad = new Series("Calidad (1-10)");
            serieCalidad.ChartType = SeriesChartType.Column;
            serieCalidad.Color = Color.Blue;
            chartRendimiento.Series.Add(serieCalidad);

            // Serie para beneficio
            Series serieBeneficio = new Series("Beneficio ($)");
            serieBeneficio.ChartType = SeriesChartType.Column;
            serieBeneficio.Color = Color.Gold;
            chartRendimiento.Series.Add(serieBeneficio);

            // Configurar ejes
            chartRendimiento.ChartAreas[0].AxisX.Title = "Cultivos";
            chartRendimiento.ChartAreas[0].AxisY.Title = "Valores";
            chartRendimiento.ChartAreas[0].AxisY.Minimum = 0;

            // Habilitar leyendas
            chartRendimiento.Legends.Clear();
            chartRendimiento.Legends.Add(new Legend("Default"));
            chartRendimiento.Legends[0].Docking = Docking.Bottom;
            chartRendimiento.Legends[0].Alignment = StringAlignment.Center;
        }

        private void ActualizarGrafico()
        {
            try
            {
                // Limpiar las series
                foreach (Series serie in chartRendimiento.Series)
                {
                    serie.Points.Clear();
                }

                // Obtener datos de cosechas
                DataTable dt = BLL_Cosecha.Listar_Cosechas();

                // Si no hay datos, salir
                if (dt == null || dt.Rows.Count == 0)
                    return;

                // Limitar a las últimas 5 cosechas para mejor visualización
                int count = Math.Min(dt.Rows.Count, 5);
                for (int i = 0; i < count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string nombreCultivo = row["Cultivo"].ToString() ?? string.Empty;

                    // Ajustar nombre si es muy largo
                    if (nombreCultivo.Length > 10)
                        nombreCultivo = nombreCultivo.Substring(0, 8) + "...";

                    decimal rendimiento = 0;
                    if (row["Rendimiento %"] != DBNull.Value)
                        rendimiento = Convert.ToDecimal(row["Rendimiento %"]);

                    decimal calidad = 0;
                    if (row["Calidad"] != DBNull.Value)
                        calidad = Convert.ToDecimal(row["Calidad"]);

                    decimal beneficio = 0;
                    if (row["Beneficio"] != DBNull.Value)
                        beneficio = Convert.ToDecimal(row["Beneficio"]);

                    // Agregar punto a cada serie
                    int idx = chartRendimiento.Series["Rendimiento (%)"].Points.AddXY(nombreCultivo, rendimiento);
                    chartRendimiento.Series["Calidad (1-10)"].Points.AddXY(nombreCultivo, calidad);

                    // Mostrar beneficio solo si es positivo (para mejor visualización)
                    decimal beneficioPositivo = beneficio > 0 ? beneficio : 0;
                    // Escalar el beneficio para que se vea en la misma escala que los otros valores
                    decimal beneficioEscalado = Math.Min(beneficioPositivo / 100, 200);
                    chartRendimiento.Series["Beneficio ($)"].Points.AddXY(nombreCultivo, beneficioEscalado);

                    // Añadir tooltips con los valores reales
                    chartRendimiento.Series["Rendimiento (%)"].Points[idx].ToolTip =
                        string.Format("{0}\nRendimiento: {1:N2}%", nombreCultivo, rendimiento);

                    chartRendimiento.Series["Calidad (1-10)"].Points[idx].ToolTip =
                        string.Format("{0}\nCalidad: {1:N1}/10", nombreCultivo, calidad);

                    chartRendimiento.Series["Beneficio ($)"].Points[idx].ToolTip =
                        string.Format("{0}\nBeneficio: ${1:N2}", nombreCultivo, beneficio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el gráfico: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarCultivosDisponibles()
        {
            try
            {
                DataTable dt = BLL_Cosecha.Listar_Cultivos_Disponibles();

                if (cbxCultivo == null)
                {
                    return;
                }

                // Verificar si hay datos
                if (dt == null || dt.Rows.Count == 0)
                {
                    cbxCultivo.DataSource = null;
                    MessageBox.Show("No hay cultivos disponibles para cosechar",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Imprimir información sobre las columnas
                Console.WriteLine("Columnas recibidas: " + string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                // Configurar el combobox correctamente
                cbxCultivo.DataSource = dt;
                cbxCultivo.DisplayMember = "Nombre_Cultivo";
                cbxCultivo.ValueMember = "ID_Cultivo";


            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "cargar los cultivos disponibles");
            }

        }
        private void CargarCosechas()
        {
            try
            {
                // Obtener todas las cosechas
                DataTable dt = BLL_Cosecha.Listar_Cosechas();
                dgvCosechas.DataSource = dt;

                // Formatear columnas numéricas
                FormatearColumnas();

                // Actualizar gráfico con los datos cargados
                ActualizarGrafico();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cosechas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnas()
        {
            if (dgvCosechas.DataSource == null || dgvCosechas.Columns.Count == 0)
                return;

            // Ocultar columna ID
            if (dgvCosechas.Columns.Contains("ID"))
                dgvCosechas.Columns["ID"].Visible = false;

            // Formatear columnas numéricas
            string[] columnasDecimal = { "Cantidad Cosechada", "Cantidad Sembrada", "Rendimiento %",
                                        "Calidad", "Costo Cosecha", "Precio Venta", "Beneficio" };

            foreach (string columna in columnasDecimal)
            {
                if (dgvCosechas.Columns.Contains(columna))
                {
                    dgvCosechas.Columns[columna].DefaultCellStyle.Format = "N2";
                    dgvCosechas.Columns[columna].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Formatear columna de fecha
            if (dgvCosechas.Columns.Contains("Fecha Cosecha"))
                dgvCosechas.Columns["Fecha Cosecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Aplicar colores a la columna de beneficio para resaltar valores positivos/negativos
            if (dgvCosechas.Columns.Contains("Beneficio"))
            {
                dgvCosechas.CellFormatting += (sender, e) =>
                {
                    if (e.ColumnIndex == dgvCosechas.Columns["Beneficio"].Index && e.RowIndex >= 0)
                    {
                        if (e.Value != null && e.Value != DBNull.Value)
                        {
                            decimal valor = Convert.ToDecimal(e.Value);
                            if (valor < 0 && e.CellStyle != null)
                                e.CellStyle.ForeColor = Color.Red;
                            else if (valor > 0 && e.CellStyle != null)
                                e.CellStyle.ForeColor = Color.Green;
                        }
                    }
                };
            }
        }

        private void LimpiarFormulario()
        {
            if (txtIDCosecha != null)
            {
                txtIDCosecha.Text = "";
            }

            if (cbxCultivo != null)
            {
                cbxCultivo.SelectedIndex = -1;
                cbxCultivo.Enabled = true;
            }

            if (dtpFechaCosecha != null)
            {
                dtpFechaCosecha.Value = DateTime.Now;
            }

            if (cbxEstadoCosecha != null)
            {
                cbxEstadoCosecha.SelectedIndex = -1;
            }

            if (numCantidadCosechada != null)
            {
                numCantidadCosechada.Value = 0;
            }

            if (numCalidad != null)
            {
                numCalidad.Value = 8; // Valor predeterminado de 8/10
            }

            if (numRendimiento != null)
            {
                numRendimiento.Value = 100; // 100% por defecto
            }

            if (txtObservaciones != null)
            {
                txtObservaciones.Text = "";
            }

            if (numCostoCosecha != null)
            {
                numCostoCosecha.Value = 0;
            }

            if (numPrecioVenta != null)
            {
                numPrecioVenta.Value = 0;
            }

            if (lblCantidadSembrada != null)
            {
                lblCantidadSembrada.Text = "Cantidad sembrada: 0";
            }

            if (lblCostoSiembra != null)
            {
                lblCostoSiembra.Text = "Costo siembra $: 0";
            }

            if (lblBeneficio != null)
            {
                lblBeneficio.Text = "0";
            }

            cantidadSembrada = 0;
            costoSiembra = 0;

            idCultivoSeleccionado = -1;
            idCosechaSeleccionada = -1;

            if (btnActualizar != null)
            {
                btnActualizar.Enabled = false;
            }

            if (btnEliminar != null)
            {
                btnEliminar.Enabled = false;
            }

            if (btnGuardar != null)
            {
                btnGuardar.Enabled = true;
            }
        }

        //private void cbxCultivo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Al seleccionar un cultivo, cargamos sus datos
        //    if (cbxCultivo.SelectedIndex != -1 && cbxCultivo.SelectedValue != null)
        //    {
        //        try
        //        {
        //            // Obtener la fila seleccionada de manera segura
        //            DataRowView drv = cbxCultivo.SelectedItem as DataRowView;
        //            if (drv == null)
        //            {
        //                MessageBox.Show("No se pudo obtener la información del cultivo seleccionado.",
        //                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }

        //            // Obtener el ID del cultivo de manera segura
        //            idCultivoSeleccionado = Convert.ToInt32(drv["ID_Cultivo"]);

        //            // Obtener cantidad sembrada de manera segura
        //            if (drv.Row.Table.Columns.Contains("Cantidad") && drv["Cantidad"] != DBNull.Value)
        //            {
        //                cantidadSembrada = Convert.ToDecimal(drv["Cantidad"]);
        //            }
        //            else
        //            {
        //                cantidadSembrada = 0;
        //                MessageBox.Show("No se encontró la cantidad sembrada para este cultivo.",
        //                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //            // Obtener costo de siembra de manera segura
        //            if (drv.Row.Table.Columns.Contains("Costo_Siembra") && drv["Costo_Siembra"] != DBNull.Value)
        //            {
        //                costoSiembra = Convert.ToDecimal(drv["Costo_Siembra"]);
        //            }
        //            else
        //            {
        //                costoSiembra = 0;
        //                MessageBox.Show("No se encontró el costo de siembra para este cultivo.",
        //                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //            // Actualizar etiquetas
        //            lblCantidadSembrada.Text = $"Cantidad sembrada: {cantidadSembrada}";
        //            lblCostoSiembra.Text = $"Costo siembra $: {costoSiembra:N2}";

        //            // Establecer la fecha estimada de cosecha desde la siembra
        //            if (drv.Row.Table.Columns.Contains("Fecha_Cosecha") && drv["Fecha_Cosecha"] != DBNull.Value)
        //            {
        //                dtpFechaCosecha.Value = Convert.ToDateTime(drv["Fecha_Cosecha"]);
        //            }
        //            else
        //            {
        //                dtpFechaCosecha.Value = DateTime.Now;
        //                MessageBox.Show("No se encontró la fecha de cosecha estimada para este cultivo.",
        //                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //            // Calcular rendimiento con la cantidad cosechada actual
        //            CalcularRendimiento();

        //            // Calcular beneficio con los valores actuales
        //            CalcularBeneficio();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error al cargar los datos del cultivo: " + ex.Message +
        //                            "\nDetalles técnicos: " + ex.StackTrace,
        //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
        private void cbxCultivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar que el control no sea null
            if (cbxCultivo == null)
            {
                return;
            }

            // Al seleccionar un cultivo, cargamos sus datos
            if (cbxCultivo.SelectedIndex != -1 && cbxCultivo.SelectedValue != null)
            {
                try
                {
                    // Obtener la fila seleccionada
                    DataRowView? drv = cbxCultivo.SelectedItem as DataRowView;
                    if (drv == null)
                    {
                        return;
                    }

                    idCultivoSeleccionado = Convert.ToInt32(drv["ID_Cultivo"]);

                    // Obtener cantidad sembrada y costo de siembra
                    cantidadSembrada = Convert.ToDecimal(drv["Cantidad"]);

                    // Verificar si hay un valor para costo_siembra
                    if (drv.Row.Table.Columns.Contains("Costo_Siembra") &&
                        drv["Costo_Siembra"] != DBNull.Value)
                    {
                        costoSiembra = Convert.ToDecimal(drv["Costo_Siembra"]);
                    }
                    else
                    {
                        costoSiembra = 0;
                    }

                    // Actualizar etiquetas
                    if (lblCantidadSembrada != null)
                    {
                        lblCantidadSembrada.Text = $"Cantidad sembrada: {cantidadSembrada}";
                    }

                    if (lblCostoSiembra != null)
                    {
                        lblCostoSiembra.Text = $"Costo siembra $: {costoSiembra:N2}";
                    }

                    // Establecer la fecha estimada de cosecha desde la siembra
                    if (dtpFechaCosecha != null &&
                        drv.Row.Table.Columns.Contains("Fecha_Cosecha") &&
                        drv["Fecha_Cosecha"] != DBNull.Value)
                    {
                        dtpFechaCosecha.Value = Convert.ToDateTime(drv["Fecha_Cosecha"]);
                    }

                    // Calcular rendimiento con la cantidad cosechada actual
                    CalcularRendimiento();

                    // Calcular beneficio con los valores actuales
                    CalcularBeneficio();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos del cultivo: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcularRendimiento()
        {
            if (numCantidadCosechada == null || numRendimiento == null)
            {
                return;
            }

            if (cantidadSembrada > 0 && numCantidadCosechada.Value > 0)
            {
                decimal rendimiento = BLL_Cosecha.Calcular_Rendimiento(
                    numCantidadCosechada.Value, cantidadSembrada);

                if (rendimiento > numRendimiento.Maximum)
                {
                    numRendimiento.Maximum = rendimiento;
                }

                numRendimiento.Value = rendimiento;
            }
            else
            {
                numRendimiento.Value = 0;
            }
        }

        private void CalcularBeneficio()
        {
            if (numPrecioVenta == null || numCostoCosecha == null || lblBeneficio == null)
            {
                return;
            }

            decimal precioVenta = numPrecioVenta.Value;
            decimal costoCosecha = numCostoCosecha.Value;

            decimal beneficio = BLL_Cosecha.Calcular_Beneficio(precioVenta, costoCosecha, costoSiembra);

            // Actualizar la etiqueta de beneficio
            lblBeneficio.Text = beneficio.ToString("N2");

            // Cambiar el color según sea positivo o negativo
            if (beneficio < 0)
                lblBeneficio.ForeColor = Color.Red;
            else if (beneficio > 0)
                lblBeneficio.ForeColor = Color.Green;
            else
                lblBeneficio.ForeColor = Color.Black;
        }

        private decimal ObtenerBeneficioActual()
        {
            if (lblBeneficio == null)
            {
                return 0;
            }

            decimal beneficio;
            if (decimal.TryParse(lblBeneficio.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out beneficio))
            {
                return beneficio;
            }

            if (decimal.TryParse(lblBeneficio.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out beneficio))
            {
                return beneficio;
            }

            return 0;
        }

        private void numCantidadCosechada_ValueChanged(object sender, EventArgs e)
        {
            CalcularRendimiento();
            CalcularBeneficio();
        }

        private void numCostoCosecha_ValueChanged(object sender, EventArgs e)
        {
            CalcularBeneficio();
        }

        private void numPrecioVenta_ValueChanged(object sender, EventArgs e)
        {
            CalcularBeneficio();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() == false)
                return;

            try
            {
                // Cambiar el cursor 
                this.UseWaitCursor = true;
                btnGuardar.Enabled = false;

                // Registrar todos los parámetros que se envían
                Console.WriteLine($"Enviando ID_Cultivo: {idCultivoSeleccionado}, " +
                    $"Estado: {cbxEstadoCosecha.Text}, " +
                    $"Cantidad: {numCantidadCosechada.Value}, " +
                    $"Rendimiento: {numRendimiento.Value}");

                // Crear el objeto con los datos
                Entitie_Cosecha cosecha = new Entitie_Cosecha();
                cosecha.ID_Cultivo = idCultivoSeleccionado;
                cosecha.Estado_Cosecha = cbxEstadoCosecha.Text;
                cosecha.Cantidad_Cosechada = numCantidadCosechada.Value;
                cosecha.Rendimiento = numRendimiento.Value;
                cosecha.Calidad = numCalidad.Value;
                cosecha.Observaciones = txtObservaciones.Text;
                cosecha.Fecha_Cosecha = dtpFechaCosecha.Value;
                cosecha.Costo_Cosecha = numCostoCosecha.Value;
                cosecha.Precio_Venta = numPrecioVenta.Value;
                cosecha.Beneficio = ObtenerBeneficioActual();

                // Llamar al método para registrar la cosecha
                string resultado = BLL_Cosecha.Registrar_Cosecha(cosecha);

                // Restaurar cursor y botón
                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Verificar el resultado para decidir si recargar datos
                if (resultado.Contains("registrado correctamente") || resultado.Contains("actualizado correctamente"))
                {
                    CargarCultivosDisponibles();
                    CargarCosechas();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                // Restaurar cursor y botón en caso de error
                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                // Mostrar el error con más detalle
                MessageBox.Show("Error al guardar la cosecha: " + ex.Message + "\n" + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() == false)
                return;

            if (idCosechaSeleccionada == -1)
            {
                MessageBox.Show("Debe seleccionar una cosecha para actualizar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string resultado = BLL_Cosecha.Actualizar_Cosecha(
                    idCosechaSeleccionada,
                    idCultivoSeleccionado,
                    cbxEstadoCosecha.Text,
                    numCantidadCosechada.Value,
                    numRendimiento.Value,
                    numCalidad.Value,
                    txtObservaciones.Text,
                    dtpFechaCosecha.Value,
                    numCostoCosecha.Value,
                    numPrecioVenta.Value,
                    ObtenerBeneficioActual()
                );

                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar datos
                CargarCosechas();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la cosecha: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCosechaSeleccionada == -1)
            {
                MessageBox.Show("Debe seleccionar una cosecha para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro que desea eliminar esta cosecha?\n\n" +
                "Esta acción reactivará el cultivo para futuras cosechas.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    string resultado = BLL_Cosecha.Eliminar_Cosecha(idCosechaSeleccionada, idCultivoSeleccionado);

                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar datos
                    CargarCultivosDisponibles();
                    CargarCosechas();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la cosecha: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvCosechas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtener el ID de la cosecha seleccionada
                    idCosechaSeleccionada = Convert.ToInt32(dgvCosechas.Rows[e.RowIndex].Cells["ID"].Value);

                    // Cargar los detalles de la cosecha
                    Entitie_Cosecha cosecha = BLL_Cosecha.Obtener_Cosecha(idCosechaSeleccionada);

                    if (cosecha != null)
                    {

                        // Cargar datos en los controles
                        if (txtIDCosecha != null)
                        {
                            txtIDCosecha.Text = cosecha.ID_Cosecha.ToString();
                        }

                        idCultivoSeleccionado = cosecha.ID_Cultivo;

                        SeleccionarCultivoParaEdicion(cosecha);

                        // Deshabilitar la selección de cultivo, ya que estamos editando
                        if (cbxCultivo != null)
                        {
                            cbxCultivo.Enabled = false;
                        }

                        // Cargar la información del cultivo
                        cantidadSembrada = cosecha.Cantidad_Sembrada;

                        if (lblCantidadSembrada != null)
                        {
                            lblCantidadSembrada.Text = $"Cantidad sembrada: {cantidadSembrada}";
                        }

                        costoSiembra = cosecha.Costo_Siembra;

                        if (lblCostoSiembra != null)
                        {
                            lblCostoSiembra.Text = $"Costo siembra $: {costoSiembra:N2}";
                        }

                        // Cargar datos específicos de la cosecha
                        if (dtpFechaCosecha != null)
                        {
                            if (cosecha.Fecha_Cosecha != DateTime.MinValue)
                            {
                                dtpFechaCosecha.Value = cosecha.Fecha_Cosecha;
                            }
                            else
                            {
                                // Si la fecha no es válida, usar la fecha actual
                                dtpFechaCosecha.Value = DateTime.Now;
                            }
                        }

                        // Seleccionar el estado de cosecha
                        if (cbxEstadoCosecha != null)
                        {
                            for (int i = 0; i < cbxEstadoCosecha.Items.Count; i++)
                            {
                                if (Convert.ToString(cbxEstadoCosecha.Items[i]) ==
                                    cosecha.Estado_Cosecha)
                                {
                                    cbxEstadoCosecha.SelectedIndex = i;
                                    break;
                                }
                            }
                        }

                        if (numCantidadCosechada != null)
                        {
                            numCantidadCosechada.Value = cosecha.Cantidad_Cosechada;
                        }

                        if (numRendimiento != null)
                        {
                            numRendimiento.Value = cosecha.Rendimiento;
                        }

                        if (numCalidad != null)
                        {
                            numCalidad.Value = cosecha.Calidad;
                        }

                        if (txtObservaciones != null)
                        {
                            txtObservaciones.Text = cosecha.Observaciones;
                        }

                        if (numCostoCosecha != null)
                        {
                            numCostoCosecha.Value = cosecha.Costo_Cosecha;
                        }

                        if (numPrecioVenta != null)
                        {
                            numPrecioVenta.Value = cosecha.Precio_Venta;
                        }

                        // Actualizar beneficio
                        if (lblBeneficio != null)
                        {
                            lblBeneficio.Text = cosecha.Beneficio.ToString("N2");
                            if (cosecha.Beneficio < 0)
                                lblBeneficio.ForeColor = Color.Red;
                            else if (cosecha.Beneficio > 0)
                                lblBeneficio.ForeColor = Color.Green;
                            else
                                lblBeneficio.ForeColor = Color.Black;
                        }

                        // Habilitar botones de actualizar y eliminar
                        if (btnActualizar != null)
                        {
                            btnActualizar.Enabled = true;
                        }

                        if (btnEliminar != null)
                        {
                            btnEliminar.Enabled = BLL_Seguridad.TienePermiso(PermisosSistema.CosechasEliminar);
                        }

                        if (btnGuardar != null)
                        {
                            btnGuardar.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de la cosecha: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            // Validar que se haya seleccionado un cultivo
            if (idCultivoSeleccionado == -1)
            {
                MessageBox.Show("Debe seleccionar un cultivo",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que se haya seleccionado un estado de cosecha
            if (cbxEstadoCosecha == null || cbxEstadoCosecha.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado de cosecha",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que la cantidad cosechada sea mayor a cero
            if (numCantidadCosechada.Value <= 0)
            {
                MessageBox.Show("La cantidad cosechada debe ser mayor a cero",
                    "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que la calidad esté entre 1 y 10
            if (numCalidad.Value < 1 || numCalidad.Value > 10)
            {
                MessageBox.Show("La calidad debe estar entre 1 y 10",
                    "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que el precio de venta sea mayor o igual a cero
            if (numPrecioVenta.Value < 0)
            {
                MessageBox.Show("El precio de venta no puede ser negativo",
                    "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si todo está correcto, devolver verdadero
            return true;
        }

        private void SeleccionarCultivoParaEdicion(Entitie_Cosecha cosecha)
        {
            if (cbxCultivo == null || cosecha == null)
            {
                return;
            }

            DataTable? dt = cbxCultivo.DataSource as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
            }
            else
            {
                dt = dt.Copy();
            }

            AsegurarColumna(dt, "ID_Cultivo", typeof(int));
            AsegurarColumna(dt, "Nombre_Cultivo", typeof(string));
            AsegurarColumna(dt, "Tipo_Cultivo", typeof(string));
            AsegurarColumna(dt, "Categoria_Cultivo", typeof(string));
            AsegurarColumna(dt, "Cantidad", typeof(decimal));
            AsegurarColumna(dt, "Tipo_Suelo", typeof(string));
            AsegurarColumna(dt, "Fecha_Plantacion", typeof(DateTime));
            AsegurarColumna(dt, "Fecha_Cosecha", typeof(DateTime));
            AsegurarColumna(dt, "Costo_Siembra", typeof(decimal));

            bool existe = false;
            foreach (DataRow row in dt.Rows)
            {
                if (row["ID_Cultivo"] != DBNull.Value && Convert.ToInt32(row["ID_Cultivo"]) == cosecha.ID_Cultivo)
                {
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                DataRow row = dt.NewRow();
                row["ID_Cultivo"] = cosecha.ID_Cultivo;
                row["Nombre_Cultivo"] = cosecha.Nombre_Cultivo;
                row["Tipo_Cultivo"] = cosecha.Tipo_Cultivo;
                row["Categoria_Cultivo"] = cosecha.Categoria_Cultivo;
                row["Cantidad"] = cosecha.Cantidad_Sembrada;
                row["Tipo_Suelo"] = "";
                row["Fecha_Plantacion"] = cosecha.Fecha_Plantacion == DateTime.MinValue ? DateTime.Now : cosecha.Fecha_Plantacion;
                row["Fecha_Cosecha"] = cosecha.Fecha_Cosecha == DateTime.MinValue ? DateTime.Now : cosecha.Fecha_Cosecha;
                row["Costo_Siembra"] = cosecha.Costo_Siembra;
                dt.Rows.InsertAt(row, 0);
            }

            cbxCultivo.DataSource = dt;
            cbxCultivo.DisplayMember = "Nombre_Cultivo";
            cbxCultivo.ValueMember = "ID_Cultivo";
            cbxCultivo.SelectedValue = cosecha.ID_Cultivo;
        }

        private void AsegurarColumna(DataTable dt, string nombreColumna, Type tipo)
        {
            if (!dt.Columns.Contains(nombreColumna))
            {
                dt.Columns.Add(nombreColumna, tipo);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void PreseleccionarCultivo(int idCultivo)
        {
            try
            {
                // Guardar el ID del cultivo a preseleccionar
                this.idCultivoPreseleccionado = idCultivo;
                if (this.IsHandleCreated)
                {
                    SeleccionarCultivoPorId(idCultivo);
                }

                // Este valor se usará cuando se cargue el formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al preseleccionar el cultivo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

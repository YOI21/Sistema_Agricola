using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmRegistroTerreno : Form
    {
        private int idTerreno = -1;
        private bool esNuevoRegistro = true;

        public FrmRegistroTerreno()
        {
            InitializeComponent();
        }

        private void FrmRegistroTerreno_Load(object sender, EventArgs e)
        {
            CargarTerrenos();
            InicializarControles();
            GenerarNuevoID();
        }

        private void InicializarControles()
        {
            // Rellenar combo box de tipos de suelo
            cmbTipoSuelo.Items.Clear();
            cmbTipoSuelo.Items.Add("Arcilloso");
            cmbTipoSuelo.Items.Add("Arenoso");
            cmbTipoSuelo.Items.Add("Franco");
            cmbTipoSuelo.Items.Add("Limoso");
            cmbTipoSuelo.Items.Add("Franco arenoso");
            cmbTipoSuelo.Items.Add("Franco arcilloso");
            cmbTipoSuelo.SelectedIndex = 2; // Franco por defecto

            // Rellenar combo box de unidades de medida
            cmbUnidadMedida.Items.Clear();
            cmbUnidadMedida.Items.Add("Metros Cuadrados");
            cmbUnidadMedida.Items.Add("Hectáreas");
            cmbUnidadMedida.Items.Add("Tareas");
            cmbUnidadMedida.SelectedIndex = 0; // Metros cuadrados por defecto

            // Rellenar combo box de estados
            cmbEstadoActual.Items.Clear();
            cmbEstadoActual.Items.Add("Disponible");
            cmbEstadoActual.Items.Add("En uso");
            cmbEstadoActual.Items.Add("En descanso");
            cmbEstadoActual.SelectedIndex = 0; // Disponible por defecto

            cmbProvincia.Items.Clear();
            cmbProvincia.Items.AddRange(
                BLL_RecomendacionesAgricolas.Provincias.Cast<object>().ToArray());

            cmbZonaAgroclimatica.Items.Clear();
            cmbZonaAgroclimatica.Items.AddRange(
                BLL_RecomendacionesAgricolas.ZonasAgroclimaticas.Cast<object>().ToArray());

            cmbDrenaje.Items.Clear();
            cmbDrenaje.Items.AddRange(new object[] { "Bueno", "Moderado", "Deficiente", "Excesivo" });

            cmbFuenteAgua.Items.Clear();
            cmbFuenteAgua.Items.AddRange(new object[]
            {
                "Ninguna", "Acueducto", "Canal", "Pozo", "Río o arroyo", "Reservorio", "Lluvia"
            });
            cmbFuenteAgua.SelectedIndex = 0;
        }

        private void CargarTerrenos()
        {
            try
            {
                DataTable dtTerrenos = BLL_Terreno.Listar_Terrenos();
                dgvTerrenos.DataSource = dtTerrenos;

                // Formatear las columnas
                FormatearColumnasGrid();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.CargarTerrenos");
            }
        }

        private void FormatearColumnasGrid()
        {
            if (dgvTerrenos.Columns.Count > 0)
            {
                // Ocultar columnas que no queremos mostrar directamente
                if (dgvTerrenos.Columns.Contains("ID_Terreno"))
                    dgvTerrenos.Columns["ID_Terreno"].Visible = false;

                // Formato para columnas numéricas
                if (dgvTerrenos.Columns.Contains("Area"))
                    dgvTerrenos.Columns["Area"].DefaultCellStyle.Format = "N2";

                if (dgvTerrenos.Columns.Contains("pH"))
                    dgvTerrenos.Columns["pH"].DefaultCellStyle.Format = "N1";

                if (dgvTerrenos.Columns.Contains("Pendiente"))
                    dgvTerrenos.Columns["Pendiente"].DefaultCellStyle.Format = "N2";

                if (dgvTerrenos.Columns.Contains("Humedad"))
                    dgvTerrenos.Columns["Humedad"].DefaultCellStyle.Format = "N2";

                // Fecha
                if (dgvTerrenos.Columns.Contains("FechaRegistro"))
                    dgvTerrenos.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void LimpiarFormulario()
        {
            txtIDTerreno.Text = string.Empty;
            txtNombre.Text = string.Empty;
            numArea.Value = 0;
            cmbUnidadMedida.SelectedIndex = 0;
            cmbTipoSuelo.SelectedIndex = 2; // Franco por defecto
            numPH.Value = 0; // cero indica que todavía no se ha medido
            numPendiente.Value = 0;
            numHumedad.Value = 0;
            txtUbicacion.Text = string.Empty;
            txtPropietario.Text = string.Empty;
            cmbEstadoActual.SelectedIndex = 0;
            txtObservaciones.Text = string.Empty;
            cmbProvincia.SelectedIndex = -1;
            cmbZonaAgroclimatica.SelectedIndex = -1;
            numPrecipitacionAnual.Value = 0;
            cmbDrenaje.SelectedIndex = -1;
            cmbFuenteAgua.SelectedIndex = 0;
            chkDisponibilidadRiego.Checked = false;

            esNuevoRegistro = true;
            idTerreno = -1;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;

            GenerarNuevoID();
        }

        private void GenerarNuevoID()
        {
            // Esta función es simbólica ya que el ID se genera en la DB
            // Pero podemos mostrar un ID temporal o un placeholder
            Random random = new Random();
            string tempID = "T" + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString();
            txtIDTerreno.Text = tempID;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                // Cambiar el cursor
                this.UseWaitCursor = true;
                btnGuardar.Enabled = false;

                string resultado;

                if (esNuevoRegistro)
                {
                    // Método sobrecargado que acepta todos los parámetros individuales
                    resultado = BLL_Terreno.Registrar_Terreno(
                        txtNombre.Text,
                        numArea.Value,
                        cmbUnidadMedida.Text,
                        cmbTipoSuelo.Text,
                        numPH.Value,
                        numPendiente.Value,
                        numHumedad.Value,
                        txtUbicacion.Text,
                        txtPropietario.Text,
                        cmbEstadoActual.Text,
                        txtObservaciones.Text,
                        cmbDrenaje.Text,
                        cmbFuenteAgua.Text,
                        cmbProvincia.Text,
                        cmbZonaAgroclimatica.Text,
                        numPrecipitacionAnual.Value,
                        chkDisponibilidadRiego.Checked
                    );
                }
                else
                {
                    resultado = BLL_Terreno.Actualizar_Terreno(
                        idTerreno,
                        txtNombre.Text,
                        numArea.Value,
                        cmbUnidadMedida.Text,
                        cmbTipoSuelo.Text,
                        numPH.Value,
                        numPendiente.Value,
                        numHumedad.Value,
                        txtUbicacion.Text,
                        txtPropietario.Text,
                        cmbEstadoActual.Text,
                        txtObservaciones.Text,
                        cmbDrenaje.Text,
                        cmbFuenteAgua.Text,
                        cmbProvincia.Text,
                        cmbZonaAgroclimatica.Text,
                        numPrecipitacionAnual.Value,
                        chkDisponibilidadRiego.Checked
                    );
                }

                // Restaurar cursor y botón
                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Verificar el resultado para decidir si recargar datos
                if (resultado.Contains("registrado con éxito") || resultado.Contains("actualizado con éxito"))
                {
                    CargarTerrenos();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                // Restaurar cursor y botón en caso de error
                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.Guardar");
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el terreno",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (numArea.Value <= 0)
            {
                MessageBox.Show("El área debe ser mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numArea.Focus();
                return false;
            }

            if (cmbTipoSuelo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de suelo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoSuelo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPropietario.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del propietario",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPropietario.Focus();
                return false;
            }

            if (cmbProvincia.SelectedIndex < 0 || cmbZonaAgroclimatica.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione la provincia y la zona agroclimática del terreno.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProvincia.Focus();
                return false;
            }

            if (cmbDrenaje.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione la capacidad de drenaje del terreno.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDrenaje.Focus();
                return false;
            }

            if (numPrecipitacionAnual.Value <= 0)
            {
                MessageBox.Show("Indique la precipitación anual estimada en milímetros.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrecipitacionAnual.Focus();
                return false;
            }

            if (chkDisponibilidadRiego.Checked && cmbFuenteAgua.Text == "Ninguna")
            {
                MessageBox.Show("Seleccione la fuente de agua utilizada para el riego.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFuenteAgua.Focus();
                return false;
            }

            return true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idTerreno == -1)
            {
                MessageBox.Show("Debe seleccionar un terreno para eliminar",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de eliminar este terreno? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string resultado = BLL_Terreno.Eliminar_Terreno(idTerreno);
                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarTerrenos();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.Eliminar");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvTerrenos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtener el ID del terreno seleccionado
                    idTerreno = Convert.ToInt32(dgvTerrenos.Rows[e.RowIndex].Cells["ID"].Value);

                    // Cargar los datos del terreno
                    Entitie_Terreno? terreno = BLL_Terreno.Obtener_Terreno(idTerreno);

                    if (terreno != null)
                    {
                        esNuevoRegistro = false;

                        txtIDTerreno.Text = terreno.ID_Terreno.ToString();
                        txtNombre.Text = terreno.Nombre;
                        numArea.Value = terreno.Area;

                        // Seleccionar la unidad de medida
                        for (int i = 0; i < cmbUnidadMedida.Items.Count; i++)
                        {
                            if (Convert.ToString(cmbUnidadMedida.Items[i]) ==
                                terreno.UnidadMedida)
                            {
                                cmbUnidadMedida.SelectedIndex = i;
                                break;
                            }
                        }

                        // Seleccionar el tipo de suelo
                        for (int i = 0; i < cmbTipoSuelo.Items.Count; i++)
                        {
                            if (Convert.ToString(cmbTipoSuelo.Items[i]) ==
                                terreno.TipoSuelo)
                            {
                                cmbTipoSuelo.SelectedIndex = i;
                                break;
                            }
                        }

                        numPH.Value = LimitarValor(terreno.pH, numPH);
                        numPendiente.Value = LimitarValor(terreno.Pendiente, numPendiente);
                        numHumedad.Value = LimitarValor(terreno.Humedad, numHumedad);
                        SeleccionarTexto(cmbProvincia, terreno.Provincia);
                        SeleccionarTexto(cmbZonaAgroclimatica, terreno.ZonaAgroclimatica);
                        numPrecipitacionAnual.Value = LimitarValor(
                            terreno.PrecipitacionAnual, numPrecipitacionAnual);
                        SeleccionarTexto(cmbDrenaje, terreno.Drenaje);
                        SeleccionarTexto(cmbFuenteAgua, terreno.FuenteAgua);
                        chkDisponibilidadRiego.Checked = terreno.DisponibilidadRiego;
                        txtUbicacion.Text = terreno.Ubicacion;
                        txtPropietario.Text = terreno.Propietario;

                        // Seleccionar el estado actual
                        for (int i = 0; i < cmbEstadoActual.Items.Count; i++)
                        {
                            if (Convert.ToString(cmbEstadoActual.Items[i]) ==
                                terreno.EstadoActual)
                            {
                                cmbEstadoActual.SelectedIndex = i;
                                break;
                            }
                        }

                        txtObservaciones.Text = terreno.Observaciones;

                        // Habilitar botones de actualizar y eliminar
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnGuardar.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.Seleccionar");
                }
            }
        }

        private void btnComprobarCompatibilidad_Click(object sender, EventArgs e)
        {
            if (idTerreno == -1)
            {
                MessageBox.Show("Debe seleccionar un terreno para comprobar compatibilidad",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear una instancia de FrmComprobarCompatibilidad y pasarle el ID del terreno
            try
            {
                FrmComprobarCompatibilidad frmCompatibilidad = new FrmComprobarCompatibilidad(idTerreno);
                frmCompatibilidad.ShowDialog();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.AbrirCompatibilidad");
            }
        }

        private void btnVerRecomendaciones_Click(object sender, EventArgs e)
        {
            if (idTerreno == -1)
            {
                MessageBox.Show("Debe seleccionar un terreno para ver recomendaciones",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                List<string> recomendaciones = BLL_Terreno.RecomendarCultivosPorTerreno(idTerreno);

                if (recomendaciones.Count == 0)
                {
                    MessageBox.Show("No hay recomendaciones disponibles para este terreno.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string mensajeRecomendaciones = "Cultivos recomendados para este terreno:\n\n";
                foreach (string cultivo in recomendaciones)
                {
                    mensajeRecomendaciones += "• " + cultivo + "\n";
                }

                MessageBox.Show(mensajeRecomendaciones,
                    "Recomendaciones de Cultivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmRegistroTerreno.Recomendaciones");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numArea_ValueChanged(object sender, EventArgs e)
        {
            // Actualizar etiqueta con equivalencia de área en hectáreas
            ActualizarEtiquetaAreaEquivalente();
        }

        private void cmbUnidadMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Actualizar etiqueta con equivalencia de área en hectáreas
            ActualizarEtiquetaAreaEquivalente();
        }

        private void ActualizarEtiquetaAreaEquivalente()
        {
            try
            {
                decimal area = numArea.Value;
                decimal areaEnHectareas = 0;

                switch (cmbUnidadMedida.Text)
                {
                    case "Metros Cuadrados":
                        areaEnHectareas = area / 10000; // 1 hectárea = 10,000 m²
                        lblAreaEquivalente.Text = $"Equivalente a {areaEnHectareas:N4} hectáreas";
                        break;
                    case "Hectáreas":
                        areaEnHectareas = area;
                        lblAreaEquivalente.Text = $"Equivalente a {area * 10000:N0} metros cuadrados";
                        break;
                    case "Tareas":
                        // En Rep. Dominicana, 1 tarea ≈ 628.86 m²
                        areaEnHectareas = (area * 628.86m) / 10000;
                        lblAreaEquivalente.Text = $"Equivalente a {areaEnHectareas:N4} hectáreas";
                        break;
                    default:
                        lblAreaEquivalente.Text = "";
                        break;
                }
            }
            catch (Exception)
            {
                lblAreaEquivalente.Text = "";
            }
        }

        private void dgvTerrenos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zona = BLL_RecomendacionesAgricolas.InferirZona(cmbProvincia.Text);
            if (!string.IsNullOrWhiteSpace(zona))
                SeleccionarTexto(cmbZonaAgroclimatica, zona);
        }

        private static void SeleccionarTexto(ComboBox combo, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                combo.SelectedIndex = -1;
                return;
            }

            int indice = combo.FindStringExact(valor);
            combo.SelectedIndex = indice;
        }

        private static decimal LimitarValor(decimal valor, NumericUpDown control)
        {
            return Math.Min(control.Maximum, Math.Max(control.Minimum, valor));
        }
    }
}

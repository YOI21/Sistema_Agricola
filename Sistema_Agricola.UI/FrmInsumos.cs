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
    public partial class FrmInsumos : Form
    {
        private int idInsumo = -1;
        private bool esNuevoRegistro = true;

        public FrmInsumos()
        {
            InitializeComponent();
        }

        private void FrmInsumos_Load(object sender, EventArgs e)
        {
            CargarInsumos();
            CargarTiposInsumo();
            LimpiarFormulario();
        }

        private void CargarTiposInsumo()
        {
            // Agregar tipos de insumos comunes
            cmbTipoInsumo.Items.Clear();
            cmbTipoInsumo.Items.Add("Fertilizante");
            cmbTipoInsumo.Items.Add("Semilla");
            cmbTipoInsumo.Items.Add("Herbicida");
            cmbTipoInsumo.Items.Add("Pesticida");
            cmbTipoInsumo.Items.Add("Fungicida");
            cmbTipoInsumo.Items.Add("Herramienta");
            cmbTipoInsumo.Items.Add("Equipo");
            cmbTipoInsumo.Items.Add("Otro");
            cmbTipoInsumo.SelectedIndex = 0;
        }

        private void CargarInsumos()
        {
            try
            {
                DataTable dtInsumos = BLL_Insumos.ListarInsumos();
                dgvInsumos.DataSource = dtInsumos;
                FormatearColumnasGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los insumos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasGrid()
        {
            if (dgvInsumos.Columns.Count > 0)
            {
                // Ocultar ID si es necesario
                if (dgvInsumos.Columns.Contains("Id_Insumo"))
                    dgvInsumos.Columns["Id_Insumo"].Visible = false;

                // Formato para columnas numéricas
                if (dgvInsumos.Columns.Contains("Costo"))
                    dgvInsumos.Columns["Costo"].DefaultCellStyle.Format = "C2";

                // Fecha
                if (dgvInsumos.Columns.Contains("Fecha"))
                    dgvInsumos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Ajustar anchos de columna automáticamente
                dgvInsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LimpiarFormulario()
        {
            txtIdInsumo.Text = "";
            txtNombre.Text = "";
            cmbTipoInsumo.SelectedIndex = 0;
            numCosto.Value = 0;
            dtpFecha.Value = DateTime.Now;
            numCantidad.Value = 0;

            esNuevoRegistro = true;
            idInsumo = -1;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el insumo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (cmbTipoInsumo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de insumo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoInsumo.Focus();
                return false;
            }

            if (numCosto.Value <= 0)
            {
                MessageBox.Show("El costo debe ser mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCosto.Focus();
                return false;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                this.UseWaitCursor = true;
                btnGuardar.Enabled = false;

                string resultado;

                if (esNuevoRegistro)
                {
                    // Insertar nuevo insumo
                    resultado = BLL_Insumos.RegistrarInsumo(
                        txtNombre.Text,
                        cmbTipoInsumo.Text,
                        (int)numCosto.Value,
                        dtpFecha.Value,
                        (int)numCantidad.Value);
                }
                else
                {
                    // Actualizar insumo existente
                    resultado = BLL_Insumos.ActualizarInsumo(
                        idInsumo,
                        txtNombre.Text,
                        cmbTipoInsumo.Text,
                        (int)numCosto.Value,
                        dtpFecha.Value,
                        (int)numCantidad.Value);
                }

                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                MessageBox.Show(resultado, "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Contains("registrado con éxito") ||
                    resultado.Contains("actualizado con éxito"))
                {
                    CargarInsumos();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;
                btnGuardar.Enabled = true;

                MessageBox.Show("Error al guardar el insumo: " + ex.Message +
                    "\nDetalles técnicos: " + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idInsumo == -1)
            {
                MessageBox.Show("Debe seleccionar un insumo para eliminar",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de eliminar este insumo? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string resultado = BLL_Insumos.EliminarInsumo(idInsumo);
                    MessageBox.Show(resultado, "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInsumos();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el insumo: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvInsumos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtener el ID del insumo seleccionado
                    idInsumo = Convert.ToInt32(dgvInsumos.Rows[e.RowIndex].Cells["Id_Insumo"].Value);

                    // Cargar los datos del insumo
                    Entitie_Insumo? insumo = BLL_Insumos.ObtenerInsumo(idInsumo);
                    if (insumo != null)
                    {
                        esNuevoRegistro = false;

                        txtIdInsumo.Text = insumo.Id_Insumo.ToString();
                        txtNombre.Text = insumo.Nombre;

                        // Seleccionar el tipo de insumo
                        for (int i = 0; i < cmbTipoInsumo.Items.Count; i++)
                        {
                            if (Convert.ToString(cmbTipoInsumo.Items[i]) == insumo.Tipo)
                            {
                                cmbTipoInsumo.SelectedIndex = i;
                                break;
                            }
                        }

                        numCosto.Value = insumo.Costo;
                        dtpFecha.Value = insumo.Fecha;
                        numCantidad.Value = insumo.Cantidad;

                        // Habilitar botones de actualizar y eliminar
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnGuardar.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos del insumo: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

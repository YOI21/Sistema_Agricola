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
    public partial class FrmAsignacionMaquinaria : Form
    {
        private int idAsignacionSeleccionada = -1;

        public FrmAsignacionMaquinaria()
        {
            InitializeComponent();
        }

        private void FrmAsignacionMaquinaria_Load(object sender, EventArgs e)
        {
            // Cargar lista de operadores y maquinaria disponible
            CargarOperadores();
            CargarMaquinariaDisponible();
            CargarAsignacionesActivas();
        }

        private void CargarOperadores()
        {
            try
            {
                DataTable dt = BLL_Asignacion_Maquinaria.Listar_Operadores();

                // Verificar si hay operadores
                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbOperadores.DataSource = dt;
                    cmbOperadores.DisplayMember = "Nombre";
                    cmbOperadores.ValueMember = "ID_Trabajador";

                    // Crear columna de nombre completo con posición
                    if (dt.Columns.Contains("Apellido") && dt.Columns.Contains("Posicion"))
                    {
                        DataColumn expresionColumn = new DataColumn("NombreCompleto", typeof(string),
                            "Nombre + ' ' + Apellido + ' - ' + Posicion");
                        dt.Columns.Add(expresionColumn);
                        cmbOperadores.DisplayMember = "NombreCompleto";
                    }

                    // Por defecto, no seleccionar ningún operador inicialmente
                    cmbOperadores.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(
                        "No se encontraron operadores registrados en el sistema.\n" +
                        "Por favor, registre operadores para poder realizar asignaciones.",
                        "Sin operadores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de operadores: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMaquinariaDisponible()
        {
            try
            {
                // Mostrar un indicador de carga
                this.Cursor = Cursors.WaitCursor;

                // Intentamos obtener la lista de maquinaria disponible
                DataTable dt = BLL_Asignacion_Maquinaria.Listar_Maquinaria_Disponible();

                if (dt == null)
                {
                    MessageBox.Show(
                        "No se pudo obtener información sobre maquinaria disponible. " +
                        "La consulta a la base de datos devolvió un resultado nulo.",
                        "Error de conexión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    btnAsignar.Enabled = false;
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay maquinaria disponible para asignar.\n" +
                        "Todas las máquinas están asignadas o no hay máquinas registradas.",
                        "Sin maquinaria disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnAsignar.Enabled = false;
                    return;
                }

                // Verificar si el primer registro tiene ID_Maquina = 0 (mensaje especial)
                if (dt.Rows.Count > 0 && dt.Rows[0]["ID_Maquina"].ToString() == "0")
                {
                    string mensaje = dt.Rows[0]["Nombre_Maquina"].ToString() ?? string.Empty;
                    string detalle = "";

                    if (dt.Columns.Contains("Tipo_Maquinaria"))
                    {
                        detalle = dt.Rows[0]["Tipo_Maquinaria"].ToString() ?? string.Empty;
                    }

                    MessageBox.Show(
                        mensaje + "\n" + detalle,
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btnAsignar.Enabled = false;
                    return;
                }

                // Si llegamos aquí, hay maquinaria disponible
                cmbMaquinaria.DataSource = dt;
                cmbMaquinaria.DisplayMember = "Nombre_Maquina";
                cmbMaquinaria.ValueMember = "ID_Maquina";

                // Agregar información del tipo de maquinaria si está disponible
                if (dt.Columns.Contains("Tipo_Maquinaria"))
                {
                    DataColumn expresionColumn = new DataColumn(
                        "InfoCompleta",
                        typeof(string),
                        "Nombre_Maquina + ' - ' + Tipo_Maquinaria");
                    dt.Columns.Add(expresionColumn);
                    cmbMaquinaria.DisplayMember = "InfoCompleta";
                }

                // Por defecto, no seleccionar ninguna maquinaria inicialmente
                if (cmbMaquinaria.Items.Count > 0)
                {
                    cmbMaquinaria.SelectedIndex = -1;
                    btnAsignar.Enabled = true;
                }
                else
                {
                    btnAsignar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // Mostrar detalles del error
                MessageBox.Show(
                    "Error al cargar la lista de maquinaria disponible:\n\n" +
                    ex.Message + "\n\n" +
                    "Detalles técnicos: " + ex.StackTrace,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnAsignar.Enabled = false;
            }
            finally
            {
                // Restaurar el cursor normal
                this.Cursor = Cursors.Default;
            }
        }

        private void CargarAsignacionesActivas()
        {
            try
            {
                DataTable dt = BLL_Asignacion_Maquinaria.Listar_Asignaciones_Activas();
                dgvAsignaciones.DataSource = dt;

                // Configurar las columnas para mejor visualización
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de asignaciones activas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            // Si el DataGridView tiene columnas
            if (dgvAsignaciones.Columns.Count > 0)
            {
                // Ocultar columnas de IDs que no necesitamos mostrar
                if (dgvAsignaciones.Columns.Contains("ID_Trabajador"))
                    dgvAsignaciones.Columns["ID_Trabajador"].Visible = false;
                if (dgvAsignaciones.Columns.Contains("ID_Maquina"))
                    dgvAsignaciones.Columns["ID_Maquina"].Visible = false;

                // Configurar ancho de columnas
                if (dgvAsignaciones.Columns.Contains("ID_Asignacion"))
                    dgvAsignaciones.Columns["ID_Asignacion"].Width = 80;
                if (dgvAsignaciones.Columns.Contains("Nombre_Trabajador"))
                    dgvAsignaciones.Columns["Nombre_Trabajador"].Width = 200;
                if (dgvAsignaciones.Columns.Contains("Posicion"))
                    dgvAsignaciones.Columns["Posicion"].Width = 150;
                if (dgvAsignaciones.Columns.Contains("Nombre_Maquina"))
                    dgvAsignaciones.Columns["Nombre_Maquina"].Width = 200;
                if (dgvAsignaciones.Columns.Contains("Tipo_Maquinaria"))
                    dgvAsignaciones.Columns["Tipo_Maquinaria"].Width = 150;
                if (dgvAsignaciones.Columns.Contains("Fecha_Asignacion"))
                {
                    dgvAsignaciones.Columns["Fecha_Asignacion"].Width = 150;
                    dgvAsignaciones.Columns["Fecha_Asignacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                if (dgvAsignaciones.Columns.Contains("Observaciones"))
                    dgvAsignaciones.Columns["Observaciones"].Width = 300;
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado un operador y una maquinaria
                if (cmbOperadores.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione un operador para la asignación.",
                        "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbOperadores.Focus();
                    return;
                }

                if (cmbMaquinaria.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione una maquinaria para asignar.",
                        "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMaquinaria.Focus();
                    return;
                }

                // Obtener los IDs seleccionados
                int idTrabajador = Convert.ToInt32(cmbOperadores.SelectedValue);
                int idMaquina = Convert.ToInt32(cmbMaquinaria.SelectedValue);
                string observaciones = txtObservaciones.Text;

                // Llamar al método BLL para realizar la asignación
                string resultado = BLL_Asignacion_Maquinaria.Asignar_Maquinaria(
                    idTrabajador, idMaquina, observaciones);

                // Mostrar el resultado
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                    resultado.ToLower().Contains("error") ? MessageBoxIcon.Error : MessageBoxIcon.Information);

                // Si la operación fue exitosa, actualizar las listas
                if (!resultado.ToLower().Contains("error"))
                {
                    CargarMaquinariaDisponible();
                    CargarAsignacionesActivas();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar maquinaria: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya una asignación seleccionada
                if (idAsignacionSeleccionada <= 0)
                {
                    MessageBox.Show("Por favor seleccione una asignación para finalizar.",
                        "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmar la finalización
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea finalizar esta asignación?\n" +
                    "La maquinaria estará disponible para otras asignaciones.",
                    "Confirmar finalización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Llamar al método BLL para finalizar la asignación
                    string resultado = BLL_Asignacion_Maquinaria.Finalizar_Asignacion(idAsignacionSeleccionada);

                    // Mostrar el resultado
                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                        resultado.ToLower().Contains("error") ? MessageBoxIcon.Error : MessageBoxIcon.Information);

                    // Si la operación fue exitosa, actualizar las listas
                    if (!resultado.ToLower().Contains("error"))
                    {
                        CargarMaquinariaDisponible();
                        CargarAsignacionesActivas();
                        idAsignacionSeleccionada = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al finalizar asignación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            cmbOperadores.SelectedIndex = -1;
            cmbMaquinaria.SelectedIndex = -1;
            txtObservaciones.Clear();
            idAsignacionSeleccionada = -1;
        }

        private void dgvAsignaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que el clic sea en una fila válida
                if (e.RowIndex >= 0)
                {
                    // Obtener el ID de la asignación seleccionada
                    idAsignacionSeleccionada = Convert.ToInt32(dgvAsignaciones.Rows[e.RowIndex].Cells["ID_Asignacion"].Value);

                    // Opcionalmente: resaltar la fila seleccionada
                    dgvAsignaciones.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar asignación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

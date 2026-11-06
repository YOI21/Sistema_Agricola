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
    public partial class FrmAlertas : Form
    {
        private bool esNuevoRegistro = true;
        private int idCultivoSeleccionado = -1;
        private int idAlertaSeleccionada = -1;

        public FrmAlertas()
        {
            InitializeComponent();
        }

        #region Eventos del Formulario

        private void FrmAlertas_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarControles();
                CargarCultivos();
                CargarTodasLasAlertas();
                CargarAlertasPendientes();
                CargarAlertasProximas();

                // Iniciar el timer para verificación periódica de alertas
                timerAlertas.Start();

                // Verificar alertas al cargar
                VerificarAlertas(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                // Crear objeto de alerta con los datos del formulario
                Entitie_Alerta alerta = CrearObjetoAlerta();

                // Guardar alerta en base de datos
                int idAlerta = BLL_Alertas.CrearAlerta(alerta);

                if (idAlerta > 0)
                {
                    MessageBox.Show("Alerta guardada correctamente",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la alerta",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la alerta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LimpiarFormulario();
            RecargarListasAlertas();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            if (esNuevoRegistro || idAlertaSeleccionada == -1)
            {
                MessageBox.Show("Debe seleccionar una alerta para actualizar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Crear objeto de alerta con los datos del formulario
                Entitie_Alerta alerta = CrearObjetoAlerta();
                alerta.ID_Alerta = idAlertaSeleccionada;

                // Actualizar alerta en base de datos
                bool resultado = BLL_Alertas.ActualizarAlerta(alerta);

                if (resultado)
                {
                    MessageBox.Show("Alerta actualizada correctamente",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    RecargarListasAlertas();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la alerta",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la alerta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (esNuevoRegistro || idAlertaSeleccionada == -1)
            {
                MessageBox.Show("Debe seleccionar una alerta para eliminar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar esta alerta?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    // Eliminar alerta en base de datos
                    bool resultado = BLL_Alertas.EliminarAlerta(idAlertaSeleccionada);

                    if (resultado)
                    {
                        MessageBox.Show("Alerta eliminada correctamente",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarFormulario();
                        RecargarListasAlertas();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la alerta",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la alerta: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnVerificarAlertas_Click(object sender, EventArgs e)
        {
            VerificarAlertas();
        }

        private void btnGenerarAlertas_Click(object sender, EventArgs e)
        {
            GenerarAlertasAutomaticas();
        }

        private void tabControlAlertas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlAlertas.SelectedIndex)
            {
                case 0: // Todas las alertas
                    CargarTodasLasAlertas();
                    break;
                case 1: // Alertas pendientes
                    CargarAlertasPendientes();
                    break;
                case 2: // Alertas próximas
                    CargarAlertasProximas();
                    break;
            }
        }

        private void dgvTodasAlertas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarDatosAlerta(dgvTodasAlertas, e.RowIndex);
            }
        }

        private void dgvAlertasPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarDatosAlerta(dgvAlertasPendientes, e.RowIndex);
            }
        }

        private void dgvAlertasProximas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarDatosAlerta(dgvAlertasProximas, e.RowIndex);
            }
        }

        private void timerAlertas_Tick(object sender, EventArgs e)
        {
            // Verificar alertas automáticamente cada minuto
            VerificarAlertas(false);
        }

        #endregion

        #region Métodos Auxiliares

        private void ConfigurarControles()
        {
            // Configurar ComboBox
            cmbEstado.SelectedIndex = 0; // "Pendiente" por defecto
            cmbPrioridad.SelectedIndex = 1; // "Media" por defecto
            cmbTipoAlerta.SelectedIndex = 0; // Primer elemento por defecto

            // Fechas por defecto
            dtpFechaProgramada.Value = DateTime.Now.AddDays(7);
            lblFechaCreacion.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // Configurar botones
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            // Configurar días de anticipación
            numDiasAnticipacion.Value = 7;
            txtTitulo.MaxLength = 150;
            txtDescripcion.MaxLength = 500;
        }

        private void CargarCultivos()
        {
            try
            {
                DataTable dt = BLL_Siembra.Mostrar_Cultivos();

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbCultivo.DataSource = dt;
                    cmbCultivo.DisplayMember = "Cultivos";
                    cmbCultivo.ValueMember = "ID_Cultivo";
                    cmbCultivo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar cultivos: " + ex.Message);
            }
        }

        private void CargarTodasLasAlertas()
        {
            try
            {
                if (cmbCultivo.SelectedIndex != -1 && cmbCultivo.SelectedValue != null)
                {
                    idCultivoSeleccionado = Convert.ToInt32(cmbCultivo.SelectedValue);
                    DataTable dt = BLL_Alertas.ObtenerAlertasPorCultivo(idCultivoSeleccionado);
                    ConfigurarDataGridView(dgvTodasAlertas, dt);
                }
                else
                {
                    DataTable dt = BLL_Alertas.ObtenerTodasAlertas();
                    ConfigurarDataGridView(dgvTodasAlertas, dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alertas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAlertasPendientes()
        {
            try
            {
                DataTable dt = BLL_Alertas.ObtenerAlertasPendientes();
                ConfigurarDataGridView(dgvAlertasPendientes, dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alertas pendientes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAlertasProximas()
        {
            try
            {
                DataTable dt = BLL_Alertas.ObtenerAlertasProximas();
                ConfigurarDataGridView(dgvAlertasProximas, dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alertas próximas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView(DataGridView dgv, DataTable dt)
        {
            dgv.CellFormatting -= DgvAlertas_CellFormatting;
            dgv.DataSource = dt;
            dgv.CellFormatting += DgvAlertas_CellFormatting;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv.Columns.Contains("ID_Alerta"))
                dgv.Columns["ID_Alerta"].HeaderText = "ID";

            if (dgv.Columns.Contains("ID_Cultivo"))
                dgv.Columns["ID_Cultivo"].Visible = false;

            if (dgv.Columns.Contains("ID_Agricultor"))
                dgv.Columns["ID_Agricultor"].Visible = false;

            if (dgv.Columns.Contains("Nombre_Cultivo"))
                dgv.Columns["Nombre_Cultivo"].HeaderText = "Cultivo";

            ConfigurarColumna(dgv, "Agricultor", "Agricultor", true);
            ConfigurarColumna(dgv, "Email_Agricultor", "Gmail", true);

            if (dgv.Columns.Contains("Fecha_Programada"))
                dgv.Columns["Fecha_Programada"].HeaderText = "Fecha";

            if (dgv.Columns.Contains("Fecha_Programada"))
                dgv.Columns["Fecha_Programada"].DefaultCellStyle.Format = "dd/MM/yyyy";

            if (dgv.Columns.Contains("Fecha_Creacion"))
            {
                dgv.Columns["Fecha_Creacion"].HeaderText = "Creada";
                dgv.Columns["Fecha_Creacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            ConfigurarColumna(dgv, "Tipo_Alerta", "Tipo", true);
            ConfigurarColumna(dgv, "Dias_Anticipacion", "Aviso dias", true);
            ConfigurarColumna(dgv, "Dias_Restantes", "Faltan", true);
            ConfigurarColumna(dgv, "Notificacion_Enviada", "Notificada", false);
            ConfigurarColumna(dgv, "Correo_Enviado", "Gmail enviado", false);
            ConfigurarColumna(dgv, "Resultado_Correo", "Resultado Gmail", true);
            ConfigurarColumna(dgv, "Situacion", "Situacion", true);

            if (dgv.Columns.Contains("Descripcion"))
            {
                dgv.Columns["Descripcion"].FillWeight = 160;
            }

            if (dgv.Columns.Contains("Titulo"))
            {
                dgv.Columns["Titulo"].FillWeight = 140;
            }

            dgv.ClearSelection();
        }

        private void ConfigurarColumna(DataGridView dgv, string nombre, string encabezado, bool visible)
        {
            if (!dgv.Columns.Contains(nombre))
            {
                return;
            }

            dgv.Columns[nombre].HeaderText = encabezado;
            dgv.Columns[nombre].Visible = visible;
        }

        private void DgvAlertas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView? dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count)
            {
                return;
            }

            DataGridViewCellStyle estilo = e.CellStyle ?? new DataGridViewCellStyle();
            e.CellStyle = estilo;

            DataGridViewRow fila = dgv.Rows[e.RowIndex];
            string estado = LeerValorFila(fila, "Estado");
            string prioridad = LeerValorFila(fila, "Prioridad");
            string situacion = LeerValorFila(fila, "Situacion");

            if (estado == "Completada")
            {
                estilo.BackColor = Color.FromArgb(232, 245, 233);
                estilo.ForeColor = Color.FromArgb(46, 125, 50);
            }
            else if (estado == "Cancelada")
            {
                estilo.BackColor = Color.FromArgb(238, 238, 238);
                estilo.ForeColor = Color.Gray;
            }
            else if (situacion == "Vencida")
            {
                estilo.BackColor = Color.FromArgb(255, 235, 238);
                estilo.ForeColor = Color.FromArgb(183, 28, 28);
            }
            else if (situacion == "Hoy")
            {
                estilo.BackColor = Color.FromArgb(255, 243, 224);
                estilo.ForeColor = Color.FromArgb(230, 81, 0);
            }
            else if (situacion == "Proxima")
            {
                estilo.BackColor = Color.FromArgb(255, 253, 231);
                estilo.ForeColor = Color.FromArgb(121, 85, 0);
            }

            if (dgv.Columns[e.ColumnIndex].Name == "Prioridad")
            {
                if (prioridad == "Alta")
                {
                    estilo.Font = new Font(dgv.Font, FontStyle.Bold);
                    estilo.ForeColor = Color.FromArgb(183, 28, 28);
                }
                else if (prioridad == "Media")
                {
                    estilo.ForeColor = Color.DarkOrange;
                }
            }
        }

        private string LeerValorFila(DataGridViewRow fila, string columna)
        {
            if (fila.DataGridView == null || !fila.DataGridView.Columns.Contains(columna))
            {
                return string.Empty;
            }

            object? valor = fila.Cells[columna].Value;
            return valor == null || valor == DBNull.Value
                ? string.Empty
                : valor.ToString() ?? string.Empty;
        }

        private void RecargarListasAlertas()
        {
            CargarTodasLasAlertas();
            CargarAlertasPendientes();
            CargarAlertasProximas();
        }

        private void LimpiarFormulario()
        {
            txtIdAlerta.Text = "";
            cmbCultivo.SelectedIndex = -1;
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            dtpFechaProgramada.Value = DateTime.Now.AddDays(7);
            cmbTipoAlerta.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            cmbPrioridad.SelectedIndex = 1;
            numDiasAnticipacion.Value = 7;
            lblFechaCreacion.Text = DateTime.Now.ToString("dd/MM/yyyy");

            esNuevoRegistro = true;
            idAlertaSeleccionada = -1;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;

        }

        private bool ValidarDatos()
        {
            // Validar que el cultivo esté seleccionado
            if (cmbCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cultivo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCultivo.Focus();
                return false;
            }

            // Validar título
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Debe ingresar un título para la alerta",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            // Validar descripción
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción para la alerta",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            // Validar tipo de alerta
            if (cmbTipoAlerta.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de alerta",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoAlerta.Focus();
                return false;
            }

            // Validar estado
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado para la alerta",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return false;
            }

            // Validar prioridad
            if (cmbPrioridad.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una prioridad para la alerta",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPrioridad.Focus();
                return false;
            }

            // Validar fecha programada nueva. Al editar se permite conservar alertas vencidas
            // para poder completarlas o cancelarlas sin cambiar la fecha original.
            if (esNuevoRegistro && dtpFechaProgramada.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La fecha programada no puede ser anterior a hoy",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaProgramada.Focus();
                return false;
            }

            return true;
        }

        private Entitie_Alerta CrearObjetoAlerta()
        {
            Entitie_Alerta alerta = new Entitie_Alerta();

            // Si estamos editando, conservamos el ID
            if (!esNuevoRegistro && idAlertaSeleccionada > 0)
                alerta.ID_Alerta = idAlertaSeleccionada;

            // Datos del formulario
            alerta.ID_Cultivo = Convert.ToInt32(cmbCultivo.SelectedValue);
            alerta.Titulo = txtTitulo.Text.Trim();
            alerta.Descripcion = txtDescripcion.Text.Trim();
            alerta.Fecha_Programada = dtpFechaProgramada.Value.Date;
            alerta.Tipo_Alerta = cmbTipoAlerta.Text;
            alerta.Estado = cmbEstado.Text;
            alerta.Prioridad = cmbPrioridad.Text;
            alerta.Dias_Anticipacion = (int)numDiasAnticipacion.Value;

            // Si es nuevo registro, establecer fecha de creación
            if (esNuevoRegistro)
                alerta.Fecha_Creacion = DateTime.Now;

            return alerta;
        }

        private void CargarDatosAlerta(DataGridView dgv, int rowIndex)
        {
            try
            {
                // Extraer datos de la fila seleccionada
                idAlertaSeleccionada = Convert.ToInt32(dgv.Rows[rowIndex].Cells["ID_Alerta"].Value);

                // Cargar datos en controles
                txtIdAlerta.Text = idAlertaSeleccionada.ToString();

                int idCultivo = Convert.ToInt32(dgv.Rows[rowIndex].Cells["ID_Cultivo"].Value);
                // Seleccionar cultivo en el ComboBox
                for (int i = 0; i < cmbCultivo.Items.Count; i++)
                {
                    DataRowView? drv = cmbCultivo.Items[i] as DataRowView;
                    if (drv != null && Convert.ToInt32(drv["ID_Cultivo"]) == idCultivo)
                    {
                        cmbCultivo.SelectedIndex = i;
                        break;
                    }
                }

                txtTitulo.Text = Convert.ToString(
                    dgv.Rows[rowIndex].Cells["Titulo"].Value) ?? string.Empty;
                txtDescripcion.Text = Convert.ToString(
                    dgv.Rows[rowIndex].Cells["Descripcion"].Value) ?? string.Empty;

                DateTime fechaProgramada = Convert.ToDateTime(dgv.Rows[rowIndex].Cells["Fecha_Programada"].Value);
                dtpFechaProgramada.Value = fechaProgramada;

                // Fecha de creación (solo mostrar, no editable)
                if (dgv.Columns.Contains("Fecha_Creacion") && dgv.Rows[rowIndex].Cells["Fecha_Creacion"].Value != DBNull.Value)
                {
                    DateTime fechaCreacion = Convert.ToDateTime(dgv.Rows[rowIndex].Cells["Fecha_Creacion"].Value);
                    lblFechaCreacion.Text = fechaCreacion.ToString("dd/MM/yyyy");
                }

                // Seleccionar tipo de alerta
                string tipoAlerta = Convert.ToString(
                    dgv.Rows[rowIndex].Cells["Tipo_Alerta"].Value) ?? string.Empty;
                for (int i = 0; i < cmbTipoAlerta.Items.Count; i++)
                {
                    if (Convert.ToString(cmbTipoAlerta.Items[i]) == tipoAlerta)
                    {
                        cmbTipoAlerta.SelectedIndex = i;
                        break;
                    }
                }

                // Seleccionar estado
                string estado = Convert.ToString(
                    dgv.Rows[rowIndex].Cells["Estado"].Value) ?? string.Empty;
                for (int i = 0; i < cmbEstado.Items.Count; i++)
                {
                    if (Convert.ToString(cmbEstado.Items[i]) == estado)
                    {
                        cmbEstado.SelectedIndex = i;
                        break;
                    }
                }

                // Seleccionar prioridad
                string prioridad = Convert.ToString(
                    dgv.Rows[rowIndex].Cells["Prioridad"].Value) ?? string.Empty;
                for (int i = 0; i < cmbPrioridad.Items.Count; i++)
                {
                    if (Convert.ToString(cmbPrioridad.Items[i]) == prioridad)
                    {
                        cmbPrioridad.SelectedIndex = i;
                        break;
                    }
                }

                // Días de anticipación
                if (dgv.Columns.Contains("Dias_Anticipacion") && dgv.Rows[rowIndex].Cells["Dias_Anticipacion"].Value != DBNull.Value)
                {
                    int diasAnticipacion = Convert.ToInt32(dgv.Rows[rowIndex].Cells["Dias_Anticipacion"].Value);
                    numDiasAnticipacion.Value = diasAnticipacion;
                }

                // Cambiar estado de registro y habilitar/deshabilitar botones
                esNuevoRegistro = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = BLL_Seguridad.TienePermiso(PermisosSistema.AlertasEliminar);
                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de la alerta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarAlertas(bool mostrarMensaje = true)
        {
            try
            {
                // Obtener alertas próximas
                DataTable alertas = BLL_Alertas.VerificarAlertas();

                // Si hay alertas próximas, mostrar notificación
                if (alertas != null && alertas.Rows.Count > 0)
                {
                    if (mostrarMensaje)
                    {
                        // Preparar mensaje
                        StringBuilder mensaje = new StringBuilder();
                        mensaje.AppendLine("Se han detectado las siguientes alertas próximas:");
                        mensaje.AppendLine();
                        int correosEnviados = 0;
                        int correosPendientes = 0;

                        foreach (DataRow row in alertas.Rows)
                        {
                            string titulo = Convert.ToString(row["Titulo"]) ?? string.Empty;
                            string cultivo = Convert.ToString(row["Nombre_Cultivo"]) ?? string.Empty;
                            DateTime fecha = Convert.ToDateTime(row["Fecha_Programada"]);
                            string prioridad = Convert.ToString(row["Prioridad"]) ?? string.Empty;
                            string resultadoCorreo = row.Table.Columns.Contains("Resultado_Correo")
                                ? Convert.ToString(row["Resultado_Correo"]) ?? string.Empty
                                : string.Empty;
                            bool correoEnviado = row.Table.Columns.Contains("Correo_Enviado") &&
                                                  row["Correo_Enviado"] != DBNull.Value &&
                                                  Convert.ToBoolean(row["Correo_Enviado"]);

                            if (correoEnviado)
                            {
                                correosEnviados++;
                            }
                            else
                            {
                                correosPendientes++;
                            }

                            mensaje.AppendLine($"• {titulo}");
                            mensaje.AppendLine($"  Cultivo: {cultivo}");
                            mensaje.AppendLine($"  Fecha: {fecha.ToShortDateString()}");
                            mensaje.AppendLine($"  Prioridad: {prioridad}");
                            if (!string.IsNullOrWhiteSpace(resultadoCorreo))
                            {
                                mensaje.AppendLine($"  Gmail: {resultadoCorreo}");
                            }
                            mensaje.AppendLine();
                        }

                        mensaje.AppendLine($"Correos enviados: {correosEnviados}");
                        mensaje.AppendLine($"Pendientes sin enviar: {correosPendientes}");

                        // Mostrar notificación
                        MessageBox.Show(mensaje.ToString(),
                            "Alertas Próximas", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    // Actualizar la pestaña de alertas próximas
                    if (tabControlAlertas.SelectedTab == tabPageAlertasProximas)
                    {
                        CargarAlertasProximas();
                    }

                    CargarAlertasPendientes();

                    // Para una implementación más avanzada, podrías mostrar notificaciones del sistema
                    // usando NotifyIcon o bibliotecas de notificaciones de terceros
                }
                else if (mostrarMensaje)
                {
                    MessageBox.Show("No hay alertas próximas para mostrar",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show($"Error al verificar alertas: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GenerarAlertasAutomaticas()
        {
            // Este método generará alertas automáticas basadas en la información de cultivos

            try
            {
                // Verificar que hay cultivos disponibles
                if (cmbCultivo.Items.Count == 0)
                {
                    MessageBox.Show("No hay cultivos disponibles para generar alertas",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Si hay un cultivo seleccionado, preguntar si generar para ese cultivo o todos
                int idCultivo = -1;
                bool generarParaTodos = true;

                if (cmbCultivo.SelectedIndex != -1)
                {
                    DialogResult respuesta = MessageBox.Show(
                        $"¿Desea generar alertas automáticas para el cultivo seleccionado ({cmbCultivo.Text}) " +
                        $"o para todos los cultivos?\n\n" +
                        $"Sí: Generar solo para {cmbCultivo.Text}\n" +
                        $"No: Generar para todos los cultivos",
                        "Generar Alertas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        idCultivo = Convert.ToInt32(cmbCultivo.SelectedValue);
                        generarParaTodos = false;
                    }
                    else if (respuesta == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                // Mostrar indicador de progreso
                Cursor.Current = Cursors.WaitCursor;

                int alertasGeneradas = 0;
                int cultivosProcesados = 0;

                if (generarParaTodos)
                {
                    // Generar alertas para todos los cultivos
                    for (int i = 0; i < cmbCultivo.Items.Count; i++)
                    {
                        if (cmbCultivo.Items[i] is DataRowView drv)
                        {
                            int id = Convert.ToInt32(drv["ID_Cultivo"]);
                            try
                            {
                                alertasGeneradas += BLL_Alertas.GenerarAlertasAutomaticas(id);
                                cultivosProcesados++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error al generar alertas para cultivo ID {id}: {ex.Message}");
                            }
                        }
                    }
                }
                else
                {
                    // Generar alertas solo para el cultivo seleccionado
                    alertasGeneradas = BLL_Alertas.GenerarAlertasAutomaticas(idCultivo);
                    cultivosProcesados = 1;
                }

                // Restaurar cursor
                Cursor.Current = Cursors.Default;

                // Mostrar mensaje de éxito
                MessageBox.Show(
                    $"Cultivos procesados: {cultivosProcesados}\nAlertas nuevas creadas: {alertasGeneradas}",
                    "Generación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar lista de alertas
                RecargarListasAlertas();

                // Si se generaron alertas, preguntar si verificar inmediatamente
                if (alertasGeneradas > 0)
                {
                    DialogResult respuesta = MessageBox.Show(
                        "¿Desea verificar si hay alertas próximas ahora?",
                        "Verificar Alertas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        VerificarAlertas();
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error al generar alertas automáticas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}

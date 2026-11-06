using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmTrabajadores : Form
    {
        private bool esNuevo = true;
        private int idTrabajadorSeleccionado = -1;
        private readonly ErrorProvider errorProvider1;

        public FrmTrabajadores()
        {
            InitializeComponent();
            errorProvider1 = new ErrorProvider(this);
            ConfigurarComponentes();
            AplicarDisenoOrdenado();
        }
        private void ConfigurarComponentes()
        {
            this.errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            this.txtCedula.Leave += new EventHandler(this.txtCedula_Leave);
            this.txtTelefono.MaxLength = 11;
            this.dgvTrabajadores.CellClick += dgvTrabajadores_CellClick;
            ActualizarEstadoEdicion(false);
        }

        private void AplicarDisenoOrdenado()
        {
            ClientSize = new Size(1120, 720);
            MinimumSize = new Size(980, 640);

            panelHeader.Height = 54;
            labelTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            labelTitle.Location = new Point(16, 10);
            btnVolver.Size = new Size(34, 34);
            btnVolver.Location = new Point(panelHeader.Width - btnVolver.Width - 14, 10);

            cardDatosTrabajador.Height = 250;
            labelDatosTrabajador.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelDatosTrabajador.Location = new Point(14, 12);
            panelForm.Location = new Point(14, 42);
            panelForm.Height = 198;

            OrganizarCamposFormulario();
            ConfigurarPanelAcciones();
            ConfigurarListado();
            ReorganizarLayout();

            panelHeader.Resize += (_, _) =>
                btnVolver.Location = new Point(panelHeader.Width - btnVolver.Width - 14, 10);
            panelContent.Resize += (_, _) => ReorganizarLayout();
            cardTrabajadores.Resize += (_, _) => ConfigurarListado();
        }

        private void OrganizarCamposFormulario()
        {
            int w = 236;
            int x1 = 18, x2 = 286, x3 = 554, x4 = 822;
            int y1 = 4, y2 = 58, y3 = 112, y4 = 166;

            txtID.Visible = false;
            lblID.Visible = false;

            UbicarCampo(lblNombre, txtNombre, x1, y1, w, "Nombre");
            UbicarCampo(label2, txtApellido, x2, y1, w, "Apellido");
            UbicarCampo(label3, txtCedula, x3, y1, w, "Cédula");
            UbicarCampo(label4, cmbSexo, x4, y1, w, "Sexo");

            UbicarCampo(label5, txtEmail, x1, y2, w, "Correo");
            UbicarCampo(label8, txtTelefono, x2, y2, w, "Teléfono");
            UbicarCampo(label6, dtpFechaNac, x3, y2, w, "Nacimiento");
            UbicarCampo(label11, dtpFechaIngreso, x4, y2, w, "Ingreso");

            UbicarCampo(label13, cmbPosicion, x1, y3, w, "Rol laboral");
            UbicarCampo(lblSueldo, nudSueldo, x2, y3, w, "Sueldo");
            UbicarCampo(label9, txtCiudad, x3, y3, w, "Ciudad");
            UbicarCampo(label10, txtSector, x4, y3, w, "Sector");

            UbicarCampo(label7, txtDireccion, x1, y4, (x4 + w) - x1, "Dirección");
        }

        private static void UbicarCampo(
            Label etiqueta,
            Control control,
            int x,
            int y,
            int ancho,
            string texto)
        {
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.Font = new Font("Segoe UI Semibold", 8.75F, FontStyle.Bold);
            control.Location = new Point(x, y + 20);
            control.Width = ancho;
            if (control is ComboBox)
                control.Height = 28;
        }

        private void ConfigurarPanelAcciones()
        {
            panelAcciones.Height = 46;
            foreach (Button boton in new[] { btnGuardar, btnActualizar, btnEliminar, btnLimpiar })
            {
                boton.Size = new Size(122, 34);
                boton.Font = new Font("Segoe UI", 9F);
                boton.TextAlign = ContentAlignment.MiddleCenter;
            }

            panelAcciones.Resize += (_, _) => ReubicarAcciones();
            ReubicarAcciones();
        }

        private void ReubicarAcciones()
        {
            int gap = 10;
            int total = (btnGuardar.Width * 4) + (gap * 3);
            int x = Math.Max(0, panelAcciones.Width - total);
            int y = 6;

            foreach (Button boton in new[] { btnGuardar, btnActualizar, btnEliminar, btnLimpiar })
            {
                boton.Location = new Point(x, y);
                x += boton.Width + gap;
            }
        }

        private void ConfigurarListado()
        {
            labelListaTrabajadores.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelListaTrabajadores.Location = new Point(14, 10);

            panelBusqueda.Location = new Point(14, 38);
            panelBusqueda.Width = Math.Max(600, cardTrabajadores.Width - 28);
            panelBusqueda.Height = 38;

            labelBuscar.Location = new Point(0, 7);
            txtBuscar.Location = new Point(74, 4);
            txtBuscar.Size = new Size(360, 28);
            btnBuscar.Location = new Point(444, 3);
            btnBuscar.Size = new Size(92, 30);

            panelListado.Location = new Point(14, 82);
            panelListado.Size = new Size(
                Math.Max(600, cardTrabajadores.Width - 28),
                Math.Max(160, cardTrabajadores.Height - 96));
            dgvTrabajadores.RowHeadersVisible = false;
        }

        private void ReorganizarLayout()
        {
            int margen = 14;
            int ancho = Math.Max(900, panelContent.Width - (margen * 2));

            cardDatosTrabajador.Location = new Point(margen, margen);
            cardDatosTrabajador.Size = new Size(ancho, 250);
            panelForm.Width = Math.Max(900, cardDatosTrabajador.Width - 28);

            panelAcciones.Location = new Point(margen, cardDatosTrabajador.Bottom + 8);
            panelAcciones.Size = new Size(ancho, 46);

            cardTrabajadores.Location = new Point(margen, panelAcciones.Bottom + 8);
            cardTrabajadores.Size = new Size(
                ancho,
                Math.Max(220, panelContent.Height - cardTrabajadores.Top - margen));

            ReubicarAcciones();
            ConfigurarListado();
        }

        private void FrmTrabajadores_Load(object sender, EventArgs e)
        {
            // Cargar la lista de trabajadores al iniciar el formulario
            CargarListaTrabajadores();

            // Inicializar los ComboBox
            if (cmbSexo.Items.Count > 0)
                cmbSexo.SelectedIndex = 0;

            if (cmbPosicion.Items.Count > 0)
                cmbPosicion.SelectedIndex = 0;

            // Establecer fecha actual para los DateTimePicker
            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaIngreso.MaxDate = DateTime.Today;

            // Por defecto, la fecha de ingreso será la fecha actual
            dtpFechaIngreso.Value = DateTime.Today;
        }

        private void CargarListaTrabajadores()
        {
            try
            {
                // Obtener la lista de trabajadores desde la capa BLL
                DataTable dt = BLL_Trabajador.Listar_Trabajadores();

                // Asignar la lista al DataGridView
                dgvTrabajadores.DataSource = dt;

                // Configurar la apariencia del DataGridView
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de trabajadores: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            // Ajustar las columnas del DataGridView para una mejor visualización
            if (dgvTrabajadores.Columns.Count > 0)
            {
                // Ocultar la columna ID (pero mantenerla accesible para su selección)
                if (dgvTrabajadores.Columns.Contains("ID"))
                    dgvTrabajadores.Columns["ID"].Visible = false;

                // Ajustar anchos de columnas específicas
                if (dgvTrabajadores.Columns.Contains("Nombre"))
                    dgvTrabajadores.Columns["Nombre"].Width = 120;

                if (dgvTrabajadores.Columns.Contains("Apellido"))
                    dgvTrabajadores.Columns["Apellido"].Width = 120;

                if (dgvTrabajadores.Columns.Contains("Cedula"))
                    dgvTrabajadores.Columns["Cedula"].Width = 100;

                if (dgvTrabajadores.Columns.Contains("Telefono"))
                    dgvTrabajadores.Columns["Telefono"].Width = 100;

                if (dgvTrabajadores.Columns.Contains("Sueldo"))
                {
                    dgvTrabajadores.Columns["Sueldo"].DefaultCellStyle.Format = "C2";
                    dgvTrabajadores.Columns["Sueldo"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }

                // Formato para fechas
                if (dgvTrabajadores.Columns.Contains("Fecha de Nacimiento"))
                    dgvTrabajadores.Columns["Fecha de Nacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dgvTrabajadores.Columns.Contains("Fecha de Ingreso"))
                    dgvTrabajadores.Columns["Fecha de Ingreso"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void LimpiarFormulario()
        {
            // Limpiar los controles del formulario
            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCedula.Text = "";
            if (cmbSexo.Items.Count > 0)
                cmbSexo.SelectedIndex = 0;
            if (cmbPosicion.Items.Count > 0)
                cmbPosicion.SelectedIndex = 0;
            txtEmail.Text = "";
            dtpFechaNac.Value = DateTime.Today.AddYears(-20); // Valor predeterminado: 20 años atrás
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCiudad.Text = "";
            txtSector.Text = "";
            nudSueldo.Value = 0;
            dtpFechaIngreso.Value = DateTime.Today;

            // Establecer el modo a nuevo registro
            esNuevo = true;
            idTrabajadorSeleccionado = -1;
            errorProvider1.Clear();
            ActualizarEstadoEdicion(false);

            // Enfocar el primer campo
            txtNombre.Focus();
        }
        // Modificamos el método existente txtCedula_KeyPress para limitar a 11 dígitos y sólo números
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control (como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limitar a 11 caracteres
            if (txtCedula.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Añadimos un nuevo método para validar la cédula cuando el campo pierde el foco
        private void txtCedula_Leave(object? sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim();

            // Solo validar si el campo tiene contenido
            if (!string.IsNullOrEmpty(cedula))
            {
                // Verificar que tenga exactamente 11 dígitos
                if (cedula.Length != 11)
                {
                    errorProvider1.SetError(txtCedula, "La cédula debe tener 11 dígitos");
                    return;
                }

                // Verificar que solo contenga números
                foreach (char c in cedula)
                {
                    if (!char.IsDigit(c))
                    {
                        errorProvider1.SetError(txtCedula, "La cédula solo debe contener números");
                        return;
                    }
                }

                // Validar la cédula usando el algoritmo
                if (!Utilidades.ValidacionCedula.EsCedulaValida(cedula))
                {
                    errorProvider1.SetError(txtCedula, "El número de cédula no es válido");
                }
                else
                {
                    // Limpiar el error si la cédula es válida
                    errorProvider1.SetError(txtCedula, "");
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtCedula.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) || cmbPosicion.SelectedItem == null ||
                    nudSueldo.Value <= 0)
                {
                    MessageBox.Show("Complete nombre, apellido, cédula, correo, teléfono, rol laboral y sueldo.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato de cédula - VALIDACIÓN ACTUALIZADA
                string cedula = txtCedula.Text.Trim();
                if (!Utilidades.ValidacionCedula.EsCedulaValida(cedula))
                {
                    MessageBox.Show("El número de cédula ingresado no es válido. Por favor verifique.",
                        "Cédula inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCedula.Focus();
                    return;
                }

                // Validar formato de email (validación simple)
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MessageBox.Show("El formato del email no es válido. Debe contener '@' y '.'",
                        "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Validar fecha de nacimiento
                int edad = DateTime.Today.Year - dtpFechaNac.Value.Year;
                if (dtpFechaNac.Value.Date > DateTime.Today.AddYears(-edad))
                    edad--;

                if (edad < 18)
                {
                    MessageBox.Show("El trabajador debe ser mayor de edad (18 años o más).",
                        "Edad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFechaNac.Focus();
                    return;
                }

                // Validar fecha de ingreso
                if (dtpFechaIngreso.Value > DateTime.Today)
                {
                    MessageBox.Show("La fecha de ingreso no puede ser futura.",
                        "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFechaIngreso.Focus();
                    return;
                }

                string resultado;

                if (esNuevo)
                {
                    // Insertar nuevo trabajador
                    resultado = BLL_Trabajador.Insertar_Trabajador(
                        txtNombre.Text,
                        txtApellido.Text,
                        txtCedula.Text,
                        cmbSexo.SelectedItem?.ToString() ?? string.Empty,
                        txtEmail.Text,
                        dtpFechaNac.Value,
                        txtDireccion.Text,
                        txtTelefono.Text,
                        txtCiudad.Text,
                        txtSector.Text,
                        cmbPosicion.SelectedItem?.ToString() ?? string.Empty,
                        nudSueldo.Value,
                        dtpFechaIngreso.Value);
                }
                else
                {
                    // Actualizar trabajador existente
                    resultado = BLL_Trabajador.Actualizar_Trabajador(
                        idTrabajadorSeleccionado,
                        txtNombre.Text,
                        txtApellido.Text,
                        txtCedula.Text,
                        cmbSexo.SelectedItem?.ToString() ?? string.Empty,
                        txtEmail.Text,
                        dtpFechaNac.Value,
                        txtDireccion.Text,
                        txtTelefono.Text,
                        txtCiudad.Text,
                        txtSector.Text,
                        cmbPosicion.SelectedItem?.ToString() ?? string.Empty,
                        nudSueldo.Value,
                        dtpFechaIngreso.Value);
                }

                // Mostrar el resultado de la operación
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                    OperacionExitosa(resultado) ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Si la operación fue exitosa, actualizar la lista y limpiar el formulario
                if (OperacionExitosa(resultado))
                {
                    CargarListaTrabajadores();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el trabajador: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay un trabajador seleccionado
                if (idTrabajadorSeleccionado <= 0)
                {
                    MessageBox.Show("Debe seleccionar un trabajador para actualizar.",
                        "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtCedula.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) || cmbPosicion.SelectedItem == null ||
                    nudSueldo.Value <= 0)
                {
                    MessageBox.Show("Complete nombre, apellido, cédula, correo, teléfono, rol laboral y sueldo.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato de cédula - VALIDACIÓN ACTUALIZADA
                string cedula = txtCedula.Text.Trim();
                if (!Utilidades.ValidacionCedula.EsCedulaValida(cedula))
                {
                    MessageBox.Show("El número de cédula ingresado no es válido. Por favor verifique.",
                        "Cédula inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCedula.Focus();
                    return;
                }

                // Validar formato de email (validación simple)
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MessageBox.Show("El formato del email no es válido. Debe contener '@' y '.'",
                        "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Validar fecha de nacimiento
                int edad = DateTime.Today.Year - dtpFechaNac.Value.Year;
                if (dtpFechaNac.Value.Date > DateTime.Today.AddYears(-edad))
                    edad--;

                if (edad < 18)
                {
                    MessageBox.Show("El trabajador debe ser mayor de edad (18 años o más).",
                        "Edad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFechaNac.Focus();
                    return;
                }

                // Validar fecha de ingreso
                if (dtpFechaIngreso.Value > DateTime.Today)
                {
                    MessageBox.Show("La fecha de ingreso no puede ser futura.",
                        "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFechaIngreso.Focus();
                    return;
                }

                // Actualizar trabajador existente
                string resultado = BLL_Trabajador.Actualizar_Trabajador(
                    idTrabajadorSeleccionado,
                    txtNombre.Text,
                    txtApellido.Text,
                    txtCedula.Text,
                    cmbSexo.SelectedItem?.ToString() ?? string.Empty,
                    txtEmail.Text,
                    dtpFechaNac.Value,
                    txtDireccion.Text,
                    txtTelefono.Text,
                    txtCiudad.Text,
                    txtSector.Text,
                    cmbPosicion.SelectedItem?.ToString() ?? string.Empty,
                    nudSueldo.Value,
                    dtpFechaIngreso.Value);

                // Mostrar el resultado de la operación
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                    OperacionExitosa(resultado) ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Si la operación fue exitosa, actualizar la lista y limpiar el formulario
                if (OperacionExitosa(resultado))
                {
                    CargarListaTrabajadores();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el trabajador: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTrabajadorSeleccionado <= 0)
                {
                    DataGridViewRow? fila = ObtenerFilaSeleccionada();
                    if (fila != null)
                        SeleccionarTrabajadorDesdeFila(fila, false);
                }

                // Verificar si hay un trabajador seleccionado
                if (idTrabajadorSeleccionado <= 0)
                {
                    MessageBox.Show("Debe seleccionar un trabajador para eliminar.",
                        "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmar la eliminación
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar al trabajador " +
                    ObtenerNombreTrabajadorSeleccionado() + "?\n\n" +
                    "Si tiene pagos o historial, quedará inactivo para conservar los reportes.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Eliminar trabajador
                    string resultado = BLL_Trabajador.Eliminar_Trabajador(idTrabajadorSeleccionado);

                    // Mostrar el resultado de la operación
                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                        OperacionExitosa(resultado) ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                    // Si la operación fue exitosa, actualizar la lista y limpiar el formulario
                    if (OperacionExitosa(resultado))
                    {
                        CargarListaTrabajadores();
                        LimpiarFormulario();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el trabajador: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvTrabajadores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que se haya hecho doble clic en una fila válida
                if (e.RowIndex < 0)
                    return;

                SeleccionarTrabajadorDesdeFila(dgvTrabajadores.Rows[e.RowIndex], true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del trabajador: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTrabajadores_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SeleccionarTrabajadorDesdeFila(dgvTrabajadores.Rows[e.RowIndex], true);
        }

        private void SeleccionarTrabajadorDesdeFila(DataGridViewRow fila, bool cargarFormulario)
        {
            idTrabajadorSeleccionado = Convert.ToInt32(fila.Cells["ID"].Value);
            ActualizarEstadoEdicion(true);

            if (!cargarFormulario) return;

            Entitie_Trabajador trabajador = BLL_Trabajador.Obtener_Trabajador(idTrabajadorSeleccionado);
            CargarTrabajadorEnFormulario(trabajador);
        }

        private void CargarTrabajadorEnFormulario(Entitie_Trabajador trabajador)
        {
            txtID.Text = trabajador.ID_Trabajador.ToString();
            txtNombre.Text = trabajador.Nombre;
            txtApellido.Text = trabajador.Apellido;
            txtCedula.Text = trabajador.Cedula;
            cmbSexo.SelectedItem = trabajador.Sexo;
            txtEmail.Text = trabajador.Email;
            dtpFechaNac.Value = trabajador.Fecha_Nac;
            txtDireccion.Text = trabajador.Direccion;
            txtTelefono.Text = trabajador.Telefono;
            txtCiudad.Text = trabajador.Ciudad;
            txtSector.Text = trabajador.Sector;

            if (!string.IsNullOrEmpty(trabajador.Posicion))
            {
                int index = cmbPosicion.Items.IndexOf(trabajador.Posicion);
                if (index >= 0)
                    cmbPosicion.SelectedIndex = index;
                else
                {
                    cmbPosicion.Items.Add(trabajador.Posicion);
                    cmbPosicion.SelectedItem = trabajador.Posicion;
                }
            }
            else if (cmbPosicion.Items.Count > 0)
            {
                cmbPosicion.SelectedIndex = 0;
            }

            nudSueldo.Value = trabajador.Sueldo >= nudSueldo.Minimum &&
                              trabajador.Sueldo <= nudSueldo.Maximum
                ? trabajador.Sueldo
                : 0;
            dtpFechaIngreso.Value = trabajador.Fecha_Ingreso;

            esNuevo = false;
            ActualizarEstadoEdicion(true);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Si el texto de búsqueda está vacío, mostrar todos los trabajadores
                if (string.IsNullOrEmpty(txtBuscar.Text))
                {
                    CargarListaTrabajadores();
                    return;
                }

                // Realizar la búsqueda
                DataTable dt = BLL_Trabajador.Buscar_Trabajador(txtBuscar.Text);

                // Asignar los resultados al DataGridView
                dgvTrabajadores.DataSource = dt;

                // Configurar la apariencia del DataGridView
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar trabajadores: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario
            this.Close();
        }

        // Validación de entrada para campos de texto

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir letras, espacios y teclas de control (como Backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir letras, espacios y teclas de control (como Backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control (como Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ActualizarEstadoEdicion(bool haySeleccion)
        {
            btnGuardar.Enabled = !haySeleccion;
            btnActualizar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;
        }

        private DataGridViewRow? ObtenerFilaSeleccionada()
        {
            if (dgvTrabajadores.CurrentRow != null && !dgvTrabajadores.CurrentRow.IsNewRow)
                return dgvTrabajadores.CurrentRow;

            if (dgvTrabajadores.SelectedRows.Count > 0 && !dgvTrabajadores.SelectedRows[0].IsNewRow)
                return dgvTrabajadores.SelectedRows[0];

            return null;
        }

        private string ObtenerNombreTrabajadorSeleccionado()
        {
            DataGridViewRow? fila = ObtenerFilaSeleccionada();
            if (fila != null)
            {
                string nombre = ObtenerValorCelda(fila, "Nombre");
                string apellido = ObtenerValorCelda(fila, "Apellido");
                string nombreCompleto = $"{nombre} {apellido}".Trim();
                if (!string.IsNullOrWhiteSpace(nombreCompleto))
                    return nombreCompleto;
            }

            return $"{txtNombre.Text} {txtApellido.Text}".Trim();
        }

        private static string ObtenerValorCelda(DataGridViewRow fila, string columna)
        {
            if (fila.DataGridView == null || !fila.DataGridView.Columns.Contains(columna))
                return string.Empty;

            object valor = fila.Cells[columna].Value;
            return valor == DBNull.Value ? string.Empty : Convert.ToString(valor) ?? string.Empty;
        }

        private static bool OperacionExitosa(string resultado)
        {
            return resultado.Contains("exitosamente", StringComparison.OrdinalIgnoreCase) ||
                   resultado.Contains("correctamente", StringComparison.OrdinalIgnoreCase);
        }
    }
}


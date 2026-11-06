using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmGestionDeMaquinaria : Form
    {
        private readonly int pestañaInicial;
        private int idMaquina;
        private int idAsignacion;
        private DataTable maquinas = new DataTable();

        private readonly TabControl tabsOperacion = new TabControl();
        private readonly TabControl tabsDatos = new TabControl();
        private readonly DataGridView dgvMaquinas = new DataGridView();
        private readonly DataGridView dgvAsignaciones = new DataGridView();
        private readonly DataGridView dgvMantenimientos = new DataGridView();
        private readonly TextBox txtBuscar = new TextBox();
        private readonly ComboBox cmbFiltroTipo = new ComboBox();
        private readonly ComboBox cmbFiltroEstado = new ComboBox();
        private readonly Label lblEstado = new Label();
        private readonly Label lblTotal = new Label();
        private readonly Label lblDisponibles = new Label();
        private readonly Label lblAsignadas = new Label();
        private readonly Label lblMantenimiento = new Label();
        private readonly Label lblValor = new Label();

        private readonly TextBox txtCodigo = new TextBox();
        private readonly TextBox txtNombre = new TextBox();
        private readonly ComboBox cmbTipo = new ComboBox();
        private readonly TextBox txtMarca = new TextBox();
        private readonly TextBox txtModelo = new TextBox();
        private readonly TextBox txtSerie = new TextBox();
        private readonly NumericUpDown nudCosto = new NumericUpDown();
        private readonly DateTimePicker dtpAdquisicion = new DateTimePicker();
        private readonly ComboBox cmbFrecuencia = new ComboBox();
        private readonly DateTimePicker dtpProximo = new DateTimePicker();
        private readonly NumericUpDown nudHorometro = new NumericUpDown();
        private readonly TextBox txtUbicacion = new TextBox();
        private readonly TextBox txtObservaciones = new TextBox();
        private readonly Button btnGuardar = new Button();
        private readonly Button btnNuevo = new Button();
        private readonly Button btnEstadoMaquina = new Button();
        private readonly Button btnEditarMaquina = new Button();
        private readonly Button btnEliminarMaquina = new Button();
        private readonly Button btnEliminarSeleccionMaquina = new Button();

        private readonly ComboBox cmbTrabajador = new ComboBox();
        private readonly ComboBox cmbMaquinaAsignacion = new ComboBox();
        private readonly DateTimePicker dtpAsignacion = new DateTimePicker();
        private readonly DateTimePicker dtpDevolucion = new DateTimePicker();
        private readonly TextBox txtActividad = new TextBox();
        private readonly TextBox txtObservacionAsignacion = new TextBox();
        private readonly Button btnAsignar = new Button();
        private readonly Button btnDevolver = new Button();
        private readonly Button btnQuitarAsignaciones = new Button();

        private readonly ComboBox cmbMaquinaMantenimiento = new ComboBox();
        private readonly ComboBox cmbTipoMantenimiento = new ComboBox();
        private readonly DateTimePicker dtpMantenimiento = new DateTimePicker();
        private readonly NumericUpDown nudCostoMantenimiento = new NumericUpDown();
        private readonly NumericUpDown nudHorometroMantenimiento = new NumericUpDown();
        private readonly DateTimePicker dtpSiguienteMantenimiento = new DateTimePicker();
        private readonly TextBox txtProveedor = new TextBox();
        private readonly TextBox txtDescripcionMantenimiento = new TextBox();
        private readonly Button btnRegistrarMantenimiento = new Button();

        public FrmGestionDeMaquinaria(int pestañaInicial = 0)
        {
            this.pestañaInicial = Math.Max(0, Math.Min(pestañaInicial, 2));
            ConstruirInterfaz();
            Load += FrmGestionDeMaquinaria_Load;
        }

        private void ConstruirInterfaz()
        {
            Text = "Gestión de maquinaria";
            FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1100, 680);
            MinimumSize = new Size(900, 600);
            Font = new Font("Segoe UI", 8.5F);

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                Size = new Size(1100, 52),
                BackColor = Color.FromArgb(245, 247, 250)
            };
            header.Controls.Add(new Label
            {
                Text = "Gestión de maquinaria",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(16, 10)
            });
            Button cerrar = CrearBoton("X", Color.FromArgb(192, 57, 43), 34);
            cerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cerrar.Location = new Point(1052, 10);
            cerrar.Click += (_, _) => Close();
            header.Resize += (_, _) =>
                cerrar.Location = new Point(header.Width - cerrar.Width - 14, 10);
            header.Controls.Add(cerrar);

            Panel contenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(238, 242, 245)
            };

            Panel filtros = new Panel { Dock = DockStyle.Top, Height = 54, BackColor = Color.White };
            ConfigurarTexto(txtBuscar, "Código, nombre, marca, modelo o serie");
            txtBuscar.Location = new Point(12, 23);
            txtBuscar.Size = new Size(330, 25);
            cmbFiltroTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroTipo.Location = new Point(360, 23);
            cmbFiltroTipo.Size = new Size(185, 25);
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Location = new Point(562, 23);
            cmbFiltroEstado.Size = new Size(165, 25);
            Button aplicar = CrearBoton("Aplicar", Color.FromArgb(46, 125, 50), 92);
            aplicar.Location = new Point(745, 19);
            aplicar.Click += (_, _) => CargarTodo();
            filtros.Controls.AddRange(new Control[]
            {
                CrearEtiqueta("Buscar",12,4),txtBuscar,CrearEtiqueta("Tipo",360,4),cmbFiltroTipo,
                CrearEtiqueta("Estado",562,4),cmbFiltroEstado,aplicar
            });

            TableLayoutPanel resumen = new TableLayoutPanel { Dock = DockStyle.Top, Height = 70, ColumnCount = 5 };
            for (int i = 0; i < 5; i++) resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            AgregarResumen(resumen, 0, "Activos", lblTotal, Color.FromArgb(52, 73, 94));
            AgregarResumen(resumen, 1, "Disponibles", lblDisponibles, Color.FromArgb(35, 115, 70));
            AgregarResumen(resumen, 2, "Asignadas", lblAsignadas, Color.FromArgb(41, 128, 185));
            AgregarResumen(resumen, 3, "Mant. 30 días", lblMantenimiento, Color.FromArgb(192, 57, 43));
            AgregarResumen(resumen, 4, "Valor de activos", lblValor, Color.FromArgb(110, 80, 145));

            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                FixedPanel = FixedPanel.Panel1,
                Size = new Size(1080, 520),
                SplitterDistance = 330,
                SplitterWidth = 5,
                Panel1MinSize = 300,
                Panel2MinSize = 420
            };
            split.Panel1.Padding = new Padding(0, 6, 6, 0);
            split.Panel2.Padding = new Padding(6, 6, 0, 0);

            tabsOperacion.Dock = DockStyle.Fill;
            tabsOperacion.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            tabsOperacion.TabPages.Add(CrearTabMaquina());
            tabsOperacion.TabPages.Add(CrearTabAsignacion());
            tabsOperacion.TabPages.Add(CrearTabMantenimiento());
            split.Panel1.Controls.Add(tabsOperacion);

            Panel listado = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            tabsDatos.Dock = DockStyle.Fill;
            tabsDatos.TabPages.Add(CrearTabDatosMaquinaria());
            tabsDatos.TabPages.Add(CrearTabDatosAsignaciones());
            tabsDatos.TabPages.Add(CrearTabDatos("Mantenimientos", dgvMantenimientos));
            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.Height = 30;
            lblEstado.Padding = new Padding(8, 5, 0, 0);
            listado.Controls.Add(tabsDatos);
            listado.Controls.Add(lblEstado);
            split.Panel2.Controls.Add(listado);

            contenido.Controls.Add(split);
            contenido.Controls.Add(resumen);
            contenido.Controls.Add(filtros);
            Controls.Add(contenido);
            Controls.Add(header);
        }

        private TabPage CrearTabMaquina()
        {
            TabPage tab = new TabPage("Activo") { BackColor = Color.White, AutoScroll = true };
            TableLayoutPanel t = CrearFormulario();
            AgregarCampo(t, "Código", txtCodigo);
            AgregarCampo(t, "Nombre", txtNombre);
            AgregarCampo(t, "Tipo", cmbTipo);
            AgregarCampo(t, "Marca", txtMarca);
            AgregarCampo(t, "Modelo", txtModelo);
            AgregarCampo(t, "Número de serie", txtSerie);
            AgregarCampo(t, "Costo de adquisición RD$", nudCosto);
            AgregarCampo(t, "Fecha de adquisición", dtpAdquisicion);
            AgregarCampo(t, "Frecuencia de mantenimiento", cmbFrecuencia);
            AgregarCampo(t, "Próximo mantenimiento", dtpProximo);
            AgregarCampo(t, "Horómetro", nudHorometro);
            AgregarCampo(t, "Ubicación", txtUbicacion);
            AgregarCampo(t, "Observaciones", txtObservaciones);
            FlowLayoutPanel acciones = new FlowLayoutPanel { Dock = DockStyle.Fill };
            ConfigurarBoton(btnGuardar, "Guardar", Color.FromArgb(46, 125, 50), 88);
            ConfigurarBoton(btnNuevo, "Nuevo", Color.FromArgb(110, 120, 130), 78);
            ConfigurarBoton(btnEstadoMaquina, "Desactivar", Color.FromArgb(192, 57, 43), 92);
            ConfigurarBoton(btnEliminarMaquina, "Eliminar", Color.FromArgb(146, 43, 33), 86);
            btnGuardar.Click += btnGuardar_Click;
            btnNuevo.Click += (_, _) => LimpiarMaquina();
            btnEstadoMaquina.Click += btnEstadoMaquina_Click;
            btnEliminarMaquina.Click += btnEliminarMaquina_Click;
            btnEstadoMaquina.Enabled = false;
            btnEliminarMaquina.Enabled = false;
            acciones.Controls.AddRange(new Control[]
            {
                btnGuardar, btnNuevo, btnEstadoMaquina, btnEliminarMaquina
            });
            t.Controls.Add(acciones);
            tab.Controls.Add(t);
            return tab;
        }

        private TabPage CrearTabAsignacion()
        {
            TabPage tab = new TabPage("Asignación") { BackColor = Color.White };
            TableLayoutPanel t = CrearFormulario();
            AgregarCampo(t, "Trabajador responsable", cmbTrabajador);
            AgregarCampo(t, "Maquinaria disponible", cmbMaquinaAsignacion);
            AgregarCampo(t, "Fecha de asignación", dtpAsignacion);
            AgregarCampo(t, "Devolución prevista", dtpDevolucion);
            AgregarCampo(t, "Actividad", txtActividad);
            AgregarCampo(t, "Observaciones", txtObservacionAsignacion);
            FlowLayoutPanel acciones = new FlowLayoutPanel { Dock = DockStyle.Fill };
            ConfigurarBoton(btnAsignar, "Asignar", Color.FromArgb(46, 125, 50), 86);
            ConfigurarBoton(btnDevolver, "Devolver", Color.FromArgb(41, 128, 185), 88);
            btnAsignar.Click += btnAsignar_Click;
            btnDevolver.Click += btnDevolver_Click;
            btnDevolver.Enabled = false;
            acciones.Controls.AddRange(new Control[] { btnAsignar, btnDevolver });
            t.Controls.Add(acciones);
            tab.Controls.Add(t);
            return tab;
        }

        private TabPage CrearTabMantenimiento()
        {
            TabPage tab = new TabPage("Mantenimiento") { BackColor = Color.White };
            TableLayoutPanel t = CrearFormulario();
            AgregarCampo(t, "Maquinaria", cmbMaquinaMantenimiento);
            AgregarCampo(t, "Tipo", cmbTipoMantenimiento);
            AgregarCampo(t, "Fecha", dtpMantenimiento);
            AgregarCampo(t, "Costo RD$", nudCostoMantenimiento);
            AgregarCampo(t, "Horómetro", nudHorometroMantenimiento);
            AgregarCampo(t, "Próximo mantenimiento", dtpSiguienteMantenimiento);
            AgregarCampo(t, "Proveedor / taller", txtProveedor);
            AgregarCampo(t, "Trabajo realizado", txtDescripcionMantenimiento);
            ConfigurarBoton(btnRegistrarMantenimiento, "Registrar mantenimiento",
                Color.FromArgb(46, 125, 50), 170);
            btnRegistrarMantenimiento.Click += btnRegistrarMantenimiento_Click;
            t.Controls.Add(btnRegistrarMantenimiento);
            tab.Controls.Add(t);
            return tab;
        }

        private void FrmGestionDeMaquinaria_Load(object? sender, EventArgs e)
        {
            ConfigurarCombos();
            foreach (DataGridView grid in new[] { dgvMaquinas, dgvAsignaciones, dgvMantenimientos })
                ConfigurarGrid(grid);
            dgvMaquinas.CellDoubleClick += dgvMaquinas_CellDoubleClick;
            dgvAsignaciones.CellDoubleClick += dgvAsignaciones_CellDoubleClick;
            tabsOperacion.SelectedIndex = pestañaInicial;
            CargarTodo();
        }

        private void ConfigurarCombos()
        {
            object[] tipos = { "Tractor", "Cosechadora", "Sembradora", "Pulverizadora",
                "Cargador frontal", "Equipo de labranza", "Equipo de riego", "Otro" };
            cmbTipo.Items.AddRange(tipos); cmbTipo.SelectedIndex = 0;
            cmbFiltroTipo.Items.Add("Todos"); cmbFiltroTipo.Items.AddRange(tipos); cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroEstado.Items.AddRange(new object[] { "Todos", "Disponible", "Asignada", "En mantenimiento", "Inactiva" });
            cmbFiltroEstado.SelectedIndex = 0;
            cmbFrecuencia.Items.AddRange(new object[] { "Mensual", "Trimestral", "Semestral", "Anual", "Por horómetro" });
            cmbFrecuencia.SelectedIndex = 1;
            cmbTipoMantenimiento.Items.AddRange(new object[] { "Preventivo", "Correctivo", "Inspección" });
            cmbTipoMantenimiento.SelectedIndex = 0;
            foreach (NumericUpDown n in new[] { nudCosto, nudHorometro, nudCostoMantenimiento, nudHorometroMantenimiento })
            { n.Maximum = 999999999; n.DecimalPlaces = n == nudHorometro || n == nudHorometroMantenimiento ? 1 : 2; }
            dtpAdquisicion.MaxDate = DateTime.Today; dtpAsignacion.MaxDate = DateTime.Today;
            dtpMantenimiento.MaxDate = DateTime.Today;
            dtpProximo.Value = DateTime.Today.AddMonths(3);
            dtpDevolucion.Value = DateTime.Today.AddDays(1);
            dtpSiguienteMantenimiento.Value = DateTime.Today.AddMonths(3);
        }

        private void CargarTodo()
        {
            try
            {
                maquinas = BLL_GestionMaquinaria.ListarMaquinaria(txtBuscar.Text,
                    cmbFiltroTipo.Text, cmbFiltroEstado.Text);
                dgvMaquinas.DataSource = maquinas;
                DataTable todas = BLL_GestionMaquinaria.ListarMaquinaria("", "Todos", "Todos");
                ActualizarResumen(BLL_GestionMaquinaria.CalcularResumen(todas));
                CargarCombosDatos(todas);
                dgvAsignaciones.DataSource = BLL_GestionMaquinaria.ListarAsignaciones(false);
                dgvMantenimientos.DataSource = BLL_GestionMaquinaria.ListarMantenimientos(
                    null, DateTime.Today.AddYears(-2), DateTime.Today);
                FormatearColumnas();
                lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
                lblEstado.Text = $"{maquinas.Rows.Count} maquinarias encontradas";
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void CargarCombosDatos(DataTable todas)
        {
            DataTable disponibles = todas.Clone();
            foreach (DataRow r in todas.Rows)
                if (Convert.ToString(r["Estado"]) == "Disponible") disponibles.ImportRow(r);
            AgregarDisplay(disponibles, "Maquina", "Código", "Nombre");
            cmbMaquinaAsignacion.DataSource = disponibles; cmbMaquinaAsignacion.DisplayMember = "Maquina"; cmbMaquinaAsignacion.ValueMember = "ID";
            DataTable mant = todas.Copy(); AgregarDisplay(mant, "Maquina", "Código", "Nombre");
            cmbMaquinaMantenimiento.DataSource = mant; cmbMaquinaMantenimiento.DisplayMember = "Maquina"; cmbMaquinaMantenimiento.ValueMember = "ID";
            DataTable trabajadores = BLL_GestionMaquinaria.ListarTrabajadores();
            cmbTrabajador.DataSource = trabajadores; cmbTrabajador.DisplayMember = "Trabajador"; cmbTrabajador.ValueMember = "ID";
        }

        private static void AgregarDisplay(DataTable dt, string nueva, string c1, string c2)
        {
            if (!dt.Columns.Contains(nueva)) dt.Columns.Add(nueva, typeof(string));
            foreach (DataRow r in dt.Rows) r[nueva] = $"{r[c1]} - {r[c2]}";
        }

        private void ActualizarResumen(Entitie_ResumenMaquinaria r)
        {
            lblTotal.Text = r.TotalActivos.ToString();
            lblDisponibles.Text = r.Disponibles.ToString();
            lblAsignadas.Text = r.Asignadas.ToString();
            lblMantenimiento.Text = r.MantenimientoProximo.ToString();
            lblValor.Text = $"RD$ {r.ValorActivos:N2}";
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                bool actualizando = idMaquina > 0;
                Entitie_Maquinaria m = CrearMaquina();
                if (idMaquina > 0) { m.Id_Maquina = idMaquina; BLL_GestionMaquinaria.ActualizarMaquinaria(m); }
                else BLL_GestionMaquinaria.RegistrarMaquinaria(m);
                LimpiarMaquina(); CargarTodo();
                lblEstado.Text = actualizando
                    ? "Maquinaria actualizada correctamente."
                    : "Maquinaria registrada correctamente.";
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private Entitie_Maquinaria CrearMaquina() => new Entitie_Maquinaria
        {
            Codigo=txtCodigo.Text,Nombre_Maquina=txtNombre.Text,Tipo_Maquina=cmbTipo.Text,
            Marca=txtMarca.Text,Modelo=txtModelo.Text,Numero_Serie=txtSerie.Text,
            Costo_Maquina=nudCosto.Value,Fecha_Adquisicion=dtpAdquisicion.Value,
            Mantenimiento_Programado=cmbFrecuencia.Text,Proximo_Mantenimiento=dtpProximo.Value,
            Horometro=nudHorometro.Value,Ubicacion=txtUbicacion.Text,Observaciones=txtObservaciones.Text
        };

        private void dgvMaquinas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CargarMaquinaDesdeFila(dgvMaquinas.Rows[e.RowIndex]);
        }

        private void btnEditarMaquina_Click(object? sender, EventArgs e)
        {
            DataGridViewRow? fila = ObtenerFilaMaquinaSeleccionada();
            if (fila == null)
            {
                MostrarError("Seleccione una maquinaria para editar.");
                return;
            }

            CargarMaquinaDesdeFila(fila);
        }

        private void CargarMaquinaDesdeFila(DataGridViewRow r)
        {
            idMaquina = Convert.ToInt32(r.Cells["ID"].Value);
            txtCodigo.Text = ValorCelda(r, "Código");
            txtNombre.Text = ValorCelda(r, "Nombre");
            cmbTipo.Text = ValorCelda(r, "Tipo");
            txtMarca.Text = ValorCelda(r, "Marca");
            txtModelo.Text = ValorCelda(r, "Modelo");
            txtSerie.Text = ValorCelda(r, "Serie");
            nudCosto.Value = Ajustar(r.Cells["Costo"].Value, nudCosto.Maximum);

            object adquisicion = r.Cells["Adquisición"].Value;
            if (adquisicion != DBNull.Value)
                dtpAdquisicion.Value = Convert.ToDateTime(adquisicion);

            cmbFrecuencia.Text = ValorCelda(r, "Frecuencia");
            object proximo = r.Cells["Próximo mantenimiento"].Value;
            if (proximo != DBNull.Value)
                dtpProximo.Value = Convert.ToDateTime(proximo);

            nudHorometro.Value = Ajustar(r.Cells["Horómetro"].Value, nudHorometro.Maximum);
            txtUbicacion.Text = ValorCelda(r, "Ubicación");
            txtObservaciones.Text = ValorCelda(r, "Observaciones");
            btnGuardar.Text = "Actualizar";
            btnEstadoMaquina.Enabled = true;
            btnEliminarMaquina.Enabled = true;
            btnEstadoMaquina.Text = ValorCelda(r, "Estado") == "Inactiva" ? "Reactivar" : "Desactivar";
            tabsOperacion.SelectedIndex = 0;
            lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            lblEstado.Text = "Editando maquinaria seleccionada.";
        }

        private void LimpiarMaquina()
        {
            idMaquina=0; foreach(TextBox t in new[]{txtCodigo,txtNombre,txtMarca,txtModelo,txtSerie,txtUbicacion,txtObservaciones})t.Clear();
            nudCosto.Value=0;nudHorometro.Value=0;btnGuardar.Text="Guardar";
            btnEstadoMaquina.Enabled=false;btnEliminarMaquina.Enabled=false;
            if (cmbTipo.Items.Count > 0) cmbTipo.SelectedIndex = 0;
            if (cmbFrecuencia.Items.Count > 0) cmbFrecuencia.SelectedIndex = 1;
            dtpAdquisicion.Value = DateTime.Today;
            dtpProximo.Value = DateTime.Today.AddMonths(3);
        }

        private void btnEstadoMaquina_Click(object? sender, EventArgs e)
        {
            try { BLL_GestionMaquinaria.CambiarEstado(idMaquina,btnEstadoMaquina.Text=="Reactivar"?"Disponible":"Inactiva");LimpiarMaquina();CargarTodo(); }
            catch(Exception ex)
            {
                MostrarError(PrepararMensajeAsignacionActiva(ex.Message));
                if (ex.Message.IndexOf("asignación activa", StringComparison.OrdinalIgnoreCase) >= 0)
                    tabsDatos.SelectedIndex = 1;
            }
        }

        private void btnEliminarMaquina_Click(object? sender, EventArgs e)
        {
            try
            {
                if (idMaquina <= 0)
                {
                    MostrarError("Abra una maquinaria con Editar antes de eliminarla desde el formulario.");
                    return;
                }

                string detalle = string.IsNullOrWhiteSpace(txtCodigo.Text)
                    ? txtNombre.Text
                    : $"{txtCodigo.Text} - {txtNombre.Text}";
                DialogResult confirmar = MessageBox.Show(
                    $"¿Desea eliminar la maquinaria {detalle}?\n\nSi tiene historial, el sistema la marcará como inactiva para conservar los reportes.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes) return;

                string resultado = BLL_GestionMaquinaria.EliminarMaquinaria(idMaquina);
                LimpiarMaquina();
                CargarTodo();
                lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
                lblEstado.Text = resultado;
            }
            catch(Exception ex){MostrarError(ex.Message);}
        }

        private void btnEliminarSeleccionMaquina_Click(object? sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> filas = ObtenerFilasMaquinasSeleccionadas();
                if (filas.Count == 0)
                {
                    MostrarError("Seleccione una o varias maquinarias para eliminar.");
                    return;
                }

                string mensaje = filas.Count == 1
                    ? $"¿Desea eliminar la maquinaria {DetalleFilaMaquina(filas[0])}?"
                    : $"¿Desea eliminar las {filas.Count} maquinarias seleccionadas?";

                DialogResult confirmar = MessageBox.Show(
                    $"{mensaje}\n\nSi alguna tiene historial, el sistema la marcará como inactiva para conservar los reportes.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes) return;

                int eliminadas = 0;
                int inactivadas = 0;
                List<string> errores = new List<string>();

                foreach (DataGridViewRow fila in filas)
                {
                    int idEliminar = Convert.ToInt32(fila.Cells["ID"].Value);
                    string detalle = DetalleFilaMaquina(fila);
                    try
                    {
                        string resultado = BLL_GestionMaquinaria.EliminarMaquinaria(idEliminar);
                        if (resultado.IndexOf("inactiva", StringComparison.OrdinalIgnoreCase) >= 0)
                            inactivadas++;
                        else
                            eliminadas++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"{detalle}: {PrepararMensajeAsignacionActiva(ex.Message)}");
                    }
                }

                LimpiarMaquina();
                CargarTodo();
                lblEstado.ForeColor = errores.Count == 0
                    ? Color.FromArgb(52, 73, 94)
                    : Color.FromArgb(183, 28, 28);
                lblEstado.Text = CrearResumenEliminacion(eliminadas, inactivadas, errores.Count);

                if (errores.Count > 0)
                {
                    MessageBox.Show(
                        string.Join(Environment.NewLine, errores),
                        "Maquinarias no procesadas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex){MostrarError(ex.Message);}
        }

        private void btnAsignar_Click(object? sender, EventArgs e)
        {
            try
            {
                BLL_GestionMaquinaria.AsignarMaquinaria(new Entitie_Asignacion_Maquinaria
                {ID_Trabajador=Convert.ToInt32(cmbTrabajador.SelectedValue),ID_Maquina=Convert.ToInt32(cmbMaquinaAsignacion.SelectedValue),
                 Fecha_Asignacion=dtpAsignacion.Value,Fecha_Prevista_Devolucion=dtpDevolucion.Value,
                 Actividad=txtActividad.Text,Observaciones=txtObservacionAsignacion.Text});
                CargarTodo();
            } catch(Exception ex){MostrarError(ex.Message);}
        }

        private void dgvAsignaciones_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex<0)return;
            DataGridViewRow r=dgvAsignaciones.Rows[e.RowIndex];
            if(ValorCelda(r, "Estado")=="Asignada")
            {
                idAsignacion=Convert.ToInt32(r.Cells["ID"].Value);
                btnDevolver.Enabled=true;
                lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
                lblEstado.Text = "Asignación seleccionada para devolución.";
            }
        }

        private void btnDevolver_Click(object? sender, EventArgs e)
        {
            try
            {
                DataGridViewRow? fila = ObtenerFilaAsignacionSeleccionada();
                int idDevolver = idAsignacion > 0
                    ? idAsignacion
                    : fila == null ? 0 : Convert.ToInt32(fila.Cells["ID"].Value);

                if (idDevolver <= 0)
                {
                    MostrarError("Seleccione una asignación activa para quitarla.");
                    return;
                }

                BLL_GestionMaquinaria.FinalizarAsignacion(idDevolver);
                idAsignacion=0;
                btnDevolver.Enabled=false;
                CargarTodo();
                lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
                lblEstado.Text = "Asignación quitada correctamente.";
            }
            catch(Exception ex){MostrarError(ex.Message);}
        }

        private void btnQuitarAsignaciones_Click(object? sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> filas = ObtenerFilasAsignacionesSeleccionadas();
                if (filas.Count == 0)
                {
                    MostrarError("Seleccione una o varias asignaciones activas.");
                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    filas.Count == 1
                        ? $"¿Desea quitar la asignación de {DetalleFilaAsignacion(filas[0])}?"
                        : $"¿Desea quitar las {filas.Count} asignaciones seleccionadas?",
                    "Confirmar devolución",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar != DialogResult.Yes) return;

                int quitadas = 0;
                int omitidas = 0;
                List<string> errores = new List<string>();

                foreach (DataGridViewRow fila in filas)
                {
                    if (ValorCelda(fila, "Estado") != "Asignada")
                    {
                        omitidas++;
                        continue;
                    }

                    try
                    {
                        BLL_GestionMaquinaria.FinalizarAsignacion(
                            Convert.ToInt32(fila.Cells["ID"].Value));
                        quitadas++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"{DetalleFilaAsignacion(fila)}: {ex.Message}");
                    }
                }

                idAsignacion = 0;
                btnDevolver.Enabled = false;
                CargarTodo();
                lblEstado.ForeColor = errores.Count == 0
                    ? Color.FromArgb(52, 73, 94)
                    : Color.FromArgb(183, 28, 28);
                lblEstado.Text = CrearResumenAsignaciones(quitadas, omitidas, errores.Count);

                if (errores.Count > 0)
                {
                    MessageBox.Show(
                        string.Join(Environment.NewLine, errores),
                        "Asignaciones no procesadas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex){MostrarError(ex.Message);}
        }

        private void btnRegistrarMantenimiento_Click(object? sender, EventArgs e)
        {
            try
            {
                BLL_GestionMaquinaria.RegistrarMantenimiento(new Entitie_MantenimientoMaquinaria
                {ID_Maquina=Convert.ToInt32(cmbMaquinaMantenimiento.SelectedValue),Tipo_Mantenimiento=cmbTipoMantenimiento.Text,
                 Fecha_Mantenimiento=dtpMantenimiento.Value,Costo=nudCostoMantenimiento.Value,
                 Horometro=nudHorometroMantenimiento.Value,Proximo_Mantenimiento=dtpSiguienteMantenimiento.Value,
                 Proveedor=txtProveedor.Text,Descripcion=txtDescripcionMantenimiento.Text});
                CargarTodo();
            }catch(Exception ex){MostrarError(ex.Message);}
        }

        private void FormatearColumnas()
        {
            foreach(DataGridView g in new[]{dgvMaquinas,dgvAsignaciones,dgvMantenimientos})
                foreach(string c in new[]{"ID","ID Máquina"})if(g.Columns.Contains(c))g.Columns[c].Visible=false;
            foreach(string c in new[]{"Costo","Horómetro"})if(dgvMaquinas.Columns.Contains(c))dgvMaquinas.Columns[c].DefaultCellStyle.Format="N2";
        }

        private void MostrarError(string m){lblEstado.Text=m;lblEstado.ForeColor=Color.FromArgb(183,28,28);}
        private static decimal Ajustar(object v,decimal max)=>Math.Min(max,Math.Max(0,v==DBNull.Value?0:Convert.ToDecimal(v)));

        private static string PrepararMensajeAsignacionActiva(string mensaje)
        {
            return mensaje.IndexOf("asignación activa", StringComparison.OrdinalIgnoreCase) >= 0
                ? mensaje + " Use la pestaña Asignaciones y el botón Quitar asignaciones."
                : mensaje;
        }

        private DataGridViewRow? ObtenerFilaMaquinaSeleccionada()
        {
            if (dgvMaquinas.CurrentRow != null && !dgvMaquinas.CurrentRow.IsNewRow)
                return dgvMaquinas.CurrentRow;
            if (dgvMaquinas.SelectedRows.Count > 0 && !dgvMaquinas.SelectedRows[0].IsNewRow)
                return dgvMaquinas.SelectedRows[0];
            return null;
        }

        private List<DataGridViewRow> ObtenerFilasMaquinasSeleccionadas()
        {
            List<DataGridViewRow> filas = new List<DataGridViewRow>();
            HashSet<int> ids = new HashSet<int>();

            foreach (DataGridViewRow fila in dgvMaquinas.SelectedRows)
                AgregarFilaMaquina(fila, filas, ids);

            if (filas.Count == 0)
                AgregarFilaMaquina(dgvMaquinas.CurrentRow, filas, ids);

            return filas;
        }

        private DataGridViewRow? ObtenerFilaAsignacionSeleccionada()
        {
            if (dgvAsignaciones.CurrentRow != null && !dgvAsignaciones.CurrentRow.IsNewRow)
                return dgvAsignaciones.CurrentRow;
            if (dgvAsignaciones.SelectedRows.Count > 0 && !dgvAsignaciones.SelectedRows[0].IsNewRow)
                return dgvAsignaciones.SelectedRows[0];
            return null;
        }

        private List<DataGridViewRow> ObtenerFilasAsignacionesSeleccionadas()
        {
            List<DataGridViewRow> filas = new List<DataGridViewRow>();
            HashSet<int> ids = new HashSet<int>();

            foreach (DataGridViewRow fila in dgvAsignaciones.SelectedRows)
                AgregarFilaPorId(fila, filas, ids);

            if (filas.Count == 0)
                AgregarFilaPorId(dgvAsignaciones.CurrentRow, filas, ids);

            return filas;
        }

        private static void AgregarFilaMaquina(
            DataGridViewRow? fila,
            List<DataGridViewRow> filas,
            HashSet<int> ids)
        {
            AgregarFilaPorId(fila, filas, ids);
        }

        private static void AgregarFilaPorId(
            DataGridViewRow? fila,
            List<DataGridViewRow> filas,
            HashSet<int> ids)
        {
            if (fila == null || fila.IsNewRow ||
                fila.DataGridView == null ||
                !fila.DataGridView.Columns.Contains("ID"))
            {
                return;
            }

            int id = Convert.ToInt32(fila.Cells["ID"].Value);
            if (ids.Add(id)) filas.Add(fila);
        }

        private static string DetalleFilaMaquina(DataGridViewRow fila)
        {
            string codigo = ValorCelda(fila, "Código");
            string nombre = ValorCelda(fila, "Nombre");
            return string.IsNullOrWhiteSpace(codigo) ? nombre : $"{codigo} - {nombre}";
        }

        private static string DetalleFilaAsignacion(DataGridViewRow fila)
        {
            string codigo = ValorCelda(fila, "Código");
            string maquinaria = ValorCelda(fila, "Maquinaria");
            string responsable = ValorCelda(fila, "Responsable");
            string activo = string.IsNullOrWhiteSpace(codigo)
                ? maquinaria
                : $"{codigo} - {maquinaria}";
            return string.IsNullOrWhiteSpace(responsable)
                ? activo
                : $"{activo} ({responsable})";
        }

        private static string CrearResumenEliminacion(
            int eliminadas,
            int inactivadas,
            int errores)
        {
            List<string> partes = new List<string>();
            if (eliminadas > 0) partes.Add($"{eliminadas} eliminada(s)");
            if (inactivadas > 0) partes.Add($"{inactivadas} marcada(s) como inactiva(s)");
            if (errores > 0) partes.Add($"{errores} no procesada(s)");
            return partes.Count == 0
                ? "No se procesaron maquinarias."
                : string.Join(", ", partes) + ".";
        }

        private static string CrearResumenAsignaciones(
            int quitadas,
            int omitidas,
            int errores)
        {
            List<string> partes = new List<string>();
            if (quitadas > 0) partes.Add($"{quitadas} asignación(es) quitada(s)");
            if (omitidas > 0) partes.Add($"{omitidas} omitida(s) por no estar activa(s)");
            if (errores > 0) partes.Add($"{errores} no procesada(s)");
            return partes.Count == 0
                ? "No se quitaron asignaciones."
                : string.Join(", ", partes) + ".";
        }

        private static string ValorCelda(DataGridViewRow fila, string columna)
        {
            if (fila.DataGridView == null || !fila.DataGridView.Columns.Contains(columna))
                return string.Empty;
            object valor = fila.Cells[columna].Value;
            return valor == DBNull.Value ? string.Empty : Convert.ToString(valor) ?? string.Empty;
        }

        private static TableLayoutPanel CrearFormulario()=>new TableLayoutPanel
        {Dock=DockStyle.Fill,AutoScroll=true,ColumnCount=1,AutoSize=false,Padding=new Padding(10)};

        private static void AgregarCampo(TableLayoutPanel t,string etiqueta,Control control)
        {
            Panel p=new Panel{Height=47,Dock=DockStyle.Top};
            Label l=CrearEtiqueta(etiqueta,0,0); control.Location=new Point(0,18);control.Width=276;
            if(control is ComboBox cb)cb.DropDownStyle=ComboBoxStyle.DropDownList;
            if(control is DateTimePicker d)d.Format=DateTimePickerFormat.Short;
            if(control is TextBox tx)ConfigurarTexto(tx,"");
            p.Controls.Add(control);p.Controls.Add(l);t.Controls.Add(p);
        }

        private TabPage CrearTabDatosMaquinaria()
        {
            TabPage t = new TabPage("Maquinaria");
            Panel contenedor = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 42,
                Padding = new Padding(6, 5, 0, 0)
            };
            ConfigurarBoton(btnEditarMaquina, "Editar seleccionado", Color.FromArgb(41, 128, 185), 136);
            ConfigurarBoton(btnEliminarSeleccionMaquina, "Eliminar seleccionados", Color.FromArgb(146, 43, 33), 158);
            btnEditarMaquina.Click += btnEditarMaquina_Click;
            btnEliminarSeleccionMaquina.Click += btnEliminarSeleccionMaquina_Click;
            acciones.Controls.AddRange(new Control[] { btnEditarMaquina, btnEliminarSeleccionMaquina });
            dgvMaquinas.Dock = DockStyle.Fill;
            dgvMaquinas.BackgroundColor = Color.White;
            dgvMaquinas.MultiSelect = true;
            contenedor.Controls.Add(dgvMaquinas);
            contenedor.Controls.Add(acciones);
            t.Controls.Add(contenedor);
            return t;
        }

        private TabPage CrearTabDatosAsignaciones()
        {
            TabPage t = new TabPage("Asignaciones");
            Panel contenedor = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 42,
                Padding = new Padding(6, 5, 0, 0)
            };

            ConfigurarBoton(btnQuitarAsignaciones, "Quitar asignaciones", Color.FromArgb(41, 128, 185), 142);
            btnQuitarAsignaciones.Click += btnQuitarAsignaciones_Click;
            acciones.Controls.Add(btnQuitarAsignaciones);

            dgvAsignaciones.Dock = DockStyle.Fill;
            dgvAsignaciones.BackgroundColor = Color.White;
            dgvAsignaciones.MultiSelect = true;
            contenedor.Controls.Add(dgvAsignaciones);
            contenedor.Controls.Add(acciones);
            t.Controls.Add(contenedor);
            return t;
        }

        private static TabPage CrearTabDatos(string titulo,DataGridView grid)
        {TabPage t=new TabPage(titulo);grid.Dock=DockStyle.Fill;grid.BackgroundColor=Color.White;t.Controls.Add(grid);return t;}
        private static void ConfigurarGrid(DataGridView g)
        {g.ReadOnly=true;g.AllowUserToAddRows=false;g.RowHeadersVisible=false;g.SelectionMode=DataGridViewSelectionMode.FullRowSelect;g.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;}
        private static void AgregarResumen(TableLayoutPanel t,int col,string titulo,Label valor,Color color)
        {Panel p=new Panel{Dock=DockStyle.Fill,BackColor=Color.White,Margin=new Padding(4),BorderStyle=BorderStyle.FixedSingle};
         p.Controls.Add(valor);p.Controls.Add(new Label{Text=titulo,Dock=DockStyle.Top,Height=20,Padding=new Padding(8,3,0,0)});
         valor.Dock=DockStyle.Fill;valor.Font=new Font("Segoe UI",12F,FontStyle.Bold);valor.ForeColor=color;valor.Padding=new Padding(8,0,0,0);valor.Text="0";t.Controls.Add(p,col,0);}
        private static Label CrearEtiqueta(string text,int x,int y)=>new Label{Text=text,AutoSize=true,Location=new Point(x,y)};
        private static void ConfigurarTexto(TextBox t,string placeholder){t.BorderStyle=BorderStyle.FixedSingle;t.Font=new Font("Segoe UI",9F);t.PlaceholderText=placeholder;}
        private static Button CrearBoton(string text,Color color,int width){Button b=new Button();ConfigurarBoton(b,text,color,width);return b;}
        private static void ConfigurarBoton(Button b,string text,Color color,int width){b.Text=text;b.BackColor=color;b.ForeColor=Color.White;b.FlatStyle=FlatStyle.Flat;b.FlatAppearance.BorderSize=0;b.Width=width;b.Height=32;b.Cursor=Cursors.Hand;}
    }
}

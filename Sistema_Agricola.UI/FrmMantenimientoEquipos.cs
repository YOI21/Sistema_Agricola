using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmMantenimientoEquipos : Form
    {
        private int idEquipo;
        private int idMantenimiento;
        private string estadoEquipoSeleccionado = "Activo";
        private DataTable equipos = new DataTable();

        private readonly TextBox txtBuscar = new TextBox();
        private readonly ComboBox cmbFiltroEstado = new ComboBox();
        private readonly CheckBox chkSoloEquipo = new CheckBox();
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly DateTimePicker dtpHasta = new DateTimePicker();
        private readonly Label lblEstado = new Label();
        private readonly Label lblTotalEquipos = new Label();
        private readonly Label lblUnidades = new Label();
        private readonly Label lblPorMantener = new Label();
        private readonly Label lblValor = new Label();

        private readonly TabControl tabsOperacion = new TabControl();
        private readonly TabControl tabsDatos = new TabControl();
        private readonly DataGridView dgvEquipos = new DataGridView();
        private readonly DataGridView dgvMantenimientos = new DataGridView();

        private readonly TextBox txtCodigo = new TextBox();
        private readonly TextBox txtNombre = new TextBox();
        private readonly ComboBox cmbTipoEquipo = new ComboBox();
        private readonly NumericUpDown nudCantidad = new NumericUpDown();
        private readonly NumericUpDown nudCosto = new NumericUpDown();
        private readonly DateTimePicker dtpAdquisicion = new DateTimePicker();
        private readonly DateTimePicker dtpProximoEquipo = new DateTimePicker();
        private readonly TextBox txtUbicacion = new TextBox();
        private readonly TextBox txtObservaciones = new TextBox();
        private readonly Button btnGuardarEquipo = new Button();
        private readonly Button btnNuevoEquipo = new Button();
        private readonly Button btnEstadoEquipo = new Button();
        private readonly Button btnEliminarEquipo = new Button();

        private readonly ComboBox cmbEquipoMantenimiento = new ComboBox();
        private readonly ComboBox cmbTipoMantenimiento = new ComboBox();
        private readonly DateTimePicker dtpMantenimiento = new DateTimePicker();
        private readonly NumericUpDown nudCantidadAtendida = new NumericUpDown();
        private readonly NumericUpDown nudCostoMantenimiento = new NumericUpDown();
        private readonly DateTimePicker dtpProximoMantenimiento = new DateTimePicker();
        private readonly TextBox txtResponsable = new TextBox();
        private readonly TextBox txtDescripcion = new TextBox();
        private readonly Button btnGuardarMantenimiento = new Button();
        private readonly Button btnNuevoMantenimiento = new Button();
        private readonly Button btnEliminarMantenimiento = new Button();

        public FrmMantenimientoEquipos()
        {
            ConstruirInterfaz();
            Load += FrmMantenimientoEquipos_Load;
        }

        private void ConstruirInterfaz()
        {
            Text = "Mantenimiento de equipos";
            FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1120, 680);
            MinimumSize = new Size(960, 620);
            Font = new Font("Segoe UI", 8.8F);

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            Label titulo = new Label
            {
                Text = "Mantenimiento de equipos",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(16, 10)
            };

            Button cerrar = CrearBoton("X", Color.FromArgb(192, 57, 43), 34);
            cerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cerrar.Location = new Point(1072, 10);
            cerrar.Click += (_, _) => Close();
            header.Resize += (_, _) =>
                cerrar.Location = new Point(header.Width - cerrar.Width - 14, 10);
            header.Controls.AddRange(new Control[] { titulo, cerrar });

            Panel contenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(238, 242, 245)
            };

            Panel filtros = CrearPanelFiltros();
            TableLayoutPanel resumen = CrearResumen();

            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                FixedPanel = FixedPanel.Panel1,
                SplitterWidth = 5,
                Panel1MinSize = 0,
                Panel2MinSize = 0
            };
            split.Panel1.Padding = new Padding(0, 6, 6, 0);
            split.Panel2.Padding = new Padding(6, 6, 0, 0);
            split.Resize += (_, _) => AjustarSeparador(split);

            tabsOperacion.Dock = DockStyle.Fill;
            tabsOperacion.Font = new Font("Segoe UI Semibold", 8.6F, FontStyle.Bold);
            tabsOperacion.TabPages.Add(CrearTabEquipo());
            tabsOperacion.TabPages.Add(CrearTabMantenimiento());
            split.Panel1.Controls.Add(tabsOperacion);

            Panel listado = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            tabsDatos.Dock = DockStyle.Fill;
            tabsDatos.TabPages.Add(CrearTabDatos("Equipos", dgvEquipos));
            tabsDatos.TabPages.Add(CrearTabDatos("Historial", dgvMantenimientos));
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
            AjustarSeparador(split);
        }

        private static void AjustarSeparador(SplitContainer split)
        {
            if (split.Width <= 0) return;

            int anchoPanelFormulario = Math.Min(340, Math.Max(280, split.Width / 3));
            int anchoListadoMinimo = Math.Min(420, Math.Max(220, split.Width / 2));

            split.Panel1MinSize = Math.Min(280, Math.Max(0, split.Width - anchoListadoMinimo - split.SplitterWidth));
            split.Panel2MinSize = Math.Min(anchoListadoMinimo, Math.Max(0, split.Width - split.Panel1MinSize - split.SplitterWidth));

            int maximo = split.Width - split.Panel2MinSize - split.SplitterWidth;
            if (maximo < split.Panel1MinSize) return;

            split.SplitterDistance = Math.Max(
                split.Panel1MinSize,
                Math.Min(anchoPanelFormulario, maximo));
        }

        private Panel CrearPanelFiltros()
        {
            Panel filtros = new Panel
            {
                Dock = DockStyle.Top,
                Height = 58,
                BackColor = Color.White
            };

            ConfigurarTexto(txtBuscar, "Codigo, nombre, tipo o ubicacion");
            txtBuscar.Location = new Point(12, 25);
            txtBuscar.Size = new Size(275, 25);

            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Location = new Point(304, 25);
            cmbFiltroEstado.Size = new Size(140, 25);

            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(462, 25);
            dtpDesde.Width = 112;
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(590, 25);
            dtpHasta.Width = 112;

            chkSoloEquipo.Text = "Solo equipo";
            chkSoloEquipo.AutoSize = true;
            chkSoloEquipo.Location = new Point(716, 27);
            chkSoloEquipo.CheckedChanged += (_, _) => CargarMantenimientos();

            Button aplicar = CrearBoton("Aplicar", Color.FromArgb(46, 125, 50), 88);
            aplicar.Location = new Point(812, 21);
            aplicar.Click += (_, _) => CargarTodo();

            Button historial = CrearBoton("Historial", Color.FromArgb(41, 128, 185), 92);
            historial.Location = new Point(908, 21);
            historial.Click += (_, _) => CargarMantenimientos();

            filtros.Controls.AddRange(new Control[]
            {
                CrearEtiqueta("Buscar", 12, 6),
                txtBuscar,
                CrearEtiqueta("Estado", 304, 6),
                cmbFiltroEstado,
                CrearEtiqueta("Desde", 462, 6),
                dtpDesde,
                CrearEtiqueta("Hasta", 590, 6),
                dtpHasta,
                chkSoloEquipo,
                aplicar,
                historial
            });

            return filtros;
        }

        private TableLayoutPanel CrearResumen()
        {
            TableLayoutPanel resumen = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 68,
                ColumnCount = 4
            };

            for (int i = 0; i < 4; i++)
            {
                resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            AgregarResumen(resumen, 0, "Equipos activos", lblTotalEquipos, Color.FromArgb(52, 73, 94));
            AgregarResumen(resumen, 1, "Unidades", lblUnidades, Color.FromArgb(35, 115, 70));
            AgregarResumen(resumen, 2, "Mant. 30 dias", lblPorMantener, Color.FromArgb(192, 57, 43));
            AgregarResumen(resumen, 3, "Valor equipos", lblValor, Color.FromArgb(110, 80, 145));

            return resumen;
        }

        private TabPage CrearTabEquipo()
        {
            TabPage tab = new TabPage("Equipo") { BackColor = Color.White };
            TableLayoutPanel formulario = CrearFormulario();

            AgregarCampo(formulario, "Codigo", txtCodigo);
            AgregarCampo(formulario, "Nombre", txtNombre);
            AgregarCampo(formulario, "Tipo", cmbTipoEquipo);
            AgregarCampo(formulario, "Cantidad", nudCantidad);
            AgregarCampo(formulario, "Costo unitario RD$", nudCosto);
            AgregarCampo(formulario, "Fecha de adquisicion", dtpAdquisicion);
            AgregarCampo(formulario, "Proximo mantenimiento", dtpProximoEquipo);
            AgregarCampo(formulario, "Ubicacion", txtUbicacion);
            AgregarCampo(formulario, "Observaciones", txtObservaciones);

            FlowLayoutPanel acciones = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 74 };
            ConfigurarBoton(btnGuardarEquipo, "Guardar", Color.FromArgb(46, 125, 50), 86);
            ConfigurarBoton(btnNuevoEquipo, "Nuevo", Color.FromArgb(110, 120, 130), 76);
            ConfigurarBoton(btnEstadoEquipo, "Inactivar", Color.FromArgb(192, 57, 43), 86);
            ConfigurarBoton(btnEliminarEquipo, "Eliminar", Color.FromArgb(146, 43, 33), 86);
            btnGuardarEquipo.Click += btnGuardarEquipo_Click;
            btnNuevoEquipo.Click += (_, _) => LimpiarEquipo();
            btnEstadoEquipo.Click += btnEstadoEquipo_Click;
            btnEliminarEquipo.Click += btnEliminarEquipo_Click;
            btnEstadoEquipo.Enabled = false;
            btnEliminarEquipo.Enabled = false;
            acciones.Controls.AddRange(new Control[]
            {
                btnGuardarEquipo,
                btnNuevoEquipo,
                btnEstadoEquipo,
                btnEliminarEquipo
            });
            formulario.Controls.Add(acciones);
            tab.Controls.Add(formulario);
            return tab;
        }

        private TabPage CrearTabMantenimiento()
        {
            TabPage tab = new TabPage("Mantenimiento") { BackColor = Color.White };
            TableLayoutPanel formulario = CrearFormulario();

            AgregarCampo(formulario, "Equipo", cmbEquipoMantenimiento);
            AgregarCampo(formulario, "Tipo", cmbTipoMantenimiento);
            AgregarCampo(formulario, "Fecha", dtpMantenimiento);
            AgregarCampo(formulario, "Cantidad atendida", nudCantidadAtendida);
            AgregarCampo(formulario, "Costo RD$", nudCostoMantenimiento);
            AgregarCampo(formulario, "Proximo mantenimiento", dtpProximoMantenimiento);
            AgregarCampo(formulario, "Responsable / taller", txtResponsable);
            AgregarCampo(formulario, "Trabajo realizado", txtDescripcion);

            FlowLayoutPanel acciones = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 74 };
            ConfigurarBoton(btnGuardarMantenimiento, "Guardar", Color.FromArgb(46, 125, 50), 86);
            ConfigurarBoton(btnNuevoMantenimiento, "Nuevo", Color.FromArgb(110, 120, 130), 76);
            ConfigurarBoton(btnEliminarMantenimiento, "Eliminar", Color.FromArgb(146, 43, 33), 86);
            btnGuardarMantenimiento.Click += btnGuardarMantenimiento_Click;
            btnNuevoMantenimiento.Click += (_, _) => LimpiarMantenimiento();
            btnEliminarMantenimiento.Click += btnEliminarMantenimiento_Click;
            btnEliminarMantenimiento.Enabled = false;
            acciones.Controls.AddRange(new Control[]
            {
                btnGuardarMantenimiento,
                btnNuevoMantenimiento,
                btnEliminarMantenimiento
            });
            formulario.Controls.Add(acciones);
            tab.Controls.Add(formulario);
            return tab;
        }

        private void FrmMantenimientoEquipos_Load(object? sender, EventArgs e)
        {
            try
            {
                ConfigurarCombos();
                ConfigurarGrid(dgvEquipos);
                ConfigurarGrid(dgvMantenimientos);
                dgvEquipos.CellClick += dgvEquipos_CellClick;
                dgvMantenimientos.CellClick += dgvMantenimientos_CellClick;
                LimpiarEquipo();
                LimpiarMantenimiento();
                CargarTodo();
            }
            catch (Exception ex)
            {
                MostrarError("Error al iniciar mantenimiento de equipos: " + ex.Message);
            }
        }

        private void ConfigurarCombos()
        {
            cmbFiltroEstado.Items.AddRange(new object[]
            {
                "Activo",
                "En mantenimiento",
                "Inactivo",
                "Todos"
            });
            cmbFiltroEstado.SelectedIndex = 0;

            cmbTipoEquipo.Items.AddRange(new object[]
            {
                "Herramienta manual",
                "Equipo menor",
                "Equipo de riego",
                "Equipo de fumigacion",
                "Equipo de seguridad",
                "Equipo electrico",
                "Otro"
            });
            cmbTipoEquipo.SelectedIndex = 1;

            cmbTipoMantenimiento.Items.AddRange(new object[]
            {
                "Preventivo",
                "Correctivo",
                "Limpieza",
                "Calibracion",
                "Reparacion",
                "Inspeccion"
            });
            cmbTipoMantenimiento.SelectedIndex = 0;

            foreach (NumericUpDown n in new[]
            {
                nudCosto,
                nudCostoMantenimiento
            })
            {
                n.Maximum = 999999999;
                n.DecimalPlaces = 2;
                n.ThousandsSeparator = true;
            }

            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 999999;
            nudCantidadAtendida.Minimum = 1;
            nudCantidadAtendida.Maximum = 999999;

            dtpAdquisicion.Format = DateTimePickerFormat.Short;
            dtpAdquisicion.ShowCheckBox = true;
            dtpAdquisicion.Value = DateTime.Today;
            dtpAdquisicion.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtpAdquisicion.Checked = false;

            dtpProximoEquipo.Format = DateTimePickerFormat.Short;
            dtpProximoEquipo.ShowCheckBox = true;
            dtpProximoEquipo.Value = DateTime.Today.AddMonths(3);
            dtpProximoEquipo.Checked = false;

            dtpMantenimiento.Format = DateTimePickerFormat.Short;
            dtpMantenimiento.Value = DateTime.Today;
            dtpMantenimiento.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            dtpProximoMantenimiento.Format = DateTimePickerFormat.Short;
            dtpProximoMantenimiento.ShowCheckBox = true;
            dtpProximoMantenimiento.Value = DateTime.Today.AddMonths(3);
            dtpProximoMantenimiento.Checked = true;

            dtpDesde.Value = DateTime.Today.AddYears(-1);
            dtpHasta.Value = DateTime.Today;
        }

        private void CargarTodo()
        {
            try
            {
                equipos = BLL_MantenimientoEquipos.ListarEquipos(
                    txtBuscar.Text,
                    cmbFiltroEstado.Text);
                dgvEquipos.DataSource = equipos;

                DataTable todos = BLL_MantenimientoEquipos.ListarEquipos(string.Empty, "Todos");
                ActualizarResumen(BLL_MantenimientoEquipos.CalcularResumen(todos));
                CargarComboEquipos(todos);
                CargarMantenimientos();
                FormatearColumnas();

                lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
                lblEstado.Text = $"{equipos.Rows.Count} equipos encontrados.";
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void CargarMantenimientos()
        {
            try
            {
                int? idFiltro = chkSoloEquipo.Checked && idEquipo > 0
                    ? idEquipo
                    : null;

                dgvMantenimientos.DataSource = BLL_MantenimientoEquipos.ListarMantenimientos(
                    idFiltro,
                    dtpDesde.Value,
                    dtpHasta.Value);
                FormatearColumnas();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void CargarComboEquipos(DataTable todos)
        {
            DataTable activos = todos.Clone();
            foreach (DataRow fila in todos.Rows)
            {
                string estado = Convert.ToString(fila["Estado"]) ?? string.Empty;
                if (!estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                {
                    activos.ImportRow(fila);
                }
            }

            if (!activos.Columns.Contains("Equipo"))
            {
                activos.Columns.Add("Equipo", typeof(string));
            }

            foreach (DataRow fila in activos.Rows)
            {
                fila["Equipo"] = $"{fila["Codigo"]} - {fila["Nombre"]}";
            }

            cmbEquipoMantenimiento.DataSource = activos;
            cmbEquipoMantenimiento.DisplayMember = "Equipo";
            cmbEquipoMantenimiento.ValueMember = "ID";
        }

        private void ActualizarResumen(Entitie_ResumenEquipos resumen)
        {
            lblTotalEquipos.Text = resumen.TotalActivos.ToString();
            lblUnidades.Text = resumen.Unidades.ToString();
            lblPorMantener.Text = resumen.MantenimientoProximo.ToString();
            lblValor.Text = $"RD$ {resumen.ValorTotal:N2}";
        }

        private void btnGuardarEquipo_Click(object? sender, EventArgs e)
        {
            try
            {
                bool editando = idEquipo > 0;
                Entitie_Equipo equipo = CrearEquipo();
                if (editando)
                {
                    equipo.Id_Equipo = idEquipo;
                    BLL_MantenimientoEquipos.ActualizarEquipo(equipo);
                }
                else
                {
                    BLL_MantenimientoEquipos.RegistrarEquipo(equipo);
                }

                LimpiarEquipo();
                CargarTodo();
                MostrarInfo(editando
                    ? "Equipo actualizado correctamente."
                    : "Equipo registrado correctamente.");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnEstadoEquipo_Click(object? sender, EventArgs e)
        {
            try
            {
                if (idEquipo <= 0)
                {
                    MostrarError("Seleccione un equipo.");
                    return;
                }

                string nuevoEstado = estadoEquipoSeleccionado == "Inactivo"
                    ? "Activo"
                    : "Inactivo";
                BLL_MantenimientoEquipos.CambiarEstadoEquipo(idEquipo, nuevoEstado);
                LimpiarEquipo();
                CargarTodo();
                MostrarInfo($"Equipo marcado como {nuevoEstado.ToLower()}.");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnEliminarEquipo_Click(object? sender, EventArgs e)
        {
            try
            {
                if (idEquipo <= 0)
                {
                    MostrarError("Seleccione un equipo.");
                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    $"Desea eliminar el equipo {txtCodigo.Text} - {txtNombre.Text}?\n\n" +
                    "Si tiene mantenimientos registrados quedara inactivo para conservar el historial.",
                    "Confirmar eliminacion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes) return;

                string resultado = BLL_MantenimientoEquipos.EliminarEquipo(idEquipo);
                LimpiarEquipo();
                CargarTodo();
                MostrarInfo(resultado);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnGuardarMantenimiento_Click(object? sender, EventArgs e)
        {
            try
            {
                bool editando = idMantenimiento > 0;
                Entitie_MantenimientoEquipo mantenimiento = CrearMantenimiento();

                if (editando)
                {
                    mantenimiento.Id_Mantenimiento = idMantenimiento;
                    BLL_MantenimientoEquipos.ActualizarMantenimiento(mantenimiento);
                }
                else
                {
                    BLL_MantenimientoEquipos.RegistrarMantenimiento(mantenimiento);
                }

                LimpiarMantenimiento();
                CargarTodo();
                tabsDatos.SelectedIndex = 1;
                MostrarInfo(editando
                    ? "Mantenimiento actualizado correctamente."
                    : "Mantenimiento registrado correctamente.");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnEliminarMantenimiento_Click(object? sender, EventArgs e)
        {
            try
            {
                if (idMantenimiento <= 0)
                {
                    MostrarError("Seleccione un mantenimiento.");
                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    "Desea eliminar el mantenimiento seleccionado?",
                    "Confirmar eliminacion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes) return;

                string resultado = BLL_MantenimientoEquipos
                    .EliminarMantenimiento(idMantenimiento);
                LimpiarMantenimiento();
                CargarTodo();
                tabsDatos.SelectedIndex = 1;
                MostrarInfo(resultado);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private Entitie_Equipo CrearEquipo()
        {
            return new Entitie_Equipo
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Tipo = cmbTipoEquipo.Text,
                Cantidad = Convert.ToInt32(nudCantidad.Value),
                Costo = nudCosto.Value,
                Fecha_Adquisicion = dtpAdquisicion.Checked
                    ? (DateTime?)dtpAdquisicion.Value.Date
                    : null,
                Proximo_Mantenimiento = dtpProximoEquipo.Checked
                    ? (DateTime?)dtpProximoEquipo.Value.Date
                    : null,
                Ubicacion = txtUbicacion.Text,
                Observaciones = txtObservaciones.Text
            };
        }

        private Entitie_MantenimientoEquipo CrearMantenimiento()
        {
            return new Entitie_MantenimientoEquipo
            {
                Id_Equipo = ObtenerIdEquipoMantenimiento(),
                Tipo_Mantenimiento = cmbTipoMantenimiento.Text,
                Fecha_Mantenimiento = dtpMantenimiento.Value.Date,
                Cantidad_Atendida = Convert.ToInt32(nudCantidadAtendida.Value),
                Costo = nudCostoMantenimiento.Value,
                Proximo_Mantenimiento = dtpProximoMantenimiento.Checked
                    ? (DateTime?)dtpProximoMantenimiento.Value.Date
                    : null,
                Responsable = txtResponsable.Text,
                Descripcion = txtDescripcion.Text
            };
        }

        private int ObtenerIdEquipoMantenimiento()
        {
            if (cmbEquipoMantenimiento.SelectedValue == null ||
                cmbEquipoMantenimiento.SelectedValue == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(cmbEquipoMantenimiento.SelectedValue);
        }

        private void dgvEquipos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CargarEquipoDesdeFila(dgvEquipos.Rows[e.RowIndex]);
        }

        private void dgvMantenimientos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CargarMantenimientoDesdeFila(dgvMantenimientos.Rows[e.RowIndex]);
        }

        private void CargarEquipoDesdeFila(DataGridViewRow fila)
        {
            idEquipo = Convert.ToInt32(fila.Cells["ID"].Value);
            txtCodigo.Text = ValorCelda(fila, "Codigo");
            txtNombre.Text = ValorCelda(fila, "Nombre");
            SeleccionarCombo(cmbTipoEquipo, ValorCelda(fila, "Tipo"));
            nudCantidad.Value = AjustarEntero(fila.Cells["Cantidad"].Value, nudCantidad);
            nudCosto.Value = AjustarDecimal(fila.Cells["Costo"].Value, nudCosto);
            AsignarFecha(dtpAdquisicion, fila.Cells["Adquisicion"].Value, optional: true);
            AsignarFecha(dtpProximoEquipo, fila.Cells["Proximo mantenimiento"].Value, optional: true);
            txtUbicacion.Text = ValorCelda(fila, "Ubicacion");
            txtObservaciones.Text = ValorCelda(fila, "Observaciones");
            estadoEquipoSeleccionado = ValorCelda(fila, "Estado");
            btnEstadoEquipo.Text = estadoEquipoSeleccionado == "Inactivo"
                ? "Activar"
                : "Inactivar";
            btnEstadoEquipo.Enabled = true;
            btnEliminarEquipo.Enabled = true;
            chkSoloEquipo.Text = "Solo equipo";
            CargarMantenimientos();
        }

        private void CargarMantenimientoDesdeFila(DataGridViewRow fila)
        {
            idMantenimiento = Convert.ToInt32(fila.Cells["ID"].Value);
            int idEquipoFila = Convert.ToInt32(fila.Cells["ID Equipo"].Value);
            SeleccionarComboPorValor(cmbEquipoMantenimiento, idEquipoFila);
            SeleccionarCombo(cmbTipoMantenimiento, ValorCelda(fila, "Tipo"));
            AsignarFecha(dtpMantenimiento, fila.Cells["Fecha"].Value, optional: false);
            nudCantidadAtendida.Value = AjustarEntero(
                fila.Cells["Cantidad atendida"].Value,
                nudCantidadAtendida);
            nudCostoMantenimiento.Value = AjustarDecimal(
                fila.Cells["Costo"].Value,
                nudCostoMantenimiento);
            AsignarFecha(
                dtpProximoMantenimiento,
                fila.Cells["Proximo mantenimiento"].Value,
                optional: true);
            txtResponsable.Text = ValorCelda(fila, "Responsable");
            txtDescripcion.Text = ValorCelda(fila, "Descripcion");
            btnEliminarMantenimiento.Enabled = true;
            tabsOperacion.SelectedIndex = 1;
        }

        private void LimpiarEquipo()
        {
            idEquipo = 0;
            estadoEquipoSeleccionado = "Activo";
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            if (cmbTipoEquipo.Items.Count > 0) cmbTipoEquipo.SelectedIndex = 1;
            nudCantidad.Value = 1;
            nudCosto.Value = 0;
            dtpAdquisicion.Value = DateTime.Today;
            dtpAdquisicion.Checked = false;
            dtpProximoEquipo.Value = DateTime.Today.AddMonths(3);
            dtpProximoEquipo.Checked = false;
            txtUbicacion.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            btnEstadoEquipo.Text = "Inactivar";
            btnEstadoEquipo.Enabled = false;
            btnEliminarEquipo.Enabled = false;
        }

        private void LimpiarMantenimiento()
        {
            idMantenimiento = 0;
            if (cmbEquipoMantenimiento.Items.Count > 0 &&
                cmbEquipoMantenimiento.SelectedIndex < 0)
            {
                cmbEquipoMantenimiento.SelectedIndex = 0;
            }

            if (cmbTipoMantenimiento.Items.Count > 0) cmbTipoMantenimiento.SelectedIndex = 0;
            dtpMantenimiento.Value = DateTime.Today;
            nudCantidadAtendida.Value = 1;
            nudCostoMantenimiento.Value = 0;
            dtpProximoMantenimiento.Value = DateTime.Today.AddMonths(3);
            dtpProximoMantenimiento.Checked = true;
            txtResponsable.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            btnEliminarMantenimiento.Enabled = false;
        }

        private void FormatearColumnas()
        {
            foreach (DataGridView grid in new[] { dgvEquipos, dgvMantenimientos })
            {
                foreach (string columna in new[] { "ID", "ID Equipo" })
                {
                    if (grid.Columns.Contains(columna)) grid.Columns[columna].Visible = false;
                }

                foreach (string columna in new[] { "Costo" })
                {
                    if (grid.Columns.Contains(columna))
                    {
                        grid.Columns[columna].DefaultCellStyle.Format = "C2";
                        grid.Columns[columna].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleRight;
                    }
                }

                foreach (string columna in new[] { "Adquisicion", "Fecha", "Proximo mantenimiento" })
                {
                    if (grid.Columns.Contains(columna))
                    {
                        grid.Columns[columna].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
            }
        }

        private void MostrarError(string mensaje)
        {
            lblEstado.ForeColor = Color.FromArgb(183, 28, 28);
            lblEstado.Text = mensaje;
        }

        private void MostrarInfo(string mensaje)
        {
            lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            lblEstado.Text = mensaje;
        }

        private static TabPage CrearTabDatos(string titulo, DataGridView grid)
        {
            TabPage tab = new TabPage(titulo);
            grid.Dock = DockStyle.Fill;
            grid.BackgroundColor = Color.White;
            tab.Controls.Add(grid);
            return tab;
        }

        private static TableLayoutPanel CrearFormulario()
        {
            return new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                ColumnCount = 1,
                Padding = new Padding(10)
            };
        }

        private static void AgregarCampo(TableLayoutPanel panel, string etiqueta, Control control)
        {
            Panel contenedor = new Panel { Height = 47, Dock = DockStyle.Top };
            Label label = CrearEtiqueta(etiqueta, 0, 0);
            control.Location = new Point(0, 18);
            control.Width = 292;

            if (control is ComboBox combo) combo.DropDownStyle = ComboBoxStyle.DropDownList;
            if (control is TextBox texto) ConfigurarTexto(texto, string.Empty);
            if (control is DateTimePicker fecha) fecha.Format = DateTimePickerFormat.Short;

            contenedor.Controls.Add(control);
            contenedor.Controls.Add(label);
            panel.Controls.Add(contenedor);
        }

        private static void ConfigurarGrid(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.MultiSelect = false;
        }

        private static void AgregarResumen(
            TableLayoutPanel tabla,
            int columna,
            string titulo,
            Label valor,
            Color color)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(4),
                BorderStyle = BorderStyle.FixedSingle
            };

            panel.Controls.Add(valor);
            panel.Controls.Add(new Label
            {
                Text = titulo,
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(8, 3, 0, 0)
            });

            valor.Dock = DockStyle.Fill;
            valor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            valor.ForeColor = color;
            valor.Padding = new Padding(8, 0, 0, 0);
            valor.Text = "0";
            tabla.Controls.Add(panel, columna, 0);
        }

        private static Label CrearEtiqueta(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Location = new Point(x, y),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
        }

        private static Button CrearBoton(string text, Color color, int width)
        {
            Button button = new Button();
            ConfigurarBoton(button, text, color, width);
            return button;
        }

        private static void ConfigurarBoton(Button button, string text, Color color, int width)
        {
            button.Text = text;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Width = width;
            button.Height = 32;
            button.Cursor = Cursors.Hand;
        }

        private static void ConfigurarTexto(TextBox textBox, string placeholder)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 9F);
            textBox.PlaceholderText = placeholder;
        }

        private static void SeleccionarCombo(ComboBox combo, string valor)
        {
            int index = combo.Items.IndexOf(valor);
            if (index >= 0)
            {
                combo.SelectedIndex = index;
                return;
            }

            if (!string.IsNullOrWhiteSpace(valor))
            {
                combo.Items.Add(valor);
                combo.SelectedItem = valor;
            }
        }

        private static void SeleccionarComboPorValor(ComboBox combo, int valor)
        {
            combo.SelectedValue = valor;
        }

        private static void AsignarFecha(
            DateTimePicker picker,
            object valor,
            bool optional)
        {
            if (valor == DBNull.Value || valor == null)
            {
                if (optional) picker.Checked = false;
                return;
            }

            picker.Value = Convert.ToDateTime(valor);
            if (optional) picker.Checked = true;
        }

        private static decimal AjustarDecimal(object valor, NumericUpDown control)
        {
            decimal numero = valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);
            if (numero < control.Minimum) return control.Minimum;
            if (numero > control.Maximum) return control.Maximum;
            return numero;
        }

        private static decimal AjustarEntero(object valor, NumericUpDown control)
        {
            decimal numero = valor == DBNull.Value ? 1 : Convert.ToDecimal(valor);
            if (numero < control.Minimum) return control.Minimum;
            if (numero > control.Maximum) return control.Maximum;
            return Math.Truncate(numero);
        }

        private static string ValorCelda(DataGridViewRow fila, string columna)
        {
            if (fila.DataGridView == null || !fila.DataGridView.Columns.Contains(columna))
            {
                return string.Empty;
            }

            object valor = fila.Cells[columna].Value;
            return valor == DBNull.Value ? string.Empty : Convert.ToString(valor) ?? string.Empty;
        }
    }
}

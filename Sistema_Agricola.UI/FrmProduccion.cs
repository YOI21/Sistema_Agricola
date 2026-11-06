using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmProduccion : Form
    {
        private int idProduccionSeleccionada;
        private DataTable producciones = new DataTable();
        private decimal cantidadCosechada;
        private decimal costoBaseTotal;

        public FrmProduccion()
        {
            InitializeComponent();
        }

        private void FrmProduccion_Load(object? sender, EventArgs e)
        {
            cmbUnidad.Items.AddRange(new object[]
            {
                "kg", "libra", "quintal", "tonelada", "unidad", "caja", "saco"
            });
            cmbEstado.Items.AddRange(new object[]
            {
                "Registrada", "Almacenada", "Vendida", "Cancelada"
            });
            cmbMetodoPago.Items.AddRange(new object[]
            {
                "Efectivo", "Transferencia", "Tarjeta", "Cheque", "Otro"
            });
            cmbEstadoCobro.Items.AddRange(new object[] { "Pendiente", "Pagado" });
            cmbFiltroEstado.Items.AddRange(new object[]
            {
                "Todos", "Registrada", "Almacenada", "Vendida", "Cancelada"
            });

            dtpDesde.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpHasta.Value = DateTime.Today;
            dtpFechaProduccion.Value = DateTime.Today;
            dtpFechaVenta.Value = DateTime.Today;
            cmbFiltroEstado.SelectedIndex = 0;

            btnEliminar.Visible =
                BLL_Seguridad.TienePermiso(PermisosSistema.ProduccionEliminar);
            btnAnular.Visible =
                BLL_Seguridad.TienePermiso(PermisosSistema.ProduccionAnular);

            LimpiarEdicion();
            CargarProducciones();
        }

        private void CargarProducciones()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string estado = cmbFiltroEstado.SelectedItem?.ToString() ?? "Todos";
                producciones = BLL_Produccion.Listar(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    txtBuscar.Text,
                    estado == "Todos" ? string.Empty : estado);
                dgvProduccion.DataSource = producciones;
                ActualizarResumen();
                lblEstadoFormulario.Text =
                    $"{producciones.Rows.Count} lotes de producción encontrados";
                lblEstadoFormulario.ForeColor = Color.FromArgb(70, 80, 90);
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex, "cargar la producción");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CargarCosechas(int? idProduccion = null, int? idCosecha = null)
        {
            DataTable cosechas = BLL_Produccion.ListarCosechasDisponibles(idProduccion);
            cmbCosecha.DataSource = cosechas;
            cmbCosecha.DisplayMember = "Cosecha";
            cmbCosecha.ValueMember = "ID_Cosecha";

            if (idCosecha.HasValue)
            {
                cmbCosecha.SelectedValue = idCosecha.Value;
            }

            if (cosechas.Rows.Count == 0)
            {
                lblDisponible.Text = "Sin cosechas disponibles";
                nudCantidadBruta.Maximum = 1;
            }
        }

        private void cmbCosecha_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbCosecha.SelectedItem is not DataRowView fila)
            {
                cantidadCosechada = 0;
                costoBaseTotal = 0;
                lblDisponible.Text = "0";
                CalcularValores();
                return;
            }

            decimal disponible = ObtenerDecimal(fila.Row, "Cantidad_Disponible");
            cantidadCosechada = ObtenerDecimal(fila.Row, "Cantidad_Cosechada");
            costoBaseTotal = ObtenerDecimal(fila.Row, "Costo_Base_Total");
            lblDisponible.Text = $"{disponible:N3}";
            nudCantidadBruta.Maximum = Math.Max(1, disponible);

            DateTime? fechaCosecha = ObtenerFecha(fila.Row, "Fecha_Cosecha");
            if (fechaCosecha.HasValue &&
                dtpFechaProduccion.Value.Date < fechaCosecha.Value.Date)
            {
                dtpFechaProduccion.Value =
                    fechaCosecha.Value.Date > DateTime.Today
                        ? DateTime.Today
                        : fechaCosecha.Value.Date;
            }

            CalcularValores();
        }

        private void Calculo_ValueChanged(object? sender, EventArgs e)
        {
            CalcularValores();
        }

        private void CalcularValores()
        {
            decimal neta = Math.Max(0, nudCantidadBruta.Value - nudMerma.Value);
            decimal valor = neta * nudPrecioUnitario.Value;
            decimal costoBase = cantidadCosechada <= 0
                ? 0
                : Math.Round(
                    costoBaseTotal * nudCantidadBruta.Value / cantidadCosechada,
                    2);
            decimal beneficio = valor - costoBase - nudCostoPostcosecha.Value;

            lblCantidadNeta.Text = $"{neta:N3}";
            lblValorProduccion.Text = $"RD$ {valor:N2}";
            lblCostoBase.Text = $"RD$ {costoBase:N2}";
            lblBeneficio.Text = $"RD$ {beneficio:N2}";
            lblBeneficio.ForeColor = beneficio < 0
                ? Color.FromArgb(176, 58, 46)
                : Color.FromArgb(35, 125, 82);
        }

        private void cmbEstado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool vendida = cmbEstado.SelectedItem?.ToString() == "Vendida";
            foreach (Control control in tablaVenta.Controls)
            {
                control.Enabled = vendida;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                Entitie_Produccion produccion = CrearDesdeFormulario();
                if (idProduccionSeleccionada > 0)
                {
                    produccion.ID_Produccion = idProduccionSeleccionada;
                    BLL_Produccion.Actualizar(produccion);
                    MostrarExito("Producción actualizada y finanzas sincronizadas.");
                }
                else
                {
                    BLL_Produccion.Registrar(produccion);
                    MostrarExito("Producción registrada y finanzas sincronizadas.");
                }

                LimpiarEdicion();
                CargarProducciones();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex, "guardar la producción");
            }
        }

        private Entitie_Produccion CrearDesdeFormulario()
        {
            if (cmbCosecha.SelectedValue == null)
            {
                throw new ArgumentException("Seleccione una cosecha disponible.");
            }

            bool vendida = cmbEstado.SelectedItem?.ToString() == "Vendida";
            return new Entitie_Produccion
            {
                ID_Cosecha = Convert.ToInt32(cmbCosecha.SelectedValue),
                Fecha_Produccion = dtpFechaProduccion.Value.Date,
                Unidad_Medida = cmbUnidad.SelectedItem?.ToString() ?? string.Empty,
                Cantidad_Bruta = nudCantidadBruta.Value,
                Merma = nudMerma.Value,
                Precio_Unitario = nudPrecioUnitario.Value,
                Costo_Base_Asignado = ObtenerMoneda(lblCostoBase.Text),
                Costo_Postcosecha = nudCostoPostcosecha.Value,
                Destino = txtDestino.Text,
                Estado = cmbEstado.SelectedItem?.ToString() ?? string.Empty,
                Observaciones = txtObservaciones.Text,
                Fecha_Venta = vendida ? dtpFechaVenta.Value.Date : null,
                Comprador = vendida ? txtComprador.Text : string.Empty,
                Metodo_Pago = vendida
                    ? cmbMetodoPago.SelectedItem?.ToString() ?? string.Empty
                    : string.Empty,
                Estado_Cobro = vendida
                    ? cmbEstadoCobro.SelectedItem?.ToString() ?? string.Empty
                    : "Pendiente",
                Referencia_Venta = vendida ? txtReferenciaVenta.Text : string.Empty
            };
        }

        private void dgvProduccion_CellDoubleClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            try
            {
                DataGridViewRow fila = dgvProduccion.Rows[e.RowIndex];
                idProduccionSeleccionada = Convert.ToInt32(fila.Cells["ID"].Value);
                int idCosecha = Convert.ToInt32(fila.Cells["ID Cosecha"].Value);
                CargarCosechas(idProduccionSeleccionada, idCosecha);

                dtpFechaProduccion.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
                SeleccionarCombo(cmbUnidad, Texto(fila, "Unidad"));
                nudCantidadBruta.Value = Ajustar(
                    Decimal(fila, "Cantidad bruta"), nudCantidadBruta.Maximum);
                nudMerma.Value = Ajustar(Decimal(fila, "Merma"), nudMerma.Maximum);
                nudPrecioUnitario.Value = Ajustar(
                    Decimal(fila, "Precio unitario"), nudPrecioUnitario.Maximum);
                nudCostoPostcosecha.Value = Ajustar(
                    Decimal(fila, "Costo postcosecha"), nudCostoPostcosecha.Maximum);
                txtDestino.Text = Texto(fila, "Destino");
                SeleccionarCombo(cmbEstado, Texto(fila, "Estado"));
                txtObservaciones.Text = Texto(fila, "Observaciones");

                DateTime? fechaVenta = Fecha(fila, "Fecha venta");
                dtpFechaVenta.Value = fechaVenta ?? DateTime.Today;
                txtComprador.Text = Texto(fila, "Comprador");
                SeleccionarCombo(cmbMetodoPago, Texto(fila, "Metodo"));
                SeleccionarCombo(cmbEstadoCobro, Texto(fila, "Estado cobro"));
                txtReferenciaVenta.Text = Texto(fila, "Referencia venta");

                btnGuardar.Text = "Actualizar";
                btnAnular.Enabled = Texto(fila, "Estado") != "Cancelada";
                btnEliminar.Enabled = true;
                CalcularValores();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex, "abrir la producción seleccionada");
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarEdicion();
        }

        private void LimpiarEdicion()
        {
            idProduccionSeleccionada = 0;
            CargarCosechas();
            dtpFechaProduccion.Value = DateTime.Today;
            dtpFechaVenta.Value = DateTime.Today;
            SeleccionarIndice(cmbUnidad, 0);
            SeleccionarIndice(cmbEstado, 0);
            SeleccionarIndice(cmbMetodoPago, 0);
            SeleccionarIndice(cmbEstadoCobro, 0);
            nudCantidadBruta.Value = 0;
            nudMerma.Value = 0;
            nudPrecioUnitario.Value = 0;
            nudCostoPostcosecha.Value = 0;
            txtDestino.Clear();
            txtObservaciones.Clear();
            txtComprador.Clear();
            txtReferenciaVenta.Clear();
            btnGuardar.Text = "Guardar";
            btnAnular.Enabled = false;
            btnEliminar.Enabled = false;
            tabRegistro.SelectedTab = tabProduccion;
            CalcularValores();
        }

        private void btnAnular_Click(object? sender, EventArgs e)
        {
            if (idProduccionSeleccionada <= 0)
            {
                MostrarError("Seleccione un lote para anular.");
                return;
            }

            if (MessageBox.Show(
                this,
                "¿Desea anular esta producción y revertir sus movimientos financieros?",
                "Confirmar anulación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Entitie_Produccion produccion = CrearDesdeFormulario();
                produccion.ID_Produccion = idProduccionSeleccionada;
                produccion.Estado = "Cancelada";
                BLL_Produccion.Actualizar(produccion);
                MostrarExito("Producción anulada y movimientos financieros revertidos.");
                LimpiarEdicion();
                CargarProducciones();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex, "anular la producción");
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idProduccionSeleccionada <= 0)
            {
                MostrarError("Seleccione un lote para eliminar.");
                return;
            }

            if (MessageBox.Show(
                this,
                "¿Desea eliminar definitivamente el lote seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                BLL_Produccion.Eliminar(idProduccionSeleccionada);
                MostrarExito("Producción eliminada.");
                LimpiarEdicion();
                CargarProducciones();
            }
            catch (Exception ex)
            {
                MostrarExcepcion(ex, "eliminar la producción");
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            CargarProducciones();
        }

        private void btnRecargar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            cmbFiltroEstado.SelectedIndex = 0;
            dtpDesde.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpHasta.Value = DateTime.Today;
            CargarProducciones();
        }

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProducciones();
                e.SuppressKeyPress = true;
            }
        }

        private void dgvProduccion_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            string[] ocultas = { "ID", "ID Cosecha" };
            foreach (string nombre in ocultas)
            {
                if (dgvProduccion.Columns.Contains(nombre))
                {
                    dgvProduccion.Columns[nombre].Visible = false;
                }
            }

            string[] decimales =
            {
                "Cantidad cosechada", "Cantidad bruta", "Merma", "Cantidad neta",
                "Precio unitario", "Valor produccion", "Costo base",
                "Costo postcosecha", "Costo total", "Beneficio neto"
            };
            foreach (string nombre in decimales)
            {
                if (dgvProduccion.Columns.Contains(nombre))
                {
                    dgvProduccion.Columns[nombre].DefaultCellStyle.Format = "N2";
                    dgvProduccion.Columns[nombre].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvProduccion.Columns.Contains("Cultivo"))
                dgvProduccion.Columns["Cultivo"].FillWeight = 125;
            if (dgvProduccion.Columns.Contains("Observaciones"))
                dgvProduccion.Columns["Observaciones"].Visible = false;
            if (dgvProduccion.Columns.Contains("Fecha cosecha"))
                dgvProduccion.Columns["Fecha cosecha"].Visible = false;
        }

        private void ActualizarResumen()
        {
            Entitie_ResumenProduccion resumen =
                BLL_Produccion.CalcularResumen(producciones);
            lblLotesResumen.Text = $"Lotes\n{resumen.Lotes:N0}";
            lblNetaResumen.Text = $"Cantidad neta\n{resumen.CantidadNeta:N3}";
            lblValorResumen.Text = $"Valor\nRD$ {resumen.ValorProduccion:N2}";
            lblBeneficioResumen.Text = $"Beneficio\nRD$ {resumen.Beneficio:N2}";
            lblBeneficioResumen.ForeColor = resumen.Beneficio < 0
                ? Color.FromArgb(176, 58, 46)
                : Color.FromArgb(35, 125, 82);
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void MostrarExcepcion(Exception ex, string accion)
        {
            if (ex is ArgumentException ||
                ex is InvalidOperationException ||
                ex is UnauthorizedAccessException)
            {
                MostrarError(ex.Message);
                return;
            }

            MostrarError(ManejadorErroresUI.ObtenerMensajeSeguro(ex, $"Produccion.{accion}"));
        }

        private void MostrarError(string mensaje)
        {
            lblEstadoFormulario.Text = mensaje;
            lblEstadoFormulario.ForeColor = Color.FromArgb(176, 58, 46);
        }

        private void MostrarExito(string mensaje)
        {
            lblEstadoFormulario.Text = mensaje;
            lblEstadoFormulario.ForeColor = Color.FromArgb(35, 125, 82);
        }

        private static void SeleccionarCombo(ComboBox combo, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                SeleccionarIndice(combo, 0);
                return;
            }

            int indice = combo.FindStringExact(valor);
            combo.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private static void SeleccionarIndice(ComboBox combo, int indice)
        {
            if (combo.Items.Count > indice)
            {
                combo.SelectedIndex = indice;
            }
        }

        private static decimal Ajustar(decimal valor, decimal maximo)
        {
            return Math.Max(0, Math.Min(valor, maximo));
        }

        private static string Texto(DataGridViewRow fila, string columna)
        {
            DataGridView? grid = fila.DataGridView;
            return grid != null && grid.Columns.Contains(columna)
                ? Convert.ToString(fila.Cells[columna].Value) ?? string.Empty
                : string.Empty;
        }

        private static decimal Decimal(DataGridViewRow fila, string columna)
        {
            DataGridView? grid = fila.DataGridView;
            return grid != null &&
                   grid.Columns.Contains(columna) &&
                   fila.Cells[columna].Value != DBNull.Value
                ? Convert.ToDecimal(fila.Cells[columna].Value)
                : 0;
        }

        private static DateTime? Fecha(DataGridViewRow fila, string columna)
        {
            DataGridView? grid = fila.DataGridView;
            if (grid == null ||
                !grid.Columns.Contains(columna) ||
                fila.Cells[columna].Value == null ||
                fila.Cells[columna].Value == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDateTime(fila.Cells[columna].Value);
        }

        private static decimal ObtenerDecimal(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value
                ? Convert.ToDecimal(fila[columna])
                : 0;
        }

        private static DateTime? ObtenerFecha(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value
                ? Convert.ToDateTime(fila[columna])
                : null;
        }

        private static decimal ObtenerMoneda(string texto)
        {
            string limpio = texto.Replace("RD$", string.Empty).Trim();
            return decimal.TryParse(limpio, out decimal valor) ? valor : 0;
        }
    }
}

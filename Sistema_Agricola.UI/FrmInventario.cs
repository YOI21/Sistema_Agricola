using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmInventario : Form
    {
        private int idProductoSeleccionado;
        private string estadoProductoSeleccionado = "Activo";
        private DataTable productosActuales = new DataTable();

        public FrmInventario()
        {
            InitializeComponent();
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {
            ConfigurarCombos();
            ConfigurarGrid(dgvProductos);
            ConfigurarGrid(dgvMovimientos);
            dtpMovDesde.Value = DateTime.Today.AddDays(-90);
            dtpMovHasta.Value = DateTime.Today;
            LimpiarProducto();
            CargarProductos();
            CargarMovimientos();
        }

        private void ConfigurarCombos()
        {
            object[] tipos =
            {
                "Fertilizante", "Semilla", "Herbicida", "Insecticida", "Fungicida",
                "Combustible", "Herramienta", "Equipo menor", "Empaque", "Otro"
            };

            cmbTipo.Items.AddRange(tipos);
            cmbFiltroTipo.Items.Add("Todos");
            cmbFiltroTipo.Items.AddRange(tipos);
            cmbFiltroTipo.SelectedIndex = 0;

            cmbUnidad.Items.AddRange(new object[]
            {
                "Unidad", "Kilogramo", "Gramo", "Litro", "Mililitro",
                "Saco", "Caja", "Paquete", "Galón", "Metro"
            });

            cmbTipoMovimiento.Items.AddRange(new object[] { "Entrada", "Salida" });
            cmbTipo.SelectedIndex = 0;
            cmbUnidad.SelectedIndex = 0;
            cmbTipoMovimiento.SelectedIndex = 0;
        }

        private static void ConfigurarGrid(DataGridView grid)
        {
            grid.AutoGenerateColumns = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 88, 104);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                productosActuales = BLL_Inventario.ListarProductos(
                    txtBuscar.Text,
                    cmbFiltroTipo.SelectedItem?.ToString() ?? "Todos",
                    chkBajoStock.Checked);

                dgvProductos.DataSource = productosActuales;
                FormatearGridProductos();
                CargarComboProductos();

                DataTable todos = BLL_Inventario.ListarProductos(string.Empty, "Todos", false);
                ActualizarResumen(BLL_Inventario.CalcularResumen(todos));
                lblEstado.Text = $"{productosActuales.Rows.Count} productos encontrados";
                lblEstado.ForeColor = Color.FromArgb(80, 90, 100);
            }
            catch (Exception ex)
            {
                productosActuales = new DataTable();
                dgvProductos.DataSource = productosActuales;
                MostrarError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FormatearGridProductos()
        {
            if (dgvProductos.Columns.Contains("ID"))
                dgvProductos.Columns["ID"].Visible = false;

            foreach (string columna in new[]
                     { "Cantidad", "Stock mínimo", "Costo unitario", "Valor stock" })
            {
                if (dgvProductos.Columns.Contains(columna))
                {
                    dgvProductos.Columns[columna].DefaultCellStyle.Format = "N2";
                    dgvProductos.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvProductos.Columns.Contains("Fecha vencimiento"))
                dgvProductos.Columns["Fecha vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void CargarComboProductos()
        {
            DataTable combo = productosActuales.Copy();
            combo.Columns.Add("Producto", typeof(string));
            foreach (DataRow fila in combo.Rows)
                fila["Producto"] = $"{fila["Código"]} - {fila["Nombre"]}";

            cmbProductoMovimiento.DataSource = combo;
            cmbProductoMovimiento.DisplayMember = "Producto";
            cmbProductoMovimiento.ValueMember = "ID";
            cmbProductoMovimiento.SelectedIndex = combo.Rows.Count > 0 ? 0 : -1;
        }

        private void ActualizarResumen(Entitie_ResumenInventario resumen)
        {
            lblProductosValor.Text = resumen.ProductosActivos.ToString("N0");
            lblBajoStockValor.Text = resumen.BajoStock.ToString("N0");
            lblValorInventarioValor.Text = $"RD$ {resumen.ValorInventario:N2}";
            lblVencimientoValor.Text = resumen.PorVencer.ToString("N0");
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
            idProductoSeleccionado = Convert.ToInt32(fila.Cells["ID"].Value);
            estadoProductoSeleccionado =
                Convert.ToString(fila.Cells["Estado"].Value) ?? "Activo";

            txtCodigo.Text = Convert.ToString(fila.Cells["Código"].Value) ?? string.Empty;
            txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value) ?? string.Empty;
            SeleccionarOAgregar(cmbTipo, Convert.ToString(fila.Cells["Tipo"].Value));
            SeleccionarOAgregar(cmbUnidad, Convert.ToString(fila.Cells["Unidad"].Value));
            nudCosto.Value = AjustarDecimal(fila.Cells["Costo unitario"].Value, nudCosto.Maximum);
            nudStockMinimo.Value =
                AjustarDecimal(fila.Cells["Stock mínimo"].Value, nudStockMinimo.Maximum);
            txtUbicacion.Text =
                Convert.ToString(fila.Cells["Ubicación"].Value) ?? string.Empty;

            bool tieneVencimiento = fila.Cells["Fecha vencimiento"].Value != DBNull.Value;
            chkVencimiento.Checked = tieneVencimiento;
            if (tieneVencimiento)
                dtpVencimiento.Value = Convert.ToDateTime(fila.Cells["Fecha vencimiento"].Value);

            nudStockInicial.Value = 0;
            nudStockInicial.Enabled = false;
            lblModoProducto.Text = "Editando producto";
            btnGuardarProducto.Text = "Actualizar";
            btnEstadoProducto.Enabled = true;
            btnEstadoProducto.Text =
                estadoProductoSeleccionado == "Activo" ? "Desactivar" : "Reactivar";
            cmbProductoMovimiento.SelectedValue = idProductoSeleccionado;
        }

        private static void SeleccionarOAgregar(ComboBox combo, string? valor)
        {
            string texto = valor ?? string.Empty;
            int indice = combo.Items.IndexOf(texto);
            if (indice < 0 && !string.IsNullOrWhiteSpace(texto))
            {
                combo.Items.Add(texto);
                indice = combo.Items.Count - 1;
            }

            if (indice >= 0) combo.SelectedIndex = indice;
        }

        private static decimal AjustarDecimal(object valor, decimal maximo)
        {
            decimal numero = valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);
            return Math.Max(0, Math.Min(numero, maximo));
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Entitie_Insumo producto = CrearProductoDesdeFormulario();
                if (idProductoSeleccionado > 0)
                {
                    producto.Id_Insumo = idProductoSeleccionado;
                    BLL_Inventario.ActualizarProducto(producto);
                }
                else
                {
                    BLL_Inventario.RegistrarProducto(producto, nudStockInicial.Value);
                }

                LimpiarProducto();
                CargarProductos();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private Entitie_Insumo CrearProductoDesdeFormulario()
        {
            return new Entitie_Insumo
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Tipo = cmbTipo.SelectedItem?.ToString() ?? string.Empty,
                Unidad_Medida = cmbUnidad.SelectedItem?.ToString() ?? string.Empty,
                Costo = nudCosto.Value,
                Stock_Minimo = nudStockMinimo.Value,
                Ubicacion = txtUbicacion.Text,
                Fecha_Vencimiento = chkVencimiento.Checked
                    ? dtpVencimiento.Value.Date
                    : null
            };
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            LimpiarProducto();
        }

        private void LimpiarProducto()
        {
            idProductoSeleccionado = 0;
            estadoProductoSeleccionado = "Activo";
            txtCodigo.Clear();
            txtNombre.Clear();
            txtUbicacion.Clear();
            nudCosto.Value = 0;
            nudStockMinimo.Value = 0;
            nudStockInicial.Value = 0;
            nudStockInicial.Enabled = true;
            chkVencimiento.Checked = false;
            dtpVencimiento.Value = DateTime.Today.AddMonths(6);
            dtpVencimiento.Enabled = false;

            if (cmbTipo.Items.Count > 0) cmbTipo.SelectedIndex = 0;
            if (cmbUnidad.Items.Count > 0) cmbUnidad.SelectedIndex = 0;

            lblModoProducto.Text = "Nuevo producto";
            btnGuardarProducto.Text = "Guardar";
            btnEstadoProducto.Text = "Desactivar";
            btnEstadoProducto.Enabled = false;
        }

        private void btnEstadoProducto_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado <= 0) return;

            try
            {
                string estado = estadoProductoSeleccionado == "Activo" ? "Inactivo" : "Activo";
                BLL_Inventario.CambiarEstadoProducto(idProductoSeleccionado, estado);
                LimpiarProducto();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void chkVencimiento_CheckedChanged(object sender, EventArgs e)
        {
            dtpVencimiento.Enabled = chkVencimiento.Checked;
        }

        private void cmbTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool entrada = cmbTipoMovimiento.SelectedItem?.ToString() == "Entrada";
            nudCostoMovimiento.Enabled = entrada;
            lblCostoMovimiento.Text = entrada ? "Costo unitario RD$" : "Costo promedio RD$";
            ActualizarCostoMovimiento();
        }

        private void cmbProductoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCostoMovimiento();
        }

        private void ActualizarCostoMovimiento()
        {
            if (cmbProductoMovimiento.SelectedItem is DataRowView fila &&
                fila.Row.Table.Columns.Contains("Costo unitario"))
            {
                nudCostoMovimiento.Value =
                    AjustarDecimal(fila["Costo unitario"], nudCostoMovimiento.Maximum);
            }
        }

        private void btnRegistrarMovimiento_Click(object sender, EventArgs e)
        {
            if (cmbProductoMovimiento.SelectedValue == null)
            {
                MostrarError("Seleccione un producto.");
                return;
            }

            try
            {
                BLL_Inventario.RegistrarMovimiento(new Entitie_MovimientoInventario
                {
                    Id_Insumo = Convert.ToInt32(cmbProductoMovimiento.SelectedValue),
                    Tipo_Movimiento =
                        cmbTipoMovimiento.SelectedItem?.ToString() ?? string.Empty,
                    Cantidad = nudCantidadMovimiento.Value,
                    Costo_Unitario = nudCostoMovimiento.Value,
                    Fecha_Movimiento = dtpFechaMovimiento.Value.Date,
                    Motivo = txtMotivo.Text,
                    Referencia = txtReferenciaMovimiento.Text
                });

                LimpiarMovimiento();
                CargarProductos();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void LimpiarMovimiento()
        {
            nudCantidadMovimiento.Value = 0;
            txtMotivo.Clear();
            txtReferenciaMovimiento.Clear();
            dtpFechaMovimiento.Value = DateTime.Today;
            if (cmbTipoMovimiento.Items.Count > 0) cmbTipoMovimiento.SelectedIndex = 0;
        }

        private void btnCargarMovimientos_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        private void CargarMovimientos()
        {
            try
            {
                int? id = chkSoloProducto.Checked && cmbProductoMovimiento.SelectedValue != null
                    ? Convert.ToInt32(cmbProductoMovimiento.SelectedValue)
                    : null;

                dgvMovimientos.DataSource = BLL_Inventario.ListarMovimientos(
                    id, dtpMovDesde.Value.Date, dtpMovHasta.Value.Date);
                FormatearGridMovimientos();
            }
            catch (Exception ex)
            {
                dgvMovimientos.DataSource = new DataTable();
                MostrarError(ex.Message);
            }
        }

        private void FormatearGridMovimientos()
        {
            if (dgvMovimientos.Columns.Contains("ID"))
                dgvMovimientos.Columns["ID"].Visible = false;
            if (dgvMovimientos.Columns.Contains("ID Insumo"))
                dgvMovimientos.Columns["ID Insumo"].Visible = false;

            foreach (string columna in new[] { "Cantidad", "Costo unitario", "Total" })
            {
                if (dgvMovimientos.Columns.Contains(columna))
                {
                    dgvMovimientos.Columns[columna].DefaultCellStyle.Format = "N2";
                    dgvMovimientos.Columns[columna].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvMovimientos.Columns.Contains("Fecha"))
                dgvMovimientos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void dgvProductos_CellFormatting(
            object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dgvProductos.Columns.Contains("Nivel")) return;
            string nivel = Convert.ToString(
                dgvProductos.Rows[e.RowIndex].Cells["Nivel"].Value) ?? string.Empty;

            if (nivel is "Agotado" or "Bajo")
            {
                dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Color.FromArgb(255, 235, 230);
                dgvProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                    Color.FromArgb(160, 45, 35);
            }
            else if (nivel == "Inactivo")
            {
                dgvProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private void dgvMovimientos_CellFormatting(
            object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dgvMovimientos.Columns.Contains("Movimiento")) return;
            string tipo = Convert.ToString(
                dgvMovimientos.Rows[e.RowIndex].Cells["Movimiento"].Value) ?? string.Empty;
            dgvMovimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                tipo == "Entrada" ? Color.FromArgb(35, 115, 70) : Color.FromArgb(160, 55, 45);
        }

        private void MostrarError(string mensaje)
        {
            lblEstado.Text = mensaje;
            lblEstado.ForeColor = Color.FromArgb(183, 28, 28);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

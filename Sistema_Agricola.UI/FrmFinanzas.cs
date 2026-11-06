using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmFinanzas : Form
    {
        private int idMovimientoSeleccionado;
        private DataTable movimientosActuales = new DataTable();

        public FrmFinanzas()
        {
            InitializeComponent();
        }

        private void FrmFinanzas_Load(object sender, EventArgs e)
        {
            ConfigurarCombos();
            ConfigurarGrid();
            cmbPeriodo.SelectedIndex = 0;
            LimpiarEdicion();
            CargarMovimientos();
        }

        private void ConfigurarCombos()
        {
            cmbPeriodo.Items.Clear();
            cmbPeriodo.Items.AddRange(new object[]
            {
                "Este mes",
                "Mes anterior",
                "Últimos 30 días",
                "Este año",
                "Personalizado"
            });

            cmbTipo.Items.Clear();
            cmbTipo.Items.AddRange(new object[] { "Ingreso", "Gasto" });

            cmbMetodo.Items.Clear();
            cmbMetodo.Items.AddRange(new object[]
            {
                "Efectivo",
                "Transferencia",
                "Tarjeta",
                "Cheque",
                "Otro"
            });

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new object[] { "Pagado", "Pendiente" });

            cmbTipo.SelectedIndex = 1;
            cmbMetodo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            ActualizarCategorias();
        }

        private void ConfigurarGrid()
        {
            dgvMovimientos.AutoGenerateColumns = true;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.EnableHeadersVisualStyles = false;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 88, 104);
            dgvMovimientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvMovimientos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            string periodo = cmbPeriodo.SelectedItem?.ToString() ?? "Este mes";

            switch (periodo)
            {
                case "Mes anterior":
                    DateTime mesAnterior = hoy.AddMonths(-1);
                    dtpDesde.Value = new DateTime(mesAnterior.Year, mesAnterior.Month, 1);
                    dtpHasta.Value = new DateTime(hoy.Year, hoy.Month, 1).AddDays(-1);
                    break;
                case "Últimos 30 días":
                    dtpDesde.Value = hoy.AddDays(-29);
                    dtpHasta.Value = hoy;
                    break;
                case "Este año":
                    dtpDesde.Value = new DateTime(hoy.Year, 1, 1);
                    dtpHasta.Value = hoy;
                    break;
                case "Personalizado":
                    break;
                default:
                    dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
                    dtpHasta.Value = hoy;
                    break;
            }

            bool personalizado = periodo == "Personalizado";
            dtpDesde.Enabled = personalizado;
            dtpHasta.Enabled = personalizado;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCategorias();
        }

        private void ActualizarCategorias()
        {
            string categoriaActual = cmbCategoria.SelectedItem?.ToString() ?? string.Empty;
            cmbCategoria.Items.Clear();

            if (cmbTipo.SelectedItem?.ToString() == "Ingreso")
            {
                cmbCategoria.Items.AddRange(new object[]
                {
                    "Venta de cosecha",
                    "Venta de productos",
                    "Servicio agrícola",
                    "Subsidio",
                    "Préstamo recibido",
                    "Otro ingreso"
                });
            }
            else
            {
                cmbCategoria.Items.AddRange(new object[]
                {
                    "Insumos",
                    "Nómina",
                    "Maquinaria",
                    "Mantenimiento",
                    "Combustible",
                    "Transporte",
                    "Servicios",
                    "Alquiler",
                    "Impuestos",
                    "Otro gasto"
                });
            }

            int indice = cmbCategoria.Items.IndexOf(categoriaActual);
            cmbCategoria.SelectedIndex = indice >= 0 ? indice : 0;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        private void CargarMovimientos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                movimientosActuales = BLL_Finanzas.ListarMovimientos(
                    dtpDesde.Value.Date,
                    dtpHasta.Value.Date,
                    txtBuscar.Text);

                dgvMovimientos.DataSource = movimientosActuales;
                FormatearColumnas();
                ActualizarResumen();
                lblEstado.Text = $"{movimientosActuales.Rows.Count} movimientos encontrados";
                lblEstado.ForeColor = Color.FromArgb(80, 90, 100);
            }
            catch (Exception ex)
            {
                movimientosActuales = new DataTable();
                dgvMovimientos.DataSource = movimientosActuales;
                MostrarError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FormatearColumnas()
        {
            if (dgvMovimientos.Columns.Contains("ID"))
            {
                dgvMovimientos.Columns["ID"].Visible = false;
            }

            if (dgvMovimientos.Columns.Contains("Editable"))
            {
                dgvMovimientos.Columns["Editable"].Visible = false;
            }

            if (dgvMovimientos.Columns.Contains("Monto"))
            {
                dgvMovimientos.Columns["Monto"].DefaultCellStyle.Format = "N2";
                dgvMovimientos.Columns["Monto"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvMovimientos.Columns.Contains("Fecha"))
            {
                dgvMovimientos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvMovimientos.Columns.Contains("Descripción"))
            {
                dgvMovimientos.Columns["Descripción"].FillWeight = 180;
            }
            else if (dgvMovimientos.Columns.Contains("Descripcion"))
            {
                dgvMovimientos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvMovimientos.Columns["Descripcion"].FillWeight = 180;
            }

            if (dgvMovimientos.Columns.Contains("Referencia"))
            {
                dgvMovimientos.Columns["Referencia"].FillWeight = 90;
            }

            if (dgvMovimientos.Columns.Contains("Metodo"))
            {
                dgvMovimientos.Columns["Metodo"].HeaderText = "Método";
            }
        }

        private void ActualizarResumen()
        {
            Entitie_ResumenFinanciero resumen = BLL_Finanzas.CalcularResumen(movimientosActuales);
            lblIngresosValor.Text = FormatearMoneda(resumen.Ingresos);
            lblGastosValor.Text = FormatearMoneda(resumen.Gastos);
            lblBalanceValor.Text = FormatearMoneda(resumen.Balance);
            lblPendientesValor.Text =
                $"Cobrar {FormatearMoneda(resumen.PendienteCobro)}  |  Pagar {FormatearMoneda(resumen.PendientePago)}";

            lblBalanceValor.ForeColor = resumen.Balance < 0
                ? Color.FromArgb(192, 57, 43)
                : Color.FromArgb(35, 115, 70);
        }

        private static string FormatearMoneda(decimal valor)
        {
            return $"RD$ {valor:N2}";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Entitie_MovimientoFinanciero movimiento = CrearMovimientoDesdeFormulario();

                if (idMovimientoSeleccionado > 0)
                {
                    movimiento.ID_Movimiento = idMovimientoSeleccionado;
                    BLL_Finanzas.ActualizarMovimiento(movimiento);
                    lblEstado.Text = "Movimiento actualizado correctamente.";
                }
                else
                {
                    BLL_Finanzas.InsertarMovimiento(movimiento);
                    lblEstado.Text = "Movimiento registrado correctamente.";
                }

                lblEstado.ForeColor = Color.FromArgb(35, 115, 70);
                LimpiarEdicion();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private Entitie_MovimientoFinanciero CrearMovimientoDesdeFormulario()
        {
            return new Entitie_MovimientoFinanciero
            {
                Tipo = cmbTipo.SelectedItem?.ToString() ?? string.Empty,
                Categoria = cmbCategoria.SelectedItem?.ToString() ?? string.Empty,
                Descripcion = txtDescripcion.Text,
                Monto = nudMonto.Value,
                Fecha = dtpFecha.Value.Date,
                Metodo_Pago = cmbMetodo.SelectedItem?.ToString() ?? string.Empty,
                Estado = cmbEstado.SelectedItem?.ToString() ?? string.Empty,
                Referencia = txtReferencia.Text
            };
        }

        private void dgvMovimientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow fila = dgvMovimientos.Rows[e.RowIndex];
            bool editable = fila.Cells["Editable"].Value != DBNull.Value &&
                            Convert.ToBoolean(fila.Cells["Editable"].Value);

            if (!editable)
            {
                MostrarError(
                    "Este movimiento es automático. Debe modificarse desde su módulo de origen.");
                return;
            }

            idMovimientoSeleccionado = Convert.ToInt32(fila.Cells["ID"].Value);
            cmbTipo.SelectedItem = Convert.ToString(fila.Cells["Tipo"].Value);
            ActualizarCategorias();

            string categoria = Convert.ToString(fila.Cells["Categoria"].Value) ?? string.Empty;
            if (!cmbCategoria.Items.Contains(categoria))
            {
                cmbCategoria.Items.Add(categoria);
            }

            cmbCategoria.SelectedItem = categoria;
            txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value) ?? string.Empty;
            nudMonto.Value = AjustarDecimal(fila.Cells["Monto"].Value, nudMonto.Maximum);
            dtpFecha.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
            cmbMetodo.SelectedItem = Convert.ToString(fila.Cells["Metodo"].Value);
            cmbEstado.SelectedItem = Convert.ToString(fila.Cells["Estado"].Value);
            txtReferencia.Text = Convert.ToString(fila.Cells["Referencia"].Value) ?? string.Empty;

            lblModo.Text = "Editando movimiento manual";
            btnGuardar.Text = "Actualizar";
            btnEliminar.Enabled = true;
        }

        private static decimal AjustarDecimal(object valor, decimal maximo)
        {
            decimal numero = valor == DBNull.Value ? 0 : Convert.ToDecimal(valor);
            return Math.Max(0, Math.Min(numero, maximo));
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarEdicion();
        }

        private void LimpiarEdicion()
        {
            idMovimientoSeleccionado = 0;
            txtDescripcion.Clear();
            txtReferencia.Clear();
            nudMonto.Value = 0;
            dtpFecha.Value = DateTime.Today;

            if (cmbTipo.Items.Count > 0)
            {
                cmbTipo.SelectedIndex = 1;
            }

            if (cmbMetodo.Items.Count > 0)
            {
                cmbMetodo.SelectedIndex = 0;
            }

            if (cmbEstado.Items.Count > 0)
            {
                cmbEstado.SelectedIndex = 0;
            }

            lblModo.Text = "Nuevo movimiento manual";
            btnGuardar.Text = "Guardar";
            btnEliminar.Enabled = false;
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMovimientoSeleccionado <= 0)
            {
                return;
            }

            DialogResult confirmar = MessageBox.Show(
                "¿Desea eliminar el movimiento manual seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                BLL_Finanzas.EliminarMovimiento(idMovimientoSeleccionado);
                LimpiarEdicion();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (movimientosActuales.Rows.Count == 0)
            {
                MostrarError("No hay movimientos para exportar.");
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"Finanzas_{dtpDesde.Value:yyyyMMdd}_{dtpHasta.Value:yyyyMMdd}.csv"
            };

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                ExportarCsv(movimientosActuales, dialogo.FileName);
                lblEstado.Text = "Archivo exportado correctamente.";
                lblEstado.ForeColor = Color.FromArgb(35, 115, 70);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo exportar: " + ex.Message);
            }
        }

        private static void ExportarCsv(DataTable tabla, string ruta)
        {
            using StreamWriter writer = new StreamWriter(
                ruta,
                false,
                new UTF8Encoding(true));

            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                if (tabla.Columns[i].ColumnName == "Editable")
                {
                    continue;
                }

                writer.Write(EscaparCsv(tabla.Columns[i].ColumnName));
                writer.Write(';');
            }

            writer.WriteLine();

            foreach (DataRow fila in tabla.Rows)
            {
                foreach (DataColumn columna in tabla.Columns)
                {
                    if (columna.ColumnName == "Editable")
                    {
                        continue;
                    }

                    string valor = fila[columna] == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(fila[columna], CultureInfo.CurrentCulture) ?? string.Empty;
                    writer.Write(EscaparCsv(valor));
                    writer.Write(';');
                }

                writer.WriteLine();
            }
        }

        private static string EscaparCsv(string valor)
        {
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }

        private void dgvMovimientos_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dgvMovimientos.Columns.Contains("Tipo"))
            {
                return;
            }

            string tipo = Convert.ToString(
                dgvMovimientos.Rows[e.RowIndex].Cells["Tipo"].Value) ?? string.Empty;

            if (tipo == "Ingreso")
            {
                dgvMovimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                    Color.FromArgb(35, 115, 70);
            }
            else
            {
                dgvMovimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                    Color.FromArgb(160, 55, 45);
            }
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

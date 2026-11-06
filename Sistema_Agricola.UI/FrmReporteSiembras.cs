using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using System.Drawing.Printing;

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteSiembras : Form
    {
        private DataTable dtSiembras = new DataTable();
        private string filtroActual = "";
        private DataTable? datosImpresion;
        private int indiceFilaImpresion;
        private int numeroPaginaImpresion;
        private bool resumenImpreso;

        public FrmReporteSiembras()
        {
            InitializeComponent();
        }

        private void FrmReporteSiembras_Load(object? sender, EventArgs e)
        {
            try
            {
                // Configurar la apariencia inicial
                ConfigurarComponentes();

                // Cargar datos iniciales
                CargarDatos();

                // Configurar eventos
                ConfigurarEventos();

                // Configurar los filtros disponibles
                CargarFiltros();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmReporteSiembras.Cargar");
            }
        }

        private void ConfigurarComponentes()
        {
            // Configurar el DataGridView
            dgvSiembras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSiembras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSiembras.MultiSelect = false;
            dgvSiembras.ReadOnly = true;
            dgvSiembras.AllowUserToAddRows = false;
            dgvSiembras.AllowUserToDeleteRows = false;
            dgvSiembras.AllowUserToOrderColumns = true;
            dgvSiembras.RowHeadersVisible = false;
            dgvSiembras.BackgroundColor = Color.White;
            dgvSiembras.BorderStyle = BorderStyle.Fixed3D;
            dgvSiembras.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvSiembras.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvSiembras.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSiembras.RowsDefaultCellStyle.SelectionBackColor = Color.LightSeaGreen;

            // Configurar el DateTimePicker de rango de fechas
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-6); // Últimos 6 meses por defecto
            dtpFechaFin.Value = DateTime.Now;

            AutoScroll = true;
            KeyPreview = true;
            AcceptButton = btnFiltrar;
            CancelButton = btnVolver;
            btnFiltrar.AccessibleName = "Aplicar filtros del reporte";
            btnLimpiarFiltros.AccessibleName = "Limpiar filtros del reporte";
            btnExportarExcel.AccessibleName = "Exportar reporte a Excel";
            btnImprimir.AccessibleName = "Imprimir reporte completo";
            btnVolver.AccessibleName = "Cerrar reporte de siembras";
            dgvSiembras.AccessibleName = "Detalle de siembras filtradas";
        }

        private void CargarDatos()
        {
            // Cargar todos los datos de siembras
            dtSiembras = BLL_Siembra.Obtener_Reporte_Siembras();

            // Aplicar filtro inicial (últimos 6 meses)
            AplicarFiltroPorFecha();

            // Actualizar estadísticas
            ActualizarEstadisticas();
        }

        private void ConfigurarEventos()
        {
            // Configurar eventos para filtros y botones
            btnFiltrar.Click += new EventHandler(btnFiltrar_Click);
            btnExportarExcel.Click += new EventHandler(btnExportarExcel_Click);
            btnImprimir.Click += new EventHandler(btnImprimir_Click);
            btnLimpiarFiltros.Click += new EventHandler(btnLimpiarFiltros_Click);
            cmbFiltro.SelectedIndexChanged += new EventHandler(cmbFiltro_SelectedIndexChanged);
            dgvSiembras.CellDoubleClick += new DataGridViewCellEventHandler(dgvSiembras_CellDoubleClick);
        }

        private void dgvSiembras_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            try
            {
                DataGridViewRow fila = dgvSiembras.Rows[e.RowIndex];
                if (fila.Cells["ID_Cultivo"].Value == null)
                {
                    MessageBox.Show("No se pudo identificar la siembra seleccionada.",
                        "Edición de siembra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idCultivo = Convert.ToInt32(fila.Cells["ID_Cultivo"].Value);
                using (FrmSiembra frmSiembra = new FrmSiembra(idCultivo))
                {
                    frmSiembra.ShowDialog();
                }

                CargarDatos();
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmReporteSiembras.AbrirSiembra");
            }
        }

        private void CargarFiltros()
        {
            // Agregar opciones de filtro
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add("Todos");
            cmbFiltro.Items.Add("Tipo de Cultivo");
            cmbFiltro.Items.Add("Categoría");
            cmbFiltro.Items.Add("Estación");
            cmbFiltro.Items.Add("Tipo de Suelo");
            cmbFiltro.Items.Add("Estado");
            cmbFiltro.SelectedIndex = 0; // "Todos" por defecto
        }

        private void cmbFiltro_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Cambiar las opciones disponibles según el filtro seleccionado
            cmbValorFiltro.Items.Clear();
            string filtroSeleccionado = cmbFiltro.SelectedItem?.ToString() ?? "Todos";

            if (filtroSeleccionado == "Todos")
            {
                cmbValorFiltro.Enabled = false;
                return;
            }

            cmbValorFiltro.Enabled = true;

            // Obtener valores únicos para el filtro seleccionado
            DataColumn? columnToFilter = null;

            switch (filtroSeleccionado)
            {
                case "Tipo de Cultivo":
                    columnToFilter = dtSiembras.Columns["Tipo_Cultivo"];
                    break;
                case "Categoría":
                    columnToFilter = dtSiembras.Columns["Categoria_Cultivo"];
                    break;
                case "Estación":
                    columnToFilter = dtSiembras.Columns["Estacion_Siembra"];
                    break;
                case "Tipo de Suelo":
                    columnToFilter = dtSiembras.Columns["Tipo_Suelo"];
                    break;
                case "Estado":
                    columnToFilter = dtSiembras.Columns["Estado_Cultivo"];
                    break;
            }

            if (columnToFilter != null)
            {
                // Obtener valores únicos de la columna
                var uniqueValues = new HashSet<string>();
                foreach (DataRow row in dtSiembras.Rows)
                {
                    string value = Convert.ToString(row[columnToFilter]) ?? string.Empty;
                    if (!string.IsNullOrEmpty(value))
                    {
                        uniqueValues.Add(value);
                    }
                }

                // Agregar los valores únicos al combobox
                foreach (string value in uniqueValues.OrderBy(v => v))
                {
                    cmbValorFiltro.Items.Add(value);
                }

                if (cmbValorFiltro.Items.Count > 0)
                {
                    cmbValorFiltro.SelectedIndex = 0;
                }
            }
        }

        private void btnFiltrar_Click(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            try
            {
                // Construir el filtro
                string filtro = "";

                // Primero aplicar filtro por fecha
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1); // Final del día

                filtro = $"Fecha_Plantacion >= #{fechaInicio.ToString("yyyy-MM-dd")}# AND " +
                         $"Fecha_Plantacion <= #{fechaFin.ToString("yyyy-MM-dd")}#";

                // Luego agregar filtro adicional si se ha seleccionado
                if (cmbFiltro.SelectedIndex > 0 && cmbValorFiltro.SelectedIndex >= 0)
                {
                    string columnaFiltro = "";
                    switch (cmbFiltro.SelectedItem?.ToString())
                    {
                        case "Tipo de Cultivo":
                            columnaFiltro = "Tipo_Cultivo";
                            break;
                        case "Categoría":
                            columnaFiltro = "Categoria_Cultivo";
                            break;
                        case "Estación":
                            columnaFiltro = "Estacion_Siembra";
                            break;
                        case "Tipo de Suelo":
                            columnaFiltro = "Tipo_Suelo";
                            break;
                        case "Estado":
                            columnaFiltro = "Estado_Cultivo";
                            break;
                    }

                    if (!string.IsNullOrEmpty(columnaFiltro))
                    {
                        string valorFiltro = EscaparValorFiltro(cmbValorFiltro.SelectedItem?.ToString());
                        filtro += $" AND {columnaFiltro} = '{valorFiltro}'";
                    }
                }

                // Aplicar búsqueda por texto si se ha ingresado
                if (!string.IsNullOrEmpty(txtBusqueda.Text))
                {
                    string busqueda = EscaparValorFiltro(txtBusqueda.Text.Trim());
                    filtro += $" AND Nombre_Cultivo LIKE '%{busqueda}%'";
                }

                // Guardar el filtro actual para referencias
                filtroActual = filtro;

                // Aplicar el filtro
                DataView dv = new DataView(dtSiembras);
                dv.RowFilter = filtro;
                dgvSiembras.DataSource = dv;

                // Actualizar estadísticas basadas en el filtro
                ActualizarEstadisticas(dv);

                // Colorear filas según el estado
                ColorearFilas();

            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmReporteSiembras.AplicarFiltros");
            }
        }

        private static string EscaparValorFiltro(string? valor)
        {
            return (valor ?? string.Empty)
                .Replace("'", "''", StringComparison.Ordinal)
                .Replace("[", "[[]", StringComparison.Ordinal);
        }

        private void AplicarFiltroPorFecha()
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                string filtro = $"Fecha_Plantacion >= #{fechaInicio.ToString("yyyy-MM-dd")}# AND " +
                               $"Fecha_Plantacion <= #{fechaFin.ToString("yyyy-MM-dd")}#";

                filtroActual = filtro;

                DataView dv = new DataView(dtSiembras);
                dv.RowFilter = filtro;
                dgvSiembras.DataSource = dv;

                ActualizarEstadisticas(dv);
                ColorearFilas();

            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmReporteSiembras.FiltroFecha");
            }
        }

        private void ActualizarEstadisticas()
        {
            DataView? dv = dgvSiembras.DataSource as DataView;
            if (dv != null)
            {
                ActualizarEstadisticas(dv);
            }
        }

        private void ActualizarEstadisticas(DataView dv)
        {
            try
            {
                ResumenReporteSiembras resumen = CalculosReporteSiembras.Calcular(dv.ToTable());
                lblTotalRegistros.Text = $"Total: {resumen.TotalSiembras} siembras";
                lblEnCrecimiento.Text = $"En crecimiento: {resumen.EnCrecimiento}";
                lblCosechadas.Text = $"Cosechadas: {resumen.Cosechadas}";

                string tipos = resumen.TiposCultivo.Count == 0
                    ? "N/D"
                    : string.Join(", ", resumen.TiposCultivo.Take(3)) +
                      (resumen.TiposCultivo.Count > 3 ? "..." : string.Empty);
                lblTiposCultivo.Text = $"Tipos: {tipos}";
                lblPromedioCiclo.Text = resumen.PromedioDiasCiclo > 0
                    ? $"Ciclo promedio: {resumen.PromedioDiasCiclo:F1} días"
                    : "Ciclo promedio: N/D";
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.ObtenerReferencia(ex, "FrmReporteSiembras.Estadisticas");
            }
        }

        private void ColorearFilas()
        {
            try
            {
                // Colorear filas según estado
                foreach (DataGridViewRow row in dgvSiembras.Rows)
                {
                    string estado = row.Cells["Estado_Cultivo"]?.Value?.ToString() ?? "";

                    if (estado.Equals("Cosechado", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (estado.Equals("En Riesgo", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }

                    // Verificar si está próximo a la cosecha (dentro de 10 días)
                    if (row.Cells["Fecha_Cosecha"].Value != null)
                    {
                        DateTime fechaCosecha = Convert.ToDateTime(row.Cells["Fecha_Cosecha"].Value);
                        TimeSpan diferencia = fechaCosecha - DateTime.Now;

                        if (diferencia.TotalDays <= 10 && diferencia.TotalDays > 0)
                        {
                            row.DefaultCellStyle.ForeColor = Color.Blue;
                            row.DefaultCellStyle.Font = new Font(dgvSiembras.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.ObtenerReferencia(ex, "FrmReporteSiembras.ColorearFilas");
            }
        }

        private void btnLimpiarFiltros_Click(object? sender, EventArgs e)
        {
            // Resetear filtros
            cmbFiltro.SelectedIndex = 0;
            txtBusqueda.Clear();
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-6);
            dtpFechaFin.Value = DateTime.Now;

            // Aplicar el filtro por fecha
            AplicarFiltroPorFecha();
        }

        private void btnExportarExcel_Click(object? sender, EventArgs e)
        {
            DataView? vista = dgvSiembras.DataSource as DataView;
            if (vista == null || vista.Count == 0)
            {
                MessageBox.Show(this, "No hay siembras filtradas para exportar.",
                    "Reporte de siembras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Libro de Excel (*.xlsx)|*.xlsx",
                    Title = "Guardar reporte de siembras",
                    FileName = $"Reporte_Siembras_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                    DefaultExt = "xlsx",
                    AddExtension = true,
                    OverwritePrompt = true
                };

                if (saveDialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                UseWaitCursor = true;
                ExportadorReporteSiembrasExcel.Exportar(
                    vista.ToTable(), saveDialog.FileName, ObtenerDescripcionFiltros());

                DialogResult abrir = MessageBox.Show(this,
                    "El libro de Excel se generó correctamente. ¿Desea abrirlo?",
                    "Exportación completada", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (abrir == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(saveDialog.FileName)
                    {
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "exportar el reporte de siembras");
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private string ObtenerDescripcionFiltros()
        {
            List<string> filtros = new List<string>
            {
                $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta {dtpFechaFin.Value:dd/MM/yyyy}"
            };

            if (cmbFiltro.SelectedIndex > 0 && cmbValorFiltro.SelectedIndex >= 0)
            {
                filtros.Add($"{cmbFiltro.SelectedItem}: {cmbValorFiltro.SelectedItem}");
            }

            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                filtros.Add($"Búsqueda: {txtBusqueda.Text.Trim()}");
            }

            return string.Join(" | ", filtros);
        }

        private void btnImprimir_Click(object? sender, EventArgs e)
        {
            DataView? vista = dgvSiembras.DataSource as DataView;
            if (vista == null || vista.Count == 0)
            {
                MessageBox.Show(this, "No hay siembras filtradas para imprimir.",
                    "Reporte de siembras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using PrintDocument documento = new PrintDocument
                {
                    DocumentName = $"Reporte de siembras {DateTime.Now:yyyy-MM-dd}"
                };
                documento.DefaultPageSettings.Landscape = true;
                documento.BeginPrint += PrepararImpresion;
                documento.PrintPage += ImprimirReporte;
                documento.EndPrint += FinalizarImpresion;

                using PrintDialog dialogo = new PrintDialog
                {
                    Document = documento,
                    UseEXDialog = true,
                    AllowSomePages = false
                };

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    documento.Print();
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "imprimir el reporte de siembras");
            }
        }

        private void PrepararImpresion(object? sender, PrintEventArgs e)
        {
            datosImpresion = (dgvSiembras.DataSource as DataView)?.ToTable() ?? new DataTable();
            indiceFilaImpresion = 0;
            numeroPaginaImpresion = 0;
            resumenImpreso = false;
        }

        private void FinalizarImpresion(object? sender, PrintEventArgs e)
        {
            datosImpresion?.Dispose();
            datosImpresion = null;
            indiceFilaImpresion = 0;
            numeroPaginaImpresion = 0;
            resumenImpreso = false;
        }

        private void ImprimirReporte(object? sender, PrintPageEventArgs e)
        {
            if (datosImpresion == null || e.Graphics == null)
            {
                e.HasMorePages = false;
                return;
            }

            numeroPaginaImpresion++;
            using Font fuenteTitulo = new Font("Segoe UI", 16F, FontStyle.Bold);
            using Font fuenteSubtitulo = new Font("Segoe UI", 9F, FontStyle.Bold);
            using Font fuenteTexto = new Font("Segoe UI", 8F);
            using Font fuentePie = new Font("Segoe UI", 8F, FontStyle.Italic);
            using StringFormat formatoCelda = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.LineLimit
            };

            Rectangle limites = e.MarginBounds;
            float y = limites.Top;
            float pieY = limites.Bottom - 24;

            e.Graphics.DrawString("REPORTE DE SIEMBRAS", fuenteTitulo, Brushes.Black,
                limites.Left, y);
            e.Graphics.DrawString($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteTexto,
                Brushes.Black, limites.Right - 190, y + 5);
            y += 30;

            RectangleF rectFiltro = new RectangleF(limites.Left, y, limites.Width, 32);
            e.Graphics.DrawString(ObtenerDescripcionFiltros(), fuenteTexto, Brushes.DimGray,
                rectFiltro, formatoCelda);
            y += 34;

            ResumenReporteSiembras resumen = CalculosReporteSiembras.Calcular(datosImpresion);
            string indicadores =
                $"Siembras: {resumen.TotalSiembras}   En crecimiento: {resumen.EnCrecimiento}   " +
                $"Cosechadas: {resumen.Cosechadas}   Hectáreas: {resumen.HectareasSembradas:N2}";
            e.Graphics.DrawString(indicadores, fuenteSubtitulo, Brushes.Black, limites.Left, y);
            y += 24;

            (string Titulo, string Campo, float Proporcion)[] columnas =
            {
                ("ID", "ID_Cultivo", 0.07F),
                ("Cultivo", "Nombre_Cultivo", 0.25F),
                ("Tipo", "Tipo_Cultivo", 0.17F),
                ("Fecha siembra", "Fecha_Plantacion", 0.17F),
                ("Fecha cosecha", "Fecha_Cosecha", 0.17F),
                ("Estado", "Estado_Cultivo", 0.17F)
            };

            const float altoEncabezado = 28;
            float x = limites.Left;
            foreach ((string titulo, string _, float proporcion) in columnas)
            {
                float ancho = limites.Width * proporcion;
                RectangleF celda = new RectangleF(x, y, ancho, altoEncabezado);
                e.Graphics.FillRectangle(Brushes.DarkGreen, celda);
                e.Graphics.DrawRectangle(Pens.White, celda.X, celda.Y, celda.Width, celda.Height);
                e.Graphics.DrawString(titulo, fuenteSubtitulo, Brushes.White, celda, formatoCelda);
                x += ancho;
            }
            y += altoEncabezado;

            const float altoFila = 25;
            while (indiceFilaImpresion < datosImpresion.Rows.Count && y + altoFila <= pieY)
            {
                DataRow fila = datosImpresion.Rows[indiceFilaImpresion];
                x = limites.Left;
                for (int i = 0; i < columnas.Length; i++)
                {
                    float ancho = limites.Width * columnas[i].Proporcion;
                    RectangleF celda = new RectangleF(x, y, ancho, altoFila);
                    if (indiceFilaImpresion % 2 != 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Honeydew, celda);
                    }
                    e.Graphics.DrawRectangle(Pens.LightGray, celda.X, celda.Y, celda.Width, celda.Height);
                    string texto = FormatearValorImpresion(fila, columnas[i].Campo);
                    e.Graphics.DrawString(texto, fuenteTexto, Brushes.Black, celda, formatoCelda);
                    x += ancho;
                }

                indiceFilaImpresion++;
                y += altoFila;
            }

            bool quedanFilas = indiceFilaImpresion < datosImpresion.Rows.Count;
            if (!quedanFilas && !resumenImpreso)
            {
                const float altoResumen = 48;
                if (y + altoResumen <= pieY)
                {
                    y += 8;
                    string totales =
                        $"TOTALES: cantidad {resumen.CantidadSembrada:N2} | " +
                        $"hectáreas {resumen.HectareasSembradas:N2} | " +
                        $"costo siembra {resumen.CostoSiembra:C2} | " +
                        $"costo fertilización {resumen.CostoFertilizacion:C2}";
                    e.Graphics.DrawString(totales, fuenteSubtitulo, Brushes.Black,
                        new RectangleF(limites.Left, y, limites.Width, altoResumen), formatoCelda);
                    resumenImpreso = true;
                }
            }

            e.Graphics.DrawString("Sistema Agrícola - Reporte de siembras", fuentePie,
                Brushes.DimGray, limites.Left, pieY + 5);
            string numeroPagina = $"Página {numeroPaginaImpresion}";
            SizeF tamanoPagina = e.Graphics.MeasureString(numeroPagina, fuentePie);
            e.Graphics.DrawString(numeroPagina, fuentePie, Brushes.DimGray,
                limites.Right - tamanoPagina.Width, pieY + 5);

            e.HasMorePages = quedanFilas || !resumenImpreso;
        }

        private static string FormatearValorImpresion(DataRow fila, string columna)
        {
            if (!fila.Table.Columns.Contains(columna) || fila.IsNull(columna))
            {
                return string.Empty;
            }

            object valor = fila[columna];
            return valor is DateTime fecha
                ? fecha.ToString("dd/MM/yyyy")
                : Convert.ToString(valor) ?? string.Empty;
        }
        private void btnVolver_Click(object? sender, EventArgs e)
        {
            // Preguntar si hay cambios sin guardar (si aplica)
            DialogResult result = MessageBox.Show(
                "¿Está seguro que desea cerrar el informe de siembras?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

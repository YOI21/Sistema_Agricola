using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteInsumos : Form
    {
        private void ConstruirInterfaz()
        {
            Text = "Reporte de Insumos";
            FormBorderStyle = FormBorderStyle.None;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            KeyPreview = true;
            ClientSize = new Size(1180, 720);
            MinimumSize = new Size(820, 480);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 250);

            TableLayoutPanel contenido = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(14),
                BackColor = BackColor
            };

            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            contenido.Controls.Add(CrearEncabezado(), 0, 0);
            contenido.Controls.Add(CrearPanelFiltros(), 0, 1);
            contenido.Controls.Add(CrearPanelResumen(), 0, 2);
            contenido.Controls.Add(CrearPanelPrincipal(), 0, 3);
            contenido.Controls.Add(CrearPanelAcciones(), 0, 4);
            Controls.Add(contenido);
            ConfigurarNavegacion();
        }

        private Control CrearEncabezado()
        {
            Panel encabezado = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(38, 92, 74)
            };

            Label titulo = new Label
            {
                Text = "Reporte de Insumos",
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 14)
            };

            Button cerrar = CrearBoton("X", Color.FromArgb(176, 58, 46), 36);
            cerrar.Name = "btnCerrarReporteInsumos";
            cerrar.AccessibleName = "Cerrar reporte de insumos";
            cerrar.TabIndex = 0;
            cerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cerrar.Location = new Point(ClientSize.Width - 52, 11);
            cerrar.Click += (_, _) => Close();
            encabezado.Resize += (_, _) =>
                cerrar.Location = new Point(encabezado.Width - cerrar.Width - 14, 11);

            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(cerrar);
            return encabezado;
        }

        private Control CrearPanelFiltros()
        {
            Panel panel = CrearPanelBase();
            FlowLayoutPanel filtros = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 10, 12, 6),
                WrapContents = true
            };

            ConfigurarCombo(cmbTipo);
            ConfigurarCombo(cmbNivel);
            ConfigurarCombo(cmbVistaGrafico);
            ConfigurarFecha(dtpDesde);
            ConfigurarFecha(dtpHasta);

            txtBuscar.Width = 190;
            txtBuscar.PlaceholderText = "Codigo, nombre o ubicacion";

            cmbTipo.Items.Add("Todos");
            cmbNivel.Items.AddRange(new object[] { "Todos", "Normal", "Bajo", "Agotado", "Inactivo" });
            cmbVistaGrafico.Items.AddRange(new object[]
            {
                "Valor por tipo",
                "Stock por nivel",
                "Movimientos por producto"
            });
            cmbTipo.SelectedIndex = 0;
            cmbNivel.SelectedIndex = 0;
            cmbVistaGrafico.SelectedIndex = 0;

            chkSoloActivos.Text = "Solo activos";
            chkSoloActivos.Checked = true;
            chkSoloActivos.AutoSize = true;
            chkSoloActivos.Margin = new Padding(8, 27, 8, 0);

            chkSoloBajoStock.Text = "Bajo stock";
            chkSoloBajoStock.AutoSize = true;
            chkSoloBajoStock.Margin = new Padding(8, 27, 8, 0);

            chkConMovimientos.Text = "Movimientos";
            chkConMovimientos.Checked = true;
            chkConMovimientos.AutoSize = true;
            chkConMovimientos.Margin = new Padding(8, 27, 8, 0);

            Button btnBuscar = CrearBoton("Buscar", Color.FromArgb(45, 125, 94), 88);
            Button btnLimpiar = CrearBoton("Limpiar", Color.FromArgb(95, 106, 106), 88);
            Button btnRecargar = CrearBoton("Recargar", Color.FromArgb(52, 101, 164), 92);

            btnBuscar.Name = "btnBuscarInsumos";
            btnBuscar.AccessibleName = "Buscar insumos";
            btnBuscar.TabIndex = 9;
            btnLimpiar.Name = "btnLimpiarFiltrosInsumos";
            btnLimpiar.AccessibleName = "Limpiar filtros de insumos";
            btnLimpiar.TabIndex = 10;
            btnRecargar.Name = "btnRecargarInsumos";
            btnRecargar.AccessibleName = "Recargar insumos";
            btnRecargar.TabIndex = 11;

            btnBuscar.Click += (_, _) => AplicarFiltros();
            btnLimpiar.Click += (_, _) => LimpiarFiltros();
            btnRecargar.Click += (_, _) => CargarDatos();

            filtros.Controls.Add(CrearCampo("Buscar", txtBuscar, 210));
            filtros.Controls.Add(CrearCampo("Tipo", cmbTipo, 148));
            filtros.Controls.Add(CrearCampo("Nivel", cmbNivel, 124));
            filtros.Controls.Add(CrearCampo("Grafico", cmbVistaGrafico, 170));
            filtros.Controls.Add(chkSoloActivos);
            filtros.Controls.Add(chkSoloBajoStock);
            filtros.Controls.Add(chkConMovimientos);
            filtros.Controls.Add(CrearCampo("Desde", dtpDesde, 126));
            filtros.Controls.Add(CrearCampo("Hasta", dtpHasta, 126));
            filtros.Controls.Add(btnBuscar);
            filtros.Controls.Add(btnLimpiar);
            filtros.Controls.Add(btnRecargar);
            panel.Controls.Add(filtros);
            return panel;
        }

        private Control CrearPanelResumen()
        {
            TableLayoutPanel resumen = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 1,
                BackColor = BackColor,
                Padding = new Padding(0, 6, 0, 8)
            };

            for (int i = 0; i < 6; i++)
            {
                resumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            }

            resumen.Controls.Add(CrearTarjeta("Productos activos", lblProductos), 0, 0);
            resumen.Controls.Add(CrearTarjeta("Bajo stock", lblBajoStock), 1, 0);
            resumen.Controls.Add(CrearTarjeta("Valor inventario", lblValorInventario), 2, 0);
            resumen.Controls.Add(CrearTarjeta("Por vencer", lblPorVencer), 3, 0);
            resumen.Controls.Add(CrearTarjeta("Entradas", lblEntradas), 4, 0);
            resumen.Controls.Add(CrearTarjeta("Salidas", lblSalidas), 5, 0);
            return resumen;
        }

        private Control CrearPanelPrincipal()
        {
            TableLayoutPanel principal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = BackColor
            };
            principal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            principal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));

            Panel panelTabla = CrearPanelBase();
            panelTabla.Padding = new Padding(8);

            tabControl.Dock = DockStyle.Fill;
            TabPage tabProductos = new TabPage("Inventario");
            TabPage tabMovimientos = new TabPage("Movimientos");
            ConfigurarGrid(dgvInsumos);
            ConfigurarGrid(dgvMovimientos);
            tabProductos.Controls.Add(dgvInsumos);
            tabMovimientos.Controls.Add(dgvMovimientos);
            tabControl.TabPages.Add(tabProductos);
            tabControl.TabPages.Add(tabMovimientos);
            panelTabla.Controls.Add(tabControl);

            Panel panelGrafico = CrearPanelBase();
            panelGrafico.Padding = new Padding(10);
            lblDetalleGrafico.Dock = DockStyle.Bottom;
            lblDetalleGrafico.Height = 42;
            lblDetalleGrafico.ForeColor = Color.FromArgb(72, 83, 96);
            lblDetalleGrafico.TextAlign = ContentAlignment.MiddleLeft;
            lblDetalleGrafico.Font = new Font("Segoe UI", 8.5F);
            ConfigurarGrafico();
            panelGrafico.Controls.Add(chartInsumos);
            panelGrafico.Controls.Add(lblDetalleGrafico);

            principal.Controls.Add(panelTabla, 0, 0);
            principal.Controls.Add(panelGrafico, 1, 0);
            return principal;
        }

        private Control CrearPanelAcciones()
        {
            Panel acciones = new Panel { Dock = DockStyle.Fill, BackColor = BackColor };

            lblRegistros.AutoSize = true;
            lblRegistros.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblRegistros.ForeColor = Color.FromArgb(62, 73, 88);
            lblRegistros.Location = new Point(2, 13);

            Button btnExportar = CrearBoton("Exportar CSV", Color.FromArgb(45, 125, 94), 116);
            Button btnImprimir = CrearBoton("Imprimir", Color.FromArgb(52, 101, 164), 96);
            Button btnVolver = CrearBoton("Volver", Color.FromArgb(176, 58, 46), 86);

            btnExportar.Name = "btnExportarInsumos";
            btnExportar.AccessibleName = "Exportar reporte de insumos";
            btnExportar.TabIndex = 0;
            btnImprimir.Name = "btnImprimirInsumos";
            btnImprimir.AccessibleName = "Imprimir reporte de insumos";
            btnImprimir.TabIndex = 1;
            btnVolver.Name = "btnVolverInsumos";
            btnVolver.AccessibleName = "Volver";
            btnVolver.TabIndex = 2;

            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImprimir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnVolver.Click += (_, _) => Close();
            btnExportar.Click += (_, _) => ExportarCsv();
            btnImprimir.Click += (_, _) => MostrarVistaImpresion();

            acciones.Resize += (_, _) =>
            {
                btnVolver.Location = new Point(acciones.Width - btnVolver.Width, 5);
                btnImprimir.Location = new Point(btnVolver.Left - btnImprimir.Width - 8, 5);
                btnExportar.Location = new Point(btnImprimir.Left - btnExportar.Width - 8, 5);
            };

            acciones.Controls.Add(lblRegistros);
            acciones.Controls.Add(btnExportar);
            acciones.Controls.Add(btnImprimir);
            acciones.Controls.Add(btnVolver);
            return acciones;
        }

        private void ConfigurarNavegacion()
        {
            txtBuscar.Name = "txtBuscarInsumos";
            txtBuscar.AccessibleName = "Buscar insumos";
            txtBuscar.TabIndex = 0;
            cmbTipo.Name = "cmbTipoInsumo";
            cmbTipo.AccessibleName = "Tipo de insumo";
            cmbTipo.TabIndex = 1;
            cmbNivel.Name = "cmbNivelInventario";
            cmbNivel.AccessibleName = "Nivel de inventario";
            cmbNivel.TabIndex = 2;
            cmbVistaGrafico.Name = "cmbVistaGraficoInsumos";
            cmbVistaGrafico.AccessibleName = "Vista del gráfico";
            cmbVistaGrafico.TabIndex = 3;
            chkSoloActivos.TabIndex = 4;
            chkSoloBajoStock.TabIndex = 5;
            chkConMovimientos.TabIndex = 6;
            dtpDesde.AccessibleName = "Fecha inicial";
            dtpDesde.TabIndex = 7;
            dtpHasta.AccessibleName = "Fecha final";
            dtpHasta.TabIndex = 8;
            tabControl.AccessibleName = "Datos del reporte de insumos";
            tabControl.TabIndex = 12;
            dgvInsumos.AccessibleName = "Inventario de insumos";
            dgvInsumos.TabIndex = 0;
            dgvMovimientos.AccessibleName = "Movimientos de insumos";
            dgvMovimientos.TabIndex = 0;
        }

    }
}

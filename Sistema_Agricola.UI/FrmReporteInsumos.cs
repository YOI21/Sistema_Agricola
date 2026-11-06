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
        private readonly TextBox txtBuscar = new TextBox();
        private readonly ComboBox cmbTipo = new ComboBox();
        private readonly ComboBox cmbNivel = new ComboBox();
        private readonly ComboBox cmbVistaGrafico = new ComboBox();
        private readonly CheckBox chkSoloBajoStock = new CheckBox();
        private readonly CheckBox chkSoloActivos = new CheckBox();
        private readonly CheckBox chkConMovimientos = new CheckBox();
        private readonly DateTimePicker dtpDesde = new DateTimePicker();
        private readonly DateTimePicker dtpHasta = new DateTimePicker();
        private readonly Label lblProductos = new Label();
        private readonly Label lblBajoStock = new Label();
        private readonly Label lblValorInventario = new Label();
        private readonly Label lblPorVencer = new Label();
        private readonly Label lblEntradas = new Label();
        private readonly Label lblSalidas = new Label();
        private readonly Label lblRegistros = new Label();
        private readonly Label lblDetalleGrafico = new Label();
        private readonly DataGridView dgvInsumos = new DataGridView();
        private readonly DataGridView dgvMovimientos = new DataGridView();
        private readonly Chart chartInsumos = new Chart();
        private readonly TabControl tabControl = new TabControl();

        private DataTable todosLosProductos = new DataTable();
        private DataTable productosFiltrados = new DataTable();
        private DataTable movimientosFiltrados = new DataTable();
        private int filaImpresion;
        private readonly bool omitirCargaParaPruebaVisual;

        public FrmReporteInsumos() : this(false)
        {
        }

        internal FrmReporteInsumos(bool omitirCargaParaPruebaVisual)
        {
            this.omitirCargaParaPruebaVisual = omitirCargaParaPruebaVisual;
            InitializeComponent();
            ConstruirInterfaz();
            ConfigurarEventos();
            Load += FrmReporteInsumos_Load;
        }

        private void FrmReporteInsumos_Load(object? sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-90);
            dtpHasta.Value = DateTime.Today;
            if (omitirCargaParaPruebaVisual)
            {
                return;
            }

            CargarDatos();
        }

        private void ConfigurarEventos()
        {
            txtBuscar.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AplicarFiltros();
                    e.SuppressKeyPress = true;
                }
            };
            chkSoloActivos.CheckedChanged += (_, _) => AplicarFiltros();
            chkSoloBajoStock.CheckedChanged += (_, _) => AplicarFiltros();
            chkConMovimientos.CheckedChanged += (_, _) => AplicarFiltros();
            cmbTipo.SelectedIndexChanged += (_, _) => AplicarFiltros();
            cmbNivel.SelectedIndexChanged += (_, _) => AplicarFiltros();
            cmbVistaGrafico.SelectedIndexChanged += (_, _) => ActualizarGrafico();
            dtpDesde.ValueChanged += (_, _) => CargarMovimientosYResumen();
            dtpHasta.ValueChanged += (_, _) => CargarMovimientosYResumen();
        }

        private void LimpiarFiltros()
        {
            txtBuscar.Clear();
            if (cmbTipo.Items.Count > 0) cmbTipo.SelectedIndex = 0;
            if (cmbNivel.Items.Count > 0) cmbNivel.SelectedIndex = 0;
            chkSoloActivos.Checked = true;
            chkSoloBajoStock.Checked = false;
            chkConMovimientos.Checked = true;
            dtpDesde.Value = DateTime.Today.AddDays(-90);
            dtpHasta.Value = DateTime.Today;
            AplicarFiltros();
        }

    }
}

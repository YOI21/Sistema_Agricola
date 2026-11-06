using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmSiembra : Form
    {
        private EvaluacionAgricola? evaluacionActual;
        private bool esModoEdicion = false;
        private int idCultivoEdicion = -1;
        private readonly bool omitirCargaParaPruebaVisual;

        private class SugerenciaCultivo
        {
            public string Nombre { get; set; } = string.Empty;
            public string TipoCultivo { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public string MejorSiembra { get; set; } = string.Empty;
            public string MejorCosecha { get; set; } = string.Empty;
            public int DuracionMeses { get; set; }
            public NivelRecomendacionAgricola Nivel { get; set; }
            public int Prioridad { get; set; }
            public string Motivo { get; set; } = string.Empty;
            public bool BuenaTemporada { get; set; }

            public override string ToString()
            {
                string ciclo = DuracionMeses > 0 ? $"{DuracionMeses} meses" : "ciclo variable";
                string temporada = BuenaTemporada ? "temporada actual favorable" : $"siembra: {MejorSiembra}";
                string nivel = Nivel switch
                {
                    NivelRecomendacionAgricola.Recomendado => "Recomendado",
                    NivelRecomendacionAgricola.Condicionado => "Condicionado",
                    _ => "Revisar"
                };
                return $"{Nombre} - {nivel} - {TipoCultivo} - {ciclo} - {temporada}";
            }
        }

        private class TerrenoSiembraItem
        {
            public int IdTerreno { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string TipoSuelo { get; set; } = string.Empty;
            public string Provincia { get; set; } = string.Empty;
            public string ZonaAgroclimatica { get; set; } = string.Empty;
            public decimal PH { get; set; }
            public decimal Humedad { get; set; }
            public string Drenaje { get; set; } = string.Empty;
            public decimal Pendiente { get; set; }
            public decimal PrecipitacionAnual { get; set; }
            public bool DisponibilidadRiego { get; set; }
            public string FuenteAgua { get; set; } = string.Empty;
            public decimal AreaHectareas { get; set; }
            public decimal HectareasOcupadas { get; set; }
            public decimal HectareasDisponibles => Math.Max(0, AreaHectareas - HectareasOcupadas);

            public override string ToString()
            {
                return $"{Nombre} - {HectareasDisponibles:N2} ha disponibles";
            }
        }

        public FrmSiembra() : this(false)
        {
        }

        internal FrmSiembra(bool omitirCargaParaPruebaVisual)
        {
            this.omitirCargaParaPruebaVisual = omitirCargaParaPruebaVisual;
            InitializeComponent();

            MinimumSize = new Size(820, 480);
            AutoScroll = true;

            ConfigurarTiposAutomaticos();
            this.btnActualizar.Click += btnGuardar_Click;
            ConfigurarAccesibilidad();
        }

        private void ConfigurarAccesibilidad()
        {
            txtIDSiembra.AccessibleName = "ID de siembra";
            dtpFechadeSiembra.AccessibleName = "Fecha de siembra";
            cbMetododeSiem.AccessibleName = "Método de siembra";
            cmbTerrenoSiembra.AccessibleName = "Terreno de siembra";
            txtHectareasSiembra.AccessibleName = "Hectáreas que se sembrarán";
            cmbCultivosDisponibles.AccessibleName = "Cultivo disponible";
            txtCultivo.AccessibleName = "Nombre del cultivo";
            cmbTipoCultivo.AccessibleName = "Tipo de cultivo";
            cmbCategoriaCultivo.AccessibleName = "Categoría del cultivo";
            cmbEstaciones.AccessibleName = "Temporada de siembra";
            cmbTipoSuelo.AccessibleName = "Tipo de suelo";
            btnGuardar.AccessibleName = "Guardar siembra";
            btnActualizar.AccessibleName = "Actualizar siembra";
            btnLimpiar.AccessibleName = "Limpiar formulario";
            btnVolver.AccessibleName = "Cerrar gestión de siembras";
        }

        public FrmSiembra(int idCultivo) : this()
        {
            esModoEdicion = true;
            idCultivoEdicion = idCultivo;
            this.Shown += FrmSiembra_Shown;
        }

        private void FrmSiembra_Shown(object? sender, EventArgs e)
        {
            if (esModoEdicion && idCultivoEdicion > 0)
            {
                CargarSiembraParaEdicion(idCultivoEdicion);
            }
        }

        public void CargarSiembraParaEdicion(int idCultivo)
        {
            try
            {
                Entitie_Siembra? siembra = BLL_Siembra.Obtener_Cultivo(idCultivo);
                if (siembra == null || siembra.ID_Cultivo <= 0)
                {
                    MessageBox.Show("No se encontró la siembra seleccionada.",
                        "Siembra no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                esModoEdicion = true;
                idCultivoEdicion = siembra.ID_Cultivo;

                txtIDSiembra.Text = siembra.ID_Cultivo.ToString();
                txtCultivo.Text = siembra.Nombre_Cultivo;
                txtVariedadCultivo.Text = siembra.Variedad_Cultivo;
                SeleccionarValorCombo(cmbTipoCultivo, siembra.Tipo_Cultivo);
                SeleccionarValorCombo(cmbCategoriaCultivo, siembra.Categoria_Cultivo);
                SeleccionarValorCombo(cmbEstaciones, siembra.Estacion_Siembra);
                SeleccionarValorCombo(cmbTipoSuelo, siembra.Tipo_Suelo);
                SeleccionarValorCombo(cbMetododeSiem, siembra.Metodo_Siembra);
                SeleccionarValorCombo(comboBox2, siembra.Tipo_Cultivo);
                AplicarDatosDerivadosDelCultivo(siembra.Nombre_Cultivo);
                textBox3.Text = siembra.Cantidad.ToString();
                textBox2.Text = siembra.Profundidad;
                textBox4.Text = siembra.Costo_Siembra > 0 ? siembra.Costo_Siembra.ToString("0.##") : "";
                txtHectareasSiembra.Text = siembra.Hectareas_Sembradas > 0 ? siembra.Hectareas_Sembradas.ToString("0.##") : "";
                CargarTerrenosParaSiembra();
                SeleccionarTerrenoSiembra(siembra.ID_Terreno);
                dtpFechadeSiembra.Value = siembra.Fecha_Plantacion;

                if (dtpFechaCosecha != null)
                {
                    dtpFechaCosecha.Value = siembra.Fecha_Cosecha;
                }

                lblTitulo.Text = "EDICIÓN DE SIEMBRA";
                btnGuardar.Text = "Guardar cambios";
                btnActualizar.Enabled = true;
                tabControl.SelectedTab = tabDatos;
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.CargarEdicion");
            }
        }

        private void SeleccionarValorCombo(ComboBox combo, string valor)
        {
            if (combo == null || string.IsNullOrWhiteSpace(valor))
            {
                return;
            }

            int index = combo.FindStringExact(valor);
            if (index >= 0)
            {
                combo.SelectedIndex = index;
                return;
            }

            string valorNormalizado = NormalizarTexto(valor);
            for (int i = 0; i < combo.Items.Count; i++)
            {
                string item = combo.Items[i]?.ToString() ?? "";
                if (NormalizarTexto(item) == valorNormalizado)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }

        private void cmbTerrenoSiembra_SelectedIndexChanged(object sender, EventArgs e)
        {
            evaluacionActual = null;
            lblFactibilidad.Text = "Recomendación: Sin evaluar";
            lblFactibilidad.ForeColor = Color.Black;
            lblDetalleRecomendacion.Text = "";
            ActualizarDisponibilidadTerreno();
        }

        private void txtHectareasSiembra_TextChanged(object sender, EventArgs e)
        {
            ActualizarDisponibilidadTerreno();
        }

        private void cmbUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Las unidades se cargan desde el diseñador.
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

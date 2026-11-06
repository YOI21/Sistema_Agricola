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
        private void btnCalendarioAgricola_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTerrenoSiembra.SelectedItem is not TerrenoSiembraItem terreno)
                {
                    MessageBox.Show("Seleccione un terreno para consultar el calendario de su zona.",
                        "Terreno requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string periodo = BLL_RecomendacionesAgricolas.ObtenerPeriodoClimatico(
                    dtpFechadeSiembra.Value, terreno.ZonaAgroclimatica);
                List<SugerenciaCultivo> sugerencias = GenerarSugerenciasCultivo(terreno);
                StringBuilder mensaje = new();
                mensaje.AppendLine("CALENDARIO AGRÍCOLA POR ZONA");
                mensaje.AppendLine();
                mensaje.AppendLine($"Fecha: {dtpFechadeSiembra.Value:d}");
                mensaje.AppendLine($"Provincia: {terreno.Provincia}");
                mensaje.AppendLine($"Zona agroclimática: {terreno.ZonaAgroclimatica}");
                mensaje.AppendLine($"Período climático: {periodo}");
                mensaje.AppendLine();
                mensaje.AppendLine(sugerencias.Count == 0
                    ? "No hay resultados suficientes. Complete las mediciones agroclimáticas del terreno."
                    : "Cultivos mejor puntuados con las condiciones registradas:");
                foreach (SugerenciaCultivo sugerencia in sugerencias)
                    mensaje.AppendLine($"• {sugerencia.Nombre}: {sugerencia.Motivo}");

                MessageBox.Show(mensaje.ToString(), "Calendario agrícola",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.CalendarioAgricola");
            }
        }

        private string NombreDelMes(int numeroMes)
        {
            switch (numeroMes)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "Mes no válido";
            }
        }

        private void cmbCultivosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? seleccion = cmbCultivosDisponibles.SelectedItem?.ToString();

            // Si es un encabezado de categoría, no hacer nada
            if (string.IsNullOrEmpty(seleccion) || seleccion.StartsWith("--"))
                return;

            // Transferir la selección al campo de texto
            txtCultivo.Text = seleccion;

            // Mostrar información del ciclo de cultivo
            var infoCiclo = BLL_RecomendacionesAgricolas.ObtenerCicloParaCultivo(ObtenerNombreCultivoBase(seleccion));

            string zona = (cmbTerrenoSiembra.SelectedItem as TerrenoSiembraItem)?.ZonaAgroclimatica
                ?? string.Empty;
            string periodo = BLL_RecomendacionesAgricolas.ObtenerPeriodoClimatico(dtpFechadeSiembra.Value, zona);
            string mensaje = $"Fecha en {periodo.ToLowerInvariant()}; evalúe las condiciones del terreno";
            lblSeleccionCultivo.ForeColor = Color.FromArgb(62, 79, 98);

            // Estimar fecha de cosecha
            if (infoCiclo.DuracionAproxMeses > 0)
            {
                DateTime fechaEstimada = dtpFechadeSiembra.Value.AddMonths(infoCiclo.DuracionAproxMeses);
                mensaje += $" • Cosecha: ~{fechaEstimada.ToString("MMM yyyy")}";

                // Establecer automáticamente la fecha de cosecha estimada
                if (dtpFechaCosecha != null) // Verificar si existe dtpFechaCosecha
                {
                    dtpFechaCosecha.Value = fechaEstimada;
                }
            }

            lblSeleccionCultivo.Text = mensaje;

            AplicarDatosDerivadosDelCultivo(seleccion);

            SeleccionarValorCombo(cmbEstaciones, periodo);
        }

        private void CargarCultivos()
        {
            // Limpiar el ComboBox existente
            cmbCultivosDisponibles.Items.Clear();

            // Agregar cultivos de República Dominicana organizados por categorías
            // Granos básicos
            cmbCultivosDisponibles.Items.Add("-- GRANOS BÁSICOS --");
            cmbCultivosDisponibles.Items.Add("Arroz");
            cmbCultivosDisponibles.Items.Add("Maíz");
            cmbCultivosDisponibles.Items.Add("Habichuela");
            cmbCultivosDisponibles.Items.Add("Guandules");

            // Tubérculos
            cmbCultivosDisponibles.Items.Add("-- TUBÉRCULOS --");
            cmbCultivosDisponibles.Items.Add("Yuca");
            cmbCultivosDisponibles.Items.Add("Batata");
            cmbCultivosDisponibles.Items.Add("Ñame");

            // Frutas
            cmbCultivosDisponibles.Items.Add("-- FRUTAS --");
            cmbCultivosDisponibles.Items.Add("Plátano");
            cmbCultivosDisponibles.Items.Add("Guineo");
            cmbCultivosDisponibles.Items.Add("Piña");
            cmbCultivosDisponibles.Items.Add("Mango");
            cmbCultivosDisponibles.Items.Add("Aguacate");
            cmbCultivosDisponibles.Items.Add("Coco");
            cmbCultivosDisponibles.Items.Add("Lechosa");
            cmbCultivosDisponibles.Items.Add("Chinola");
            cmbCultivosDisponibles.Items.Add("Zapote");
            cmbCultivosDisponibles.Items.Add("Guayaba");
            cmbCultivosDisponibles.Items.Add("Naranja");
            cmbCultivosDisponibles.Items.Add("Limón");

            // Cultivos comerciales
            cmbCultivosDisponibles.Items.Add("-- CULTIVOS COMERCIALES --");
            cmbCultivosDisponibles.Items.Add("Café");
            cmbCultivosDisponibles.Items.Add("Cacao");
            cmbCultivosDisponibles.Items.Add("Cacao Orgánico");
            cmbCultivosDisponibles.Items.Add("Caña de Azúcar");
            cmbCultivosDisponibles.Items.Add("Tabaco");

            // Hortalizas
            cmbCultivosDisponibles.Items.Add("-- HORTALIZAS --");
            cmbCultivosDisponibles.Items.Add("Tomate");
            cmbCultivosDisponibles.Items.Add("Berenjena");
            cmbCultivosDisponibles.Items.Add("Ají");
            cmbCultivosDisponibles.Items.Add("Tayota");
            cmbCultivosDisponibles.Items.Add("Auyama");
            cmbCultivosDisponibles.Items.Add("Cebolla");
            cmbCultivosDisponibles.Items.Add("Zanahoria");
            cmbCultivosDisponibles.Items.Add("Repollo");
            cmbCultivosDisponibles.Items.Add("Lechuga");

            // Plantas medicinales y especias
            cmbCultivosDisponibles.Items.Add("-- MEDICINALES Y ESPECIAS --");
            cmbCultivosDisponibles.Items.Add("Moringa");
            cmbCultivosDisponibles.Items.Add("Sábila");
            cmbCultivosDisponibles.Items.Add("Jengibre");
            cmbCultivosDisponibles.Items.Add("Cúrcuma");
        }

        private void dtpFechadeSiembra_ValueChanged(object sender, EventArgs e)
        {
            VerificarFechaPlantacion();
        }

        private void VerificarFechaPlantacion()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCultivo.Text))
                    return;

                string nombreCultivo = txtCultivo.Text.Trim();
                DateTime fechaSeleccionada = dtpFechadeSiembra.Value;
                var infoCiclo = BLL_RecomendacionesAgricolas.ObtenerCicloParaCultivo(ObtenerNombreCultivoBase(nombreCultivo));
                if (infoCiclo.DuracionAproxMeses > 0 && dtpFechaCosecha != null)
                {
                    dtpFechaCosecha.Value = fechaSeleccionada.AddMonths(infoCiclo.DuracionAproxMeses);
                }

                string zona = (cmbTerrenoSiembra.SelectedItem as TerrenoSiembraItem)?.ZonaAgroclimatica
                    ?? string.Empty;
                string periodo = BLL_RecomendacionesAgricolas.ObtenerPeriodoClimatico(fechaSeleccionada, zona);
                SeleccionarValorCombo(cmbEstaciones, periodo);
                evaluacionActual = null;
                lblFactibilidad.Text = "Recomendación: Sin evaluar";
                lblFactibilidad.ForeColor = Color.Black;
                lblDetalleRecomendacion.Text = $"{periodo}. Pulse Evaluar condiciones para revisar el terreno.";
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.ObtenerMensajeSeguro(ex, "FrmSiembra.VerificarFecha");
            }
        }

        private void txtCultivo_TextChanged(object sender, EventArgs e)
        {
            // Limpia la evaluación actual
            evaluacionActual = null;
            lblFactibilidad.Text = "Recomendación: Sin evaluar";
            lblFactibilidad.ForeColor = Color.Black;
            lblDetalleRecomendacion.Text = "";

            if (!string.IsNullOrWhiteSpace(txtCultivo.Text))
            {
                AplicarDatosDerivadosDelCultivo(txtCultivo.Text.Trim());
            }

            // Si hay texto y fecha de siembra seleccionada, verificar
            if (!string.IsNullOrEmpty(txtCultivo.Text))
            {
                // Solo verificar si el texto cambió por interacción del usuario
                // (no por el cambio automático desde el ComboBox)
                if (!txtCultivo.Focused)
                    return;

                VerificarFechaPlantacion();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("No se aceptan números ni caracteres especiales", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

    }
}

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
        private void CargarTerrenosParaSiembra()
        {
            try
            {
                int idExcluir = esModoEdicion ? idCultivoEdicion : 0;
                int idSeleccionado = (cmbTerrenoSiembra.SelectedItem as TerrenoSiembraItem)?.IdTerreno ?? 0;

                cmbTerrenoSiembra.Items.Clear();
                DataTable terrenos = BLL_Terreno.Listar_Terrenos();

                foreach (DataRow fila in terrenos.Rows)
                {
                    int idTerreno = LeerEnteroFila(fila, "ID", "ID_Terreno");
                    string estado = LeerTextoFila(fila, "EstadoActual", "Estado");
                    if (idTerreno <= 0 || NormalizarTexto(estado) == "en descanso")
                    {
                        continue;
                    }

                    decimal area = LeerDecimalFila(fila, "Area", "Superficie_Total");
                    string unidad = LeerTextoFila(fila, "UnidadMedida", "Unidad_Medida");
                    decimal areaHectareas = ConvertirAreaAHectareas(area, unidad);
                    if (areaHectareas <= 0)
                    {
                        continue;
                    }

                    decimal ocupadas = BLL_Siembra.Obtener_Hectareas_Ocupadas_Terreno(idTerreno, idExcluir);
                    TerrenoSiembraItem item = new TerrenoSiembraItem
                    {
                        IdTerreno = idTerreno,
                        Nombre = LeerTextoFila(fila, "Nombre", "Nombre_Terreno"),
                        TipoSuelo = LeerTextoFila(fila, "TipoSuelo", "Tipo_Suelo"),
                        Provincia = LeerTextoFila(fila, "Provincia"),
                        ZonaAgroclimatica = LeerTextoFila(fila, "ZonaAgroclimatica", "Zona_Agroclimatica"),
                        PH = LeerDecimalFila(fila, "pH", "pH_Suelo"),
                        Humedad = LeerDecimalFila(fila, "Humedad"),
                        Drenaje = LeerTextoFila(fila, "Drenaje"),
                        Pendiente = LeerDecimalFila(fila, "Pendiente"),
                        PrecipitacionAnual = LeerDecimalFila(fila, "PrecipitacionAnual", "Precipitacion_Anual"),
                        DisponibilidadRiego = LeerBooleanoFila(fila, "DisponibilidadRiego", "Disponibilidad_Riego"),
                        FuenteAgua = LeerTextoFila(fila, "FuenteAgua", "Fuente_Agua"),
                        AreaHectareas = areaHectareas,
                        HectareasOcupadas = ocupadas
                    };

                    if (item.HectareasDisponibles > 0 || idTerreno == idSeleccionado)
                    {
                        cmbTerrenoSiembra.Items.Add(item);
                    }
                }

                if (idSeleccionado > 0)
                {
                    SeleccionarTerrenoSiembra(idSeleccionado);
                }

                ActualizarDisponibilidadTerreno();
            }
            catch (Exception ex)
            {
                lblDisponibilidadTerreno.ForeColor = Color.Firebrick;
                lblDisponibilidadTerreno.Text = ManejadorErroresUI.ObtenerMensajeSeguro(
                    ex, "FrmSiembra.CargarTerrenos");
            }
        }

        private void SeleccionarTerrenoSiembra(int idTerreno)
        {
            if (idTerreno <= 0)
            {
                return;
            }

            for (int i = 0; i < cmbTerrenoSiembra.Items.Count; i++)
            {
                if (cmbTerrenoSiembra.Items[i] is TerrenoSiembraItem item && item.IdTerreno == idTerreno)
                {
                    cmbTerrenoSiembra.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ActualizarDisponibilidadTerreno()
        {
            if (cmbTerrenoSiembra.SelectedItem is not TerrenoSiembraItem terreno)
            {
                lblDisponibilidadTerreno.ForeColor = Color.FromArgb(91, 124, 153);
                lblDisponibilidadTerreno.Text = cmbTerrenoSiembra.Items.Count == 0
                    ? "No hay terrenos con hectáreas disponibles."
                    : "Seleccione un terreno para ver disponibilidad.";
                return;
            }

            SeleccionarValorCombo(cmbTipoSuelo, terreno.TipoSuelo);
            string periodo = BLL_RecomendacionesAgricolas.ObtenerPeriodoClimatico(
                dtpFechadeSiembra.Value, terreno.ZonaAgroclimatica);
            SeleccionarValorCombo(cmbEstaciones, periodo);

            string uso = $"Área total: {terreno.AreaHectareas:N2} ha | Ocupadas: {terreno.HectareasOcupadas:N2} ha | Disponibles: {terreno.HectareasDisponibles:N2} ha\n" +
                $"{terreno.Provincia} | {terreno.ZonaAgroclimatica} | {periodo}";
            if (LeerDecimalTexto(txtHectareasSiembra.Text, out decimal hectareasSolicitadas) &&
                hectareasSolicitadas > terreno.HectareasDisponibles)
            {
                lblDisponibilidadTerreno.ForeColor = Color.Firebrick;
                lblDisponibilidadTerreno.Text = $"{uso}\nLa siembra supera las hectáreas disponibles.";
                return;
            }

            lblDisponibilidadTerreno.ForeColor = terreno.HectareasDisponibles > 0 ? Color.FromArgb(46, 125, 50) : Color.Firebrick;
            lblDisponibilidadTerreno.Text = uso;
        }

        private bool ValidarTerrenoSiembra(out int idTerreno, out decimal hectareasSembradas, out TerrenoSiembraItem terreno)
        {
            idTerreno = 0;
            hectareasSembradas = 0;
            terreno = null!;

            if (cmbTerrenoSiembra.SelectedItem is not TerrenoSiembraItem terrenoSeleccionado)
            {
                MessageBox.Show("Por favor seleccione el terreno donde se va a sembrar.",
                    "Terreno requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTab = tabDatos;
                cmbTerrenoSiembra.Focus();
                return false;
            }

            terreno = terrenoSeleccionado;

            if (!LeerDecimalTexto(txtHectareasSiembra.Text, out hectareasSembradas) || hectareasSembradas <= 0)
            {
                MessageBox.Show("Por favor ingrese las hectáreas que se van a sembrar.",
                    "Hectáreas requeridas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedTab = tabDatos;
                txtHectareasSiembra.Focus();
                return false;
            }

            if (hectareasSembradas > terreno.HectareasDisponibles)
            {
                MessageBox.Show(
                    $"El terreno seleccionado no tiene suficiente área disponible.\n\n" +
                    $"Terreno: {terreno.Nombre}\n" +
                    $"Disponibles: {terreno.HectareasDisponibles:N2} ha\n" +
                    $"Solicitadas: {hectareasSembradas:N2} ha",
                    "Terreno sin área suficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tabControl.SelectedTab = tabDatos;
                txtHectareasSiembra.Focus();
                return false;
            }

            idTerreno = terreno.IdTerreno;
            return true;
        }

    }
}

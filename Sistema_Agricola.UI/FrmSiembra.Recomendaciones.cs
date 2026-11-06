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
        private string NormalizarTexto(string valor)
        {
            return (valor ?? string.Empty).Trim().ToLowerInvariant()
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("ñ", "n");
        }

        private void ConfigurarTiposAutomaticos()
        {
            cmbTipoCultivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoCultivo.Enabled = false;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Enabled = false;
            cmbTipoSuelo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private string ObtenerNombreCultivoBase(string cultivo)
        {
            return (cultivo ?? string.Empty).Replace(" Orgánico", "").Replace(" orgánico", "").Trim();
        }

        private string ObtenerTipoCultivoRecomendado(string cultivo)
        {
            string normalizado = NormalizarTexto(cultivo);
            return normalizado.Contains("organico") ? "Orgánica" : "Híbrida";
        }

        private string ObtenerCategoriaCultivoRecomendada(string cultivo)
        {
            string cultivoBase = ObtenerNombreCultivoBase(cultivo);
            if (cultivoBase == "Moringa" || cultivoBase == "Sábila")
            {
                return "Medicinal";
            }

            if (cultivoBase == "Caña de Azúcar" || cultivoBase == "Tabaco")
            {
                return "Industrial";
            }

            return "Comestible";
        }

        private void AplicarDatosDerivadosDelCultivo(string cultivo)
        {
            if (string.IsNullOrWhiteSpace(cultivo))
            {
                return;
            }

            string tipoRecomendado = ObtenerTipoCultivoRecomendado(cultivo);
            SeleccionarValorCombo(cmbTipoCultivo, tipoRecomendado);
            SeleccionarValorCombo(comboBox2, tipoRecomendado);
            SeleccionarValorCombo(cmbCategoriaCultivo, ObtenerCategoriaCultivoRecomendada(cultivo));
        }

        private bool ValidarTipoDerivadoDelCultivo()
        {
            string cultivo = txtCultivo.Text.Trim();
            if (string.IsNullOrWhiteSpace(cultivo))
            {
                return true;
            }

            AplicarDatosDerivadosDelCultivo(cultivo);
            string tipoEsperado = ObtenerTipoCultivoRecomendado(cultivo);
            string tipoSeleccionado = cmbTipoCultivo.SelectedItem?.ToString() ?? string.Empty;
            string tipoSemilla = comboBox2.SelectedItem?.ToString() ?? string.Empty;

            bool tipoCorrecto = NormalizarTexto(tipoSeleccionado) == NormalizarTexto(tipoEsperado);
            bool semillaCorrecta = NormalizarTexto(tipoSemilla) == NormalizarTexto(tipoEsperado);
            if (tipoCorrecto && semillaCorrecta)
            {
                return true;
            }

            MessageBox.Show(
                $"El tipo de cultivo y el tipo aplicado deben coincidir con el cultivo seleccionado.\n\n" +
                $"Cultivo: {cultivo}\n" +
                $"Tipo esperado: {tipoEsperado}",
                "Tipo de cultivo incoherente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        private List<string> ObtenerCultivosDisponiblesParaSugerir()
        {
            if (cmbCultivosDisponibles.Items.Count == 0)
            {
                CargarCultivos();
            }

            return cmbCultivosDisponibles.Items
                .Cast<object>()
                .Select(item => item?.ToString() ?? string.Empty)
                .Where(item => !string.IsNullOrWhiteSpace(item) && !item.StartsWith("--"))
                .Distinct()
                .ToList();
        }

        private List<SugerenciaCultivo> GenerarSugerenciasCultivo(TerrenoSiembraItem terreno)
        {
            List<SugerenciaCultivo> sugerencias = new List<SugerenciaCultivo>();

            foreach (string cultivo in ObtenerCultivosDisponiblesParaSugerir())
            {
                string tipoCultivo = ObtenerTipoCultivoRecomendado(cultivo);
                string categoria = ObtenerCategoriaCultivoRecomendada(cultivo);
                string cultivoBase = ObtenerNombreCultivoBase(cultivo);
                EvaluacionAgricola evaluacion = BLL_RecomendacionesAgricolas.Evaluar(
                    cultivoBase,
                    CrearCondicionesAgroclimaticas(terreno, string.Empty));
                var ciclo = BLL_RecomendacionesAgricolas.ObtenerCicloParaCultivo(cultivoBase);

                if (evaluacion.Nivel == NivelRecomendacionAgricola.Recomendado ||
                    evaluacion.Nivel == NivelRecomendacionAgricola.Condicionado)
                {
                    sugerencias.Add(new SugerenciaCultivo
                    {
                        Nombre = cultivo,
                        TipoCultivo = tipoCultivo,
                        Categoria = categoria,
                        MejorSiembra = ciclo.MejorMesSiembra,
                        MejorCosecha = ciclo.MejorMesCosecha,
                        DuracionMeses = ciclo.DuracionAproxMeses,
                        Nivel = evaluacion.Nivel,
                        Prioridad = evaluacion.Puntuacion,
                        Motivo = evaluacion.Resumen,
                        BuenaTemporada = evaluacion.TemporadaCompatible
                    });
                }
            }

            return sugerencias
                .OrderByDescending(s => s.Prioridad)
                .ThenBy(s => s.Nombre)
                .Take(10)
                .ToList();
        }

        private void MostrarSugerenciasCultivo(List<SugerenciaCultivo> sugerencias, TerrenoSiembraItem terreno)
        {
            using (Form dialogo = new Form())
            using (ListBox lista = new ListBox())
            using (Label encabezado = new Label())
            using (Panel panelBotones = new Panel())
            using (Button btnAplicar = new Button())
            using (Button btnCerrarDialogo = new Button())
            {
                dialogo.Text = "Recomendaciones agrícolas";
                dialogo.StartPosition = FormStartPosition.CenterParent;
                dialogo.Size = new Size(760, 420);
                dialogo.MinimizeBox = false;
                dialogo.MaximizeBox = false;
                dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;

                encabezado.Dock = DockStyle.Top;
                encabezado.Height = 60;
                encabezado.Padding = new Padding(12, 10, 12, 4);
                encabezado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                string periodo = BLL_RecomendacionesAgricolas.ObtenerPeriodoClimatico(
                    dtpFechadeSiembra.Value, terreno.ZonaAgroclimatica);
                encabezado.Text = $"{terreno.Nombre} | {terreno.ZonaAgroclimatica} | {periodo}. Resultados mejor puntuados.";

                lista.Dock = DockStyle.Fill;
                lista.Font = new Font("Segoe UI", 10F);
                lista.DataSource = sugerencias;

                panelBotones.Dock = DockStyle.Bottom;
                panelBotones.Height = 56;
                panelBotones.Padding = new Padding(12);

                btnAplicar.Text = "Aplicar";
                btnAplicar.Width = 120;
                btnAplicar.Dock = DockStyle.Right;
                btnAplicar.DialogResult = DialogResult.OK;

                btnCerrarDialogo.Text = "Cerrar";
                btnCerrarDialogo.Width = 120;
                btnCerrarDialogo.Dock = DockStyle.Right;
                btnCerrarDialogo.DialogResult = DialogResult.Cancel;

                panelBotones.Controls.Add(btnAplicar);
                panelBotones.Controls.Add(btnCerrarDialogo);
                dialogo.Controls.Add(lista);
                dialogo.Controls.Add(panelBotones);
                dialogo.Controls.Add(encabezado);
                dialogo.AcceptButton = btnAplicar;
                dialogo.CancelButton = btnCerrarDialogo;

                if (dialogo.ShowDialog(this) == DialogResult.OK && lista.SelectedItem is SugerenciaCultivo sugerencia)
                {
                    AplicarSugerenciaCultivo(sugerencia);
                }
            }
        }

        private void AplicarSugerenciaCultivo(SugerenciaCultivo sugerencia)
        {
            txtCultivo.Text = sugerencia.Nombre;
            AplicarDatosDerivadosDelCultivo(sugerencia.Nombre);

            if (sugerencia.DuracionMeses > 0)
            {
                dtpFechaCosecha.Value = dtpFechadeSiembra.Value.AddMonths(sugerencia.DuracionMeses);
            }

            lblSeleccionCultivo.ForeColor = sugerencia.BuenaTemporada ? Color.Green : Color.DarkOrange;
            lblSeleccionCultivo.Text = sugerencia.BuenaTemporada
                ? "Cultivo recomendado para la fecha seleccionada"
                : $"Recomendación condicionada; periodo preferido: {sugerencia.MejorSiembra}";
        }

        private decimal LeerDecimalOpcional(TextBox textBox)
        {
            if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
            {
                return 0;
            }

            string valor = textBox.Text.Replace("$", "").Replace("%", "").Trim();
            return LeerDecimalTexto(valor, out decimal resultado) ? resultado : 0;
        }

        private bool LeerDecimalTexto(string valor, out decimal resultado)
        {
            resultado = 0;
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            string limpio = valor.Replace("$", "").Replace("%", "").Trim();
            return decimal.TryParse(limpio, NumberStyles.Number, CultureInfo.CurrentCulture, out resultado)
                || decimal.TryParse(limpio.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out resultado);
        }

        private string LeerTextoFila(DataRow fila, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value)
                {
                    return fila[columna].ToString() ?? string.Empty;
                }
            }

            return string.Empty;
        }

        private int LeerEnteroFila(DataRow fila, params string[] columnas)
        {
            string valor = LeerTextoFila(fila, columnas);
            return int.TryParse(valor, out int resultado) ? resultado : 0;
        }

        private decimal LeerDecimalFila(DataRow fila, params string[] columnas)
        {
            string valor = LeerTextoFila(fila, columnas);
            return LeerDecimalTexto(valor, out decimal resultado) ? resultado : 0;
        }

        private bool LeerBooleanoFila(DataRow fila, params string[] columnas)
        {
            string valor = LeerTextoFila(fila, columnas);
            return bool.TryParse(valor, out bool booleano)
                ? booleano
                : int.TryParse(valor, out int numero) && numero != 0;
        }

        private CondicionesAgroclimaticas CrearCondicionesAgroclimaticas(
            TerrenoSiembraItem terreno,
            string variedad)
        {
            return new CondicionesAgroclimaticas
            {
                FechaSiembra = dtpFechadeSiembra.Value.Date,
                Provincia = terreno.Provincia,
                ZonaAgroclimatica = terreno.ZonaAgroclimatica,
                TipoSuelo = terreno.TipoSuelo,
                PH = terreno.PH > 0 ? terreno.PH : null,
                Humedad = terreno.Humedad > 0 ? terreno.Humedad : null,
                Drenaje = terreno.Drenaje,
                Pendiente = terreno.Pendiente >= 0 ? terreno.Pendiente : null,
                PrecipitacionAnual = terreno.PrecipitacionAnual > 0
                    ? terreno.PrecipitacionAnual
                    : null,
                DisponibilidadRiego = terreno.DisponibilidadRiego,
                Variedad = variedad
            };
        }

        private decimal ConvertirAreaAHectareas(decimal area, string unidadMedida)
        {
            string unidad = NormalizarTexto(unidadMedida);
            if (unidad.Contains("metro"))
            {
                return area / 10000m;
            }

            if (unidad.Contains("tarea"))
            {
                return area * 0.062886m;
            }

            return area;
        }

    }
}

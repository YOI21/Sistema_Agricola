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
        private void btnEvaluarFactibilidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTerrenoSiembra.SelectedItem is not TerrenoSiembraItem terrenoSeleccionado)
                {
                    MessageBox.Show("Seleccione el terreno para evaluar las condiciones reales de siembra.",
                        "Terreno requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedTab = tabDatos;
                    cmbTerrenoSiembra.Focus();
                    return;
                }

                SeleccionarValorCombo(cmbTipoSuelo, terrenoSeleccionado.TipoSuelo);

                // Verificar que todos los campos necesarios estén completos
                if (string.IsNullOrEmpty(txtCultivo.Text) || cmbTipoCultivo.SelectedIndex == -1 ||
                    cmbCategoriaCultivo.SelectedIndex == -1 || cmbEstaciones.SelectedIndex == -1 ||
                    cmbTipoSuelo.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor complete todos los campos necesarios para la evaluación.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos del formulario
                string nombreCultivo = txtCultivo.Text.Trim();
                string cultivoBase = ObtenerNombreCultivoBase(nombreCultivo);
                string tipoSuelo = cmbTipoSuelo.SelectedItem?.ToString() ?? string.Empty;

                evaluacionActual = BLL_RecomendacionesAgricolas.Evaluar(
                    cultivoBase,
                    CrearCondicionesAgroclimaticas(
                        terrenoSeleccionado,
                        txtVariedadCultivo.Text));

                var infoCiclo = BLL_RecomendacionesAgricolas.ObtenerCicloParaCultivo(cultivoBase);

                if (evaluacionActual.Nivel == NivelRecomendacionAgricola.Recomendado)
                {
                    lblFactibilidad.Text = "Recomendación: FAVORABLE";
                    lblFactibilidad.ForeColor = Color.Green;
                }
                else if (evaluacionActual.Nivel == NivelRecomendacionAgricola.Condicionado)
                {
                    lblFactibilidad.Text = "Recomendación: CONDICIONADA";
                    lblFactibilidad.ForeColor = Color.DarkOrange;
                }
                else if (evaluacionActual.Nivel == NivelRecomendacionAgricola.NoRecomendado)
                {
                    lblFactibilidad.Text = "Recomendación: NO FAVORABLE";
                    lblFactibilidad.ForeColor = Color.Red;
                }
                else
                {
                    lblFactibilidad.Text = "Recomendación: REQUIERE REVISIÓN";
                    lblFactibilidad.ForeColor = Color.DarkOrange;
                }

                lblDetalleRecomendacion.Text = evaluacionActual.Resumen;

                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine("Recomendación agrícola basada en reglas:");
                mensaje.AppendLine($"Terreno: {terrenoSeleccionado.Nombre}");
                mensaje.AppendLine($"Cultivo: {nombreCultivo}");
                mensaje.AppendLine($"Variedad: {(string.IsNullOrWhiteSpace(txtVariedadCultivo.Text) ? "sin registrar" : txtVariedadCultivo.Text.Trim())}");
                mensaje.AppendLine($"Suelo registrado: {tipoSuelo}");
                mensaje.AppendLine($"Zona: {evaluacionActual.ZonaAgroclimatica}");
                mensaje.AppendLine($"Período climático: {evaluacionActual.PeriodoClimatico}");
                mensaje.AppendLine($"Resultado: {evaluacionActual.Resumen}");
                mensaje.AppendLine();

                foreach (string criterio in evaluacionActual.Criterios)
                {
                    mensaje.AppendLine("- " + criterio);
                }

                foreach (string advertencia in evaluacionActual.Advertencias)
                {
                    mensaje.AppendLine("- Advertencia: " + advertencia);
                }

                mensaje.AppendLine();
                mensaje.AppendLine("Fuentes institucionales:");
                foreach (FuenteAgricola fuente in evaluacionActual.Fuentes)
                {
                    mensaje.AppendLine($"- {fuente.Id}: {fuente.Institucion}");
                }
                mensaje.AppendLine("- Estado: " + evaluacionActual.EstadoValidacion);

                mensaje.AppendLine();
                mensaje.AppendLine("Ciclo agrícola orientativo:");
                mensaje.AppendLine($"- Fecha evaluada: {dtpFechadeSiembra.Value:d}");
                mensaje.AppendLine($"- Ventana usual: {infoCiclo.MejorMesSiembra}");
                mensaje.AppendLine($"- Duración aproximada: {infoCiclo.DuracionAproxMeses} meses");
                mensaje.AppendLine($"- Cosecha esperada: {infoCiclo.MejorMesCosecha}");

                MessageBoxIcon icono =
                    evaluacionActual.Nivel == NivelRecomendacionAgricola.Recomendado
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning;
                MessageBox.Show(
                    mensaje.ToString(),
                    "Recomendación agrícola",
                    MessageBoxButtons.OK,
                    icono);
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.EvaluarCondiciones");
            }
        }

        // Evento para sugerir cultivos según la estación seleccionada
        private void btnSugerirCultivos_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTerrenoSiembra.SelectedItem is not TerrenoSiembraItem terrenoSeleccionado)
                {
                    MessageBox.Show("Seleccione un terreno para sugerir cultivos según su suelo disponible.",
                        "Terreno requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedTab = tabDatos;
                    cmbTerrenoSiembra.Focus();
                    return;
                }

                SeleccionarValorCombo(cmbTipoSuelo, terrenoSeleccionado.TipoSuelo);

                List<SugerenciaCultivo> sugerencias = GenerarSugerenciasCultivo(terrenoSeleccionado);

                if (sugerencias.Count > 0)
                {
                    MostrarSugerenciasCultivo(sugerencias, terrenoSeleccionado);
                }
                else
                {
                    MessageBox.Show("No se encontraron cultivos recomendados para la combinación seleccionada.",
                        "Sin sugerencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.SugerirCultivos");
            }
        }

        // Guardar la siembra en la base de datos
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validación de campos
                if (string.IsNullOrEmpty(txtCultivo.Text))
                {
                    MessageBox.Show("Por favor ingrese el nombre del cultivo", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtVariedadCultivo.Text))
                {
                    MessageBox.Show("Indique la variedad del cultivo para evaluar su adaptación y conservar ese dato.",
                        "Variedad requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedTab = tabCultivo;
                    txtVariedadCultivo.Focus();
                    return;
                }

                if (!ValidarTipoDerivadoDelCultivo())
                {
                    return;
                }

                if (cmbTipoCultivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione el tipo de cultivo", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCategoriaCultivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione la categoría del cultivo", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbEstaciones.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione la estación de siembra", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTipoSuelo.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor seleccione el tipo de suelo", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (textBox3 == null || string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor ingrese la cantidad a sembrar", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedTab = tabDatos;
                    textBox3?.Focus();
                    return;
                }

                string cantidadText = textBox3.Text.Trim();
                if (!int.TryParse(cantidadText, out int cantidadSiembra) || cantidadSiembra <= 0)
                {
                    MessageBox.Show("Por favor ingrese una cantidad válida mayor que cero", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedTab = tabDatos;
                    textBox3.Focus();
                    return;
                }

                if (!ValidarTerrenoSiembra(out int idTerreno, out decimal hectareasSembradas, out TerrenoSiembraItem terrenoSeleccionado))
                {
                    return;
                }

                SeleccionarValorCombo(cmbTipoSuelo, terrenoSeleccionado.TipoSuelo);

                evaluacionActual = BLL_RecomendacionesAgricolas.Evaluar(
                    ObtenerNombreCultivoBase(txtCultivo.Text),
                    CrearCondicionesAgroclimaticas(terrenoSeleccionado, txtVariedadCultivo.Text));
                if (evaluacionActual.RequiereConfirmacion)
                {
                    DialogResult respuesta = MessageBox.Show(
                        evaluacionActual.Nivel == NivelRecomendacionAgricola.SinInformacion
                            ? "Faltan condiciones del terreno para respaldar esta recomendación. ¿Desea guardar la siembra de todos modos?"
                            : "Hay condiciones críticas no favorables para este cultivo. ¿Desea guardar la siembra de todos modos?",
                        "Recomendación agrícola",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.No)
                        return;
                }

                // Preparar fechas
                DateTime fechaPlantacion = dtpFechadeSiembra.Value;
                DateTime fechaCosecha = fechaPlantacion;

                // Si tenemos dtpFechaCosecha, usamos su valor; de lo contrario, calculamos
                try
                {
                    if (dtpFechaCosecha != null)
                    {
                        fechaCosecha = dtpFechaCosecha.Value;
                    }
                    else
                    {
                        // Calcular fecha de cosecha basada en el ciclo del cultivo
                        var infoCiclo = BLL_RecomendacionesAgricolas.ObtenerCicloParaCultivo(txtCultivo.Text);
                        fechaCosecha = fechaPlantacion.AddMonths(infoCiclo.DuracionAproxMeses);
                    }
                }
                catch (Exception)
                {
                    // Si hay algún error, simplemente usamos una fecha 3 meses después
                    fechaCosecha = fechaPlantacion.AddMonths(3);
                }

                // Preparar datos básicos de siembra
                string nombreCultivo = txtCultivo.Text;
                string variedadCultivo = txtVariedadCultivo.Text.Trim();
                string tipoCultivo = Convert.ToString(cmbTipoCultivo.SelectedItem) ?? string.Empty;
                string categoriaCultivo = Convert.ToString(cmbCategoriaCultivo.SelectedItem) ?? string.Empty;
                string estacionSiembra = Convert.ToString(cmbEstaciones.SelectedItem) ?? string.Empty;
                string tipoSuelo = Convert.ToString(cmbTipoSuelo.SelectedItem) ?? string.Empty;
                string metodoSiembra = cbMetododeSiem.SelectedItem?.ToString() ?? "";
                string profundidad = textBox2.Text.Trim();
                decimal humedadSuelo = 0;
                decimal costoSiembra = LeerDecimalOpcional(textBox4);

                string resultado = string.Empty;
                bool operacionExitosa = false;
                int idAgricultorSesion = Session.IsAgricultor()
                    ? Session.CurrentUser!.ID
                    : 0;

                // Mostrar diálogo de confirmación antes de guardar
                DialogResult confirmarGuardado = MessageBox.Show(
                    $"¿Está seguro que desea guardar la siembra de {nombreCultivo}?\n\n" +
                    $"Tipo: {tipoCultivo}\n" +
                    $"Variedad: {variedadCultivo}\n" +
                    $"Categoría: {categoriaCultivo}\n" +
                    $"Período: {estacionSiembra}\n" +
                    $"Suelo: {tipoSuelo}\n" +
                    $"Terreno: {terrenoSeleccionado.Nombre}\n" +
                    $"Hectáreas: {hectareasSembradas:N2} ha\n" +
                    $"Fecha siembra: {fechaPlantacion.ToShortDateString()}\n" +
                    $"Fecha cosecha: {fechaCosecha.ToShortDateString()}",
                    "Confirmar guardado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmarGuardado == DialogResult.No)
                {
                    return;
                }

                // Mostrar indicador de progreso
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    if (esModoEdicion && idCultivoEdicion > -1)
                    {
                        resultado = BLL_Siembra.Actualizar_Siembra(
                            idCultivoEdicion,
                            nombreCultivo,
                            tipoCultivo,
                            categoriaCultivo,
                            estacionSiembra,
                            cantidadSiembra,
                            tipoSuelo,
                            fechaPlantacion,
                            fechaCosecha,
                            metodoSiembra,
                            profundidad,
                            humedadSuelo,
                            costoSiembra,
                            idTerreno,
                            hectareasSembradas,
                            idAgricultorSesion,
                            variedadCultivo
                        );

                        operacionExitosa = !resultado.ToLower().Contains("error") &&
                                          !resultado.ToLower().Contains("excepción") &&
                                          !resultado.ToLower().Contains("falló") &&
                                          !resultado.ToLower().Contains("problema");
                    }
                    else
                    {
                        // Usar solo los parámetros básicos para la inserción
                        resultado = BLL_Siembra.Insertar_Siembra_Corregido(
                            nombreCultivo,
                            tipoCultivo,
                            categoriaCultivo,
                            estacionSiembra,
                            cantidadSiembra,
                            tipoSuelo,
                            fechaPlantacion,
                            fechaCosecha,
                            metodoSiembra,
                            profundidad,
                            humedadSuelo,
                            costoSiembra,
                            idTerreno,
                            hectareasSembradas,
                            idAgricultorSesion,
                            variedadCultivo
                        );

                        // Verificar si la operación fue exitosa basándonos en el mensaje devuelto
                        operacionExitosa = !resultado.ToLower().Contains("error") &&
                                          !resultado.ToLower().Contains("excepción") &&
                                          !resultado.ToLower().Contains("falló");
                    }
                }
                catch (Exception ex)
                {
                    operacionExitosa = false;
                    resultado = ManejadorErroresUI.ObtenerMensajeSeguro(
                        ex, "FrmSiembra.GuardarOperacion");
                }
                finally
                {
                    // Restaurar el cursor
                    Cursor.Current = Cursors.Default;
                }

                // Mostrar mensaje de éxito o error según corresponda
                if (operacionExitosa)
                {
                    if (esModoEdicion)
                    {
                        MessageBox.Show(
                            $"La siembra de {nombreCultivo} se ha actualizado correctamente.",
                            "Actualización exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LimpiarFormulario(this);
                        GenerarNuevoID();
                        return;
                    }

                    MessageBox.Show(
                        $"¡La siembra de {nombreCultivo} se ha guardado correctamente!\n\n" +
                        "Los datos han sido registrados en el sistema.",
                        "Operación Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Reiniciar el formulario
                    LimpiarFormulario(this);
                    GenerarNuevoID();

                    // Preguntar si desea ir a la pantalla de seguimiento
                    DialogResult irASeguimiento = MessageBox.Show(
                        "¿Desea ir a la pantalla de seguimiento de crecimiento para este cultivo?",
                        "Seguimiento de Crecimiento",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (irASeguimiento == DialogResult.Yes)
                    {
                        FrmSeguimientoCrecimiento frmSeguimiento = new FrmSeguimientoCrecimiento();
                        frmSeguimiento.ShowDialog();
                    }
                }
                else
                {
                    // Mostrar mensaje de error
                    MessageBox.Show(
                        $"No se pudo guardar la siembra: {resultado}\n\n" +
                        "Por favor verifique los datos e intente nuevamente.",
                        "Error al guardar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.Guardar");
            }
        }

    }
}

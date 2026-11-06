using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.UI
{
    public partial class FrmComprobarCompatibilidad : Form
    {
        private int ID_Terreno;
        private Entitie_Terreno terreno = new Entitie_Terreno();

        public FrmComprobarCompatibilidad(int idTerreno)
        {
            InitializeComponent();
            this.ID_Terreno = idTerreno;

            // Cargar datos del terreno
            try
            {
                Entitie_Terreno? terrenoEncontrado = BLL_Terreno.Obtener_Terreno(idTerreno);
                if (terrenoEncontrado == null)
                {
                    MessageBox.Show("No se pudo obtener la información del terreno.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                terreno = terrenoEncontrado;
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmComprobarCompatibilidad.CargarTerreno");
                this.Close();
            }
        }


        private void FrmComprobarCompatibilidad_Load(object sender, EventArgs e)
        {
            CargarDatosTerreno();
            CargarCultivos();
        }

        private void CargarDatosTerreno()
        {
            if (terreno != null)
            {
                lblTituloTerreno.Text = $"Terreno: {terreno.Nombre}";
                lblTipoSuelo.Text = $"Tipo de Suelo: {terreno.TipoSuelo}";

                // Convertir área a hectáreas si es necesario para mostrar
                decimal areaEnHectareas = terreno.Area;
                if (terreno.UnidadMedida.ToLower() == "metros cuadrados" ||
                    terreno.UnidadMedida.ToLower() == "m²")
                {
                    areaEnHectareas = terreno.Area / 10000; // 1 hectárea = 10,000 m²
                    lblArea.Text = $"Área: {terreno.Area:N2} {terreno.UnidadMedida} ({areaEnHectareas:N4} hectáreas)";
                }
                else if (terreno.UnidadMedida.ToLower() == "tareas")
                {
                    // En Rep. Dominicana, 1 tarea ≈ 628.86 m²
                    areaEnHectareas = (terreno.Area * 628.86m) / 10000;
                    lblArea.Text = $"Área: {terreno.Area:N2} {terreno.UnidadMedida} ({areaEnHectareas:N4} hectáreas)";
                }
                else
                {
                    lblArea.Text = $"Área: {terreno.Area:N2} {terreno.UnidadMedida}";
                }

                lblPH.Text = $"pH: {terreno.pH:N1}";
                lblHumedad.Text = $"Humedad: {terreno.Humedad:N1}%";
                lblPendiente.Text = $"Pendiente: {terreno.Pendiente:N1}%";
            }
        }

        private void CargarCultivos()
        {
            try
            {
                cmbCultivos.Items.Clear();
                cmbCultivos.Items.AddRange(
                    BLL_RecomendacionesAgricolas.CultivosSoportados.Cast<object>().ToArray());
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmComprobarCompatibilidad.CargarCultivos");
            }
        }

        private void btnComprobar_Click(object sender, EventArgs e)
        {
            if (cmbCultivos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cultivo válido",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de cultivo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategoriaCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar la categoría del cultivo",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener datos del cultivo seleccionado
            string nombreCultivo = cmbCultivos.SelectedItem?.ToString() ?? string.Empty;

            try
            {
                EvaluacionAgricola evaluacion = BLL_Terreno.EvaluarCompatibilidadCultivo(
                    terreno.ID_Terreno,
                    nombreCultivo,
                    txtVariedad.Text,
                    dtpFechaSiembra.Value.Date);

                StringBuilder mensaje = new();
                mensaje.AppendLine($"{nombreCultivo} - {evaluacion.Resumen}");
                mensaje.AppendLine($"Zona: {ValorODatoFaltante(evaluacion.ZonaAgroclimatica)}");
                mensaje.AppendLine($"Período: {ValorODatoFaltante(evaluacion.PeriodoClimatico)}");
                mensaje.AppendLine();
                mensaje.AppendLine("Condiciones evaluadas:");
                foreach (string criterio in evaluacion.Criterios)
                    mensaje.AppendLine("• " + criterio);

                foreach (string advertencia in evaluacion.Advertencias)
                    mensaje.AppendLine("\nNota: " + advertencia);

                mensaje.AppendLine("\nFuentes consultadas:");
                foreach (FuenteAgricola fuente in evaluacion.Fuentes)
                    mensaje.AppendLine($"• {fuente.Id}: {fuente.Institucion}");
                mensaje.AppendLine("Estado: " + evaluacion.EstadoValidacion);

                if (evaluacion.Nivel == NivelRecomendacionAgricola.NoRecomendado ||
                    evaluacion.Nivel == NivelRecomendacionAgricola.SinInformacion)
                {
                    List<string> alternativas = BLL_Terreno.RecomendarCultivosPorTerreno(terreno.ID_Terreno)
                        .Where(c => !c.Equals(nombreCultivo, StringComparison.OrdinalIgnoreCase))
                        .Take(5)
                        .ToList();
                    if (alternativas.Count > 0)
                        mensaje.AppendLine("\nAlternativas mejor puntuadas: " + string.Join(", ", alternativas));
                }

                MessageBoxIcon icono = evaluacion.Nivel == NivelRecomendacionAgricola.Recomendado
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning;
                MessageBox.Show(mensaje.ToString(), "Evaluación agrícola",
                    MessageBoxButtons.OK, icono);
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmComprobarCompatibilidad.Evaluar");
            }
        }

        private decimal CalcularAreaEnHectareas(Entitie_Terreno terreno)
        {
            decimal areaEnHectareas = terreno.Area;

            if (terreno.UnidadMedida.ToLower().Contains("metro"))
            {
                areaEnHectareas = terreno.Area / 10000; // Convertir m² a hectáreas
            }
            else if (terreno.UnidadMedida.ToLower().Contains("tarea"))
            {
                // En Rep. Dominicana, 1 tarea ≈ 628.86 m²
                areaEnHectareas = (terreno.Area * 628.86m) / 10000;
            }

            return areaEnHectareas;
        }

        private decimal ObtenerAreaMinima(string nombreCultivo)
        {
            // Retorna el área mínima recomendada en hectáreas para cada cultivo
            switch (nombreCultivo.ToLower())
            {
                case "arroz":
                    return 1.0m; // 1 hectárea mínimo
                case "maíz":
                    return 0.5m; // 0.5 hectáreas mínimo
                case "habichuela":
                case "frijol":
                    return 0.25m;
                case "café":
                    return 0.5m;
                case "cacao":
                    return 0.5m;
                case "caña de azúcar":
                    return 1.0m;
                case "plátano":
                case "guineo":
                    return 0.25m;
                case "yuca":
                case "batata":
                case "ñame":
                    return 0.2m;
                case "tomate":
                case "ají":
                case "cebolla":
                case "zanahoria":
                case "repollo":
                case "lechuga":
                    return 0.1m; // 1000 m²
                default:
                    return 0.1m; // Por defecto 0.1 hectáreas
            }
        }

        private decimal CalcularCantidadRecomendada(string nombreCultivo, decimal areaEnHectareas, string tipoCultivo)
        {
            // Retorna la cantidad recomendada según el área en hectáreas y el tipo de cultivo
            // Las densidades de siembra varían según el cultivo y si es orgánico o no

            // Factor de reducción para cultivo orgánico (generalmente menos denso)
            decimal factorOrganico = (tipoCultivo.ToLower() == "orgánica") ? 0.8m : 1.0m;

            switch (nombreCultivo.ToLower())
            {
                case "arroz":
                    return Math.Round(areaEnHectareas * 100 * factorOrganico); // Kg de semilla por hectárea

                case "maíz":
                    return Math.Round(areaEnHectareas * 25 * factorOrganico); // Kg de semilla por hectárea

                case "habichuela":
                case "frijol":
                    return Math.Round(areaEnHectareas * 70 * factorOrganico); // Kg de semilla por hectárea

                case "guandules":
                    return Math.Round(areaEnHectareas * 15 * factorOrganico); // Kg de semilla por hectárea

                case "yuca":
                    return Math.Round(areaEnHectareas * 10000 * factorOrganico); // Plantas por hectárea (tallos)

                case "batata":
                    return Math.Round(areaEnHectareas * 40000 * factorOrganico); // Plantas por hectárea

                case "ñame":
                    return Math.Round(areaEnHectareas * 8000 * factorOrganico); // Plantas por hectárea

                case "plátano":
                case "guineo":
                    return Math.Round(areaEnHectareas * 1600 * factorOrganico); // Plantas por hectárea

                case "piña":
                    return Math.Round(areaEnHectareas * 35000 * factorOrganico); // Plantas por hectárea

                case "café":
                    return Math.Round(areaEnHectareas * 3000 * factorOrganico); // Plantas por hectárea

                case "cacao":
                    return Math.Round(areaEnHectareas * 1100 * factorOrganico); // Plantas por hectárea

                case "caña de azúcar":
                    return Math.Round(areaEnHectareas * 8 * factorOrganico); // Toneladas de semilla por hectárea

                case "tomate":
                    return Math.Round(areaEnHectareas * 12000 * factorOrganico); // Plantas por hectárea

                case "berenjena":
                    return Math.Round(areaEnHectareas * 10000 * factorOrganico); // Plantas por hectárea

                case "ají":
                case "pimiento":
                    return Math.Round(areaEnHectareas * 15000 * factorOrganico); // Plantas por hectárea

                case "cebolla":
                    return Math.Round(areaEnHectareas * 250000 * factorOrganico); // Plantas por hectárea

                case "zanahoria":
                    return Math.Round(areaEnHectareas * 500000 * factorOrganico); // Plantas por hectárea (semillas)

                case "repollo":
                case "lechuga":
                    return Math.Round(areaEnHectareas * 40000 * factorOrganico); // Plantas por hectárea

                default:
                    return Math.Round(areaEnHectareas * 10000 * factorOrganico); // Valor genérico (10,000 plantas/ha)
            }
        }

        private string ObtenerUnidadSiembra(string nombreCultivo)
        {
            // Retorna la unidad de medida para la siembra según el cultivo
            switch (nombreCultivo.ToLower())
            {
                case "arroz":
                case "maíz":
                case "habichuela":
                case "frijol":
                case "guandules":
                    return "kg de semilla";

                case "caña de azúcar":
                    return "toneladas de semilla";

                default:
                    return "plantas";
            }
        }

        private decimal EstimarProduccion(string nombreCultivo, decimal areaEnHectareas)
        {
            // Estima la producción total basada en rendimientos promedio
            // Rendimientos aproximados para República Dominicana
            switch (nombreCultivo.ToLower())
            {
                case "arroz":
                    return Math.Round(areaEnHectareas * 4000); // kg/ha

                case "maíz":
                    return Math.Round(areaEnHectareas * 2500); // kg/ha

                case "habichuela":
                case "frijol":
                    return Math.Round(areaEnHectareas * 1200); // kg/ha

                case "guandules":
                    return Math.Round(areaEnHectareas * 800); // kg/ha

                case "yuca":
                    return Math.Round(areaEnHectareas * 15000); // kg/ha

                case "batata":
                    return Math.Round(areaEnHectareas * 12000); // kg/ha

                case "ñame":
                    return Math.Round(areaEnHectareas * 18000); // kg/ha

                case "plátano":
                    return Math.Round(areaEnHectareas * 15000); // kg/ha

                case "guineo":
                    return Math.Round(areaEnHectareas * 20000); // kg/ha

                case "piña":
                    return Math.Round(areaEnHectareas * 40000); // kg/ha

                case "café":
                    return Math.Round(areaEnHectareas * 800); // kg/ha (pergamino seco)

                case "cacao":
                    return Math.Round(areaEnHectareas * 500); // kg/ha (grano seco)

                case "caña de azúcar":
                    return Math.Round(areaEnHectareas * 80); // toneladas/ha

                case "tomate":
                    return Math.Round(areaEnHectareas * 30000); // kg/ha

                case "berenjena":
                    return Math.Round(areaEnHectareas * 25000); // kg/ha

                case "ají":
                case "pimiento":
                    return Math.Round(areaEnHectareas * 20000); // kg/ha

                case "cebolla":
                    return Math.Round(areaEnHectareas * 15000); // kg/ha

                case "zanahoria":
                    return Math.Round(areaEnHectareas * 18000); // kg/ha

                case "repollo":
                    return Math.Round(areaEnHectareas * 25000); // kg/ha

                case "lechuga":
                    return Math.Round(areaEnHectareas * 15000); // kg/ha

                default:
                    return 0; // Valor desconocido
            }
        }

        private string ObtenerUnidadCosecha(string nombreCultivo)
        {
            // Retorna la unidad de medida para la cosecha según el cultivo
            if (nombreCultivo.ToLower() == "caña de azúcar")
            {
                return "toneladas";
            }
            else
            {
                return "kg";
            }
        }

        private void cmbCultivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si selecciona un encabezado de categoría, no hacer nada
            if (cmbCultivos.SelectedItem != null &&
                (cmbCultivos.SelectedItem.ToString() ?? string.Empty).StartsWith("--"))
            {
                btnComprobar.Enabled = false;
                return;
            }

            btnComprobar.Enabled = true;

            // Seleccionar tipo de cultivo y categoría apropiados según el cultivo
            if (cmbCultivos.SelectedItem != null)
            {
                string cultivo = cmbCultivos.SelectedItem.ToString() ?? string.Empty;

                // Seleccionar tipo de cultivo predeterminado
                cmbTipoCultivo.SelectedIndex = 0; // "Orgánica" por defecto

                // Seleccionar categoría según el cultivo
                if (cultivo == "Tabaco" || cultivo == "Caña de Azúcar")
                {
                    // Seleccionar "Industrial"
                    for (int i = 0; i < cmbCategoriaCultivo.Items.Count; i++)
                    {
                        if (Convert.ToString(cmbCategoriaCultivo.Items[i]) == "Industrial")
                        {
                            cmbCategoriaCultivo.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else if (cultivo == "Moringa" || cultivo == "Sábila")
                {
                    // Seleccionar "Medicinal"
                    for (int i = 0; i < cmbCategoriaCultivo.Items.Count; i++)
                    {
                        if (Convert.ToString(cmbCategoriaCultivo.Items[i]) == "Medicinal")
                        {
                            cmbCategoriaCultivo.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    // Por defecto "Comestible"
                    for (int i = 0; i < cmbCategoriaCultivo.Items.Count; i++)
                    {
                        if (Convert.ToString(cmbCategoriaCultivo.Items[i]) == "Comestible")
                        {
                            cmbCategoriaCultivo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private static string ValorODatoFaltante(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? "sin registrar" : valor;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

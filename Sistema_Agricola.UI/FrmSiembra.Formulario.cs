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
        private void FrmSiembra_Load(object sender, EventArgs e)
        {
            if (omitirCargaParaPruebaVisual)
            {
                return;
            }

            try
            {
                // Inicializar componentes de captura y calendario
                // Cargar lista de cultivos dominicanos
                CargarCultivos();
                CargarTerrenosParaSiembra();

                cmbEstaciones.Items.Clear();
                cmbEstaciones.Items.AddRange(new object[]
                    { "Período lluvioso", "Período seco", "Transición" });

                // Agregar tipos de suelo al comboBox si no existen
                if (!cmbTipoSuelo.Items.Contains("Arcilloso"))
                {
                    cmbTipoSuelo.Items.Add("Arcilloso");
                    cmbTipoSuelo.Items.Add("Arenoso");
                    cmbTipoSuelo.Items.Add("Franco");
                    cmbTipoSuelo.Items.Add("Limoso");
                    cmbTipoSuelo.Items.Add("Franco arenoso");
                    cmbTipoSuelo.Items.Add("Franco arcilloso");
                    // Seleccionar un tipo por defecto
                    cmbTipoSuelo.SelectedIndex = 2; // Franco
                }

                // Crear fechaCosecha si no existe
                if (dtpFechaCosecha == null)
                {
                    this.dtpFechaCosecha = new DateTimePicker();
                    this.dtpFechaCosecha.Format = DateTimePickerFormat.Short;
                    this.dtpFechaCosecha.Location = new Point(dtpFechadeSiembra.Location.X, dtpFechadeSiembra.Location.Y + 50);
                    this.dtpFechaCosecha.Name = "dtpFechaCosecha";
                    this.dtpFechaCosecha.Size = dtpFechadeSiembra.Size;
                    this.Controls.Add(dtpFechaCosecha);

                    // Añadir etiqueta para fecha de cosecha
                    Label lblFechaCosecha = new Label();
                    lblFechaCosecha.AutoSize = true;
                    lblFechaCosecha.Text = "Fecha de Cosecha:";
                    lblFechaCosecha.Location = new Point(label3.Location.X, label3.Location.Y + 50);
                    lblFechaCosecha.Font = label3.Font;
                    this.Controls.Add(lblFechaCosecha);
                }
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(this, ex, "FrmSiembra.Inicializar");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void txtIDSiembra_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCantidadUtilizada_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y un punto decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo un punto decimal
            if (e.KeyChar == '.' && sender is TextBox texto && texto.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtCostodeFertilizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void LimpiarFormulario(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else if (c is ComboBox)
                    ((ComboBox)c).SelectedIndex = -1;
                else if (c is NumericUpDown)
                    ((NumericUpDown)c).Value = ((NumericUpDown)c).Minimum;
                else if (c is CheckBox)
                    ((CheckBox)c).Checked = false;
                else if (c is RadioButton)
                    ((RadioButton)c).Checked = false;
                else if (c is DateTimePicker)
                    ((DateTimePicker)c).Value = DateTime.Now;
                else if (c.HasChildren)
                    LimpiarFormulario(c);
            }

            // Limpiar el indicador de condiciones
            lblFactibilidad.Text = "Recomendación: Sin evaluar";
            lblFactibilidad.ForeColor = Color.Black;
            lblDetalleRecomendacion.Text = "";
            evaluacionActual = null;

            // Reiniciar estado de edición
            esModoEdicion = false;
            idCultivoEdicion = -1;
            ConfigurarTiposAutomaticos();
            CargarTerrenosParaSiembra();

            // Generar nuevo ID
            GenerarNuevoID();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(this);
        }

        private void txtIDSiembra_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerarNuevoID();
        }

        private void GenerarNuevoID()
        {
            string nuevoId = DateTime.Now.ToString("yyyyMMddHHmmss"); // Formato: AñoMesDíaHoraMinutoSegundo
            txtIDSiembra.Text = nuevoId;
        }

        // Evento para evaluar las condiciones del cultivo
    }
}

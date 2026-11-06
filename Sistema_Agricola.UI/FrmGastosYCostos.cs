using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Agricola.UI
{
    public partial class FrmGastosYCostos : Form
    {
        public FrmGastosYCostos()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Numeros*/
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {

                e.Handled = true;
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Solo Texto*/
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(this);
        }

        private void btnGeneralID_Click(object sender, EventArgs e)
        {
            string nuevoId = DateTime.Now.ToString("yyyyMMddHHmmss"); // Formato: AñoMesDíaHoraMinutoSegundo
            txtIDGastos.Text = nuevoId;
        }
    }
}

using Sistema_Agricola.BLL;
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


    public partial class FrmReporte_Maquinarias : Form
    {
        public FrmReporte_Maquinarias()
        {
            InitializeComponent();
            cbMaquinaria.Visible = false;
            txtBuscarNombre.Visible = false;
        }





        private void btnBuscarM_Click(object sender, EventArgs e)
        {
            dgvReporteM.DataSource = BLL_Maquinaria.BuscarMaquinaria(txtBuscarNombre.Text);


            if (cbRM.Text == "Tipo de Maquinaria")
            {
                if (cbMaquinaria.Text == "Tractor")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Tractor();
                }
                if (cbMaquinaria.Text == "Pulverizadora")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Pulverizadora();
                }
                if (cbMaquinaria.Text == "Cosechadora")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Cosechadora();
                }
                if (cbMaquinaria.Text == "Cargador Frontal")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.CargadorFrontal();
                }
                if (cbMaquinaria.Text == "Equipo de forraje")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Equipodeforraje();
                }
                if (cbMaquinaria.Text == "Labranza")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Labranza();
                }
                if (cbMaquinaria.Text == "Sembradora")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Sembradora();
                }
                if (cbMaquinaria.Text == "Desmalezadora")
                {
                    dgvReporteM.DataSource = BLL_Maquinaria.Desmalezadora();
                }
            }
        }

        private void FrmReporte_Maquinarias_Load(object sender, EventArgs e)
        {


        }

        private void cbRM_Enter(object sender, EventArgs e)
        {
            if (cbRM.Text == "Tipo de Maquinaria")
            {
                cbMaquinaria.Visible = true;
            }
        }

        private void cbRM_TextChanged(object sender, EventArgs e)
        {
            if (cbRM.Text == "Tipo de Maquinaria")
            {
                txtBuscarNombre.Visible = false;
                cbMaquinaria.Visible = true;
            }

            if (cbRM.Text == "Nombre")
            {
                cbMaquinaria.Visible = false;
                txtBuscarNombre.Visible = true;
            }

        }

        private void txtBuscarNombre_Enter(object sender, EventArgs e)
        {

        }

        private void txtBuscarNombre_Leave(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

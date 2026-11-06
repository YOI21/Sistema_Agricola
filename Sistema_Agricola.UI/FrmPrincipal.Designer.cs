using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Sistema_Agricola.UI
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            panelSuperior = new Panel();
            panelBuscador = new Panel();
            txtBuscar = new TextBox();
            btnBuscar = new PictureBox();
            lblFechaHora = new Label();
            lblTituloPanel = new Label();
            btnMinimizar = new PictureBox();
            btnMaximizar = new PictureBox();
            btnCerrar = new PictureBox();
            btnRestaurar = new PictureBox();
            panelMenu = new Panel();
            panelMenuDeslizable = new Panel();
            panelSubMenuReportes = new Panel();
            btnReportInsumo = new Button();
            btnReportFinanciero = new Button();
            btnReportCrecimiento = new Button();
            btnReportProduccion = new Button();
            btnReportSiembras = new Button();
            btnReportes = new Button();
            panelSubMenuMantenimiento = new Panel();
            btnConfigSistema = new Button();
            btnMantEquipos = new Button();
            btnMantMaquinaria = new Button();
            btnMantenimiento = new Button();
            panelSubMenuAdministracion = new Panel();
            btnInventario = new Button();
            btnFinanzas = new Button();
            btnAdministracion = new Button();
            panelSubMenuPersonal = new Panel();
            btnRegistroAgricultor = new Button();
            btnRegistroTrabajadores = new Button();
            btnPersonal = new Button();
            panelSubMenuMaquinaria = new Panel();
            btnAsignacionMaquinaria = new Button();
            btnRegistroMaquinaria = new Button();
            btnMaquinaria = new Button();
            panelSubMenuRecursos = new Panel();
            btnInsumos = new Button();
            btnRecursos = new Button();
            panelSubMenuCultivos = new Panel();
            btnAlertas = new Button();
            btnControlPlagas = new Button();
            btnProcesoTerreno = new Button();
            btnSeguimiento = new Button();
            btnProduccion = new Button();
            btnCosechas = new Button();
            btnSiembras = new Button();
            btnCultivos = new Button();
            btnDashboard = new Button();
            panelLogo = new Panel();
            panelInfoUsuario = new Panel();
            btnCerrarSesion = new Label();
            lblUsuarioActual = new Label();
            pictureUser = new PictureBox();
            pictureLogo = new PictureBox();
            panelCentral = new Panel();
            panelBienvenida = new Panel();
            lblBienvenidaSubtitulo = new Label();
            lblBienvenidaTitulo = new Label();
            pictureFondo = new PictureBox();
            panelSuperior.SuspendLayout();
            panelBuscador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnBuscar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMaximizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnRestaurar).BeginInit();
            panelMenu.SuspendLayout();
            panelMenuDeslizable.SuspendLayout();
            panelSubMenuReportes.SuspendLayout();
            panelSubMenuMantenimiento.SuspendLayout();
            panelSubMenuAdministracion.SuspendLayout();
            panelSubMenuPersonal.SuspendLayout();
            panelSubMenuMaquinaria.SuspendLayout();
            panelSubMenuRecursos.SuspendLayout();
            panelSubMenuCultivos.SuspendLayout();
            panelLogo.SuspendLayout();
            panelInfoUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            panelCentral.SuspendLayout();
            panelBienvenida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureFondo).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(245, 247, 250);
            panelSuperior.BorderStyle = BorderStyle.FixedSingle;
            panelSuperior.Controls.Add(panelBuscador);
            panelSuperior.Controls.Add(lblFechaHora);
            panelSuperior.Controls.Add(lblTituloPanel);
            panelSuperior.Controls.Add(btnMinimizar);
            panelSuperior.Controls.Add(btnMaximizar);
            panelSuperior.Controls.Add(btnCerrar);
            panelSuperior.Controls.Add(btnRestaurar);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(1300, 60);
            panelSuperior.TabIndex = 0;
            // 
            // panelBuscador
            // 
            panelBuscador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelBuscador.BackColor = Color.FromArgb(220, 226, 235);
            panelBuscador.BorderStyle = BorderStyle.FixedSingle;
            panelBuscador.Controls.Add(txtBuscar);
            panelBuscador.Controls.Add(btnBuscar);
            panelBuscador.Location = new Point(750, 15);
            panelBuscador.Name = "panelBuscador";
            panelBuscador.Size = new Size(300, 30);
            panelBuscador.TabIndex = 6;
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(220, 226, 235);
            txtBuscar.BorderStyle = BorderStyle.None;
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.ForeColor = Color.FromArgb(44, 62, 80);
            txtBuscar.Location = new Point(35, 0);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar...";
            txtBuscar.Size = new Size(263, 25);
            txtBuscar.TabIndex = 1;
            txtBuscar.Text = "Buscar...";
            // 
            // btnBuscar
            // 
            btnBuscar.Dock = DockStyle.Left;
            btnBuscar.Location = new Point(0, 0);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(35, 28);
            btnBuscar.SizeMode = PictureBoxSizeMode.CenterImage;
            btnBuscar.TabIndex = 0;
            btnBuscar.TabStop = false;
            // 
            // lblFechaHora
            // 
            lblFechaHora.AutoSize = true;
            lblFechaHora.Font = new Font("Segoe UI Semibold", 10F);
            lblFechaHora.ForeColor = Color.FromArgb(91, 124, 153);
            lblFechaHora.Location = new Point(410, 20);
            lblFechaHora.Name = "lblFechaHora";
            lblFechaHora.Size = new Size(72, 23);
            lblFechaHora.TabIndex = 5;
            lblFechaHora.Text = "00:00:00";
            // 
            // lblTituloPanel
            // 
            lblTituloPanel.AutoSize = true;
            lblTituloPanel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTituloPanel.ForeColor = Color.FromArgb(62, 79, 98);
            lblTituloPanel.Location = new Point(20, 15);
            lblTituloPanel.Name = "lblTituloPanel";
            lblTituloPanel.Size = new Size(274, 37);
            lblTituloPanel.TabIndex = 4;
            lblTituloPanel.Text = "SISTEMA AGRÍCOLA";
            // 
            // btnMinimizar
            // 
            btnMinimizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimizar.BackColor = Color.FromArgb(91, 124, 153);
            btnMinimizar.BorderStyle = BorderStyle.FixedSingle;
            btnMinimizar.Cursor = Cursors.Hand;
            btnMinimizar.Location = new Point(1189, 15);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(30, 30);
            btnMinimizar.SizeMode = PictureBoxSizeMode.CenterImage;
            btnMinimizar.TabIndex = 3;
            btnMinimizar.TabStop = false;
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // btnMaximizar
            // 
            btnMaximizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximizar.BackColor = Color.FromArgb(91, 124, 153);
            btnMaximizar.BorderStyle = BorderStyle.FixedSingle;
            btnMaximizar.Cursor = Cursors.Hand;
            btnMaximizar.Location = new Point(1224, 15);
            btnMaximizar.Name = "btnMaximizar";
            btnMaximizar.Size = new Size(30, 30);
            btnMaximizar.SizeMode = PictureBoxSizeMode.CenterImage;
            btnMaximizar.TabIndex = 2;
            btnMaximizar.TabStop = false;
            btnMaximizar.Click += btnMaximizar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(231, 76, 60);
            btnCerrar.BorderStyle = BorderStyle.FixedSingle;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Location = new Point(1259, 15);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(30, 30);
            btnCerrar.SizeMode = PictureBoxSizeMode.CenterImage;
            btnCerrar.TabIndex = 1;
            btnCerrar.TabStop = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnRestaurar
            // 
            btnRestaurar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRestaurar.BackColor = Color.FromArgb(91, 124, 153);
            btnRestaurar.BorderStyle = BorderStyle.FixedSingle;
            btnRestaurar.Cursor = Cursors.Hand;
            btnRestaurar.Location = new Point(1224, 15);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(30, 30);
            btnRestaurar.SizeMode = PictureBoxSizeMode.Zoom;
            btnRestaurar.TabIndex = 0;
            btnRestaurar.TabStop = false;
            btnRestaurar.Visible = false;
            btnRestaurar.Click += btnRestaurar_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(62, 79, 98);
            panelMenu.BorderStyle = BorderStyle.FixedSingle;
            panelMenu.Controls.Add(panelMenuDeslizable);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 60);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 689);
            panelMenu.TabIndex = 1;
            // 
            // panelMenuDeslizable
            // 
            panelMenuDeslizable.AutoScroll = true;
            panelMenuDeslizable.BackColor = Color.FromArgb(62, 79, 98);
            panelMenuDeslizable.Controls.Add(panelSubMenuReportes);
            panelMenuDeslizable.Controls.Add(btnReportes);
            panelMenuDeslizable.Controls.Add(panelSubMenuMantenimiento);
            panelMenuDeslizable.Controls.Add(btnMantenimiento);
            panelMenuDeslizable.Controls.Add(panelSubMenuAdministracion);
            panelMenuDeslizable.Controls.Add(btnAdministracion);
            panelMenuDeslizable.Controls.Add(panelSubMenuPersonal);
            panelMenuDeslizable.Controls.Add(btnPersonal);
            panelMenuDeslizable.Controls.Add(panelSubMenuMaquinaria);
            panelMenuDeslizable.Controls.Add(btnMaquinaria);
            panelMenuDeslizable.Controls.Add(panelSubMenuRecursos);
            panelMenuDeslizable.Controls.Add(btnRecursos);
            panelMenuDeslizable.Controls.Add(panelSubMenuCultivos);
            panelMenuDeslizable.Controls.Add(btnCultivos);
            panelMenuDeslizable.Controls.Add(btnDashboard);
            panelMenuDeslizable.Controls.Add(panelLogo);
            panelMenuDeslizable.Dock = DockStyle.Fill;
            panelMenuDeslizable.Location = new Point(0, 0);
            panelMenuDeslizable.Name = "panelMenuDeslizable";
            panelMenuDeslizable.Size = new Size(248, 687);
            panelMenuDeslizable.TabIndex = 0;
            // 
            // panelSubMenuReportes
            // 
            panelSubMenuReportes.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuReportes.Controls.Add(btnReportInsumo);
            panelSubMenuReportes.Controls.Add(btnReportFinanciero);
            panelSubMenuReportes.Controls.Add(btnReportCrecimiento);
            panelSubMenuReportes.Controls.Add(btnReportProduccion);
            panelSubMenuReportes.Controls.Add(btnReportSiembras);
            panelSubMenuReportes.Dock = DockStyle.Top;
            panelSubMenuReportes.Location = new Point(0, 1110);
            panelSubMenuReportes.Name = "panelSubMenuReportes";
            panelSubMenuReportes.Size = new Size(227, 175);
            panelSubMenuReportes.TabIndex = 15;
            panelSubMenuReportes.Visible = false;
            // 
            // btnReportInsumo
            // 
            btnReportInsumo.Dock = DockStyle.Top;
            btnReportInsumo.FlatAppearance.BorderSize = 0;
            btnReportInsumo.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportInsumo.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnReportInsumo.FlatStyle = FlatStyle.Flat;
            btnReportInsumo.Font = new Font("Segoe UI Semibold", 10F);
            btnReportInsumo.ForeColor = Color.White;
            btnReportInsumo.Location = new Point(0, 152);
            btnReportInsumo.Name = "btnReportInsumo";
            btnReportInsumo.Padding = new Padding(35, 0, 0, 0);
            btnReportInsumo.Size = new Size(227, 38);
            btnReportInsumo.TabIndex = 4;
            btnReportInsumo.Text = "Reportes de insumos";
            btnReportInsumo.TextAlign = ContentAlignment.MiddleLeft;
            btnReportInsumo.UseVisualStyleBackColor = true;
            btnReportInsumo.Click += btnReportInsumo_Click;
            // 
            // btnReportFinanciero
            // 
            btnReportFinanciero.Dock = DockStyle.Top;
            btnReportFinanciero.FlatAppearance.BorderSize = 0;
            btnReportFinanciero.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportFinanciero.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnReportFinanciero.FlatStyle = FlatStyle.Flat;
            btnReportFinanciero.Font = new Font("Segoe UI Semibold", 10F);
            btnReportFinanciero.ForeColor = Color.White;
            btnReportFinanciero.Location = new Point(0, 114);
            btnReportFinanciero.Name = "btnReportFinanciero";
            btnReportFinanciero.Padding = new Padding(35, 0, 0, 0);
            btnReportFinanciero.Size = new Size(227, 38);
            btnReportFinanciero.TabIndex = 3;
            btnReportFinanciero.Text = "Reporte financiero";
            btnReportFinanciero.TextAlign = ContentAlignment.MiddleLeft;
            btnReportFinanciero.UseVisualStyleBackColor = true;
            btnReportFinanciero.Click += btnReportFinanciero_Click;
            // 
            // btnReportCrecimiento
            // 
            btnReportCrecimiento.Dock = DockStyle.Top;
            btnReportCrecimiento.FlatAppearance.BorderSize = 0;
            btnReportCrecimiento.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportCrecimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnReportCrecimiento.FlatStyle = FlatStyle.Flat;
            btnReportCrecimiento.Font = new Font("Segoe UI Semibold", 10F);
            btnReportCrecimiento.ForeColor = Color.White;
            btnReportCrecimiento.Location = new Point(0, 76);
            btnReportCrecimiento.Name = "btnReportCrecimiento";
            btnReportCrecimiento.Padding = new Padding(35, 0, 0, 0);
            btnReportCrecimiento.Size = new Size(227, 38);
            btnReportCrecimiento.TabIndex = 2;
            btnReportCrecimiento.Text = "Reportes de Crecimiento";
            btnReportCrecimiento.TextAlign = ContentAlignment.MiddleLeft;
            btnReportCrecimiento.UseVisualStyleBackColor = true;
            btnReportCrecimiento.Click += btnReportCrecimiento_Click;
            // 
            // btnReportProduccion
            // 
            btnReportProduccion.Dock = DockStyle.Top;
            btnReportProduccion.FlatAppearance.BorderSize = 0;
            btnReportProduccion.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportProduccion.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnReportProduccion.FlatStyle = FlatStyle.Flat;
            btnReportProduccion.Font = new Font("Segoe UI Semibold", 10F);
            btnReportProduccion.ForeColor = Color.White;
            btnReportProduccion.Location = new Point(0, 38);
            btnReportProduccion.Name = "btnReportProduccion";
            btnReportProduccion.Padding = new Padding(35, 0, 0, 0);
            btnReportProduccion.Size = new Size(227, 38);
            btnReportProduccion.TabIndex = 1;
            btnReportProduccion.Text = "Reporte de Producción";
            btnReportProduccion.TextAlign = ContentAlignment.MiddleLeft;
            btnReportProduccion.UseVisualStyleBackColor = true;
            btnReportProduccion.Click += btnReportProduccion_Click;
            // 
            // btnReportSiembras
            // 
            btnReportSiembras.Dock = DockStyle.Top;
            btnReportSiembras.FlatAppearance.BorderSize = 0;
            btnReportSiembras.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportSiembras.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnReportSiembras.FlatStyle = FlatStyle.Flat;
            btnReportSiembras.Font = new Font("Segoe UI Semibold", 10F);
            btnReportSiembras.ForeColor = Color.White;
            btnReportSiembras.Location = new Point(0, 0);
            btnReportSiembras.Name = "btnReportSiembras";
            btnReportSiembras.Padding = new Padding(35, 0, 0, 0);
            btnReportSiembras.Size = new Size(227, 38);
            btnReportSiembras.TabIndex = 0;
            btnReportSiembras.Text = "Reportes de Siembras";
            btnReportSiembras.TextAlign = ContentAlignment.MiddleLeft;
            btnReportSiembras.UseVisualStyleBackColor = true;
            btnReportSiembras.Click += btnReportesCultivos_Click;
            // 
            // btnReportes
            // 
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnReportes.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportes.Location = new Point(0, 1060);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(12, 0, 0, 0);
            btnReportes.Size = new Size(227, 50);
            btnReportes.TabIndex = 14;
            btnReportes.Text = "  📊 Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // panelSubMenuMantenimiento
            // 
            panelSubMenuMantenimiento.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuMantenimiento.Controls.Add(btnConfigSistema);
            panelSubMenuMantenimiento.Controls.Add(btnMantEquipos);
            panelSubMenuMantenimiento.Controls.Add(btnMantMaquinaria);
            panelSubMenuMantenimiento.Dock = DockStyle.Top;
            panelSubMenuMantenimiento.Location = new Point(0, 955);
            panelSubMenuMantenimiento.Name = "panelSubMenuMantenimiento";
            panelSubMenuMantenimiento.Size = new Size(227, 105);
            panelSubMenuMantenimiento.TabIndex = 13;
            panelSubMenuMantenimiento.Visible = false;
            // 
            // btnConfigSistema
            // 
            btnConfigSistema.Dock = DockStyle.Top;
            btnConfigSistema.FlatAppearance.BorderSize = 0;
            btnConfigSistema.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnConfigSistema.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnConfigSistema.FlatStyle = FlatStyle.Flat;
            btnConfigSistema.Font = new Font("Segoe UI Semibold", 10F);
            btnConfigSistema.ForeColor = Color.White;
            btnConfigSistema.Location = new Point(0, 76);
            btnConfigSistema.Name = "btnConfigSistema";
            btnConfigSistema.Padding = new Padding(35, 0, 0, 0);
            btnConfigSistema.Size = new Size(227, 38);
            btnConfigSistema.TabIndex = 2;
            btnConfigSistema.Text = "Configuración del Sistema";
            btnConfigSistema.TextAlign = ContentAlignment.MiddleLeft;
            btnConfigSistema.UseVisualStyleBackColor = true;
            btnConfigSistema.Click += btnConfigSistema_Click;
            // 
            // btnMantEquipos
            // 
            btnMantEquipos.Dock = DockStyle.Top;
            btnMantEquipos.FlatAppearance.BorderSize = 0;
            btnMantEquipos.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnMantEquipos.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnMantEquipos.FlatStyle = FlatStyle.Flat;
            btnMantEquipos.Font = new Font("Segoe UI Semibold", 10F);
            btnMantEquipos.ForeColor = Color.White;
            btnMantEquipos.Location = new Point(0, 38);
            btnMantEquipos.Name = "btnMantEquipos";
            btnMantEquipos.Padding = new Padding(35, 0, 0, 0);
            btnMantEquipos.Size = new Size(227, 38);
            btnMantEquipos.TabIndex = 1;
            btnMantEquipos.Text = "Mantenimiento de equipos";
            btnMantEquipos.TextAlign = ContentAlignment.MiddleLeft;
            btnMantEquipos.UseVisualStyleBackColor = true;
            btnMantEquipos.Click += btnMantEquipos_Click;
            // 
            // btnMantMaquinaria
            // 
            btnMantMaquinaria.Dock = DockStyle.Top;
            btnMantMaquinaria.FlatAppearance.BorderSize = 0;
            btnMantMaquinaria.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnMantMaquinaria.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnMantMaquinaria.FlatStyle = FlatStyle.Flat;
            btnMantMaquinaria.Font = new Font("Segoe UI Semibold", 10F);
            btnMantMaquinaria.ForeColor = Color.White;
            btnMantMaquinaria.Location = new Point(0, 0);
            btnMantMaquinaria.Name = "btnMantMaquinaria";
            btnMantMaquinaria.Padding = new Padding(35, 0, 0, 0);
            btnMantMaquinaria.Size = new Size(227, 38);
            btnMantMaquinaria.TabIndex = 0;
            btnMantMaquinaria.Text = "Mantenimiento de Maquinaria";
            btnMantMaquinaria.TextAlign = ContentAlignment.MiddleLeft;
            btnMantMaquinaria.UseVisualStyleBackColor = true;
            btnMantMaquinaria.Click += btnMantMaquinaria_Click;
            // 
            // btnMantenimiento
            // 
            btnMantenimiento.Dock = DockStyle.Top;
            btnMantenimiento.FlatAppearance.BorderSize = 0;
            btnMantenimiento.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnMantenimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnMantenimiento.FlatStyle = FlatStyle.Flat;
            btnMantenimiento.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnMantenimiento.ForeColor = Color.White;
            btnMantenimiento.ImageAlign = ContentAlignment.MiddleLeft;
            btnMantenimiento.Location = new Point(0, 905);
            btnMantenimiento.Name = "btnMantenimiento";
            btnMantenimiento.Padding = new Padding(12, 0, 0, 0);
            btnMantenimiento.Size = new Size(227, 50);
            btnMantenimiento.TabIndex = 12;
            btnMantenimiento.Text = "  🔧 Mantenimiento";
            btnMantenimiento.TextAlign = ContentAlignment.MiddleLeft;
            btnMantenimiento.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMantenimiento.UseVisualStyleBackColor = true;
            btnMantenimiento.Click += btnMantenimiento_Click;
            // 
            // panelSubMenuAdministracion
            // 
            panelSubMenuAdministracion.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuAdministracion.Controls.Add(btnInventario);
            panelSubMenuAdministracion.Controls.Add(btnFinanzas);
            panelSubMenuAdministracion.Dock = DockStyle.Top;
            panelSubMenuAdministracion.Location = new Point(0, 835);
            panelSubMenuAdministracion.Name = "panelSubMenuAdministracion";
            panelSubMenuAdministracion.Size = new Size(227, 70);
            panelSubMenuAdministracion.TabIndex = 11;
            panelSubMenuAdministracion.Visible = false;
            // 
            // btnInventario
            // 
            btnInventario.Dock = DockStyle.Top;
            btnInventario.FlatAppearance.BorderSize = 0;
            btnInventario.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnInventario.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnInventario.FlatStyle = FlatStyle.Flat;
            btnInventario.Font = new Font("Segoe UI Semibold", 10F);
            btnInventario.ForeColor = Color.White;
            btnInventario.Location = new Point(0, 38);
            btnInventario.Name = "btnInventario";
            btnInventario.Padding = new Padding(35, 0, 0, 0);
            btnInventario.Size = new Size(227, 38);
            btnInventario.TabIndex = 1;
            btnInventario.Text = "Inventario";
            btnInventario.TextAlign = ContentAlignment.MiddleLeft;
            btnInventario.UseVisualStyleBackColor = true;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnFinanzas
            // 
            btnFinanzas.Dock = DockStyle.Top;
            btnFinanzas.FlatAppearance.BorderSize = 0;
            btnFinanzas.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnFinanzas.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnFinanzas.FlatStyle = FlatStyle.Flat;
            btnFinanzas.Font = new Font("Segoe UI Semibold", 10F);
            btnFinanzas.ForeColor = Color.White;
            btnFinanzas.Location = new Point(0, 0);
            btnFinanzas.Name = "btnFinanzas";
            btnFinanzas.Padding = new Padding(35, 0, 0, 0);
            btnFinanzas.Size = new Size(227, 38);
            btnFinanzas.TabIndex = 0;
            btnFinanzas.Text = "Finanzas";
            btnFinanzas.TextAlign = ContentAlignment.MiddleLeft;
            btnFinanzas.UseVisualStyleBackColor = true;
            btnFinanzas.Click += btnFinanzas_Click;
            // 
            // btnAdministracion
            // 
            btnAdministracion.Dock = DockStyle.Top;
            btnAdministracion.FlatAppearance.BorderSize = 0;
            btnAdministracion.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnAdministracion.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnAdministracion.FlatStyle = FlatStyle.Flat;
            btnAdministracion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAdministracion.ForeColor = Color.White;
            btnAdministracion.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdministracion.Location = new Point(0, 785);
            btnAdministracion.Name = "btnAdministracion";
            btnAdministracion.Padding = new Padding(12, 0, 0, 0);
            btnAdministracion.Size = new Size(227, 50);
            btnAdministracion.TabIndex = 10;
            btnAdministracion.Text = "  📋 Administración";
            btnAdministracion.TextAlign = ContentAlignment.MiddleLeft;
            btnAdministracion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdministracion.UseVisualStyleBackColor = true;
            btnAdministracion.Click += btnAdministracion_Click;
            // 
            // panelSubMenuPersonal
            // 
            panelSubMenuPersonal.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuPersonal.Controls.Add(btnRegistroAgricultor);
            panelSubMenuPersonal.Controls.Add(btnRegistroTrabajadores);
            panelSubMenuPersonal.Dock = DockStyle.Top;
            panelSubMenuPersonal.Location = new Point(0, 715);
            panelSubMenuPersonal.Name = "panelSubMenuPersonal";
            panelSubMenuPersonal.Size = new Size(227, 70);
            panelSubMenuPersonal.TabIndex = 9;
            panelSubMenuPersonal.Visible = false;
            // 
            // btnRegistroAgricultor
            // 
            btnRegistroAgricultor.Dock = DockStyle.Top;
            btnRegistroAgricultor.FlatAppearance.BorderSize = 0;
            btnRegistroAgricultor.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnRegistroAgricultor.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnRegistroAgricultor.FlatStyle = FlatStyle.Flat;
            btnRegistroAgricultor.Font = new Font("Segoe UI Semibold", 10F);
            btnRegistroAgricultor.ForeColor = Color.White;
            btnRegistroAgricultor.Location = new Point(0, 38);
            btnRegistroAgricultor.Name = "btnRegistroAgricultor";
            btnRegistroAgricultor.Padding = new Padding(35, 0, 0, 0);
            btnRegistroAgricultor.Size = new Size(227, 38);
            btnRegistroAgricultor.TabIndex = 1;
            btnRegistroAgricultor.Text = "Registro de Agricultor";
            btnRegistroAgricultor.TextAlign = ContentAlignment.MiddleLeft;
            btnRegistroAgricultor.UseVisualStyleBackColor = true;
            btnRegistroAgricultor.Click += btnAsignacionTrabajador_Click;
            // 
            // btnRegistroTrabajadores
            // 
            btnRegistroTrabajadores.Dock = DockStyle.Top;
            btnRegistroTrabajadores.FlatAppearance.BorderSize = 0;
            btnRegistroTrabajadores.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnRegistroTrabajadores.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnRegistroTrabajadores.FlatStyle = FlatStyle.Flat;
            btnRegistroTrabajadores.Font = new Font("Segoe UI Semibold", 10F);
            btnRegistroTrabajadores.ForeColor = Color.White;
            btnRegistroTrabajadores.Location = new Point(0, 0);
            btnRegistroTrabajadores.Name = "btnRegistroTrabajadores";
            btnRegistroTrabajadores.Padding = new Padding(35, 0, 0, 0);
            btnRegistroTrabajadores.Size = new Size(227, 38);
            btnRegistroTrabajadores.TabIndex = 0;
            btnRegistroTrabajadores.Text = "Registro de Trabajadores";
            btnRegistroTrabajadores.TextAlign = ContentAlignment.MiddleLeft;
            btnRegistroTrabajadores.UseVisualStyleBackColor = true;
            btnRegistroTrabajadores.Click += btnRegistroTrabahjadores_Click;
            // 
            // btnPersonal
            // 
            btnPersonal.Dock = DockStyle.Top;
            btnPersonal.FlatAppearance.BorderSize = 0;
            btnPersonal.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnPersonal.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnPersonal.FlatStyle = FlatStyle.Flat;
            btnPersonal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPersonal.ForeColor = Color.White;
            btnPersonal.ImageAlign = ContentAlignment.MiddleLeft;
            btnPersonal.Location = new Point(0, 665);
            btnPersonal.Name = "btnPersonal";
            btnPersonal.Padding = new Padding(12, 0, 0, 0);
            btnPersonal.Size = new Size(227, 50);
            btnPersonal.TabIndex = 8;
            btnPersonal.Text = "  👥 Gestión de Personal";
            btnPersonal.TextAlign = ContentAlignment.MiddleLeft;
            btnPersonal.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPersonal.UseVisualStyleBackColor = true;
            btnPersonal.Click += btnPersonal_Click;
            // 
            // panelSubMenuMaquinaria
            // 
            panelSubMenuMaquinaria.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuMaquinaria.Controls.Add(btnAsignacionMaquinaria);
            panelSubMenuMaquinaria.Controls.Add(btnRegistroMaquinaria);
            panelSubMenuMaquinaria.Dock = DockStyle.Top;
            panelSubMenuMaquinaria.Location = new Point(0, 595);
            panelSubMenuMaquinaria.Name = "panelSubMenuMaquinaria";
            panelSubMenuMaquinaria.Size = new Size(227, 70);
            panelSubMenuMaquinaria.TabIndex = 7;
            panelSubMenuMaquinaria.Visible = false;
            // 
            // btnAsignacionMaquinaria
            // 
            btnAsignacionMaquinaria.Dock = DockStyle.Top;
            btnAsignacionMaquinaria.FlatAppearance.BorderSize = 0;
            btnAsignacionMaquinaria.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnAsignacionMaquinaria.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnAsignacionMaquinaria.FlatStyle = FlatStyle.Flat;
            btnAsignacionMaquinaria.Font = new Font("Segoe UI Semibold", 10F);
            btnAsignacionMaquinaria.ForeColor = Color.White;
            btnAsignacionMaquinaria.Location = new Point(0, 38);
            btnAsignacionMaquinaria.Name = "btnAsignacionMaquinaria";
            btnAsignacionMaquinaria.Padding = new Padding(35, 0, 0, 0);
            btnAsignacionMaquinaria.Size = new Size(227, 38);
            btnAsignacionMaquinaria.TabIndex = 1;
            btnAsignacionMaquinaria.Text = "Asignación de Maquinaria";
            btnAsignacionMaquinaria.TextAlign = ContentAlignment.MiddleLeft;
            btnAsignacionMaquinaria.UseVisualStyleBackColor = true;
            btnAsignacionMaquinaria.Click += btnAsignacionMaquinaria_Click;
            // 
            // btnRegistroMaquinaria
            // 
            btnRegistroMaquinaria.Dock = DockStyle.Top;
            btnRegistroMaquinaria.FlatAppearance.BorderSize = 0;
            btnRegistroMaquinaria.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnRegistroMaquinaria.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnRegistroMaquinaria.FlatStyle = FlatStyle.Flat;
            btnRegistroMaquinaria.Font = new Font("Segoe UI Semibold", 10F);
            btnRegistroMaquinaria.ForeColor = Color.White;
            btnRegistroMaquinaria.Location = new Point(0, 0);
            btnRegistroMaquinaria.Name = "btnRegistroMaquinaria";
            btnRegistroMaquinaria.Padding = new Padding(35, 0, 0, 0);
            btnRegistroMaquinaria.Size = new Size(227, 38);
            btnRegistroMaquinaria.TabIndex = 0;
            btnRegistroMaquinaria.Text = "Registro de Maquinaria";
            btnRegistroMaquinaria.TextAlign = ContentAlignment.MiddleLeft;
            btnRegistroMaquinaria.UseVisualStyleBackColor = true;
            btnRegistroMaquinaria.Click += btnRegistroMaquinaria_Click;
            // 
            // btnMaquinaria
            // 
            btnMaquinaria.Dock = DockStyle.Top;
            btnMaquinaria.FlatAppearance.BorderSize = 0;
            btnMaquinaria.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnMaquinaria.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnMaquinaria.FlatStyle = FlatStyle.Flat;
            btnMaquinaria.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnMaquinaria.ForeColor = Color.White;
            btnMaquinaria.ImageAlign = ContentAlignment.MiddleLeft;
            btnMaquinaria.Location = new Point(0, 545);
            btnMaquinaria.Name = "btnMaquinaria";
            btnMaquinaria.Padding = new Padding(10, 0, 0, 0);
            btnMaquinaria.Size = new Size(227, 50);
            btnMaquinaria.TabIndex = 6;
            btnMaquinaria.Text = "  🚜 Gestión de Maquinaria";
            btnMaquinaria.TextAlign = ContentAlignment.MiddleLeft;
            btnMaquinaria.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMaquinaria.UseVisualStyleBackColor = true;
            btnMaquinaria.Click += btnGestionMaquinaria_Click_1;
            // 
            // panelSubMenuRecursos
            // 
            panelSubMenuRecursos.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuRecursos.Controls.Add(btnInsumos);
            panelSubMenuRecursos.Dock = DockStyle.Top;
            panelSubMenuRecursos.Location = new Point(0, 510);
            panelSubMenuRecursos.Name = "panelSubMenuRecursos";
            panelSubMenuRecursos.Size = new Size(227, 35);
            panelSubMenuRecursos.TabIndex = 5;
            panelSubMenuRecursos.Visible = false;
            // 
            // btnInsumos
            // 
            btnInsumos.Dock = DockStyle.Top;
            btnInsumos.FlatAppearance.BorderSize = 0;
            btnInsumos.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnInsumos.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnInsumos.FlatStyle = FlatStyle.Flat;
            btnInsumos.Font = new Font("Segoe UI Semibold", 10F);
            btnInsumos.ForeColor = Color.White;
            btnInsumos.Location = new Point(0, 0);
            btnInsumos.Name = "btnInsumos";
            btnInsumos.Padding = new Padding(35, 0, 0, 0);
            btnInsumos.Size = new Size(227, 38);
            btnInsumos.TabIndex = 0;
            btnInsumos.Text = "Insumos";
            btnInsumos.TextAlign = ContentAlignment.MiddleLeft;
            btnInsumos.UseVisualStyleBackColor = true;
            btnInsumos.Click += btnInsumos_Click_2;
            // 
            // btnRecursos
            // 
            btnRecursos.Dock = DockStyle.Top;
            btnRecursos.FlatAppearance.BorderSize = 0;
            btnRecursos.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnRecursos.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnRecursos.FlatStyle = FlatStyle.Flat;
            btnRecursos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRecursos.ForeColor = Color.White;
            btnRecursos.ImageAlign = ContentAlignment.MiddleLeft;
            btnRecursos.Location = new Point(0, 460);
            btnRecursos.Name = "btnRecursos";
            btnRecursos.Padding = new Padding(12, 0, 0, 0);
            btnRecursos.Size = new Size(227, 50);
            btnRecursos.TabIndex = 4;
            btnRecursos.Text = "  🌿 Gestión de Recursos";
            btnRecursos.TextAlign = ContentAlignment.MiddleLeft;
            btnRecursos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRecursos.UseVisualStyleBackColor = true;
            btnRecursos.Click += btnRecursos_Click;
            // 
            // panelSubMenuCultivos
            // 
            panelSubMenuCultivos.BackColor = Color.FromArgb(91, 110, 135);
            panelSubMenuCultivos.Controls.Add(btnAlertas);
            panelSubMenuCultivos.Controls.Add(btnControlPlagas);
            panelSubMenuCultivos.Controls.Add(btnProcesoTerreno);
            panelSubMenuCultivos.Controls.Add(btnSeguimiento);
            panelSubMenuCultivos.Controls.Add(btnProduccion);
            panelSubMenuCultivos.Controls.Add(btnCosechas);
            panelSubMenuCultivos.Controls.Add(btnSiembras);
            panelSubMenuCultivos.Dock = DockStyle.Top;
            panelSubMenuCultivos.Location = new Point(0, 250);
            panelSubMenuCultivos.Name = "panelSubMenuCultivos";
            panelSubMenuCultivos.Size = new Size(227, 266);
            panelSubMenuCultivos.TabIndex = 3;
            panelSubMenuCultivos.Visible = false;
            // 
            // btnAlertas
            // 
            btnAlertas.Dock = DockStyle.Top;
            btnAlertas.FlatAppearance.BorderSize = 0;
            btnAlertas.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnAlertas.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnAlertas.FlatStyle = FlatStyle.Flat;
            btnAlertas.Font = new Font("Segoe UI Semibold", 10F);
            btnAlertas.ForeColor = Color.White;
            btnAlertas.Location = new Point(0, 228);
            btnAlertas.Name = "btnAlertas";
            btnAlertas.Padding = new Padding(35, 0, 0, 0);
            btnAlertas.Size = new Size(227, 38);
            btnAlertas.TabIndex = 5;
            btnAlertas.Text = "Alertas";
            btnAlertas.TextAlign = ContentAlignment.MiddleLeft;
            btnAlertas.UseVisualStyleBackColor = true;
            btnAlertas.Click += btnAlertas_Click;
            // 
            // btnControlPlagas
            // 
            btnControlPlagas.Dock = DockStyle.Top;
            btnControlPlagas.FlatAppearance.BorderSize = 0;
            btnControlPlagas.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnControlPlagas.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnControlPlagas.FlatStyle = FlatStyle.Flat;
            btnControlPlagas.Font = new Font("Segoe UI Semibold", 10F);
            btnControlPlagas.ForeColor = Color.White;
            btnControlPlagas.Location = new Point(0, 190);
            btnControlPlagas.Name = "btnControlPlagas";
            btnControlPlagas.Padding = new Padding(35, 0, 0, 0);
            btnControlPlagas.Size = new Size(227, 38);
            btnControlPlagas.TabIndex = 4;
            btnControlPlagas.Text = "Control de Plagas";
            btnControlPlagas.TextAlign = ContentAlignment.MiddleLeft;
            btnControlPlagas.UseVisualStyleBackColor = true;
            btnControlPlagas.Click += btnControlPlagas_Click;
            // 
            // btnProcesoTerreno
            // 
            btnProcesoTerreno.Dock = DockStyle.Top;
            btnProcesoTerreno.FlatAppearance.BorderSize = 0;
            btnProcesoTerreno.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnProcesoTerreno.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnProcesoTerreno.FlatStyle = FlatStyle.Flat;
            btnProcesoTerreno.Font = new Font("Segoe UI Semibold", 10F);
            btnProcesoTerreno.ForeColor = Color.White;
            btnProcesoTerreno.Location = new Point(0, 152);
            btnProcesoTerreno.Name = "btnProcesoTerreno";
            btnProcesoTerreno.Padding = new Padding(35, 0, 0, 0);
            btnProcesoTerreno.Size = new Size(227, 38);
            btnProcesoTerreno.TabIndex = 3;
            btnProcesoTerreno.Text = "Proceso del terreno";
            btnProcesoTerreno.TextAlign = ContentAlignment.MiddleLeft;
            btnProcesoTerreno.UseVisualStyleBackColor = true;
            btnProcesoTerreno.Click += btnProcesoTe_Click;
            // 
            // btnSeguimiento
            // 
            btnSeguimiento.Dock = DockStyle.Top;
            btnSeguimiento.FlatAppearance.BorderSize = 0;
            btnSeguimiento.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnSeguimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnSeguimiento.FlatStyle = FlatStyle.Flat;
            btnSeguimiento.Font = new Font("Segoe UI Semibold", 10F);
            btnSeguimiento.ForeColor = Color.White;
            btnSeguimiento.Location = new Point(0, 114);
            btnSeguimiento.Name = "btnSeguimiento";
            btnSeguimiento.Padding = new Padding(35, 0, 0, 0);
            btnSeguimiento.Size = new Size(227, 38);
            btnSeguimiento.TabIndex = 2;
            btnSeguimiento.Text = "Seguimiento de Crecimiento";
            btnSeguimiento.TextAlign = ContentAlignment.MiddleLeft;
            btnSeguimiento.UseVisualStyleBackColor = true;
            btnSeguimiento.Click += btnSeguimiento_Click;
            // 
            // btnProduccion
            // 
            btnProduccion.Dock = DockStyle.Top;
            btnProduccion.FlatAppearance.BorderSize = 0;
            btnProduccion.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnProduccion.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnProduccion.FlatStyle = FlatStyle.Flat;
            btnProduccion.Font = new Font("Segoe UI Semibold", 10F);
            btnProduccion.ForeColor = Color.White;
            btnProduccion.Location = new Point(0, 76);
            btnProduccion.Name = "btnProduccion";
            btnProduccion.Padding = new Padding(35, 0, 0, 0);
            btnProduccion.Size = new Size(227, 38);
            btnProduccion.TabIndex = 2;
            btnProduccion.Text = "Producción";
            btnProduccion.TextAlign = ContentAlignment.MiddleLeft;
            btnProduccion.UseVisualStyleBackColor = true;
            btnProduccion.Click += btnProduccion_Click;
            // 
            // btnCosechas
            // 
            btnCosechas.Dock = DockStyle.Top;
            btnCosechas.FlatAppearance.BorderSize = 0;
            btnCosechas.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnCosechas.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnCosechas.FlatStyle = FlatStyle.Flat;
            btnCosechas.Font = new Font("Segoe UI Semibold", 10F);
            btnCosechas.ForeColor = Color.White;
            btnCosechas.Location = new Point(0, 38);
            btnCosechas.Name = "btnCosechas";
            btnCosechas.Padding = new Padding(35, 0, 0, 0);
            btnCosechas.Size = new Size(227, 38);
            btnCosechas.TabIndex = 1;
            btnCosechas.Text = "Cosechas";
            btnCosechas.TextAlign = ContentAlignment.MiddleLeft;
            btnCosechas.UseVisualStyleBackColor = true;
            btnCosechas.Click += btnCosechas_Click_1;
            // 
            // btnSiembras
            // 
            btnSiembras.Dock = DockStyle.Top;
            btnSiembras.FlatAppearance.BorderSize = 0;
            btnSiembras.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnSiembras.FlatAppearance.MouseOverBackColor = Color.FromArgb(107, 140, 170);
            btnSiembras.FlatStyle = FlatStyle.Flat;
            btnSiembras.Font = new Font("Segoe UI Semibold", 10F);
            btnSiembras.ForeColor = Color.White;
            btnSiembras.Location = new Point(0, 0);
            btnSiembras.Name = "btnSiembras";
            btnSiembras.Padding = new Padding(35, 0, 0, 0);
            btnSiembras.Size = new Size(227, 38);
            btnSiembras.TabIndex = 0;
            btnSiembras.Text = "Siembras";
            btnSiembras.TextAlign = ContentAlignment.MiddleLeft;
            btnSiembras.UseVisualStyleBackColor = true;
            btnSiembras.Click += btnSiembras_Click;
            // 
            // btnCultivos
            // 
            btnCultivos.Dock = DockStyle.Top;
            btnCultivos.FlatAppearance.BorderSize = 0;
            btnCultivos.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnCultivos.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnCultivos.FlatStyle = FlatStyle.Flat;
            btnCultivos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCultivos.ForeColor = Color.White;
            btnCultivos.ImageAlign = ContentAlignment.MiddleLeft;
            btnCultivos.Location = new Point(0, 200);
            btnCultivos.Name = "btnCultivos";
            btnCultivos.Padding = new Padding(12, 0, 0, 0);
            btnCultivos.Size = new Size(227, 50);
            btnCultivos.TabIndex = 2;
            btnCultivos.Text = "  🎏 Gestión de Cultivos";
            btnCultivos.TextAlign = ContentAlignment.MiddleLeft;
            btnCultivos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCultivos.UseVisualStyleBackColor = true;
            btnCultivos.Click += btnCultivos_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 95, 120);
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(85, 108, 135);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(0, 150);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(12, 0, 0, 0);
            btnDashboard.Size = new Size(227, 50);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "  🌈 Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(52, 69, 90);
            panelLogo.Controls.Add(panelInfoUsuario);
            panelLogo.Controls.Add(pictureLogo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(227, 150);
            panelLogo.TabIndex = 0;
            // 
            // panelInfoUsuario
            // 
            panelInfoUsuario.BackColor = Color.FromArgb(52, 69, 90);
            panelInfoUsuario.Controls.Add(btnCerrarSesion);
            panelInfoUsuario.Controls.Add(lblUsuarioActual);
            panelInfoUsuario.Controls.Add(pictureUser);
            panelInfoUsuario.Dock = DockStyle.Bottom;
            panelInfoUsuario.Location = new Point(0, 90);
            panelInfoUsuario.Name = "panelInfoUsuario";
            panelInfoUsuario.Size = new Size(227, 60);
            panelInfoUsuario.TabIndex = 1;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.AutoSize = true;
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            btnCerrarSesion.ForeColor = Color.FromArgb(198, 213, 230);
            btnCerrarSesion.Location = new Point(60, 35);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(96, 20);
            btnCerrarSesion.TabIndex = 2;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // lblUsuarioActual
            // 
            lblUsuarioActual.AutoSize = true;
            lblUsuarioActual.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblUsuarioActual.ForeColor = Color.White;
            lblUsuarioActual.Location = new Point(60, 10);
            lblUsuarioActual.Name = "lblUsuarioActual";
            lblUsuarioActual.Size = new Size(179, 25);
            lblUsuarioActual.TabIndex = 1;
            lblUsuarioActual.Text = "Nombre de Usuario";
            // 
            // pictureUser
            // 
            pictureUser.Location = new Point(15, 10);
            pictureUser.Name = "pictureUser";
            pictureUser.Size = new Size(40, 40);
            pictureUser.SizeMode = PictureBoxSizeMode.Zoom;
            pictureUser.TabIndex = 0;
            pictureUser.TabStop = false;
            // 
            // pictureLogo
            // 
            pictureLogo.Anchor = AnchorStyles.Top;
            pictureLogo.Location = new Point(63, 10);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(100, 70);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 0;
            pictureLogo.TabStop = false;
            // 
            // panelCentral
            // 
            panelCentral.BackColor = Color.FromArgb(250, 251, 252);
            panelCentral.Controls.Add(panelBienvenida);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(250, 60);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(1050, 689);
            panelCentral.TabIndex = 2;
            // 
            // panelBienvenida
            // 
            panelBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBienvenida.BackColor = Color.White;
            panelBienvenida.BorderStyle = BorderStyle.FixedSingle;
            panelBienvenida.Controls.Add(lblBienvenidaSubtitulo);
            panelBienvenida.Controls.Add(lblBienvenidaTitulo);
            panelBienvenida.Controls.Add(pictureFondo);
            panelBienvenida.Location = new Point(30, 30);
            panelBienvenida.Name = "panelBienvenida";
            panelBienvenida.Size = new Size(1000, 629);
            panelBienvenida.TabIndex = 0;
            // 
            // lblBienvenidaSubtitulo
            // 
            lblBienvenidaSubtitulo.Anchor = AnchorStyles.Top;
            lblBienvenidaSubtitulo.Font = new Font("Segoe UI", 14F);
            lblBienvenidaSubtitulo.ForeColor = Color.FromArgb(100, 120, 140);
            lblBienvenidaSubtitulo.Location = new Point(0, 100);
            lblBienvenidaSubtitulo.Name = "lblBienvenidaSubtitulo";
            lblBienvenidaSubtitulo.Size = new Size(1000, 35);
            lblBienvenidaSubtitulo.TabIndex = 1;
            lblBienvenidaSubtitulo.Text = "Gestione sus cultivos, recursos y operaciones agrícolas de manera eficiente e integrada";
            lblBienvenidaSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBienvenidaTitulo
            // 
            lblBienvenidaTitulo.Anchor = AnchorStyles.Top;
            lblBienvenidaTitulo.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblBienvenidaTitulo.ForeColor = Color.FromArgb(91, 124, 153);
            lblBienvenidaTitulo.Location = new Point(0, 35);
            lblBienvenidaTitulo.Name = "lblBienvenidaTitulo";
            lblBienvenidaTitulo.Size = new Size(1000, 60);
            lblBienvenidaTitulo.TabIndex = 0;
            lblBienvenidaTitulo.Text = "✨ BIENVENIDO AL SISTEMA AGRÍCOLA";
            lblBienvenidaTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureFondo
            // 
            pictureFondo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureFondo.Location = new Point(100, 150);
            pictureFondo.Name = "pictureFondo";
            pictureFondo.Size = new Size(810, 470);
            pictureFondo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureFondo.TabIndex = 2;
            pictureFondo.TabStop = false;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 251, 252);
            ClientSize = new Size(1300, 749);
            Controls.Add(panelCentral);
            Controls.Add(panelMenu);
            Controls.Add(panelSuperior);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Agrícola";
            Load += FrmPrincipal_Load;
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelBuscador.ResumeLayout(false);
            panelBuscador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnBuscar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMaximizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnRestaurar).EndInit();
            panelMenu.ResumeLayout(false);
            panelMenuDeslizable.ResumeLayout(false);
            panelSubMenuReportes.ResumeLayout(false);
            panelSubMenuMantenimiento.ResumeLayout(false);
            panelSubMenuAdministracion.ResumeLayout(false);
            panelSubMenuPersonal.ResumeLayout(false);
            panelSubMenuMaquinaria.ResumeLayout(false);
            panelSubMenuRecursos.ResumeLayout(false);
            panelSubMenuCultivos.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelInfoUsuario.ResumeLayout(false);
            panelInfoUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            panelCentral.ResumeLayout(false);
            panelBienvenida.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureFondo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelBuscador;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.PictureBox btnBuscar;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.Label lblTituloPanel;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox btnRestaurar;

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelMenuDeslizable;

        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelInfoUsuario;
        private System.Windows.Forms.Label btnCerrarSesion;
        private System.Windows.Forms.Label lblUsuarioActual;
        private System.Windows.Forms.PictureBox pictureUser;
        private System.Windows.Forms.PictureBox pictureLogo;

        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnCultivos;
        private System.Windows.Forms.Panel panelSubMenuCultivos;
        private System.Windows.Forms.Button btnAlertas;
        private System.Windows.Forms.Button btnControlPlagas;
        private System.Windows.Forms.Button btnProcesoTerreno;
        private System.Windows.Forms.Button btnSeguimiento;
        private System.Windows.Forms.Button btnProduccion;
        private System.Windows.Forms.Button btnCosechas;
        private System.Windows.Forms.Button btnSiembras;

        private System.Windows.Forms.Button btnRecursos;
        private System.Windows.Forms.Panel panelSubMenuRecursos;
        private System.Windows.Forms.Button btnInsumos;

        private System.Windows.Forms.Button btnMaquinaria;
        private System.Windows.Forms.Panel panelSubMenuMaquinaria;
        private System.Windows.Forms.Button btnAsignacionMaquinaria;
        private System.Windows.Forms.Button btnRegistroMaquinaria;

        private System.Windows.Forms.Button btnPersonal;
        private System.Windows.Forms.Panel panelSubMenuPersonal;
        private System.Windows.Forms.Button btnRegistroAgricultor;
        private System.Windows.Forms.Button btnRegistroTrabajadores;

        private System.Windows.Forms.Button btnAdministracion;
        private System.Windows.Forms.Panel panelSubMenuAdministracion;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnFinanzas;

        private System.Windows.Forms.Button btnMantenimiento;
        private System.Windows.Forms.Panel panelSubMenuMantenimiento;
        private System.Windows.Forms.Button btnConfigSistema;
        private System.Windows.Forms.Button btnMantEquipos;
        private System.Windows.Forms.Button btnMantMaquinaria;

        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Panel panelSubMenuReportes;
        private System.Windows.Forms.Button btnReportInsumo;
        private System.Windows.Forms.Button btnReportFinanciero;
        private System.Windows.Forms.Button btnReportCrecimiento;
        private System.Windows.Forms.Button btnReportProduccion;
        private System.Windows.Forms.Button btnReportSiembras;

        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.Panel panelBienvenida;
        private System.Windows.Forms.Label lblBienvenidaSubtitulo;
        private System.Windows.Forms.Label lblBienvenidaTitulo;
        private System.Windows.Forms.PictureBox pictureFondo;
    }
}

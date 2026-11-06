namespace Sistema_Agricola.UI
{
    partial class FrmDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboard));

            // Panel principal superior
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();

            // Panel de resumen (para tarjetas de métricas)
            this.panelResumen = new System.Windows.Forms.TableLayoutPanel();

            // Tarjetas de resumen
            this.cardCultivosActivos = new System.Windows.Forms.Panel();
            this.lblCultivosActivosValor = new System.Windows.Forms.Label();
            this.lblCultivosActivosTexto = new System.Windows.Forms.Label();
            this.picCultivosActivos = new System.Windows.Forms.PictureBox();

            this.cardProximasCosechas = new System.Windows.Forms.Panel();
            this.lblProximasCosechasValor = new System.Windows.Forms.Label();
            this.lblProximasCosechasTexto = new System.Windows.Forms.Label();
            this.picProximasCosechas = new System.Windows.Forms.PictureBox();

            this.cardCantidadSembrada = new System.Windows.Forms.Panel();
            this.lblCantidadSembradaValor = new System.Windows.Forms.Label();
            this.lblCantidadSembradaTexto = new System.Windows.Forms.Label();
            this.picCantidadSembrada = new System.Windows.Forms.PictureBox();

            this.cardAlertas = new System.Windows.Forms.Panel();
            this.lblAlertasValor = new System.Windows.Forms.Label();
            this.lblAlertasTexto = new System.Windows.Forms.Label();
            this.picAlertas = new System.Windows.Forms.PictureBox();

            // Panel principal (2 columnas)
            this.panelContenido = new System.Windows.Forms.TableLayoutPanel();

            // Columna izquierda
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.tabControlDatos = new System.Windows.Forms.TabControl();

            this.tabCultivosActivos = new System.Windows.Forms.TabPage();
            this.dgvCultivosActivos = new System.Windows.Forms.DataGridView();

            this.tabProximasCosechas = new System.Windows.Forms.TabPage();
            this.dgvProximasCosechas = new System.Windows.Forms.DataGridView();

            this.tabSeguimientos = new System.Windows.Forms.TabPage();
            this.dgvUltimosSeguimientos = new System.Windows.Forms.DataGridView();

            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.dgvAlertas = new System.Windows.Forms.DataGridView();

            // Columna derecha (gráficos)
            this.panelDerecho = new System.Windows.Forms.Panel();
            this.panelGraficos = new System.Windows.Forms.TableLayoutPanel();

            // Gráficos
            this.panelDistribucionCultivos = new System.Windows.Forms.Panel();
            this.chartTiposCultivo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblDistribucionCultivos = new System.Windows.Forms.Label();

            this.panelSeguimientoCrecimiento = new System.Windows.Forms.Panel();
            this.chartSeguimiento = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblSeguimientoCrecimiento = new System.Windows.Forms.Label();

            this.panelEstadisticasRendimiento = new System.Windows.Forms.Panel();
            this.chartRendimientos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblEstadisticasRendimiento = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.picCultivosActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProximasCosechas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCantidadSembrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAlertas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultivosActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasCosechas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimosSeguimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTiposCultivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSeguimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRendimientos)).BeginInit();

            this.panelHeader.SuspendLayout();
            this.panelResumen.SuspendLayout();
            this.cardCultivosActivos.SuspendLayout();
            this.cardProximasCosechas.SuspendLayout();
            this.cardCantidadSembrada.SuspendLayout();
            this.cardAlertas.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelIzquierdo.SuspendLayout();
            this.tabControlDatos.SuspendLayout();
            this.tabCultivosActivos.SuspendLayout();
            this.tabProximasCosechas.SuspendLayout();
            this.tabSeguimientos.SuspendLayout();
            this.tabAlertas.SuspendLayout();
            this.panelDerecho.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelDistribucionCultivos.SuspendLayout();
            this.panelSeguimientoCrecimiento.SuspendLayout();
            this.panelEstadisticasRendimiento.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblFechaHora);
            this.panelHeader.Controls.Add(this.btnRefrescar);
            this.panelHeader.Controls.Add(this.btnCerrar);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 0;

            // 
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular);
            this.lblFechaHora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(124)))), ((int)(((byte)(153)))));
            this.lblFechaHora.Location = new System.Drawing.Point(350, 20);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Size = new System.Drawing.Size(170, 23);
            this.lblFechaHora.TabIndex = 2;
            this.lblFechaHora.Text = "01/01/2025 00:00:00";

            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(124)))), ((int)(((byte)(153)))));
            this.btnRefrescar.FlatAppearance.BorderSize = 0;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.btnRefrescar.ForeColor = System.Drawing.Color.White;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Location = new System.Drawing.Point(1070, 15);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(100, 30);
            this.btnRefrescar.TabIndex = 3;
            this.btnRefrescar.Text = "&Actualizar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);

            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Location = new System.Drawing.Point(1170, 15);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(79)))), ((int)(((byte)(98)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(310, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Dashboard";

            // 
            // panelResumen
            // 
            this.panelResumen.ColumnCount = 4;
            this.panelResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelResumen.Controls.Add(this.cardCultivosActivos, 0, 0);
            this.panelResumen.Controls.Add(this.cardProximasCosechas, 1, 0);
            this.panelResumen.Controls.Add(this.cardCantidadSembrada, 2, 0);
            this.panelResumen.Controls.Add(this.cardAlertas, 3, 0);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResumen.Location = new System.Drawing.Point(0, 60);
            this.panelResumen.Margin = new System.Windows.Forms.Padding(10);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Padding = new System.Windows.Forms.Padding(10);
            this.panelResumen.RowCount = 1;
            this.panelResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelResumen.Size = new System.Drawing.Size(1200, 120);
            this.panelResumen.TabIndex = 1;

            // 
            // cardCultivosActivos
            // 
            this.cardCultivosActivos.BackColor = System.Drawing.Color.White;
            this.cardCultivosActivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCultivosActivos.Controls.Add(this.lblCultivosActivosValor);
            this.cardCultivosActivos.Controls.Add(this.lblCultivosActivosTexto);
            this.cardCultivosActivos.Controls.Add(this.picCultivosActivos);
            this.cardCultivosActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCultivosActivos.Location = new System.Drawing.Point(15, 15);
            this.cardCultivosActivos.Margin = new System.Windows.Forms.Padding(5);
            this.cardCultivosActivos.Name = "cardCultivosActivos";
            this.cardCultivosActivos.Size = new System.Drawing.Size(280, 90);
            this.cardCultivosActivos.TabIndex = 0;

            // 
            // lblCultivosActivosValor
            // 
            this.lblCultivosActivosValor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCultivosActivosValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCultivosActivosValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblCultivosActivosValor.Location = new System.Drawing.Point(185, 25);
            this.lblCultivosActivosValor.Name = "lblCultivosActivosValor";
            this.lblCultivosActivosValor.Size = new System.Drawing.Size(90, 40);
            this.lblCultivosActivosValor.TabIndex = 2;
            this.lblCultivosActivosValor.Text = "0";
            this.lblCultivosActivosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblCultivosActivosTexto
            // 
            this.lblCultivosActivosTexto.AutoSize = true;
            this.lblCultivosActivosTexto.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblCultivosActivosTexto.Location = new System.Drawing.Point(70, 10);
            this.lblCultivosActivosTexto.Name = "lblCultivosActivosTexto";
            this.lblCultivosActivosTexto.Size = new System.Drawing.Size(131, 23);
            this.lblCultivosActivosTexto.TabIndex = 1;
            this.lblCultivosActivosTexto.Text = "Cultivos Activos";

            // 
            // picCultivosActivos
            // 
            this.picCultivosActivos.Image = global::Sistema_Agricola.UI.Properties.Resources.CultivoIcon;
            this.picCultivosActivos.Location = new System.Drawing.Point(10, 10);
            this.picCultivosActivos.Name = "picCultivosActivos";
            this.picCultivosActivos.Size = new System.Drawing.Size(50, 50);
            this.picCultivosActivos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCultivosActivos.TabIndex = 0;
            this.picCultivosActivos.TabStop = false;

            // 
            // cardProximasCosechas
            // 
            this.cardProximasCosechas.BackColor = System.Drawing.Color.White;
            this.cardProximasCosechas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardProximasCosechas.Controls.Add(this.lblProximasCosechasValor);
            this.cardProximasCosechas.Controls.Add(this.lblProximasCosechasTexto);
            this.cardProximasCosechas.Controls.Add(this.picProximasCosechas);
            this.cardProximasCosechas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardProximasCosechas.Location = new System.Drawing.Point(305, 15);
            this.cardProximasCosechas.Margin = new System.Windows.Forms.Padding(5);
            this.cardProximasCosechas.Name = "cardProximasCosechas";
            this.cardProximasCosechas.Size = new System.Drawing.Size(280, 90);
            this.cardProximasCosechas.TabIndex = 1;

            // 
            // lblProximasCosechasValor
            // 
            this.lblProximasCosechasValor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProximasCosechasValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblProximasCosechasValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblProximasCosechasValor.Location = new System.Drawing.Point(185, 25);
            this.lblProximasCosechasValor.Name = "lblProximasCosechasValor";
            this.lblProximasCosechasValor.Size = new System.Drawing.Size(90, 40);
            this.lblProximasCosechasValor.TabIndex = 2;
            this.lblProximasCosechasValor.Text = "0";
            this.lblProximasCosechasValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblProximasCosechasTexto
            // 
            this.lblProximasCosechasTexto.AutoSize = true;
            this.lblProximasCosechasTexto.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblProximasCosechasTexto.Location = new System.Drawing.Point(70, 10);
            this.lblProximasCosechasTexto.Name = "lblProximasCosechasTexto";
            this.lblProximasCosechasTexto.Size = new System.Drawing.Size(115, 23);
            this.lblProximasCosechasTexto.TabIndex = 1;
            this.lblProximasCosechasTexto.Text = "Próx. Cosechas";

            // 
            // picProximasCosechas
            // 
            this.picProximasCosechas.Image = global::Sistema_Agricola.UI.Properties.Resources.CosechaIcon;
            this.picProximasCosechas.Location = new System.Drawing.Point(10, 10);
            this.picProximasCosechas.Name = "picProximasCosechas";
            this.picProximasCosechas.Size = new System.Drawing.Size(50, 50);
            this.picProximasCosechas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProximasCosechas.TabIndex = 0;
            this.picProximasCosechas.TabStop = false;

            // 
            // cardCantidadSembrada
            // 
            this.cardCantidadSembrada.BackColor = System.Drawing.Color.White;
            this.cardCantidadSembrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCantidadSembrada.Controls.Add(this.lblCantidadSembradaValor);
            this.cardCantidadSembrada.Controls.Add(this.lblCantidadSembradaTexto);
            this.cardCantidadSembrada.Controls.Add(this.picCantidadSembrada);
            this.cardCantidadSembrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCantidadSembrada.Location = new System.Drawing.Point(595, 15);
            this.cardCantidadSembrada.Margin = new System.Windows.Forms.Padding(5);
            this.cardCantidadSembrada.Name = "cardCantidadSembrada";
            this.cardCantidadSembrada.Size = new System.Drawing.Size(280, 90);
            this.cardCantidadSembrada.TabIndex = 2;

            // 
            // lblCantidadSembradaValor
            // 
            this.lblCantidadSembradaValor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCantidadSembradaValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCantidadSembradaValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblCantidadSembradaValor.Location = new System.Drawing.Point(185, 25);
            this.lblCantidadSembradaValor.Name = "lblCantidadSembradaValor";
            this.lblCantidadSembradaValor.Size = new System.Drawing.Size(90, 40);
            this.lblCantidadSembradaValor.TabIndex = 2;
            this.lblCantidadSembradaValor.Text = "0";
            this.lblCantidadSembradaValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblCantidadSembradaTexto
            // 
            this.lblCantidadSembradaTexto.AutoSize = true;
            this.lblCantidadSembradaTexto.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblCantidadSembradaTexto.Location = new System.Drawing.Point(70, 10);
            this.lblCantidadSembradaTexto.Name = "lblCantidadSembradaTexto";
            this.lblCantidadSembradaTexto.Size = new System.Drawing.Size(166, 23);
            this.lblCantidadSembradaTexto.TabIndex = 1;
            this.lblCantidadSembradaTexto.Text = "Cantidad Sembrada";

            // 
            // picCantidadSembrada
            // 
            this.picCantidadSembrada.Image = global::Sistema_Agricola.UI.Properties.Resources.SemillaIcon;
            this.picCantidadSembrada.Location = new System.Drawing.Point(10, 10);
            this.picCantidadSembrada.Name = "picCantidadSembrada";
            this.picCantidadSembrada.Size = new System.Drawing.Size(50, 50);
            this.picCantidadSembrada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCantidadSembrada.TabIndex = 0;
            this.picCantidadSembrada.TabStop = false;

            // 
            // cardAlertas
            // 
            this.cardAlertas.BackColor = System.Drawing.Color.White;
            this.cardAlertas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAlertas.Controls.Add(this.lblAlertasValor);
            this.cardAlertas.Controls.Add(this.lblAlertasTexto);
            this.cardAlertas.Controls.Add(this.picAlertas);
            this.cardAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAlertas.Location = new System.Drawing.Point(885, 15);
            this.cardAlertas.Margin = new System.Windows.Forms.Padding(5);
            this.cardAlertas.Name = "cardAlertas";
            this.cardAlertas.Size = new System.Drawing.Size(280, 90);
            this.cardAlertas.TabIndex = 3;

            // 
            // lblAlertasValor
            // 
            this.lblAlertasValor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertasValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblAlertasValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblAlertasValor.Location = new System.Drawing.Point(185, 25);
            this.lblAlertasValor.Name = "lblAlertasValor";
            this.lblAlertasValor.Size = new System.Drawing.Size(90, 40);
            this.lblAlertasValor.TabIndex = 2;
            this.lblAlertasValor.Text = "0";
            this.lblAlertasValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // lblAlertasTexto
            // 
            this.lblAlertasTexto.AutoSize = true;
            this.lblAlertasTexto.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblAlertasTexto.Location = new System.Drawing.Point(70, 10);
            this.lblAlertasTexto.Name = "lblAlertasTexto";
            this.lblAlertasTexto.Size = new System.Drawing.Size(154, 23);
            this.lblAlertasTexto.TabIndex = 1;
            this.lblAlertasTexto.Text = "Alertas Pendientes";

            // 
            // picAlertas
            // 
            this.picAlertas.Image = global::Sistema_Agricola.UI.Properties.Resources.advertencia;
            this.picAlertas.Location = new System.Drawing.Point(10, 10);
            this.picAlertas.Name = "picAlertas";
            this.picAlertas.Size = new System.Drawing.Size(50, 50);
            this.picAlertas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAlertas.TabIndex = 0;
            this.picAlertas.TabStop = false;

            // 
            // panelContenido
            // 
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelContenido.Controls.Add(this.panelIzquierdo, 0, 0);
            this.panelContenido.Controls.Add(this.panelDerecho, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 180);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1200, 520);
            this.panelContenido.TabIndex = 2;

            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.Controls.Add(this.tabControlDatos);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIzquierdo.Location = new System.Drawing.Point(10, 10);
            this.panelIzquierdo.Margin = new System.Windows.Forms.Padding(10);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(580, 500);
            this.panelIzquierdo.TabIndex = 0;

            // 
            // tabControlDatos
            // 
            this.tabControlDatos.Controls.Add(this.tabCultivosActivos);
            this.tabControlDatos.Controls.Add(this.tabProximasCosechas);
            this.tabControlDatos.Controls.Add(this.tabSeguimientos);
            this.tabControlDatos.Controls.Add(this.tabAlertas);
            this.tabControlDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDatos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDatos.Location = new System.Drawing.Point(0, 0);
            this.tabControlDatos.Name = "tabControlDatos";
            this.tabControlDatos.SelectedIndex = 0;
            this.tabControlDatos.Size = new System.Drawing.Size(580, 500);
            this.tabControlDatos.TabIndex = 0;

            // 
            // tabCultivosActivos
            // 
            this.tabCultivosActivos.Controls.Add(this.dgvCultivosActivos);
            this.tabCultivosActivos.Location = new System.Drawing.Point(4, 29);
            this.tabCultivosActivos.Name = "tabCultivosActivos";
            this.tabCultivosActivos.Padding = new System.Windows.Forms.Padding(3);
            this.tabCultivosActivos.Size = new System.Drawing.Size(572, 467);
            this.tabCultivosActivos.TabIndex = 0;
            this.tabCultivosActivos.Text = "Cultivos Activos";
            this.tabCultivosActivos.UseVisualStyleBackColor = true;

            // 
            // dgvCultivosActivos
            // 
            this.dgvCultivosActivos.AllowUserToAddRows = false;
            this.dgvCultivosActivos.AllowUserToDeleteRows = false;
            this.dgvCultivosActivos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCultivosActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCultivosActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCultivosActivos.Location = new System.Drawing.Point(3, 3);
            this.dgvCultivosActivos.Name = "dgvCultivosActivos";
            this.dgvCultivosActivos.ReadOnly = true;
            this.dgvCultivosActivos.RowHeadersWidth = 51;
            this.dgvCultivosActivos.RowTemplate.Height = 24;
            this.dgvCultivosActivos.Size = new System.Drawing.Size(566, 461);
            this.dgvCultivosActivos.TabIndex = 0;

            // 
            // tabProximasCosechas
            // 
            this.tabProximasCosechas.Controls.Add(this.dgvProximasCosechas);
            this.tabProximasCosechas.Location = new System.Drawing.Point(4, 29);
            this.tabProximasCosechas.Name = "tabProximasCosechas";
            this.tabProximasCosechas.Padding = new System.Windows.Forms.Padding(3);
            this.tabProximasCosechas.Size = new System.Drawing.Size(572, 467);
            this.tabProximasCosechas.TabIndex = 1;
            this.tabProximasCosechas.Text = "Próximas Cosechas";
            this.tabProximasCosechas.UseVisualStyleBackColor = true;

            // 
            // dgvProximasCosechas
            // 
            this.dgvProximasCosechas.AllowUserToAddRows = false;
            this.dgvProximasCosechas.AllowUserToDeleteRows = false;
            this.dgvProximasCosechas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProximasCosechas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProximasCosechas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProximasCosechas.Location = new System.Drawing.Point(3, 3);
            this.dgvProximasCosechas.Name = "dgvProximasCosechas";
            this.dgvProximasCosechas.ReadOnly = true;
            this.dgvProximasCosechas.RowHeadersWidth = 51;
            this.dgvProximasCosechas.RowTemplate.Height = 24;
            this.dgvProximasCosechas.Size = new System.Drawing.Size(566, 461);
            this.dgvProximasCosechas.TabIndex = 0;

            // 
            // tabSeguimientos
            // 
            this.tabSeguimientos.Controls.Add(this.dgvUltimosSeguimientos);
            this.tabSeguimientos.Location = new System.Drawing.Point(4, 29);
            this.tabSeguimientos.Name = "tabSeguimientos";
            this.tabSeguimientos.Size = new System.Drawing.Size(572, 467);
            this.tabSeguimientos.TabIndex = 2;
            this.tabSeguimientos.Text = "Seguimientos";
            this.tabSeguimientos.UseVisualStyleBackColor = true;

            // 
            // dgvUltimosSeguimientos
            // 
            this.dgvUltimosSeguimientos.AllowUserToAddRows = false;
            this.dgvUltimosSeguimientos.AllowUserToDeleteRows = false;
            this.dgvUltimosSeguimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUltimosSeguimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUltimosSeguimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUltimosSeguimientos.Location = new System.Drawing.Point(0, 0);
            this.dgvUltimosSeguimientos.Name = "dgvUltimosSeguimientos";
            this.dgvUltimosSeguimientos.ReadOnly = true;
            this.dgvUltimosSeguimientos.RowHeadersWidth = 51;
            this.dgvUltimosSeguimientos.RowTemplate.Height = 24;
            this.dgvUltimosSeguimientos.Size = new System.Drawing.Size(572, 467);
            this.dgvUltimosSeguimientos.TabIndex = 0;

            // 
            // tabAlertas
            // 
            this.tabAlertas.Controls.Add(this.dgvAlertas);
            this.tabAlertas.Location = new System.Drawing.Point(4, 29);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Size = new System.Drawing.Size(572, 467);
            this.tabAlertas.TabIndex = 3;
            this.tabAlertas.Text = "Alertas";
            this.tabAlertas.UseVisualStyleBackColor = true;

            // 
            // dgvAlertas
            // 
            this.dgvAlertas.AllowUserToAddRows = false;
            this.dgvAlertas.AllowUserToDeleteRows = false;
            this.dgvAlertas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertas.Location = new System.Drawing.Point(0, 0);
            this.dgvAlertas.Name = "dgvAlertas";
            this.dgvAlertas.ReadOnly = true;
            this.dgvAlertas.RowHeadersWidth = 51;
            this.dgvAlertas.RowTemplate.Height = 24;
            this.dgvAlertas.Size = new System.Drawing.Size(572, 467);
            this.dgvAlertas.TabIndex = 0;

            // 
            // panelDerecho
            // 
            this.panelDerecho.Controls.Add(this.panelGraficos);
            this.panelDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDerecho.Location = new System.Drawing.Point(610, 10);
            this.panelDerecho.Margin = new System.Windows.Forms.Padding(10);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Size = new System.Drawing.Size(580, 500);
            this.panelDerecho.TabIndex = 1;

            // 
            // panelGraficos
            // 
            this.panelGraficos.ColumnCount = 1;
            this.panelGraficos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelGraficos.Controls.Add(this.panelDistribucionCultivos, 0, 0);
            this.panelGraficos.Controls.Add(this.panelSeguimientoCrecimiento, 0, 1);
            this.panelGraficos.Controls.Add(this.panelEstadisticasRendimiento, 0, 2);
            this.panelGraficos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraficos.Location = new System.Drawing.Point(0, 0);
            this.panelGraficos.Name = "panelGraficos";
            this.panelGraficos.RowCount = 3;
            this.panelGraficos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.panelGraficos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.panelGraficos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.panelGraficos.Size = new System.Drawing.Size(580, 500);
            this.panelGraficos.TabIndex = 0;

            // 
            // panelDistribucionCultivos
            // 
            this.panelDistribucionCultivos.Controls.Add(this.chartTiposCultivo);
            this.panelDistribucionCultivos.Controls.Add(this.lblDistribucionCultivos);
            this.panelDistribucionCultivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDistribucionCultivos.Location = new System.Drawing.Point(3, 3);
            this.panelDistribucionCultivos.Name = "panelDistribucionCultivos";
            this.panelDistribucionCultivos.Size = new System.Drawing.Size(574, 160);
            this.panelDistribucionCultivos.TabIndex = 0;

            // 
            // chartTiposCultivo
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTiposCultivo.ChartAreas.Add(chartArea1);
            this.chartTiposCultivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTiposCultivo.Location = new System.Drawing.Point(0, 23);
            this.chartTiposCultivo.Name = "chartTiposCultivo";
            this.chartTiposCultivo.Size = new System.Drawing.Size(574, 137);
            this.chartTiposCultivo.TabIndex = 1;
            this.chartTiposCultivo.Text = "Distribución por tipo de cultivo";

            // 
            // lblDistribucionCultivos
            // 
            this.lblDistribucionCultivos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(124)))), ((int)(((byte)(153)))));
            this.lblDistribucionCultivos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDistribucionCultivos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDistribucionCultivos.ForeColor = System.Drawing.Color.White;
            this.lblDistribucionCultivos.Location = new System.Drawing.Point(0, 0);
            this.lblDistribucionCultivos.Name = "lblDistribucionCultivos";
            this.lblDistribucionCultivos.Size = new System.Drawing.Size(574, 23);
            this.lblDistribucionCultivos.TabIndex = 0;
            this.lblDistribucionCultivos.Text = "  Distribución por Tipo de Cultivo";
            this.lblDistribucionCultivos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelSeguimientoCrecimiento
            // 
            this.panelSeguimientoCrecimiento.Controls.Add(this.chartSeguimiento);
            this.panelSeguimientoCrecimiento.Controls.Add(this.lblSeguimientoCrecimiento);
            this.panelSeguimientoCrecimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSeguimientoCrecimiento.Location = new System.Drawing.Point(3, 169);
            this.panelSeguimientoCrecimiento.Name = "panelSeguimientoCrecimiento";
            this.panelSeguimientoCrecimiento.Size = new System.Drawing.Size(574, 160);
            this.panelSeguimientoCrecimiento.TabIndex = 1;

            // 
            // chartSeguimiento
            // 
            chartArea2.Name = "ChartArea1";
            this.chartSeguimiento.ChartAreas.Add(chartArea2);
            this.chartSeguimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSeguimiento.Location = new System.Drawing.Point(0, 23);
            this.chartSeguimiento.Name = "chartSeguimiento";
            this.chartSeguimiento.Size = new System.Drawing.Size(574, 137);
            this.chartSeguimiento.TabIndex = 1;
            this.chartSeguimiento.Text = "Seguimiento de crecimiento";

            // 
            // lblSeguimientoCrecimiento
            // 
            this.lblSeguimientoCrecimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(124)))), ((int)(((byte)(153)))));
            this.lblSeguimientoCrecimiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeguimientoCrecimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeguimientoCrecimiento.ForeColor = System.Drawing.Color.White;
            this.lblSeguimientoCrecimiento.Location = new System.Drawing.Point(0, 0);
            this.lblSeguimientoCrecimiento.Name = "lblSeguimientoCrecimiento";
            this.lblSeguimientoCrecimiento.Size = new System.Drawing.Size(574, 23);
            this.lblSeguimientoCrecimiento.TabIndex = 0;
            this.lblSeguimientoCrecimiento.Text = "  Seguimiento de Crecimiento";
            this.lblSeguimientoCrecimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelEstadisticasRendimiento
            // 
            this.panelEstadisticasRendimiento.Controls.Add(this.chartRendimientos);
            this.panelEstadisticasRendimiento.Controls.Add(this.lblEstadisticasRendimiento);
            this.panelEstadisticasRendimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstadisticasRendimiento.Location = new System.Drawing.Point(3, 335);
            this.panelEstadisticasRendimiento.Name = "panelEstadisticasRendimiento";
            this.panelEstadisticasRendimiento.Size = new System.Drawing.Size(574, 162);
            this.panelEstadisticasRendimiento.TabIndex = 2;

            // 
            // chartRendimientos
            // 
            chartArea3.Name = "ChartArea1";
            this.chartRendimientos.ChartAreas.Add(chartArea3);
            this.chartRendimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRendimientos.Location = new System.Drawing.Point(0, 23);
            this.chartRendimientos.Name = "chartRendimientos";
            this.chartRendimientos.Size = new System.Drawing.Size(574, 139);
            this.chartRendimientos.TabIndex = 1;
            this.chartRendimientos.Text = "Rendimientos por cultivo";

            // 
            // lblEstadisticasRendimiento
            // 
            this.lblEstadisticasRendimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(124)))), ((int)(((byte)(153)))));
            this.lblEstadisticasRendimiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEstadisticasRendimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstadisticasRendimiento.ForeColor = System.Drawing.Color.White;
            this.lblEstadisticasRendimiento.Location = new System.Drawing.Point(0, 0);
            this.lblEstadisticasRendimiento.Name = "lblEstadisticasRendimiento";
            this.lblEstadisticasRendimiento.Size = new System.Drawing.Size(574, 23);
            this.lblEstadisticasRendimiento.TabIndex = 0;
            this.lblEstadisticasRendimiento.Text = "  Rendimientos por Cultivo";
            this.lblEstadisticasRendimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelResumen);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDashboard_FormClosing);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelResumen.ResumeLayout(false);
            this.cardCultivosActivos.ResumeLayout(false);
            this.cardCultivosActivos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCultivosActivos)).EndInit();
            this.cardProximasCosechas.ResumeLayout(false);
            this.cardProximasCosechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProximasCosechas)).EndInit();
            this.cardCantidadSembrada.ResumeLayout(false);
            this.cardCantidadSembrada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCantidadSembrada)).EndInit();
            this.cardAlertas.ResumeLayout(false);
            this.cardAlertas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlertas)).EndInit();
            this.panelContenido.ResumeLayout(false);
            this.panelIzquierdo.ResumeLayout(false);
            this.tabControlDatos.ResumeLayout(false);
            this.tabCultivosActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCultivosActivos)).EndInit();
            this.tabProximasCosechas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximasCosechas)).EndInit();
            this.tabSeguimientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimosSeguimientos)).EndInit();
            this.tabAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).EndInit();
            this.panelDerecho.ResumeLayout(false);
            this.panelGraficos.ResumeLayout(false);
            this.panelDistribucionCultivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTiposCultivo)).EndInit();
            this.panelSeguimientoCrecimiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSeguimiento)).EndInit();
            this.panelEstadisticasRendimiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRendimientos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.Button btnRefrescar;

        private System.Windows.Forms.TableLayoutPanel panelResumen;
        private System.Windows.Forms.Panel cardCultivosActivos;
        private System.Windows.Forms.Label lblCultivosActivosValor;
        private System.Windows.Forms.Label lblCultivosActivosTexto;
        private System.Windows.Forms.PictureBox picCultivosActivos;
        private System.Windows.Forms.Panel cardProximasCosechas;
        private System.Windows.Forms.Label lblProximasCosechasValor;
        private System.Windows.Forms.Label lblProximasCosechasTexto;
        private System.Windows.Forms.PictureBox picProximasCosechas;
        private System.Windows.Forms.Panel cardCantidadSembrada;
        private System.Windows.Forms.Label lblCantidadSembradaValor;
        private System.Windows.Forms.Label lblCantidadSembradaTexto;
        private System.Windows.Forms.PictureBox picCantidadSembrada;
        private System.Windows.Forms.Panel cardAlertas;
        private System.Windows.Forms.Label lblAlertasValor;
        private System.Windows.Forms.Label lblAlertasTexto;
        private System.Windows.Forms.PictureBox picAlertas;

        private System.Windows.Forms.TableLayoutPanel panelContenido;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.TabControl tabControlDatos;
        private System.Windows.Forms.TabPage tabCultivosActivos;
        private System.Windows.Forms.DataGridView dgvCultivosActivos;
        private System.Windows.Forms.TabPage tabProximasCosechas;
        private System.Windows.Forms.DataGridView dgvProximasCosechas;
        private System.Windows.Forms.TabPage tabSeguimientos;
        private System.Windows.Forms.DataGridView dgvUltimosSeguimientos;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.DataGridView dgvAlertas;

        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.TableLayoutPanel panelGraficos;
        private System.Windows.Forms.Panel panelDistribucionCultivos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTiposCultivo;
        private System.Windows.Forms.Label lblDistribucionCultivos;
        private System.Windows.Forms.Panel panelSeguimientoCrecimiento;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeguimiento;
        private System.Windows.Forms.Label lblSeguimientoCrecimiento;
        private System.Windows.Forms.Panel panelEstadisticasRendimiento;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRendimientos;
        private System.Windows.Forms.Label lblEstadisticasRendimiento;
    }
}

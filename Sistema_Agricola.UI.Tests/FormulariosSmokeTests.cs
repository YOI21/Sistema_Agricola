using System.Runtime.ExceptionServices;
using Sistema_Agricola.UI;

namespace Sistema_Agricola.UI.Tests;

[CollectionDefinition("Pruebas visuales", DisableParallelization = true)]
public sealed class PruebasVisualesCollection
{
}

[Collection("Pruebas visuales")]
public sealed class FormulariosSmokeTests
{
    private static readonly TimeSpan TiempoConstruccion = TimeSpan.FromSeconds(20);
    private static readonly TimeSpan TiempoRenderizado = TimeSpan.FromSeconds(90);

    [Fact]
    public void Siembra_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmSiembra());

    [Fact]
    public void Trabajadores_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmTrabajadores());

    [Fact]
    public void GestionMaquinaria_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmGestionDeMaquinaria());

    [Fact]
    public void Configuracion_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmConfiguracionSistema());

    [Fact]
    public void ReporteSiembras_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmReporteSiembras());

    [Fact]
    public void ReporteProduccion_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmReporteProduccion());

    [Fact]
    public void ReporteInsumos_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmReporteInsumos());

    [Fact]
    public void ReporteFinanciero_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmReporteFinanciero());

    [Fact]
    public void ReporteCrecimiento_ConstruyeInterfaz() =>
        VerificarEnSta(() => new FrmReporteCrecimiento());

    [Theory]
    [InlineData(1.25F)]
    [InlineData(1.50F)]
    public void FormulariosGrandes_RenderizanEn1366x768ConEscalado(float escala)
    {
        EjecutarEnSta(() =>
        {
            VerificarEscalado(() => new FrmSiembra(true), escala);
            VerificarEscalado(() => new FrmDashboard(true), escala);
            VerificarEscalado(() => new FrmReporteInsumos(true), escala);
        }, TiempoRenderizado);
    }

    private static void VerificarEnSta(Func<Form> crearFormulario)
    {
        EjecutarEnSta(() =>
        {
            using Form formulario = crearFormulario();
            VerificarEstructura(formulario);
        });
    }

    private static void EjecutarEnSta(
        Action accion,
        TimeSpan? tiempoMaximo = null)
    {
        Exception? error = null;
        using ManualResetEventSlim terminado = new(false);

        Thread hilo = new(() =>
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                terminado.Set();
            }
        });
        hilo.SetApartmentState(ApartmentState.STA);
        hilo.IsBackground = true;
        hilo.Start();

        TimeSpan limite = tiempoMaximo ?? TiempoConstruccion;
        Assert.True(
            terminado.Wait(limite),
            $"La prueba visual no terminó en {limite.TotalSeconds:N0} segundos.");
        hilo.Join();

        if (error is not null)
        {
            ExceptionDispatchInfo.Capture(error).Throw();
        }
    }

    private static void VerificarEscalado(
        Func<Form> crearFormulario,
        float escala)
    {
        using Form formulario = crearFormulario();
        Assert.Equal(AutoScaleMode.Dpi, formulario.AutoScaleMode);
        Assert.True(formulario.AutoScroll);

        formulario.MinimumSize = Size.Empty;
        formulario.ShowInTaskbar = false;
        formulario.StartPosition = FormStartPosition.Manual;
        formulario.Location = new Point(-32000, -32000);
        formulario.Show();
        Application.DoEvents();
        formulario.Scale(new SizeF(escala, escala));
        formulario.ClientSize = new Size(
            (int)Math.Floor(1366 / escala),
            (int)Math.Floor(768 / escala));
        formulario.CreateControl();
        formulario.PerformLayout();

        List<Control> controles = ObtenerControles(formulario).ToList();
        Assert.All(
            controles.Where(control => control.TabStop),
            control =>
            {
                Assert.True(control.Width > 0, $"{control.Name} quedó sin ancho.");
                Assert.True(control.Height > 0, $"{control.Name} quedó sin alto.");
            });

        List<Control> navegables = controles
            .Where(EsControlNavegable)
            .ToList();
        Assert.NotEmpty(navegables);
        Assert.All(navegables, control => Assert.True(control.TabIndex >= 0));
        string[] conflictosTab = navegables
            .Where(control => control.Parent is not null)
            .GroupBy(control => new { control.Parent, control.TabIndex })
            .Where(grupo => grupo.Count() > 1)
            .Select(grupo =>
                $"{grupo.Key.Parent!.Name}: TabIndex {grupo.Key.TabIndex}")
            .ToArray();
        Assert.True(
            conflictosTab.Length == 0,
            "Hay controles hermanos con el mismo orden de tabulación: " +
            string.Join(", ", conflictosTab));

        List<string> superposiciones = new();
        foreach (IGrouping<Control?, Control> grupo in navegables
                     .Where(control => control.Visible && control.Parent is not null)
                     .GroupBy(control => control.Parent))
        {
            Control[] hermanos = grupo.ToArray();
            for (int primero = 0; primero < hermanos.Length; primero++)
            {
                for (int segundo = primero + 1; segundo < hermanos.Length; segundo++)
                {
                    Rectangle interseccion = Rectangle.Intersect(
                        hermanos[primero].Bounds,
                        hermanos[segundo].Bounds);
                    if (interseccion.Width > 1 && interseccion.Height > 1)
                    {
                        superposiciones.Add(
                            $"{hermanos[primero].Name}/{hermanos[segundo].Name}");
                    }
                }
            }
        }

        Assert.True(
            superposiciones.Count == 0,
            "Hay controles interactivos superpuestos: " +
            string.Join(", ", superposiciones));

        foreach (Button boton in controles.OfType<Button>()
                     .Where(boton => boton.Visible && !string.IsNullOrWhiteSpace(boton.Text)))
        {
            Size texto = TextRenderer.MeasureText(boton.Text, boton.Font);
            Assert.True(
                texto.Width <= boton.ClientSize.Width + 8,
                $"El texto del botón {boton.Name} no cabe a {escala:P0}.");
        }

        using Bitmap captura = new(
            formulario.ClientSize.Width,
            formulario.ClientSize.Height);
        formulario.DrawToBitmap(
            captura,
            new Rectangle(Point.Empty, formulario.ClientSize));

        HashSet<int> colores = new();
        for (int y = 0; y < captura.Height; y += 24)
        {
            for (int x = 0; x < captura.Width; x += 24)
            {
                colores.Add(captura.GetPixel(x, y).ToArgb());
            }
        }

        Assert.True(colores.Count >= 4, "La captura del formulario quedó vacía.");
        formulario.Close();
    }

    private static bool EsControlNavegable(Control control) =>
        control.Enabled &&
        control.TabStop &&
        control is TextBoxBase or ComboBox or Button or CheckBox or
            DateTimePicker or NumericUpDown or DataGridView or TabControl;

    private static void VerificarEstructura(Form formulario)
    {
        Assert.NotEmpty(formulario.Controls.Cast<Control>());
        Assert.InRange(formulario.ClientSize.Width, 640, 1366);
        Assert.InRange(formulario.ClientSize.Height, 480, 768);

        List<Control> controles = ObtenerControles(formulario).ToList();
        string[] nombresDuplicados = controles
            .Where(control => !string.IsNullOrWhiteSpace(control.Name))
            .GroupBy(control => control.Name, StringComparer.OrdinalIgnoreCase)
            .Where(grupo => grupo.Count() > 1)
            .Select(grupo => grupo.Key)
            .ToArray();

        Assert.True(
            nombresDuplicados.Length == 0,
            "Hay controles con nombres duplicados: " +
            string.Join(", ", nombresDuplicados));

        foreach (Button boton in controles.OfType<Button>())
        {
            Assert.False(
                string.IsNullOrWhiteSpace(boton.Text) &&
                string.IsNullOrWhiteSpace(boton.AccessibleName),
                "Todos los botones deben tener texto o nombre accesible.");
        }
    }

    private static IEnumerable<Control> ObtenerControles(Control padre)
    {
        foreach (Control control in padre.Controls)
        {
            yield return control;
            foreach (Control hijo in ObtenerControles(control))
            {
                yield return hijo;
            }
        }
    }
}

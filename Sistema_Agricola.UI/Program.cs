namespace Sistema_Agricola.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            ConfigurarManejoGlobalErrores();

            try
            {
                if (args.Any(x => x.Equals(
                        "--diagnosticar-correo",
                        StringComparison.OrdinalIgnoreCase)))
                {
                    EjecutarDiagnosticoCorreo(args);
                    return;
                }

                if (!SistemaConfiguracionServicio.AplicarConexionBaseDatos())
                {
                    MessageBox.Show(
                        "No se encontro la conexion restringida de la base de datos.\n\n" +
                        "Ejecute Configurar_Conexion_Restringida.ps1 desde la carpeta " +
                        "DBSistema_Agricola y vuelva a abrir el sistema.",
                        "Conexion segura requerida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Sistema_Agricola.BLL.ResultadoVerificacionEsquema estadoEsquema =
                    Sistema_Agricola.BLL.BLL_Esquema.VerificarCompatibilidad();

                if (!estadoEsquema.EsCompatible)
                {
                    MessageBox.Show(
                        estadoEsquema.Mensaje,
                        "Base de datos desactualizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SistemaConfiguracionServicio.AplicarVariablesCorreo(SistemaConfiguracionServicio.Cargar());
                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                ManejadorErroresUI.Mostrar(ex, "iniciar el sistema");
            }
        }

        private static void EjecutarDiagnosticoCorreo(string[] args)
        {
            string rutaResultado = ObtenerArgumento(
                args,
                "--resultado-correo",
                Path.Combine(Path.GetTempPath(), "Sistema_Agricola_correo.txt"));

            try
            {
                SistemaConfiguracion config = SistemaConfiguracionServicio.Cargar();
                string destinatario = ObtenerArgumento(
                    args,
                    "--destinatario",
                    config.GmailRemitente);

                Sistema_Agricola.BLL.BLL_ConfiguracionSistema.EnviarCorreoPrueba(
                    config.GmailRemitente,
                    config.GmailClaveAplicacion,
                    config.NombreRemitente,
                    "smtp.gmail.com",
                    587,
                    true,
                    destinatario);

                EscribirResultadoCorreo(
                    rutaResultado,
                    true,
                    "Gmail acepto el mensaje. La configuracion funciona en esta PC.");
            }
            catch (Exception ex)
            {
                EscribirResultadoCorreo(rutaResultado, false, ex.Message);
            }
        }

        private static string ObtenerArgumento(
            IReadOnlyList<string> args,
            string nombre,
            string valorPredeterminado)
        {
            for (int i = 0; i < args.Count - 1; i++)
            {
                if (args[i].Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return args[i + 1];
                }
            }

            return valorPredeterminado;
        }

        private static void EscribirResultadoCorreo(
            string ruta,
            bool exitoso,
            string mensaje)
        {
            string? carpeta = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrWhiteSpace(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            File.WriteAllLines(
                ruta,
                new[]
                {
                    exitoso ? "OK" : "ERROR",
                    mensaje
                });
        }

        private static void ConfigurarManejoGlobalErrores()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, args) =>
                ManejadorErroresUI.Mostrar(args.Exception, "completar la operacion solicitada");

            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    Sistema_Agricola.Common.RegistroErrores.Registrar(
                        ex,
                        "Excepcion no controlada del proceso");
                }
            };

            TaskScheduler.UnobservedTaskException += (_, args) =>
            {
                Sistema_Agricola.Common.RegistroErrores.Registrar(
                    args.Exception,
                    "Tarea asincrona no observada");
                args.SetObserved();
            };
        }
    }
}

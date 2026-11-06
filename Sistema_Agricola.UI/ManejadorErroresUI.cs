using Sistema_Agricola.Common;

namespace Sistema_Agricola.UI
{
    internal static class ManejadorErroresUI
    {
        public static string ObtenerMensajeSeguro(Exception excepcion, string contexto = "Interfaz")
        {
            return RegistroErrores.ObtenerMensajeSeguro(excepcion, CompletarContexto(contexto));
        }

        public static string ObtenerReferencia(Exception excepcion, string contexto = "Interfaz")
        {
            return RegistroErrores.ObtenerReferencia(excepcion, CompletarContexto(contexto));
        }

        public static void Mostrar(
            Exception excepcion,
            string accion,
            IWin32Window? propietario = null)
        {
            string codigo = RegistroErrores.Registrar(excepcion, CompletarContexto(accion));
            string mensaje =
                $"No fue posible {accion}. Intente nuevamente.\n\nCodigo de seguimiento: {codigo}";

            if (propietario == null)
            {
                MessageBox.Show(
                    mensaje,
                    "Operacion no completada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    propietario,
                    mensaje,
                    "Operacion no completada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static void Mostrar(
            IWin32Window? propietario,
            Exception excepcion,
            string accion)
        {
            Mostrar(excepcion, accion, propietario);
        }

        public static bool ContieneDetalle(Exception excepcion, string texto)
        {
            RegistroErrores.Registrar(excepcion, CompletarContexto("Clasificacion de error"));
            return (excepcion.Message ?? string.Empty).IndexOf(
                texto ?? string.Empty,
                StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string CompletarContexto(string contexto)
        {
            string usuario = Session.CurrentUser?.Username ?? "Sin sesion";
            string rol = Session.CurrentUser?.Rol ?? "Sin rol";
            return $"{contexto}; Usuario={usuario}; Rol={rol}";
        }
    }
}

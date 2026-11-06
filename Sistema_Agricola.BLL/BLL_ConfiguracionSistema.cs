using System;

namespace Sistema_Agricola.BLL
{
    public static class BLL_ConfiguracionSistema
    {
        private const string PermisoConfiguracionInicial = "configuracion.inicial";

        public static void Guardar(string detalle, Action persistir)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.ConfiguracionActualizar,
                "ConfiguracionSistema",
                "Actualizar",
                detalle,
                persistir);
        }

        public static void GuardarInicial(string detalle, Action persistir)
        {
            EjecutarConfiguracionInicial("Actualizar", detalle, persistir);
        }

        public static void ProbarCorreo(string detalle, Action enviar)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.ConfiguracionProbarCorreo,
                "ConfiguracionSistema",
                "Probar correo",
                detalle,
                enviar);
        }

        public static void ProbarCorreoInicial(string detalle, Action enviar)
        {
            EjecutarConfiguracionInicial("Probar correo", detalle, enviar);
        }

        public static void EnviarCorreoPrueba(
            string correoRemitente,
            string claveAplicacion,
            string nombreRemitente,
            string host,
            int puerto,
            bool ssl,
            string destinatario)
        {
            ServicioCorreoRecuperacion.EnviarPrueba(
                correoRemitente,
                claveAplicacion,
                nombreRemitente,
                host,
                puerto,
                ssl,
                destinatario);
        }

        private static void EjecutarConfiguracionInicial(
            string accion,
            string detalle,
            Action operacion)
        {
            ArgumentNullException.ThrowIfNull(operacion);

            if (BLL_Administrador.ExisteAdministradorRegistrado())
            {
                BLL_Seguridad.Auditar(
                    PermisoConfiguracionInicial,
                    "ConfiguracionSistema",
                    accion,
                    detalle,
                    ResultadosAuditoria.Denegado);
                throw new UnauthorizedAccessException(
                    "La configuracion inicial ya no esta disponible. Inicie sesion como administrador.");
            }

            try
            {
                operacion();
                BLL_Seguridad.Auditar(
                    PermisoConfiguracionInicial,
                    "ConfiguracionSistema",
                    accion,
                    detalle,
                    ResultadosAuditoria.Exito);
            }
            catch
            {
                BLL_Seguridad.Auditar(
                    PermisoConfiguracionInicial,
                    "ConfiguracionSistema",
                    accion,
                    detalle,
                    ResultadosAuditoria.Error);
                throw;
            }
        }
    }
}

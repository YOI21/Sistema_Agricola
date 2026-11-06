using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sistema_Agricola.DAL;

namespace Sistema_Agricola.BLL
{
    public static class PermisosSistema
    {
        public const string FinanzasCrear = "finanzas.crear";
        public const string FinanzasActualizar = "finanzas.actualizar";
        public const string FinanzasEliminar = "finanzas.eliminar";

        public const string InventarioCrear = "inventario.crear";
        public const string InventarioActualizar = "inventario.actualizar";
        public const string InventarioMovimiento = "inventario.movimiento";

        public const string TrabajadoresCrear = "trabajadores.crear";
        public const string TrabajadoresActualizar = "trabajadores.actualizar";
        public const string TrabajadoresEliminar = "trabajadores.eliminar";

        public const string MaquinariaCrear = "maquinaria.crear";
        public const string MaquinariaActualizar = "maquinaria.actualizar";
        public const string MaquinariaEliminar = "maquinaria.eliminar";
        public const string MaquinariaAsignar = "maquinaria.asignar";
        public const string MaquinariaMantenimiento = "maquinaria.mantenimiento";

        public const string EquiposCrear = "equipos.crear";
        public const string EquiposActualizar = "equipos.actualizar";
        public const string EquiposEliminar = "equipos.eliminar";
        public const string EquiposMantenimiento = "equipos.mantenimiento";

        public const string SiembrasCrear = "siembras.crear";
        public const string SiembrasActualizar = "siembras.actualizar";
        public const string SiembrasEliminar = "siembras.eliminar";
        public const string SiembrasFinalizar = "siembras.finalizar";
        public const string SiembrasAnular = "siembras.anular";

        public const string CosechasCrear = "cosechas.crear";
        public const string CosechasActualizar = "cosechas.actualizar";
        public const string CosechasEliminar = "cosechas.eliminar";
        public const string CosechasFinalizar = "cosechas.finalizar";
        public const string CosechasAnular = "cosechas.anular";

        public const string TerrenosCrear = "terrenos.crear";
        public const string TerrenosActualizar = "terrenos.actualizar";
        public const string TerrenosEliminar = "terrenos.eliminar";

        public const string AlertasCrear = "alertas.crear";
        public const string AlertasActualizar = "alertas.actualizar";
        public const string AlertasCompletar = "alertas.completar";
        public const string AlertasAnular = "alertas.anular";
        public const string AlertasEliminar = "alertas.eliminar";
        public const string AlertasNotificar = "alertas.notificar";

        public const string ProduccionCrear = "produccion.crear";
        public const string ProduccionActualizar = "produccion.actualizar";
        public const string ProduccionEliminar = "produccion.eliminar";
        public const string ProduccionFinalizar = "produccion.finalizar";
        public const string ProduccionAnular = "produccion.anular";

        public const string SeguimientoCrear = "seguimiento.crear";
        public const string SeguimientoActualizar = "seguimiento.actualizar";
        public const string SeguimientoEliminar = "seguimiento.eliminar";

        public const string ConfiguracionActualizar = "configuracion.actualizar";
        public const string ConfiguracionProbarCorreo = "configuracion.probar_correo";
        public const string AgricultoresCrear = "agricultores.crear";
    }

    public static class ResultadosAuditoria
    {
        public const string Exito = "Exito";
        public const string Fallido = "Fallido";
        public const string Denegado = "Denegado";
        public const string Error = "Error";
    }

    public sealed class UsuarioSeguridad
    {
        public int ID { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    internal sealed record EventoAuditoriaSeguridad(
        int? IdUsuario,
        string Usuario,
        string Rol,
        string Permiso,
        string Entidad,
        string Accion,
        string Detalle,
        string Resultado);

    public static class BLL_Seguridad
    {
        private static UsuarioSeguridad? usuarioActual;
        private static Action<EventoAuditoriaSeguridad>? receptorAuditoriaPrueba;

        private static readonly Dictionary<string, HashSet<string>> PermisosPorRol =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Administrador"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    PermisosSistema.FinanzasCrear,
                    PermisosSistema.FinanzasActualizar,
                    PermisosSistema.FinanzasEliminar,
                    PermisosSistema.InventarioCrear,
                    PermisosSistema.InventarioActualizar,
                    PermisosSistema.InventarioMovimiento,
                    PermisosSistema.TrabajadoresCrear,
                    PermisosSistema.TrabajadoresActualizar,
                    PermisosSistema.TrabajadoresEliminar,
                    PermisosSistema.MaquinariaCrear,
                    PermisosSistema.MaquinariaActualizar,
                    PermisosSistema.MaquinariaEliminar,
                    PermisosSistema.MaquinariaAsignar,
                    PermisosSistema.MaquinariaMantenimiento,
                    PermisosSistema.EquiposCrear,
                    PermisosSistema.EquiposActualizar,
                    PermisosSistema.EquiposEliminar,
                    PermisosSistema.EquiposMantenimiento,
                    PermisosSistema.SiembrasCrear,
                    PermisosSistema.SiembrasActualizar,
                    PermisosSistema.SiembrasEliminar,
                    PermisosSistema.SiembrasFinalizar,
                    PermisosSistema.SiembrasAnular,
                    PermisosSistema.CosechasCrear,
                    PermisosSistema.CosechasActualizar,
                    PermisosSistema.CosechasEliminar,
                    PermisosSistema.CosechasFinalizar,
                    PermisosSistema.CosechasAnular,
                    PermisosSistema.TerrenosCrear,
                    PermisosSistema.TerrenosActualizar,
                    PermisosSistema.TerrenosEliminar,
                    PermisosSistema.AlertasCrear,
                    PermisosSistema.AlertasActualizar,
                    PermisosSistema.AlertasCompletar,
                    PermisosSistema.AlertasAnular,
                    PermisosSistema.AlertasEliminar,
                    PermisosSistema.AlertasNotificar,
                    PermisosSistema.ProduccionCrear,
                    PermisosSistema.ProduccionActualizar,
                    PermisosSistema.ProduccionEliminar,
                    PermisosSistema.ProduccionFinalizar,
                    PermisosSistema.ProduccionAnular,
                    PermisosSistema.SeguimientoCrear,
                    PermisosSistema.SeguimientoActualizar,
                    PermisosSistema.SeguimientoEliminar,
                    PermisosSistema.ConfiguracionActualizar,
                    PermisosSistema.ConfiguracionProbarCorreo,
                    PermisosSistema.AgricultoresCrear
                },
                ["Agricultor"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    PermisosSistema.SiembrasCrear,
                    PermisosSistema.SiembrasActualizar,
                    PermisosSistema.SiembrasFinalizar,
                    PermisosSistema.SiembrasAnular,
                    PermisosSistema.CosechasCrear,
                    PermisosSistema.CosechasActualizar,
                    PermisosSistema.CosechasFinalizar,
                    PermisosSistema.CosechasAnular,
                    PermisosSistema.AlertasCrear,
                    PermisosSistema.AlertasActualizar,
                    PermisosSistema.AlertasCompletar,
                    PermisosSistema.AlertasAnular,
                    PermisosSistema.AlertasNotificar,
                    PermisosSistema.ProduccionCrear,
                    PermisosSistema.ProduccionActualizar,
                    PermisosSistema.ProduccionFinalizar,
                    PermisosSistema.ProduccionAnular,
                    PermisosSistema.SeguimientoCrear,
                    PermisosSistema.SeguimientoActualizar
                }
            };

        public static UsuarioSeguridad? UsuarioActual => usuarioActual;

        public static void IniciarSesion(int id, string usuario, string nombreCompleto, string rol)
        {
            usuarioActual = new UsuarioSeguridad
            {
                ID = id,
                Usuario = usuario ?? string.Empty,
                NombreCompleto = nombreCompleto ?? string.Empty,
                Rol = rol ?? string.Empty
            };
        }

        public static void CerrarSesion()
        {
            usuarioActual = null;
        }

        internal static void ConfigurarAuditoriaParaPruebas(
            Action<EventoAuditoriaSeguridad>? receptor)
        {
            receptorAuditoriaPrueba = receptor;
        }

        public static void ExigirPermiso(string permiso)
        {
            ExigirPermiso(permiso, "Seguridad", "Autorizar", string.Empty);
        }

        public static void ExigirPermiso(
            string permiso,
            string entidad,
            string accion,
            string detalle)
        {
            if (!TienePermiso(permiso))
            {
                Auditar(
                    permiso,
                    entidad,
                    accion,
                    detalle,
                    ResultadosAuditoria.Denegado);
                throw new UnauthorizedAccessException(
                    "No tiene permiso para realizar esta operacion.");
            }
        }

        public static bool TienePermiso(string permiso)
        {
            UsuarioSeguridad? usuario = UsuarioActual;
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Rol))
            {
                return false;
            }

            return PermisosPorRol.TryGetValue(usuario.Rol, out HashSet<string>? permisos) &&
                   permisos.Contains(permiso);
        }

        public static T EjecutarAutorizado<T>(
            string permiso,
            string entidad,
            string accion,
            string detalle,
            Func<T> operacion,
            Func<T, bool>? esExitoso = null)
        {
            if (operacion == null)
            {
                throw new ArgumentNullException(nameof(operacion));
            }

            ExigirPermiso(permiso, entidad, accion, detalle);

            try
            {
                T resultado = operacion();
                bool exitoso = esExitoso?.Invoke(resultado) ?? true;
                Auditar(
                    permiso,
                    entidad,
                    accion,
                    CombinarDetalle(detalle, resultado),
                    exitoso ? ResultadosAuditoria.Exito : ResultadosAuditoria.Fallido);
                return resultado;
            }
            catch (Exception ex)
            {
                try
                {
                    Auditar(
                        permiso,
                        entidad,
                        accion,
                        CombinarDetalle(detalle, "Error=" + ex.Message),
                        ResultadosAuditoria.Error);
                }
                catch (Exception errorAuditoria)
                {
                    throw new AggregateException(
                        "La operacion fallo y su auditoria tampoco pudo conservarse.",
                        ex,
                        errorAuditoria);
                }

                throw;
            }
        }

        public static void EjecutarAutorizado(
            string permiso,
            string entidad,
            string accion,
            string detalle,
            Action operacion)
        {
            EjecutarAutorizado(
                permiso,
                entidad,
                accion,
                detalle,
                () =>
                {
                    operacion();
                    return true;
                },
                resultado => resultado);
        }

        public static bool EsResultadoTextoExitoso(string? resultado)
        {
            if (string.IsNullOrWhiteSpace(resultado))
            {
                return false;
            }

            string texto = QuitarAcentos(resultado).ToLowerInvariant();
            string[] indicadoresError =
            {
                "error", "problema", "no se pudo", "no fue posible", "debe ",
                "seleccione", "inval", "excepcion", "fall", "duplicad",
                "ya existe", "ya esta", "no existe", "no se encontro",
                "conflict", "restriccion"
            };

            foreach (string indicador in indicadoresError)
            {
                if (texto.Contains(indicador, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        public static void Auditar(
            string permiso,
            string entidad,
            string accion,
            string detalle,
            string? resultado = null)
        {
            UsuarioSeguridad? usuario = UsuarioActual;
            string resultadoFinal = resultado ?? InferirResultado(detalle);

            if (receptorAuditoriaPrueba != null)
            {
                receptorAuditoriaPrueba(new EventoAuditoriaSeguridad(
                    usuario?.ID,
                    usuario?.Usuario ?? "Sin sesion",
                    usuario?.Rol ?? "Sin rol",
                    permiso,
                    entidad,
                    accion,
                    detalle,
                    resultadoFinal));
                return;
            }

            bool registradaEnBase = new DAL_AuditoriaSeguridad().RegistrarDurable(
                usuario?.ID,
                usuario?.Usuario ?? "Sin sesion",
                usuario?.Rol ?? "Sin rol",
                permiso,
                entidad,
                accion,
                detalle,
                resultadoFinal);

            if (!registradaEnBase)
            {
                Trace.TraceWarning(
                    "El evento de auditoria quedo pendiente en la cola local cifrada.");
            }
        }

        private static string InferirResultado(string detalle)
        {
            const string marcador = "Resultado=";
            string detalleSeguro = detalle ?? string.Empty;
            int posicion = detalleSeguro.IndexOf(
                marcador,
                StringComparison.OrdinalIgnoreCase);

            if (posicion < 0)
            {
                return ResultadosAuditoria.Exito;
            }

            string valor = detalleSeguro.Substring(posicion + marcador.Length);
            int separador = valor.IndexOf(';');
            if (separador >= 0)
            {
                valor = valor.Substring(0, separador);
            }

            return EsResultadoTextoExitoso(valor)
                ? ResultadosAuditoria.Exito
                : ResultadosAuditoria.Fallido;
        }

        private static string CombinarDetalle<T>(string detalle, T resultado)
        {
            string valorResultado = resultado?.ToString() ?? string.Empty;
            return string.IsNullOrWhiteSpace(detalle)
                ? "Resultado=" + valorResultado
                : detalle.Trim().TrimEnd(';') + "; Resultado=" + valorResultado;
        }

        private static string QuitarAcentos(string texto)
        {
            return texto
                .Replace('á', 'a').Replace('é', 'e').Replace('í', 'i')
                .Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u')
                .Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I')
                .Replace('Ó', 'O').Replace('Ú', 'U').Replace('Ü', 'U');
        }
    }
}

using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_MantenimientoEquipos
    {
        public static DataTable ListarEquipos(string busqueda, string estado)
        {
            return new DAL_MantenimientoEquipos().ListarEquipos(
                busqueda ?? string.Empty,
                estado ?? string.Empty);
        }

        public static DataTable ListarMantenimientos(
            int? idEquipo,
            DateTime desde,
            DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new ArgumentException("La fecha inicial no puede superar la fecha final.");
            }

            return new DAL_MantenimientoEquipos()
                .ListarMantenimientos(idEquipo, desde, hasta);
        }

        public static Entitie_ResumenEquipos CalcularResumen(DataTable equipos)
        {
            Entitie_ResumenEquipos resumen = new Entitie_ResumenEquipos();
            if (equipos == null) return resumen;

            foreach (DataRow fila in equipos.Rows)
            {
                string estado = Convert.ToString(fila["Estado"]) ?? string.Empty;
                if (estado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                int cantidad = ObtenerEntero(fila, "Cantidad");
                decimal costo = ObtenerDecimal(fila, "Costo");

                resumen.TotalActivos++;
                resumen.Unidades += cantidad;
                resumen.ValorTotal += cantidad * costo;

                if (fila.Table.Columns.Contains("Proximo mantenimiento") &&
                    fila["Proximo mantenimiento"] != DBNull.Value &&
                    Convert.ToDateTime(fila["Proximo mantenimiento"]).Date <=
                    DateTime.Today.AddDays(30))
                {
                    resumen.MantenimientoProximo++;
                }
            }

            return resumen;
        }

        public static int RegistrarEquipo(Entitie_Equipo equipo)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposCrear);
            NormalizarYValidarEquipo(equipo);
            int id = new DAL_MantenimientoEquipos().RegistrarEquipo(equipo);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposCrear,
                "Equipo",
                "Crear",
                $"ID={id}; Codigo={equipo.Codigo}; Nombre={equipo.Nombre}");
            return id;
        }

        public static void ActualizarEquipo(Entitie_Equipo equipo)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposActualizar);
            if (equipo.Id_Equipo <= 0)
            {
                throw new ArgumentException("Seleccione un equipo valido para actualizar.");
            }

            NormalizarYValidarEquipo(equipo);
            new DAL_MantenimientoEquipos().ActualizarEquipo(equipo);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposActualizar,
                "Equipo",
                "Actualizar",
                $"ID={equipo.Id_Equipo}; Codigo={equipo.Codigo}; Nombre={equipo.Nombre}");
        }

        public static void CambiarEstadoEquipo(int idEquipo, string estado)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposActualizar);
            if (idEquipo <= 0 ||
                (estado != "Activo" && estado != "En mantenimiento" && estado != "Inactivo"))
            {
                throw new ArgumentException("Equipo o estado no valido.");
            }

            new DAL_MantenimientoEquipos().CambiarEstadoEquipo(idEquipo, estado);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposActualizar,
                "Equipo",
                "Cambiar estado",
                $"ID={idEquipo}; Estado={estado}");
        }

        public static string EliminarEquipo(int idEquipo)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposEliminar);
            if (idEquipo <= 0)
            {
                throw new ArgumentException("Seleccione un equipo valido.");
            }

            string resultado = new DAL_MantenimientoEquipos().EliminarEquipo(idEquipo);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposEliminar,
                "Equipo",
                "Eliminar",
                $"ID={idEquipo}; Resultado={resultado}");
            return resultado;
        }

        public static int RegistrarMantenimiento(Entitie_MantenimientoEquipo mantenimiento)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposMantenimiento);
            NormalizarYValidarMantenimiento(mantenimiento, requiereId: false);
            int id = new DAL_MantenimientoEquipos().RegistrarMantenimiento(mantenimiento);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposMantenimiento,
                "MantenimientoEquipo",
                "Crear",
                $"ID={id}; Equipo={mantenimiento.Id_Equipo}; Costo={mantenimiento.Costo}");
            return id;
        }

        public static void ActualizarMantenimiento(Entitie_MantenimientoEquipo mantenimiento)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposMantenimiento);
            NormalizarYValidarMantenimiento(mantenimiento, requiereId: true);
            new DAL_MantenimientoEquipos().ActualizarMantenimiento(mantenimiento);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposMantenimiento,
                "MantenimientoEquipo",
                "Actualizar",
                $"ID={mantenimiento.Id_Mantenimiento}; Equipo={mantenimiento.Id_Equipo}; Costo={mantenimiento.Costo}");
        }

        public static string EliminarMantenimiento(int idMantenimiento)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.EquiposMantenimiento);
            if (idMantenimiento <= 0)
            {
                throw new ArgumentException("Seleccione un mantenimiento valido.");
            }

            string resultado = new DAL_MantenimientoEquipos().EliminarMantenimiento(idMantenimiento);
            BLL_Seguridad.Auditar(
                PermisosSistema.EquiposMantenimiento,
                "MantenimientoEquipo",
                "Eliminar",
                $"ID={idMantenimiento}; Resultado={resultado}");
            return resultado;
        }

        private static void NormalizarYValidarEquipo(Entitie_Equipo equipo)
        {
            if (equipo == null) throw new ArgumentNullException(nameof(equipo));

            equipo.Codigo = (equipo.Codigo ?? string.Empty).Trim().ToUpperInvariant();
            equipo.Nombre = (equipo.Nombre ?? string.Empty).Trim();
            equipo.Tipo = (equipo.Tipo ?? string.Empty).Trim();
            equipo.Ubicacion = (equipo.Ubicacion ?? string.Empty).Trim();
            equipo.Observaciones = (equipo.Observaciones ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(equipo.Codigo) || equipo.Codigo.Length > 30)
            {
                throw new ArgumentException("Ingrese un codigo de hasta 30 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(equipo.Nombre) || equipo.Nombre.Length > 100)
            {
                throw new ArgumentException("Ingrese el nombre del equipo.");
            }

            if (string.IsNullOrWhiteSpace(equipo.Tipo))
            {
                throw new ArgumentException("Seleccione el tipo de equipo.");
            }

            if (equipo.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }

            if (equipo.Costo < 0)
            {
                throw new ArgumentException("El costo no puede ser negativo.");
            }

            if (equipo.Fecha_Adquisicion.HasValue &&
                equipo.Fecha_Adquisicion.Value.Date > DateTime.Today)
            {
                throw new ArgumentException("La fecha de adquisicion no puede ser futura.");
            }
        }

        private static void NormalizarYValidarMantenimiento(
            Entitie_MantenimientoEquipo mantenimiento,
            bool requiereId)
        {
            if (mantenimiento == null) throw new ArgumentNullException(nameof(mantenimiento));

            mantenimiento.Tipo_Mantenimiento =
                (mantenimiento.Tipo_Mantenimiento ?? string.Empty).Trim();
            mantenimiento.Responsable = (mantenimiento.Responsable ?? string.Empty).Trim();
            mantenimiento.Descripcion = (mantenimiento.Descripcion ?? string.Empty).Trim();

            if (requiereId && mantenimiento.Id_Mantenimiento <= 0)
            {
                throw new ArgumentException("Seleccione un mantenimiento para actualizar.");
            }

            if (mantenimiento.Id_Equipo <= 0)
            {
                throw new ArgumentException("Seleccione un equipo.");
            }

            if (string.IsNullOrWhiteSpace(mantenimiento.Tipo_Mantenimiento))
            {
                throw new ArgumentException("Seleccione el tipo de mantenimiento.");
            }

            if (mantenimiento.Cantidad_Atendida <= 0)
            {
                throw new ArgumentException("La cantidad atendida debe ser mayor que cero.");
            }

            if (mantenimiento.Costo < 0)
            {
                throw new ArgumentException("El costo no puede ser negativo.");
            }

            if (mantenimiento.Fecha_Mantenimiento.Date > DateTime.Today)
            {
                throw new ArgumentException("La fecha de mantenimiento no puede ser futura.");
            }

            if (mantenimiento.Proximo_Mantenimiento.HasValue &&
                mantenimiento.Proximo_Mantenimiento.Value.Date <=
                mantenimiento.Fecha_Mantenimiento.Date)
            {
                throw new ArgumentException("El proximo mantenimiento debe ser posterior al actual.");
            }

            if (string.IsNullOrWhiteSpace(mantenimiento.Descripcion))
            {
                throw new ArgumentException("Describa el trabajo realizado.");
            }
        }

        private static int ObtenerEntero(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToInt32(fila[columna])
                : 0;
        }

        private static decimal ObtenerDecimal(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToDecimal(fila[columna])
                : 0;
        }
    }
}

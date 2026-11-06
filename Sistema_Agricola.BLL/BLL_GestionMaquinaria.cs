using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_GestionMaquinaria
    {
        public static DataTable ListarMaquinaria(string busqueda, string tipo, string estado)
        {
            return new DAL_GestionMaquinaria().ListarMaquinaria(
                busqueda ?? string.Empty,
                tipo ?? string.Empty,
                estado ?? string.Empty);
        }

        public static DataTable ListarTrabajadores()
        {
            return new DAL_GestionMaquinaria().ListarTrabajadores();
        }

        public static DataTable ListarAsignaciones(bool soloActivas)
        {
            return new DAL_GestionMaquinaria().ListarAsignaciones(soloActivas);
        }

        public static DataTable ListarMantenimientos(
            int? idMaquina,
            DateTime desde,
            DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new ArgumentException("La fecha inicial no puede superar la fecha final.");
            }

            return new DAL_GestionMaquinaria().ListarMantenimientos(idMaquina, desde, hasta);
        }

        public static Entitie_ResumenMaquinaria CalcularResumen(DataTable maquinas)
        {
            Entitie_ResumenMaquinaria resumen = new Entitie_ResumenMaquinaria();
            if (maquinas == null)
            {
                return resumen;
            }

            foreach (DataRow fila in maquinas.Rows)
            {
                string estado = Convert.ToString(fila["Estado"]) ?? string.Empty;
                if (estado == "Inactiva")
                {
                    continue;
                }

                resumen.TotalActivos++;
                resumen.ValorActivos += fila["Costo"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(fila["Costo"]);

                if (estado == "Disponible") resumen.Disponibles++;
                if (estado == "Asignada") resumen.Asignadas++;

                DateTime? proximoMantenimiento = ObtenerFecha(fila, "mantenimiento");
                if (proximoMantenimiento.HasValue &&
                    proximoMantenimiento.Value.Date <= DateTime.Today.AddDays(30))
                {
                    resumen.MantenimientoProximo++;
                }
            }

            return resumen;
        }

        private static DateTime? ObtenerFecha(DataRow fila, string textoColumna)
        {
            foreach (DataColumn columna in fila.Table.Columns)
            {
                if (columna.ColumnName.IndexOf(textoColumna, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }

                object valor = fila[columna];
                if (valor != DBNull.Value && DateTime.TryParse(Convert.ToString(valor), out DateTime fecha))
                {
                    return fecha;
                }
            }

            return null;
        }

        public static int RegistrarMaquinaria(Entitie_Maquinaria maquina)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaCrear,
                "Maquinaria",
                "Crear",
                $"Codigo={maquina?.Codigo}; Nombre={maquina?.Nombre_Maquina}",
                () =>
                {
                    ArgumentNullException.ThrowIfNull(maquina);
                    NormalizarYValidarMaquina(maquina);
                    return new DAL_GestionMaquinaria().RegistrarMaquinaria(maquina);
                },
                id => id > 0);
        }

        public static void ActualizarMaquinaria(Entitie_Maquinaria maquina)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaActualizar,
                "Maquinaria",
                "Actualizar",
                $"ID={maquina?.Id_Maquina}; Codigo={maquina?.Codigo}",
                () =>
                {
                    if (maquina == null || maquina.Id_Maquina <= 0)
                    {
                        throw new ArgumentException("Seleccione una maquinaria valida.");
                    }

                    NormalizarYValidarMaquina(maquina);
                    if (!new DAL_GestionMaquinaria().ActualizarMaquinaria(maquina))
                    {
                        throw new InvalidOperationException(
                            "No se encontro la maquinaria seleccionada.");
                    }
                });
        }

        public static void CambiarEstado(int idMaquina, string estado)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaActualizar,
                "Maquinaria",
                "Cambiar estado",
                $"ID={idMaquina}; Estado={estado}",
                () =>
                {
                    if (idMaquina <= 0 ||
                        (estado != "Disponible" && estado != "En mantenimiento" &&
                         estado != "Inactiva"))
                    {
                        throw new ArgumentException("Estado de maquinaria no valido.");
                    }

                    if (!new DAL_GestionMaquinaria().CambiarEstado(idMaquina, estado))
                    {
                        throw new InvalidOperationException(
                            "No se pudo cambiar el estado de la maquinaria.");
                    }
                });
        }

        public static string EliminarMaquinaria(int idMaquina)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaEliminar,
                "Maquinaria",
                "Eliminar",
                $"ID={idMaquina}",
                () =>
                {
                    if (idMaquina <= 0)
                    {
                        throw new ArgumentException("Seleccione una maquinaria valida.");
                    }

                    return new DAL_GestionMaquinaria().EliminarMaquinaria(idMaquina);
                },
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static int AsignarMaquinaria(Entitie_Asignacion_Maquinaria asignacion)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaAsignar,
                "AsignacionMaquinaria",
                "Asignar",
                $"Maquina={asignacion?.ID_Maquina}; Trabajador={asignacion?.ID_Trabajador}",
                () =>
                {
                    if (asignacion == null ||
                        asignacion.ID_Trabajador <= 0 || asignacion.ID_Maquina <= 0)
                    {
                        throw new ArgumentException("Seleccione el trabajador y la maquinaria.");
                    }

                    if (string.IsNullOrWhiteSpace(asignacion.Actividad))
                    {
                        throw new ArgumentException("Ingrese la actividad que se realizara.");
                    }

                    if (asignacion.Fecha_Asignacion.Date > DateTime.Today)
                    {
                        throw new ArgumentException("La fecha de asignacion no puede ser futura.");
                    }

                    if (asignacion.Fecha_Prevista_Devolucion.HasValue &&
                        asignacion.Fecha_Prevista_Devolucion.Value.Date <
                        asignacion.Fecha_Asignacion.Date)
                    {
                        throw new ArgumentException(
                            "La devolucion prevista no puede ser anterior a la asignacion.");
                    }

                    asignacion.Actividad = asignacion.Actividad.Trim();
                    asignacion.Observaciones =
                        (asignacion.Observaciones ?? string.Empty).Trim();
                    return new DAL_GestionMaquinaria().AsignarMaquinaria(asignacion);
                },
                id => id > 0);
        }

        public static void FinalizarAsignacion(int idAsignacion)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaAsignar,
                "AsignacionMaquinaria",
                "Finalizar",
                $"ID={idAsignacion}",
                () =>
                {
                    if (idAsignacion <= 0)
                    {
                        throw new ArgumentException("Seleccione una asignacion activa.");
                    }

                    if (!new DAL_GestionMaquinaria().FinalizarAsignacion(idAsignacion))
                    {
                        throw new InvalidOperationException(
                            "La asignacion ya fue finalizada o no existe.");
                    }
                });
        }

        public static int RegistrarMantenimiento(
            Entitie_MantenimientoMaquinaria mantenimiento)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaMantenimiento,
                "MantenimientoMaquinaria",
                "Crear",
                $"Maquina={mantenimiento?.ID_Maquina}; Costo={mantenimiento?.Costo}",
                () =>
                {
                    if (mantenimiento == null || mantenimiento.ID_Maquina <= 0)
                    {
                        throw new ArgumentException("Seleccione una maquinaria.");
                    }

                    if (string.IsNullOrWhiteSpace(mantenimiento.Tipo_Mantenimiento))
                    {
                        throw new ArgumentException("Seleccione el tipo de mantenimiento.");
                    }

                    if (string.IsNullOrWhiteSpace(mantenimiento.Descripcion))
                    {
                        throw new ArgumentException("Describa el trabajo realizado.");
                    }

                    if (mantenimiento.Costo < 0 || mantenimiento.Horometro < 0)
                    {
                        throw new ArgumentException(
                            "El costo y el horometro no pueden ser negativos.");
                    }

                    if (mantenimiento.Fecha_Mantenimiento.Date > DateTime.Today)
                    {
                        throw new ArgumentException(
                            "La fecha de mantenimiento no puede ser futura.");
                    }

                    if (mantenimiento.Proximo_Mantenimiento.HasValue &&
                        mantenimiento.Proximo_Mantenimiento.Value.Date <=
                        mantenimiento.Fecha_Mantenimiento.Date)
                    {
                        throw new ArgumentException(
                            "El proximo mantenimiento debe ser posterior al actual.");
                    }

                    mantenimiento.Descripcion = mantenimiento.Descripcion.Trim();
                    mantenimiento.Proveedor =
                        (mantenimiento.Proveedor ?? string.Empty).Trim();
                    return new DAL_GestionMaquinaria().RegistrarMantenimiento(mantenimiento);
                },
                id => id > 0);
        }

        private static void NormalizarYValidarMaquina(Entitie_Maquinaria maquina)
        {
            if (maquina == null) throw new ArgumentNullException(nameof(maquina));

            maquina.Codigo = (maquina.Codigo ?? string.Empty).Trim().ToUpperInvariant();
            maquina.Nombre_Maquina = (maquina.Nombre_Maquina ?? string.Empty).Trim();
            maquina.Tipo_Maquina = (maquina.Tipo_Maquina ?? string.Empty).Trim();
            maquina.Marca = (maquina.Marca ?? string.Empty).Trim();
            maquina.Modelo = (maquina.Modelo ?? string.Empty).Trim();
            maquina.Numero_Serie = (maquina.Numero_Serie ?? string.Empty).Trim().ToUpperInvariant();
            maquina.Ubicacion = (maquina.Ubicacion ?? string.Empty).Trim();
            maquina.Observaciones = (maquina.Observaciones ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(maquina.Codigo) || maquina.Codigo.Length > 30)
                throw new ArgumentException("Ingrese un codigo de hasta 30 caracteres.");
            if (string.IsNullOrWhiteSpace(maquina.Nombre_Maquina))
                throw new ArgumentException("Ingrese el nombre de la maquinaria.");
            if (string.IsNullOrWhiteSpace(maquina.Tipo_Maquina))
                throw new ArgumentException("Seleccione el tipo de maquinaria.");
            if (maquina.Costo_Maquina < 0 || maquina.Horometro < 0)
                throw new ArgumentException("El costo y el horometro no pueden ser negativos.");
            if (maquina.Fecha_Adquisicion.HasValue &&
                maquina.Fecha_Adquisicion.Value.Date > DateTime.Today)
                throw new ArgumentException("La fecha de adquisicion no puede ser futura.");
        }
    }
}

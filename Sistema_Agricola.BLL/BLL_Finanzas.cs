using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Finanzas
    {
        public static DataTable ListarMovimientos(
            DateTime fechaDesde,
            DateTime fechaHasta,
            string busqueda)
        {
            if (fechaDesde.Date > fechaHasta.Date)
            {
                throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final.");
            }

            return new DAL_Finanzas().ListarMovimientos(
                fechaDesde.Date,
                fechaHasta.Date,
                busqueda ?? string.Empty);
        }

        public static Entitie_ResumenFinanciero CalcularResumen(DataTable movimientos)
        {
            Entitie_ResumenFinanciero resumen = new Entitie_ResumenFinanciero();
            if (movimientos == null)
            {
                return resumen;
            }

            foreach (DataRow fila in movimientos.Rows)
            {
                string tipo = Convert.ToString(fila["Tipo"]) ?? string.Empty;
                string estado = Convert.ToString(fila["Estado"]) ?? string.Empty;
                decimal monto = fila["Monto"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(fila["Monto"]);

                if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    if (tipo.Equals("Ingreso", StringComparison.OrdinalIgnoreCase))
                    {
                        resumen.PendienteCobro += monto;
                    }
                    else
                    {
                        resumen.PendientePago += monto;
                    }

                    continue;
                }

                if (tipo.Equals("Ingreso", StringComparison.OrdinalIgnoreCase))
                {
                    resumen.Ingresos += monto;
                }
                else if (tipo.Equals("Gasto", StringComparison.OrdinalIgnoreCase))
                {
                    resumen.Gastos += monto;
                }
            }

            return resumen;
        }

        public static int InsertarMovimiento(Entitie_MovimientoFinanciero movimiento)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.FinanzasCrear,
                "MovimientoFinanciero",
                "Crear",
                $"Tipo={movimiento?.Tipo}; Monto={movimiento?.Monto}; Categoria={movimiento?.Categoria}",
                () =>
                {
                    ArgumentNullException.ThrowIfNull(movimiento);
                    NormalizarYValidar(movimiento);
                    return new DAL_Finanzas().InsertarMovimiento(movimiento);
                },
                id => id > 0);
        }

        public static void ActualizarMovimiento(Entitie_MovimientoFinanciero movimiento)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.FinanzasActualizar,
                "MovimientoFinanciero",
                "Actualizar",
                $"ID={movimiento?.ID_Movimiento}; Tipo={movimiento?.Tipo}; Monto={movimiento?.Monto}",
                () =>
                {
                    if (movimiento == null || movimiento.ID_Movimiento <= 0)
                    {
                        throw new ArgumentException(
                            "Seleccione un movimiento manual valido para actualizar.");
                    }

                    NormalizarYValidar(movimiento);
                    if (!new DAL_Finanzas().ActualizarMovimiento(movimiento))
                    {
                        throw new InvalidOperationException(
                            "No se encontro el movimiento que desea actualizar.");
                    }
                });
        }

        public static void EliminarMovimiento(int idMovimiento)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.FinanzasEliminar,
                "MovimientoFinanciero",
                "Eliminar",
                $"ID={idMovimiento}",
                () =>
                {
                    if (idMovimiento <= 0)
                    {
                        throw new ArgumentException(
                            "Seleccione un movimiento manual valido para eliminar.");
                    }

                    if (!new DAL_Finanzas().EliminarMovimiento(idMovimiento))
                    {
                        throw new InvalidOperationException(
                            "No se encontro el movimiento que desea eliminar.");
                    }
                });
        }

        private static void NormalizarYValidar(Entitie_MovimientoFinanciero movimiento)
        {
            if (movimiento == null)
            {
                throw new ArgumentNullException(nameof(movimiento));
            }

            movimiento.Tipo = (movimiento.Tipo ?? string.Empty).Trim();
            movimiento.Categoria = (movimiento.Categoria ?? string.Empty).Trim();
            movimiento.Descripcion = (movimiento.Descripcion ?? string.Empty).Trim();
            movimiento.Metodo_Pago = (movimiento.Metodo_Pago ?? string.Empty).Trim();
            movimiento.Estado = (movimiento.Estado ?? string.Empty).Trim();
            movimiento.Referencia = (movimiento.Referencia ?? string.Empty).Trim();
            movimiento.Fecha = movimiento.Fecha.Date;

            if (movimiento.Tipo != "Ingreso" && movimiento.Tipo != "Gasto")
            {
                throw new ArgumentException("Seleccione si el movimiento es un ingreso o un gasto.");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Categoria))
            {
                throw new ArgumentException("Seleccione una categoria.");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Descripcion))
            {
                throw new ArgumentException("Ingrese una descripcion.");
            }

            if (movimiento.Descripcion.Length > 200)
            {
                throw new ArgumentException("La descripcion no puede superar los 200 caracteres.");
            }

            if (movimiento.Monto <= 0)
            {
                throw new ArgumentException("El monto debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Metodo_Pago))
            {
                throw new ArgumentException("Seleccione un metodo de pago.");
            }

            if (movimiento.Estado != "Pagado" && movimiento.Estado != "Pendiente")
            {
                throw new ArgumentException("Seleccione un estado valido.");
            }

            if (movimiento.Estado == "Pagado" && movimiento.Fecha > DateTime.Today)
            {
                throw new ArgumentException("Un movimiento pagado no puede tener una fecha futura.");
            }

            if (movimiento.Referencia.Length > 80)
            {
                throw new ArgumentException("La referencia no puede superar los 80 caracteres.");
            }
        }
    }
}

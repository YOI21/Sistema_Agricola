using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public static class BLL_Produccion
    {
        public static DataTable ListarCosechasDisponibles(int? idProduccion = null)
        {
            return new DAL_Produccion().ListarCosechasDisponibles(idProduccion);
        }

        public static DataTable Listar(
            DateTime fechaDesde,
            DateTime fechaHasta,
            string busqueda = "",
            string estado = "")
        {
            if (fechaDesde.Date > fechaHasta.Date)
            {
                throw new ArgumentException(
                    "La fecha inicial no puede ser mayor que la fecha final.");
            }

            return new DAL_Produccion().Listar(
                fechaDesde.Date,
                fechaHasta.Date,
                busqueda ?? string.Empty,
                estado ?? string.Empty);
        }

        public static int Registrar(Entitie_Produccion produccion)
        {
            string permiso = produccion?.Estado == "Vendida"
                    ? PermisosSistema.ProduccionFinalizar
                    : PermisosSistema.ProduccionCrear;
            string accion = produccion?.Estado == "Vendida" ? "Vender" : "Crear";

            return BLL_Seguridad.EjecutarAutorizado(
                permiso,
                "Produccion",
                accion,
                $"Cosecha={produccion?.ID_Cosecha}; Estado={produccion?.Estado}",
                () =>
                {
                    ArgumentNullException.ThrowIfNull(produccion);
                    NormalizarYValidar(produccion);
                    if (produccion.Estado == "Cancelada")
                    {
                        throw new ArgumentException(
                            "No se puede registrar una produccion nueva como cancelada.");
                    }

                    return new DAL_Produccion().Registrar(produccion);
                },
                id => id > 0);
        }

        public static void Actualizar(Entitie_Produccion produccion)
        {
            string permiso = produccion?.Estado == "Vendida"
                ? PermisosSistema.ProduccionFinalizar
                : produccion?.Estado == "Cancelada"
                    ? PermisosSistema.ProduccionAnular
                    : PermisosSistema.ProduccionActualizar;
            string accion = produccion?.Estado == "Vendida"
                ? "Vender"
                : produccion?.Estado == "Cancelada" ? "Anular" : "Actualizar";

            BLL_Seguridad.EjecutarAutorizado(
                permiso,
                "Produccion",
                accion,
                $"ID={produccion?.ID_Produccion}; Estado={produccion?.Estado}",
                () =>
                {
                    if (produccion == null || produccion.ID_Produccion <= 0)
                    {
                        throw new ArgumentException(
                            "Seleccione una produccion valida para actualizar.");
                    }

                    NormalizarYValidar(produccion);
                    if (!new DAL_Produccion().Actualizar(produccion))
                    {
                        throw new InvalidOperationException(
                            "No se encontro la produccion que desea actualizar.");
                    }
                });
        }

        public static void Eliminar(int idProduccion)
        {
            BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.ProduccionEliminar,
                "Produccion",
                "Eliminar",
                $"ID={idProduccion}",
                () =>
                {
                    if (idProduccion <= 0)
                    {
                        throw new ArgumentException(
                            "Seleccione una produccion valida para eliminar.");
                    }

                    if (!new DAL_Produccion().Eliminar(idProduccion))
                    {
                        throw new InvalidOperationException(
                            "No se encontro la produccion que desea eliminar.");
                    }
                });
        }

        public static Entitie_ResumenProduccion CalcularResumen(DataTable? datos)
        {
            Entitie_ResumenProduccion resumen = new Entitie_ResumenProduccion();
            if (datos == null)
            {
                return resumen;
            }

            resumen.Lotes = datos.Rows.Count;
            foreach (DataRow fila in datos.Rows)
            {
                resumen.CantidadBruta += Decimal(fila, "Cantidad bruta");
                resumen.Merma += Decimal(fila, "Merma");
                resumen.CantidadNeta += Decimal(fila, "Cantidad neta");
                resumen.ValorProduccion += Decimal(fila, "Valor produccion");
                resumen.Costos += Decimal(fila, "Costo total");
                resumen.Beneficio += Decimal(fila, "Beneficio neto");
            }

            return resumen;
        }

        private static void NormalizarYValidar(Entitie_Produccion produccion)
        {
            ArgumentNullException.ThrowIfNull(produccion);

            produccion.Unidad_Medida = (produccion.Unidad_Medida ?? string.Empty).Trim();
            produccion.Destino = (produccion.Destino ?? string.Empty).Trim();
            produccion.Estado = (produccion.Estado ?? string.Empty).Trim();
            produccion.Observaciones = (produccion.Observaciones ?? string.Empty).Trim();
            produccion.Comprador = (produccion.Comprador ?? string.Empty).Trim();
            produccion.Metodo_Pago = (produccion.Metodo_Pago ?? string.Empty).Trim();
            produccion.Estado_Cobro = (produccion.Estado_Cobro ?? string.Empty).Trim();
            produccion.Referencia_Venta =
                (produccion.Referencia_Venta ?? string.Empty).Trim();
            produccion.Fecha_Produccion = produccion.Fecha_Produccion.Date;

            if (produccion.ID_Cosecha < 0)
            {
                throw new ArgumentException("Seleccione una cosecha.");
            }

            if (produccion.Fecha_Produccion > DateTime.Today)
            {
                throw new ArgumentException(
                    "La fecha de produccion no puede ser futura.");
            }

            if (string.IsNullOrWhiteSpace(produccion.Unidad_Medida) ||
                produccion.Unidad_Medida.Length > 20)
            {
                throw new ArgumentException("Seleccione una unidad de medida valida.");
            }

            if (produccion.Cantidad_Bruta <= 0)
            {
                throw new ArgumentException(
                    "La cantidad bruta debe ser mayor que cero.");
            }

            if (produccion.Merma < 0 || produccion.Merma >= produccion.Cantidad_Bruta)
            {
                throw new ArgumentException(
                    "La merma debe ser menor que la cantidad bruta.");
            }

            if (produccion.Precio_Unitario < 0 || produccion.Costo_Postcosecha < 0)
            {
                throw new ArgumentException(
                    "El precio y el costo poscosecha no pueden ser negativos.");
            }

            if (produccion.Estado != "Registrada" &&
                produccion.Estado != "Almacenada" &&
                produccion.Estado != "Vendida" &&
                produccion.Estado != "Cancelada")
            {
                throw new ArgumentException("Seleccione un estado valido.");
            }

            ValidarLongitud(produccion.Destino, 80, "El destino");
            ValidarLongitud(produccion.Observaciones, 250, "Las observaciones");
            ValidarLongitud(produccion.Comprador, 120, "El comprador");
            ValidarLongitud(produccion.Referencia_Venta, 80, "La referencia");

            if (produccion.Estado == "Vendida")
            {
                if (produccion.Precio_Unitario <= 0)
                {
                    throw new ArgumentException(
                        "Ingrese el precio unitario de la venta.");
                }

                if (!produccion.Fecha_Venta.HasValue ||
                    produccion.Fecha_Venta.Value.Date < produccion.Fecha_Produccion ||
                    produccion.Fecha_Venta.Value.Date > DateTime.Today)
                {
                    throw new ArgumentException(
                        "Ingrese una fecha de venta valida.");
                }

                if (string.IsNullOrWhiteSpace(produccion.Comprador))
                {
                    throw new ArgumentException("Ingrese el comprador.");
                }

                if (string.IsNullOrWhiteSpace(produccion.Metodo_Pago))
                {
                    throw new ArgumentException("Seleccione el metodo de pago.");
                }

                if (produccion.Estado_Cobro != "Pendiente" &&
                    produccion.Estado_Cobro != "Pagado")
                {
                    throw new ArgumentException("Seleccione el estado del cobro.");
                }
            }
        }

        private static void ValidarLongitud(string texto, int maximo, string campo)
        {
            if (texto.Length > maximo)
            {
                throw new ArgumentException(
                    $"{campo} no puede superar {maximo} caracteres.");
            }
        }

        private static decimal Decimal(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value
                ? Convert.ToDecimal(fila[columna])
                : 0;
        }

        private static string Detalle(int id, Entitie_Produccion produccion)
        {
            return
                $"ID={id}; Cosecha={produccion.ID_Cosecha}; " +
                $"Bruta={produccion.Cantidad_Bruta}; Neta={produccion.Cantidad_Neta}; " +
                $"Estado={produccion.Estado}; Valor={produccion.Ingreso_Estimado}";
        }
    }
}

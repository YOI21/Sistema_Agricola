using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Inventario
    {
        public static DataTable ListarProductos(string busqueda, string tipo, bool soloBajoStock)
        {
            return new DAL_Inventario().ListarProductos(
                busqueda ?? string.Empty,
                tipo ?? string.Empty,
                soloBajoStock);
        }

        public static DataTable ListarMovimientos(
            int? idInsumo,
            DateTime desde,
            DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final.");
            }

            return new DAL_Inventario().ListarMovimientos(idInsumo, desde, hasta);
        }

        public static Entitie_ResumenInventario CalcularResumen(DataTable productos)
        {
            Entitie_ResumenInventario resumen = new Entitie_ResumenInventario();
            if (productos == null)
            {
                return resumen;
            }

            foreach (DataRow fila in productos.Rows)
            {
                string estado = Convert.ToString(fila["Estado"]) ?? string.Empty;
                if (!estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                resumen.ProductosActivos++;
                decimal cantidad = ObtenerDecimal(fila, "Cantidad");
                decimal minimo = ObtenerDecimal(fila, "Stock minimo");
                if (minimo == 0)
                {
                    minimo = ObtenerDecimal(fila, "Stock mínimo");
                }

                decimal costo = ObtenerDecimal(fila, "Costo unitario");

                if (cantidad <= minimo)
                {
                    resumen.BajoStock++;
                }

                resumen.ValorInventario += cantidad * costo;

                if (fila.Table.Columns.Contains("Fecha vencimiento") &&
                    fila["Fecha vencimiento"] != DBNull.Value)
                {
                    DateTime vencimiento = Convert.ToDateTime(fila["Fecha vencimiento"]).Date;
                    if (vencimiento <= DateTime.Today.AddDays(30))
                    {
                        resumen.PorVencer++;
                    }
                }
            }

            return resumen;
        }

        public static int RegistrarProducto(Entitie_Insumo insumo, decimal stockInicial)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.InventarioCrear);
            NormalizarYValidarProducto(insumo);
            if (stockInicial < 0)
            {
                throw new ArgumentException("El stock inicial no puede ser negativo.");
            }

            int id = new DAL_Inventario().RegistrarProducto(insumo, stockInicial);
            BLL_Seguridad.Auditar(
                PermisosSistema.InventarioCrear,
                "Inventario",
                "Crear producto",
                $"ID={id}; Codigo={insumo.Codigo}; Nombre={insumo.Nombre}; StockInicial={stockInicial}");
            return id;
        }

        public static void ActualizarProducto(Entitie_Insumo insumo)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.InventarioActualizar);
            if (insumo.Id_Insumo <= 0)
            {
                throw new ArgumentException("Seleccione un producto valido para actualizar.");
            }

            NormalizarYValidarProducto(insumo);
            if (!new DAL_Inventario().ActualizarProducto(insumo))
            {
                throw new InvalidOperationException("No se encontro el producto seleccionado.");
            }

            BLL_Seguridad.Auditar(
                PermisosSistema.InventarioActualizar,
                "Inventario",
                "Actualizar producto",
                $"ID={insumo.Id_Insumo}; Codigo={insumo.Codigo}; Nombre={insumo.Nombre}");
        }

        public static void CambiarEstadoProducto(int idInsumo, string estado)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.InventarioActualizar);
            if (idInsumo <= 0 || (estado != "Activo" && estado != "Inactivo"))
            {
                throw new ArgumentException("Producto o estado no valido.");
            }

            if (!new DAL_Inventario().CambiarEstadoProducto(idInsumo, estado))
            {
                throw new InvalidOperationException("No se encontro el producto seleccionado.");
            }

            BLL_Seguridad.Auditar(
                PermisosSistema.InventarioActualizar,
                "Inventario",
                "Cambiar estado",
                $"ID={idInsumo}; Estado={estado}");
        }

        public static int RegistrarMovimiento(Entitie_MovimientoInventario movimiento)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.InventarioMovimiento);
            if (movimiento.Id_Insumo <= 0)
            {
                throw new ArgumentException("Seleccione un producto.");
            }

            movimiento.Tipo_Movimiento = (movimiento.Tipo_Movimiento ?? string.Empty).Trim();
            movimiento.Motivo = (movimiento.Motivo ?? string.Empty).Trim();
            movimiento.Referencia = (movimiento.Referencia ?? string.Empty).Trim();

            if (movimiento.Tipo_Movimiento != "Entrada" &&
                movimiento.Tipo_Movimiento != "Salida")
            {
                throw new ArgumentException("Seleccione entrada o salida.");
            }

            if (movimiento.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            }

            if (movimiento.Tipo_Movimiento == "Entrada" && movimiento.Costo_Unitario <= 0)
            {
                throw new ArgumentException("Ingrese el costo unitario de la entrada.");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Motivo))
            {
                throw new ArgumentException("Ingrese el motivo del movimiento.");
            }

            if (movimiento.Motivo.Length > 150 || movimiento.Referencia.Length > 80)
            {
                throw new ArgumentException("El motivo o la referencia supera la longitud permitida.");
            }

            if (movimiento.Fecha_Movimiento.Date > DateTime.Today)
            {
                throw new ArgumentException("La fecha del movimiento no puede ser futura.");
            }

            int id = new DAL_Inventario().RegistrarMovimiento(movimiento);
            BLL_Seguridad.Auditar(
                PermisosSistema.InventarioMovimiento,
                "Inventario",
                "Registrar movimiento",
                $"ID={id}; Insumo={movimiento.Id_Insumo}; Tipo={movimiento.Tipo_Movimiento}; Cantidad={movimiento.Cantidad}");
            return id;
        }

        private static void NormalizarYValidarProducto(Entitie_Insumo insumo)
        {
            if (insumo == null)
            {
                throw new ArgumentNullException(nameof(insumo));
            }

            insumo.Codigo = (insumo.Codigo ?? string.Empty).Trim().ToUpperInvariant();
            insumo.Nombre = (insumo.Nombre ?? string.Empty).Trim();
            insumo.Tipo = (insumo.Tipo ?? string.Empty).Trim();
            insumo.Unidad_Medida = (insumo.Unidad_Medida ?? string.Empty).Trim();
            insumo.Ubicacion = (insumo.Ubicacion ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(insumo.Codigo) || insumo.Codigo.Length > 30)
            {
                throw new ArgumentException("Ingrese un codigo de hasta 30 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(insumo.Nombre) || insumo.Nombre.Length > 100)
            {
                throw new ArgumentException("Ingrese un nombre de hasta 100 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(insumo.Tipo))
            {
                throw new ArgumentException("Seleccione el tipo de producto.");
            }

            if (string.IsNullOrWhiteSpace(insumo.Unidad_Medida))
            {
                throw new ArgumentException("Seleccione la unidad de medida.");
            }

            if (insumo.Costo < 0 || insumo.Stock_Minimo < 0)
            {
                throw new ArgumentException("El costo y el stock minimo no pueden ser negativos.");
            }
        }

        private static decimal ObtenerDecimal(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToDecimal(fila[columna])
                : 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Insumos
    {
        // Método para registrar un nuevo insumo
        public static string RegistrarInsumo(Entitie_Insumo insumo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.InventarioCrear,
                "Insumo",
                "Crear",
                $"Nombre={insumo?.Nombre}",
                () =>
                {
                    ArgumentNullException.ThrowIfNull(insumo);
                    return new DAL_Insumos().InsertarInsumo(insumo);
                },
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método sobrecargado para mayor comodidad
        public static string RegistrarInsumo(
            string nombre,
            string tipo,
            int costo,
            DateTime fecha,
            int cantidad)
        {
            Entitie_Insumo insumo = new Entitie_Insumo();
            insumo.Nombre = nombre;
            insumo.Tipo = tipo;
            insumo.Costo = costo;
            insumo.Fecha = fecha;
            insumo.Cantidad = cantidad;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.InventarioCrear,
                "Insumo",
                "Crear",
                $"Nombre={insumo.Nombre}; Cantidad={insumo.Cantidad}",
                () => new DAL_Insumos().InsertarInsumo(insumo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para actualizar un insumo
        public static string ActualizarInsumo(
            int idInsumo,
            string nombre,
            string tipo,
            int costo,
            DateTime fecha,
            int cantidad)
        {
            Entitie_Insumo insumo = new Entitie_Insumo();
            insumo.Id_Insumo = idInsumo;
            insumo.Nombre = nombre;
            insumo.Tipo = tipo;
            insumo.Costo = costo;
            insumo.Fecha = fecha;
            insumo.Cantidad = cantidad;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.InventarioActualizar,
                "Insumo",
                "Actualizar",
                $"ID={idInsumo}; Nombre={insumo.Nombre}",
                () => new DAL_Insumos().ActualizarInsumo(insumo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para listar todos los insumos
        public static DataTable ListarInsumos()
        {
            DAL_Insumos dal = new DAL_Insumos();
            return dal.ListarInsumos();
        }

        // Método para obtener un insumo por ID
        public static Entitie_Insumo? ObtenerInsumo(int idInsumo)
        {
            DAL_Insumos dal = new DAL_Insumos();
            return dal.ObtenerInsumo(idInsumo);
        }

        // Método para eliminar un insumo
        public static string EliminarInsumo(int idInsumo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.InventarioActualizar,
                "Insumo",
                "Eliminar",
                $"ID={idInsumo}",
                () => new DAL_Insumos().EliminarInsumo(idInsumo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para buscar insumos por nombre o tipo
        public static DataTable BuscarInsumos(string termino)
        {
            DAL_Insumos dal = new DAL_Insumos();
            return dal.BuscarInsumos(termino);
        }

        // Método para verificar si un insumo está por debajo del stock mínimo
        public static bool VerificarStockMinimo(int idInsumo, int stockMinimo)
        {
            Entitie_Insumo? insumo = ObtenerInsumo(idInsumo);
            if (insumo != null)
            {
                return insumo.Cantidad < stockMinimo;
            }
            return false;
        }

        // Método para obtener insumos con bajo stock
        public static DataTable ObtenerInsumosBajoStock(int stockMinimo)
        {
            DAL_Insumos dal = new DAL_Insumos();
            return dal.ObtenerInsumosBajoStock(stockMinimo);
        }
    }
}

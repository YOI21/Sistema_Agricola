using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Cosecha
    {
        // Método para registrar una nueva cosecha
        public static string Registrar_Cosecha(Entitie_Cosecha cosecha)
        {
            if (cosecha == null)
            {
                throw new ArgumentNullException(nameof(cosecha));
            }

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.CosechasCrear,
                "Cosecha",
                "Crear",
                $"Cultivo={cosecha.ID_Cultivo}; Estado={cosecha.Estado_Cosecha}",
                () => new DAL_Cosecha().Registrar_Cosecha(cosecha),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método sobrecargado que recibe parámetros individuales y crea el objeto Entitie_Cosecha
        public static string Registrar_Cosecha(
            int idCultivo,
            string estadoCosecha,
            decimal cantidadCosechada,
            decimal rendimiento,
            decimal calidad,
            string observaciones,
            DateTime fechaCosecha,
            decimal costoCosecha,
            decimal precioVenta,
            decimal beneficio)
        {
            Entitie_Cosecha obj = new Entitie_Cosecha();

            obj.ID_Cultivo = idCultivo;
            obj.Estado_Cosecha = estadoCosecha;
            obj.Cantidad_Cosechada = cantidadCosechada;
            obj.Rendimiento = rendimiento;
            obj.Calidad = calidad;
            obj.Observaciones = observaciones;
            obj.Fecha_Cosecha = fechaCosecha;
            obj.Costo_Cosecha = costoCosecha;
            obj.Precio_Venta = precioVenta;
            obj.Beneficio = beneficio;

            return Registrar_Cosecha(obj);
        }

        // Método para actualizar una cosecha existente
        public static string Actualizar_Cosecha(
            int idCosecha,
            int idCultivo,
            string estadoCosecha,
            decimal cantidadCosechada,
            decimal rendimiento,
            decimal calidad,
            string observaciones,
            DateTime fechaCosecha,
            decimal costoCosecha,
            decimal precioVenta,
            decimal beneficio)
        {
            Entitie_Cosecha obj = new Entitie_Cosecha();

            obj.ID_Cosecha = idCosecha;
            obj.ID_Cultivo = idCultivo;
            obj.Estado_Cosecha = estadoCosecha;
            obj.Cantidad_Cosechada = cantidadCosechada;
            obj.Rendimiento = rendimiento;
            obj.Calidad = calidad;
            obj.Observaciones = observaciones;
            obj.Fecha_Cosecha = fechaCosecha;
            obj.Costo_Cosecha = costoCosecha;
            obj.Precio_Venta = precioVenta;
            obj.Beneficio = beneficio;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.CosechasActualizar,
                "Cosecha",
                "Actualizar",
                $"ID={idCosecha}; Cultivo={idCultivo}; Estado={estadoCosecha}",
                () => new DAL_Cosecha().Registrar_Cosecha(obj),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para listar todas las cosechas
        public static DataTable Listar_Cosechas()
        {
            DAL_Cosecha dal = new DAL_Cosecha();
            return dal.Listar_Cosechas();
        }

        // Método para obtener detalles de una cosecha específica
        public static Entitie_Cosecha Obtener_Cosecha(int idCosecha)
        {
            DAL_Cosecha dal = new DAL_Cosecha();
            return dal.Obtener_Cosecha(idCosecha);
        }

        // Método para eliminar una cosecha
        public static string Eliminar_Cosecha(int idCosecha, int idCultivo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.CosechasEliminar,
                "Cosecha",
                "Eliminar",
                $"ID={idCosecha}; Cultivo={idCultivo}",
                () => new DAL_Cosecha().Eliminar_Cosecha(idCosecha, idCultivo),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para listar cultivos disponibles para cosechar
        public static DataTable Listar_Cultivos_Disponibles()
        {
            DAL_Cosecha dal = new DAL_Cosecha();
            return dal.Listar_Cultivos_Disponibles();
        }

        // Método para calcular el rendimiento (cantidad cosechada / cantidad sembrada)
        public static decimal Calcular_Rendimiento(decimal cantidadCosechada, decimal cantidadSembrada)
        {
            if (cantidadSembrada == 0)
                return 0;

            return Math.Round((cantidadCosechada / cantidadSembrada) * 100, 2);
        }

        // Método para calcular el beneficio (precio de venta - costo total)
        public static decimal Calcular_Beneficio(decimal precioVenta, decimal costoCosecha, decimal costoSiembra)
        {
            return precioVenta - (costoCosecha + costoSiembra);
        }
    }
}

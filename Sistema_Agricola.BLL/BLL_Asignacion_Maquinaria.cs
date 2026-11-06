using System;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Asignacion_Maquinaria
    {
        // Método para asignar maquinaria a un trabajador
        public static string Asignar_Maquinaria(int idTrabajador, int idMaquina, string observaciones)
        {
            Entitie_Asignacion_Maquinaria obj = new Entitie_Asignacion_Maquinaria
            {
                ID_Trabajador = idTrabajador,
                ID_Maquina = idMaquina,
                Observaciones = observaciones
            };

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaAsignar,
                "AsignacionMaquinaria",
                "Asignar",
                $"Maquina={idMaquina}; Trabajador={idTrabajador}",
                () => new DAL_Asignacion_Maquinaria().Asignar_Maquinaria(obj),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para finalizar una asignación
        public static string Finalizar_Asignacion(int idAsignacion)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.MaquinariaAsignar,
                "AsignacionMaquinaria",
                "Finalizar",
                $"ID={idAsignacion}",
                () => new DAL_Asignacion_Maquinaria().Finalizar_Asignacion(idAsignacion),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para listar asignaciones activas
        public static DataTable Listar_Asignaciones_Activas()
        {
            DAL_Asignacion_Maquinaria dal = new DAL_Asignacion_Maquinaria();
            return dal.Listar_Asignaciones_Activas();
        }

        // Método para listar operadores
        public static DataTable Listar_Operadores()
        {
            DAL_Asignacion_Maquinaria dal = new DAL_Asignacion_Maquinaria();
            return dal.Listar_Operadores();
        }

        // Método para listar maquinaria disponible
        public static DataTable Listar_Maquinaria_Disponible()
        {
            DAL_Asignacion_Maquinaria dal = new DAL_Asignacion_Maquinaria();
            return dal.Listar_Maquinaria_Disponible();
        }
    }
}

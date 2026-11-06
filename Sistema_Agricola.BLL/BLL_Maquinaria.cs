using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.BLL
{
    public class BLL_Maquinaria
    {
        public static string Ingresar_Maquinaria(string Nombre_Maquina, string Tipo_Maquina, decimal Costo_Maquina,
           DateTime Fecha_Adquisicion, string Mantenimiento_Programado)
        {
            try
            {
                BLL_Seguridad.ExigirPermiso(PermisosSistema.MaquinariaCrear);
                // Verificar parámetros obligatorios
                if (string.IsNullOrEmpty(Nombre_Maquina))
                    return "Error: El nombre de la máquina es obligatorio";

                if (string.IsNullOrEmpty(Tipo_Maquina))
                    return "Error: El tipo de máquina es obligatorio";

                if (Costo_Maquina <= 0)
                    return "Error: El costo debe ser mayor que cero";

                // Crear objeto de entidad
                DAL_Maquinaria ma = new DAL_Maquinaria();
                Entitie_Maquinaria obj = new Entitie_Maquinaria();

                obj.Nombre_Maquina = Nombre_Maquina;
                obj.Tipo_Maquina = Tipo_Maquina;
                obj.Costo_Maquina = Costo_Maquina;
                obj.Fecha_Adquisicion = Fecha_Adquisicion;
                obj.Mantenimiento_Programado = Mantenimiento_Programado;

                // Agregar el Estado (como nuevo campo)
                obj.Estado = "Activa";

                // Registrar para depuración
                Console.WriteLine($"BLL: Registrando maquinaria: {Nombre_Maquina}, {Tipo_Maquina}, {Costo_Maquina}");

                // Llamar al DAL
                string resultado = ma.Ingresar_Maquinaria(obj);
                BLL_Seguridad.Auditar(
                    PermisosSistema.MaquinariaCrear,
                    "Maquinaria",
                    "Crear",
                    $"Nombre={Nombre_Maquina}; Tipo={Tipo_Maquina}; Resultado={resultado}");

                // Verificar resultado para depuración
                Console.WriteLine($"BLL: Resultado del registro: {resultado}");

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BLL_Maquinaria.Ingresar_Maquinaria: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }


        public static DataTable Tractor()
        {
            DAL_Maquinaria i = new DAL_Maquinaria();
            return i.Tractor();
        }

        public static DataTable Pulverizadora()
        {
            DAL_Maquinaria p = new DAL_Maquinaria();
            return p.Pulverizadora();
        }

        public static DataTable Cosechadora()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.Cosechadora();
        }

        public static DataTable CargadorFrontal()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.CargadorFrontal();
        }

        public static DataTable Equipodeforraje()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.Equipodeforraje();
        }

        public static DataTable Labranza()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.Labranza();
        }

        public static DataTable Sembradora()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.Sembradora();
        }

        public static DataTable Desmalezadora()
        {
            DAL_Maquinaria c = new DAL_Maquinaria();
            return c.Desmalezadora();
        }
        public static DataTable BuscarMaquinaria(string valor)
        {
            DAL_Maquinaria bc = new DAL_Maquinaria();
            return bc.BuscarMaquinaria(valor);
        }


    }
}

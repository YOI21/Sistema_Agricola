using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.BLL
{
    public class BLL_RP
    {

        public static DataTable Mostrar_Siembras()
        {
        
            DAL_Siembra mc = new DAL_Siembra();
            return mc.Mostrar_Cultivos();

        }




        public static string Insertar_RP(string nombrecultivo, string tipocultivo, string categoriacultivo,
    string estacionsiembra, string tiposuelo, string duracion)
        {
            DAL_RP rp = new DAL_RP();  // Cambiar aquí a DAL_RP
            Entitie_RP obj = new Entitie_RP();
            obj.Nombre_Cultivo = nombrecultivo;
            obj.Tipo_Cultivo = tipocultivo;
            obj.Categoria_Cultivo = categoriacultivo;
            obj.Estacion_Siembra = estacionsiembra;
            obj.Tipo_Suelo = tiposuelo;
            obj.Duracion = duracion;
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.ProduccionCrear,
                "RegistroProduccion",
                "Crear",
                $"Cultivo={nombrecultivo}; Tipo={tipocultivo}; Duracion={duracion}",
                () => rp.Insertar_RP(obj),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }


    }
}

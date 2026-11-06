using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Common;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Terreno
    {
        // Método para registrar un nuevo terreno
        public static string Registrar_Terreno(Entitie_Terreno terreno)
        {
            if (terreno == null)
            {
                throw new ArgumentNullException(nameof(terreno));
            }

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.TerrenosCrear,
                "Terreno",
                "Crear",
                $"Nombre={terreno.Nombre}; Area={terreno.Area}; Unidad={terreno.UnidadMedida}",
                () => new DAL_Terreno().Insertar_Terreno(terreno),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método sobrecargado para mayor comodidad
        public static string Registrar_Terreno(
            string nombre,
            decimal area,
            string unidadMedida,
            string tipoSuelo,
            decimal pH,
            decimal pendiente,
            decimal humedad,
            string ubicacion,
            string propietario,
            string estadoActual,
            string observaciones,
            string drenaje = "",
            string fuenteAgua = "",
            string provincia = "",
            string zonaAgroclimatica = "",
            decimal precipitacionAnual = 0,
            bool disponibilidadRiego = false)
        {
            Entitie_Terreno terreno = new Entitie_Terreno();
            terreno.Nombre = nombre;
            terreno.Area = area;
            terreno.UnidadMedida = unidadMedida;
            terreno.TipoSuelo = tipoSuelo;
            terreno.pH = pH;
            terreno.Pendiente = pendiente;
            terreno.Humedad = humedad;
            terreno.Drenaje = drenaje;
            terreno.FuenteAgua = fuenteAgua;
            terreno.Provincia = provincia;
            terreno.ZonaAgroclimatica = zonaAgroclimatica;
            terreno.PrecipitacionAnual = precipitacionAnual;
            terreno.DisponibilidadRiego = disponibilidadRiego;
            terreno.Ubicacion = ubicacion;
            terreno.Propietario = propietario;
            terreno.EstadoActual = estadoActual;
            terreno.Observaciones = observaciones;
            terreno.FechaRegistro = DateTime.Now;

            return Registrar_Terreno(terreno);
        }

        // Método para actualizar un terreno
        public static string Actualizar_Terreno(
            int ID_Terreno,
            string nombre,
            decimal area,
            string unidadMedida,
            string tipoSuelo,
            decimal pH,
            decimal pendiente,
            decimal humedad,
            string ubicacion,
            string propietario,
            string estadoActual,
            string observaciones,
            string drenaje = "",
            string fuenteAgua = "",
            string provincia = "",
            string zonaAgroclimatica = "",
            decimal precipitacionAnual = 0,
            bool disponibilidadRiego = false)
        {
            Entitie_Terreno terreno = new Entitie_Terreno();
            terreno.ID_Terreno = ID_Terreno;
            terreno.Nombre = nombre;
            terreno.Area = area;
            terreno.UnidadMedida = unidadMedida;
            terreno.TipoSuelo = tipoSuelo;
            terreno.pH = pH;
            terreno.Pendiente = pendiente;
            terreno.Humedad = humedad;
            terreno.Drenaje = drenaje;
            terreno.FuenteAgua = fuenteAgua;
            terreno.Provincia = provincia;
            terreno.ZonaAgroclimatica = zonaAgroclimatica;
            terreno.PrecipitacionAnual = precipitacionAnual;
            terreno.DisponibilidadRiego = disponibilidadRiego;
            terreno.Ubicacion = ubicacion;
            terreno.Propietario = propietario;
            terreno.EstadoActual = estadoActual;
            terreno.Observaciones = observaciones;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.TerrenosActualizar,
                "Terreno",
                "Actualizar",
                $"ID={ID_Terreno}; Nombre={nombre}; Area={area}; Unidad={unidadMedida}",
                () => new DAL_Terreno().Actualizar_Terreno(terreno),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para listar todos los terrenos
        public static DataTable Listar_Terrenos()
        {
            DAL_Terreno dal = new DAL_Terreno();
            return dal.Listar_Terrenos();
        }

        // Método para obtener un terreno por ID
        public static Entitie_Terreno? Obtener_Terreno(int ID_Terreno)
        {
            DAL_Terreno dal = new DAL_Terreno();
            return dal.Obtener_Terreno(ID_Terreno);
        }

        // Método para eliminar un terreno
        public static string Eliminar_Terreno(int ID_Terreno)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.TerrenosEliminar,
                "Terreno",
                "Eliminar",
                $"ID={ID_Terreno}",
                () => new DAL_Terreno().Eliminar_Terreno(ID_Terreno),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        // Método para validar si un cultivo es compatible con un terreno
        public static bool ValidarCompatibilidadCultivo(int ID_Terreno, string nombreCultivo, string tipoCultivo, string categoriaCultivo)
        {
            EvaluacionAgricola evaluacion = EvaluarCompatibilidadCultivo(
                ID_Terreno, nombreCultivo, string.Empty, DateTime.Today);
            return evaluacion.Nivel == NivelRecomendacionAgricola.Recomendado ||
                   evaluacion.Nivel == NivelRecomendacionAgricola.Condicionado;
        }

        public static EvaluacionAgricola EvaluarCompatibilidadCultivo(
            int idTerreno,
            string nombreCultivo,
            string variedad,
            DateTime fechaSiembra)
        {
            Entitie_Terreno? terreno = new DAL_Terreno().Obtener_Terreno(idTerreno);
            if (terreno == null)
            {
                return BLL_RecomendacionesAgricolas.Evaluar(nombreCultivo, new CondicionesAgroclimaticas
                {
                    FechaSiembra = fechaSiembra,
                    Variedad = variedad
                });
            }

            return BLL_RecomendacionesAgricolas.Evaluar(
                nombreCultivo,
                CrearCondiciones(terreno, variedad, fechaSiembra));
        }

        // Método para obtener recomendaciones de cultivos para un terreno
        public static List<string> RecomendarCultivosPorTerreno(int ID_Terreno)
        {
            try
            {
                Entitie_Terreno? terreno = new DAL_Terreno().Obtener_Terreno(ID_Terreno);
                if (terreno == null)
                    return new List<string>();

                CondicionesAgroclimaticas condiciones = CrearCondiciones(
                    terreno, string.Empty, DateTime.Today);
                return BLL_RecomendacionesAgricolas.CultivosSoportados
                    .Select(c => new { Cultivo = c, Evaluacion = BLL_RecomendacionesAgricolas.Evaluar(c, condiciones) })
                    .Where(x => x.Evaluacion.Nivel == NivelRecomendacionAgricola.Recomendado ||
                                x.Evaluacion.Nivel == NivelRecomendacionAgricola.Condicionado)
                    .OrderByDescending(x => x.Evaluacion.Puntuacion)
                    .ThenBy(x => x.Cultivo)
                    .Select(x => x.Cultivo)
                    .ToList();
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex, "BLL_Terreno.RecomendarCultivosPorTerreno");
                return new List<string>();
            }
        }

        private static CondicionesAgroclimaticas CrearCondiciones(
            Entitie_Terreno terreno,
            string variedad,
            DateTime fechaSiembra)
        {
            return new CondicionesAgroclimaticas
            {
                FechaSiembra = fechaSiembra,
                Provincia = terreno.Provincia,
                ZonaAgroclimatica = terreno.ZonaAgroclimatica,
                TipoSuelo = terreno.TipoSuelo,
                PH = terreno.pH > 0 ? terreno.pH : null,
                Humedad = terreno.Humedad > 0 ? terreno.Humedad : null,
                Drenaje = terreno.Drenaje,
                Pendiente = terreno.Pendiente >= 0 ? terreno.Pendiente : null,
                PrecipitacionAnual = terreno.PrecipitacionAnual > 0 ? terreno.PrecipitacionAnual : null,
                DisponibilidadRiego = terreno.DisponibilidadRiego,
                Variedad = variedad
            };
        }
    }
}

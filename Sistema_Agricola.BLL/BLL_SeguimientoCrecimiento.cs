using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_SeguimientoCrecimiento
    {
        public static string Insertar_SeguimientoCrecimiento(int idCultivo, DateTime fechaRegistro,
                                                            string etapaCrecimiento, decimal alturaPlanta,
                                                            int numHojas, string estadoSalud,
                                                            string observaciones, decimal porcentajeCrecimiento,
                                                            string problemasDetectados, string accionesTomadas)
        {
            DAL_SeguimientoCrecimiento dal = new DAL_SeguimientoCrecimiento();
            Entitie_SeguimientoCrecimiento obj = new Entitie_SeguimientoCrecimiento();

            obj.ID_Cultivo = idCultivo;
            obj.Fecha_Registro = fechaRegistro;
            obj.Etapa_Crecimiento = etapaCrecimiento;
            obj.Altura_Planta = alturaPlanta;
            obj.Num_Hojas = numHojas;
            obj.Estado_Salud = estadoSalud;
            obj.Observaciones = observaciones;
            obj.Porcentaje_Crecimiento = porcentajeCrecimiento;
            obj.Problemas_Detectados = problemasDetectados;
            obj.Acciones_Tomadas = accionesTomadas;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SeguimientoCrear,
                "SeguimientoCrecimiento",
                "Crear",
                $"Cultivo={idCultivo}; Fecha={fechaRegistro:yyyy-MM-dd}; Etapa={etapaCrecimiento}",
                () => dal.Insertar_SeguimientoCrecimiento(obj),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static DataTable Mostrar_SeguimientoCrecimiento(int idCultivo)
        {
            DAL_SeguimientoCrecimiento dal = new DAL_SeguimientoCrecimiento();
            return dal.Mostrar_SeguimientoCrecimiento(idCultivo);
        }

        public static string Actualizar_SeguimientoCrecimiento(int idSeguimiento, DateTime fechaRegistro,
                                                              string etapaCrecimiento, decimal alturaPlanta,
                                                              int numHojas, string estadoSalud,
                                                              string observaciones, decimal porcentajeCrecimiento,
                                                              string problemasDetectados, string accionesTomadas)
        {
            DAL_SeguimientoCrecimiento dal = new DAL_SeguimientoCrecimiento();
            Entitie_SeguimientoCrecimiento obj = new Entitie_SeguimientoCrecimiento();

            obj.ID_Seguimiento = idSeguimiento;
            obj.Fecha_Registro = fechaRegistro;
            obj.Etapa_Crecimiento = etapaCrecimiento;
            obj.Altura_Planta = alturaPlanta;
            obj.Num_Hojas = numHojas;
            obj.Estado_Salud = estadoSalud;
            obj.Observaciones = observaciones;
            obj.Porcentaje_Crecimiento = porcentajeCrecimiento;
            obj.Problemas_Detectados = problemasDetectados;
            obj.Acciones_Tomadas = accionesTomadas;

            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SeguimientoActualizar,
                "SeguimientoCrecimiento",
                "Actualizar",
                $"ID={idSeguimiento}; Fecha={fechaRegistro:yyyy-MM-dd}; Etapa={etapaCrecimiento}",
                () => dal.Actualizar_SeguimientoCrecimiento(obj),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static string Eliminar_SeguimientoCrecimiento(int idSeguimiento)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.SeguimientoEliminar,
                "SeguimientoCrecimiento",
                "Eliminar",
                $"ID={idSeguimiento}",
                () => new DAL_SeguimientoCrecimiento().Eliminar_SeguimientoCrecimiento(idSeguimiento),
                BLL_Seguridad.EsResultadoTextoExitoso);
        }

        public static DataTable Obtener_Reporte_Crecimiento()
        {
            DAL_SeguimientoCrecimiento dal = new DAL_SeguimientoCrecimiento();
            DataTable tabla = dal.Obtener_Reporte_Crecimiento();

            AgregarColumna(tabla, "Dias_Desde_Siembra", typeof(int));
            AgregarColumna(tabla, "Crecimiento_Esperado", typeof(decimal));
            AgregarColumna(tabla, "Desviacion", typeof(decimal));
            AgregarColumna(tabla, "Evaluacion", typeof(string));

            foreach (DataRow fila in tabla.Rows)
            {
                CalcularEvaluacion(fila);
            }

            return tabla;
        }

        private static void AgregarColumna(DataTable tabla, string nombre, Type tipo)
        {
            if (!tabla.Columns.Contains(nombre))
            {
                tabla.Columns.Add(nombre, tipo);
            }
        }

        private static void CalcularEvaluacion(DataRow fila)
        {
            fila["Dias_Desde_Siembra"] = DBNull.Value;
            fila["Crecimiento_Esperado"] = DBNull.Value;
            fila["Desviacion"] = DBNull.Value;

            if (fila["ID_Seguimiento"] == DBNull.Value)
            {
                fila["Evaluacion"] = "Sin seguimiento";
                return;
            }

            if (!ObtenerFecha(fila, "Fecha_Plantacion", out DateTime fechaPlantacion) ||
                !ObtenerFecha(fila, "Fecha_Cosecha_Estimada", out DateTime fechaCosecha) ||
                !ObtenerFecha(fila, "Fecha_Registro", out DateTime fechaRegistro))
            {
                fila["Evaluacion"] = "Sin referencia";
                return;
            }

            int diasCiclo = (fechaCosecha.Date - fechaPlantacion.Date).Days;
            int diasTranscurridos = (fechaRegistro.Date - fechaPlantacion.Date).Days;

            if (diasCiclo <= 0 || diasTranscurridos < 0)
            {
                fila["Evaluacion"] = "Fecha inconsistente";
                return;
            }

            fila["Dias_Desde_Siembra"] = diasTranscurridos;
            decimal esperado = Math.Round(
                Math.Min(100m, Math.Max(0m, (decimal)diasTranscurridos / diasCiclo * 100m)), 2);
            fila["Crecimiento_Esperado"] = esperado;

            if (!ObtenerDecimal(fila, "Porcentaje_Crecimiento", out decimal crecimientoReal))
            {
                fila["Evaluacion"] = "Sin evaluación";
                return;
            }

            decimal desviacion = Math.Round(crecimientoReal - esperado, 2);
            fila["Desviacion"] = desviacion;
            fila["Evaluacion"] = EvaluarEstado(fila, desviacion);
        }

        private static string EvaluarEstado(DataRow fila, decimal desviacion)
        {
            string salud = ObtenerTexto(fila, "Estado_Salud").ToLowerInvariant();
            string problemas = ObtenerTexto(fila, "Problemas_Detectados").ToLowerInvariant();

            bool saludCritica = salud.Contains("malo") || salud.Contains("crític") ||
                                 salud.Contains("critic") || salud.Contains("enferm");
            bool saludRegular = salud.Contains("regular");
            bool tieneProblemas = !string.IsNullOrWhiteSpace(problemas) &&
                                  !problemas.Contains("ningun") &&
                                  !problemas.Contains("ningún") &&
                                  !problemas.Contains("sin problema") &&
                                  problemas != "n/a" && problemas != "no";

            if (saludCritica || desviacion <= -20m)
            {
                return "Requiere atención";
            }

            if (saludRegular || tieneProblemas || desviacion < -10m)
            {
                return "En seguimiento";
            }

            if (desviacion >= 10m)
            {
                return "Adelantado";
            }

            return "En objetivo";
        }

        private static bool ObtenerFecha(DataRow fila, string columna, out DateTime valor)
        {
            valor = default;
            return fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value &&
                   DateTime.TryParse(Convert.ToString(fila[columna]), out valor);
        }

        private static bool ObtenerDecimal(DataRow fila, string columna, out decimal valor)
        {
            valor = 0m;
            return fila.Table.Columns.Contains(columna) &&
                   fila[columna] != DBNull.Value &&
                   decimal.TryParse(Convert.ToString(fila[columna]), out valor);
        }

        private static string ObtenerTexto(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value
                ? Convert.ToString(fila[columna])?.Trim() ?? string.Empty
                : string.Empty;
        }
    }
}

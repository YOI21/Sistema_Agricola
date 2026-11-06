using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using Sistema_Agricola.Common;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Alertas
    {
        // Método para crear una nueva alerta
        public static int CrearAlerta(Entitie_Alerta alerta)
        {
            ValidarAlerta(alerta);
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AlertasCrear,
                "Alerta",
                "Crear",
                $"Cultivo={alerta.ID_Cultivo}; Tipo={alerta.Tipo_Alerta}; Fecha={alerta.Fecha_Programada:yyyy-MM-dd}",
                () => new DAL_Alertas().Insertar_Alerta(alerta),
                id => id > 0);
        }

        // Método para crear alerta con parámetros individuales
        public static int CrearAlerta(int idCultivo, string titulo, string descripcion,
            DateTime fechaProgramada, string tipoAlerta, string prioridad = "Media", int diasAnticipacion = 7)
        {
            Entitie_Alerta alerta = new Entitie_Alerta
            {
                ID_Cultivo = idCultivo,
                Titulo = titulo,
                Descripcion = descripcion,
                Fecha_Programada = fechaProgramada,
                Tipo_Alerta = tipoAlerta,
                Prioridad = prioridad,
                Dias_Anticipacion = diasAnticipacion
            };

            return CrearAlerta(alerta);
        }

        // Método para actualizar una alerta existente
        public static bool ActualizarAlerta(Entitie_Alerta alerta)
        {
            ValidarAlerta(alerta);
            string permiso = ObtenerPermisoActualizacion(alerta.Estado);
            string accion = ObtenerAccionActualizacion(alerta.Estado);
            return BLL_Seguridad.EjecutarAutorizado(
                permiso,
                "Alerta",
                accion,
                $"ID={alerta.ID_Alerta}; Cultivo={alerta.ID_Cultivo}; Estado={alerta.Estado}",
                () => new DAL_Alertas().Actualizar_Alerta(alerta),
                resultado => resultado);
        }

        // Método para actualizar una alerta con parámetros individuales
        public static bool ActualizarAlerta(int idAlerta, int idCultivo, string titulo,
            string descripcion, DateTime fechaProgramada, string tipoAlerta,
            string estado, string prioridad, int diasAnticipacion)
        {
            Entitie_Alerta alerta = new Entitie_Alerta
            {
                ID_Alerta = idAlerta,
                ID_Cultivo = idCultivo,
                Titulo = titulo,
                Descripcion = descripcion,
                Fecha_Programada = fechaProgramada,
                Tipo_Alerta = tipoAlerta,
                Estado = estado,
                Prioridad = prioridad,
                Dias_Anticipacion = diasAnticipacion
            };

            return ActualizarAlerta(alerta);
        }

        // Método para marcar una alerta como completada
        public static bool CompletarAlerta(int idAlerta)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AlertasCompletar,
                "Alerta",
                "Completar",
                $"ID={idAlerta}",
                () => new DAL_Alertas().Completar_Alerta(idAlerta),
                resultado => resultado);
        }

        // Método para eliminar una alerta
        public static bool EliminarAlerta(int idAlerta)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AlertasEliminar,
                "Alerta",
                "Eliminar",
                $"ID={idAlerta}",
                () => new DAL_Alertas().Eliminar_Alerta(idAlerta),
                resultado => resultado);
        }

        // Método para obtener alertas por cultivo
        public static DataTable ObtenerAlertasPorCultivo(int idCultivo)
        {
            DAL_Alertas dal = new DAL_Alertas();
            return dal.Obtener_Alertas_Por_Cultivo(idCultivo);
        }

        // Método para obtener todas las alertas registradas
        public static DataTable ObtenerTodasAlertas()
        {
            DAL_Alertas dal = new DAL_Alertas();
            return dal.Obtener_Todas_Alertas();
        }

        // Método para obtener todas las alertas pendientes
        public static DataTable ObtenerAlertasPendientes()
        {
            DAL_Alertas dal = new DAL_Alertas();
            return dal.Obtener_Alertas_Pendientes();
        }

        // Método para obtener alertas próximas
        public static DataTable ObtenerAlertasProximas()
        {
            return ObtenerAlertasProximas(
                () => new DAL_Alertas().Obtener_Alertas_Proximas());
        }

        internal static DataTable ObtenerAlertasProximas(
            Func<DataTable> cargarAlertas)
        {
            ArgumentNullException.ThrowIfNull(cargarAlertas);
            return cargarAlertas();
        }

        // Método para marcar una alerta como notificada
        public static bool MarcarAlertaNotificada(int idAlerta)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AlertasNotificar,
                "Alerta",
                "Marcar notificada",
                $"ID={idAlerta}",
                () => new DAL_Alertas().Marcar_Alerta_Notificada(idAlerta),
                resultado => resultado);
        }

        // Método para generar alertas automáticas basadas en la siembra
        public static int GenerarAlertasAutomaticas(int idCultivo)
        {
            return BLL_Seguridad.EjecutarAutorizado(
                PermisosSistema.AlertasCrear,
                "Alerta",
                "Generar automaticas",
                $"Cultivo={idCultivo}",
                () => GenerarAlertasAutomaticasInterno(idCultivo),
                resultado => resultado >= 0);
        }

        private static int GenerarAlertasAutomaticasInterno(int idCultivo)
        {
            try
            {
                // Verificar que el ID del cultivo sea válido
                if (idCultivo <= 0)
                {
                    throw new ArgumentException("ID de cultivo inválido: " + idCultivo);
                }

                // Obtenemos los datos del cultivo
                Entitie_Siembra? cultivo = BLL_Siembra.Obtener_Cultivo(idCultivo);

                if (cultivo == null)
                {
                    throw new Exception("No se pudo obtener información del cultivo con ID: " + idCultivo);
                }

                int alertasCreadas = 0;

                // Alerta para cosecha (7 días antes de la fecha de cosecha)
                alertasCreadas += CrearAlertaAutomatica(
                    idCultivo,
                    "Próxima cosecha: " + cultivo.Nombre_Cultivo,
                    "Revisar madurez, rendimiento esperado y recursos para cosechar " + cultivo.Nombre_Cultivo + ".",
                    cultivo.Fecha_Cosecha,
                    "Cosecha",
                    "Alta",
                    7
                );

                // Calcular fechas intermedias entre plantación y cosecha
                int diasTotales = (int)(cultivo.Fecha_Cosecha - cultivo.Fecha_Plantacion).TotalDays;
                if (diasTotales <= 0)
                {
                    throw new Exception("La fecha de cosecha debe ser posterior a la fecha de plantación.");
                }

                // Alerta para fertilización (a 1/4 del ciclo)
                DateTime fechaFertilizacion = cultivo.Fecha_Plantacion.AddDays(diasTotales / 4);
                if (fechaFertilizacion > DateTime.Now && fechaFertilizacion < cultivo.Fecha_Cosecha)
                {
                    alertasCreadas += CrearAlertaAutomatica(
                        idCultivo,
                        "Fertilización programada: " + cultivo.Nombre_Cultivo,
                        "Evaluar crecimiento y aplicar fertilizante si el cultivo lo requiere.",
                        fechaFertilizacion,
                        "Fertilización",
                        "Media",
                        3
                    );
                }

                // Alertas para riego (cada 7 días o según la estación)
                int intervaloDias = CalcularIntervaloRiego(cultivo);

                // Generar alertas de riego
                DateTime fechaRiego = cultivo.Fecha_Plantacion.AddDays(intervaloDias);
                int cicloRiego = 1;

                while (fechaRiego < cultivo.Fecha_Cosecha)
                {
                    if (fechaRiego > DateTime.Now) // Solo crear alertas futuras
                    {
                        alertasCreadas += CrearAlertaAutomatica(
                            idCultivo,
                            "Riego programado: " + cultivo.Nombre_Cultivo + " (Ciclo " + cicloRiego + ")",
                            "Verificar humedad del suelo antes de regar. Ajustar cantidad segun clima, drenaje y estado del cultivo.",
                            fechaRiego,
                            "Riego",
                            "Media",
                            2
                        );
                    }

                    fechaRiego = fechaRiego.AddDays(intervaloDias);
                    cicloRiego++;
                }

                // Alertas para control de plagas (cada 15 días)
                int intervaloPlagas = CalcularIntervaloPlagas(cultivo);
                DateTime fechaControlPlagas = cultivo.Fecha_Plantacion.AddDays(intervaloPlagas);
                int cicloControlPlagas = 1;

                while (fechaControlPlagas < cultivo.Fecha_Cosecha)
                {
                    if (fechaControlPlagas > DateTime.Now) // Solo crear alertas futuras
                    {
                        alertasCreadas += CrearAlertaAutomatica(
                            idCultivo,
                            "Control de plagas: " + cultivo.Nombre_Cultivo + " (Ciclo " + cicloControlPlagas + ")",
                            "Inspeccionar hojas, tallos y frutos. Buscar manchas, insectos, melaza, galerias o marchitez antes de aplicar productos.",
                            fechaControlPlagas,
                            "Control de Plagas",
                            "Media",
                            3
                        );
                    }

                    fechaControlPlagas = fechaControlPlagas.AddDays(intervaloPlagas);
                    cicloControlPlagas++;
                }

                return alertasCreadas;
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(
                    ex,
                    $"BLL_Alertas.GenerarAlertasAutomaticas; Cultivo={idCultivo}");
                throw;
            }
        }

        private static int CrearAlertaAutomatica(int idCultivo, string titulo, string descripcion,
            DateTime fechaProgramada, string tipoAlerta, string prioridad, int diasAnticipacion)
        {
            DAL_Alertas dal = new DAL_Alertas();
            if (dal.Existe_Alerta_Programada(idCultivo, titulo, fechaProgramada, tipoAlerta))
            {
                return 0;
            }

            int idAlerta = CrearAlerta(idCultivo, titulo, descripcion, fechaProgramada, tipoAlerta, prioridad, diasAnticipacion);
            return idAlerta > 0 ? 1 : 0;
        }

        private static int CalcularIntervaloRiego(Entitie_Siembra cultivo)
        {
            int intervaloDias = 7;
            string estacion = (cultivo.Estacion_Siembra ?? string.Empty).Trim().ToLower();

            if (estacion.Contains("verano") || estacion.Contains("seca"))
            {
                intervaloDias = 4;
            }
            else if (estacion.Contains("invierno") || estacion.Contains("lluv"))
            {
                intervaloDias = 10;
            }

            if (cultivo.Humedad_Suelo > 0 && cultivo.Humedad_Suelo < 35)
            {
                intervaloDias = Math.Min(intervaloDias, 4);
            }
            else if (cultivo.Humedad_Suelo >= 70)
            {
                intervaloDias = Math.Max(intervaloDias, 10);
            }

            return intervaloDias;
        }

        private static int CalcularIntervaloPlagas(Entitie_Siembra cultivo)
        {
            string estacion = (cultivo.Estacion_Siembra ?? string.Empty).Trim().ToLower();
            string categoria = (cultivo.Categoria_Cultivo ?? string.Empty).Trim().ToLower();

            if (estacion.Contains("verano") || estacion.Contains("lluv") || categoria.Contains("hortaliza"))
            {
                return 12;
            }

            if (estacion.Contains("invierno"))
            {
                return 21;
            }

            return 15;
        }

        private static void ValidarAlerta(Entitie_Alerta alerta)
        {
            if (alerta == null)
            {
                throw new ArgumentException("La alerta no puede estar vacía.");
            }

            if (alerta.ID_Cultivo <= 0)
            {
                throw new ArgumentException("Debe seleccionar un cultivo válido.");
            }

            if (string.IsNullOrWhiteSpace(alerta.Titulo))
            {
                throw new ArgumentException("Debe ingresar un título para la alerta.");
            }

            if (string.IsNullOrWhiteSpace(alerta.Descripcion))
            {
                throw new ArgumentException("Debe ingresar una descripción para la alerta.");
            }

            if (string.IsNullOrWhiteSpace(alerta.Tipo_Alerta))
            {
                alerta.Tipo_Alerta = "Otro";
            }

            if (string.IsNullOrWhiteSpace(alerta.Estado))
            {
                alerta.Estado = "Pendiente";
            }

            if (string.IsNullOrWhiteSpace(alerta.Prioridad))
            {
                alerta.Prioridad = "Media";
            }

            if (alerta.Dias_Anticipacion <= 0)
            {
                alerta.Dias_Anticipacion = 7;
            }
        }

        private static string ObtenerPermisoActualizacion(string? estado)
        {
            if (string.Equals(estado, "Completada", StringComparison.OrdinalIgnoreCase))
            {
                return PermisosSistema.AlertasCompletar;
            }

            if (string.Equals(estado, "Cancelada", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(estado, "Anulada", StringComparison.OrdinalIgnoreCase))
            {
                return PermisosSistema.AlertasAnular;
            }

            return PermisosSistema.AlertasActualizar;
        }

        private static string ObtenerAccionActualizacion(string? estado)
        {
            if (string.Equals(estado, "Completada", StringComparison.OrdinalIgnoreCase))
            {
                return "Completar";
            }

            if (string.Equals(estado, "Cancelada", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(estado, "Anulada", StringComparison.OrdinalIgnoreCase))
            {
                return "Anular";
            }

            return "Actualizar";
        }

        // Método para verificar si hay alertas próximas y mostrar notificaciones
        public static DataTable VerificarAlertas()
        {
            BLL_Seguridad.ExigirPermiso(
                PermisosSistema.AlertasNotificar,
                "Alerta",
                "Notificar",
                "Verificacion de alertas proximas");
            DataTable alertasProximas = ObtenerAlertasProximas();
            PrepararColumnasResultadoCorreo(alertasProximas);

            foreach (DataRow row in alertasProximas.Rows)
            {
                int idAlerta = Convert.ToInt32(row["ID_Alerta"]);
                bool correoEnviado = EnviarNotificacionGmail(row, out string resultadoCorreo);

                row["Correo_Enviado"] = correoEnviado;
                row["Resultado_Correo"] = resultadoCorreo;

                BLL_Seguridad.Auditar(
                    PermisosSistema.AlertasNotificar,
                    "Alerta",
                    "Enviar correo",
                    $"ID={idAlerta}; Resultado={resultadoCorreo}",
                    correoEnviado ? ResultadosAuditoria.Exito : ResultadosAuditoria.Fallido);

                if (correoEnviado)
                {
                    MarcarAlertaNotificada(idAlerta);
                }
            }

            return alertasProximas;
        }

        private static void PrepararColumnasResultadoCorreo(DataTable alertas)
        {
            if (!alertas.Columns.Contains("Correo_Enviado"))
            {
                alertas.Columns.Add("Correo_Enviado", typeof(bool));
            }

            if (!alertas.Columns.Contains("Resultado_Correo"))
            {
                alertas.Columns.Add("Resultado_Correo", typeof(string));
            }
        }

        private static bool EnviarNotificacionGmail(DataRow alerta, out string resultado)
        {
            string destinatario = LeerTexto(alerta, "Email_Agricultor");
            string agricultor = LeerTexto(alerta, "Agricultor");

            if (!EsCorreoValido(destinatario))
            {
                resultado = "No se envio: la siembra no tiene un Gmail de agricultor asignado.";
                return false;
            }

            try
            {
                ServicioCorreoRecuperacion.EnviarAlerta(
                    destinatario,
                    agricultor,
                    LeerTexto(alerta, "Titulo"),
                    LeerTexto(alerta, "Descripcion"),
                    LeerTexto(alerta, "Nombre_Cultivo"),
                    LeerFecha(alerta, "Fecha_Programada"),
                    LeerTexto(alerta, "Tipo_Alerta"),
                    LeerTexto(alerta, "Prioridad"),
                    LeerEntero(alerta, "Dias_Restantes"));

                resultado = "Correo enviado a " + destinatario + ".";
                return true;
            }
            catch (SmtpException ex)
            {
                resultado = ServicioCorreoRecuperacion.ObtenerMensajeError(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                resultado = "No se pudo enviar la alerta. " +
                    RegistroErrores.ObtenerReferencia(
                        ex,
                        "BLL_Alertas.EnviarNotificacionGmail.Configuracion");
                return false;
            }
            catch (Exception ex)
            {
                resultado = "No se pudo enviar la alerta por Gmail. " +
                    RegistroErrores.ObtenerReferencia(
                        ex,
                        "BLL_Alertas.EnviarNotificacionGmail");
                return false;
            }
        }

        private static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return false;
            }

            try
            {
                MailAddress direccion = new MailAddress(correo);
                return direccion.Address.Equals(correo.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static string LeerTexto(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna) || row[columna] == DBNull.Value)
            {
                return string.Empty;
            }

            return row[columna].ToString() ?? string.Empty;
        }

        private static int LeerEntero(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna) || row[columna] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(row[columna]);
        }

        private static DateTime LeerFecha(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna) || row[columna] == DBNull.Value)
            {
                return DateTime.Now;
            }

            return Convert.ToDateTime(row[columna]);
        }
    }
}

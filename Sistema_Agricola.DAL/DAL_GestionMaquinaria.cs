using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_GestionMaquinaria
    {
        public DataTable ListarMaquinaria(string busqueda, string tipo, string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Listar_Gestion_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Busqueda", SqlDbType.VarChar, 100).Value =
                ValorOpcional(busqueda, "Todos");
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value =
                ValorOpcional(tipo, "Todos");
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value =
                ValorOpcional(estado, "Todos");
            return CargarTabla(cmd);
        }

        public DataTable ListarTrabajadores()
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Listar_Trabajadores_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            return CargarTabla(cmd);
        }

        public DataTable ListarAsignaciones(bool soloActivas)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Listar_Asignaciones_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SoloActivas", SqlDbType.Bit).Value = soloActivas;
            return CargarTabla(cmd);
        }

        public DataTable ListarMantenimientos(
            int? idMaquina,
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Listar_Mantenimientos_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value =
                idMaquina.HasValue ? idMaquina.Value : DBNull.Value;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = fechaDesde.Date;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = fechaHasta.Date;
            return CargarTabla(cmd);
        }

        public int RegistrarMaquinaria(Entitie_Maquinaria maquina)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Registrar_Gestion_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            AgregarParametrosMaquina(cmd, maquina);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool ActualizarMaquinaria(Entitie_Maquinaria maquina)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Actualizar_Gestion_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = maquina.Id_Maquina;
            AgregarParametrosMaquina(cmd, maquina);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CambiarEstado(int idMaquina, string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Cambiar_Estado_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = idMaquina;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = estado;
            return cmd.ExecuteNonQuery() > 0;
        }

        public string EliminarMaquinaria(int idMaquina)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Eliminar_Gestion_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = idMaquina;
            object resultado = cmd.ExecuteScalar();
            return Convert.ToString(resultado) ?? "Operación completada.";
        }

        public int AsignarMaquinaria(Entitie_Asignacion_Maquinaria asignacion)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Asignar_Gestion_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Trabajador", SqlDbType.Int).Value = asignacion.ID_Trabajador;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = asignacion.ID_Maquina;
            cmd.Parameters.Add("@Fecha_Asignacion", SqlDbType.Date).Value =
                asignacion.Fecha_Asignacion.Date;
            cmd.Parameters.Add("@Fecha_Prevista_Devolucion", SqlDbType.Date).Value =
                asignacion.Fecha_Prevista_Devolucion.HasValue
                    ? asignacion.Fecha_Prevista_Devolucion.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Actividad", SqlDbType.VarChar, 120).Value =
                asignacion.Actividad;
            cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 200).Value =
                string.IsNullOrWhiteSpace(asignacion.Observaciones)
                    ? DBNull.Value
                    : asignacion.Observaciones;
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool FinalizarAsignacion(int idAsignacion)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Finalizar_Gestion_Asignacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Asignacion", SqlDbType.Int).Value = idAsignacion;
            return cmd.ExecuteNonQuery() > 0;
        }

        public int RegistrarMantenimiento(Entitie_MantenimientoMaquinaria mantenimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);
            using SqlCommand cmd = new SqlCommand("Registrar_Mantenimiento_Maquinaria", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID_Maquina", SqlDbType.Int).Value = mantenimiento.ID_Maquina;
            cmd.Parameters.Add("@Tipo_Mantenimiento", SqlDbType.VarChar, 30).Value =
                mantenimiento.Tipo_Mantenimiento;
            cmd.Parameters.Add("@Fecha_Mantenimiento", SqlDbType.Date).Value =
                mantenimiento.Fecha_Mantenimiento.Date;
            AgregarDecimal(cmd, "@Costo", mantenimiento.Costo, 14, 2);
            AgregarDecimal(cmd, "@Horometro", mantenimiento.Horometro, 14, 1);
            cmd.Parameters.Add("@Proximo_Mantenimiento", SqlDbType.Date).Value =
                mantenimiento.Proximo_Mantenimiento.HasValue
                    ? mantenimiento.Proximo_Mantenimiento.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Proveedor", SqlDbType.VarChar, 100).Value =
                string.IsNullOrWhiteSpace(mantenimiento.Proveedor)
                    ? DBNull.Value
                    : mantenimiento.Proveedor;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value =
                mantenimiento.Descripcion;
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static void AgregarParametrosMaquina(
            SqlCommand cmd,
            Entitie_Maquinaria maquina)
        {
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 30).Value = maquina.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = maquina.Nombre_Maquina;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = maquina.Tipo_Maquina;
            cmd.Parameters.Add("@Marca", SqlDbType.VarChar, 50).Value =
                string.IsNullOrWhiteSpace(maquina.Marca) ? DBNull.Value : maquina.Marca;
            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 50).Value =
                string.IsNullOrWhiteSpace(maquina.Modelo) ? DBNull.Value : maquina.Modelo;
            cmd.Parameters.Add("@Numero_Serie", SqlDbType.VarChar, 60).Value =
                string.IsNullOrWhiteSpace(maquina.Numero_Serie)
                    ? DBNull.Value
                    : maquina.Numero_Serie;
            AgregarDecimal(cmd, "@Costo", maquina.Costo_Maquina, 14, 2);
            cmd.Parameters.Add("@Fecha_Adquisicion", SqlDbType.Date).Value =
                maquina.Fecha_Adquisicion?.Date ?? DateTime.Today;
            cmd.Parameters.Add("@Mantenimiento_Programado", SqlDbType.VarChar, 30).Value =
                maquina.Mantenimiento_Programado;
            cmd.Parameters.Add("@Proximo_Mantenimiento", SqlDbType.Date).Value =
                maquina.Proximo_Mantenimiento.HasValue
                    ? maquina.Proximo_Mantenimiento.Value.Date
                    : DBNull.Value;
            AgregarDecimal(cmd, "@Horometro", maquina.Horometro, 14, 1);
            cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 80).Value =
                string.IsNullOrWhiteSpace(maquina.Ubicacion)
                    ? DBNull.Value
                    : maquina.Ubicacion;
            cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 250).Value =
                string.IsNullOrWhiteSpace(maquina.Observaciones)
                    ? DBNull.Value
                    : maquina.Observaciones;
        }

        private static DataTable CargarTabla(SqlCommand cmd)
        {
            DataTable tabla = new DataTable();
            using SqlDataReader reader = cmd.ExecuteReader();
            tabla.Load(reader);
            return tabla;
        }

        private static object ValorOpcional(string valor, string comodin)
        {
            return string.IsNullOrWhiteSpace(valor) || valor == comodin
                ? DBNull.Value
                : valor.Trim();
        }

        private static void AgregarDecimal(
            SqlCommand cmd,
            string nombre,
            decimal valor,
            byte precision,
            byte escala)
        {
            SqlParameter parametro = cmd.Parameters.Add(nombre, SqlDbType.Decimal);
            parametro.Precision = precision;
            parametro.Scale = escala;
            parametro.Value = valor;
        }

        private static void VerificarEsquema(SqlConnection conexion)
        {
            using SqlCommand cmd = new SqlCommand(@"
                SELECT CASE
                    WHEN OBJECT_ID('Mantenimiento_Maquinaria', 'U') IS NOT NULL
                     AND OBJECT_ID('Listar_Gestion_Maquinaria', 'P') IS NOT NULL
                     AND OBJECT_ID('Actualizar_Gestion_Maquinaria', 'P') IS NOT NULL
                     AND OBJECT_ID('Eliminar_Gestion_Maquinaria', 'P') IS NOT NULL
                     AND OBJECT_ID('Asignar_Gestion_Maquinaria', 'P') IS NOT NULL
                    THEN 1 ELSE 0
                END;", conexion);

            if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
            {
                throw new InvalidOperationException(
                    "El módulo de maquinaria no está instalado. " +
                    "Ejecute el script oficial Script_Completo_Sistema_Agricola.sql o las migraciones numeradas pendientes.");
            }
        }
    }
}

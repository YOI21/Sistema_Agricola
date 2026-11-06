using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_MantenimientoEquipos
    {
        public DataTable ListarEquipos(string busqueda, string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Equipos_Mantenimiento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Busqueda", SqlDbType.VarChar, 100).Value =
                ValorOpcional(busqueda, "Todos");
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value =
                ValorOpcional(estado, "Todos");

            return CargarTabla(cmd);
        }

        public DataTable ListarMantenimientos(
            int? idEquipo,
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Mantenimientos_Equipos", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Equipo", SqlDbType.Int).Value =
                idEquipo.HasValue ? idEquipo.Value : DBNull.Value;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = fechaDesde.Date;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = fechaHasta.Date;

            return CargarTabla(cmd);
        }

        public int RegistrarEquipo(Entitie_Equipo equipo)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Registrar_Equipo_Mantenimiento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            AgregarParametrosEquipo(cmd, equipo);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void ActualizarEquipo(Entitie_Equipo equipo)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Actualizar_Equipo_Mantenimiento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Equipo", SqlDbType.Int).Value = equipo.Id_Equipo;
            AgregarParametrosEquipo(cmd, equipo);
            cmd.ExecuteNonQuery();
        }

        public void CambiarEstadoEquipo(int idEquipo, string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Cambiar_Estado_Equipo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Equipo", SqlDbType.Int).Value = idEquipo;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = estado;
            cmd.ExecuteNonQuery();
        }

        public string EliminarEquipo(int idEquipo)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Eliminar_Equipo_Mantenimiento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Equipo", SqlDbType.Int).Value = idEquipo;
            object resultado = cmd.ExecuteScalar();
            return Convert.ToString(resultado) ?? "Operacion completada.";
        }

        public int RegistrarMantenimiento(Entitie_MantenimientoEquipo mantenimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Registrar_Mantenimiento_Equipo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            AgregarParametrosMantenimiento(cmd, mantenimiento);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void ActualizarMantenimiento(Entitie_MantenimientoEquipo mantenimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Actualizar_Mantenimiento_Equipo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Mantenimiento", SqlDbType.Int).Value =
                mantenimiento.Id_Mantenimiento;
            AgregarParametrosMantenimiento(cmd, mantenimiento);
            cmd.ExecuteNonQuery();
        }

        public string EliminarMantenimiento(int idMantenimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Eliminar_Mantenimiento_Equipo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Mantenimiento", SqlDbType.Int).Value = idMantenimiento;
            object resultado = cmd.ExecuteScalar();
            return Convert.ToString(resultado) ?? "Mantenimiento eliminado correctamente.";
        }

        private static void AgregarParametrosEquipo(SqlCommand cmd, Entitie_Equipo equipo)
        {
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 30).Value = equipo.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = equipo.Nombre;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = equipo.Tipo;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = equipo.Cantidad;
            AgregarDecimal(cmd, "@Costo", equipo.Costo, 14, 2);
            cmd.Parameters.Add("@Fecha_Adquisicion", SqlDbType.Date).Value =
                equipo.Fecha_Adquisicion.HasValue
                    ? equipo.Fecha_Adquisicion.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Proximo_Mantenimiento", SqlDbType.Date).Value =
                equipo.Proximo_Mantenimiento.HasValue
                    ? equipo.Proximo_Mantenimiento.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 80).Value =
                string.IsNullOrWhiteSpace(equipo.Ubicacion)
                    ? DBNull.Value
                    : equipo.Ubicacion;
            cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 250).Value =
                string.IsNullOrWhiteSpace(equipo.Observaciones)
                    ? DBNull.Value
                    : equipo.Observaciones;
        }

        private static void AgregarParametrosMantenimiento(
            SqlCommand cmd,
            Entitie_MantenimientoEquipo mantenimiento)
        {
            cmd.Parameters.Add("@Id_Equipo", SqlDbType.Int).Value = mantenimiento.Id_Equipo;
            cmd.Parameters.Add("@Tipo_Mantenimiento", SqlDbType.VarChar, 30).Value =
                mantenimiento.Tipo_Mantenimiento;
            cmd.Parameters.Add("@Fecha_Mantenimiento", SqlDbType.Date).Value =
                mantenimiento.Fecha_Mantenimiento.Date;
            cmd.Parameters.Add("@Cantidad_Atendida", SqlDbType.Int).Value =
                mantenimiento.Cantidad_Atendida;
            AgregarDecimal(cmd, "@Costo", mantenimiento.Costo, 14, 2);
            cmd.Parameters.Add("@Proximo_Mantenimiento", SqlDbType.Date).Value =
                mantenimiento.Proximo_Mantenimiento.HasValue
                    ? mantenimiento.Proximo_Mantenimiento.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Responsable", SqlDbType.VarChar, 100).Value =
                string.IsNullOrWhiteSpace(mantenimiento.Responsable)
                    ? DBNull.Value
                    : mantenimiento.Responsable;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value =
                mantenimiento.Descripcion;
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
                    WHEN OBJECT_ID('dbo.Equipos', 'U') IS NOT NULL
                     AND OBJECT_ID('dbo.Mantenimiento_Equipos', 'U') IS NOT NULL
                     AND OBJECT_ID('dbo.Listar_Equipos_Mantenimiento', 'P') IS NOT NULL
                     AND OBJECT_ID('dbo.Registrar_Mantenimiento_Equipo', 'P') IS NOT NULL
                     AND OBJECT_ID('dbo.Actualizar_Mantenimiento_Equipo', 'P') IS NOT NULL
                     AND OBJECT_ID('dbo.Eliminar_Mantenimiento_Equipo', 'P') IS NOT NULL
                    THEN 1 ELSE 0
                END;", conexion);

            if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
            {
                throw new InvalidOperationException(
                    "El modulo de mantenimiento de equipos no esta instalado. " +
                    "Ejecute el script oficial Script_Completo_Sistema_Agricola.sql o las migraciones numeradas pendientes.");
            }
        }
    }
}

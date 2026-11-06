using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Finanzas
    {
        public DataTable ListarMovimientos(DateTime fechaDesde, DateTime fechaHasta, string busqueda)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Movimientos_Financieros", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = fechaDesde.Date;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = fechaHasta.Date;
            cmd.Parameters.Add("@Busqueda", SqlDbType.VarChar, 100).Value =
                string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda.Trim();

            using SqlDataReader reader = cmd.ExecuteReader();
            DataTable tabla = new DataTable();
            tabla.Load(reader);
            return tabla;
        }

        public int InsertarMovimiento(Entitie_MovimientoFinanciero movimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand(
                "Insertar_Movimiento_Financiero_Seguro",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            AgregarParametros(cmd, movimiento);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool ActualizarMovimiento(Entitie_MovimientoFinanciero movimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand(
                "Actualizar_Movimiento_Financiero_Seguro",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            AgregarParametros(cmd, movimiento);
            cmd.Parameters.Add("@ID_Movimiento", SqlDbType.Int).Value = movimiento.ID_Movimiento;
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool EliminarMovimiento(int idMovimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand(
                "Eliminar_Movimiento_Financiero_Seguro",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@ID_Movimiento", SqlDbType.Int).Value = idMovimiento;
            return cmd.ExecuteNonQuery() > 0;
        }

        private static void AgregarParametros(
            SqlCommand cmd,
            Entitie_MovimientoFinanciero movimiento)
        {
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 10).Value = movimiento.Tipo;
            cmd.Parameters.Add("@Categoria", SqlDbType.VarChar, 50).Value = movimiento.Categoria;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = movimiento.Descripcion;

            SqlParameter monto = cmd.Parameters.Add("@Monto", SqlDbType.Decimal);
            monto.Precision = 14;
            monto.Scale = 2;
            monto.Value = movimiento.Monto;

            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = movimiento.Fecha.Date;
            cmd.Parameters.Add("@Metodo_Pago", SqlDbType.VarChar, 30).Value = movimiento.Metodo_Pago;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 15).Value = movimiento.Estado;
            cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 80).Value =
                string.IsNullOrWhiteSpace(movimiento.Referencia)
                    ? DBNull.Value
                    : movimiento.Referencia;
        }

        private static void VerificarEsquema(SqlConnection conexion)
        {
            using SqlCommand cmd = new SqlCommand(@"
                SELECT CASE
                    WHEN OBJECT_ID('Movimiento_Financiero', 'U') IS NOT NULL
                     AND OBJECT_ID('Listar_Movimientos_Financieros', 'P') IS NOT NULL
                     AND OBJECT_ID('Insertar_Movimiento_Financiero_Seguro', 'P') IS NOT NULL
                    THEN 1 ELSE 0
                END;", conexion);

            if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
            {
                throw new InvalidOperationException(
                    "El módulo financiero no está instalado en la base de datos. " +
                    "Ejecute el script oficial Script_Completo_Sistema_Agricola.sql o las migraciones numeradas pendientes.");
            }
        }
    }
}

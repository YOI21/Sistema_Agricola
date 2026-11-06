using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Produccion
    {
        public DataTable ListarCosechasDisponibles(int? idProduccion = null)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand(
                "Listar_Cosechas_Para_Produccion",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@ID_Produccion", SqlDbType.Int).Value =
                idProduccion.HasValue && idProduccion.Value > 0
                    ? idProduccion.Value
                    : DBNull.Value;

            using SqlDataReader reader = cmd.ExecuteReader();
            DataTable tabla = new DataTable();
            tabla.Load(reader);
            return tabla;
        }

        public DataTable Listar(
            DateTime fechaDesde,
            DateTime fechaHasta,
            string busqueda,
            string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Produccion", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = fechaDesde.Date;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = fechaHasta.Date;
            cmd.Parameters.Add("@Busqueda", SqlDbType.VarChar, 100).Value =
                TextoONulo(busqueda);
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value =
                TextoONulo(estado);

            using SqlDataReader reader = cmd.ExecuteReader();
            DataTable tabla = new DataTable();
            tabla.Load(reader);
            return tabla;
        }

        public int Registrar(Entitie_Produccion produccion)
        {
            try
            {
                using SqlConnection conexion = Conexion.conectardb();
                VerificarEsquema(conexion);

                using SqlCommand cmd = new SqlCommand("Registrar_Produccion", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                AgregarParametros(cmd, produccion);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex) when (EsErrorNegocio(ex.Number))
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public bool Actualizar(Entitie_Produccion produccion)
        {
            try
            {
                using SqlConnection conexion = Conexion.conectardb();
                VerificarEsquema(conexion);

                using SqlCommand cmd = new SqlCommand("Actualizar_Produccion", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Produccion", SqlDbType.Int).Value =
                    produccion.ID_Produccion;
                AgregarParametros(cmd, produccion);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex) when (EsErrorNegocio(ex.Number))
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        public bool Eliminar(int idProduccion)
        {
            try
            {
                using SqlConnection conexion = Conexion.conectardb();
                VerificarEsquema(conexion);

                using SqlCommand cmd = new SqlCommand("Eliminar_Produccion", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@ID_Produccion", SqlDbType.Int).Value = idProduccion;
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex) when (EsErrorNegocio(ex.Number))
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        private static void AgregarParametros(
            SqlCommand cmd,
            Entitie_Produccion produccion)
        {
            cmd.Parameters.Add("@ID_Cosecha", SqlDbType.Int).Value =
                produccion.ID_Cosecha;
            cmd.Parameters.Add("@Fecha_Produccion", SqlDbType.Date).Value =
                produccion.Fecha_Produccion.Date;
            cmd.Parameters.Add("@Unidad_Medida", SqlDbType.VarChar, 20).Value =
                produccion.Unidad_Medida;
            AgregarDecimal(cmd, "@Cantidad_Bruta", produccion.Cantidad_Bruta, 14, 3);
            AgregarDecimal(cmd, "@Merma", produccion.Merma, 14, 3);
            AgregarDecimal(cmd, "@Precio_Unitario", produccion.Precio_Unitario, 14, 2);
            AgregarDecimal(cmd, "@Costo_Postcosecha", produccion.Costo_Postcosecha, 14, 2);
            cmd.Parameters.Add("@Destino", SqlDbType.VarChar, 80).Value =
                TextoONulo(produccion.Destino);
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value =
                produccion.Estado;
            cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 250).Value =
                TextoONulo(produccion.Observaciones);
            cmd.Parameters.Add("@Fecha_Venta", SqlDbType.Date).Value =
                produccion.Fecha_Venta.HasValue
                    ? produccion.Fecha_Venta.Value.Date
                    : DBNull.Value;
            cmd.Parameters.Add("@Comprador", SqlDbType.VarChar, 120).Value =
                TextoONulo(produccion.Comprador);
            cmd.Parameters.Add("@Metodo_Pago", SqlDbType.VarChar, 30).Value =
                TextoONulo(produccion.Metodo_Pago);
            cmd.Parameters.Add("@Estado_Cobro", SqlDbType.VarChar, 15).Value =
                produccion.Estado_Cobro;
            cmd.Parameters.Add("@Referencia_Venta", SqlDbType.VarChar, 80).Value =
                TextoONulo(produccion.Referencia_Venta);
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

        private static object TextoONulo(string? texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                ? DBNull.Value
                : texto.Trim();
        }

        private static bool EsErrorNegocio(int numero)
        {
            return numero is >= 51400 and <= 51499 or >= 52000 and <= 52099;
        }

        private static void VerificarEsquema(SqlConnection conexion)
        {
            using SqlCommand cmd = new SqlCommand(@"
                SELECT CASE
                    WHEN OBJECT_ID(N'dbo.Produccion', N'U') IS NOT NULL
                     AND COL_LENGTH(N'dbo.Produccion', N'Costo_Base_Asignado') IS NOT NULL
                     AND COL_LENGTH(N'dbo.Produccion', N'Estado_Cobro') IS NOT NULL
                     AND COL_LENGTH(N'dbo.Movimiento_Financiero', N'Origen') IS NOT NULL
                     AND OBJECT_ID(N'dbo.Registrar_Produccion', N'P') IS NOT NULL
                     AND OBJECT_ID(N'dbo.Actualizar_Produccion', N'P') IS NOT NULL
                     AND OBJECT_ID(N'dbo.Listar_Produccion', N'P') IS NOT NULL
                    THEN 1 ELSE 0
                END;", conexion);

            if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
            {
                throw new InvalidOperationException(
                    "El modulo de produccion no esta actualizado. Ejecute la migracion 011.");
            }
        }
    }
}

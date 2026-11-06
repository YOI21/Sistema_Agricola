using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_Inventario
    {
        public DataTable ListarProductos(string busqueda, string tipo, bool soloBajoStock)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Inventario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Busqueda", SqlDbType.VarChar, 100).Value =
                string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda.Trim();
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value =
                string.IsNullOrWhiteSpace(tipo) || tipo == "Todos" ? DBNull.Value : tipo;
            cmd.Parameters.Add("@SoloBajoStock", SqlDbType.Bit).Value = soloBajoStock;

            DataTable tabla = new DataTable();
            using SqlDataReader reader = cmd.ExecuteReader();
            tabla.Load(reader);
            return tabla;
        }

        public DataTable ListarMovimientos(int? idInsumo, DateTime desde, DateTime hasta)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Listar_Movimientos_Inventario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value =
                idInsumo.HasValue ? idInsumo.Value : DBNull.Value;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = desde.Date;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = hasta.Date;

            DataTable tabla = new DataTable();
            using SqlDataReader reader = cmd.ExecuteReader();
            tabla.Load(reader);
            return tabla;
        }

        public int RegistrarProducto(Entitie_Insumo insumo, decimal stockInicial)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Registrar_Producto_Inventario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            AgregarParametrosProducto(cmd, insumo);
            AgregarDecimal(cmd, "@StockInicial", stockInicial, 14, 3);

            object resultado = cmd.ExecuteScalar();
            return Convert.ToInt32(resultado);
        }

        public bool ActualizarProducto(Entitie_Insumo insumo)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Actualizar_Producto_Inventario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = insumo.Id_Insumo;
            AgregarParametrosProducto(cmd, insumo);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CambiarEstadoProducto(int idInsumo, string estado)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand(
                "Cambiar_Estado_Producto_Inventario",
                conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 15).Value = estado;
            cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = idInsumo;
            return cmd.ExecuteNonQuery() > 0;
        }

        public int RegistrarMovimiento(Entitie_MovimientoInventario movimiento)
        {
            using SqlConnection conexion = Conexion.conectardb();
            VerificarEsquema(conexion);

            using SqlCommand cmd = new SqlCommand("Registrar_Movimiento_Inventario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Insumo", SqlDbType.Int).Value = movimiento.Id_Insumo;
            cmd.Parameters.Add("@Tipo_Movimiento", SqlDbType.VarChar, 10).Value =
                movimiento.Tipo_Movimiento;
            AgregarDecimal(cmd, "@Cantidad", movimiento.Cantidad, 14, 3);
            AgregarDecimal(cmd, "@Costo_Unitario", movimiento.Costo_Unitario, 14, 2);
            cmd.Parameters.Add("@Fecha_Movimiento", SqlDbType.Date).Value =
                movimiento.Fecha_Movimiento.Date;
            cmd.Parameters.Add("@Motivo", SqlDbType.VarChar, 150).Value = movimiento.Motivo;
            cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 80).Value =
                string.IsNullOrWhiteSpace(movimiento.Referencia)
                    ? DBNull.Value
                    : movimiento.Referencia;

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static void AgregarParametrosProducto(SqlCommand cmd, Entitie_Insumo insumo)
        {
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 30).Value = insumo.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = insumo.Nombre;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = insumo.Tipo;
            cmd.Parameters.Add("@Unidad_Medida", SqlDbType.VarChar, 20).Value =
                insumo.Unidad_Medida;
            AgregarDecimal(cmd, "@Costo", insumo.Costo, 14, 2);
            AgregarDecimal(cmd, "@Stock_Minimo", insumo.Stock_Minimo, 14, 3);
            cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 80).Value =
                string.IsNullOrWhiteSpace(insumo.Ubicacion)
                    ? DBNull.Value
                    : insumo.Ubicacion;
            cmd.Parameters.Add("@Fecha_Vencimiento", SqlDbType.Date).Value =
                insumo.Fecha_Vencimiento.HasValue
                    ? insumo.Fecha_Vencimiento.Value.Date
                    : DBNull.Value;
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
                    WHEN OBJECT_ID('Movimiento_Inventario', 'U') IS NOT NULL
                     AND OBJECT_ID('Listar_Inventario', 'P') IS NOT NULL
                     AND OBJECT_ID('Registrar_Movimiento_Inventario', 'P') IS NOT NULL
                     AND OBJECT_ID('Cambiar_Estado_Producto_Inventario', 'P') IS NOT NULL
                    THEN 1 ELSE 0
                END;", conexion);

            if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
            {
                throw new InvalidOperationException(
                    "El módulo de inventario no está instalado en la base de datos. " +
                    "Ejecute el script oficial Script_Completo_Sistema_Agricola.sql o las migraciones numeradas pendientes.");
            }
        }
    }
}

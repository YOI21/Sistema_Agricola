using System.Data;
using Microsoft.Data.SqlClient;
using Sistema_Agricola.DAL;

namespace Sistema_Agricola.Tests;

public sealed class BaseDatosIntegracionTests
{
    [BaseDatosFact]
    [Trait("Categoria", "Integracion")]
    public void ConexionAplicacion_UsaRolRestringido()
    {
        using SqlConnection conexion = Conexion.conectardb();
        using SqlCommand comando = new SqlCommand(@"
SELECT
    ISNULL(IS_SRVROLEMEMBER(N'sysadmin'), 0),
    ISNULL(IS_MEMBER(N'db_owner'), 0),
    ISNULL(IS_MEMBER(N'db_ddladmin'), 0),
    ISNULL(IS_MEMBER(N'db_datareader'), 0),
    ISNULL(IS_MEMBER(N'db_datawriter'), 0),
    ISNULL(IS_ROLEMEMBER(N'db_sistema_agricola_app'), 0);", conexion);
        using SqlDataReader lector = comando.ExecuteReader();

        Assert.True(lector.Read());
        Assert.Equal(0, lector.GetInt32(0));
        Assert.Equal(0, lector.GetInt32(1));
        Assert.Equal(0, lector.GetInt32(2));
        Assert.Equal(0, lector.GetInt32(3));
        Assert.Equal(0, lector.GetInt32(4));
        Assert.Equal(1, lector.GetInt32(5));
    }

    [BaseDatosFact]
    [Trait("Categoria", "Integracion")]
    public void ConexionAplicacion_NoLeeTablasSensiblesNiAlteraEsquema()
    {
        using SqlConnection conexion = Conexion.conectardb();
        using SqlCommand comando = new SqlCommand(@"
SELECT
    ISNULL(HAS_PERMS_BY_NAME(DB_NAME(), 'DATABASE', 'ALTER'), 0),
    ISNULL(HAS_PERMS_BY_NAME(DB_NAME(), 'DATABASE', 'CONTROL'), 0),
    ISNULL(HAS_PERMS_BY_NAME(N'dbo.Usuario', 'OBJECT', 'SELECT'), 0),
    ISNULL(HAS_PERMS_BY_NAME(N'dbo.Insumos', 'OBJECT', 'INSERT'), 0),
    ISNULL(HAS_PERMS_BY_NAME(N'dbo.Insumos', 'OBJECT', 'UPDATE'), 0),
    ISNULL(HAS_PERMS_BY_NAME(N'dbo.Insumos', 'OBJECT', 'DELETE'), 0);", conexion);
        using SqlDataReader lector = comando.ExecuteReader();

        Assert.True(lector.Read());
        for (int indice = 0; indice < lector.FieldCount; indice++)
        {
            Assert.Equal(0, lector.GetInt32(indice));
        }
    }

    [BaseDatosFact]
    [Trait("Categoria", "Integracion")]
    public void ProductoDuplicado_RevierteTodaLaTransaccion()
    {
        string sufijo = Guid.NewGuid().ToString("N")[..12];
        string codigo = "PRUEBA-" + sufijo;
        string nombre = "Producto prueba " + sufijo;

        using SqlConnection conexion = Conexion.conectardb();
        using SqlTransaction transaccion = conexion.BeginTransaction();

        try
        {
            int id = RegistrarProducto(conexion, transaccion, codigo, nombre);
            Assert.True(id > 0);

            SqlException error = Assert.Throws<SqlException>(
                () => RegistrarProducto(conexion, transaccion, codigo, nombre));
            Assert.Equal(51100, error.Number);
        }
        finally
        {
            try
            {
                transaccion.Rollback();
            }
            catch (InvalidOperationException)
            {
                // El procedimiento ya revirtio la transaccion exterior.
            }
        }

        DataTable productos = new DAL_Inventario().ListarProductos(
            codigo,
            "Todos",
            false);
        Assert.Empty(productos.Rows.Cast<DataRow>());
    }

    private static int RegistrarProducto(
        SqlConnection conexion,
        SqlTransaction transaccion,
        string codigo,
        string nombre)
    {
        using SqlCommand comando = new SqlCommand(
            "Registrar_Producto_Inventario",
            conexion,
            transaccion)
        {
            CommandType = CommandType.StoredProcedure
        };

        comando.Parameters.Add("@Codigo", SqlDbType.VarChar, 30).Value = codigo;
        comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre;
        comando.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = "Prueba";
        comando.Parameters.Add("@Unidad_Medida", SqlDbType.VarChar, 20).Value = "Unidad";
        AgregarDecimal(comando, "@Costo", 25m, 14, 2);
        AgregarDecimal(comando, "@Stock_Minimo", 1m, 14, 3);
        comando.Parameters.Add("@Ubicacion", SqlDbType.VarChar, 80).Value = "Pruebas";
        comando.Parameters.Add("@Fecha_Vencimiento", SqlDbType.Date).Value = DBNull.Value;
        AgregarDecimal(comando, "@StockInicial", 2m, 14, 3);

        return Convert.ToInt32(comando.ExecuteScalar());
    }

    private static void AgregarDecimal(
        SqlCommand comando,
        string nombre,
        decimal valor,
        byte precision,
        byte escala)
    {
        SqlParameter parametro = comando.Parameters.Add(nombre, SqlDbType.Decimal);
        parametro.Precision = precision;
        parametro.Scale = escala;
        parametro.Value = valor;
    }
}

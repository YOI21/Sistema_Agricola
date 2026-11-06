using System.Text.RegularExpressions;

namespace Sistema_Agricola.Tests;

public sealed class ContratosSqlTests
{
    public static IEnumerable<object[]> ProcedimientosAtomicos()
    {
        string[] migracion009 =
        {
            "Registrar_Siembra_Atomica",
            "Actualizar_Siembra_Atomica",
            "Finalizar_Siembra_Atomica",
            "Cambiar_Estado_Cultivo",
            "Eliminar_Siembra",
            "Guardar_Cosecha_Segura",
            "Eliminar_Cosecha_Segura",
            "Registrar_Producto_Inventario",
            "Registrar_Movimiento_Inventario"
        };

        string[] migracion011 =
        {
            "Registrar_Produccion",
            "Actualizar_Produccion",
            "Eliminar_Produccion"
        };

        foreach (string procedimiento in migracion009)
        {
            yield return new object[]
            {
                "009_Transacciones_Operativas_Atomicas.sql",
                procedimiento
            };
        }

        foreach (string procedimiento in migracion011)
        {
            yield return new object[]
            {
                "011_Produccion_Integracion_Financiera.sql",
                procedimiento
            };
        }
    }

    [Theory]
    [MemberData(nameof(ProcedimientosAtomicos))]
    public void EscriturasCriticas_DeclaranTransaccionYReversion(
        string archivo,
        string procedimiento)
    {
        string cuerpo = ObtenerProcedimiento(archivo, procedimiento);

        Assert.Contains("SET XACT_ABORT ON", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("BEGIN TRANSACTION", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("COMMIT TRANSACTION", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ROLLBACK TRANSACTION", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("BEGIN CATCH", cuerpo, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("THROW", cuerpo, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("UX_Usuario_Usuario")]
    [InlineData("UX_Agricultor_Cedula")]
    [InlineData("UX_Administrador_Email")]
    [InlineData("UX_Trabajador_Email")]
    [InlineData("UX_Insumos_Codigo")]
    [InlineData("UX_RMaquinaria_Codigo")]
    [InlineData("UX_Cosecha_Cultivo")]
    [InlineData("UX_Agricultor_Siembra")]
    [InlineData("UX_MovimientoFinanciero_Origen")]
    public void Instalador_DeclaraIndicesUnicosContraDuplicados(string indice)
    {
        string instalador = LeerSql("Script_Completo_Sistema_Agricola.sql");

        Assert.Contains(indice, instalador, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("003_Acceso_Usuarios.sql", "Rol IN ('Administrador', 'Agricultor')")]
    [InlineData("007_Resultados_Autorizacion_Modulos_Agricolas.sql", "CK_Auditoria_Seguridad_Resultado")]
    [InlineData("008_Endurecer_Autenticacion_Y_Permisos.sql", "CK_Usuario_Iteraciones_PBKDF2")]
    [InlineData("008_Endurecer_Autenticacion_Y_Permisos.sql", "Contrasena_Iteraciones >= 150000")]
    [InlineData("011_Produccion_Integracion_Financiera.sql", "CK_Produccion_EstadoCobro")]
    public void Migraciones_DeclaranRestriccionesDeDominio(
        string archivo,
        string restriccion)
    {
        Assert.Contains(
            restriccion,
            LeerSql(archivo),
            StringComparison.OrdinalIgnoreCase);
    }

    private static string ObtenerProcedimiento(string archivo, string procedimiento)
    {
        string sql = LeerSql(archivo);
        string patron =
            $@"CREATE\s+OR\s+ALTER\s+PROCEDURE\s+dbo\.{Regex.Escape(procedimiento)}\b" +
            @"(?<cuerpo>.*?)(?=\r?\nGO\s*(?:\r?\n|$))";
        Match coincidencia = Regex.Match(
            sql,
            patron,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        Assert.True(
            coincidencia.Success,
            $"No se encontró dbo.{procedimiento} en {archivo}.");
        return coincidencia.Groups["cuerpo"].Value;
    }

    private static string LeerSql(string archivo)
    {
        string ruta = Path.Combine(AppContext.BaseDirectory, "Sql", archivo);
        Assert.True(File.Exists(ruta), "No se copió el contrato SQL: " + ruta);
        return File.ReadAllText(ruta);
    }
}

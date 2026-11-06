using System.Data.Common;

namespace Sistema_Agricola.DAL
{
    public sealed class EstadoEsquemaDatos
    {
        public bool ExisteTablaVersion { get; init; }
        public int VersionActual { get; init; }
        public IReadOnlyList<string> ObjetosFaltantes { get; init; } = Array.Empty<string>();
    }

    public sealed class DAL_Esquema
    {
        public EstadoEsquemaDatos ObtenerEstado()
        {
            using DbConnection conexion = Conexion.conectardb();

            bool existeTablaVersion;
            using (DbCommand cmd = conexion.CreateCommand())
            {
                cmd.CommandText =
                    "SELECT CASE WHEN OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL THEN 0 ELSE 1 END;";
                existeTablaVersion = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }

            int versionActual = -1;
            if (existeTablaVersion)
            {
                using DbCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version;";
                versionActual = Convert.ToInt32(cmd.ExecuteScalar());
            }

            List<string> objetosFaltantes = new List<string>();
            const string sqlObjetos = @"
SELECT Nombre
FROM
(
    SELECT N'dbo.Actualizar_Terreno' AS Nombre,
           OBJECT_ID(N'dbo.Actualizar_Terreno', N'P') AS IdObjeto
    UNION ALL
    SELECT N'dbo.Eliminar_Terreno', OBJECT_ID(N'dbo.Eliminar_Terreno', N'P')
    UNION ALL
    SELECT N'dbo.Alertas', OBJECT_ID(N'dbo.Alertas', N'U')
    UNION ALL
    SELECT N'dbo.Agricultor_Siembra', OBJECT_ID(N'dbo.Agricultor_Siembra', N'U')
    UNION ALL
    SELECT N'dbo.Enfermedades', OBJECT_ID(N'dbo.Enfermedades', N'U')
    UNION ALL
    SELECT N'dbo.Siembra.ID_Terreno', COL_LENGTH(N'dbo.Siembra', N'ID_Terreno')
    UNION ALL
    SELECT N'dbo.Siembra.Hectareas_Sembradas', COL_LENGTH(N'dbo.Siembra', N'Hectareas_Sembradas')
    UNION ALL
    SELECT N'dbo.Siembra.Costo_Siembra', COL_LENGTH(N'dbo.Siembra', N'Costo_Siembra')
    UNION ALL
    SELECT N'dbo.Cosecha.Cantidad_Cosechada', COL_LENGTH(N'dbo.Cosecha', N'Cantidad_Cosechada')
    UNION ALL
    SELECT N'dbo.Cosecha.Rendimiento', COL_LENGTH(N'dbo.Cosecha', N'Rendimiento')
    UNION ALL
    SELECT N'dbo.Cosecha.Calidad', COL_LENGTH(N'dbo.Cosecha', N'Calidad')
    UNION ALL
    SELECT N'dbo.Cosecha.Fecha_Cosecha', COL_LENGTH(N'dbo.Cosecha', N'Fecha_Cosecha')
    UNION ALL
    SELECT N'dbo.Cosecha.Costo_Cosecha', COL_LENGTH(N'dbo.Cosecha', N'Costo_Cosecha')
    UNION ALL
    SELECT N'dbo.Cosecha.Precio_Venta', COL_LENGTH(N'dbo.Cosecha', N'Precio_Venta')
    UNION ALL
    SELECT N'dbo.Cosecha.Beneficio', COL_LENGTH(N'dbo.Cosecha', N'Beneficio')
    UNION ALL
    SELECT N'dbo.Obtener_Cuenta_Autenticacion', OBJECT_ID(N'dbo.Obtener_Cuenta_Autenticacion', N'P')
    UNION ALL
    SELECT N'dbo.Actualizar_Contrasena_Segura', OBJECT_ID(N'dbo.Actualizar_Contrasena_Segura', N'P')
    UNION ALL
    SELECT N'dbo.Registrar_Siembra_Atomica', OBJECT_ID(N'dbo.Registrar_Siembra_Atomica', N'P')
    UNION ALL
    SELECT N'dbo.Actualizar_Siembra_Atomica', OBJECT_ID(N'dbo.Actualizar_Siembra_Atomica', N'P')
    UNION ALL
    SELECT N'dbo.Finalizar_Siembra_Atomica', OBJECT_ID(N'dbo.Finalizar_Siembra_Atomica', N'P')
    UNION ALL
    SELECT N'dbo.Terrenos.Provincia', COL_LENGTH(N'dbo.Terrenos', N'Provincia')
    UNION ALL
    SELECT N'dbo.Terrenos.Zona_Agroclimatica', COL_LENGTH(N'dbo.Terrenos', N'Zona_Agroclimatica')
    UNION ALL
    SELECT N'dbo.Terrenos.Precipitacion_Anual', COL_LENGTH(N'dbo.Terrenos', N'Precipitacion_Anual')
    UNION ALL
    SELECT N'dbo.Terrenos.Disponibilidad_Riego', COL_LENGTH(N'dbo.Terrenos', N'Disponibilidad_Riego')
    UNION ALL
    SELECT N'dbo.Siembra.Variedad_Cultivo', COL_LENGTH(N'dbo.Siembra', N'Variedad_Cultivo')
    UNION ALL
    SELECT N'dbo.Produccion.Costo_Base_Asignado', COL_LENGTH(N'dbo.Produccion', N'Costo_Base_Asignado')
    UNION ALL
    SELECT N'dbo.Produccion.Estado_Cobro', COL_LENGTH(N'dbo.Produccion', N'Estado_Cobro')
    UNION ALL
    SELECT N'dbo.Movimiento_Financiero.Origen', COL_LENGTH(N'dbo.Movimiento_Financiero', N'Origen')
    UNION ALL
    SELECT N'dbo.Movimiento_Financiero.ID_Origen', COL_LENGTH(N'dbo.Movimiento_Financiero', N'ID_Origen')
    UNION ALL
    SELECT N'dbo.Registrar_Produccion', OBJECT_ID(N'dbo.Registrar_Produccion', N'P')
    UNION ALL
    SELECT N'dbo.Actualizar_Produccion', OBJECT_ID(N'dbo.Actualizar_Produccion', N'P')
    UNION ALL
    SELECT N'dbo.Registrar_Auditoria_Seguridad', OBJECT_ID(N'dbo.Registrar_Auditoria_Seguridad', N'P')
) AS Objetos
WHERE IdObjeto IS NULL;";

            using (DbCommand cmd = conexion.CreateCommand())
            {
                cmd.CommandText = sqlObjetos;
                using DbDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    objetosFaltantes.Add(lector.GetString(0));
                }
            }

            return new EstadoEsquemaDatos
            {
                ExisteTablaVersion = existeTablaVersion,
                VersionActual = versionActual,
                ObjetosFaltantes = objetosFaltantes
            };
        }
    }
}

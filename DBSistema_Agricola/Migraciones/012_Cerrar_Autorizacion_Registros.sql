USE DBSistema_Agricola;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) < 11
    THROW 52100, 'Ejecute primero las migraciones 000 a 011.', 1;

IF OBJECT_ID(N'dbo.Auditoria_Seguridad', N'U') IS NULL OR
   COL_LENGTH(N'dbo.Auditoria_Seguridad', N'Resultado') IS NULL
    THROW 52101, 'La tabla de auditoria no tiene la estructura requerida.', 1;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Auditoria_Seguridad
    @ID_Usuario INT = NULL,
    @Usuario VARCHAR(80),
    @Rol VARCHAR(30),
    @Permiso VARCHAR(80),
    @Entidad VARCHAR(80),
    @Accion VARCHAR(40),
    @Detalle VARCHAR(1000) = NULL,
    @Resultado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @Usuario = LEFT(LTRIM(RTRIM(ISNULL(@Usuario, 'Sin sesion'))), 80);
    SET @Rol = LEFT(LTRIM(RTRIM(ISNULL(@Rol, 'Sin rol'))), 30);
    SET @Permiso = LEFT(LTRIM(RTRIM(ISNULL(@Permiso, 'sin_permiso'))), 80);
    SET @Entidad = LEFT(LTRIM(RTRIM(ISNULL(@Entidad, 'Sistema'))), 80);
    SET @Accion = LEFT(LTRIM(RTRIM(ISNULL(@Accion, 'Registrar'))), 40);
    SET @Detalle = LEFT(LTRIM(RTRIM(ISNULL(@Detalle, ''))), 1000);
    SET @Resultado = LEFT(LTRIM(RTRIM(ISNULL(@Resultado, 'Error'))), 20);

    IF @Resultado NOT IN ('Exito', 'Fallido', 'Denegado', 'Error')
        THROW 52102, 'El resultado de auditoria no es valido.', 1;

    INSERT dbo.Auditoria_Seguridad
    (
        ID_Usuario,
        Usuario,
        Rol,
        Permiso,
        Entidad,
        Accion,
        Detalle,
        Resultado,
        Nombre_Equipo,
        Usuario_Windows,
        Fecha_Registro
    )
    VALUES
    (
        @ID_Usuario,
        @Usuario,
        @Rol,
        @Permiso,
        @Entidad,
        @Accion,
        @Detalle,
        @Resultado,
        HOST_NAME(),
        SUSER_SNAME(),
        GETDATE()
    );
END;
GO

IF DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app') IS NOT NULL
BEGIN
    REVOKE INSERT ON OBJECT::dbo.Auditoria_Seguridad
        FROM db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Registrar_Auditoria_Seguridad
        TO db_sistema_agricola_app;
END;
GO

MERGE dbo.Esquema_Version AS destino
USING
(
    SELECT 12 AS Version,
           'Autorizacion cerrada para registros y auditoria por procedimiento' AS Nombre
) AS origen
ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

IF OBJECT_ID(N'dbo.Registrar_Auditoria_Seguridad', N'P') IS NULL
    THROW 52103, 'No se creo el procedimiento de auditoria segura.', 1;

IF EXISTS
(
    SELECT 1
    FROM sys.database_permissions
    WHERE grantee_principal_id =
          DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app')
      AND class = 1
      AND major_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
      AND permission_name = N'INSERT'
      AND state IN (N'G', N'W')
)
    THROW 52104, 'El rol de aplicacion conserva INSERT directo sobre la auditoria.', 1;

IF (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) <> 12
    THROW 52105, 'La version 12 no quedo registrada.', 1;
GO

PRINT 'Migracion 012 aplicada: registros y auditoria cerrados.';
GO

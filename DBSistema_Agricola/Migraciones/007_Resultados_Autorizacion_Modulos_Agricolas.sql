USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL
    THROW 51970, 'Falta dbo.Esquema_Version. Ejecute primero las migraciones 000 a 006.', 1;

IF OBJECT_ID(N'dbo.Auditoria_Seguridad', N'U') IS NULL
    THROW 51971, 'Falta dbo.Auditoria_Seguridad. Ejecute primero la migracion 004.', 1;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF COL_LENGTH(N'dbo.Auditoria_Seguridad', N'Resultado') IS NULL
    BEGIN
        ALTER TABLE dbo.Auditoria_Seguridad
        ADD Resultado VARCHAR(20) NULL;
    END;

    EXEC sys.sp_executesql N'
        UPDATE dbo.Auditoria_Seguridad
           SET Resultado = ''Exito''
         WHERE Resultado IS NULL
            OR Resultado NOT IN (''Exito'', ''Fallido'', ''Denegado'', ''Error'');

        ALTER TABLE dbo.Auditoria_Seguridad
        ALTER COLUMN Resultado VARCHAR(20) NOT NULL;';

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.default_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
          AND parent_column_id = COLUMNPROPERTY(
                OBJECT_ID(N'dbo.Auditoria_Seguridad'),
                N'Resultado',
                'ColumnId')
    )
    BEGIN
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.Auditoria_Seguridad
            ADD CONSTRAINT DF_Auditoria_Seguridad_Resultado
                DEFAULT (''Exito'') FOR Resultado;';
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE name = N'CK_Auditoria_Seguridad_Resultado'
          AND parent_object_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
    )
    BEGIN
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.Auditoria_Seguridad WITH CHECK
            ADD CONSTRAINT CK_Auditoria_Seguridad_Resultado
                CHECK (Resultado IN (''Exito'', ''Fallido'', ''Denegado'', ''Error''));

            ALTER TABLE dbo.Auditoria_Seguridad
            CHECK CONSTRAINT CK_Auditoria_Seguridad_Resultado;';
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE name = N'IX_Auditoria_Seguridad_Resultado'
          AND object_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
    )
    BEGIN
        EXEC sys.sp_executesql N'
            CREATE INDEX IX_Auditoria_Seguridad_Resultado
            ON dbo.Auditoria_Seguridad(Resultado, Fecha_Registro DESC);';
    END;

    MERGE dbo.Esquema_Version AS destino
    USING
    (
        SELECT
            7 AS Version,
            'Resultados de auditoria y autorizacion de modulos agricolas' AS Nombre
    ) AS origen
       ON destino.Version = origen.Version
    WHEN MATCHED THEN
        UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

IF COL_LENGTH(N'dbo.Auditoria_Seguridad', N'Resultado') IS NULL
    THROW 51972, 'No se agrego dbo.Auditoria_Seguridad.Resultado.', 1;

IF (SELECT MAX(Version) FROM dbo.Esquema_Version) <> 7
    THROW 51973, 'La version 7 no quedo registrada.', 1;
GO

PRINT 'Migracion 007 aplicada: resultados de auditoria y autorizacion agricola.';
GO

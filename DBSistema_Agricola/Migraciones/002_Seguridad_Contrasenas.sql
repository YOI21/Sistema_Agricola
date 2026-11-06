USE DBSistema_Agricola;
GO

IF OBJECT_ID('dbo.Esquema_Version', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Esquema_Version
    (
        Version INT NOT NULL CONSTRAINT PK_Esquema_Version PRIMARY KEY,
        Nombre VARCHAR(150) NOT NULL,
        Aplicada_En DATETIME NOT NULL CONSTRAINT DF_Esquema_Version_Aplicada DEFAULT GETDATE()
    );
END;
GO

IF COL_LENGTH('dbo.Usuario', 'Contrasena_Hash') IS NULL
    ALTER TABLE dbo.Usuario ADD Contrasena_Hash VARBINARY(64) NULL;
GO

IF COL_LENGTH('dbo.Usuario', 'Contrasena_Salt') IS NULL
    ALTER TABLE dbo.Usuario ADD Contrasena_Salt VARCHAR(64) NULL;
GO

IF COL_LENGTH('dbo.Usuario', 'Contrasena_Iteraciones') IS NULL
    ALTER TABLE dbo.Usuario ADD Contrasena_Iteraciones INT NULL;
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 2 AS Version, 'Seguridad de contrasenas PBKDF2' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 002 aplicada: seguridad de contrasenas.';
GO

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

IF OBJECT_ID('dbo.Auditoria_Seguridad', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Auditoria_Seguridad
    (
        ID_Auditoria INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_Auditoria_Seguridad PRIMARY KEY,
        ID_Usuario INT NULL,
        Usuario VARCHAR(80) NOT NULL,
        Rol VARCHAR(30) NOT NULL,
        Permiso VARCHAR(80) NOT NULL,
        Entidad VARCHAR(80) NOT NULL,
        Accion VARCHAR(40) NOT NULL,
        Detalle VARCHAR(1000) NULL,
        Nombre_Equipo VARCHAR(128) NULL,
        Usuario_Windows VARCHAR(128) NULL,
        Fecha_Registro DATETIME NOT NULL
            CONSTRAINT DF_Auditoria_Seguridad_Fecha DEFAULT GETDATE()
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Auditoria_Seguridad_Fecha'
      AND object_id = OBJECT_ID('dbo.Auditoria_Seguridad')
)
    CREATE INDEX IX_Auditoria_Seguridad_Fecha
    ON dbo.Auditoria_Seguridad(Fecha_Registro DESC);
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Auditoria_Seguridad_Usuario'
      AND object_id = OBJECT_ID('dbo.Auditoria_Seguridad')
)
    CREATE INDEX IX_Auditoria_Seguridad_Usuario
    ON dbo.Auditoria_Seguridad(Usuario, Rol, Fecha_Registro DESC);
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'db_sistema_agricola_app')
    CREATE ROLE db_sistema_agricola_app;
GO

GRANT EXECUTE TO db_sistema_agricola_app;
GRANT SELECT TO db_sistema_agricola_app;
GRANT INSERT ON dbo.Auditoria_Seguridad TO db_sistema_agricola_app;
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 4 AS Version, 'Autorizacion centralizada y auditoria' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 004 aplicada: autorizacion y auditoria.';
GO

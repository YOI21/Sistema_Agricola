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

IF COL_LENGTH('dbo.Usuario', 'Rol') IS NULL
BEGIN
    ALTER TABLE dbo.Usuario
    ADD Rol VARCHAR(20) NOT NULL
        CONSTRAINT DF_Usuario_Rol DEFAULT 'Agricultor';
END;
GO

UPDATE dbo.Usuario
   SET Rol = 'Agricultor'
 WHERE Rol IS NULL OR LTRIM(RTRIM(Rol)) = '';
GO

IF OBJECT_ID('dbo.Administrador', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Administrador
    (
        ID_Administrador INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_Administrador PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Apellido VARCHAR(50) NOT NULL,
        Email VARCHAR(100) NOT NULL,
        Telefono VARCHAR(20) NOT NULL
            CONSTRAINT DF_Administrador_Telefono DEFAULT '',
        Fecha_Registro DATETIME NOT NULL
            CONSTRAINT DF_Administrador_Fecha DEFAULT GETDATE(),
        ID_Usuario INT NOT NULL
    );
END;
GO

IF COL_LENGTH('dbo.Administrador', 'Telefono') IS NULL
    ALTER TABLE dbo.Administrador ADD Telefono VARCHAR(20) NOT NULL CONSTRAINT DF_Administrador_Telefono DEFAULT '';
GO

IF COL_LENGTH('dbo.Administrador', 'Fecha_Registro') IS NULL
    ALTER TABLE dbo.Administrador ADD Fecha_Registro DATETIME NOT NULL CONSTRAINT DF_Administrador_Fecha DEFAULT GETDATE();
GO

IF COL_LENGTH('dbo.Administrador', 'ID_Usuario') IS NULL
    ALTER TABLE dbo.Administrador ADD ID_Usuario INT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Administrador_Email')
    CREATE UNIQUE INDEX UX_Administrador_Email ON dbo.Administrador(Email);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Administrador_ID_Usuario')
    CREATE UNIQUE INDEX UX_Administrador_ID_Usuario ON dbo.Administrador(ID_Usuario);
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 1 AS Version, 'Administrador y roles de usuario' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 001 aplicada: administrador y roles.';
GO

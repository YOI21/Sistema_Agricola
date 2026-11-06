USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
GO

IF OBJECT_ID('dbo.Esquema_Version', 'U') IS NULL
    THROW 51960, 'Falta dbo.Esquema_Version. Ejecute primero las migraciones 000 a 005.', 1;
GO

IF OBJECT_ID('dbo.Alertas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Alertas
    (
        ID_Alerta INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Alertas PRIMARY KEY,
        ID_Cultivo INT NULL,
        Titulo VARCHAR(150) NOT NULL,
        Descripcion VARCHAR(500) NOT NULL,
        Fecha_Programada DATE NOT NULL,
        Fecha_Creacion DATETIME NOT NULL CONSTRAINT DF_Alertas_Fecha_Creacion DEFAULT GETDATE(),
        Tipo_Alerta VARCHAR(50) NOT NULL,
        Estado VARCHAR(20) NOT NULL CONSTRAINT DF_Alertas_Estado DEFAULT 'Pendiente',
        Prioridad VARCHAR(10) NOT NULL CONSTRAINT DF_Alertas_Prioridad DEFAULT 'Media',
        Notificacion_Enviada BIT NOT NULL CONSTRAINT DF_Alertas_Notificacion DEFAULT 0,
        Dias_Anticipacion INT NOT NULL CONSTRAINT DF_Alertas_Dias_Anticipacion DEFAULT 7
    );
END;
GO

IF COL_LENGTH('dbo.Alertas', 'ID_Cultivo') IS NULL
    ALTER TABLE dbo.Alertas ADD ID_Cultivo INT NULL;
IF COL_LENGTH('dbo.Alertas', 'Titulo') IS NULL
    ALTER TABLE dbo.Alertas ADD Titulo VARCHAR(150) NOT NULL CONSTRAINT DF_Alertas_Titulo DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Descripcion') IS NULL
    ALTER TABLE dbo.Alertas ADD Descripcion VARCHAR(500) NOT NULL CONSTRAINT DF_Alertas_Descripcion DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Fecha_Programada') IS NULL
    ALTER TABLE dbo.Alertas ADD Fecha_Programada DATE NOT NULL CONSTRAINT DF_Alertas_Fecha_Programada DEFAULT CAST(GETDATE() AS DATE) WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Fecha_Creacion') IS NULL
    ALTER TABLE dbo.Alertas ADD Fecha_Creacion DATETIME NOT NULL CONSTRAINT DF_Alertas_Fecha_Creacion_Migracion DEFAULT GETDATE() WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Tipo_Alerta') IS NULL
    ALTER TABLE dbo.Alertas ADD Tipo_Alerta VARCHAR(50) NOT NULL CONSTRAINT DF_Alertas_Tipo DEFAULT 'Otro' WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Estado') IS NULL
    ALTER TABLE dbo.Alertas ADD Estado VARCHAR(20) NOT NULL CONSTRAINT DF_Alertas_Estado_Migracion DEFAULT 'Pendiente' WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Prioridad') IS NULL
    ALTER TABLE dbo.Alertas ADD Prioridad VARCHAR(10) NOT NULL CONSTRAINT DF_Alertas_Prioridad_Migracion DEFAULT 'Media' WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Notificacion_Enviada') IS NULL
    ALTER TABLE dbo.Alertas ADD Notificacion_Enviada BIT NOT NULL CONSTRAINT DF_Alertas_Notificacion_Migracion DEFAULT 0 WITH VALUES;
IF COL_LENGTH('dbo.Alertas', 'Dias_Anticipacion') IS NULL
    ALTER TABLE dbo.Alertas ADD Dias_Anticipacion INT NOT NULL CONSTRAINT DF_Alertas_Dias_Migracion DEFAULT 7 WITH VALUES;
GO

IF COL_LENGTH('dbo.Alertas', 'Titulo') BETWEEN 1 AND 149
    ALTER TABLE dbo.Alertas ALTER COLUMN Titulo VARCHAR(150) NOT NULL;
IF COL_LENGTH('dbo.Alertas', 'Descripcion') BETWEEN 1 AND 499
    ALTER TABLE dbo.Alertas ALTER COLUMN Descripcion VARCHAR(500) NOT NULL;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Alertas')
      AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('dbo.Alertas'), 'Fecha_Creacion', 'ColumnId')
)
    ALTER TABLE dbo.Alertas ADD CONSTRAINT DF_Alertas_Fecha_Creacion_006 DEFAULT GETDATE() FOR Fecha_Creacion;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Alertas')
      AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('dbo.Alertas'), 'Estado', 'ColumnId')
)
    ALTER TABLE dbo.Alertas ADD CONSTRAINT DF_Alertas_Estado_006 DEFAULT 'Pendiente' FOR Estado;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Alertas')
      AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('dbo.Alertas'), 'Prioridad', 'ColumnId')
)
    ALTER TABLE dbo.Alertas ADD CONSTRAINT DF_Alertas_Prioridad_006 DEFAULT 'Media' FOR Prioridad;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Alertas')
      AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('dbo.Alertas'), 'Notificacion_Enviada', 'ColumnId')
)
    ALTER TABLE dbo.Alertas ADD CONSTRAINT DF_Alertas_Notificacion_006 DEFAULT 0 FOR Notificacion_Enviada;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.default_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Alertas')
      AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('dbo.Alertas'), 'Dias_Anticipacion', 'ColumnId')
)
    ALTER TABLE dbo.Alertas ADD CONSTRAINT DF_Alertas_Dias_006 DEFAULT 7 FOR Dias_Anticipacion;
GO

IF OBJECT_ID('dbo.Siembra', 'U') IS NULL
    THROW 51961, 'Falta la tabla base dbo.Siembra.', 1;
IF OBJECT_ID('dbo.Cosecha', 'U') IS NULL
    THROW 51962, 'Falta la tabla base dbo.Cosecha.', 1;
GO

IF COL_LENGTH('dbo.Siembra', 'Costo_Siembra') IS NULL
    ALTER TABLE dbo.Siembra ADD Costo_Siembra DECIMAL(10, 2) NULL;
IF COL_LENGTH('dbo.Siembra', 'ID_Terreno') IS NULL
    ALTER TABLE dbo.Siembra ADD ID_Terreno INT NULL;
IF COL_LENGTH('dbo.Siembra', 'Hectareas_Sembradas') IS NULL
    ALTER TABLE dbo.Siembra ADD Hectareas_Sembradas DECIMAL(10, 2) NULL;

IF COL_LENGTH('dbo.Cosecha', 'Cantidad_Cosechada') IS NULL
    ALTER TABLE dbo.Cosecha ADD Cantidad_Cosechada DECIMAL(10, 2) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Rendimiento') IS NULL
    ALTER TABLE dbo.Cosecha ADD Rendimiento DECIMAL(10, 2) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Calidad') IS NULL
    ALTER TABLE dbo.Cosecha ADD Calidad DECIMAL(5, 2) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Observaciones') IS NULL
    ALTER TABLE dbo.Cosecha ADD Observaciones VARCHAR(200) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Fecha_Cosecha') IS NULL
    ALTER TABLE dbo.Cosecha ADD Fecha_Cosecha DATE NULL;
IF COL_LENGTH('dbo.Cosecha', 'Costo_Cosecha') IS NULL
    ALTER TABLE dbo.Cosecha ADD Costo_Cosecha DECIMAL(10, 2) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Precio_Venta') IS NULL
    ALTER TABLE dbo.Cosecha ADD Precio_Venta DECIMAL(10, 2) NULL;
IF COL_LENGTH('dbo.Cosecha', 'Beneficio') IS NULL
    ALTER TABLE dbo.Cosecha ADD Beneficio DECIMAL(10, 2) NULL;
GO

IF OBJECT_ID('dbo.Agricultor_Siembra', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Agricultor_Siembra
    (
        ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Agricultor_Siembra PRIMARY KEY,
        ID_Agricultor INT NULL,
        ID_Cultivo INT NULL,
        Fecha_Asignacion DATETIME NOT NULL CONSTRAINT DF_Agricultor_Siembra_Fecha DEFAULT GETDATE(),
        Notas VARCHAR(500) NULL
    );
END;
GO

IF COL_LENGTH('dbo.Agricultor_Siembra', 'ID_Agricultor') IS NULL
    ALTER TABLE dbo.Agricultor_Siembra ADD ID_Agricultor INT NULL;
IF COL_LENGTH('dbo.Agricultor_Siembra', 'ID_Cultivo') IS NULL
    ALTER TABLE dbo.Agricultor_Siembra ADD ID_Cultivo INT NULL;
IF COL_LENGTH('dbo.Agricultor_Siembra', 'Fecha_Asignacion') IS NULL
    ALTER TABLE dbo.Agricultor_Siembra ADD Fecha_Asignacion DATETIME NOT NULL CONSTRAINT DF_Agricultor_Siembra_Fecha_006 DEFAULT GETDATE() WITH VALUES;
IF COL_LENGTH('dbo.Agricultor_Siembra', 'Notas') IS NULL
    ALTER TABLE dbo.Agricultor_Siembra ADD Notas VARCHAR(500) NULL;
GO

IF EXISTS
(
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Agricultor_Siembra')
      AND name = 'Fecha_Asignacion'
      AND system_type_id = TYPE_ID('date')
)
    ALTER TABLE dbo.Agricultor_Siembra ALTER COLUMN Fecha_Asignacion DATETIME NOT NULL;

IF COL_LENGTH('dbo.Agricultor_Siembra', 'Notas') BETWEEN 1 AND 499
    ALTER TABLE dbo.Agricultor_Siembra ALTER COLUMN Notas VARCHAR(500) NULL;
GO

IF OBJECT_ID('dbo.Enfermedades', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Enfermedades
    (
        Id_CP INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Enfermedades PRIMARY KEY,
        Tipo VARCHAR(100) NOT NULL,
        Nombre_Comun VARCHAR(100) NOT NULL,
        Nombre_Cientifico VARCHAR(120) NOT NULL,
        Razgos VARCHAR(500) NULL,
        Huesped VARCHAR(MAX) NOT NULL,
        Cura VARCHAR(MAX) NOT NULL
    );
END;
GO

IF COL_LENGTH('dbo.Enfermedades', 'Tipo') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Tipo VARCHAR(100) NOT NULL CONSTRAINT DF_Enfermedades_Tipo_006 DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Enfermedades', 'Nombre_Comun') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Nombre_Comun VARCHAR(100) NOT NULL CONSTRAINT DF_Enfermedades_Nombre_006 DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Enfermedades', 'Nombre_Cientifico') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Nombre_Cientifico VARCHAR(120) NOT NULL CONSTRAINT DF_Enfermedades_Cientifico_006 DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Enfermedades', 'Razgos') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Razgos VARCHAR(500) NULL;
IF COL_LENGTH('dbo.Enfermedades', 'Huesped') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Huesped VARCHAR(MAX) NOT NULL CONSTRAINT DF_Enfermedades_Huesped_006 DEFAULT '' WITH VALUES;
IF COL_LENGTH('dbo.Enfermedades', 'Cura') IS NULL
    ALTER TABLE dbo.Enfermedades ADD Cura VARCHAR(MAX) NOT NULL CONSTRAINT DF_Enfermedades_Cura_006 DEFAULT '' WITH VALUES;
GO

IF COL_LENGTH('dbo.Enfermedades', 'Tipo') BETWEEN 1 AND 99
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Tipo VARCHAR(100) NOT NULL;
IF COL_LENGTH('dbo.Enfermedades', 'Nombre_Comun') BETWEEN 1 AND 99
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Nombre_Comun VARCHAR(100) NOT NULL;
IF COL_LENGTH('dbo.Enfermedades', 'Nombre_Cientifico') BETWEEN 1 AND 119
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Nombre_Cientifico VARCHAR(120) NOT NULL;
IF COL_LENGTH('dbo.Enfermedades', 'Razgos') BETWEEN 1 AND 499
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Razgos VARCHAR(500) NULL;
GO

IF EXISTS
(
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Enfermedades')
      AND name = 'Huesped'
      AND (system_type_id = TYPE_ID('text') OR max_length <> -1 OR is_nullable = 1)
)
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Huesped VARCHAR(MAX) NOT NULL;

IF EXISTS
(
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Enfermedades')
      AND name = 'Cura'
      AND (system_type_id = TYPE_ID('text') OR max_length <> -1 OR is_nullable = 1)
)
    ALTER TABLE dbo.Enfermedades ALTER COLUMN Cura VARCHAR(MAX) NOT NULL;
GO

IF OBJECT_ID('dbo.Alertas', 'U') IS NULL
    THROW 51963, 'No se pudo preparar dbo.Alertas.', 1;
IF OBJECT_ID('dbo.Agricultor_Siembra', 'U') IS NULL
    THROW 51964, 'No se pudo preparar dbo.Agricultor_Siembra.', 1;
IF OBJECT_ID('dbo.Enfermedades', 'U') IS NULL
    THROW 51965, 'No se pudo preparar dbo.Enfermedades.', 1;
IF COL_LENGTH('dbo.Alertas', 'ID_Cultivo') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Titulo') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Descripcion') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Fecha_Programada') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Fecha_Creacion') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Tipo_Alerta') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Estado') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Prioridad') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Notificacion_Enviada') IS NULL OR
   COL_LENGTH('dbo.Alertas', 'Dias_Anticipacion') IS NULL
    THROW 51968, 'La estructura de dbo.Alertas está incompleta.', 1;
IF COL_LENGTH('dbo.Siembra', 'ID_Terreno') IS NULL OR
   COL_LENGTH('dbo.Siembra', 'Hectareas_Sembradas') IS NULL OR
   COL_LENGTH('dbo.Siembra', 'Costo_Siembra') IS NULL
    THROW 51966, 'La estructura de dbo.Siembra está incompleta.', 1;
IF COL_LENGTH('dbo.Cosecha', 'Beneficio') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Cantidad_Cosechada') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Rendimiento') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Calidad') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Observaciones') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Fecha_Cosecha') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Costo_Cosecha') IS NULL OR
   COL_LENGTH('dbo.Cosecha', 'Precio_Venta') IS NULL
    THROW 51967, 'La estructura de dbo.Cosecha está incompleta.', 1;
IF COL_LENGTH('dbo.Agricultor_Siembra', 'ID_Agricultor') IS NULL OR
   COL_LENGTH('dbo.Agricultor_Siembra', 'ID_Cultivo') IS NULL OR
   COL_LENGTH('dbo.Agricultor_Siembra', 'Fecha_Asignacion') IS NULL OR
   COL_LENGTH('dbo.Agricultor_Siembra', 'Notas') IS NULL
    THROW 51969, 'La estructura de dbo.Agricultor_Siembra está incompleta.', 1;
IF COL_LENGTH('dbo.Enfermedades', 'Tipo') IS NULL OR
   COL_LENGTH('dbo.Enfermedades', 'Nombre_Comun') IS NULL OR
   COL_LENGTH('dbo.Enfermedades', 'Nombre_Cientifico') IS NULL OR
   COL_LENGTH('dbo.Enfermedades', 'Razgos') IS NULL OR
   COL_LENGTH('dbo.Enfermedades', 'Huesped') IS NULL OR
   COL_LENGTH('dbo.Enfermedades', 'Cura') IS NULL
    THROW 51970, 'La estructura de dbo.Enfermedades está incompleta.', 1;
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 6 AS Version, 'Estructura de alertas, siembra, cosecha y catalogos fuera del DAL' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 006 aplicada: estructura operativa separada del inicio de la aplicacion.';
GO

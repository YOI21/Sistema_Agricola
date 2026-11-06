USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
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

CREATE OR ALTER PROCEDURE dbo.Actualizar_Terreno
    @ID_Terreno INT,
    @Nombre VARCHAR(100),
    @Area DECIMAL(10, 2),
    @UnidadMedida VARCHAR(20),
    @TipoSuelo VARCHAR(50),
    @pH DECIMAL(4, 2),
    @Pendiente DECIMAL(5, 2),
    @Humedad DECIMAL(5, 2),
    @Ubicacion VARCHAR(200),
    @Propietario VARCHAR(100),
    @EstadoActual VARCHAR(20),
    @Observaciones VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Terrenos WHERE ID_Terreno = @ID_Terreno)
        THROW 51030, 'El terreno indicado no existe.', 1;

    UPDATE dbo.Terrenos
    SET Nombre_Terreno = @Nombre,
        Ubicacion = @Ubicacion,
        Superficie_Total = @Area,
        Unidad_Medida = @UnidadMedida,
        Tipo_Suelo = @TipoSuelo,
        Pendiente = CONVERT(VARCHAR(50), @Pendiente),
        pH_Suelo = @pH,
        Humedad = @Humedad,
        Propietario = @Propietario,
        Estado = @EstadoActual,
        Observaciones = @Observaciones
    WHERE ID_Terreno = @ID_Terreno;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Eliminar_Terreno
    @ID_Terreno INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Terrenos WHERE ID_Terreno = @ID_Terreno)
        THROW 51031, 'El terreno indicado no existe.', 1;

    IF EXISTS (SELECT 1 FROM dbo.Siembra WHERE ID_Terreno = @ID_Terreno)
        THROW 51032, 'No se puede eliminar el terreno porque tiene siembras asociadas.', 1;

    DELETE FROM dbo.Terrenos
    WHERE ID_Terreno = @ID_Terreno;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Insertar_Fertilizacion
    @ID_Cultivo INT,
    @Tipo_Fertilizante VARCHAR(50) = NULL,
    @Metodo_Fertilizacion VARCHAR(50) = NULL,
    @Cantidad_Fertilizante INT = NULL,
    @Costo_Fertilizacion DECIMAL(10, 2) = NULL,
    @Fecha_Aplicacion DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Siembra WHERE ID_Cultivo = @ID_Cultivo)
        THROW 51033, 'La siembra indicada no existe.', 1;

    INSERT INTO dbo.Fertilizacion
    (
        ID_Cultivo,
        Tipo_Fertilizante,
        Metodo_Fertilizacion,
        Cantidad_Fertilizante,
        Costo_Fertilizacion,
        Fecha_Aplicacion
    )
    VALUES
    (
        @ID_Cultivo,
        NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), ''),
        NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), ''),
        @Cantidad_Fertilizante,
        @Costo_Fertilizacion,
        @Fecha_Aplicacion
    );
END;
GO

IF OBJECT_ID('dbo.Actualizar_Terreno', 'P') IS NULL
    THROW 51903, 'No se pudo crear dbo.Actualizar_Terreno.', 1;

IF OBJECT_ID('dbo.Eliminar_Terreno', 'P') IS NULL
    THROW 51904, 'No se pudo crear dbo.Eliminar_Terreno.', 1;

IF OBJECT_ID('dbo.Insertar_Fertilizacion', 'P') IS NULL
    THROW 51905, 'No se pudo crear dbo.Insertar_Fertilizacion.', 1;
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 5 AS Version, 'Procedimientos requeridos por terrenos y fertilizacion' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 005 aplicada: terrenos y fertilizacion.';
GO

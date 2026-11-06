USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) NOT IN (9, 10)
    THROW 51940, 'La migracion 010 requiere una base en version 9 o una version 10 que deba repararse.', 1;

IF OBJECT_ID(N'dbo.Terrenos', N'U') IS NULL OR OBJECT_ID(N'dbo.Siembra', N'U') IS NULL
    THROW 51941, 'No existen las tablas Terrenos y Siembra requeridas.', 1;
GO

IF COL_LENGTH(N'dbo.Terrenos', N'Provincia') IS NULL
    ALTER TABLE dbo.Terrenos ADD Provincia VARCHAR(50) NULL;

IF COL_LENGTH(N'dbo.Terrenos', N'Zona_Agroclimatica') IS NULL
    ALTER TABLE dbo.Terrenos ADD Zona_Agroclimatica VARCHAR(80) NULL;

IF COL_LENGTH(N'dbo.Terrenos', N'Precipitacion_Anual') IS NULL
    ALTER TABLE dbo.Terrenos ADD Precipitacion_Anual DECIMAL(8,2) NULL;

IF COL_LENGTH(N'dbo.Terrenos', N'Disponibilidad_Riego') IS NULL
BEGIN
    ALTER TABLE dbo.Terrenos ADD Disponibilidad_Riego BIT NOT NULL
        CONSTRAINT DF_Terrenos_Disponibilidad_Riego DEFAULT (0) WITH VALUES;
END;

IF COL_LENGTH(N'dbo.Siembra', N'Variedad_Cultivo') IS NULL
    ALTER TABLE dbo.Siembra ADD Variedad_Cultivo VARCHAR(80) NULL;
GO

CREATE OR ALTER PROCEDURE dbo.Insertar_Terreno
    @Nombre VARCHAR(100),
    @Area DECIMAL(10,2),
    @UnidadMedida VARCHAR(20),
    @TipoSuelo VARCHAR(50),
    @pH DECIMAL(4,2),
    @Pendiente DECIMAL(5,2),
    @Humedad DECIMAL(5,2),
    @Drenaje VARCHAR(50) = NULL,
    @FuenteAgua VARCHAR(50) = NULL,
    @Provincia VARCHAR(50) = NULL,
    @ZonaAgroclimatica VARCHAR(80) = NULL,
    @PrecipitacionAnual DECIMAL(8,2) = NULL,
    @DisponibilidadRiego BIT = 0,
    @Ubicacion VARCHAR(200),
    @Propietario VARCHAR(100),
    @FechaRegistro DATETIME,
    @EstadoActual VARCHAR(20),
    @Observaciones VARCHAR(500) = NULL,
    @ID_Terreno_Out INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT dbo.Terrenos
    (
        Nombre_Terreno, Ubicacion, Superficie_Total, Unidad_Medida, Tipo_Suelo,
        Pendiente, pH_Suelo, Humedad, Drenaje, Fuente_Agua, Provincia,
        Zona_Agroclimatica, Precipitacion_Anual, Disponibilidad_Riego,
        Propietario, Fecha_Registro, Estado, Observaciones
    )
    VALUES
    (
        @Nombre, @Ubicacion, @Area, @UnidadMedida, @TipoSuelo,
        CONVERT(VARCHAR(50), @Pendiente), NULLIF(@pH, 0), NULLIF(@Humedad, 0),
        NULLIF(LTRIM(RTRIM(@Drenaje)), ''), NULLIF(LTRIM(RTRIM(@FuenteAgua)), ''),
        NULLIF(LTRIM(RTRIM(@Provincia)), ''), NULLIF(LTRIM(RTRIM(@ZonaAgroclimatica)), ''),
        NULLIF(@PrecipitacionAnual, 0), @DisponibilidadRiego,
        @Propietario, @FechaRegistro, @EstadoActual,
        NULLIF(LTRIM(RTRIM(@Observaciones)), '')
    );

    SET @ID_Terreno_Out = CONVERT(INT, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_Terreno
    @ID_Terreno INT,
    @Nombre VARCHAR(100),
    @Area DECIMAL(10,2),
    @UnidadMedida VARCHAR(20),
    @TipoSuelo VARCHAR(50),
    @pH DECIMAL(4,2),
    @Pendiente DECIMAL(5,2),
    @Humedad DECIMAL(5,2),
    @Drenaje VARCHAR(50) = NULL,
    @FuenteAgua VARCHAR(50) = NULL,
    @Provincia VARCHAR(50) = NULL,
    @ZonaAgroclimatica VARCHAR(80) = NULL,
    @PrecipitacionAnual DECIMAL(8,2) = NULL,
    @DisponibilidadRiego BIT = 0,
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
           pH_Suelo = NULLIF(@pH, 0),
           Humedad = NULLIF(@Humedad, 0),
           Drenaje = NULLIF(LTRIM(RTRIM(@Drenaje)), ''),
           Fuente_Agua = NULLIF(LTRIM(RTRIM(@FuenteAgua)), ''),
           Provincia = NULLIF(LTRIM(RTRIM(@Provincia)), ''),
           Zona_Agroclimatica = NULLIF(LTRIM(RTRIM(@ZonaAgroclimatica)), ''),
           Precipitacion_Anual = NULLIF(@PrecipitacionAnual, 0),
           Disponibilidad_Riego = @DisponibilidadRiego,
           Propietario = @Propietario,
           Estado = @EstadoActual,
           Observaciones = NULLIF(LTRIM(RTRIM(@Observaciones)), '')
     WHERE ID_Terreno = @ID_Terreno;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Siembra_Atomica
    @Nombre_Cultivo VARCHAR(100),
    @Variedad_Cultivo VARCHAR(80) = NULL,
    @Tipo_Cultivo VARCHAR(50),
    @Categoria_Cultivo VARCHAR(50),
    @Estacion_Siembra VARCHAR(50),
    @Cantidad INT,
    @Tipo_Suelo VARCHAR(50),
    @Fecha_Plantacion DATE,
    @Fecha_Cosecha DATE,
    @ID_Terreno INT,
    @Hectareas_Sembradas DECIMAL(10,2),
    @Metodo_Siembra VARCHAR(50) = NULL,
    @Profundidad VARCHAR(50) = NULL,
    @Humedad_Suelo DECIMAL(10,2) = NULL,
    @Costo_Siembra DECIMAL(10,2) = NULL,
    @Tipo_Fertilizante VARCHAR(50) = NULL,
    @Metodo_Fertilizacion VARCHAR(50) = NULL,
    @Cantidad_Fertilizante INT = NULL,
    @Costo_Fertilizacion DECIMAL(10,2) = NULL,
    @Fecha_Aplicacion DATE = NULL,
    @ID_Agricultor INT = NULL,
    @Notas VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NULLIF(LTRIM(RTRIM(@Nombre_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Tipo_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Categoria_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Estacion_Siembra)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Tipo_Suelo)), '') IS NULL
            THROW 51810, 'Complete los datos obligatorios de la siembra.', 1;

        IF @Cantidad <= 0
            THROW 51811, 'La cantidad de semillas debe ser mayor que cero.', 1;
        IF @Fecha_Cosecha < @Fecha_Plantacion
            THROW 51812, 'La fecha de cosecha no puede ser anterior a la plantacion.', 1;
        IF @ID_Terreno IS NULL OR @ID_Terreno <= 0 OR
           @Hectareas_Sembradas IS NULL OR @Hectareas_Sembradas <= 0
            THROW 51813, 'Seleccione un terreno e indique las hectareas que se sembraran.', 1;
        IF ISNULL(@Humedad_Suelo, 0) < 0 OR ISNULL(@Costo_Siembra, 0) < 0 OR
           ISNULL(@Cantidad_Fertilizante, 0) < 0 OR ISNULL(@Costo_Fertilizacion, 0) < 0
            THROW 51814, 'Las cantidades y costos no pueden ser negativos.', 1;

        DECLARE @Superficie_Total DECIMAL(10,2), @Estado_Terreno VARCHAR(20);
        SELECT @Superficie_Total = Superficie_Total, @Estado_Terreno = Estado
        FROM dbo.Terrenos WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Terreno = @ID_Terreno;

        IF @Superficie_Total IS NULL
            THROW 51815, 'El terreno seleccionado no existe.', 1;
        IF @Estado_Terreno = 'Inactivo'
            THROW 51816, 'No se puede sembrar en un terreno inactivo.', 1;

        DECLARE @Hectareas_Ocupadas DECIMAL(10,2);
        SELECT @Hectareas_Ocupadas = ISNULL(SUM(ISNULL(Hectareas_Sembradas, 0)), 0)
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Terreno = @ID_Terreno
          AND (Estado_Cultivo IS NULL OR Estado_Cultivo NOT IN
              ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'));

        IF @Hectareas_Ocupadas + @Hectareas_Sembradas > @Superficie_Total
            THROW 51817, 'Las hectareas indicadas superan el espacio disponible del terreno.', 1;

        IF @ID_Agricultor IS NOT NULL AND @ID_Agricultor > 0 AND
           NOT EXISTS (SELECT 1 FROM dbo.Agricultor WITH (UPDLOCK, HOLDLOCK)
                       WHERE ID_Agricultor = @ID_Agricultor)
            THROW 51818, 'El agricultor seleccionado no existe.', 1;

        INSERT dbo.Siembra
        (
            Nombre_Cultivo, Variedad_Cultivo, Tipo_Cultivo, Categoria_Cultivo,
            Estacion_Siembra, Cantidad, Tipo_Suelo, Fecha_Plantacion, Fecha_Cosecha,
            Estado_Cultivo, Metodo_Siembra, Profundidad, Humedad_Suelo,
            Costo_Siembra, ID_Terreno, Hectareas_Sembradas
        )
        VALUES
        (
            @Nombre_Cultivo, NULLIF(LTRIM(RTRIM(@Variedad_Cultivo)), ''),
            @Tipo_Cultivo, @Categoria_Cultivo, @Estacion_Siembra, @Cantidad,
            @Tipo_Suelo, @Fecha_Plantacion, @Fecha_Cosecha, 'Activo',
            NULLIF(LTRIM(RTRIM(@Metodo_Siembra)), ''),
            NULLIF(LTRIM(RTRIM(@Profundidad)), ''), @Humedad_Suelo,
            @Costo_Siembra, @ID_Terreno, @Hectareas_Sembradas
        );

        DECLARE @ID_Cultivo INT = CONVERT(INT, SCOPE_IDENTITY());

        IF NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), '') IS NOT NULL OR
           NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), '') IS NOT NULL OR
           @Cantidad_Fertilizante IS NOT NULL OR @Costo_Fertilizacion IS NOT NULL OR
           @Fecha_Aplicacion IS NOT NULL
        BEGIN
            INSERT dbo.Fertilizacion
                (ID_Cultivo, Tipo_Fertilizante, Metodo_Fertilizacion,
                 Cantidad_Fertilizante, Costo_Fertilizacion, Fecha_Aplicacion)
            VALUES
                (@ID_Cultivo, NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), ''),
                 NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), ''),
                 @Cantidad_Fertilizante, @Costo_Fertilizacion, @Fecha_Aplicacion);
        END;

        IF @ID_Agricultor IS NOT NULL AND @ID_Agricultor > 0
        BEGIN
            INSERT dbo.Agricultor_Siembra
                (ID_Agricultor, ID_Cultivo, Fecha_Asignacion, Notas)
            VALUES
                (@ID_Agricultor, @ID_Cultivo, GETDATE(),
                 NULLIF(LTRIM(RTRIM(@Notas)), ''));
        END;

        UPDATE dbo.Terrenos SET Estado = 'En uso'
        WHERE ID_Terreno = @ID_Terreno AND Estado <> 'Inactivo';

        COMMIT TRANSACTION;
        SELECT @ID_Cultivo;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_Siembra_Atomica
    @ID_Cultivo INT,
    @Nombre_Cultivo VARCHAR(100),
    @Variedad_Cultivo VARCHAR(80) = NULL,
    @Tipo_Cultivo VARCHAR(50),
    @Categoria_Cultivo VARCHAR(50),
    @Estacion_Siembra VARCHAR(50),
    @Cantidad INT,
    @Tipo_Suelo VARCHAR(50),
    @Fecha_Plantacion DATE,
    @Fecha_Cosecha DATE,
    @ID_Terreno INT,
    @Hectareas_Sembradas DECIMAL(10,2),
    @Metodo_Siembra VARCHAR(50) = NULL,
    @Profundidad VARCHAR(50) = NULL,
    @Humedad_Suelo DECIMAL(10,2) = NULL,
    @Costo_Siembra DECIMAL(10,2) = NULL,
    @Tipo_Fertilizante VARCHAR(50) = NULL,
    @Metodo_Fertilizacion VARCHAR(50) = NULL,
    @Cantidad_Fertilizante INT = NULL,
    @Costo_Fertilizacion DECIMAL(10,2) = NULL,
    @Fecha_Aplicacion DATE = NULL,
    @ID_Agricultor INT = NULL,
    @Notas VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ID_Cultivo <= 0
            THROW 51820, 'La siembra seleccionada no es valida.', 1;
        IF NULLIF(LTRIM(RTRIM(@Nombre_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Tipo_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Categoria_Cultivo)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Estacion_Siembra)), '') IS NULL OR
           NULLIF(LTRIM(RTRIM(@Tipo_Suelo)), '') IS NULL
            THROW 51821, 'Complete los datos obligatorios de la siembra.', 1;
        IF @Cantidad <= 0 OR @Fecha_Cosecha < @Fecha_Plantacion
            THROW 51822, 'La cantidad o las fechas de la siembra no son validas.', 1;
        IF @ID_Terreno IS NULL OR @ID_Terreno <= 0 OR
           @Hectareas_Sembradas IS NULL OR @Hectareas_Sembradas <= 0
            THROW 51823, 'Seleccione un terreno e indique las hectareas que se sembraran.', 1;
        IF ISNULL(@Humedad_Suelo, 0) < 0 OR ISNULL(@Costo_Siembra, 0) < 0 OR
           ISNULL(@Cantidad_Fertilizante, 0) < 0 OR ISNULL(@Costo_Fertilizacion, 0) < 0
            THROW 51824, 'Las cantidades y costos no pueden ser negativos.', 1;

        DECLARE @ID_Terreno_Anterior INT, @Estado_Cultivo VARCHAR(50);
        SELECT @ID_Terreno_Anterior = ID_Terreno, @Estado_Cultivo = Estado_Cultivo
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51825, 'La siembra seleccionada no existe.', 1;

        DECLARE @Terreno_Bloqueado INT;
        SELECT @Terreno_Bloqueado = ID_Terreno
        FROM dbo.Terrenos WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Terreno IN (@ID_Terreno, @ID_Terreno_Anterior)
        ORDER BY ID_Terreno;

        DECLARE @Superficie_Total DECIMAL(10,2), @Estado_Terreno VARCHAR(20);
        SELECT @Superficie_Total = Superficie_Total, @Estado_Terreno = Estado
        FROM dbo.Terrenos WHERE ID_Terreno = @ID_Terreno;

        IF @Superficie_Total IS NULL
            THROW 51826, 'El terreno seleccionado no existe.', 1;
        IF @Estado_Terreno = 'Inactivo'
            THROW 51827, 'No se puede sembrar en un terreno inactivo.', 1;

        IF @Estado_Cultivo IS NULL OR @Estado_Cultivo NOT IN
           ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado')
        BEGIN
            DECLARE @Hectareas_Ocupadas DECIMAL(10,2);
            SELECT @Hectareas_Ocupadas = ISNULL(SUM(ISNULL(Hectareas_Sembradas, 0)), 0)
            FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Terreno = @ID_Terreno
              AND ID_Cultivo <> @ID_Cultivo
              AND (Estado_Cultivo IS NULL OR Estado_Cultivo NOT IN
                  ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'));

            IF @Hectareas_Ocupadas + @Hectareas_Sembradas > @Superficie_Total
                THROW 51828, 'Las hectareas indicadas superan el espacio disponible del terreno.', 1;
        END;

        IF @ID_Agricultor IS NOT NULL AND @ID_Agricultor > 0 AND
           NOT EXISTS (SELECT 1 FROM dbo.Agricultor WITH (UPDLOCK, HOLDLOCK)
                       WHERE ID_Agricultor = @ID_Agricultor)
            THROW 51829, 'El agricultor seleccionado no existe.', 1;

        UPDATE dbo.Siembra
           SET Nombre_Cultivo = @Nombre_Cultivo,
               Variedad_Cultivo = NULLIF(LTRIM(RTRIM(@Variedad_Cultivo)), ''),
               Tipo_Cultivo = @Tipo_Cultivo,
               Categoria_Cultivo = @Categoria_Cultivo,
               Estacion_Siembra = @Estacion_Siembra,
               Cantidad = @Cantidad,
               Tipo_Suelo = @Tipo_Suelo,
               Fecha_Plantacion = @Fecha_Plantacion,
               Fecha_Cosecha = @Fecha_Cosecha,
               Metodo_Siembra = NULLIF(LTRIM(RTRIM(@Metodo_Siembra)), ''),
               Profundidad = NULLIF(LTRIM(RTRIM(@Profundidad)), ''),
               Humedad_Suelo = @Humedad_Suelo,
               Costo_Siembra = @Costo_Siembra,
               ID_Terreno = @ID_Terreno,
               Hectareas_Sembradas = @Hectareas_Sembradas
         WHERE ID_Cultivo = @ID_Cultivo;

        IF NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), '') IS NOT NULL OR
           NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), '') IS NOT NULL OR
           @Cantidad_Fertilizante IS NOT NULL OR @Costo_Fertilizacion IS NOT NULL OR
           @Fecha_Aplicacion IS NOT NULL
        BEGIN
            DECLARE @ID_Fertilizacion INT;
            SELECT TOP (1) @ID_Fertilizacion = ID_Fertilizacion
            FROM dbo.Fertilizacion WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Cultivo = @ID_Cultivo ORDER BY ID_Fertilizacion;

            IF @ID_Fertilizacion IS NULL
                INSERT dbo.Fertilizacion
                    (ID_Cultivo, Tipo_Fertilizante, Metodo_Fertilizacion,
                     Cantidad_Fertilizante, Costo_Fertilizacion, Fecha_Aplicacion)
                VALUES
                    (@ID_Cultivo, NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), ''),
                     NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), ''),
                     @Cantidad_Fertilizante, @Costo_Fertilizacion, @Fecha_Aplicacion);
            ELSE
                UPDATE dbo.Fertilizacion
                   SET Tipo_Fertilizante = NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), ''),
                       Metodo_Fertilizacion = NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), ''),
                       Cantidad_Fertilizante = @Cantidad_Fertilizante,
                       Costo_Fertilizacion = @Costo_Fertilizacion,
                       Fecha_Aplicacion = @Fecha_Aplicacion
                 WHERE ID_Fertilizacion = @ID_Fertilizacion;
        END;

        IF @ID_Agricultor IS NOT NULL AND @ID_Agricultor > 0
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.Agricultor_Siembra WITH (UPDLOCK, HOLDLOCK)
                       WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo)
                UPDATE dbo.Agricultor_Siembra
                   SET Fecha_Asignacion = GETDATE(),
                       Notas = NULLIF(LTRIM(RTRIM(@Notas)), '')
                 WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo;
            ELSE
                INSERT dbo.Agricultor_Siembra
                    (ID_Agricultor, ID_Cultivo, Fecha_Asignacion, Notas)
                VALUES
                    (@ID_Agricultor, @ID_Cultivo, GETDATE(),
                     NULLIF(LTRIM(RTRIM(@Notas)), ''));
        END;

        UPDATE terreno
           SET Estado = CASE WHEN EXISTS
               (SELECT 1 FROM dbo.Siembra siembra
                WHERE siembra.ID_Terreno = terreno.ID_Terreno
                  AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                      ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado')))
               THEN 'En uso' ELSE 'Disponible' END
        FROM dbo.Terrenos terreno
        WHERE terreno.ID_Terreno IN (@ID_Terreno, @ID_Terreno_Anterior)
          AND terreno.Estado <> 'Inactivo';

        COMMIT TRANSACTION;
        SELECT CAST(1 AS BIT);
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

DROP PROCEDURE IF EXISTS dbo.Registrar_Siembra_Agroclimatica_Atomica;
DROP PROCEDURE IF EXISTS dbo.Actualizar_Siembra_Agroclimatica_Atomica;
GO

IF DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app') IS NOT NULL
BEGIN
    GRANT EXECUTE ON OBJECT::dbo.Insertar_Terreno TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Actualizar_Terreno TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Registrar_Siembra_Atomica TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Actualizar_Siembra_Atomica TO db_sistema_agricola_app;
END;
GO

IF COL_LENGTH(N'dbo.Terrenos', N'Provincia') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Zona_Agroclimatica') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Precipitacion_Anual') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Disponibilidad_Riego') IS NULL OR
   COL_LENGTH(N'dbo.Siembra', N'Variedad_Cultivo') IS NULL OR
   OBJECT_ID(N'dbo.Registrar_Siembra_Atomica', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Actualizar_Siembra_Atomica', N'P') IS NULL
    THROW 51944, 'La migracion 010 no completo todos sus objetos.', 1;

MERGE dbo.Esquema_Version AS destino
USING (SELECT 10 AS Version, 'Reglas agroclimaticas y variedades' AS Nombre) AS origen
ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 010 aplicada correctamente.';
GO

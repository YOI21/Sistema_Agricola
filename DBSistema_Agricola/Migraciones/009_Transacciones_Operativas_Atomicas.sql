USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) <> 8
    THROW 51800, 'La migracion 009 requiere que la base este exactamente en la version 8.', 1;

IF OBJECT_ID(N'dbo.Siembra', N'U') IS NULL OR
   OBJECT_ID(N'dbo.Terrenos', N'U') IS NULL OR
   OBJECT_ID(N'dbo.Cosecha', N'U') IS NULL
    THROW 51801, 'La estructura agricola requerida no esta completa.', 1;
GO

IF EXISTS
(
    SELECT id_cultivo
    FROM dbo.Cosecha
    WHERE id_cultivo IS NOT NULL
    GROUP BY id_cultivo
    HAVING COUNT(*) > 1
)
    THROW 51802, 'Hay cultivos con mas de una cosecha. Corrija los duplicados antes de migrar.', 1;

IF EXISTS
(
    SELECT ID_Agricultor, ID_Cultivo
    FROM dbo.Agricultor_Siembra
    WHERE ID_Agricultor IS NOT NULL AND ID_Cultivo IS NOT NULL
    GROUP BY ID_Agricultor, ID_Cultivo
    HAVING COUNT(*) > 1
)
    THROW 51803, 'Hay asignaciones duplicadas de agricultores y siembras.', 1;
GO

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Siembra')
      AND name = N'IX_Siembra_Terreno_Estado'
)
BEGIN
    CREATE INDEX IX_Siembra_Terreno_Estado
        ON dbo.Siembra (ID_Terreno, Estado_Cultivo)
        INCLUDE (Hectareas_Sembradas);
END;

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Cosecha')
      AND name = N'UX_Cosecha_Cultivo'
)
BEGIN
    CREATE UNIQUE INDEX UX_Cosecha_Cultivo
        ON dbo.Cosecha (id_cultivo)
        WHERE id_cultivo IS NOT NULL;
END;

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Agricultor_Siembra')
      AND name = N'UX_Agricultor_Siembra'
)
BEGIN
    CREATE UNIQUE INDEX UX_Agricultor_Siembra
        ON dbo.Agricultor_Siembra (ID_Agricultor, ID_Cultivo)
        WHERE ID_Agricultor IS NOT NULL AND ID_Cultivo IS NOT NULL;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Siembra_Atomica
    @Nombre_Cultivo VARCHAR(100),
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

        DECLARE @Superficie_Total DECIMAL(10,2);
        DECLARE @Estado_Terreno VARCHAR(20);

        SELECT @Superficie_Total = Superficie_Total,
               @Estado_Terreno = Estado
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
           NOT EXISTS
           (
               SELECT 1 FROM dbo.Agricultor WITH (UPDLOCK, HOLDLOCK)
               WHERE ID_Agricultor = @ID_Agricultor
           )
            THROW 51818, 'El agricultor seleccionado no existe.', 1;

        INSERT dbo.Siembra
            (Nombre_Cultivo, Tipo_Cultivo, Categoria_Cultivo, Estacion_Siembra,
             Cantidad, Tipo_Suelo, Fecha_Plantacion, Fecha_Cosecha,
             Estado_Cultivo, Metodo_Siembra, Profundidad, Humedad_Suelo,
             Costo_Siembra, ID_Terreno, Hectareas_Sembradas)
        VALUES
            (@Nombre_Cultivo, @Tipo_Cultivo, @Categoria_Cultivo, @Estacion_Siembra,
             @Cantidad, @Tipo_Suelo, @Fecha_Plantacion, @Fecha_Cosecha,
             'Activo', NULLIF(LTRIM(RTRIM(@Metodo_Siembra)), ''),
             NULLIF(LTRIM(RTRIM(@Profundidad)), ''), @Humedad_Suelo,
             @Costo_Siembra, @ID_Terreno, @Hectareas_Sembradas);

        DECLARE @ID_Cultivo INT = CONVERT(INT, SCOPE_IDENTITY());

        IF NULLIF(LTRIM(RTRIM(@Tipo_Fertilizante)), '') IS NOT NULL OR
           NULLIF(LTRIM(RTRIM(@Metodo_Fertilizacion)), '') IS NOT NULL OR
           @Cantidad_Fertilizante IS NOT NULL OR
           @Costo_Fertilizacion IS NOT NULL OR
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

        UPDATE dbo.Terrenos
           SET Estado = 'En uso'
         WHERE ID_Terreno = @ID_Terreno
           AND Estado <> 'Inactivo';

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

        DECLARE @ID_Terreno_Anterior INT;
        DECLARE @Estado_Cultivo VARCHAR(50);

        SELECT @ID_Terreno_Anterior = ID_Terreno,
               @Estado_Cultivo = Estado_Cultivo
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51825, 'La siembra seleccionada no existe.', 1;

        DECLARE @Terreno_Bloqueado INT;
        SELECT @Terreno_Bloqueado = ID_Terreno
        FROM dbo.Terrenos WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Terreno IN (@ID_Terreno, @ID_Terreno_Anterior)
        ORDER BY ID_Terreno;

        DECLARE @Superficie_Total DECIMAL(10,2);
        DECLARE @Estado_Terreno VARCHAR(20);
        SELECT @Superficie_Total = Superficie_Total,
               @Estado_Terreno = Estado
        FROM dbo.Terrenos
        WHERE ID_Terreno = @ID_Terreno;

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
           NOT EXISTS
           (
               SELECT 1 FROM dbo.Agricultor WITH (UPDLOCK, HOLDLOCK)
               WHERE ID_Agricultor = @ID_Agricultor
           )
            THROW 51829, 'El agricultor seleccionado no existe.', 1;

        UPDATE dbo.Siembra
           SET Nombre_Cultivo = @Nombre_Cultivo,
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
           @Cantidad_Fertilizante IS NOT NULL OR
           @Costo_Fertilizacion IS NOT NULL OR
           @Fecha_Aplicacion IS NOT NULL
        BEGIN
            DECLARE @ID_Fertilizacion INT;
            SELECT TOP (1) @ID_Fertilizacion = ID_Fertilizacion
            FROM dbo.Fertilizacion WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Cultivo = @ID_Cultivo
            ORDER BY ID_Fertilizacion;

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
            IF EXISTS
            (
                SELECT 1 FROM dbo.Agricultor_Siembra WITH (UPDLOCK, HOLDLOCK)
                WHERE ID_Agricultor = @ID_Agricultor
                  AND ID_Cultivo = @ID_Cultivo
            )
                UPDATE dbo.Agricultor_Siembra
                   SET Fecha_Asignacion = GETDATE(),
                       Notas = NULLIF(LTRIM(RTRIM(@Notas)), '')
                 WHERE ID_Agricultor = @ID_Agricultor
                   AND ID_Cultivo = @ID_Cultivo;
            ELSE
                INSERT dbo.Agricultor_Siembra
                    (ID_Agricultor, ID_Cultivo, Fecha_Asignacion, Notas)
                VALUES
                    (@ID_Agricultor, @ID_Cultivo, GETDATE(),
                     NULLIF(LTRIM(RTRIM(@Notas)), ''));
        END;

        UPDATE terreno
           SET Estado = CASE
               WHEN EXISTS
               (
                   SELECT 1 FROM dbo.Siembra siembra
                   WHERE siembra.ID_Terreno = terreno.ID_Terreno
                     AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                         ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'))
               ) THEN 'En uso' ELSE 'Disponible' END
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

CREATE OR ALTER PROCEDURE dbo.Finalizar_Siembra_Atomica
    @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ID_Terreno INT;
        SELECT @ID_Terreno = ID_Terreno
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51830, 'El cultivo seleccionado no existe.', 1;

        IF EXISTS
        (
            SELECT 1 FROM dbo.Cosecha WITH (UPDLOCK, HOLDLOCK)
            WHERE id_cultivo = @ID_Cultivo
        )
            UPDATE dbo.Cosecha
               SET Estado_Cosecha = 'Completada'
             WHERE id_cultivo = @ID_Cultivo;
        ELSE
            INSERT dbo.Cosecha (Estado_Cosecha, id_cultivo)
            VALUES ('Completada', @ID_Cultivo);

        UPDATE dbo.Siembra
           SET Estado_Cultivo = 'Cosechado'
         WHERE ID_Cultivo = @ID_Cultivo;

        UPDATE terreno
           SET Estado = CASE
               WHEN EXISTS
               (
                   SELECT 1 FROM dbo.Siembra siembra
                   WHERE siembra.ID_Terreno = terreno.ID_Terreno
                     AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                         ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'))
               ) THEN 'En uso' ELSE 'Disponible' END
        FROM dbo.Terrenos terreno
        WHERE terreno.ID_Terreno = @ID_Terreno
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

CREATE OR ALTER PROCEDURE dbo.Registrar_Cosecha_Cultivo_Segura
    @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.Finalizar_Siembra_Atomica @ID_Cultivo = @ID_Cultivo;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Cambiar_Estado_Cultivo
    @ID_Cultivo INT,
    @Estado_Cultivo VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ID_Terreno INT;
        SELECT @ID_Terreno = ID_Terreno
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51831, 'El cultivo seleccionado no existe.', 1;

        UPDATE dbo.Siembra
           SET Estado_Cultivo = @Estado_Cultivo
         WHERE ID_Cultivo = @ID_Cultivo;

        IF @Estado_Cultivo = 'Cosechado'
        BEGIN
            IF EXISTS
            (
                SELECT 1 FROM dbo.Cosecha WITH (UPDLOCK, HOLDLOCK)
                WHERE id_cultivo = @ID_Cultivo
            )
                UPDATE dbo.Cosecha
                   SET Estado_Cosecha = 'Completada'
                 WHERE id_cultivo = @ID_Cultivo;
            ELSE
                INSERT dbo.Cosecha (Estado_Cosecha, id_cultivo)
                VALUES ('Completada', @ID_Cultivo);
        END;

        UPDATE terreno
           SET Estado = CASE
               WHEN EXISTS
               (
                   SELECT 1 FROM dbo.Siembra siembra
                   WHERE siembra.ID_Terreno = terreno.ID_Terreno
                     AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                         ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'))
               ) THEN 'En uso' ELSE 'Disponible' END
        FROM dbo.Terrenos terreno
        WHERE terreno.ID_Terreno = @ID_Terreno
          AND terreno.Estado <> 'Inactivo';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Eliminar_Siembra
    @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ID_Terreno INT;
        SELECT @ID_Terreno = ID_Terreno
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51832, 'La siembra seleccionada no existe.', 1;

        DELETE dbo.Siembra WHERE ID_Cultivo = @ID_Cultivo;

        UPDATE terreno
           SET Estado = CASE
               WHEN EXISTS
               (
                   SELECT 1 FROM dbo.Siembra siembra
                   WHERE siembra.ID_Terreno = terreno.ID_Terreno
                     AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                         ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'))
               ) THEN 'En uso' ELSE 'Disponible' END
        FROM dbo.Terrenos terreno
        WHERE terreno.ID_Terreno = @ID_Terreno
          AND terreno.Estado <> 'Inactivo';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Guardar_Cosecha_Segura
    @ID_Cultivo INT,
    @Estado_Cosecha VARCHAR(25),
    @Cantidad_Cosechada DECIMAL(18,3),
    @Rendimiento DECIMAL(18,3),
    @Calidad DECIMAL(10,2),
    @Observaciones VARCHAR(200),
    @Fecha_Cosecha DATE,
    @Costo_Cosecha DECIMAL(18,2),
    @Precio_Venta DECIMAL(18,2),
    @Beneficio DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ID_Terreno INT;
        SELECT @ID_Terreno = ID_Terreno
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        IF @@ROWCOUNT = 0
            THROW 51833, 'El cultivo seleccionado no existe.', 1;

        DECLARE @ID_Cosecha INT;
        SELECT @ID_Cosecha = ID_Cosecha
        FROM dbo.Cosecha WITH (UPDLOCK, HOLDLOCK)
        WHERE id_cultivo = @ID_Cultivo;

        DECLARE @Resultado VARCHAR(20);
        IF @ID_Cosecha IS NULL
        BEGIN
            INSERT dbo.Cosecha
                (id_cultivo, Estado_Cosecha, Cantidad_Cosechada, Rendimiento,
                 Calidad, Observaciones, Fecha_Cosecha, Costo_Cosecha,
                 Precio_Venta, Beneficio)
            VALUES
                (@ID_Cultivo, @Estado_Cosecha, @Cantidad_Cosechada, @Rendimiento,
                 @Calidad, @Observaciones, @Fecha_Cosecha, @Costo_Cosecha,
                 @Precio_Venta, @Beneficio);
            SET @Resultado = 'Insertada';
        END
        ELSE
        BEGIN
            UPDATE dbo.Cosecha
               SET Estado_Cosecha = @Estado_Cosecha,
                   Cantidad_Cosechada = @Cantidad_Cosechada,
                   Rendimiento = @Rendimiento,
                   Calidad = @Calidad,
                   Observaciones = @Observaciones,
                   Fecha_Cosecha = @Fecha_Cosecha,
                   Costo_Cosecha = @Costo_Cosecha,
                   Precio_Venta = @Precio_Venta,
                   Beneficio = @Beneficio
             WHERE ID_Cosecha = @ID_Cosecha;
            SET @Resultado = 'Actualizada';
        END;

        UPDATE dbo.Siembra
           SET Estado_Cultivo = 'Cosechado'
         WHERE ID_Cultivo = @ID_Cultivo;

        UPDATE terreno
           SET Estado = CASE
               WHEN EXISTS
               (
                   SELECT 1 FROM dbo.Siembra siembra
                   WHERE siembra.ID_Terreno = terreno.ID_Terreno
                     AND (siembra.Estado_Cultivo IS NULL OR siembra.Estado_Cultivo NOT IN
                         ('Cosechado', 'Terminado', 'Finalizado', 'Cancelado'))
               ) THEN 'En uso' ELSE 'Disponible' END
        FROM dbo.Terrenos terreno
        WHERE terreno.ID_Terreno = @ID_Terreno
          AND terreno.Estado <> 'Inactivo';

        COMMIT TRANSACTION;
        SELECT @Resultado;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Eliminar_Cosecha_Segura
    @ID_Cosecha INT,
    @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ID_Terreno INT;
        SELECT @ID_Terreno = ID_Terreno
        FROM dbo.Siembra WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Cultivo = @ID_Cultivo;

        DELETE dbo.Cosecha
        WHERE ID_Cosecha = @ID_Cosecha AND id_cultivo = @ID_Cultivo;

        DECLARE @Eliminada BIT = CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
        IF @Eliminada = 1
        BEGIN
            UPDATE dbo.Siembra
               SET Estado_Cultivo = 'Activo'
             WHERE ID_Cultivo = @ID_Cultivo;

            UPDATE dbo.Terrenos
               SET Estado = 'En uso'
             WHERE ID_Terreno = @ID_Terreno
               AND Estado <> 'Inactivo';
        END;

        COMMIT TRANSACTION;
        SELECT @Eliminada;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Producto_Inventario
    @Codigo VARCHAR(30),
    @Nombre VARCHAR(100),
    @Tipo VARCHAR(50),
    @Unidad_Medida VARCHAR(20),
    @Costo DECIMAL(14,2),
    @Stock_Minimo DECIMAL(14,3),
    @Ubicacion VARCHAR(80) = NULL,
    @Fecha_Vencimiento DATE = NULL,
    @StockInicial DECIMAL(14,3) = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Costo < 0 OR @Stock_Minimo < 0 OR @StockInicial < 0
            THROW 51840, 'El costo y las cantidades de inventario no pueden ser negativos.', 1;

        IF EXISTS
        (
            SELECT 1 FROM dbo.Insumos WITH (UPDLOCK, HOLDLOCK)
            WHERE Codigo = @Codigo
        )
            THROW 51100, 'Ya existe un producto con ese codigo.', 1;

        IF EXISTS
        (
            SELECT 1 FROM dbo.Insumos WITH (UPDLOCK, HOLDLOCK)
            WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(LTRIM(RTRIM(@Nombre)))
              AND LOWER(LTRIM(RTRIM(Tipo))) = LOWER(LTRIM(RTRIM(@Tipo)))
              AND Estado = 'Activo'
        )
            THROW 51101, 'Ya existe un producto activo con el mismo nombre y tipo.', 1;

        INSERT dbo.Insumos
            (Codigo, Nombre, Tipo, Costo, Fecha, Cantidad, Unidad_Medida,
             Stock_Minimo, Ubicacion, Fecha_Vencimiento, Estado)
        VALUES
            (@Codigo, @Nombre, @Tipo, @Costo, GETDATE(), @StockInicial,
             @Unidad_Medida, @Stock_Minimo, @Ubicacion, @Fecha_Vencimiento, 'Activo');

        DECLARE @Id_Insumo INT = CONVERT(INT, SCOPE_IDENTITY());

        IF @StockInicial > 0
        BEGIN
            INSERT dbo.Movimiento_Inventario
                (Id_Insumo, Tipo_Movimiento, Cantidad, Costo_Unitario,
                 Fecha_Movimiento, Motivo, Referencia)
            VALUES
                (@Id_Insumo, 'Entrada', @StockInicial, @Costo,
                 GETDATE(), 'Stock inicial', NULL);

            DECLARE @ID_MovimientoInicial INT = CONVERT(INT, SCOPE_IDENTITY());

            IF OBJECT_ID(N'dbo.Movimiento_Financiero', N'U') IS NOT NULL AND @Costo > 0
                INSERT dbo.Movimiento_Financiero
                    (Tipo, Categoria, Descripcion, Monto, Fecha,
                     Metodo_Pago, Estado, Referencia)
                VALUES
                    ('Gasto', 'Insumos', LEFT('Compra inicial de ' + @Nombre, 200),
                     @StockInicial * @Costo, GETDATE(), 'Sistema', 'Pagado',
                     'INVENTARIO-' + CONVERT(VARCHAR(20), @ID_MovimientoInicial));
        END;

        COMMIT TRANSACTION;
        SELECT @Id_Insumo;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Movimiento_Inventario
    @Id_Insumo INT,
    @Tipo_Movimiento VARCHAR(10),
    @Cantidad DECIMAL(14,3),
    @Costo_Unitario DECIMAL(14,2),
    @Fecha_Movimiento DATE,
    @Motivo VARCHAR(150),
    @Referencia VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Tipo_Movimiento NOT IN ('Entrada', 'Salida')
            THROW 51841, 'El tipo de movimiento de inventario no es valido.', 1;

        IF @Cantidad <= 0 OR @Costo_Unitario < 0
            THROW 51842, 'La cantidad debe ser positiva y el costo no puede ser negativo.', 1;

        DECLARE @StockActual DECIMAL(14,3);
        DECLARE @CostoActual DECIMAL(14,2);
        DECLARE @Nombre VARCHAR(100);
        DECLARE @Estado VARCHAR(15);

        SELECT @StockActual = Cantidad,
               @CostoActual = Costo,
               @Nombre = Nombre,
               @Estado = Estado
        FROM dbo.Insumos WITH (UPDLOCK, HOLDLOCK)
        WHERE Id_Insumo = @Id_Insumo;

        IF @StockActual IS NULL
            THROW 51102, 'El producto seleccionado no existe.', 1;

        IF @Estado <> 'Activo'
            THROW 51103, 'No se pueden registrar movimientos para un producto inactivo.', 1;

        IF @Tipo_Movimiento = 'Salida' AND @Cantidad > @StockActual
            THROW 51104, 'La salida supera la existencia disponible.', 1;

        IF @Tipo_Movimiento = 'Entrada'
        BEGIN
            DECLARE @NuevoCosto DECIMAL(14,2) =
                CASE WHEN @StockActual + @Cantidad = 0 THEN @Costo_Unitario
                     ELSE ((@StockActual * @CostoActual) + (@Cantidad * @Costo_Unitario))
                          / (@StockActual + @Cantidad)
                END;

            UPDATE dbo.Insumos
               SET Cantidad = Cantidad + @Cantidad,
                   Costo = @NuevoCosto,
                   Fecha = @Fecha_Movimiento
             WHERE Id_Insumo = @Id_Insumo;
        END
        ELSE
            UPDATE dbo.Insumos
               SET Cantidad = Cantidad - @Cantidad,
                   Fecha = @Fecha_Movimiento
             WHERE Id_Insumo = @Id_Insumo;

        INSERT dbo.Movimiento_Inventario
            (Id_Insumo, Tipo_Movimiento, Cantidad, Costo_Unitario,
             Fecha_Movimiento, Motivo, Referencia)
        VALUES
            (@Id_Insumo, @Tipo_Movimiento, @Cantidad,
             CASE WHEN @Tipo_Movimiento = 'Entrada' THEN @Costo_Unitario ELSE @CostoActual END,
             @Fecha_Movimiento, @Motivo, @Referencia);

        DECLARE @ID_Movimiento INT = CONVERT(INT, SCOPE_IDENTITY());

        IF @Tipo_Movimiento = 'Entrada' AND
           OBJECT_ID(N'dbo.Movimiento_Financiero', N'U') IS NOT NULL AND
           @Costo_Unitario > 0
            INSERT dbo.Movimiento_Financiero
                (Tipo, Categoria, Descripcion, Monto, Fecha,
                 Metodo_Pago, Estado, Referencia)
            VALUES
                ('Gasto', 'Insumos', LEFT('Compra de ' + @Nombre, 200),
                 @Cantidad * @Costo_Unitario, @Fecha_Movimiento,
                 'Sistema', 'Pagado',
                 'INVENTARIO-' + CONVERT(VARCHAR(20), @ID_Movimiento));

        COMMIT TRANSACTION;
        SELECT @ID_Movimiento;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Produccion
    @ID_Cosecha INT,
    @Fecha_Produccion DATE,
    @Unidad_Medida VARCHAR(20),
    @Cantidad_Bruta DECIMAL(14,3),
    @Merma DECIMAL(14,3),
    @Precio_Unitario DECIMAL(14,2),
    @Costo_Postcosecha DECIMAL(14,2),
    @Destino VARCHAR(80) = NULL,
    @Estado VARCHAR(20),
    @Observaciones VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.Cosecha WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Cosecha = @ID_Cosecha
        )
            THROW 51400, 'La cosecha seleccionada no existe.', 1;

        IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
            THROW 51401, 'Estado de produccion no valido.', 1;

        IF @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma > @Cantidad_Bruta OR
           @Precio_Unitario < 0 OR @Costo_Postcosecha < 0
            THROW 51402, 'Las cantidades o costos de produccion no son validos.', 1;

        DECLARE @Cantidad_Neta DECIMAL(14,3) = @Cantidad_Bruta - @Merma;
        DECLARE @Ingreso_Estimado DECIMAL(14,2) = @Cantidad_Neta * @Precio_Unitario;
        DECLARE @Beneficio_Neto DECIMAL(14,2) = @Ingreso_Estimado - @Costo_Postcosecha;

        INSERT dbo.Produccion
            (ID_Cosecha, Fecha_Produccion, Unidad_Medida, Cantidad_Bruta,
             Merma, Cantidad_Neta, Precio_Unitario, Ingreso_Estimado,
             Costo_Postcosecha, Beneficio_Neto, Destino, Estado, Observaciones)
        VALUES
            (@ID_Cosecha, @Fecha_Produccion, @Unidad_Medida, @Cantidad_Bruta,
             @Merma, @Cantidad_Neta, @Precio_Unitario, @Ingreso_Estimado,
             @Costo_Postcosecha, @Beneficio_Neto, @Destino, @Estado, @Observaciones);

        DECLARE @ID_Produccion INT = CONVERT(INT, SCOPE_IDENTITY());
        COMMIT TRANSACTION;
        SELECT @ID_Produccion;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_Produccion
    @ID_Produccion INT,
    @ID_Cosecha INT,
    @Fecha_Produccion DATE,
    @Unidad_Medida VARCHAR(20),
    @Cantidad_Bruta DECIMAL(14,3),
    @Merma DECIMAL(14,3),
    @Precio_Unitario DECIMAL(14,2),
    @Costo_Postcosecha DECIMAL(14,2),
    @Destino VARCHAR(80) = NULL,
    @Estado VARCHAR(20),
    @Observaciones VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.Produccion WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Produccion = @ID_Produccion
        )
            THROW 51403, 'La produccion seleccionada no existe.', 1;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.Cosecha WITH (UPDLOCK, HOLDLOCK)
            WHERE ID_Cosecha = @ID_Cosecha
        )
            THROW 51400, 'La cosecha seleccionada no existe.', 1;

        IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
            THROW 51401, 'Estado de produccion no valido.', 1;

        IF @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma > @Cantidad_Bruta OR
           @Precio_Unitario < 0 OR @Costo_Postcosecha < 0
            THROW 51402, 'Las cantidades o costos de produccion no son validos.', 1;

        DECLARE @Cantidad_Neta DECIMAL(14,3) = @Cantidad_Bruta - @Merma;
        DECLARE @Ingreso_Estimado DECIMAL(14,2) = @Cantidad_Neta * @Precio_Unitario;
        DECLARE @Beneficio_Neto DECIMAL(14,2) = @Ingreso_Estimado - @Costo_Postcosecha;

        UPDATE dbo.Produccion
           SET ID_Cosecha = @ID_Cosecha,
               Fecha_Produccion = @Fecha_Produccion,
               Unidad_Medida = @Unidad_Medida,
               Cantidad_Bruta = @Cantidad_Bruta,
               Merma = @Merma,
               Cantidad_Neta = @Cantidad_Neta,
               Precio_Unitario = @Precio_Unitario,
               Ingreso_Estimado = @Ingreso_Estimado,
               Costo_Postcosecha = @Costo_Postcosecha,
               Beneficio_Neto = @Beneficio_Neto,
               Destino = @Destino,
               Estado = @Estado,
               Observaciones = @Observaciones
         WHERE ID_Produccion = @ID_Produccion;

        COMMIT TRANSACTION;
        SELECT CAST(1 AS BIT);
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'db_sistema_agricola_app')
    CREATE ROLE db_sistema_agricola_app;
GO

GRANT EXECUTE ON OBJECT::dbo.Registrar_Siembra_Atomica TO db_sistema_agricola_app;
GRANT EXECUTE ON OBJECT::dbo.Actualizar_Siembra_Atomica TO db_sistema_agricola_app;
GRANT EXECUTE ON OBJECT::dbo.Finalizar_Siembra_Atomica TO db_sistema_agricola_app;

REVOKE EXECUTE ON OBJECT::dbo.Insertar_Siembra_Segura FROM db_sistema_agricola_app;
REVOKE EXECUTE ON OBJECT::dbo.Actualizar_Siembra_Segura FROM db_sistema_agricola_app;
REVOKE EXECUTE ON OBJECT::dbo.Insertar_Fertilizacion FROM db_sistema_agricola_app;
REVOKE EXECUTE ON OBJECT::dbo.Asignar_Agricultor_Siembra_Segura FROM db_sistema_agricola_app;
REVOKE EXECUTE ON OBJECT::dbo.Registrar_Cosecha_Cultivo_Segura FROM db_sistema_agricola_app;
GO

MERGE dbo.Esquema_Version AS destino
USING
(
    SELECT 9 AS Version,
           'Transacciones atomicas para operaciones agricolas' AS Nombre
) AS origen
ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

IF (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) <> 9
    THROW 51890, 'La version 9 no quedo registrada.', 1;

IF OBJECT_ID(N'dbo.Registrar_Siembra_Atomica', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Actualizar_Siembra_Atomica', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Finalizar_Siembra_Atomica', N'P') IS NULL
    THROW 51891, 'No se crearon todos los procedimientos atomicos.', 1;
GO

PRINT 'Migracion 009 aplicada: transacciones operativas atomicas.';
GO

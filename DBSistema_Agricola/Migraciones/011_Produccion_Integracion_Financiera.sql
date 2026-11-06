USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) NOT IN (10, 11)
    THROW 52000, 'La migracion 011 requiere una base en version 10 o una version 11 que deba repararse.', 1;

IF OBJECT_ID(N'dbo.Produccion', N'U') IS NULL OR
   OBJECT_ID(N'dbo.Movimiento_Financiero', N'U') IS NULL
    THROW 52001, 'No existen las tablas de produccion y finanzas requeridas.', 1;
GO

IF COL_LENGTH(N'dbo.Produccion', N'Costo_Base_Asignado') IS NULL
    ALTER TABLE dbo.Produccion ADD Costo_Base_Asignado DECIMAL(14,2) NOT NULL
        CONSTRAINT DF_Produccion_CostoBase DEFAULT (0) WITH VALUES;

IF COL_LENGTH(N'dbo.Produccion', N'Fecha_Venta') IS NULL
    ALTER TABLE dbo.Produccion ADD Fecha_Venta DATE NULL;

IF COL_LENGTH(N'dbo.Produccion', N'Comprador') IS NULL
    ALTER TABLE dbo.Produccion ADD Comprador VARCHAR(120) NULL;

IF COL_LENGTH(N'dbo.Produccion', N'Metodo_Pago') IS NULL
    ALTER TABLE dbo.Produccion ADD Metodo_Pago VARCHAR(30) NULL;

IF COL_LENGTH(N'dbo.Produccion', N'Estado_Cobro') IS NULL
    ALTER TABLE dbo.Produccion ADD Estado_Cobro VARCHAR(15) NOT NULL
        CONSTRAINT DF_Produccion_EstadoCobro DEFAULT ('Pendiente') WITH VALUES;

IF COL_LENGTH(N'dbo.Produccion', N'Referencia_Venta') IS NULL
    ALTER TABLE dbo.Produccion ADD Referencia_Venta VARCHAR(80) NULL;

IF COL_LENGTH(N'dbo.Movimiento_Financiero', N'Origen') IS NULL
    ALTER TABLE dbo.Movimiento_Financiero ADD Origen VARCHAR(30) NOT NULL
        CONSTRAINT DF_MovimientoFinanciero_Origen DEFAULT ('Manual') WITH VALUES;

IF COL_LENGTH(N'dbo.Movimiento_Financiero', N'ID_Origen') IS NULL
    ALTER TABLE dbo.Movimiento_Financiero ADD ID_Origen INT NULL;
GO

UPDATE dbo.Movimiento_Financiero
   SET Origen =
       CASE
           WHEN Referencia LIKE 'INVENTARIO-%' THEN 'Inventario'
           WHEN Referencia LIKE 'MAQUINARIA-%' THEN 'Maquinaria'
           WHEN Referencia LIKE 'MANT-MAQ-%' OR Referencia LIKE 'MANT-EQ-%' THEN 'Mantenimiento'
           ELSE Origen
       END
 WHERE Origen = 'Manual'
   AND Referencia IS NOT NULL;

UPDATE dbo.Produccion
   SET Fecha_Venta = CASE WHEN Estado = 'Vendida' THEN ISNULL(Fecha_Venta, Fecha_Produccion) ELSE Fecha_Venta END,
       Metodo_Pago = CASE WHEN Estado = 'Vendida' THEN ISNULL(Metodo_Pago, 'Sistema') ELSE Metodo_Pago END,
       Estado_Cobro = CASE WHEN Estado = 'Vendida' THEN 'Pagado' ELSE Estado_Cobro END;
GO

IF OBJECT_ID(N'dbo.CK_Produccion_EstadoCobro', N'C') IS NULL
    ALTER TABLE dbo.Produccion WITH CHECK ADD CONSTRAINT CK_Produccion_EstadoCobro
        CHECK (Estado_Cobro IN ('Pendiente', 'Pagado'));
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Movimiento_Financiero')
      AND name = N'UX_MovimientoFinanciero_Origen'
)
    CREATE UNIQUE INDEX UX_MovimientoFinanciero_Origen
        ON dbo.Movimiento_Financiero (Origen, ID_Origen, Tipo)
        WHERE ID_Origen IS NOT NULL;
GO

CREATE OR ALTER PROCEDURE dbo.Listar_Cosechas_Para_Produccion
    @ID_Produccion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.ID_Cosecha,
        C.id_cultivo AS ID_Cultivo,
        S.Nombre_Cultivo,
        S.Tipo_Cultivo,
        S.Categoria_Cultivo,
        CONVERT(DECIMAL(14,3), ISNULL(C.Cantidad_Cosechada, 0)) AS Cantidad_Cosechada,
        CONVERT(DECIMAL(14,3), ISNULL(asignada.Cantidad_Asignada, 0)) AS Cantidad_Asignada,
        CONVERT(DECIMAL(14,3),
            ISNULL(C.Cantidad_Cosechada, 0) - ISNULL(asignada.Cantidad_Asignada, 0)) AS Cantidad_Disponible,
        CONVERT(DECIMAL(14,2),
            ISNULL(S.Costo_Siembra, 0) +
            ISNULL(C.Costo_Cosecha, 0) +
            ISNULL(fertilizacion.Costo_Fertilizacion, 0)) AS Costo_Base_Total,
        ISNULL(C.Rendimiento, 0) AS Rendimiento,
        ISNULL(C.Calidad, 0) AS Calidad,
        ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) AS Fecha_Cosecha,
        CONVERT(VARCHAR(20), C.ID_Cosecha) + ' - ' +
            ISNULL(S.Nombre_Cultivo, 'Cultivo') + ' | disponible: ' +
            CONVERT(VARCHAR(30), CONVERT(DECIMAL(14,3),
                ISNULL(C.Cantidad_Cosechada, 0) - ISNULL(asignada.Cantidad_Asignada, 0))) AS Cosecha
    FROM dbo.Cosecha C
    INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
    OUTER APPLY
    (
        SELECT SUM(P.Cantidad_Bruta) AS Cantidad_Asignada
        FROM dbo.Produccion P
        WHERE P.ID_Cosecha = C.ID_Cosecha
          AND P.Estado <> 'Cancelada'
          AND (@ID_Produccion IS NULL OR P.ID_Produccion <> @ID_Produccion)
    ) asignada
    OUTER APPLY
    (
        SELECT SUM(F.Costo_Fertilizacion) AS Costo_Fertilizacion
        FROM dbo.Fertilizacion F
        WHERE F.ID_Cultivo = C.id_cultivo
    ) fertilizacion
    WHERE ISNULL(C.Cantidad_Cosechada, 0) > 0
      AND C.Estado_Cosecha <> 'Cancelada'
      AND
      (
          ISNULL(C.Cantidad_Cosechada, 0) - ISNULL(asignada.Cantidad_Asignada, 0) > 0
          OR EXISTS
             (
                 SELECT 1
                 FROM dbo.Produccion actual
                 WHERE actual.ID_Produccion = @ID_Produccion
                   AND actual.ID_Cosecha = C.ID_Cosecha
             )
      )
    ORDER BY ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) DESC, C.ID_Cosecha DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Listar_Produccion
    @FechaDesde DATE,
    @FechaHasta DATE,
    @Busqueda VARCHAR(100) = NULL,
    @Estado VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.ID_Produccion AS [ID],
        P.ID_Cosecha AS [ID Cosecha],
        S.Nombre_Cultivo AS [Cultivo],
        S.Tipo_Cultivo AS [Tipo],
        S.Categoria_Cultivo AS [Categoria],
        P.Fecha_Produccion AS [Fecha],
        ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) AS [Fecha cosecha],
        P.Unidad_Medida AS [Unidad],
        CONVERT(DECIMAL(14,3), ISNULL(C.Cantidad_Cosechada, 0)) AS [Cantidad cosechada],
        P.Cantidad_Bruta AS [Cantidad bruta],
        P.Merma,
        P.Cantidad_Neta AS [Cantidad neta],
        ISNULL(C.Rendimiento, 0) AS [Rendimiento %],
        ISNULL(C.Calidad, 0) AS [Calidad],
        P.Precio_Unitario AS [Precio unitario],
        P.Ingreso_Estimado AS [Valor produccion],
        P.Costo_Base_Asignado AS [Costo base],
        P.Costo_Postcosecha AS [Costo postcosecha],
        P.Costo_Base_Asignado + P.Costo_Postcosecha AS [Costo total],
        P.Beneficio_Neto AS [Beneficio neto],
        ISNULL(P.Destino, '') AS [Destino],
        P.Estado,
        P.Fecha_Venta AS [Fecha venta],
        ISNULL(P.Comprador, '') AS [Comprador],
        ISNULL(P.Metodo_Pago, '') AS [Metodo],
        P.Estado_Cobro AS [Estado cobro],
        ISNULL(P.Referencia_Venta, '') AS [Referencia venta],
        ISNULL(P.Observaciones, '') AS [Observaciones]
    FROM dbo.Produccion P
    INNER JOIN dbo.Cosecha C ON C.ID_Cosecha = P.ID_Cosecha
    INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
    WHERE P.Fecha_Produccion BETWEEN @FechaDesde AND @FechaHasta
      AND (@Estado IS NULL OR P.Estado = @Estado)
      AND
      (
          @Busqueda IS NULL
          OR S.Nombre_Cultivo LIKE '%' + @Busqueda + '%'
          OR S.Tipo_Cultivo LIKE '%' + @Busqueda + '%'
          OR S.Categoria_Cultivo LIKE '%' + @Busqueda + '%'
          OR P.Destino LIKE '%' + @Busqueda + '%'
          OR P.Comprador LIKE '%' + @Busqueda + '%'
          OR P.Referencia_Venta LIKE '%' + @Busqueda + '%'
          OR P.Observaciones LIKE '%' + @Busqueda + '%'
      )
    ORDER BY P.Fecha_Produccion DESC, P.ID_Produccion DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Sincronizar_Finanzas_Produccion
    @ID_Produccion INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @Estado VARCHAR(20),
        @Cultivo VARCHAR(100),
        @FechaProduccion DATE,
        @FechaVenta DATE,
        @CostoPostcosecha DECIMAL(14,2),
        @Ingreso DECIMAL(14,2),
        @Comprador VARCHAR(120),
        @MetodoPago VARCHAR(30),
        @EstadoCobro VARCHAR(15);

    SELECT
        @Estado = P.Estado,
        @Cultivo = S.Nombre_Cultivo,
        @FechaProduccion = P.Fecha_Produccion,
        @FechaVenta = P.Fecha_Venta,
        @CostoPostcosecha = P.Costo_Postcosecha,
        @Ingreso = P.Ingreso_Estimado,
        @Comprador = P.Comprador,
        @MetodoPago = P.Metodo_Pago,
        @EstadoCobro = P.Estado_Cobro
    FROM dbo.Produccion P
    INNER JOIN dbo.Cosecha C ON C.ID_Cosecha = P.ID_Cosecha
    INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
    WHERE P.ID_Produccion = @ID_Produccion;

    IF @Estado IS NULL
        THROW 52002, 'No se encontro la produccion para sincronizar sus finanzas.', 1;

    IF @Estado = 'Cancelada' OR @CostoPostcosecha <= 0
        DELETE dbo.Movimiento_Financiero
         WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Gasto';
    ELSE IF EXISTS
    (
        SELECT 1 FROM dbo.Movimiento_Financiero WITH (UPDLOCK, HOLDLOCK)
        WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Gasto'
    )
        UPDATE dbo.Movimiento_Financiero
           SET Categoria = 'Postcosecha',
               Descripcion = LEFT('Costo postcosecha de ' + ISNULL(@Cultivo, 'cultivo'), 200),
               Monto = @CostoPostcosecha,
               Fecha = @FechaProduccion,
               Metodo_Pago = 'Sistema',
               Estado = 'Pagado',
               Referencia = 'PROD-GASTO-' + CONVERT(VARCHAR(20), @ID_Produccion)
         WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Gasto';
    ELSE
        INSERT dbo.Movimiento_Financiero
            (Tipo, Categoria, Descripcion, Monto, Fecha, Metodo_Pago,
             Estado, Referencia, Origen, ID_Origen)
        VALUES
            ('Gasto', 'Postcosecha',
             LEFT('Costo postcosecha de ' + ISNULL(@Cultivo, 'cultivo'), 200),
             @CostoPostcosecha, @FechaProduccion, 'Sistema', 'Pagado',
             'PROD-GASTO-' + CONVERT(VARCHAR(20), @ID_Produccion),
             'Produccion', @ID_Produccion);

    IF @Estado <> 'Vendida' OR @Ingreso <= 0
        DELETE dbo.Movimiento_Financiero
         WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Ingreso';
    ELSE IF EXISTS
    (
        SELECT 1 FROM dbo.Movimiento_Financiero WITH (UPDLOCK, HOLDLOCK)
        WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Ingreso'
    )
        UPDATE dbo.Movimiento_Financiero
           SET Categoria = 'Venta de produccion',
               Descripcion = LEFT(
                   'Venta de ' + ISNULL(@Cultivo, 'cultivo') +
                   CASE WHEN NULLIF(LTRIM(RTRIM(@Comprador)), '') IS NULL
                        THEN '' ELSE ' a ' + @Comprador END, 200),
               Monto = @Ingreso,
               Fecha = @FechaVenta,
               Metodo_Pago = @MetodoPago,
               Estado = @EstadoCobro,
               Referencia = 'PROD-VENTA-' + CONVERT(VARCHAR(20), @ID_Produccion)
         WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion AND Tipo = 'Ingreso';
    ELSE
        INSERT dbo.Movimiento_Financiero
            (Tipo, Categoria, Descripcion, Monto, Fecha, Metodo_Pago,
             Estado, Referencia, Origen, ID_Origen)
        VALUES
            ('Ingreso', 'Venta de produccion',
             LEFT(
                 'Venta de ' + ISNULL(@Cultivo, 'cultivo') +
                 CASE WHEN NULLIF(LTRIM(RTRIM(@Comprador)), '') IS NULL
                      THEN '' ELSE ' a ' + @Comprador END, 200),
             @Ingreso, @FechaVenta, @MetodoPago, @EstadoCobro,
             'PROD-VENTA-' + CONVERT(VARCHAR(20), @ID_Produccion),
             'Produccion', @ID_Produccion);
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
    @Observaciones VARCHAR(250) = NULL,
    @Fecha_Venta DATE = NULL,
    @Comprador VARCHAR(120) = NULL,
    @Metodo_Pago VARCHAR(30) = NULL,
    @Estado_Cobro VARCHAR(15) = 'Pendiente',
    @Referencia_Venta VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE
            @CantidadCosechada DECIMAL(14,3),
            @FechaCosecha DATE,
            @CostoCultivo DECIMAL(14,2);

        SELECT
            @CantidadCosechada = ISNULL(C.Cantidad_Cosechada, 0),
            @FechaCosecha = ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha),
            @CostoCultivo = CONVERT(DECIMAL(14,2),
                ISNULL(C.Costo_Cosecha, 0) + ISNULL(S.Costo_Siembra, 0) +
                ISNULL(fertilizacion.Costo_Fertilizacion, 0))
        FROM dbo.Cosecha C WITH (UPDLOCK, HOLDLOCK)
        INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
        OUTER APPLY
        (
            SELECT SUM(F.Costo_Fertilizacion) AS Costo_Fertilizacion
            FROM dbo.Fertilizacion F
            WHERE F.ID_Cultivo = C.id_cultivo
        ) fertilizacion
        WHERE C.ID_Cosecha = @ID_Cosecha
          AND C.Estado_Cosecha <> 'Cancelada';

        IF @CantidadCosechada IS NULL OR @CantidadCosechada <= 0
            THROW 51400, 'La cosecha seleccionada no existe o no tiene cantidad disponible.', 1;

        IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
            THROW 51401, 'Estado de produccion no valido.', 1;

        IF NULLIF(LTRIM(RTRIM(@Unidad_Medida)), '') IS NULL OR
           @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma >= @Cantidad_Bruta OR
           @Precio_Unitario < 0 OR @Costo_Postcosecha < 0
            THROW 51402, 'Las cantidades, la unidad o los costos de produccion no son validos.', 1;

        IF @Fecha_Produccion < @FechaCosecha OR @Fecha_Produccion > CONVERT(DATE, GETDATE())
            THROW 52003, 'La fecha de produccion debe estar entre la cosecha y la fecha actual.', 1;

        DECLARE @CantidadAsignada DECIMAL(14,3) =
            ISNULL((
                SELECT SUM(P.Cantidad_Bruta)
                FROM dbo.Produccion P WITH (UPDLOCK, HOLDLOCK)
                WHERE P.ID_Cosecha = @ID_Cosecha
                  AND P.Estado <> 'Cancelada'
            ), 0);

        IF @Estado <> 'Cancelada' AND @CantidadAsignada + @Cantidad_Bruta > @CantidadCosechada
            THROW 52004, 'La cantidad supera el saldo disponible de la cosecha.', 1;

        IF @Estado = 'Vendida'
        BEGIN
            IF @Precio_Unitario <= 0 OR @Fecha_Venta IS NULL OR
               @Fecha_Venta < @Fecha_Produccion OR @Fecha_Venta > CONVERT(DATE, GETDATE()) OR
               NULLIF(LTRIM(RTRIM(@Comprador)), '') IS NULL OR
               NULLIF(LTRIM(RTRIM(@Metodo_Pago)), '') IS NULL OR
               @Estado_Cobro NOT IN ('Pendiente', 'Pagado')
                THROW 52005, 'Complete correctamente los datos de la venta y el cobro.', 1;
        END
        ELSE
        BEGIN
            SET @Fecha_Venta = NULL;
            SET @Comprador = NULL;
            SET @Metodo_Pago = NULL;
            SET @Estado_Cobro = 'Pendiente';
            SET @Referencia_Venta = NULL;
        END;

        DECLARE @Cantidad_Neta DECIMAL(14,3) = @Cantidad_Bruta - @Merma;
        DECLARE @Ingreso_Estimado DECIMAL(14,2) = @Cantidad_Neta * @Precio_Unitario;
        DECLARE @Costo_Base_Asignado DECIMAL(14,2) =
            CASE WHEN @CantidadCosechada = 0 THEN 0
                 ELSE ROUND(@CostoCultivo * @Cantidad_Bruta / @CantidadCosechada, 2) END;
        DECLARE @Beneficio_Neto DECIMAL(14,2) =
            @Ingreso_Estimado - @Costo_Base_Asignado - @Costo_Postcosecha;

        INSERT dbo.Produccion
            (ID_Cosecha, Fecha_Produccion, Unidad_Medida, Cantidad_Bruta,
             Merma, Cantidad_Neta, Precio_Unitario, Ingreso_Estimado,
             Costo_Base_Asignado, Costo_Postcosecha, Beneficio_Neto,
             Destino, Estado, Observaciones, Fecha_Venta, Comprador,
             Metodo_Pago, Estado_Cobro, Referencia_Venta)
        VALUES
            (@ID_Cosecha, @Fecha_Produccion, LTRIM(RTRIM(@Unidad_Medida)),
             @Cantidad_Bruta, @Merma, @Cantidad_Neta, @Precio_Unitario,
             @Ingreso_Estimado, @Costo_Base_Asignado, @Costo_Postcosecha,
             @Beneficio_Neto, NULLIF(LTRIM(RTRIM(@Destino)), ''), @Estado,
             NULLIF(LTRIM(RTRIM(@Observaciones)), ''), @Fecha_Venta,
             NULLIF(LTRIM(RTRIM(@Comprador)), ''),
             NULLIF(LTRIM(RTRIM(@Metodo_Pago)), ''), @Estado_Cobro,
             NULLIF(LTRIM(RTRIM(@Referencia_Venta)), ''));

        DECLARE @ID_Produccion INT = CONVERT(INT, SCOPE_IDENTITY());
        EXEC dbo.Sincronizar_Finanzas_Produccion @ID_Produccion;

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
    @Observaciones VARCHAR(250) = NULL,
    @Fecha_Venta DATE = NULL,
    @Comprador VARCHAR(120) = NULL,
    @Metodo_Pago VARCHAR(30) = NULL,
    @Estado_Cobro VARCHAR(15) = 'Pendiente',
    @Referencia_Venta VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @EstadoAnterior VARCHAR(20);
        SELECT @EstadoAnterior = Estado
        FROM dbo.Produccion WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Produccion = @ID_Produccion;

        IF @EstadoAnterior IS NULL
            THROW 51403, 'La produccion seleccionada no existe.', 1;

        IF @EstadoAnterior = 'Cancelada' AND @Estado <> 'Cancelada'
            THROW 52009, 'Una produccion cancelada no puede reactivarse.', 1;

        IF @EstadoAnterior = 'Vendida' AND @Estado NOT IN ('Vendida', 'Cancelada')
            THROW 52010, 'Una produccion vendida solo puede corregirse o anularse.', 1;

        DECLARE
            @CantidadCosechada DECIMAL(14,3),
            @FechaCosecha DATE,
            @CostoCultivo DECIMAL(14,2);

        SELECT
            @CantidadCosechada = ISNULL(C.Cantidad_Cosechada, 0),
            @FechaCosecha = ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha),
            @CostoCultivo = CONVERT(DECIMAL(14,2),
                ISNULL(C.Costo_Cosecha, 0) + ISNULL(S.Costo_Siembra, 0) +
                ISNULL(fertilizacion.Costo_Fertilizacion, 0))
        FROM dbo.Cosecha C WITH (UPDLOCK, HOLDLOCK)
        INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
        OUTER APPLY
        (
            SELECT SUM(F.Costo_Fertilizacion) AS Costo_Fertilizacion
            FROM dbo.Fertilizacion F
            WHERE F.ID_Cultivo = C.id_cultivo
        ) fertilizacion
        WHERE C.ID_Cosecha = @ID_Cosecha
          AND C.Estado_Cosecha <> 'Cancelada';

        IF @CantidadCosechada IS NULL OR @CantidadCosechada <= 0
            THROW 51400, 'La cosecha seleccionada no existe o no tiene cantidad disponible.', 1;

        IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
            THROW 51401, 'Estado de produccion no valido.', 1;

        IF NULLIF(LTRIM(RTRIM(@Unidad_Medida)), '') IS NULL OR
           @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma >= @Cantidad_Bruta OR
           @Precio_Unitario < 0 OR @Costo_Postcosecha < 0
            THROW 51402, 'Las cantidades, la unidad o los costos de produccion no son validos.', 1;

        IF @Fecha_Produccion < @FechaCosecha OR @Fecha_Produccion > CONVERT(DATE, GETDATE())
            THROW 52003, 'La fecha de produccion debe estar entre la cosecha y la fecha actual.', 1;

        DECLARE @CantidadAsignada DECIMAL(14,3) =
            ISNULL((
                SELECT SUM(P.Cantidad_Bruta)
                FROM dbo.Produccion P WITH (UPDLOCK, HOLDLOCK)
                WHERE P.ID_Cosecha = @ID_Cosecha
                  AND P.ID_Produccion <> @ID_Produccion
                  AND P.Estado <> 'Cancelada'
            ), 0);

        IF @Estado <> 'Cancelada' AND @CantidadAsignada + @Cantidad_Bruta > @CantidadCosechada
            THROW 52004, 'La cantidad supera el saldo disponible de la cosecha.', 1;

        IF @Estado = 'Vendida'
        BEGIN
            IF @Precio_Unitario <= 0 OR @Fecha_Venta IS NULL OR
               @Fecha_Venta < @Fecha_Produccion OR @Fecha_Venta > CONVERT(DATE, GETDATE()) OR
               NULLIF(LTRIM(RTRIM(@Comprador)), '') IS NULL OR
               NULLIF(LTRIM(RTRIM(@Metodo_Pago)), '') IS NULL OR
               @Estado_Cobro NOT IN ('Pendiente', 'Pagado')
                THROW 52005, 'Complete correctamente los datos de la venta y el cobro.', 1;
        END
        ELSE
        BEGIN
            SET @Fecha_Venta = NULL;
            SET @Comprador = NULL;
            SET @Metodo_Pago = NULL;
            SET @Estado_Cobro = 'Pendiente';
            SET @Referencia_Venta = NULL;
        END;

        DECLARE @Cantidad_Neta DECIMAL(14,3) = @Cantidad_Bruta - @Merma;
        DECLARE @Ingreso_Estimado DECIMAL(14,2) = @Cantidad_Neta * @Precio_Unitario;
        DECLARE @Costo_Base_Asignado DECIMAL(14,2) =
            CASE WHEN @CantidadCosechada = 0 THEN 0
                 ELSE ROUND(@CostoCultivo * @Cantidad_Bruta / @CantidadCosechada, 2) END;
        DECLARE @Beneficio_Neto DECIMAL(14,2) =
            @Ingreso_Estimado - @Costo_Base_Asignado - @Costo_Postcosecha;

        UPDATE dbo.Produccion
           SET ID_Cosecha = @ID_Cosecha,
               Fecha_Produccion = @Fecha_Produccion,
               Unidad_Medida = LTRIM(RTRIM(@Unidad_Medida)),
               Cantidad_Bruta = @Cantidad_Bruta,
               Merma = @Merma,
               Cantidad_Neta = @Cantidad_Neta,
               Precio_Unitario = @Precio_Unitario,
               Ingreso_Estimado = @Ingreso_Estimado,
               Costo_Base_Asignado = @Costo_Base_Asignado,
               Costo_Postcosecha = @Costo_Postcosecha,
               Beneficio_Neto = @Beneficio_Neto,
               Destino = NULLIF(LTRIM(RTRIM(@Destino)), ''),
               Estado = @Estado,
               Observaciones = NULLIF(LTRIM(RTRIM(@Observaciones)), ''),
               Fecha_Venta = @Fecha_Venta,
               Comprador = NULLIF(LTRIM(RTRIM(@Comprador)), ''),
               Metodo_Pago = NULLIF(LTRIM(RTRIM(@Metodo_Pago)), ''),
               Estado_Cobro = @Estado_Cobro,
               Referencia_Venta = NULLIF(LTRIM(RTRIM(@Referencia_Venta)), '')
         WHERE ID_Produccion = @ID_Produccion;

        EXEC dbo.Sincronizar_Finanzas_Produccion @ID_Produccion;

        COMMIT TRANSACTION;
        SELECT CAST(1 AS BIT);
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Eliminar_Produccion
    @ID_Produccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Estado VARCHAR(20);
        SELECT @Estado = Estado
        FROM dbo.Produccion WITH (UPDLOCK, HOLDLOCK)
        WHERE ID_Produccion = @ID_Produccion;

        IF @Estado IS NULL
            THROW 51403, 'La produccion seleccionada no existe.', 1;

        IF @Estado = 'Vendida'
            THROW 52006, 'Una produccion vendida no se elimina. Anulela para conservar la trazabilidad.', 1;

        DELETE dbo.Movimiento_Financiero
         WHERE Origen = 'Produccion' AND ID_Origen = @ID_Produccion;

        DELETE dbo.Produccion WHERE ID_Produccion = @ID_Produccion;

        COMMIT TRANSACTION;
        SELECT CAST(1 AS BIT);
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Insertar_Movimiento_Financiero_Seguro
    @Tipo VARCHAR(10),
    @Categoria VARCHAR(50),
    @Descripcion VARCHAR(200),
    @Monto DECIMAL(14,2),
    @Fecha DATE,
    @Metodo_Pago VARCHAR(30),
    @Estado VARCHAR(15),
    @Referencia VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT dbo.Movimiento_Financiero
        (Tipo, Categoria, Descripcion, Monto, Fecha, Metodo_Pago,
         Estado, Referencia, Origen, ID_Origen)
    VALUES
        (@Tipo, @Categoria, @Descripcion, @Monto, @Fecha, @Metodo_Pago,
         @Estado, @Referencia, 'Manual', NULL);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_Movimiento_Financiero_Seguro
    @ID_Movimiento INT,
    @Tipo VARCHAR(10),
    @Categoria VARCHAR(50),
    @Descripcion VARCHAR(200),
    @Monto DECIMAL(14,2),
    @Fecha DATE,
    @Metodo_Pago VARCHAR(30),
    @Estado VARCHAR(15),
    @Referencia VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Movimiento_Financiero
       SET Tipo = @Tipo,
           Categoria = @Categoria,
           Descripcion = @Descripcion,
           Monto = @Monto,
           Fecha = @Fecha,
           Metodo_Pago = @Metodo_Pago,
           Estado = @Estado,
           Referencia = @Referencia
     WHERE ID_Movimiento = @ID_Movimiento
       AND Origen = 'Manual';

    IF @@ROWCOUNT = 0
        THROW 52007, 'El movimiento no existe o es automatico y debe modificarse desde su modulo de origen.', 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Eliminar_Movimiento_Financiero_Seguro
    @ID_Movimiento INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE dbo.Movimiento_Financiero
     WHERE ID_Movimiento = @ID_Movimiento
       AND Origen = 'Manual';

    IF @@ROWCOUNT = 0
        THROW 52007, 'El movimiento no existe o es automatico y debe modificarse desde su modulo de origen.', 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Listar_Movimientos_Financieros
    @FechaDesde DATE,
    @FechaHasta DATE,
    @Busqueda VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #Movimientos
    (
        ID INT NULL,
        Tipo VARCHAR(10) NOT NULL,
        Categoria VARCHAR(50) NOT NULL,
        Descripcion VARCHAR(200) NOT NULL,
        Monto DECIMAL(14,2) NOT NULL,
        Fecha DATE NOT NULL,
        Metodo VARCHAR(30) NOT NULL,
        Estado VARCHAR(15) NOT NULL,
        Referencia VARCHAR(80) NULL,
        Origen VARCHAR(30) NOT NULL,
        Editable BIT NOT NULL
    );

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        ID_Movimiento, Tipo, Categoria, Descripcion, Monto, Fecha,
        Metodo_Pago, Estado, Referencia, Origen,
        CONVERT(BIT, CASE WHEN Origen = 'Manual' THEN 1 ELSE 0 END)
    FROM dbo.Movimiento_Financiero;

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        NULL, 'Gasto', 'Cosecha',
        LEFT('Costo de cosecha de ' + ISNULL(S.Nombre_Cultivo, 'cultivo'), 200),
        CONVERT(DECIMAL(14,2), C.Costo_Cosecha),
        CONVERT(DATE, ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha)),
        'Sistema', 'Pagado',
        'COSTO-COSECHA-' + CONVERT(VARCHAR(20), C.ID_Cosecha),
        'Cosecha', 0
    FROM dbo.Cosecha C
    INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
    WHERE ISNULL(C.Costo_Cosecha, 0) > 0;

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        NULL, 'Gasto', 'Siembra',
        LEFT('Costo de siembra de ' + ISNULL(S.Nombre_Cultivo, 'cultivo'), 200),
        CONVERT(DECIMAL(14,2), S.Costo_Siembra),
        CONVERT(DATE, S.Fecha_Plantacion),
        'Sistema', 'Pagado',
        'SIEMBRA-' + CONVERT(VARCHAR(20), S.ID_Cultivo),
        'Siembra', 0
    FROM dbo.Siembra S
    WHERE ISNULL(S.Costo_Siembra, 0) > 0;

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        NULL, 'Gasto', 'Fertilizacion',
        LEFT('Fertilizacion' +
             CASE WHEN F.Tipo_Fertilizante IS NULL THEN ''
                  ELSE ': ' + F.Tipo_Fertilizante END, 200),
        CONVERT(DECIMAL(14,2), F.Costo_Fertilizacion),
        CONVERT(DATE, ISNULL(F.Fecha_Aplicacion, GETDATE())),
        'Sistema', 'Pagado',
        'FERTILIZACION-' + CONVERT(VARCHAR(20), F.ID_Fertilizacion),
        'Fertilizacion', 0
    FROM dbo.Fertilizacion F
    WHERE ISNULL(F.Costo_Fertilizacion, 0) > 0;

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        NULL, 'Gasto', 'Nomina',
        LEFT('Pago a ' + ISNULL(T.Nombre + ' ' + T.Apellido, 'trabajador') +
             CASE WHEN NULLIF(LTRIM(RTRIM(P.Descripcion)), '') IS NULL
                  THEN '' ELSE ': ' + P.Descripcion END, 200),
        CONVERT(DECIMAL(14,2), P.Monto),
        CONVERT(DATE, P.Fecha_Pago),
        'Sistema', 'Pagado',
        'PAGO-' + CONVERT(VARCHAR(20), P.ID_Pago),
        'Nomina', 0
    FROM dbo.Pago_Trabajador P
    LEFT JOIN dbo.Trabajador T ON T.ID_Trabajador = P.ID_Trabajador
    WHERE ISNULL(P.Monto, 0) > 0;

    INSERT #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
         Estado, Referencia, Origen, Editable)
    SELECT
        NULL, 'Gasto', 'Alquiler',
        LEFT('Alquiler de ' + ISNULL(A.Tipo_Recurso, 'recurso') +
             CASE WHEN NULLIF(LTRIM(RTRIM(A.Descripcion)), '') IS NULL
                  THEN '' ELSE ': ' + A.Descripcion END, 200),
        CONVERT(DECIMAL(14,2), A.Costo),
        CONVERT(DATE, A.Fecha_Inicio),
        'Sistema', 'Pagado',
        'ALQUILER-' + CONVERT(VARCHAR(20), A.ID_Administracion),
        'Alquiler', 0
    FROM dbo.Alquiler_Administracion A
    WHERE ISNULL(A.Costo, 0) > 0
      AND ISNULL(A.Estado, '') <> 'Cancelado';

    SELECT
        ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo,
        Estado, Referencia, Origen, Editable
    FROM #Movimientos
    WHERE Fecha BETWEEN @FechaDesde AND @FechaHasta
      AND
      (
          @Busqueda IS NULL
          OR Categoria LIKE '%' + @Busqueda + '%'
          OR Descripcion LIKE '%' + @Busqueda + '%'
          OR Referencia LIKE '%' + @Busqueda + '%'
          OR Origen LIKE '%' + @Busqueda + '%'
      )
    ORDER BY Fecha DESC, Tipo, Categoria;
END;
GO

IF DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app') IS NOT NULL
BEGIN
    GRANT EXECUTE ON OBJECT::dbo.Listar_Cosechas_Para_Produccion TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Listar_Produccion TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Registrar_Produccion TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Actualizar_Produccion TO db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Eliminar_Produccion TO db_sistema_agricola_app;
    REVOKE EXECUTE ON OBJECT::dbo.Sincronizar_Finanzas_Produccion FROM db_sistema_agricola_app;
END;
GO

IF COL_LENGTH(N'dbo.Produccion', N'Costo_Base_Asignado') IS NULL OR
   COL_LENGTH(N'dbo.Produccion', N'Estado_Cobro') IS NULL OR
   COL_LENGTH(N'dbo.Movimiento_Financiero', N'Origen') IS NULL OR
   COL_LENGTH(N'dbo.Movimiento_Financiero', N'ID_Origen') IS NULL OR
   OBJECT_ID(N'dbo.Sincronizar_Finanzas_Produccion', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Registrar_Produccion', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Actualizar_Produccion', N'P') IS NULL
    THROW 52008, 'La migracion 011 no completo todos sus objetos.', 1;

MERGE dbo.Esquema_Version AS destino
USING (SELECT 11 AS Version, 'Produccion integrada con finanzas' AS Nombre) AS origen
ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 011 aplicada correctamente.';
GO

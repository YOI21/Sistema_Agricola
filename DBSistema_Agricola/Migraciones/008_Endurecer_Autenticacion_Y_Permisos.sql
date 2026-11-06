USE DBSistema_Agricola;
GO

SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) <> 7
    THROW 51980, 'La migracion 008 requiere que la base este exactamente en la version 7.', 1;

IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
    THROW 51981, 'No existe dbo.Usuario.', 1;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF COL_LENGTH(N'dbo.Usuario', N'Debe_Cambiar_Contrasena') IS NULL
    BEGIN
        ALTER TABLE dbo.Usuario
        ADD Debe_Cambiar_Contrasena BIT NOT NULL
            CONSTRAINT DF_Usuario_DebeCambiar DEFAULT (1) WITH VALUES;
    END;

    IF COL_LENGTH(N'dbo.Usuario', N'Intentos_Fallidos') IS NULL
    BEGIN
        ALTER TABLE dbo.Usuario
        ADD Intentos_Fallidos INT NOT NULL
            CONSTRAINT DF_Usuario_IntentosFallidos DEFAULT (0) WITH VALUES;
    END;

    IF COL_LENGTH(N'dbo.Usuario', N'Bloqueado_Hasta') IS NULL
        ALTER TABLE dbo.Usuario ADD Bloqueado_Hasta DATETIME2(0) NULL;

    IF COL_LENGTH(N'dbo.Usuario', N'Ultimo_Acceso') IS NULL
        ALTER TABLE dbo.Usuario ADD Ultimo_Acceso DATETIME2(0) NULL;

    IF COL_LENGTH(N'dbo.Usuario', N'Ultimo_Cambio_Contrasena') IS NULL
        ALTER TABLE dbo.Usuario ADD Ultimo_Cambio_Contrasena DATETIME2(0) NULL;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.Usuario
        WHERE Contrasena_Hash IS NULL
           OR Contrasena_Salt IS NULL
           OR Contrasena_Iteraciones IS NULL
           OR Contrasena_Iteraciones < 150000
    )
        THROW 51982, 'Hay cuentas sin PBKDF2. Ejecute Preparar_008_Contrasenas_Legadas.ps1 y vuelva a intentar.', 1;

    ALTER TABLE dbo.Usuario ALTER COLUMN Contrasena_Hash VARBINARY(64) NOT NULL;
    ALTER TABLE dbo.Usuario ALTER COLUMN Contrasena_Salt VARCHAR(64) NOT NULL;
    ALTER TABLE dbo.Usuario ALTER COLUMN Contrasena_Iteraciones INT NOT NULL;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.check_constraints
        WHERE name = N'CK_Usuario_Intentos_Fallidos'
          AND parent_object_id = OBJECT_ID(N'dbo.Usuario')
    )
    BEGIN
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.Usuario WITH CHECK
            ADD CONSTRAINT CK_Usuario_Intentos_Fallidos
            CHECK (Intentos_Fallidos >= 0);';
    END;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.check_constraints
        WHERE name = N'CK_Usuario_Iteraciones_PBKDF2'
          AND parent_object_id = OBJECT_ID(N'dbo.Usuario')
    )
    BEGIN
        ALTER TABLE dbo.Usuario WITH CHECK
        ADD CONSTRAINT CK_Usuario_Iteraciones_PBKDF2 CHECK (Contrasena_Iteraciones >= 150000);
    END;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.check_constraints
        WHERE name = N'CK_Usuario_Hash_PBKDF2'
          AND parent_object_id = OBJECT_ID(N'dbo.Usuario')
    )
    BEGIN
        ALTER TABLE dbo.Usuario WITH CHECK
        ADD CONSTRAINT CK_Usuario_Hash_PBKDF2 CHECK (DATALENGTH(Contrasena_Hash) >= 32);
    END;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.check_constraints
        WHERE name = N'CK_Usuario_Salt_PBKDF2'
          AND parent_object_id = OBJECT_ID(N'dbo.Usuario')
    )
    BEGIN
        ALTER TABLE dbo.Usuario WITH CHECK
        ADD CONSTRAINT CK_Usuario_Salt_PBKDF2 CHECK (LEN(Contrasena_Salt) >= 20);
    END;

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Existe_Usuario_Registrado
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM dbo.Usuario;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Existe_Administrador_Seguro
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM dbo.Administrador;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Existe_Usuario_Seguro
    @Usuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM dbo.Usuario
    WHERE LTRIM(RTRIM(Usuario)) = LTRIM(RTRIM(@Usuario));
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Existe_Email_Administrador_Seguro
    @Email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM dbo.Administrador
    WHERE LTRIM(RTRIM(Email)) = LTRIM(RTRIM(@Email));
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Obtener_Cuenta_Autenticacion
    @Usuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1)
        U.ID_Usuario,
        CASE WHEN U.Rol = ''Administrador'' THEN ADM.ID_Administrador ELSE A.ID_Agricultor END AS ID_Perfil,
        U.Usuario,
        U.Rol,
        CASE WHEN U.Rol = ''Administrador'' THEN ADM.Nombre ELSE A.Nombre END AS Nombre,
        CASE WHEN U.Rol = ''Administrador'' THEN ADM.Apellido ELSE A.Apellido END AS Apellido,
        U.Contrasena_Hash,
        U.Contrasena_Salt,
        U.Contrasena_Iteraciones,
        U.Debe_Cambiar_Contrasena,
        U.Intentos_Fallidos,
        U.Bloqueado_Hasta
    FROM dbo.Usuario U
    LEFT JOIN dbo.Administrador ADM ON ADM.ID_Usuario = U.ID_Usuario
    LEFT JOIN dbo.Agricultor A ON A.ID_Usuario = U.ID_Usuario
    WHERE U.Usuario = @Usuario
      AND ((U.Rol = ''Administrador'' AND ADM.ID_Administrador IS NOT NULL)
        OR (U.Rol = ''Agricultor'' AND A.ID_Agricultor IS NOT NULL));
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Intento_Login_Fallido
    @ID_Usuario INT,
    @Maximo_Intentos INT = 5,
    @Minutos_Bloqueo INT = 15
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Ahora DATETIME2(0) = SYSDATETIME();
        DECLARE @Intentos INT;
        DECLARE @Bloqueado_Hasta DATETIME2(0);

        SELECT @Intentos = Intentos_Fallidos,
               @Bloqueado_Hasta = Bloqueado_Hasta
        FROM dbo.Usuario WITH (UPDLOCK, ROWLOCK)
        WHERE ID_Usuario = @ID_Usuario;

        IF @Intentos IS NULL
            THROW 51983, ''La cuenta no existe.'', 1;

        IF @Bloqueado_Hasta IS NOT NULL AND @Bloqueado_Hasta <= @Ahora
            SET @Intentos = 0;

        SET @Intentos += 1;
        SET @Bloqueado_Hasta = CASE
            WHEN @Intentos >= @Maximo_Intentos
                THEN DATEADD(MINUTE, @Minutos_Bloqueo, @Ahora)
            ELSE NULL
        END;

        UPDATE dbo.Usuario
           SET Intentos_Fallidos = @Intentos,
               Bloqueado_Hasta = @Bloqueado_Hasta
         WHERE ID_Usuario = @ID_Usuario;

        COMMIT TRANSACTION;
        SELECT @Intentos AS Intentos_Fallidos, @Bloqueado_Hasta AS Bloqueado_Hasta;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Login_Exitoso
    @ID_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Usuario
       SET Intentos_Fallidos = 0,
           Bloqueado_Hasta = NULL,
           Ultimo_Acceso = SYSDATETIME()
     WHERE ID_Usuario = @ID_Usuario;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Actualizar_Contrasena_Segura
    @ID_Usuario INT,
    @Hash VARBINARY(64),
    @Salt VARCHAR(64),
    @Iteraciones INT,
    @Debe_Cambiar BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    IF @Iteraciones < 150000 OR DATALENGTH(@Hash) < 32 OR NULLIF(@Salt, '''') IS NULL
        THROW 51984, ''Los datos PBKDF2 no son validos.'', 1;

    UPDATE dbo.Usuario
       SET Contrasena_Hash = @Hash,
           Contrasena_Salt = @Salt,
           Contrasena_Iteraciones = @Iteraciones,
           Debe_Cambiar_Contrasena = @Debe_Cambiar,
           Ultimo_Cambio_Contrasena = SYSDATETIME(),
           Intentos_Fallidos = 0,
           Bloqueado_Hasta = NULL
     WHERE ID_Usuario = @ID_Usuario;

    IF @@ROWCOUNT <> 1
        THROW 51985, ''La cuenta no existe.'', 1;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Administrador_Seguro
    @Nombre VARCHAR(50), @Apellido VARCHAR(50), @Email VARCHAR(100),
    @Telefono VARCHAR(20), @Usuario VARCHAR(50),
    @Contrasena_Hash VARBINARY(64), @Contrasena_Salt VARCHAR(64),
    @Contrasena_Iteraciones INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF @Contrasena_Iteraciones < 150000 OR
       DATALENGTH(@Contrasena_Hash) < 32 OR NULLIF(@Contrasena_Salt, '''') IS NULL
        THROW 51992, ''Los datos PBKDF2 no son validos.'', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT dbo.Usuario
            (Usuario, Rol, Contrasena_Hash, Contrasena_Salt,
             Contrasena_Iteraciones, Debe_Cambiar_Contrasena,
             Intentos_Fallidos, Ultimo_Cambio_Contrasena)
        VALUES
            (@Usuario, ''Administrador'', @Contrasena_Hash, @Contrasena_Salt,
             @Contrasena_Iteraciones, 1, 0, SYSDATETIME());

        DECLARE @ID_Usuario INT = SCOPE_IDENTITY();
        INSERT dbo.Administrador
            (Nombre, Apellido, Email, Telefono, Fecha_Registro, ID_Usuario)
        VALUES
            (@Nombre, @Apellido, @Email, @Telefono, GETDATE(), @ID_Usuario);

        DECLARE @ID_Administrador INT = SCOPE_IDENTITY();
        COMMIT TRANSACTION;
        SELECT @ID_Administrador;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Insertar_Agricultor
    @Nombre VARCHAR(50), @Apellido VARCHAR(50), @Cedula VARCHAR(11),
    @Telefono VARCHAR(11), @Email VARCHAR(50), @Direccion VARCHAR(100),
    @Ciudad VARCHAR(50), @Sector VARCHAR(50), @Usuario VARCHAR(50),
    @Contrasena_Hash VARBINARY(64), @Contrasena_Salt VARCHAR(64),
    @Contrasena_Iteraciones INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF @Contrasena_Iteraciones < 150000 OR
       DATALENGTH(@Contrasena_Hash) < 32 OR NULLIF(@Contrasena_Salt, '''') IS NULL
        THROW 51993, ''Los datos PBKDF2 no son validos.'', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT dbo.Usuario
            (Usuario, Rol, Contrasena_Hash, Contrasena_Salt,
             Contrasena_Iteraciones, Debe_Cambiar_Contrasena,
             Intentos_Fallidos, Ultimo_Cambio_Contrasena)
        VALUES
            (@Usuario, ''Agricultor'', @Contrasena_Hash, @Contrasena_Salt,
             @Contrasena_Iteraciones, 1, 0, SYSDATETIME());

        DECLARE @ID_Usuario INT = SCOPE_IDENTITY();
        INSERT dbo.Agricultor
            (Nombre, Apellido, Cedula, Telefono, Email, Direccion,
             Ciudad, Sector, Fecha_Registro, ID_Usuario)
        VALUES
            (@Nombre, @Apellido, @Cedula, @Telefono, @Email, @Direccion,
             @Ciudad, @Sector, GETDATE(), @ID_Usuario);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Obtener_Cuenta_Recuperacion
    @Identificador VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) *
    FROM
    (
        SELECT U.ID_Usuario, U.Usuario, ADM.Email,
               LTRIM(RTRIM(ADM.Nombre + '' '' + ADM.Apellido)) AS NombreCompleto,
               CAST(''Administrador'' AS VARCHAR(20)) AS TipoUsuario, 0 AS Orden
        FROM dbo.Usuario U
        INNER JOIN dbo.Administrador ADM ON ADM.ID_Usuario = U.ID_Usuario
        WHERE U.Usuario = @Identificador OR ADM.Email = @Identificador
        UNION ALL
        SELECT U.ID_Usuario, U.Usuario, A.Email,
               LTRIM(RTRIM(A.Nombre + '' '' + A.Apellido)),
               CAST(''Agricultor'' AS VARCHAR(20)), 1
        FROM dbo.Usuario U
        INNER JOIN dbo.Agricultor A ON A.ID_Usuario = U.ID_Usuario
        WHERE U.Usuario = @Identificador OR A.Email = @Identificador
    ) Cuentas
    ORDER BY Orden;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Codigo_Recuperacion
    @ID_Usuario INT, @Codigo_Hash VARBINARY(64), @Codigo_Salt VARCHAR(50),
    @Fecha_Expiracion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE dbo.Recuperacion_Contrasena SET Usado = 1
        WHERE ID_Usuario = @ID_Usuario AND Usado = 0;
        INSERT dbo.Recuperacion_Contrasena
            (ID_Usuario, Codigo_Hash, Codigo_Salt, Fecha_Solicitud,
             Fecha_Expiracion, Usado, Intentos)
        VALUES
            (@ID_Usuario, @Codigo_Hash, @Codigo_Salt, GETDATE(),
             @Fecha_Expiracion, 0, 0);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Obtener_Codigo_Recuperacion_Activo
    @ID_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) ID_Recuperacion, ID_Usuario, Codigo_Hash, Codigo_Salt,
           Fecha_Expiracion, Usado, Intentos
    FROM dbo.Recuperacion_Contrasena
    WHERE ID_Usuario = @ID_Usuario AND Usado = 0
    ORDER BY Fecha_Solicitud DESC, ID_Recuperacion DESC;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Intento_Recuperacion_Fallido
    @ID_Recuperacion INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Recuperacion_Contrasena SET Intentos = Intentos + 1
    WHERE ID_Recuperacion = @ID_Recuperacion;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Marcar_Codigo_Recuperacion_Usado
    @ID_Recuperacion INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Recuperacion_Contrasena SET Usado = 1
    WHERE ID_Recuperacion = @ID_Recuperacion;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Restablecer_Contrasena_Recuperacion
    @ID_Usuario INT, @ID_Recuperacion INT, @Hash VARBINARY(64),
    @Salt VARCHAR(64), @Iteraciones INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF @Iteraciones < 150000 OR DATALENGTH(@Hash) < 32 OR NULLIF(@Salt, '''') IS NULL
        THROW 51988, ''Los datos PBKDF2 no son validos.'', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.Recuperacion_Contrasena WITH (UPDLOCK, ROWLOCK)
            WHERE ID_Recuperacion = @ID_Recuperacion
              AND ID_Usuario = @ID_Usuario
              AND Usado = 0
              AND Fecha_Expiracion >= GETDATE()
        )
            THROW 51989, ''El codigo no esta activo o ya fue utilizado.'', 1;

        UPDATE dbo.Usuario
           SET Contrasena_Hash = @Hash, Contrasena_Salt = @Salt,
               Contrasena_Iteraciones = @Iteraciones,
               Debe_Cambiar_Contrasena = 0,
               Ultimo_Cambio_Contrasena = SYSDATETIME(),
               Intentos_Fallidos = 0, Bloqueado_Hasta = NULL
         WHERE ID_Usuario = @ID_Usuario;

        IF @@ROWCOUNT <> 1
            THROW 51990, ''La cuenta no existe.'', 1;

        UPDATE dbo.Recuperacion_Contrasena SET Usado = 1
        WHERE ID_Recuperacion = @ID_Recuperacion
          AND ID_Usuario = @ID_Usuario AND Usado = 0;

        COMMIT TRANSACTION;
        SELECT CAST(1 AS BIT);
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Insertar_Alerta_Segura
    @ID_Cultivo INT, @Titulo VARCHAR(150), @Descripcion VARCHAR(500),
    @Fecha_Programada DATE, @Fecha_Creacion DATETIME, @Tipo_Alerta VARCHAR(50),
    @Estado VARCHAR(20), @Prioridad VARCHAR(10), @Notificacion_Enviada BIT,
    @Dias_Anticipacion INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Alertas
        (ID_Cultivo, Titulo, Descripcion, Fecha_Programada, Fecha_Creacion,
         Tipo_Alerta, Estado, Prioridad, Notificacion_Enviada, Dias_Anticipacion)
    VALUES
        (@ID_Cultivo, @Titulo, @Descripcion, @Fecha_Programada, @Fecha_Creacion,
         @Tipo_Alerta, @Estado, @Prioridad, @Notificacion_Enviada, @Dias_Anticipacion);
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Actualizar_Alerta_Segura
    @ID_Alerta INT, @ID_Cultivo INT, @Titulo VARCHAR(150),
    @Descripcion VARCHAR(500), @Fecha_Programada DATE, @Fecha_Creacion DATETIME,
    @Tipo_Alerta VARCHAR(50), @Estado VARCHAR(20), @Prioridad VARCHAR(10),
    @Notificacion_Enviada BIT, @Dias_Anticipacion INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Alertas
       SET ID_Cultivo = @ID_Cultivo,
           Titulo = @Titulo,
           Descripcion = @Descripcion,
           Fecha_Programada = @Fecha_Programada,
           Tipo_Alerta = @Tipo_Alerta,
           Estado = @Estado,
           Prioridad = @Prioridad,
           Dias_Anticipacion = @Dias_Anticipacion,
           Notificacion_Enviada = CASE
               WHEN @Estado = ''Pendiente'' THEN 0
               ELSE Notificacion_Enviada
           END
     WHERE ID_Alerta = @ID_Alerta;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Completar_Alerta_Segura @ID_Alerta INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Alertas
       SET Estado = ''Completada'', Notificacion_Enviada = 1
     WHERE ID_Alerta = @ID_Alerta;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Eliminar_Alerta_Segura @ID_Alerta INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE dbo.Alertas WHERE ID_Alerta = @ID_Alerta;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Marcar_Alerta_Notificada_Segura @ID_Alerta INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Alertas SET Notificacion_Enviada = 1
    WHERE ID_Alerta = @ID_Alerta;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Insertar_Movimiento_Financiero_Seguro
    @Tipo VARCHAR(10), @Categoria VARCHAR(50), @Descripcion VARCHAR(200),
    @Monto DECIMAL(14,2), @Fecha DATE, @Metodo_Pago VARCHAR(30),
    @Estado VARCHAR(15), @Referencia VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Movimiento_Financiero
        (Tipo, Categoria, Descripcion, Monto, Fecha, Metodo_Pago, Estado, Referencia)
    VALUES
        (@Tipo, @Categoria, @Descripcion, @Monto, @Fecha, @Metodo_Pago, @Estado, @Referencia);
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Actualizar_Movimiento_Financiero_Seguro
    @ID_Movimiento INT, @Tipo VARCHAR(10), @Categoria VARCHAR(50),
    @Descripcion VARCHAR(200), @Monto DECIMAL(14,2), @Fecha DATE,
    @Metodo_Pago VARCHAR(30), @Estado VARCHAR(15), @Referencia VARCHAR(80) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Movimiento_Financiero
       SET Tipo = @Tipo, Categoria = @Categoria, Descripcion = @Descripcion,
           Monto = @Monto, Fecha = @Fecha, Metodo_Pago = @Metodo_Pago,
           Estado = @Estado, Referencia = @Referencia
     WHERE ID_Movimiento = @ID_Movimiento;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Eliminar_Movimiento_Financiero_Seguro
    @ID_Movimiento INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE dbo.Movimiento_Financiero WHERE ID_Movimiento = @ID_Movimiento;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Cambiar_Estado_Producto_Inventario
    @Id_Insumo INT, @Estado VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Insumos SET Estado = @Estado WHERE Id_Insumo = @Id_Insumo;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Insertar_Siembra_Segura
    @Nombre_Cultivo VARCHAR(100), @Tipo_Cultivo VARCHAR(50),
    @Categoria_Cultivo VARCHAR(50), @Estacion_Siembra VARCHAR(50),
    @Cantidad INT, @Tipo_Suelo VARCHAR(50), @Fecha_Plantacion DATE,
    @Fecha_Cosecha DATE, @Metodo_Siembra VARCHAR(50) = NULL,
    @Profundidad VARCHAR(50) = NULL, @Humedad_Suelo DECIMAL(14,3) = NULL,
    @Costo_Siembra DECIMAL(14,2) = NULL, @ID_Terreno INT = NULL,
    @Hectareas_Sembradas DECIMAL(14,3) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.Siembra
        (Nombre_Cultivo, Tipo_Cultivo, Categoria_Cultivo, Estacion_Siembra,
         Cantidad, Tipo_Suelo, Fecha_Plantacion, Fecha_Cosecha, Metodo_Siembra,
         Profundidad, Humedad_Suelo, Costo_Siembra, ID_Terreno,
         Hectareas_Sembradas, Estado_Cultivo)
    VALUES
        (@Nombre_Cultivo, @Tipo_Cultivo, @Categoria_Cultivo, @Estacion_Siembra,
         @Cantidad, @Tipo_Suelo, @Fecha_Plantacion, @Fecha_Cosecha, @Metodo_Siembra,
         @Profundidad, @Humedad_Suelo, @Costo_Siembra, @ID_Terreno,
         @Hectareas_Sembradas, ''Activo'');
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Actualizar_Siembra_Segura
    @ID_Cultivo INT, @Nombre_Cultivo VARCHAR(100), @Tipo_Cultivo VARCHAR(50),
    @Categoria_Cultivo VARCHAR(50), @Estacion_Siembra VARCHAR(50),
    @Cantidad INT, @Tipo_Suelo VARCHAR(50), @Fecha_Plantacion DATE,
    @Fecha_Cosecha DATE, @Metodo_Siembra VARCHAR(50) = NULL,
    @Profundidad VARCHAR(50) = NULL, @Humedad_Suelo DECIMAL(14,3) = NULL,
    @Costo_Siembra DECIMAL(14,2) = NULL, @ID_Terreno INT = NULL,
    @Hectareas_Sembradas DECIMAL(14,3) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Siembra
       SET Nombre_Cultivo = @Nombre_Cultivo,
           Tipo_Cultivo = @Tipo_Cultivo,
           Categoria_Cultivo = @Categoria_Cultivo,
           Estacion_Siembra = @Estacion_Siembra,
           Cantidad = @Cantidad,
           Tipo_Suelo = @Tipo_Suelo,
           Fecha_Plantacion = @Fecha_Plantacion,
           Fecha_Cosecha = @Fecha_Cosecha,
           Metodo_Siembra = @Metodo_Siembra,
           Profundidad = @Profundidad,
           Humedad_Suelo = @Humedad_Suelo,
           Costo_Siembra = @Costo_Siembra,
           ID_Terreno = @ID_Terreno,
           Hectareas_Sembradas = @Hectareas_Sembradas
     WHERE ID_Cultivo = @ID_Cultivo;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Asignar_Agricultor_Siembra_Segura
    @ID_Agricultor INT, @ID_Cultivo INT, @Notas VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1 FROM dbo.Agricultor_Siembra
        WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo
    )
        UPDATE dbo.Agricultor_Siembra
           SET Fecha_Asignacion = GETDATE(), Notas = @Notas
         WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo;
    ELSE
        INSERT dbo.Agricultor_Siembra
            (ID_Agricultor, ID_Cultivo, Fecha_Asignacion, Notas)
        VALUES (@ID_Agricultor, @ID_Cultivo, GETDATE(), @Notas);
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Registrar_Cosecha_Cultivo_Segura @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.Cosecha WHERE id_cultivo = @ID_Cultivo)
    BEGIN
        SELECT CAST(0 AS BIT);
        RETURN;
    END;

    INSERT dbo.Cosecha (Estado_Cosecha, id_cultivo)
    VALUES (''Completada'', @ID_Cultivo);
    UPDATE dbo.Siembra SET Estado_Cultivo = ''Cosechado''
    WHERE ID_Cultivo = @ID_Cultivo;
    SELECT CAST(1 AS BIT);
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Guardar_Cosecha_Segura
    @ID_Cultivo INT, @Estado_Cosecha VARCHAR(25),
    @Cantidad_Cosechada DECIMAL(18,3), @Rendimiento DECIMAL(18,3),
    @Calidad DECIMAL(10,2), @Observaciones VARCHAR(200),
    @Fecha_Cosecha DATE, @Costo_Cosecha DECIMAL(18,2),
    @Precio_Venta DECIMAL(18,2), @Beneficio DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.Siembra WITH (UPDLOCK, ROWLOCK)
            WHERE ID_Cultivo = @ID_Cultivo
        )
            THROW 51991, ''El cultivo seleccionado no existe.'', 1;

        DECLARE @ID_Cosecha INT;
        SELECT TOP (1) @ID_Cosecha = ID_Cosecha
        FROM dbo.Cosecha WITH (UPDLOCK, ROWLOCK)
        WHERE id_cultivo = @ID_Cultivo
        ORDER BY ID_Cosecha;

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
            SET @ID_Cosecha = SCOPE_IDENTITY();
            UPDATE dbo.Siembra SET Estado_Cultivo = ''Cosechado''
            WHERE ID_Cultivo = @ID_Cultivo;
            COMMIT TRANSACTION;
            SELECT CAST(''Insertada'' AS VARCHAR(20));
            RETURN;
        END;

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
        COMMIT TRANSACTION;
        SELECT CAST(''Actualizada'' AS VARCHAR(20));
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'
CREATE OR ALTER PROCEDURE dbo.Eliminar_Cosecha_Segura
    @ID_Cosecha INT, @ID_Cultivo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE dbo.Cosecha
        WHERE ID_Cosecha = @ID_Cosecha AND id_cultivo = @ID_Cultivo;
        DECLARE @Eliminada BIT = CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
        IF @Eliminada = 1
            UPDATE dbo.Siembra SET Estado_Cultivo = ''Activo''
            WHERE ID_Cultivo = @ID_Cultivo;
        COMMIT TRANSACTION;
        SELECT @Eliminada;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    IF OBJECT_ID(N'dbo.Actualizar_Usuario_Agricultor', N'P') IS NOT NULL
        DROP PROCEDURE dbo.Actualizar_Usuario_Agricultor;

    IF OBJECT_ID(N'dbo.Verificar_Agricultor_Admin', N'P') IS NOT NULL
        DROP PROCEDURE dbo.Verificar_Agricultor_Admin;

    DECLARE @SqlDinamico nvarchar(max);

    IF COL_LENGTH(N'dbo.Usuario', N'Contraseña') IS NOT NULL
    BEGIN
        DECLARE @RestriccionLegada sysname;
        SELECT @RestriccionLegada = dc.name
        FROM sys.default_constraints dc
        INNER JOIN sys.columns c
            ON c.object_id = dc.parent_object_id
           AND c.column_id = dc.parent_column_id
        WHERE dc.parent_object_id = OBJECT_ID(N'dbo.Usuario')
          AND c.name = N'Contraseña';

        IF @RestriccionLegada IS NOT NULL
        BEGIN
            SET @SqlDinamico = N'ALTER TABLE dbo.Usuario DROP CONSTRAINT ' +
                QUOTENAME(@RestriccionLegada) + N';';
            EXEC sys.sp_executesql @SqlDinamico;
        END;

        EXEC sys.sp_executesql N'ALTER TABLE dbo.Usuario DROP COLUMN [Contraseña];';
    END;

    IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'db_sistema_agricola_app')
        CREATE ROLE db_sistema_agricola_app;

    REVOKE SELECT TO db_sistema_agricola_app;
    REVOKE INSERT TO db_sistema_agricola_app;
    REVOKE UPDATE TO db_sistema_agricola_app;
    REVOKE DELETE TO db_sistema_agricola_app;
    REVOKE EXECUTE TO db_sistema_agricola_app;
    GRANT CONNECT TO db_sistema_agricola_app;
    GRANT INSERT ON dbo.Auditoria_Seguridad TO db_sistema_agricola_app;

    DECLARE @ProcedimientosAutorizados TABLE (Nombre sysname PRIMARY KEY);
    INSERT @ProcedimientosAutorizados (Nombre)
    VALUES
        (N'Actualizar_Contrasena_Segura'),
        (N'Actualizar_Equipo_Mantenimiento'),
        (N'Actualizar_Gestion_Maquinaria'),
        (N'Actualizar_Insumo'),
        (N'Actualizar_Mantenimiento_Equipo'),
        (N'Actualizar_Movimiento_Financiero_Seguro'),
        (N'Actualizar_Producto_Inventario'),
        (N'Actualizar_SeguimientoCrecimiento'),
        (N'Actualizar_Siembra_Segura'),
        (N'Actualizar_Terreno'),
        (N'Actualizar_Trabajador'),
        (N'Asignar_Agricultor_Siembra_Segura'),
        (N'Asignar_Gestion_Maquinaria'),
        (N'Asignar_Maquinaria'),
        (N'Buscar_Insumo'),
        (N'Buscar_Trabajador'),
        (N'BuscarMaquinaria'),
        (N'Cambiar_Estado_Cultivo'),
        (N'Cambiar_Estado_Equipo'),
        (N'Cambiar_Estado_Maquinaria'),
        (N'Cambiar_Estado_Producto_Inventario'),
        (N'Completar_Alerta_Segura'),
        (N'Eliminar_Alerta_Segura'),
        (N'Eliminar_Cosecha_Segura'),
        (N'Eliminar_Equipo_Mantenimiento'),
        (N'Eliminar_Gestion_Maquinaria'),
        (N'Eliminar_Insumo'),
        (N'Eliminar_Mantenimiento_Equipo'),
        (N'Eliminar_Movimiento_Financiero_Seguro'),
        (N'Eliminar_SeguimientoCrecimiento'),
        (N'Eliminar_Siembra'),
        (N'Eliminar_Terreno'),
        (N'Eliminar_Trabajador'),
        (N'Existe_Administrador_Seguro'),
        (N'Existe_Email_Administrador_Seguro'),
        (N'Existe_Usuario_Registrado'),
        (N'Existe_Usuario_Seguro'),
        (N'Finalizar_Asignacion'),
        (N'Finalizar_Gestion_Asignacion'),
        (N'Guardar_Cosecha_Segura'),
        (N'Ingresar_Maquinaria'),
        (N'Insertar_Administrador_Seguro'),
        (N'Insertar_Agricultor'),
        (N'Insertar_Alerta_Segura'),
        (N'Insertar_Fertilizacion'),
        (N'Insertar_Insumo'),
        (N'Insertar_Movimiento_Financiero_Seguro'),
        (N'Insertar_RP'),
        (N'Insertar_SeguimientoCrecimiento'),
        (N'Insertar_Siembra_Segura'),
        (N'Insertar_Terreno'),
        (N'Insertar_Trabajador'),
        (N'Listar_Asignaciones_Activas'),
        (N'Listar_Asignaciones_Maquinaria'),
        (N'Listar_Equipos_Mantenimiento'),
        (N'Listar_Gestion_Maquinaria'),
        (N'Listar_Insumos'),
        (N'Listar_Inventario'),
        (N'Listar_Mantenimientos_Equipos'),
        (N'Listar_Mantenimientos_Maquinaria'),
        (N'Listar_Maquinaria_Disponible'),
        (N'Listar_Movimientos_Financieros'),
        (N'Listar_Movimientos_Inventario'),
        (N'Listar_Operadores'),
        (N'Listar_Trabajadores'),
        (N'Listar_Trabajadores_Maquinaria'),
        (N'Marcar_Alerta_Notificada_Segura'),
        (N'Marcar_Codigo_Recuperacion_Usado'),
        (N'Mostrar_Cultivos'),
        (N'Mostrar_SeguimientoCrecimiento'),
        (N'Mostrar_Siembras'),
        (N'Obtener_Codigo_Recuperacion_Activo'),
        (N'Obtener_Cuenta_Autenticacion'),
        (N'Obtener_Cuenta_Recuperacion'),
        (N'Obtener_Insumo'),
        (N'Obtener_Reporte_Siembras'),
        (N'Obtener_Trabajador'),
        (N'Registrar_Administrador_Seguro'),
        (N'Registrar_Codigo_Recuperacion'),
        (N'Registrar_Cosecha_Cultivo_Segura'),
        (N'Registrar_Equipo_Mantenimiento'),
        (N'Registrar_Gestion_Maquinaria'),
        (N'Registrar_Intento_Login_Fallido'),
        (N'Registrar_Intento_Recuperacion_Fallido'),
        (N'Registrar_Login_Exitoso'),
        (N'Registrar_Mantenimiento_Equipo'),
        (N'Registrar_Mantenimiento_Maquinaria'),
        (N'Registrar_Movimiento_Inventario'),
        (N'Registrar_Producto_Inventario'),
        (N'Reporte_TipoMA'),
        (N'Restablecer_Contrasena_Recuperacion');

    DECLARE @Procedimiento sysname;
    DECLARE procedimientos CURSOR LOCAL FAST_FORWARD FOR
        SELECT PA.Nombre
        FROM @ProcedimientosAutorizados PA
        INNER JOIN sys.procedures P ON P.name = PA.Nombre
        INNER JOIN sys.schemas S ON S.schema_id = P.schema_id AND S.name = N'dbo';

    OPEN procedimientos;
    FETCH NEXT FROM procedimientos INTO @Procedimiento;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SqlDinamico = N'GRANT EXECUTE ON OBJECT::dbo.' +
            QUOTENAME(@Procedimiento) + N' TO db_sistema_agricola_app;';
        EXEC sys.sp_executesql @SqlDinamico;
        FETCH NEXT FROM procedimientos INTO @Procedimiento;
    END;
    CLOSE procedimientos;
    DEALLOCATE procedimientos;

    GRANT SELECT ON dbo.Agricultor TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Agricultor_Siembra TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Alertas TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Cedulas_Verificadas TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Cosecha TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Enfermedades TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Equipos TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Esquema_Version TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Fertilizacion TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Insumos TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Mantenimiento_Equipos TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Mantenimiento_Maquinaria TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Movimiento_Financiero TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Movimiento_Inventario TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.P_Terreno TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Produccion TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.R_Maquinaria TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.RP TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.S_Cultivo TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.SeguimientoCrecimiento TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Siembra TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Terrenos TO db_sistema_agricola_app;
    GRANT SELECT ON dbo.Trabajador TO db_sistema_agricola_app;

    MERGE dbo.Esquema_Version AS destino
    USING (SELECT 8 AS Version,
                  'Autenticacion endurecida y permisos SQL minimos' AS Nombre) origen
       ON destino.Version = origen.Version
    WHEN MATCHED THEN
        UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

IF COL_LENGTH(N'dbo.Usuario', N'Contraseña') IS NOT NULL
    THROW 51986, 'No se elimino la columna de contrasena heredada.', 1;

IF (SELECT MAX(Version) FROM dbo.Esquema_Version) <> 8
    THROW 51987, 'La version 8 no quedo registrada.', 1;
GO

PRINT 'Migracion 008 aplicada: autenticacion endurecida y permisos SQL minimos.';
GO

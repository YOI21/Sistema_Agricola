/*
    Sistema Agricola - configuracion completa de permisos de respaldo

    Ejecute este archivo en SQL Server Management Studio con una cuenta
    que sea sysadmin. El script puede ejecutarse mas de una vez.

    La aplicacion y los scripts PowerShell usan seguridad integrada de
    Windows. Por eso se toma automaticamente el usuario de Windows con
    el que se ejecuta este archivo.
*/

USE [master];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF DB_ID(N'DBSistema_Agricola') IS NULL
BEGIN
    THROW 51000, 'No existe la base de datos DBSistema_Agricola. Restaure o cree la base antes de configurar los permisos.', 1;
END;
GO

USE [DBSistema_Agricola];
GO

DECLARE @RolAplicacion sysname = N'db_sistema_agricola_app';
DECLARE @RolRespaldo sysname = N'db_sistema_agricola_backup';
DECLARE @Sql nvarchar(max);

IF DATABASE_PRINCIPAL_ID(@RolAplicacion) IS NULL
BEGIN
    SET @Sql = N'CREATE ROLE ' + QUOTENAME(@RolAplicacion) + N' AUTHORIZATION [dbo];';
    EXEC sys.sp_executesql @Sql;
    PRINT N'Rol creado: ' + @RolAplicacion;
END
ELSE
BEGIN
    PRINT N'El rol ya existe: ' + @RolAplicacion;
END;

IF DATABASE_PRINCIPAL_ID(@RolRespaldo) IS NULL
BEGIN
    SET @Sql = N'CREATE ROLE ' + QUOTENAME(@RolRespaldo) + N' AUTHORIZATION [dbo];';
    EXEC sys.sp_executesql @Sql;
    PRINT N'Rol creado: ' + @RolRespaldo;
END
ELSE
BEGIN
    PRINT N'El rol ya existe: ' + @RolRespaldo;
END;

/*
    Los permisos de la aplicacion se definen por objeto en la migracion 008.
    Este archivo no vuelve a conceder acceso global a las tablas.
*/
IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) < 8
BEGIN
    THROW 51002, 'Ejecute las migraciones 000 a 008 antes de asociar el usuario al rol de aplicacion.', 1;
END;

REVOKE SELECT TO [db_sistema_agricola_app];
REVOKE INSERT TO [db_sistema_agricola_app];
REVOKE UPDATE TO [db_sistema_agricola_app];
REVOKE DELETE TO [db_sistema_agricola_app];
REVOKE EXECUTE TO [db_sistema_agricola_app];
GRANT CONNECT TO [db_sistema_agricola_app];

PRINT N'Permisos de aplicacion conservados por objeto segun la migracion 008.';

/* Permisos necesarios para BACKUP DATABASE y BACKUP LOG. */
GRANT CONNECT TO [db_sistema_agricola_backup];
GRANT BACKUP DATABASE TO [db_sistema_agricola_backup];
GRANT BACKUP LOG TO [db_sistema_agricola_backup];
GO

/*
    Asociar automaticamente el usuario actual.

    Si USER_NAME() devuelve dbo significa que la cuenta ya tiene control
    total (normalmente por ser sysadmin) y no necesita pertenecer al rol.
*/
DECLARE @UsuarioActual sysname = USER_NAME();
DECLARE @EsMiembroAplicacion int;
DECLARE @EsMiembroRespaldo int;
DECLARE @Sql nvarchar(max);

IF @UsuarioActual IS NULL
BEGIN
    THROW 51001, 'No fue posible identificar el usuario de la base de datos.', 1;
END;

IF @UsuarioActual = N'dbo'
BEGIN
    PRINT N'La conexion actual usa dbo/sysadmin; ya tiene permisos para respaldar.';
    PRINT N'No se intento agregar dbo a los roles porque SQL Server no lo permite.';
END
ELSE
BEGIN
    SET @EsMiembroAplicacion = IS_ROLEMEMBER(N'db_sistema_agricola_app', @UsuarioActual);
    SET @EsMiembroRespaldo = IS_ROLEMEMBER(N'db_sistema_agricola_backup', @UsuarioActual);

    IF ISNULL(@EsMiembroAplicacion, 0) = 0
    BEGIN
        SET @Sql = N'ALTER ROLE [db_sistema_agricola_app] ADD MEMBER ' + QUOTENAME(@UsuarioActual) + N';';
        EXEC sys.sp_executesql @Sql;
        PRINT N'Usuario agregado al rol de aplicacion: ' + @UsuarioActual;
    END;

    IF ISNULL(@EsMiembroRespaldo, 0) = 0
    BEGIN
        SET @Sql = N'ALTER ROLE [db_sistema_agricola_backup] ADD MEMBER ' + QUOTENAME(@UsuarioActual) + N';';
        EXEC sys.sp_executesql @Sql;
        PRINT N'Usuario agregado al rol de respaldo: ' + @UsuarioActual;
    END;
END;
GO

/* Verificacion final. */
SELECT
    DB_NAME() AS BaseDatos,
    ORIGINAL_LOGIN() AS LoginEjecucion,
    USER_NAME() AS UsuarioBaseDatos,
    HAS_PERMS_BY_NAME(DB_NAME(), N'DATABASE', N'BACKUP DATABASE') AS PuedeRespaldarBase,
    HAS_PERMS_BY_NAME(DB_NAME(), N'DATABASE', N'BACKUP LOG') AS PuedeRespaldarLog;

SELECT
    rol.name AS Rol,
    miembro.name AS Miembro
FROM sys.database_role_members AS drm
INNER JOIN sys.database_principals AS rol
    ON rol.principal_id = drm.role_principal_id
INNER JOIN sys.database_principals AS miembro
    ON miembro.principal_id = drm.member_principal_id
WHERE rol.name IN (N'db_sistema_agricola_app', N'db_sistema_agricola_backup')
ORDER BY rol.name, miembro.name;

PRINT N'Configuracion de permisos completada correctamente.';
GO

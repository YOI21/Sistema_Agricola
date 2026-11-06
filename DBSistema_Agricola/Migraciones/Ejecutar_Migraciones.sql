/*
    Ejecutar en SQL Server Management Studio con SQLCMD Mode activado:
    Query > SQLCMD Mode.
*/

:r .\000_Crear_Tabla_Esquema_Version.sql
:r .\001_Administrador_Y_Roles.sql
:r .\002_Seguridad_Contrasenas.sql
:r .\003_Acceso_Usuarios.sql
:r .\004_Autorizacion_Auditoria.sql
:r .\005_Procedimientos_Terreno_Fertilizacion.sql
:r .\006_Estructura_Modulos_Operativos.sql
:r .\007_Resultados_Autorizacion_Modulos_Agricolas.sql
:r .\008_Endurecer_Autenticacion_Y_Permisos.sql
:r .\009_Transacciones_Operativas_Atomicas.sql
:r .\010_Reglas_Agroclimaticas_Y_Variedades.sql
:r .\011_Produccion_Integracion_Financiera.sql
:r .\012_Cerrar_Autorizacion_Registros.sql

SELECT Version, Nombre, Aplicada_En
FROM dbo.Esquema_Version
ORDER BY Version;
GO

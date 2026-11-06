/*
    INSTALACION COMPLETA DESDE CERO - SISTEMA AGRICOLA
    Compatible con SQL Server 2019 o posterior.

    ADVERTENCIA: elimina DBSistema_Agricola si ya existe.
    Use este archivo solamente para una instalacion nueva.
*/
USE [master];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
IF DB_ID(N'DBSistema_Agricola') IS NOT NULL
BEGIN
    ALTER DATABASE [DBSistema_Agricola] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [DBSistema_Agricola];
END;
GO
CREATE DATABASE [DBSistema_Agricola];
GO
ALTER DATABASE [DBSistema_Agricola] SET RECOVERY SIMPLE;
GO
USE [DBSistema_Agricola];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET DATEFORMAT dmy;
GO

/* ========================================================================== */
/* TABLAS, CLAVES LOCALES E INDICES */
/* ========================================================================== */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Administrador](
	[ID_Administrador] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Fecha_Registro] [datetime] NOT NULL,
	[ID_Usuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Administrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Agricultor](
	[ID_Agricultor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Telefono] [varchar](11) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Ciudad] [varchar](50) NOT NULL,
	[Sector] [varchar](50) NOT NULL,
	[Fecha_Registro] [date] NOT NULL,
	[ID_Usuario] [int] NULL,
 CONSTRAINT [PK_ID_Agricultor] PRIMARY KEY CLUSTERED 
(
	[ID_Agricultor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Agricultor_Siembra](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Agricultor] [int] NULL,
	[ID_Cultivo] [int] NULL,
	[Fecha_Asignacion] [datetime] NOT NULL,
	[Notas] [varchar](500) NULL,
 CONSTRAINT [PK_Agricultor_Siembra] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alertas](
	[ID_Alerta] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cultivo] [int] NULL,
	[Titulo] [varchar](150) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Fecha_Programada] [date] NOT NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Tipo_Alerta] [varchar](50) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[Prioridad] [varchar](10) NOT NULL,
	[Notificacion_Enviada] [bit] NOT NULL,
	[Dias_Anticipacion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Alerta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alquiler_Administracion](
	[ID_Administracion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Agricultor] [int] NULL,
	[Tipo_Recurso] [varchar](50) NOT NULL,
	[ID_Recurso] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Costo] [money] NOT NULL,
	[Fecha_Inicio] [date] NOT NULL,
	[Fecha_Fin] [date] NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ID_Administracion] PRIMARY KEY CLUSTERED 
(
	[ID_Administracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alquiler_Maquinaria](
	[ID_Alquiler] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Maquina] [varchar](100) NOT NULL,
	[Tipo_Maquinaria] [varchar](50) NOT NULL,
	[Costo_Hora] [money] NOT NULL,
	[Hora_Inicio] [date] NOT NULL,
	[Hora_Fin] [date] NOT NULL,
 CONSTRAINT [PK_ID_Alquiler] PRIMARY KEY CLUSTERED 
(
	[ID_Alquiler] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Asignacion_Maquinaria](
	[ID_Asignacion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Trabajador] [int] NULL,
	[ID_Maquina] [int] NULL,
	[Fecha_Asignacion] [datetime] NOT NULL,
	[Fecha_Devolucion] [datetime] NULL,
	[Estado] [varchar](50) NOT NULL,
	[Observaciones] [varchar](200) NULL,
	[Fecha_Prevista_Devolucion] [date] NULL,
	[Actividad] [varchar](120) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Asignacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Auditoria_Seguridad](
	[ID_Auditoria] [int] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [int] NULL,
	[Usuario] [varchar](80) NOT NULL,
	[Rol] [varchar](30) NOT NULL,
	[Permiso] [varchar](80) NOT NULL,
	[Entidad] [varchar](80) NOT NULL,
	[Accion] [varchar](40) NOT NULL,
	[Detalle] [varchar](1000) NULL,
	[Resultado] [varchar](20) NOT NULL,
	[Nombre_Equipo] [varchar](128) NULL,
	[Usuario_Windows] [varchar](128) NULL,
	[Fecha_Registro] [datetime] NOT NULL,
 CONSTRAINT [PK_Auditoria_Seguridad] PRIMARY KEY CLUSTERED 
(
	[ID_Auditoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cedulas_Verificadas](
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Fecha_Nacimiento] [date] NULL,
	[Sexo] [varchar](10) NULL,
	[Fecha_Actualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cosecha](
	[ID_Cosecha] [int] IDENTITY(0,1) NOT NULL,
	[Estado_Cosecha] [varchar](25) NOT NULL,
	[id_cultivo] [int] NULL,
	[Cantidad_Cosechada] [decimal](10, 2) NULL,
	[Rendimiento] [decimal](10, 2) NULL,
	[Calidad] [decimal](5, 2) NULL,
	[Observaciones] [varchar](200) NULL,
	[Fecha_Cosecha] [date] NULL,
	[Costo_Cosecha] [decimal](10, 2) NULL,
	[Precio_Venta] [decimal](10, 2) NULL,
	[Beneficio] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Cosecha] PRIMARY KEY CLUSTERED 
(
	[ID_Cosecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Enfermedades](
	[Id_CP] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](100) NOT NULL,
	[Nombre_Comun] [varchar](100) NOT NULL,
	[Nombre_Cientifico] [varchar](120) NOT NULL,
	[Razgos] [varchar](500) NULL,
	[Huesped] [varchar](max) NOT NULL,
	[Cura] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_CP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Equipos](
	[Id_Equipos] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Costo_Equipo] [decimal](14, 2) NOT NULL,
	[Codigo] [varchar](30) NOT NULL,
	[Tipo_Equipo] [varchar](50) NOT NULL,
	[Fecha_Adquisicion] [date] NULL,
	[Proximo_Mantenimiento] [date] NULL,
	[Ubicacion] [varchar](80) NULL,
	[Estado] [varchar](20) NOT NULL,
	[Observaciones] [varchar](250) NULL,
 CONSTRAINT [PK_ID_Equipos] PRIMARY KEY CLUSTERED 
(
	[Id_Equipos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EstadoMaquinaria](
	[ID_Maquina] [int] NOT NULL,
	[Estado] [varchar](50) NOT NULL,
	[Ultima_Actualizacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Maquina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fertilizacion](
	[ID_Fertilizacion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cultivo] [int] NULL,
	[Tipo_Fertilizante] [varchar](50) NULL,
	[Metodo_Fertilizacion] [varchar](50) NULL,
	[Cantidad_Fertilizante] [int] NULL,
	[Costo_Fertilizacion] [decimal](10, 2) NULL,
	[Fecha_Aplicacion] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Fertilizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Insumos](
	[Id_Insumo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
	[Costo] [decimal](14, 2) NOT NULL,
	[fecha] [date] NOT NULL,
	[Cantidad] [decimal](14, 3) NOT NULL,
	[Codigo] [varchar](30) NOT NULL,
	[Unidad_Medida] [varchar](20) NOT NULL,
	[Stock_Minimo] [decimal](14, 3) NOT NULL,
	[Ubicacion] [varchar](80) NULL,
	[Fecha_Vencimiento] [date] NULL,
	[Estado] [varchar](15) NOT NULL,
 CONSTRAINT [PK_ID_Insumo] PRIMARY KEY CLUSTERED 
(
	[Id_Insumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mantenimiento_Equipos](
	[Id_Mantenimiento] [int] IDENTITY(1,1) NOT NULL,
	[Id_Equipo] [int] NOT NULL,
	[Tipo_Mantenimiento] [varchar](30) NOT NULL,
	[Fecha_Mantenimiento] [date] NOT NULL,
	[Cantidad_Atendida] [int] NOT NULL,
	[Costo] [decimal](14, 2) NOT NULL,
	[Proximo_Mantenimiento] [date] NULL,
	[Responsable] [varchar](100) NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Fecha_Registro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Mantenimiento_Equipos] PRIMARY KEY CLUSTERED 
(
	[Id_Mantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mantenimiento_Maquinaria](
	[ID_Mantenimiento] [int] IDENTITY(1,1) NOT NULL,
	[Fecha_Mantenimiento] [date] NOT NULL,
	[ID_Maquina] [int] NULL,
	[Tipo_Mantenimiento] [varchar](30) NOT NULL,
	[Costo] [decimal](14, 2) NOT NULL,
	[Horometro] [decimal](14, 1) NOT NULL,
	[Proximo_Mantenimiento] [date] NULL,
	[Proveedor] [varchar](100) NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Fecha_Registro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ID_Mantenimiento] PRIMARY KEY CLUSTERED 
(
	[ID_Mantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movimiento_Financiero](
	[ID_Movimiento] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](10) NOT NULL,
	[Categoria] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[Monto] [decimal](14, 2) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Metodo_Pago] [varchar](30) NOT NULL,
	[Estado] [varchar](15) NOT NULL,
	[Referencia] [varchar](80) NULL,
	[Fecha_Registro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Movimiento_Financiero] PRIMARY KEY CLUSTERED 
(
	[ID_Movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Movimiento_Inventario](
	[ID_Movimiento] [int] IDENTITY(1,1) NOT NULL,
	[Id_Insumo] [int] NOT NULL,
	[Tipo_Movimiento] [varchar](10) NOT NULL,
	[Cantidad] [decimal](14, 3) NOT NULL,
	[Costo_Unitario] [decimal](14, 2) NOT NULL,
	[Fecha_Movimiento] [date] NOT NULL,
	[Motivo] [varchar](150) NOT NULL,
	[Referencia] [varchar](80) NULL,
	[Fecha_Registro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Movimiento_Inventario] PRIMARY KEY CLUSTERED 
(
	[ID_Movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[P_Terreno](
	[Id_Terreno] [int] IDENTITY(1,1) NOT NULL,
	[Fecha_Inicio] [date] NOT NULL,
	[Fecha_Fin] [date] NOT NULL,
	[Tipo_Labranza] [varchar](50) NOT NULL,
	[Maquinaria_U] [varchar](100) NOT NULL,
	[Costo_Preparacion] [money] NOT NULL,
	[Tipo_Suelo] [varchar](50) NULL,
 CONSTRAINT [PK_ID_Terreno] PRIMARY KEY CLUSTERED 
(
	[Id_Terreno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pago_Trabajador](
	[ID_Pago] [int] IDENTITY(1,1) NOT NULL,
	[ID_Trabajador] [int] NULL,
	[ID_Agricultor] [int] NULL,
	[Monto] [money] NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Fecha_Pago] [date] NOT NULL,
 CONSTRAINT [PK_ID_Pago] PRIMARY KEY CLUSTERED 
(
	[ID_Pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Produccion](
	[ID_Produccion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cosecha] [int] NOT NULL,
	[Fecha_Produccion] [date] NOT NULL,
	[Unidad_Medida] [varchar](20) NOT NULL,
	[Cantidad_Bruta] [decimal](14, 3) NOT NULL,
	[Merma] [decimal](14, 3) NOT NULL,
	[Cantidad_Neta] [decimal](14, 3) NOT NULL,
	[Precio_Unitario] [decimal](14, 2) NOT NULL,
	[Ingreso_Estimado] [decimal](14, 2) NOT NULL,
	[Costo_Postcosecha] [decimal](14, 2) NOT NULL,
	[Beneficio_Neto] [decimal](14, 2) NOT NULL,
	[Destino] [varchar](80) NULL,
	[Estado] [varchar](20) NOT NULL,
	[Observaciones] [varchar](250) NULL,
	[Fecha_Registro] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Produccion] PRIMARY KEY CLUSTERED 
(
	[ID_Produccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[R_Maquinaria](
	[ID_Maquina] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Maquina] [varchar](100) NOT NULL,
	[Tipo_Maquinaria] [varchar](50) NOT NULL,
	[Costo_Maquinaria] [money] NOT NULL,
	[Fecha_Adquisicion] [date] NOT NULL,
	[Mantenimiento_Programado] [varchar](100) NULL,
	[Estado] [varchar](20) NOT NULL,
	[Codigo] [varchar](30) NOT NULL,
	[Marca] [varchar](50) NULL,
	[Modelo] [varchar](50) NULL,
	[Numero_Serie] [varchar](60) NULL,
	[Proximo_Mantenimiento] [date] NULL,
	[Horometro] [decimal](14, 1) NOT NULL,
	[Ubicacion] [varchar](80) NULL,
	[Observaciones] [varchar](250) NULL,
 CONSTRAINT [PK_ID_Maquina] PRIMARY KEY CLUSTERED 
(
	[ID_Maquina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recuperacion_Contrasena](
	[ID_Recuperacion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [int] NOT NULL,
	[Codigo_Hash] [varbinary](64) NOT NULL,
	[Codigo_Salt] [varchar](50) NOT NULL,
	[Fecha_Solicitud] [datetime] NOT NULL,
	[Fecha_Expiracion] [datetime] NOT NULL,
	[Usado] [bit] NOT NULL,
	[Intentos] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Recuperacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RP](
	[ID_RP] [int] IDENTITY(0,1) NOT NULL,
	[Nombre_Cultivo] [varchar](100) NOT NULL,
	[Tipo_Cultivo] [varchar](50) NOT NULL,
	[Categoria_Cultivo] [varchar](50) NOT NULL,
	[Estacion] [varchar](50) NOT NULL,
	[Tipo_Suelo] [varchar](50) NOT NULL,
	[Duracion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_RP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[S_Cultivo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Cultivo] [varchar](100) NOT NULL,
	[V_Reguio] [int] NULL,
	[V_Fertilizado] [int] NULL,
	[Tipo_fertilizante] [varchar](50) NULL,
	[Metodo_Fertilizacion] [varchar](50) NULL,
	[id_cultivo] [int] NULL,
 CONSTRAINT [PK_ID_Scultivo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SeguimientoCrecimiento](
	[ID_Seguimiento] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cultivo] [int] NULL,
	[Fecha_Registro] [datetime] NOT NULL,
	[Etapa_Crecimiento] [varchar](50) NOT NULL,
	[Altura_Planta] [decimal](10, 2) NOT NULL,
	[Num_Hojas] [int] NOT NULL,
	[Estado_Salud] [varchar](20) NOT NULL,
	[Observaciones] [varchar](200) NULL,
	[Porcentaje_Crecimiento] [decimal](5, 2) NULL,
	[Problemas_Detectados] [varchar](200) NULL,
	[Acciones_Tomadas] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Seguimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Siembra](
	[ID_Cultivo] [int] IDENTITY(0,1) NOT NULL,
	[Nombre_Cultivo] [varchar](100) NOT NULL,
	[Tipo_Cultivo] [varchar](50) NOT NULL,
	[Categoria_Cultivo] [varchar](50) NOT NULL,
	[Estacion_Siembra] [varchar](50) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Tipo_Suelo] [varchar](50) NOT NULL,
	[Fecha_Plantacion] [date] NOT NULL,
	[Fecha_Cosecha] [date] NOT NULL,
	[Estado_Cultivo] [varchar](50) NULL,
	[Metodo_Siembra] [varchar](50) NULL,
	[Profundidad] [varchar](50) NULL,
	[Humedad_Suelo] [decimal](10, 2) NULL,
	[Costo_Siembra] [decimal](10, 2) NULL,
	[ID_Terreno] [int] NULL,
	[Hectareas_Sembradas] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Cultivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Terrenos](
	[ID_Terreno] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Terreno] [varchar](100) NOT NULL,
	[Ubicacion] [varchar](200) NOT NULL,
	[Superficie_Total] [decimal](10, 2) NOT NULL,
	[Unidad_Medida] [varchar](20) NULL,
	[Tipo_Suelo] [varchar](50) NOT NULL,
	[Pendiente] [varchar](50) NULL,
	[Elevacion] [decimal](10, 2) NULL,
	[pH_Suelo] [decimal](4, 2) NULL,
	[Drenaje] [varchar](50) NULL,
	[Fuente_Agua] [varchar](50) NULL,
	[Fecha_Registro] [date] NOT NULL,
	[Observaciones] [varchar](500) NULL,
	[Estado] [varchar](20) NOT NULL,
	[Humedad] [decimal](5, 2) NULL,
	[Propietario] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Terreno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trabajador](
	[ID_Trabajador] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Sexo] [varchar](10) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Fecha_Nac] [date] NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](11) NULL,
	[Ciudad] [varchar](100) NULL,
	[Sector] [varchar](50) NULL,
	[Fecha_Ingreso] [date] NULL,
	[Posicion] [varchar](50) NULL,
	[Sueldo] [decimal](12, 2) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ID_Trabajador] PRIMARY KEY CLUSTERED 
(
	[ID_Trabajador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[ID_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Rol] [varchar](20) NOT NULL,
	[Contrasena_Hash] [varbinary](64) NOT NULL,
	[Contrasena_Salt] [varchar](64) NOT NULL,
	[Contrasena_Iteraciones] [int] NOT NULL,
	[Debe_Cambiar_Contrasena] [bit] NOT NULL CONSTRAINT [DF_Usuario_DebeCambiar] DEFAULT (1),
	[Intentos_Fallidos] [int] NOT NULL CONSTRAINT [DF_Usuario_IntentosFallidos] DEFAULT (0),
	[Bloqueado_Hasta] [datetime2](0) NULL,
	[Ultimo_Acceso] [datetime2](0) NULL,
	[Ultimo_Cambio_Contrasena] [datetime2](0) NULL,
 CONSTRAINT [PK_ID_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [CK_Usuario_Intentos_Fallidos] CHECK ([Intentos_Fallidos] >= (0)),
 CONSTRAINT [CK_Usuario_Iteraciones_PBKDF2] CHECK ([Contrasena_Iteraciones] >= (150000)),
 CONSTRAINT [CK_Usuario_Hash_PBKDF2] CHECK (DATALENGTH([Contrasena_Hash]) >= (32)),
 CONSTRAINT [CK_Usuario_Salt_PBKDF2] CHECK (LEN([Contrasena_Salt]) >= (20))
) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Administrador_Email] ON [dbo].[Administrador]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Administrador_ID_Usuario] ON [dbo].[Administrador]
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Agricultor_Cedula] ON [dbo].[Agricultor]
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Auditoria_Seguridad_Fecha] ON [dbo].[Auditoria_Seguridad]
(
	[Fecha_Registro] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE NONCLUSTERED INDEX [IX_Auditoria_Seguridad_Usuario] ON [dbo].[Auditoria_Seguridad]
(
	[Usuario] ASC,
	[Rol] ASC,
	[Fecha_Registro] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE NONCLUSTERED INDEX [IX_Auditoria_Seguridad_Resultado] ON [dbo].[Auditoria_Seguridad]
(
	[Resultado] ASC,
	[Fecha_Registro] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Equipos_Codigo] ON [dbo].[Equipos]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE NONCLUSTERED INDEX [IX_Insumos_Tipo_Estado] ON [dbo].[Insumos]
(
	[Tipo] ASC,
	[Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Insumos_Codigo] ON [dbo].[Insumos]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MantenimientoEquipos_EquipoFecha] ON [dbo].[Mantenimiento_Equipos]
(
	[Id_Equipo] ASC,
	[Fecha_Mantenimiento] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Movimiento_Financiero_Fecha] ON [dbo].[Movimiento_Financiero]
(
	[Fecha] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MovimientoInventario_InsumoFecha] ON [dbo].[Movimiento_Inventario]
(
	[Id_Insumo] ASC,
	[Fecha_Movimiento] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Produccion_CosechaFecha] ON [dbo].[Produccion]
(
	[ID_Cosecha] ASC,
	[Fecha_Produccion] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_RMaquinaria_Codigo] ON [dbo].[R_Maquinaria]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Recuperacion_Usuario_Activo] ON [dbo].[Recuperacion_Contrasena]
(
	[ID_Usuario] ASC,
	[Usado] ASC,
	[Fecha_Expiracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Trabajador_Email] ON [dbo].[Trabajador]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_Usuario_Usuario] ON [dbo].[Usuario]
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Administrador] ADD  DEFAULT (getdate()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Alertas] ADD  DEFAULT (getdate()) FOR [Fecha_Creacion]
GO

ALTER TABLE [dbo].[Alertas] ADD  DEFAULT ('Pendiente') FOR [Estado]
GO

ALTER TABLE [dbo].[Alertas] ADD  DEFAULT ('Media') FOR [Prioridad]
GO

ALTER TABLE [dbo].[Alertas] ADD  DEFAULT ((0)) FOR [Notificacion_Enviada]
GO

ALTER TABLE [dbo].[Alertas] ADD  DEFAULT ((7)) FOR [Dias_Anticipacion]
GO

ALTER TABLE [dbo].[Asignacion_Maquinaria] ADD  DEFAULT (getdate()) FOR [Fecha_Asignacion]
GO

ALTER TABLE [dbo].[Asignacion_Maquinaria] ADD  DEFAULT ('Asignada') FOR [Estado]
GO

ALTER TABLE [dbo].[Auditoria_Seguridad] ADD  CONSTRAINT [DF_Auditoria_Seguridad_Fecha]  DEFAULT (getdate()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Auditoria_Seguridad] ADD  CONSTRAINT [DF_Auditoria_Seguridad_Resultado]  DEFAULT ('Exito') FOR [Resultado]
GO

ALTER TABLE [dbo].[Auditoria_Seguridad] WITH CHECK ADD CONSTRAINT [CK_Auditoria_Seguridad_Resultado]
CHECK (([Resultado]='Error' OR [Resultado]='Denegado' OR [Resultado]='Fallido' OR [Resultado]='Exito'))
GO

ALTER TABLE [dbo].[Auditoria_Seguridad] CHECK CONSTRAINT [CK_Auditoria_Seguridad_Resultado]
GO

ALTER TABLE [dbo].[Cedulas_Verificadas] ADD  DEFAULT (getdate()) FOR [Fecha_Actualizacion]
GO

ALTER TABLE [dbo].[EstadoMaquinaria] ADD  DEFAULT ('Activa') FOR [Estado]
GO

ALTER TABLE [dbo].[EstadoMaquinaria] ADD  DEFAULT (getdate()) FOR [Ultima_Actualizacion]
GO

ALTER TABLE [dbo].[Mantenimiento_Equipos] ADD  CONSTRAINT [DF_MantEquipos_Cantidad]  DEFAULT ((1)) FOR [Cantidad_Atendida]
GO

ALTER TABLE [dbo].[Mantenimiento_Equipos] ADD  CONSTRAINT [DF_MantEquipos_Costo]  DEFAULT ((0)) FOR [Costo]
GO

ALTER TABLE [dbo].[Mantenimiento_Equipos] ADD  CONSTRAINT [DF_MantEquipos_Registro]  DEFAULT (sysdatetime()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Mantenimiento_Maquinaria] ADD  CONSTRAINT [DF_Mantenimiento_Maquinaria_FechaRegistro]  DEFAULT (sysdatetime()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Movimiento_Financiero] ADD  CONSTRAINT [DF_Movimiento_Financiero_FechaRegistro]  DEFAULT (sysdatetime()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Movimiento_Inventario] ADD  CONSTRAINT [DF_MovInv_Costo]  DEFAULT ((0)) FOR [Costo_Unitario]
GO

ALTER TABLE [dbo].[Movimiento_Inventario] ADD  CONSTRAINT [DF_MovInv_Registro]  DEFAULT (sysdatetime()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Merma]  DEFAULT ((0)) FOR [Merma]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Precio]  DEFAULT ((0)) FOR [Precio_Unitario]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Ingreso]  DEFAULT ((0)) FOR [Ingreso_Estimado]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Costo]  DEFAULT ((0)) FOR [Costo_Postcosecha]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Beneficio]  DEFAULT ((0)) FOR [Beneficio_Neto]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_Estado]  DEFAULT ('Registrada') FOR [Estado]
GO

ALTER TABLE [dbo].[Produccion] ADD  CONSTRAINT [DF_Produccion_FechaRegistro]  DEFAULT (sysdatetime()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Recuperacion_Contrasena] ADD  DEFAULT (getdate()) FOR [Fecha_Solicitud]
GO

ALTER TABLE [dbo].[Recuperacion_Contrasena] ADD  DEFAULT ((0)) FOR [Usado]
GO

ALTER TABLE [dbo].[Recuperacion_Contrasena] ADD  DEFAULT ((0)) FOR [Intentos]
GO

ALTER TABLE [dbo].[Terrenos] ADD  DEFAULT (getdate()) FOR [Fecha_Registro]
GO

ALTER TABLE [dbo].[Terrenos] ADD  DEFAULT ('Disponible') FOR [Estado]
GO

ALTER TABLE [dbo].[Trabajador] ADD  CONSTRAINT [DF_Trabajador_Sueldo]  DEFAULT ((0)) FOR [Sueldo]
GO

ALTER TABLE [dbo].[Trabajador] ADD  CONSTRAINT [DF_Trabajador_Estado]  DEFAULT ('Activo') FOR [Estado]
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Rol]  DEFAULT ('Agricultor') FOR [Rol]
GO

ALTER TABLE [dbo].[Movimiento_Financiero]  WITH CHECK ADD  CONSTRAINT [CK_Movimiento_Financiero_Estado] CHECK  (([Estado]='Pendiente' OR [Estado]='Pagado'))
GO

ALTER TABLE [dbo].[Movimiento_Financiero] CHECK CONSTRAINT [CK_Movimiento_Financiero_Estado]
GO

ALTER TABLE [dbo].[Movimiento_Financiero]  WITH CHECK ADD  CONSTRAINT [CK_Movimiento_Financiero_Monto] CHECK  (([Monto]>(0)))
GO

ALTER TABLE [dbo].[Movimiento_Financiero] CHECK CONSTRAINT [CK_Movimiento_Financiero_Monto]
GO

ALTER TABLE [dbo].[Movimiento_Financiero]  WITH CHECK ADD  CONSTRAINT [CK_Movimiento_Financiero_Tipo] CHECK  (([Tipo]='Gasto' OR [Tipo]='Ingreso'))
GO

ALTER TABLE [dbo].[Movimiento_Financiero] CHECK CONSTRAINT [CK_Movimiento_Financiero_Tipo]
GO

ALTER TABLE [dbo].[Movimiento_Inventario]  WITH CHECK ADD  CONSTRAINT [CK_MovimientoInventario_Cantidad] CHECK  (([Cantidad]>(0)))
GO

ALTER TABLE [dbo].[Movimiento_Inventario] CHECK CONSTRAINT [CK_MovimientoInventario_Cantidad]
GO

ALTER TABLE [dbo].[Movimiento_Inventario]  WITH CHECK ADD  CONSTRAINT [CK_MovimientoInventario_Tipo] CHECK  (([Tipo_Movimiento]='Salida' OR [Tipo_Movimiento]='Entrada'))
GO

ALTER TABLE [dbo].[Movimiento_Inventario] CHECK CONSTRAINT [CK_MovimientoInventario_Tipo]
GO

ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD  CONSTRAINT [CK_Produccion_Cantidades] CHECK  (([Cantidad_Bruta]>(0) AND [Merma]>=(0) AND [Merma]<=[Cantidad_Bruta]))
GO

ALTER TABLE [dbo].[Produccion] CHECK CONSTRAINT [CK_Produccion_Cantidades]
GO

ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD  CONSTRAINT [CK_Produccion_Estado] CHECK  (([Estado]='Cancelada' OR [Estado]='Vendida' OR [Estado]='Almacenada' OR [Estado]='Registrada'))
GO

ALTER TABLE [dbo].[Produccion] CHECK CONSTRAINT [CK_Produccion_Estado]
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_Rol_Acceso] CHECK  (([Rol]='Agricultor' OR [Rol]='Administrador'))
GO

ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_Rol_Acceso]
GO


/* ========================================================================== */
/* RELACIONES ENTRE TABLAS */
/* ========================================================================== */

ALTER TABLE [dbo].[Agricultor]  WITH CHECK ADD  CONSTRAINT [FK_ID_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO

ALTER TABLE [dbo].[Agricultor] CHECK CONSTRAINT [FK_ID_Usuario]
GO

ALTER TABLE [dbo].[Agricultor_Siembra]  WITH CHECK ADD  CONSTRAINT [FK_AS_Agricultor] FOREIGN KEY([ID_Agricultor])
REFERENCES [dbo].[Agricultor] ([ID_Agricultor])
GO

ALTER TABLE [dbo].[Agricultor_Siembra] CHECK CONSTRAINT [FK_AS_Agricultor]
GO

ALTER TABLE [dbo].[Agricultor_Siembra]  WITH CHECK ADD  CONSTRAINT [FK_AS_Cultivo] FOREIGN KEY([ID_Cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
GO

ALTER TABLE [dbo].[Agricultor_Siembra] CHECK CONSTRAINT [FK_AS_Cultivo]
GO

ALTER TABLE [dbo].[Alertas]  WITH CHECK ADD FOREIGN KEY([ID_Cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
GO

ALTER TABLE [dbo].[Alquiler_Administracion]  WITH CHECK ADD  CONSTRAINT [FK_ID_Agricultor_Admin] FOREIGN KEY([ID_Agricultor])
REFERENCES [dbo].[Agricultor] ([ID_Agricultor])
GO

ALTER TABLE [dbo].[Alquiler_Administracion] CHECK CONSTRAINT [FK_ID_Agricultor_Admin]
GO

ALTER TABLE [dbo].[Asignacion_Maquinaria]  WITH CHECK ADD FOREIGN KEY([ID_Maquina])
REFERENCES [dbo].[R_Maquinaria] ([ID_Maquina])
GO

ALTER TABLE [dbo].[Asignacion_Maquinaria]  WITH CHECK ADD FOREIGN KEY([ID_Trabajador])
REFERENCES [dbo].[Trabajador] ([ID_Trabajador])
GO

ALTER TABLE [dbo].[Cosecha]  WITH CHECK ADD  CONSTRAINT [FK_Cultivo] FOREIGN KEY([id_cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
GO

ALTER TABLE [dbo].[Cosecha] CHECK CONSTRAINT [FK_Cultivo]
GO

ALTER TABLE [dbo].[EstadoMaquinaria]  WITH CHECK ADD FOREIGN KEY([ID_Maquina])
REFERENCES [dbo].[R_Maquinaria] ([ID_Maquina])
GO

ALTER TABLE [dbo].[Fertilizacion]  WITH CHECK ADD FOREIGN KEY([ID_Cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Mantenimiento_Equipos]  WITH CHECK ADD  CONSTRAINT [FK_MantenimientoEquipos_Equipos] FOREIGN KEY([Id_Equipo])
REFERENCES [dbo].[Equipos] ([Id_Equipos])
GO

ALTER TABLE [dbo].[Mantenimiento_Equipos] CHECK CONSTRAINT [FK_MantenimientoEquipos_Equipos]
GO

ALTER TABLE [dbo].[Mantenimiento_Maquinaria]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Maquinaria] FOREIGN KEY([ID_Maquina])
REFERENCES [dbo].[R_Maquinaria] ([ID_Maquina])
GO

ALTER TABLE [dbo].[Mantenimiento_Maquinaria] CHECK CONSTRAINT [FK_Mantenimiento_Maquinaria]
GO

ALTER TABLE [dbo].[Movimiento_Inventario]  WITH CHECK ADD  CONSTRAINT [FK_MovimientoInventario_Insumo] FOREIGN KEY([Id_Insumo])
REFERENCES [dbo].[Insumos] ([Id_Insumo])
GO

ALTER TABLE [dbo].[Movimiento_Inventario] CHECK CONSTRAINT [FK_MovimientoInventario_Insumo]
GO

ALTER TABLE [dbo].[Pago_Trabajador]  WITH CHECK ADD  CONSTRAINT [FK_ID_Agricultor_Pago] FOREIGN KEY([ID_Agricultor])
REFERENCES [dbo].[Agricultor] ([ID_Agricultor])
GO

ALTER TABLE [dbo].[Pago_Trabajador] CHECK CONSTRAINT [FK_ID_Agricultor_Pago]
GO

ALTER TABLE [dbo].[Pago_Trabajador]  WITH CHECK ADD  CONSTRAINT [FK_ID_Trabajador] FOREIGN KEY([ID_Trabajador])
REFERENCES [dbo].[Trabajador] ([ID_Trabajador])
GO

ALTER TABLE [dbo].[Pago_Trabajador] CHECK CONSTRAINT [FK_ID_Trabajador]
GO

ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Produccion_Cosecha] FOREIGN KEY([ID_Cosecha])
REFERENCES [dbo].[Cosecha] ([ID_Cosecha])
GO

ALTER TABLE [dbo].[Produccion] CHECK CONSTRAINT [FK_Produccion_Cosecha]
GO

ALTER TABLE [dbo].[Recuperacion_Contrasena]  WITH CHECK ADD  CONSTRAINT [FK_Recuperacion_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO

ALTER TABLE [dbo].[Recuperacion_Contrasena] CHECK CONSTRAINT [FK_Recuperacion_Usuario]
GO

ALTER TABLE [dbo].[S_Cultivo]  WITH CHECK ADD  CONSTRAINT [FK_ID_Cultivo] FOREIGN KEY([id_cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
GO

ALTER TABLE [dbo].[S_Cultivo] CHECK CONSTRAINT [FK_ID_Cultivo]
GO

ALTER TABLE [dbo].[SeguimientoCrecimiento]  WITH CHECK ADD FOREIGN KEY([ID_Cultivo])
REFERENCES [dbo].[Siembra] ([ID_Cultivo])
GO

ALTER TABLE [dbo].[Siembra]  WITH CHECK ADD  CONSTRAINT [FK_Siembra_Terreno] FOREIGN KEY([ID_Terreno])
REFERENCES [dbo].[Terrenos] ([ID_Terreno])
GO

ALTER TABLE [dbo].[Siembra] CHECK CONSTRAINT [FK_Siembra_Terreno]
GO


/* ========================================================================== */
/* PROCEDIMIENTOS ALMACENADOS */
/* ========================================================================== */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Agricultor]
    @ID_Agricultor INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Cedula VARCHAR(11),
    @Telefono VARCHAR(11),
    @Email VARCHAR(50),
    @Direccion VARCHAR(100),
    @Ciudad VARCHAR(50),
    @Sector VARCHAR(50)
AS
BEGIN
    UPDATE Agricultor 
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Cedula = @Cedula,
        Telefono = @Telefono,
        Email = @Email,
        Direccion = @Direccion,
        Ciudad = @Ciudad,
        Sector = @Sector
    WHERE ID_Agricultor = @ID_Agricultor;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para actualizar una alerta
CREATE   PROCEDURE [dbo].[Actualizar_Alerta]
    @ID_Alerta INT,
    @Titulo VARCHAR(100),
    @Descripcion VARCHAR(255),
    @Fecha_Programada DATE,
    @Tipo_Alerta VARCHAR(50),
    @Estado VARCHAR(20),
    @Prioridad VARCHAR(10),
    @Dias_Anticipacion INT
AS
BEGIN
    UPDATE Alertas SET
        Titulo = @Titulo,
        Descripcion = @Descripcion,
        Fecha_Programada = @Fecha_Programada,
        Tipo_Alerta = @Tipo_Alerta,
        Estado = @Estado,
        Prioridad = @Prioridad,
        Dias_Anticipacion = @Dias_Anticipacion
    WHERE ID_Alerta = @ID_Alerta;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Cosecha]
    @ID_Cosecha INT,
    @Estado_Cosecha VARCHAR(25),
    @Cantidad_Cosechada DECIMAL(10, 2),
    @Rendimiento DECIMAL(10, 2),
    @Calidad DECIMAL(5, 2),
    @Observaciones VARCHAR(200),
    @Fecha_Cosecha DATE,
    @Costo_Cosecha DECIMAL(10, 2),
    @Precio_Venta DECIMAL(10, 2),
    @Beneficio DECIMAL(10, 2)
AS
BEGIN
    -- Actualizar en la tabla Cosecha
    UPDATE Cosecha
    SET
        Estado_Cosecha = @Estado_Cosecha,
        Cantidad_Cosechada = @Cantidad_Cosechada,
        Rendimiento = @Rendimiento,
        Calidad = @Calidad,
        Observaciones = @Observaciones,
        Fecha_Cosecha = @Fecha_Cosecha,
        Costo_Cosecha = @Costo_Cosecha,
        Precio_Venta = @Precio_Venta,
        Beneficio = @Beneficio
    WHERE ID_Cosecha = @ID_Cosecha;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Equipo_Mantenimiento]
    @Id_Equipo INT,
    @Codigo VARCHAR(30),
    @Nombre VARCHAR(100),
    @Tipo VARCHAR(50),
    @Cantidad INT,
    @Costo DECIMAL(14,2),
    @Fecha_Adquisicion DATE = NULL,
    @Proximo_Mantenimiento DATE = NULL,
    @Ubicacion VARCHAR(80) = NULL,
    @Observaciones VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Codigo = UPPER(LTRIM(RTRIM(@Codigo)));
    SET @Nombre = LTRIM(RTRIM(@Nombre));
    SET @Tipo = LTRIM(RTRIM(@Tipo));

    IF NOT EXISTS (SELECT 1 FROM dbo.Equipos WHERE Id_Equipos = @Id_Equipo)
        THROW 51302, 'El equipo seleccionado no existe.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Equipos
        WHERE Codigo = @Codigo
          AND Id_Equipos <> @Id_Equipo
    )
        THROW 51300, 'Ya existe otro equipo con ese codigo.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Equipos
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(@Nombre)
          AND LOWER(LTRIM(RTRIM(Tipo_Equipo))) = LOWER(@Tipo)
          AND Id_Equipos <> @Id_Equipo
          AND Estado <> 'Inactivo'
    )
        THROW 51301, 'Ya existe otro equipo activo con el mismo nombre y tipo.', 1;

    UPDATE dbo.Equipos
       SET Codigo = @Codigo,
           Nombre = @Nombre,
           Tipo_Equipo = @Tipo,
           Cantidad = @Cantidad,
           Costo_Equipo = @Costo,
           Fecha_Adquisicion = @Fecha_Adquisicion,
           Proximo_Mantenimiento = @Proximo_Mantenimiento,
           Ubicacion = @Ubicacion,
           Observaciones = @Observaciones
     WHERE Id_Equipos = @Id_Equipo;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Estado_Alquiler]
    @ID_Administracion INT,
    @Estado VARCHAR(20),
    @Fecha_Fin DATE = NULL
AS
BEGIN
    UPDATE Alquiler_Administracion
    SET Estado = @Estado,
        Fecha_Fin = ISNULL(@Fecha_Fin, Fecha_Fin)
    WHERE ID_Administracion = @ID_Administracion;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Estado_Terreno]
    @ID_Terreno INT,
    @Estado VARCHAR(20)
AS
BEGIN
    UPDATE Terrenos
    SET Estado = @Estado
    WHERE ID_Terreno = @ID_Terreno;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Gestion_Maquinaria]
    @ID_Maquina INT, @Codigo VARCHAR(30), @Nombre VARCHAR(100), @Tipo VARCHAR(50),
    @Marca VARCHAR(50)=NULL, @Modelo VARCHAR(50)=NULL, @Numero_Serie VARCHAR(60)=NULL,
    @Costo DECIMAL(14,2), @Fecha_Adquisicion DATE,
    @Mantenimiento_Programado VARCHAR(30), @Proximo_Mantenimiento DATE=NULL,
    @Horometro DECIMAL(14,1), @Ubicacion VARCHAR(80)=NULL,
    @Observaciones VARCHAR(250)=NULL
AS
BEGIN
    IF EXISTS(SELECT 1 FROM dbo.R_Maquinaria WHERE Codigo=@Codigo AND ID_Maquina<>@ID_Maquina)
        THROW 51200, 'Ya existe otra maquinaria con ese código.', 1;
    IF @Numero_Serie IS NOT NULL AND EXISTS
       (SELECT 1 FROM dbo.R_Maquinaria WHERE Numero_Serie=@Numero_Serie AND ID_Maquina<>@ID_Maquina)
        THROW 51201, 'Ya existe otra maquinaria con ese número de serie.', 1;
    UPDATE dbo.R_Maquinaria SET Codigo=@Codigo,Nombre_Maquina=@Nombre,
      Tipo_Maquinaria=@Tipo,Marca=@Marca,Modelo=@Modelo,Numero_Serie=@Numero_Serie,
      Costo_Maquinaria=@Costo,Fecha_Adquisicion=@Fecha_Adquisicion,
      Mantenimiento_Programado=@Mantenimiento_Programado,
      Proximo_Mantenimiento=@Proximo_Mantenimiento,Horometro=@Horometro,
      Ubicacion=@Ubicacion,Observaciones=@Observaciones
    WHERE ID_Maquina=@ID_Maquina;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Insumo]
    @Id_Insumo int,
    @Nombre varchar(100),
    @Tipo varchar(50),
    @Costo int,
    @Fecha date,
    @Cantidad int
AS
BEGIN
    UPDATE Insumos
    SET 
        Nombre = @Nombre,
        Tipo = @Tipo,
        Costo = @Costo,
        Fecha = @Fecha,
        Cantidad = @Cantidad
    WHERE Id_Insumo = @Id_Insumo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Mantenimiento_Equipo]
    @Id_Mantenimiento INT,
    @Id_Equipo INT,
    @Tipo_Mantenimiento VARCHAR(30),
    @Fecha_Mantenimiento DATE,
    @Cantidad_Atendida INT,
    @Costo DECIMAL(14,2),
    @Proximo_Mantenimiento DATE = NULL,
    @Responsable VARCHAR(100) = NULL,
    @Descripcion VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Id_Equipo_Anterior INT;
    DECLARE @Nombre VARCHAR(100);
    DECLARE @Cantidad INT;
    DECLARE @Estado VARCHAR(20);

    SELECT @Id_Equipo_Anterior = Id_Equipo
    FROM dbo.Mantenimiento_Equipos
    WHERE Id_Mantenimiento = @Id_Mantenimiento;

    IF @Id_Equipo_Anterior IS NULL
        THROW 51306, 'El mantenimiento seleccionado no existe.', 1;

    SELECT
        @Nombre = Nombre,
        @Cantidad = Cantidad,
        @Estado = Estado
    FROM dbo.Equipos WITH (UPDLOCK, HOLDLOCK)
    WHERE Id_Equipos = @Id_Equipo;

    IF @Nombre IS NULL
        THROW 51302, 'El equipo seleccionado no existe.', 1;
    IF @Estado = 'Inactivo'
        THROW 51304, 'No se puede actualizar mantenimiento a un equipo inactivo.', 1;
    IF @Cantidad_Atendida <= 0 OR @Cantidad_Atendida > @Cantidad
        THROW 51305, 'La cantidad atendida no puede superar la cantidad registrada del equipo.', 1;

    BEGIN TRANSACTION;

    UPDATE dbo.Mantenimiento_Equipos
       SET Id_Equipo = @Id_Equipo,
           Tipo_Mantenimiento = @Tipo_Mantenimiento,
           Fecha_Mantenimiento = @Fecha_Mantenimiento,
           Cantidad_Atendida = @Cantidad_Atendida,
           Costo = @Costo,
           Proximo_Mantenimiento = @Proximo_Mantenimiento,
           Responsable = @Responsable,
           Descripcion = @Descripcion
     WHERE Id_Mantenimiento = @Id_Mantenimiento;

    DECLARE @NuevoProximo DATE;

    SELECT TOP 1 @NuevoProximo = Proximo_Mantenimiento
    FROM dbo.Mantenimiento_Equipos
    WHERE Id_Equipo = @Id_Equipo
      AND Proximo_Mantenimiento IS NOT NULL
    ORDER BY Fecha_Mantenimiento DESC, Id_Mantenimiento DESC;

    UPDATE dbo.Equipos
       SET Proximo_Mantenimiento = @NuevoProximo
     WHERE Id_Equipos = @Id_Equipo;

    IF @Id_Equipo_Anterior <> @Id_Equipo
    BEGIN
        SET @NuevoProximo = NULL;

        SELECT TOP 1 @NuevoProximo = Proximo_Mantenimiento
        FROM dbo.Mantenimiento_Equipos
        WHERE Id_Equipo = @Id_Equipo_Anterior
          AND Proximo_Mantenimiento IS NOT NULL
        ORDER BY Fecha_Mantenimiento DESC, Id_Mantenimiento DESC;

        UPDATE dbo.Equipos
           SET Proximo_Mantenimiento = @NuevoProximo
         WHERE Id_Equipos = @Id_Equipo_Anterior;
    END;

    IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL
    BEGIN
        IF EXISTS
        (
            SELECT 1 FROM dbo.Movimiento_Financiero
            WHERE Referencia = 'MANT-EQ-' + CONVERT(VARCHAR(20), @Id_Mantenimiento)
        )
        BEGIN
            UPDATE dbo.Movimiento_Financiero
               SET Descripcion = LEFT('Mantenimiento de equipo: ' + @Nombre, 200),
                   Monto = @Costo,
                   Fecha = @Fecha_Mantenimiento
             WHERE Referencia = 'MANT-EQ-' + CONVERT(VARCHAR(20), @Id_Mantenimiento);
        END
        ELSE IF @Costo > 0
        BEGIN
            INSERT INTO dbo.Movimiento_Financiero
                (Tipo, Categoria, Descripcion, Monto, Fecha,
                 Metodo_Pago, Estado, Referencia)
            VALUES
                ('Gasto', 'Mantenimiento',
                 LEFT('Mantenimiento de equipo: ' + @Nombre, 200),
                 @Costo, @Fecha_Mantenimiento,
                 'Sistema', 'Pagado',
                 'MANT-EQ-' + CONVERT(VARCHAR(20), @Id_Mantenimiento));
        END;
    END;

    COMMIT TRANSACTION;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Produccion]
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

    IF NOT EXISTS (SELECT 1 FROM dbo.Produccion WHERE ID_Produccion = @ID_Produccion)
        THROW 51403, 'La produccion seleccionada no existe.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.Cosecha WHERE ID_Cosecha = @ID_Cosecha)
        THROW 51400, 'La cosecha seleccionada no existe.', 1;

    IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
        THROW 51401, 'Estado de produccion no valido.', 1;

    IF @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma > @Cantidad_Bruta
        THROW 51402, 'Las cantidades de produccion no son validas.', 1;

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
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Producto_Inventario]
    @Id_Insumo INT,
    @Codigo VARCHAR(30),
    @Nombre VARCHAR(100),
    @Tipo VARCHAR(50),
    @Unidad_Medida VARCHAR(20),
    @Costo DECIMAL(14,2),
    @Stock_Minimo DECIMAL(14,3),
    @Ubicacion VARCHAR(80) = NULL,
    @Fecha_Vencimiento DATE = NULL
AS
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM dbo.Insumos
        WHERE Codigo = @Codigo AND Id_Insumo <> @Id_Insumo
    )
        THROW 51100, 'Ya existe otro producto con ese código.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Insumos
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(LTRIM(RTRIM(@Nombre)))
          AND LOWER(LTRIM(RTRIM(Tipo))) = LOWER(LTRIM(RTRIM(@Tipo)))
          AND Id_Insumo <> @Id_Insumo
          AND Estado = 'Activo'
    )
        THROW 51101, 'Ya existe otro producto activo con el mismo nombre y tipo.', 1;

    UPDATE dbo.Insumos
       SET Codigo = @Codigo,
           Nombre = @Nombre,
           Tipo = @Tipo,
           Unidad_Medida = @Unidad_Medida,
           Costo = @Costo,
           Stock_Minimo = @Stock_Minimo,
           Ubicacion = @Ubicacion,
           Fecha_Vencimiento = @Fecha_Vencimiento
     WHERE Id_Insumo = @Id_Insumo;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_SeguimientoCrecimiento]
    @ID_Seguimiento INT,
    @Fecha_Registro DATETIME,
    @Etapa_Crecimiento VARCHAR(50),
    @Altura_Planta DECIMAL(10, 2),
    @Num_Hojas INT,
    @Estado_Salud VARCHAR(20),
    @Observaciones VARCHAR(200),
    @Porcentaje_Crecimiento DECIMAL(5, 2),
    @Problemas_Detectados VARCHAR(200),
    @Acciones_Tomadas VARCHAR(200)
AS
BEGIN
    UPDATE SeguimientoCrecimiento
    SET
        Fecha_Registro = @Fecha_Registro,
        Etapa_Crecimiento = @Etapa_Crecimiento,
        Altura_Planta = @Altura_Planta,
        Num_Hojas = @Num_Hojas,
        Estado_Salud = @Estado_Salud,
        Observaciones = @Observaciones,
        Porcentaje_Crecimiento = @Porcentaje_Crecimiento,
        Problemas_Detectados = @Problemas_Detectados,
        Acciones_Tomadas = @Acciones_Tomadas
    WHERE ID_Seguimiento = @ID_Seguimiento;
    
    RETURN 'Actualización de seguimiento exitosa';
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Actualizar_Siembra]
    @ID_Cultivo int,
    @Nombre_Cultivo varchar(100),
    @Tipo_Cultivo varchar(50),
    @Categoria_Cultivo varchar(50),
    @Estacion_Siembra varchar(50),
    @Cantidad int,
    @Tipo_Suelo varchar(50),
    @Fecha_Plantacion date,
    @Fecha_Cosecha date
AS
BEGIN
    UPDATE Siembra 
    SET Nombre_Cultivo = @Nombre_Cultivo,
        Tipo_Cultivo = @Tipo_Cultivo,
        Categoria_Cultivo = @Categoria_Cultivo,
        Estacion_Siembra = @Estacion_Siembra,
        Cantidad = @Cantidad,
        Tipo_Suelo = @Tipo_Suelo,
        Fecha_Plantacion = @Fecha_Plantacion,
        Fecha_Cosecha = @Fecha_Cosecha 
    WHERE ID_Cultivo = @ID_Cultivo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Actualizar_Terreno]
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
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Actualizar_Trabajador]
    @ID_Trabajador INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Cedula VARCHAR(11),
    @Sexo VARCHAR(10),
    @Email VARCHAR(100),
    @Fecha_Nac DATE,
    @Direccion VARCHAR(100),
    @Telefono VARCHAR(11),
    @Ciudad VARCHAR(100),
    @Sector VARCHAR(50),
    @Posicion VARCHAR(50),
    @Sueldo DECIMAL(12,2),
    @Fecha_Ingreso DATE
AS
BEGIN
    DECLARE @CedulaNueva VARCHAR(11) = LTRIM(RTRIM(@Cedula));
    DECLARE @EmailNuevo VARCHAR(100) = LOWER(LTRIM(RTRIM(@Email)));
    DECLARE @TelefonoNuevo VARCHAR(11) = LTRIM(RTRIM(@Telefono));
    DECLARE @CedulaActual VARCHAR(11);
    DECLARE @EmailActual VARCHAR(100);
    DECLARE @TelefonoActual VARCHAR(11);

    SELECT
        @CedulaActual = LTRIM(RTRIM(Cedula)),
        @EmailActual = LOWER(LTRIM(RTRIM(Email))),
        @TelefonoActual = LTRIM(RTRIM(Telefono))
    FROM dbo.Trabajador
    WHERE ID_Trabajador = @ID_Trabajador;

    IF @CedulaActual IS NULL
        THROW 51013, 'El trabajador seleccionado no existe.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Trabajador
        WHERE ID_Trabajador <> @ID_Trabajador
          AND @CedulaNueva <> ISNULL(@CedulaActual, '')
          AND LTRIM(RTRIM(Cedula)) = @CedulaNueva
    )
        THROW 51010, 'Ya existe otro trabajador con esa cédula.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Trabajador
        WHERE ID_Trabajador <> @ID_Trabajador
          AND @EmailNuevo <> ISNULL(@EmailActual, '')
          AND LOWER(LTRIM(RTRIM(Email))) = @EmailNuevo
    )
        THROW 51011, 'Ya existe otro trabajador con ese correo electrónico.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Trabajador
        WHERE ID_Trabajador <> @ID_Trabajador
          AND @TelefonoNuevo <> ISNULL(@TelefonoActual, '')
          AND LTRIM(RTRIM(Telefono)) = @TelefonoNuevo
    )
        THROW 51012, 'Ya existe otro trabajador con ese teléfono.', 1;

    UPDATE dbo.Trabajador
       SET Nombre = LTRIM(RTRIM(@Nombre)),
           Apellido = LTRIM(RTRIM(@Apellido)),
           Cedula = @CedulaNueva,
           Sexo = @Sexo,
           Email = @EmailNuevo,
           Fecha_Nac = @Fecha_Nac,
           Direccion = LTRIM(RTRIM(@Direccion)),
           Telefono = @TelefonoNuevo,
           Ciudad = LTRIM(RTRIM(@Ciudad)),
           Sector = LTRIM(RTRIM(@Sector)),
           Posicion = LTRIM(RTRIM(@Posicion)),
           Sueldo = @Sueldo,
           Fecha_Ingreso = @Fecha_Ingreso
     WHERE ID_Trabajador = @ID_Trabajador;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Asignar_Agricultor_Siembra]
    @ID_Agricultor INT,
    @ID_Cultivo INT,
    @Notas VARCHAR(200) = NULL
AS
BEGIN
    -- Verificar si ya existe la asignación
    IF EXISTS (SELECT 1 FROM Agricultor_Siembra 
               WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo)
    BEGIN
        UPDATE Agricultor_Siembra
        SET Fecha_Asignacion = GETDATE(),
            Notas = ISNULL(@Notas, Notas)
        WHERE ID_Agricultor = @ID_Agricultor AND ID_Cultivo = @ID_Cultivo;
    END
    ELSE
    BEGIN
        INSERT INTO Agricultor_Siembra (ID_Agricultor, ID_Cultivo, Fecha_Asignacion, Notas)
        VALUES (@ID_Agricultor, @ID_Cultivo, GETDATE(), @Notas);
    END
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Asignar_Gestion_Maquinaria]
    @ID_Trabajador INT, @ID_Maquina INT, @Fecha_Asignacion DATE,
    @Fecha_Prevista_Devolucion DATE=NULL, @Actividad VARCHAR(120),
    @Observaciones VARCHAR(200)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRANSACTION;
    IF NOT EXISTS(SELECT 1 FROM dbo.R_Maquinaria WITH(UPDLOCK,HOLDLOCK)
                  WHERE ID_Maquina=@ID_Maquina AND Estado='Disponible')
        THROW 51203, 'La maquinaria no está disponible.', 1;
    INSERT dbo.Asignacion_Maquinaria
      (ID_Trabajador,ID_Maquina,Fecha_Asignacion,Fecha_Prevista_Devolucion,
       Estado,Actividad,Observaciones)
    VALUES(@ID_Trabajador,@ID_Maquina,@Fecha_Asignacion,@Fecha_Prevista_Devolucion,
           'Asignada',@Actividad,@Observaciones);
    DECLARE @ID INT=SCOPE_IDENTITY();
    UPDATE dbo.R_Maquinaria SET Estado='Asignada' WHERE ID_Maquina=@ID_Maquina;
    COMMIT; SELECT @ID;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 2. Procedimiento para asignar maquinaria
CREATE   PROCEDURE [dbo].[Asignar_Maquinaria]
    @ID_Trabajador INT,
    @ID_Maquina INT,
    @Observaciones VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar que la máquina esté activa (disponible)
    IF NOT EXISTS (SELECT 1 FROM R_Maquinaria WHERE ID_Maquina = @ID_Maquina AND Estado = 'Activa')
    BEGIN
        RETURN -1; -- Máquina no disponible
    END
    
    BEGIN TRANSACTION;
    
    -- Registrar la asignación
    INSERT INTO Asignacion_Maquinaria (
        ID_Trabajador,
        ID_Maquina,
        Fecha_Asignacion,
        Estado,
        Observaciones
    )
    VALUES (
        @ID_Trabajador,
        @ID_Maquina,
        GETDATE(),
        'Asignada',
        @Observaciones
    );
    
    -- Actualizar el estado de la máquina a "Asignada" (no disponible)
    UPDATE R_Maquinaria
    SET Estado = 'Asignada'
    WHERE ID_Maquina = @ID_Maquina;
    
    COMMIT;
    
    RETURN SCOPE_IDENTITY(); -- Devolver el ID de la asignación creada
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Buscar_Agricultor]
    @Termino VARCHAR(50)
AS
BEGIN
    SELECT 
        A.ID_Agricultor AS [ID],
        A.Nombre,
        A.Apellido,
        A.Cedula,
        A.Telefono,
        A.Email,
        A.Ciudad,
        A.Sector,
        A.Fecha_Registro AS [Fecha de Registro],
        U.Usuario
    FROM Agricultor A
    INNER JOIN Usuario U ON A.ID_Usuario = U.ID_Usuario
    WHERE 
        A.Nombre LIKE '%' + @Termino + '%' OR
        A.Apellido LIKE '%' + @Termino + '%' OR
        A.Cedula LIKE '%' + @Termino + '%' OR
        A.Email LIKE '%' + @Termino + '%' OR
        U.Usuario LIKE '%' + @Termino + '%'
    ORDER BY A.ID_Agricultor ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Buscar_Insumo]
    @Termino varchar(50)
AS
BEGIN
    SELECT 
        Id_Insumo AS [ID],
        Nombre,
        Tipo AS [Tipo de Insumo],
        Costo,
        Fecha AS [Fecha de Adquisición],
        Cantidad
    FROM Insumos
    WHERE 
        Nombre LIKE '%' + @Termino + '%' OR
        Tipo LIKE '%' + @Termino + '%'
    ORDER BY Nombre ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Buscar_Plaga]
@buscador varchar(50)
as
select ID_CP,Tipo,Nombre_Comun,Nombre_Cientifico,Razgos,Huesped,Cura
 from Enfermedades
 where Nombre_Comun like '%'+ @buscador + '%' or Nombre_Cientifico like '%'+ @buscador+'%'
 order by Nombre_Comun asc, Nombre_Cientifico asc
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Buscar_Trabajador]
    @Buscador VARCHAR(100)
AS
BEGIN
    SELECT
        ID_Trabajador AS [ID],
        Nombre,
        Apellido,
        Cedula,
        Sexo,
        Email,
        Telefono,
        Posicion AS [Rol laboral],
        Sueldo,
        Fecha_Nac AS [Fecha de Nacimiento],
        Direccion,
        Ciudad,
        Sector,
        Fecha_Ingreso AS [Fecha de Ingreso]
    FROM dbo.Trabajador
    WHERE Estado = 'Activo'
      AND
      (
           Nombre LIKE '%' + @Buscador + '%'
        OR Apellido LIKE '%' + @Buscador + '%'
        OR Cedula LIKE '%' + @Buscador + '%'
        OR Email LIKE '%' + @Buscador + '%'
        OR Telefono LIKE '%' + @Buscador + '%'
        OR Posicion LIKE '%' + @Buscador + '%'
      )
    ORDER BY Apellido, Nombre;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[BuscarMaquinaria]
    @Buscador varchar(50)
AS
BEGIN
    SELECT 
        ID_Maquina AS [ID],
        Nombre_Maquina AS [Nombre],
        Tipo_Maquinaria AS [Tipo de maquinaria],
        Costo_Maquinaria AS [Costo],
        Fecha_Adquisicion AS [Fecha de adquisicion],
        Mantenimiento_Programado AS [Mantenimiento Programado]
    FROM R_Maquinaria 
    WHERE Nombre_Maquina LIKE '%' + @Buscador + '%';
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cambiar_Estado_Cultivo]
    @ID_Cultivo INT,
    @Estado_Cultivo VARCHAR(50)
AS
BEGIN
    -- Actualizar el estado del cultivo
    UPDATE Siembra
    SET Estado_Cultivo = @Estado_Cultivo
    WHERE ID_Cultivo = @ID_Cultivo;
    
    -- Si el estado es "Cosechado", verificar si ya existe en Cosecha y registrar si no existe
    IF @Estado_Cultivo = 'Cosechado'
    BEGIN
        -- Verificar si ya existe en Cosecha
        IF NOT EXISTS (SELECT 1 FROM Cosecha WHERE id_cultivo = @ID_Cultivo)
        BEGIN
            -- No existe, insertar en Cosecha con estado "En Proceso"
            INSERT INTO Cosecha (Estado_Cosecha, id_cultivo)
            VALUES ('En Proceso', @ID_Cultivo);
        END
    END
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Cambiar_Estado_Equipo]
    @Id_Equipo INT,
    @Estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Estado NOT IN ('Activo', 'En mantenimiento', 'Inactivo')
        THROW 51303, 'Estado de equipo no valido.', 1;

    UPDATE dbo.Equipos
       SET Estado = @Estado
     WHERE Id_Equipos = @Id_Equipo;

    IF @@ROWCOUNT = 0
        THROW 51302, 'El equipo seleccionado no existe.', 1;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Cambiar_Estado_Maquinaria]
    @ID_Maquina INT, @Estado VARCHAR(20)
AS
BEGIN
    IF EXISTS(SELECT 1 FROM dbo.Asignacion_Maquinaria
              WHERE ID_Maquina=@ID_Maquina AND Estado='Asignada')
        THROW 51202, 'No puede cambiar el estado mientras exista una asignación activa.', 1;
    UPDATE dbo.R_Maquinaria SET Estado=@Estado WHERE ID_Maquina=@ID_Maquina;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para marcar una alerta como completada
CREATE   PROCEDURE [dbo].[Completar_Alerta]
    @ID_Alerta INT
AS
BEGIN
    UPDATE Alertas SET
        Estado = 'Completada'
    WHERE ID_Alerta = @ID_Alerta;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Agricultor]
    @ID_Agricultor INT
AS
BEGIN
    BEGIN TRANSACTION
        -- Obtener el ID de usuario asociado
        DECLARE @ID_Usuario INT;
        SELECT @ID_Usuario = ID_Usuario FROM Agricultor WHERE ID_Agricultor = @ID_Agricultor;
        
        -- Eliminar el agricultor
        DELETE FROM Agricultor WHERE ID_Agricultor = @ID_Agricultor;
        
        -- Eliminar el usuario asociado si existe
        IF @ID_Usuario IS NOT NULL
        BEGIN
            DELETE FROM Usuario WHERE ID_Usuario = @ID_Usuario;
        END
    COMMIT TRANSACTION;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para eliminar una alerta
CREATE   PROCEDURE [dbo].[Eliminar_Alerta]
    @ID_Alerta INT
AS
BEGIN
    DELETE FROM Alertas WHERE ID_Alerta = @ID_Alerta;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Cosecha]
    @ID_Cosecha INT
AS
BEGIN
    -- Obtener el ID_Cultivo antes de eliminar
    DECLARE @ID_Cultivo INT;
    SELECT @ID_Cultivo = id_cultivo FROM Cosecha WHERE ID_Cosecha = @ID_Cosecha;
    
    -- Eliminar la cosecha
    DELETE FROM Cosecha WHERE ID_Cosecha = @ID_Cosecha;
    
    -- Actualizar el estado del cultivo a "Activo" de nuevo
    IF @ID_Cultivo IS NOT NULL
    BEGIN
        UPDATE Siembra 
        SET Estado_Cultivo = 'Activo' 
        WHERE ID_Cultivo = @ID_Cultivo;
    END
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Eliminar_Equipo_Mantenimiento]
    @Id_Equipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Equipos WHERE Id_Equipos = @Id_Equipo)
        THROW 51302, 'El equipo seleccionado no existe.', 1;

    IF EXISTS (SELECT 1 FROM dbo.Mantenimiento_Equipos WHERE Id_Equipo = @Id_Equipo)
    BEGIN
        UPDATE dbo.Equipos
           SET Estado = 'Inactivo'
         WHERE Id_Equipos = @Id_Equipo;

        SELECT 'El equipo tiene historial, se marco como inactivo correctamente.';
        RETURN;
    END;

    BEGIN TRANSACTION;
        IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL
        BEGIN
            DELETE dbo.Movimiento_Financiero
             WHERE Referencia = 'EQUIPO-' + CONVERT(VARCHAR(20), @Id_Equipo);
        END;

        DELETE dbo.Equipos
         WHERE Id_Equipos = @Id_Equipo;
    COMMIT TRANSACTION;

    SELECT 'Equipo eliminado correctamente.';
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Eliminar_Gestion_Maquinaria]
    @ID_Maquina INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS(SELECT 1 FROM dbo.R_Maquinaria WHERE ID_Maquina=@ID_Maquina)
        THROW 51206, 'La maquinaria seleccionada no existe.', 1;

    IF EXISTS(SELECT 1 FROM dbo.Asignacion_Maquinaria
              WHERE ID_Maquina=@ID_Maquina AND Estado='Asignada')
        THROW 51207, 'No puede eliminar una maquinaria con una asignación activa.', 1;

    IF EXISTS(SELECT 1 FROM dbo.Asignacion_Maquinaria WHERE ID_Maquina=@ID_Maquina)
       OR EXISTS(SELECT 1 FROM dbo.Mantenimiento_Maquinaria WHERE ID_Maquina=@ID_Maquina)
    BEGIN
        UPDATE dbo.R_Maquinaria
        SET Estado='Inactiva'
        WHERE ID_Maquina=@ID_Maquina;

        SELECT 'La maquinaria tiene historial, se marcó como inactiva para conservar los reportes.';
        RETURN;
    END;

    BEGIN TRANSACTION;
        IF OBJECT_ID('dbo.Movimiento_Financiero','U') IS NOT NULL
            DELETE dbo.Movimiento_Financiero
            WHERE Referencia='MAQUINARIA-'+CONVERT(VARCHAR(20),@ID_Maquina);

        DELETE dbo.R_Maquinaria
        WHERE ID_Maquina=@ID_Maquina;
    COMMIT;

    SELECT 'Maquinaria eliminada correctamente.';
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Insumo]
    @Id_Insumo int
AS
BEGIN
    DELETE FROM Insumos
    WHERE Id_Insumo = @Id_Insumo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Eliminar_Mantenimiento_Equipo]
    @Id_Mantenimiento INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Id_Equipo INT;

    SELECT @Id_Equipo = Id_Equipo
    FROM dbo.Mantenimiento_Equipos
    WHERE Id_Mantenimiento = @Id_Mantenimiento;

    IF @Id_Equipo IS NULL
        THROW 51306, 'El mantenimiento seleccionado no existe.', 1;

    BEGIN TRANSACTION;

    IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL
    BEGIN
        DELETE dbo.Movimiento_Financiero
         WHERE Referencia = 'MANT-EQ-' + CONVERT(VARCHAR(20), @Id_Mantenimiento);
    END;

    DELETE dbo.Mantenimiento_Equipos
     WHERE Id_Mantenimiento = @Id_Mantenimiento;

    DECLARE @NuevoProximo DATE;

    SELECT TOP 1 @NuevoProximo = Proximo_Mantenimiento
    FROM dbo.Mantenimiento_Equipos
    WHERE Id_Equipo = @Id_Equipo
      AND Proximo_Mantenimiento IS NOT NULL
    ORDER BY Fecha_Mantenimiento DESC, Id_Mantenimiento DESC;

    UPDATE dbo.Equipos
       SET Proximo_Mantenimiento = @NuevoProximo
     WHERE Id_Equipos = @Id_Equipo;

    COMMIT TRANSACTION;

    SELECT 'Mantenimiento eliminado correctamente.';
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Eliminar_Produccion]
    @ID_Produccion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Produccion WHERE ID_Produccion = @ID_Produccion)
        THROW 51403, 'La produccion seleccionada no existe.', 1;

    DELETE FROM dbo.Produccion
     WHERE ID_Produccion = @ID_Produccion;

    SELECT 'Produccion eliminada correctamente.';
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_SeguimientoCrecimiento]
    @ID_Seguimiento INT
AS
BEGIN
    DELETE FROM SeguimientoCrecimiento
    WHERE ID_Seguimiento = @ID_Seguimiento;
    
    RETURN 'Eliminación de seguimiento exitosa';
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Eliminar_Siembra]
    @ID_Cultivo int
AS
BEGIN
    DELETE FROM Siembra WHERE ID_Cultivo = @ID_Cultivo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Terreno]
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
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Eliminar_Trabajador]
    @ID_Trabajador INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Trabajador WHERE ID_Trabajador = @ID_Trabajador)
        THROW 51020, 'El trabajador seleccionado no existe.', 1;

    IF OBJECT_ID('dbo.Asignacion_Maquinaria', 'U') IS NOT NULL
       AND EXISTS
       (
           SELECT 1
           FROM dbo.Asignacion_Maquinaria
           WHERE ID_Trabajador = @ID_Trabajador
             AND Estado = 'Asignada'
       )
        THROW 51021, 'No se puede eliminar el trabajador porque tiene maquinaria asignada. Primero quite o finalice esa asignación.', 1;

    IF (OBJECT_ID('dbo.Asignacion_Maquinaria', 'U') IS NOT NULL
        AND EXISTS (SELECT 1 FROM dbo.Asignacion_Maquinaria WHERE ID_Trabajador = @ID_Trabajador))
       OR (OBJECT_ID('dbo.Pago_Trabajador', 'U') IS NOT NULL
        AND EXISTS (SELECT 1 FROM dbo.Pago_Trabajador WHERE ID_Trabajador = @ID_Trabajador))
    BEGIN
        UPDATE dbo.Trabajador
           SET Estado = 'Inactivo'
         WHERE ID_Trabajador = @ID_Trabajador;

        SELECT 'El trabajador tiene historial, se marcó como inactivo correctamente.';
        RETURN;
    END;

    DELETE FROM dbo.Trabajador
    WHERE ID_Trabajador = @ID_Trabajador;

    SELECT 'Trabajador eliminado correctamente.';
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Estadisticas_Agricultor]
    @ID_Agricultor INT
AS
BEGIN
    -- Total de siembras asignadas
    SELECT COUNT(*) AS [Total Siembras Asignadas]
    FROM Agricultor_Siembra
    WHERE ID_Agricultor = @ID_Agricultor;
    
    -- Total de trabajadores pagados (distintos)
    SELECT COUNT(DISTINCT ID_Trabajador) AS [Total Trabajadores Pagados]
    FROM Pago_Trabajador
    WHERE ID_Agricultor = @ID_Agricultor;
    
    -- Total de recursos administrados por tipo
    SELECT 
        Tipo_Recurso,
        COUNT(*) AS [Cantidad],
        SUM(Costo) AS [Costo Total]
    FROM Alquiler_Administracion
    WHERE ID_Agricultor = @ID_Agricultor
    GROUP BY Tipo_Recurso;
    
    -- Siembras próximas a cosechar (en los próximos 30 días)
    SELECT 
        S.ID_Cultivo,
        S.Nombre_Cultivo,
        S.Fecha_Cosecha,
        DATEDIFF(DAY, GETDATE(), S.Fecha_Cosecha) AS [Días Restantes]
    FROM Agricultor_Siembra ASI
    INNER JOIN Siembra S ON ASI.ID_Cultivo = S.ID_Cultivo
    WHERE ASI.ID_Agricultor = @ID_Agricultor
    AND S.Fecha_Cosecha BETWEEN GETDATE() AND DATEADD(DAY, 30, GETDATE())
    ORDER BY S.Fecha_Cosecha;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para filtrar siembras por fecha
CREATE PROCEDURE [dbo].[Filtrar_Siembras_Por_Fecha]
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        s.ID_Cultivo,
        s.Nombre_Cultivo,
        s.Tipo_Cultivo,
        s.Categoria_Cultivo,
        s.Estacion_Siembra,
        s.Cantidad,
        s.Tipo_Suelo,
        s.Fecha_Plantacion,
        s.Fecha_Cosecha,
        COALESCE(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
        s.Metodo_Siembra,
        s.Profundidad,
        s.Humedad_Suelo,
        s.Costo_Siembra,
        f.Tipo_Fertilizante,
        f.Metodo_Fertilizacion,
        f.Cantidad_Fertilizante,
        f.Costo_Fertilizacion,
        f.Fecha_Aplicacion,
        DATEDIFF(DAY, s.Fecha_Plantacion, GETDATE()) AS Dias_Transcurridos,
        DATEDIFF(DAY, GETDATE(), s.Fecha_Cosecha) AS Dias_Restantes,
        DATEDIFF(DAY, s.Fecha_Plantacion, s.Fecha_Cosecha) AS Duracion_Total
    FROM 
        Siembra s
    LEFT JOIN 
        Fertilizacion f ON s.ID_Cultivo = f.ID_Cultivo
    WHERE
        s.Fecha_Plantacion >= @FechaInicio AND 
        s.Fecha_Plantacion <= @FechaFin
    ORDER BY 
        s.Fecha_Plantacion DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 3. Procedimiento para finalizar asignación
CREATE   PROCEDURE [dbo].[Finalizar_Asignacion]
    @ID_Asignacion INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ID_Maquina INT;
    
    -- Obtener el ID de la máquina de esta asignación
    SELECT @ID_Maquina = ID_Maquina 
    FROM Asignacion_Maquinaria 
    WHERE ID_Asignacion = @ID_Asignacion AND Estado = 'Asignada';
    
    -- Si no encontramos la asignación o ya está finalizada
    IF @ID_Maquina IS NULL
    BEGIN
        RETURN -1; -- Asignación no encontrada o ya finalizada
    END
    
    BEGIN TRANSACTION;
    
    -- Actualizar el estado de la asignación
    UPDATE Asignacion_Maquinaria
    SET 
        Estado = 'Finalizada',
        Fecha_Devolucion = GETDATE()
    WHERE ID_Asignacion = @ID_Asignacion;
    
    -- Restaurar el estado de la máquina a "Activa" (disponible)
    UPDATE R_Maquinaria
    SET Estado = 'Activa'
    WHERE ID_Maquina = @ID_Maquina;
    
    COMMIT;
    
    RETURN 1; -- Operación exitosa
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Finalizar_Gestion_Asignacion] @ID_Asignacion INT
AS
BEGIN
    SET XACT_ABORT ON; BEGIN TRANSACTION;
    DECLARE @ID_Maquina INT;
    SELECT @ID_Maquina=ID_Maquina FROM dbo.Asignacion_Maquinaria WITH(UPDLOCK,HOLDLOCK)
    WHERE ID_Asignacion=@ID_Asignacion AND Estado='Asignada';
    IF @ID_Maquina IS NULL THROW 51204, 'La asignación no está activa.', 1;
    UPDATE dbo.Asignacion_Maquinaria SET Estado='Finalizada',Fecha_Devolucion=GETDATE()
    WHERE ID_Asignacion=@ID_Asignacion;
    UPDATE dbo.R_Maquinaria SET Estado='Disponible' WHERE ID_Maquina=@ID_Maquina;
    COMMIT;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Historial_Actividades_Agricultor]
    @ID_Agricultor INT
AS
BEGIN
    -- Historial de pagos a trabajadores
    SELECT 
        'Pago a Trabajador' AS [Tipo de Actividad],
        T.Nombre + ' ' + T.Apellido AS [Detalle],
        P.Monto AS [Monto],
        P.Fecha_Pago AS [Fecha],
        P.Descripcion
    FROM Pago_Trabajador P
    INNER JOIN Trabajador T ON P.ID_Trabajador = T.ID_Trabajador
    WHERE P.ID_Agricultor = @ID_Agricultor
    
    UNION ALL
    
    -- Historial de alquileres y administración de recursos
    SELECT 
        'Administración de ' + AA.Tipo_Recurso AS [Tipo de Actividad],
        CASE 
            WHEN AA.Tipo_Recurso = 'Maquinaria' THEN 
                (SELECT Nombre_Maquina FROM R_Maquinaria WHERE ID_Maquina = AA.ID_Recurso)
            WHEN AA.Tipo_Recurso = 'Terreno' THEN 
                'Terreno ID: ' + CAST(AA.ID_Recurso AS VARCHAR)
            WHEN AA.Tipo_Recurso = 'Equipo' THEN 
                (SELECT Nombre FROM Equipos WHERE Id_Equipos = AA.ID_Recurso)
            ELSE 'Recurso ID: ' + CAST(AA.ID_Recurso AS VARCHAR)
        END AS [Detalle],
        AA.Costo AS [Monto],
        AA.Fecha_Inicio AS [Fecha],
        AA.Descripcion
    FROM Alquiler_Administracion AA
    WHERE AA.ID_Agricultor = @ID_Agricultor
    
    ORDER BY [Fecha] DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Ahora creamos el procedimiento con la sintaxis correcta
CREATE PROCEDURE [dbo].[Ingresar_Maquinaria]
    @nombre_maquina varchar(100),
    @tipo_maquina varchar(50),
    @costo_maquina money,
    @fecha_adquisicion date,
    @Mantenimiento_Programado varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar si existe la columna Estado en la tabla
    DECLARE @TieneEstado BIT = 0;
    
    IF EXISTS (SELECT 1 FROM sys.columns 
              WHERE name = 'Estado' AND object_id = OBJECT_ID('R_Maquinaria'))
    BEGIN
        SET @TieneEstado = 1;
    END
    
    -- Insertar con o sin el campo Estado según corresponda
    IF @TieneEstado = 1
    BEGIN
        INSERT INTO R_Maquinaria (
            Nombre_Maquina, 
            Tipo_Maquinaria, 
            Costo_Maquinaria, 
            Fecha_Adquisicion, 
            Mantenimiento_Programado,
            Estado)
        VALUES (
            @nombre_maquina, 
            @tipo_maquina, 
            @costo_maquina, 
            @fecha_adquisicion, 
            @Mantenimiento_Programado,
            'Activa');
    END
    ELSE
    BEGIN
        INSERT INTO R_Maquinaria (
            Nombre_Maquina, 
            Tipo_Maquinaria, 
            Costo_Maquinaria, 
            Fecha_Adquisicion, 
            Mantenimiento_Programado)
        VALUES (
            @nombre_maquina, 
            @tipo_maquina, 
            @costo_maquina, 
            @fecha_adquisicion, 
            @Mantenimiento_Programado);
    END
    
    -- En lugar de usar RETURN con un valor numérico, devolvemos un mensaje
    IF @@ROWCOUNT > 0
        SELECT 'Éxito' AS Resultado;
    ELSE
        SELECT 'Fracaso' AS Resultado;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para crear una nueva alerta
CREATE   PROCEDURE [dbo].[Insertar_Alerta]
    @ID_Cultivo INT,
    @Titulo VARCHAR(100),
    @Descripcion VARCHAR(255),
    @Fecha_Programada DATE,
    @Tipo_Alerta VARCHAR(50),
    @Prioridad VARCHAR(10) = 'Media',
    @Dias_Anticipacion INT = 7
AS
BEGIN
    INSERT INTO Alertas 
        (ID_Cultivo, Titulo, Descripcion, Fecha_Programada, Tipo_Alerta, Prioridad, Dias_Anticipacion)
    VALUES 
        (@ID_Cultivo, @Titulo, @Descripcion, @Fecha_Programada, @Tipo_Alerta, @Prioridad, @Dias_Anticipacion);
    
    RETURN SCOPE_IDENTITY();
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Cosecha]
    @ID_Cultivo INT,
    @Estado_Cosecha VARCHAR(25),
    @Cantidad_Cosechada DECIMAL(10, 2),
    @Rendimiento DECIMAL(10, 2),
    @Calidad DECIMAL(5, 2),
    @Observaciones VARCHAR(200),
    @Fecha_Cosecha DATE,
    @Costo_Cosecha DECIMAL(10, 2),
    @Precio_Venta DECIMAL(10, 2),
    @Beneficio DECIMAL(10, 2)
AS
BEGIN
    -- Insertar en la tabla Cosecha
    INSERT INTO Cosecha (
        id_cultivo,
        Estado_Cosecha,
        Cantidad_Cosechada,
        Rendimiento,
        Calidad,
        Observaciones,
        Fecha_Cosecha,
        Costo_Cosecha,
        Precio_Venta,
        Beneficio
    )
    VALUES (
        @ID_Cultivo,
        @Estado_Cosecha,
        @Cantidad_Cosechada,
        @Rendimiento,
        @Calidad,
        @Observaciones,
        @Fecha_Cosecha,
        @Costo_Cosecha,
        @Precio_Venta,
        @Beneficio
    );
    
    -- Actualizar el estado del cultivo a "Cosechado"
    UPDATE Siembra
    SET Estado_Cultivo = 'Cosechado'
    WHERE ID_Cultivo = @ID_Cultivo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Fertilizacion]
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
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Insumo]
    @Nombre varchar(100),
    @Tipo varchar(50),
    @Costo int,
    @Fecha date,
    @Cantidad int,
    @Id_Insumo_Out int OUTPUT
AS
BEGIN
    INSERT INTO Insumos (Nombre, Tipo, Costo, Fecha, Cantidad)
    VALUES (@Nombre, @Tipo, @Costo, @Fecha, @Cantidad);
    
    SET @Id_Insumo_Out = SCOPE_IDENTITY();
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Insertar_RP]
    @Nombre_Cultivo varchar(100),
    @Tipo_Cultivo varchar(50),
    @Categoria_Cultivo varchar(50),
    @Estacion_Siembra varchar(50),
    @Tipo_Suelo varchar(50),
    @Duracion varchar(50)
AS
BEGIN
    INSERT INTO RP (Nombre_Cultivo, Tipo_Cultivo, Categoria_Cultivo, Estacion, Tipo_Suelo, Duracion)
    VALUES (@Nombre_Cultivo, @Tipo_Cultivo, @Categoria_Cultivo, @Estacion_Siembra, @Tipo_Suelo, @Duracion);
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_SeguimientoCrecimiento]
    @ID_Cultivo INT,
    @Fecha_Registro DATETIME,
    @Etapa_Crecimiento VARCHAR(50),
    @Altura_Planta DECIMAL(10, 2),
    @Num_Hojas INT,
    @Estado_Salud VARCHAR(20),
    @Observaciones VARCHAR(200),
    @Porcentaje_Crecimiento DECIMAL(5, 2),
    @Problemas_Detectados VARCHAR(200),
    @Acciones_Tomadas VARCHAR(200)
AS
BEGIN
    INSERT INTO SeguimientoCrecimiento (
        ID_Cultivo, 
        Fecha_Registro, 
        Etapa_Crecimiento, 
        Altura_Planta, 
        Num_Hojas, 
        Estado_Salud, 
        Observaciones, 
        Porcentaje_Crecimiento, 
        Problemas_Detectados, 
        Acciones_Tomadas
    )
    VALUES (
        @ID_Cultivo, 
        @Fecha_Registro, 
        @Etapa_Crecimiento, 
        @Altura_Planta, 
        @Num_Hojas, 
        @Estado_Salud, 
        @Observaciones, 
        @Porcentaje_Crecimiento, 
        @Problemas_Detectados, 
        @Acciones_Tomadas
    );
    
    RETURN 'Registro de seguimiento exitoso';
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Siembra]
    @Nombre_Cultivo VARCHAR(100),
    @Tipo_Cultivo VARCHAR(50),
    @Categoria_Cultivo VARCHAR(50),
    @Estacion_Siembra VARCHAR(50),
    @Cantidad INT,
    @Tipo_Suelo VARCHAR(50),
    @Fecha_Plantacion DATE,
    @Fecha_Cosecha DATE,
    @Metodo_Siembra VARCHAR(50) = NULL,
    @Profundidad VARCHAR(50) = NULL,
    @Humedad_Suelo DECIMAL(10, 2) = NULL,
    @Costo_Siembra DECIMAL(10, 2) = NULL,
    @Tipo_Fertilizante VARCHAR(50) = NULL,
    @Metodo_Fertilizacion VARCHAR(50) = NULL,
    @Cantidad_Fertilizante INT = NULL,
    @Costo_Fertilizacion DECIMAL(10, 2) = NULL,
    @Fecha_Aplicacion DATE = NULL,
    @ID_Terreno INT = NULL,
    @Densidad_Siembra DECIMAL(10, 2) = NULL,
    @ID_Cultivo_Out INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar en la tabla Siembra
    INSERT INTO Siembra (
        Nombre_Cultivo,
        Tipo_Cultivo,
        Categoria_Cultivo,
        Estacion_Siembra,
        Cantidad,
        Tipo_Suelo,
        Fecha_Plantacion,
        Fecha_Cosecha,
        Metodo_Siembra,
        Profundidad,
        Humedad_Suelo,
        Costo_Siembra,
        Estado_Cultivo,
        ID_Terreno
    )
    VALUES (
        @Nombre_Cultivo,
        @Tipo_Cultivo,
        @Categoria_Cultivo,
        @Estacion_Siembra,
        @Cantidad,
        @Tipo_Suelo,
        @Fecha_Plantacion,
        @Fecha_Cosecha,
        @Metodo_Siembra,
        @Profundidad,
        @Humedad_Suelo,
        @Costo_Siembra,
        'Activo',
        @ID_Terreno
    );

    -- Obtener el ID del cultivo recién insertado
    SET @ID_Cultivo_Out = SCOPE_IDENTITY();

    -- Si hay datos de fertilización, los insertamos en la tabla correspondiente
    IF @Tipo_Fertilizante IS NOT NULL OR @Metodo_Fertilizacion IS NOT NULL
    BEGIN
        INSERT INTO Fertilizacion (
            ID_Cultivo,
            Tipo_Fertilizante,
            Metodo_Fertilizacion,
            Cantidad_Fertilizante,
            Costo_Fertilizacion,
            Fecha_Aplicacion
        )
        VALUES (
            @ID_Cultivo_Out,
            @Tipo_Fertilizante,
            @Metodo_Fertilizacion,
            @Cantidad_Fertilizante,
            @Costo_Fertilizacion,
            @Fecha_Aplicacion
        );
    END
    
    -- Si hay un terreno asignado, actualizar su estado
    IF @ID_Terreno IS NOT NULL
    BEGIN
        EXEC Actualizar_Estado_Terreno @ID_Terreno, 'En uso';
    END
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Terreno]
    @Nombre VARCHAR(100),
    @Area DECIMAL(10, 2),
    @UnidadMedida VARCHAR(20),
    @TipoSuelo VARCHAR(50),
    @pH DECIMAL(4, 2),
    @Pendiente DECIMAL(5, 2),
    @Humedad DECIMAL(5, 2),
    @Ubicacion VARCHAR(200),
    @Propietario VARCHAR(100),
    @FechaRegistro DATETIME,
    @EstadoActual VARCHAR(20),
    @Observaciones VARCHAR(500) = NULL,
    @ID_Terreno_Out INT OUTPUT
AS
BEGIN
    -- Ahora incluimos todas las columnas
    INSERT INTO Terrenos
    (
        Nombre_Terreno,
        Ubicacion,
        Superficie_Total,
        Unidad_Medida,
        Tipo_Suelo,
        Pendiente,
        pH_Suelo,
        Humedad,
        Propietario,
        Fecha_Registro,
        Estado,
        Observaciones
    )
    VALUES
    (
        @Nombre,
        @Ubicacion,
        @Area,
        @UnidadMedida,
        @TipoSuelo,
        CONVERT(VARCHAR(50), @Pendiente),
        @pH,
        @Humedad,
        @Propietario,
        @FechaRegistro,
        @EstadoActual,
        @Observaciones
    );

    SET @ID_Terreno_Out = SCOPE_IDENTITY();
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Insertar_Trabajador]
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Cedula VARCHAR(11),
    @Sexo VARCHAR(10),
    @Email VARCHAR(100),
    @Fecha_Nac DATE,
    @Direccion VARCHAR(100),
    @Telefono VARCHAR(11),
    @Ciudad VARCHAR(100),
    @Sector VARCHAR(50),
    @Posicion VARCHAR(50),
    @Sueldo DECIMAL(12,2),
    @Fecha_Ingreso DATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.Trabajador WHERE Cedula = @Cedula)
        THROW 51010, 'Ya existe un trabajador con esa cédula.', 1;

    IF EXISTS (SELECT 1 FROM dbo.Trabajador WHERE LOWER(Email) = LOWER(@Email))
        THROW 51011, 'Ya existe un trabajador con ese correo electrónico.', 1;

    IF EXISTS (SELECT 1 FROM dbo.Trabajador WHERE Telefono = @Telefono)
        THROW 51012, 'Ya existe un trabajador con ese teléfono.', 1;

    INSERT INTO dbo.Trabajador
    (
        Nombre, Apellido, Cedula, Sexo, Email, Fecha_Nac, Direccion,
        Telefono, Ciudad, Sector, Posicion, Sueldo, Fecha_Ingreso
    )
    VALUES
    (
        @Nombre, @Apellido, @Cedula, @Sexo, LOWER(@Email), @Fecha_Nac, @Direccion,
        @Telefono, @Ciudad, @Sector, @Posicion, @Sueldo, @Fecha_Ingreso
    );
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Alquiler_Administracion]
    @ID_Agricultor INT = NULL,
    @Estado VARCHAR(20) = NULL
AS
BEGIN
    SELECT 
        AA.ID_Administracion AS [ID],
        A.Nombre + ' ' + A.Apellido AS [Agricultor],
        AA.Tipo_Recurso AS [Tipo de Recurso],
        AA.ID_Recurso AS [ID del Recurso],
        AA.Descripcion,
        AA.Costo,
        AA.Fecha_Inicio AS [Fecha de Inicio],
        AA.Fecha_Fin AS [Fecha de Fin],
        AA.Estado
    FROM Alquiler_Administracion AA
    INNER JOIN Agricultor A ON AA.ID_Agricultor = A.ID_Agricultor
    WHERE (@ID_Agricultor IS NULL OR AA.ID_Agricultor = @ID_Agricultor)
    AND (@Estado IS NULL OR AA.Estado = @Estado)
    ORDER BY AA.Fecha_Inicio DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 4. Procedimiento para listar asignaciones activas
CREATE   PROCEDURE [dbo].[Listar_Asignaciones_Activas]
AS
BEGIN
    SELECT
        A.ID_Asignacion,
        T.ID_Trabajador,
        T.Nombre + ' ' + T.Apellido AS Nombre_Trabajador,
        T.Posicion,
        M.ID_Maquina,
        M.Nombre_Maquina,
        M.Tipo_Maquinaria,
        A.Fecha_Asignacion,
        A.Estado AS Estado_Asignacion,
        M.Estado AS Estado_Maquina,
        A.Observaciones
    FROM Asignacion_Maquinaria A
    INNER JOIN Trabajador T ON A.ID_Trabajador = T.ID_Trabajador
    INNER JOIN R_Maquinaria M ON A.ID_Maquina = M.ID_Maquina
    WHERE A.Estado = 'Asignada'
    ORDER BY A.Fecha_Asignacion DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Asignaciones_Maquinaria] @SoloActivas BIT=1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.ID_Asignacion AS [ID], A.ID_Maquina AS [ID Máquina],
      M.Codigo AS [Código],M.Nombre_Maquina AS [Maquinaria],
      T.Nombre+' '+T.Apellido AS [Responsable],ISNULL(T.Posicion,'') AS [Rol],
      A.Actividad,A.Fecha_Asignacion AS [Asignación],
      A.Fecha_Prevista_Devolucion AS [Devolución prevista],
      A.Fecha_Devolucion AS [Devolución],A.Estado,ISNULL(A.Observaciones,'') AS [Observaciones]
    FROM dbo.Asignacion_Maquinaria A
    JOIN dbo.R_Maquinaria M ON M.ID_Maquina=A.ID_Maquina
    JOIN dbo.Trabajador T ON T.ID_Trabajador=A.ID_Trabajador
    WHERE @SoloActivas=0 OR A.Estado='Asignada'
    ORDER BY A.Fecha_Asignacion DESC,A.ID_Asignacion DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Cosechas]
AS
BEGIN
    SELECT
        C.ID_Cosecha AS [ID],
        S.Nombre_Cultivo AS [Cultivo],
        S.Tipo_Cultivo AS [Tipo],
        S.Categoria_Cultivo AS [Categoría],
        C.Estado_Cosecha AS [Estado],
        C.Cantidad_Cosechada AS [Cantidad Cosechada],
        S.Cantidad AS [Cantidad Sembrada],
        C.Rendimiento AS [Rendimiento %],
        C.Calidad AS [Calidad],
        C.Fecha_Cosecha AS [Fecha Cosecha],
        C.Costo_Cosecha AS [Costo Cosecha],
        C.Precio_Venta AS [Precio Venta],
        C.Beneficio AS [Beneficio]
    FROM Cosecha C
    INNER JOIN Siembra S ON C.id_cultivo = S.ID_Cultivo
    ORDER BY C.Fecha_Cosecha DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Cosechas_Para_Produccion]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.ID_Cosecha,
        C.id_cultivo AS ID_Cultivo,
        S.Nombre_Cultivo,
        S.Tipo_Cultivo,
        S.Categoria_Cultivo,
        ISNULL(C.Cantidad_Cosechada, 0) AS Cantidad_Cosechada,
        ISNULL(C.Rendimiento, 0) AS Rendimiento,
        ISNULL(C.Calidad, 0) AS Calidad,
        ISNULL(C.Costo_Cosecha, 0) AS Costo_Cosecha,
        ISNULL(S.Costo_Siembra, 0) AS Costo_Siembra,
        ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) AS Fecha_Cosecha,
        CONVERT(VARCHAR(20), C.ID_Cosecha) + ' - ' +
            ISNULL(S.Nombre_Cultivo, 'Cultivo') + ' (' +
            CONVERT(VARCHAR(10), ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha), 103) + ')' AS Cosecha
    FROM dbo.Cosecha C
    INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
    ORDER BY ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha) DESC, C.ID_Cosecha DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Cultivos_Disponibles]
AS
BEGIN
    SELECT 
        S.ID_Cultivo,
        S.Nombre_Cultivo,
        S.Tipo_Cultivo,
        S.Categoria_Cultivo,
        S.Cantidad,
        S.Tipo_Suelo,
        S.Fecha_Plantacion,
        S.Fecha_Cosecha,
        S.Costo_Siembra,
        S.Estado_Cultivo
    FROM Siembra S
    LEFT JOIN Cosecha C ON S.ID_Cultivo = C.id_cultivo
    WHERE C.id_cultivo IS NULL
    ORDER BY 
        CASE 
            WHEN S.Estado_Cultivo = 'Cosechado' THEN 0  -- Priorizar los ya cosechados
            ELSE 1
        END,
        S.Fecha_Plantacion DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Equipos_Mantenimiento]
    @Busqueda VARCHAR(100) = NULL,
    @Estado VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id_Equipos AS [ID],
        Codigo,
        Nombre,
        Tipo_Equipo AS [Tipo],
        Cantidad,
        Costo_Equipo AS [Costo],
        Fecha_Adquisicion AS [Adquisicion],
        Proximo_Mantenimiento AS [Proximo mantenimiento],
        ISNULL(Ubicacion, '') AS [Ubicacion],
        Estado,
        ISNULL(Observaciones, '') AS [Observaciones]
    FROM dbo.Equipos
    WHERE (@Estado IS NULL OR Estado = @Estado)
      AND
      (
          @Busqueda IS NULL
          OR Codigo LIKE '%' + @Busqueda + '%'
          OR Nombre LIKE '%' + @Busqueda + '%'
          OR Tipo_Equipo LIKE '%' + @Busqueda + '%'
          OR Ubicacion LIKE '%' + @Busqueda + '%'
      )
    ORDER BY
        CASE
            WHEN Estado <> 'Inactivo'
             AND Proximo_Mantenimiento IS NOT NULL
             AND Proximo_Mantenimiento <= DATEADD(DAY, 30, CONVERT(DATE, GETDATE()))
                THEN 0
            WHEN Estado <> 'Inactivo' THEN 1
            ELSE 2
        END,
        Nombre;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Gestion_Maquinaria]
    @Busqueda VARCHAR(100) = NULL,
    @Tipo VARCHAR(50) = NULL,
    @Estado VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_Maquina AS [ID], Codigo AS [Código], Nombre_Maquina AS [Nombre],
           Tipo_Maquinaria AS [Tipo], ISNULL(Marca,'') AS [Marca],
           ISNULL(Modelo,'') AS [Modelo], ISNULL(Numero_Serie,'') AS [Serie],
           Costo_Maquinaria AS [Costo], Fecha_Adquisicion AS [Adquisición],
           Mantenimiento_Programado AS [Frecuencia],
           Proximo_Mantenimiento AS [Próximo mantenimiento], Horometro AS [Horómetro],
           ISNULL(Ubicacion,'') AS [Ubicación], Estado,
           ISNULL(Observaciones,'') AS [Observaciones]
    FROM dbo.R_Maquinaria
    WHERE (@Tipo IS NULL OR Tipo_Maquinaria=@Tipo)
      AND (@Estado IS NULL OR Estado=@Estado)
      AND (@Busqueda IS NULL OR Codigo LIKE '%'+@Busqueda+'%'
           OR Nombre_Maquina LIKE '%'+@Busqueda+'%'
           OR Marca LIKE '%'+@Busqueda+'%' OR Modelo LIKE '%'+@Busqueda+'%'
           OR Numero_Serie LIKE '%'+@Busqueda+'%')
    ORDER BY CASE WHEN Proximo_Mantenimiento <= DATEADD(DAY,30,GETDATE()) THEN 0 ELSE 1 END,
             Nombre_Maquina;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Insumos]
AS
BEGIN
    SELECT 
        Id_Insumo AS [ID],
        Nombre,
        Tipo AS [Tipo de Insumo],
        Costo,
        Fecha AS [Fecha de Adquisición],
        Cantidad
    FROM Insumos
    ORDER BY Id_Insumo ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Inventario]
    @Busqueda VARCHAR(100) = NULL,
    @Tipo VARCHAR(50) = NULL,
    @SoloBajoStock BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id_Insumo AS [ID],
        Codigo AS [Código],
        Nombre,
        Tipo,
        Unidad_Medida AS [Unidad],
        Cantidad,
        Stock_Minimo AS [Stock mínimo],
        Costo AS [Costo unitario],
        Cantidad * Costo AS [Valor stock],
        ISNULL(Ubicacion, '') AS [Ubicación],
        Fecha_Vencimiento AS [Fecha vencimiento],
        Estado,
        CASE
            WHEN Estado = 'Inactivo' THEN 'Inactivo'
            WHEN Cantidad <= 0 THEN 'Agotado'
            WHEN Cantidad <= Stock_Minimo THEN 'Bajo'
            ELSE 'Normal'
        END AS [Nivel]
    FROM dbo.Insumos
    WHERE
        (@Tipo IS NULL OR Tipo = @Tipo)
        AND (@SoloBajoStock = 0 OR Cantidad <= Stock_Minimo)
        AND
        (
            @Busqueda IS NULL
            OR Codigo LIKE '%' + @Busqueda + '%'
            OR Nombre LIKE '%' + @Busqueda + '%'
            OR Tipo LIKE '%' + @Busqueda + '%'
            OR Ubicacion LIKE '%' + @Busqueda + '%'
        )
    ORDER BY
        CASE
            WHEN Estado = 'Activo' AND Cantidad <= Stock_Minimo THEN 0
            WHEN Estado = 'Activo' THEN 1
            ELSE 2
        END,
        Nombre;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Mantenimientos_Equipos]
    @Id_Equipo INT = NULL,
    @FechaDesde DATE,
    @FechaHasta DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        M.Id_Mantenimiento AS [ID],
        M.Id_Equipo AS [ID Equipo],
        E.Codigo,
        E.Nombre AS [Equipo],
        E.Tipo_Equipo AS [Tipo equipo],
        M.Tipo_Mantenimiento AS [Tipo],
        M.Fecha_Mantenimiento AS [Fecha],
        M.Cantidad_Atendida AS [Cantidad atendida],
        M.Costo,
        M.Proximo_Mantenimiento AS [Proximo mantenimiento],
        ISNULL(M.Responsable, '') AS [Responsable],
        M.Descripcion AS [Descripcion]
    FROM dbo.Mantenimiento_Equipos M
    INNER JOIN dbo.Equipos E ON E.Id_Equipos = M.Id_Equipo
    WHERE (@Id_Equipo IS NULL OR M.Id_Equipo = @Id_Equipo)
      AND M.Fecha_Mantenimiento BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY M.Fecha_Mantenimiento DESC, M.Id_Mantenimiento DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Mantenimientos_Maquinaria]
    @ID_Maquina INT=NULL,@FechaDesde DATE,@FechaHasta DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MT.ID_Mantenimiento AS [ID],MT.ID_Maquina AS [ID Máquina],
      M.Codigo AS [Código],M.Nombre_Maquina AS [Maquinaria],
      MT.Tipo_Mantenimiento AS [Tipo],MT.Fecha_Mantenimiento AS [Fecha],
      MT.Costo,MT.Horometro AS [Horómetro],
      MT.Proximo_Mantenimiento AS [Próximo mantenimiento],
      ISNULL(MT.Proveedor,'') AS [Proveedor],MT.Descripcion AS [Descripción]
    FROM dbo.Mantenimiento_Maquinaria MT
    LEFT JOIN dbo.R_Maquinaria M ON M.ID_Maquina=MT.ID_Maquina
    WHERE (@ID_Maquina IS NULL OR MT.ID_Maquina=@ID_Maquina)
      AND MT.Fecha_Mantenimiento BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY MT.Fecha_Mantenimiento DESC,MT.ID_Mantenimiento DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 1. Procedimiento para listar maquinaria disponible (activa)
CREATE   PROCEDURE [dbo].[Listar_Maquinaria_Disponible]
AS
BEGIN
    -- Seleccionar máquinas activas (disponibles para asignar)
    SELECT
        M.ID_Maquina,
        M.Nombre_Maquina,
        M.Tipo_Maquinaria,
        M.Fecha_Adquisicion,
        M.Estado
    FROM R_Maquinaria M
    WHERE M.Estado = 'Activa'
    ORDER BY M.Nombre_Maquina;
    
    -- Si no hay máquinas activas
    IF @@ROWCOUNT = 0
    BEGIN
        -- Verificar si hay máquinas en la tabla
        IF EXISTS (SELECT 1 FROM R_Maquinaria)
        BEGIN
            -- Hay máquinas pero ninguna está activa
            SELECT 
                0 AS ID_Maquina, 
                'No hay máquinas disponibles para asignar' AS Nombre_Maquina,
                'Todas están asignadas o en mantenimiento' AS Tipo_Maquinaria,
                GETDATE() AS Fecha_Adquisicion,
                'No disponible' AS Estado;
        END
        ELSE
        BEGIN
            -- No hay máquinas en absoluto
            SELECT 
                0 AS ID_Maquina, 
                'No hay máquinas registradas en el sistema' AS Nombre_Maquina,
                'Debe registrar maquinaria primero' AS Tipo_Maquinaria,
                GETDATE() AS Fecha_Adquisicion,
                'No disponible' AS Estado;
        END
    END
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Movimientos_Financieros]
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

    INSERT INTO #Movimientos
        (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
    SELECT
        ID_Movimiento,
        Tipo,
        Categoria,
        Descripcion,
        Monto,
        Fecha,
        Metodo_Pago,
        Estado,
        Referencia,
        CASE
            WHEN Referencia LIKE 'INVENTARIO-%' THEN 'Inventario'
            WHEN Referencia LIKE 'MAQUINARIA-%' THEN 'Maquinaria'
            WHEN Referencia LIKE 'MANT-MAQ-%' THEN 'Mantenimiento'
            ELSE 'Manual'
        END,
        CASE
            WHEN Referencia LIKE 'INVENTARIO-%'
              OR Referencia LIKE 'MAQUINARIA-%'
              OR Referencia LIKE 'MANT-MAQ-%' THEN 0
            ELSE 1
        END
    FROM dbo.Movimiento_Financiero;

    IF OBJECT_ID('dbo.Cosecha', 'U') IS NOT NULL
       AND OBJECT_ID('dbo.Siembra', 'U') IS NOT NULL
    BEGIN
        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Ingreso',
            'Venta de cosecha',
            LEFT('Venta de ' + ISNULL(S.Nombre_Cultivo, 'cultivo'), 200),
            CONVERT(DECIMAL(14,2), C.Precio_Venta),
            CONVERT(DATE, ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha)),
            'Sistema',
            'Pagado',
            'COSECHA-' + CONVERT(VARCHAR(20), C.ID_Cosecha),
            'Cosecha',
            0
        FROM dbo.Cosecha C
        INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
        WHERE ISNULL(C.Precio_Venta, 0) > 0;

        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Gasto',
            'Cosecha',
            LEFT('Costo de cosecha de ' + ISNULL(S.Nombre_Cultivo, 'cultivo'), 200),
            CONVERT(DECIMAL(14,2), C.Costo_Cosecha),
            CONVERT(DATE, ISNULL(C.Fecha_Cosecha, S.Fecha_Cosecha)),
            'Sistema',
            'Pagado',
            'COSTO-COSECHA-' + CONVERT(VARCHAR(20), C.ID_Cosecha),
            'Cosecha',
            0
        FROM dbo.Cosecha C
        INNER JOIN dbo.Siembra S ON S.ID_Cultivo = C.id_cultivo
        WHERE ISNULL(C.Costo_Cosecha, 0) > 0;
    END;

    IF OBJECT_ID('dbo.Siembra', 'U') IS NOT NULL
    BEGIN
        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Gasto',
            'Siembra',
            LEFT('Costo de siembra de ' + ISNULL(Nombre_Cultivo, 'cultivo'), 200),
            CONVERT(DECIMAL(14,2), Costo_Siembra),
            CONVERT(DATE, Fecha_Plantacion),
            'Sistema',
            'Pagado',
            'SIEMBRA-' + CONVERT(VARCHAR(20), ID_Cultivo),
            'Siembra',
            0
        FROM dbo.Siembra
        WHERE ISNULL(Costo_Siembra, 0) > 0;
    END;

    IF OBJECT_ID('dbo.Fertilizacion', 'U') IS NOT NULL
    BEGIN
        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Gasto',
            'Fertilización',
            LEFT(
                'Fertilización' +
                CASE
                    WHEN Tipo_Fertilizante IS NULL THEN ''
                    ELSE ': ' + Tipo_Fertilizante
                END,
                200),
            CONVERT(DECIMAL(14,2), Costo_Fertilizacion),
            CONVERT(DATE, ISNULL(Fecha_Aplicacion, GETDATE())),
            'Sistema',
            'Pagado',
            'FERTILIZACION-' + CONVERT(VARCHAR(20), ID_Fertilizacion),
            'Fertilización',
            0
        FROM dbo.Fertilizacion
        WHERE ISNULL(Costo_Fertilizacion, 0) > 0;
    END;

    IF OBJECT_ID('dbo.Pago_Trabajador', 'U') IS NOT NULL
       AND OBJECT_ID('dbo.Trabajador', 'U') IS NOT NULL
    BEGIN
        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Gasto',
            'Nómina',
            LEFT(
                'Pago a ' + ISNULL(T.Nombre + ' ' + T.Apellido, 'trabajador') +
                CASE
                    WHEN P.Descripcion IS NULL OR LTRIM(RTRIM(P.Descripcion)) = '' THEN ''
                    ELSE ': ' + P.Descripcion
                END,
                200),
            CONVERT(DECIMAL(14,2), P.Monto),
            CONVERT(DATE, P.Fecha_Pago),
            'Sistema',
            'Pagado',
            'PAGO-' + CONVERT(VARCHAR(20), P.ID_Pago),
            'Nómina',
            0
        FROM dbo.Pago_Trabajador P
        LEFT JOIN dbo.Trabajador T ON T.ID_Trabajador = P.ID_Trabajador
        WHERE ISNULL(P.Monto, 0) > 0;
    END;

    IF OBJECT_ID('dbo.Alquiler_Administracion', 'U') IS NOT NULL
    BEGIN
        INSERT INTO #Movimientos
            (ID, Tipo, Categoria, Descripcion, Monto, Fecha, Metodo, Estado, Referencia, Origen, Editable)
        SELECT
            NULL,
            'Gasto',
            'Alquiler',
            LEFT(
                'Alquiler de ' + ISNULL(Tipo_Recurso, 'recurso') +
                CASE
                    WHEN Descripcion IS NULL OR LTRIM(RTRIM(Descripcion)) = '' THEN ''
                    ELSE ': ' + Descripcion
                END,
                200),
            CONVERT(DECIMAL(14,2), Costo),
            CONVERT(DATE, Fecha_Inicio),
            'Sistema',
            'Pagado',
            'ALQUILER-' + CONVERT(VARCHAR(20), ID_Administracion),
            'Alquiler',
            0
        FROM dbo.Alquiler_Administracion
        WHERE ISNULL(Costo, 0) > 0
          AND ISNULL(Estado, '') <> 'Cancelado';
    END;

    SELECT
        ID,
        Tipo,
        Categoria,
        Descripcion,
        Monto,
        Fecha,
        Metodo,
        Estado,
        Referencia,
        Origen,
        Editable
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

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Movimientos_Inventario]
    @Id_Insumo INT = NULL,
    @FechaDesde DATE,
    @FechaHasta DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        M.ID_Movimiento AS [ID],
        M.Id_Insumo AS [ID Insumo],
        I.Codigo AS [Código],
        I.Nombre AS [Producto],
        M.Tipo_Movimiento AS [Movimiento],
        M.Cantidad,
        I.Unidad_Medida AS [Unidad],
        M.Costo_Unitario AS [Costo unitario],
        M.Cantidad * M.Costo_Unitario AS [Total],
        M.Fecha_Movimiento AS [Fecha],
        M.Motivo,
        ISNULL(M.Referencia, '') AS [Referencia]
    FROM dbo.Movimiento_Inventario M
    INNER JOIN dbo.Insumos I ON I.Id_Insumo = M.Id_Insumo
    WHERE (@Id_Insumo IS NULL OR M.Id_Insumo = @Id_Insumo)
      AND M.Fecha_Movimiento BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY M.Fecha_Movimiento DESC, M.ID_Movimiento DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Operadores]
AS
BEGIN
    SELECT
        ID_Trabajador,
        Nombre,
        Apellido,
        Cedula,
        Telefono,
        Posicion
    FROM Trabajador
    WHERE Posicion LIKE '%Operador%'
       OR Posicion LIKE '%Tractorista%'
       OR Posicion = 'Operario'
    ORDER BY Apellido, Nombre;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Pagos]
    @ID_Agricultor INT = NULL
AS
BEGIN
    SELECT 
        P.ID_Pago AS [ID],
        T.Nombre + ' ' + T.Apellido AS [Trabajador],
        A.Nombre + ' ' + A.Apellido AS [Agricultor],
        P.Monto,
        P.Descripcion,
        P.Fecha_Pago AS [Fecha de Pago]
    FROM Pago_Trabajador P
    INNER JOIN Trabajador T ON P.ID_Trabajador = T.ID_Trabajador
    INNER JOIN Agricultor A ON P.ID_Agricultor = A.ID_Agricultor
    WHERE (@ID_Agricultor IS NULL OR P.ID_Agricultor = @ID_Agricultor)
    ORDER BY P.Fecha_Pago DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Produccion]
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
        P.Unidad_Medida AS [Unidad],
        P.Cantidad_Bruta AS [Cantidad bruta],
        P.Merma,
        P.Cantidad_Neta AS [Cantidad neta],
        P.Precio_Unitario AS [Precio unitario],
        P.Ingreso_Estimado AS [Ingreso estimado],
        P.Costo_Postcosecha AS [Costo postcosecha],
        P.Beneficio_Neto AS [Beneficio neto],
        ISNULL(P.Destino, '') AS [Destino],
        P.Estado,
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
          OR P.Observaciones LIKE '%' + @Busqueda + '%'
      )
    ORDER BY P.Fecha_Produccion DESC, P.ID_Produccion DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Siembras_Agricultor]
    @ID_Agricultor INT
AS
BEGIN
    SELECT 
        S.ID_Cultivo,
        S.Nombre_Cultivo,
        S.Tipo_Cultivo,
        S.Categoria_Cultivo,
        S.Estacion_Siembra,
        S.Cantidad,
        S.Tipo_Suelo,
        S.Fecha_Plantacion,
        S.Fecha_Cosecha,
        ASI.Fecha_Asignacion,
        ASI.Notas
    FROM Agricultor_Siembra ASI
    INNER JOIN Siembra S ON ASI.ID_Cultivo = S.ID_Cultivo
    WHERE ASI.ID_Agricultor = @ID_Agricultor
    ORDER BY S.Fecha_Plantacion;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Listar_Terrenos]
AS
BEGIN
    SELECT
        ID_Terreno AS [ID],
        Nombre_Terreno AS [Nombre],
        Ubicacion AS [Ubicación],
        CAST(Superficie_Total AS VARCHAR) + ' ' + Unidad_Medida AS [Superficie],
        Tipo_Suelo AS [Tipo de Suelo],
        Estado AS [Estado],
        Fecha_Registro AS [Fecha de Registro]
    FROM Terrenos
    ORDER BY Fecha_Registro DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Trabajadores]
AS
BEGIN
    SELECT
        ID_Trabajador AS [ID],
        Nombre,
        Apellido,
        Cedula,
        Sexo,
        Email,
        Telefono,
        Posicion AS [Rol laboral],
        Sueldo,
        Fecha_Nac AS [Fecha de Nacimiento],
        Direccion,
        Ciudad,
        Sector,
        Fecha_Ingreso AS [Fecha de Ingreso]
    FROM dbo.Trabajador
    WHERE Estado = 'Activo'
    ORDER BY Apellido, Nombre;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Listar_Trabajadores_Maquinaria]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_Trabajador AS [ID],
           Nombre + ' ' + Apellido + ' - ' + ISNULL(Posicion,'Trabajador') AS [Trabajador]
    FROM dbo.Trabajador ORDER BY Nombre, Apellido;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para marcar alerta como notificada
CREATE   PROCEDURE [dbo].[Marcar_Alerta_Notificada]
    @ID_Alerta INT
AS
BEGIN
    UPDATE Alertas SET
        Notificacion_Enviada = 1
    WHERE ID_Alerta = @ID_Alerta;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Mostrar_Agricultores]
AS
BEGIN
    SELECT 
        A.ID_Agricultor AS [ID],
        A.Nombre,
        A.Apellido,
        A.Cedula,
        A.Telefono,
        A.Email,
        A.Direccion,
        A.Ciudad,
        A.Sector,
        A.Fecha_Registro AS [Fecha de Registro],
        U.Usuario
    FROM Agricultor A
    INNER JOIN Usuario U ON A.ID_Usuario = U.ID_Usuario
    ORDER BY A.ID_Agricultor ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Mostrar_Cultivos]
AS
BEGIN
    -- Devolver TODOS los campos de la tabla Siembra
    -- en lugar de solo ID_Cultivo y Nombre_Cultivo
    SELECT 
        ID_Cultivo,
        Nombre_Cultivo as [Cultivos],
        Tipo_Cultivo as [Tipo De Cultivos],
        Categoria_Cultivo as [Categoria Cultivos],
        Estacion_Siembra as [Estacion],
        Cantidad as [Cantidad Sembrada],
        Tipo_Suelo as [Tipo De Suelo],
        Fecha_Plantacion as [Fecha De Plantacion],
        Fecha_Cosecha as [Fecha de Cosecha],
        ISNULL(Estado_Cultivo, 'Activo') as [Estado]
    FROM Siembra
    ORDER BY ID_Cultivo ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Crear el procedimiento almacenado
CREATE PROCEDURE [dbo].[Mostrar_SeguimientoCrecimiento]
    @ID_Cultivo INT
AS
BEGIN
    SELECT 
        S.ID_Seguimiento,
        S.ID_Cultivo,
        C.Nombre_Cultivo AS 'Cultivo',
        S.Fecha_Registro,
        S.Etapa_Crecimiento,
        S.Altura_Planta,
        S.Num_Hojas,
        S.Estado_Salud,
        S.Observaciones,
        S.Porcentaje_Crecimiento,
        S.Problemas_Detectados,
        S.Acciones_Tomadas
    FROM SeguimientoCrecimiento S
    INNER JOIN Siembra C ON S.ID_Cultivo = C.ID_Cultivo
    WHERE S.ID_Cultivo = @ID_Cultivo
    ORDER BY S.Fecha_Registro DESC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Mostrar_Siembras]
AS
BEGIN
    SELECT 
        S.Nombre_Cultivo AS [Cultivos],
        S.Tipo_Cultivo AS [Tipo De Cultivos],
        S.Categoria_Cultivo AS [Categoria Cultivos],
        S.Estacion_Siembra AS [Estacion],
        S.Cantidad AS [Cantidad Sembrada],
        S.Tipo_Suelo AS [Tipo De Suelo],
        S.Fecha_Plantacion AS [Fecha De Plantacion],
        S.Fecha_Cosecha AS [Fecha de Cosecha],
        r.Duracion AS [Tiempo hasta Cosecha]
    FROM Siembra S
    INNER JOIN RP r ON S.ID_Cultivo = r.ID_RP
    ORDER BY ID_Cultivo ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Mostrar_T_Cultivos]
AS
BEGIN
    SELECT 
        Nombre_Cultivo AS [Cultivo],
        Tipo_Cultivo AS [Tipo De Cultivo],
        Categoria_Cultivo AS [Categoria Cultivo],
        Estacion AS [Estacion],
        Tipo_Suelo AS [Tipo De Suelo]
    FROM RP 
    ORDER BY ID_RP ASC;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para obtener todas las alertas pendientes
CREATE   PROCEDURE [dbo].[Obtener_Alertas_Pendientes]
AS
BEGIN
    SELECT
        A.ID_Alerta,
        A.ID_Cultivo,
        S.Nombre_Cultivo,
        A.Titulo,
        A.Descripcion,
        A.Fecha_Programada,
        A.Fecha_Creacion,
        A.Tipo_Alerta,
        A.Estado,
        A.Prioridad,
        A.Notificacion_Enviada,
        A.Dias_Anticipacion,
        DATEDIFF(DAY, GETDATE(), A.Fecha_Programada) AS Dias_Restantes
    FROM Alertas A
    INNER JOIN Siembra S ON A.ID_Cultivo = S.ID_Cultivo
    WHERE A.Estado = 'Pendiente'
    ORDER BY A.Fecha_Programada;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para obtener alertas por cultivo
CREATE   PROCEDURE [dbo].[Obtener_Alertas_Por_Cultivo]
    @ID_Cultivo INT
AS
BEGIN
    SELECT
        A.ID_Alerta,
        A.ID_Cultivo,
        S.Nombre_Cultivo,
        A.Titulo,
        A.Descripcion,
        A.Fecha_Programada,
        A.Fecha_Creacion,
        A.Tipo_Alerta,
        A.Estado,
        A.Prioridad,
        A.Notificacion_Enviada,
        A.Dias_Anticipacion,
        DATEDIFF(DAY, GETDATE(), A.Fecha_Programada) AS Dias_Restantes
    FROM Alertas A
    INNER JOIN Siembra S ON A.ID_Cultivo = S.ID_Cultivo
    WHERE A.ID_Cultivo = @ID_Cultivo
    ORDER BY A.Fecha_Programada;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para obtener alertas próximas (para notificación)
CREATE   PROCEDURE [dbo].[Obtener_Alertas_Proximas]
AS
BEGIN
    SELECT
        A.ID_Alerta,
        A.ID_Cultivo,
        S.Nombre_Cultivo,
        A.Titulo,
        A.Descripcion,
        A.Fecha_Programada,
        A.Tipo_Alerta,
        A.Prioridad,
        DATEDIFF(DAY, GETDATE(), A.Fecha_Programada) AS Dias_Restantes
    FROM Alertas A
    INNER JOIN Siembra S ON A.ID_Cultivo = S.ID_Cultivo
    WHERE 
        A.Estado = 'Pendiente' AND
        A.Notificacion_Enviada = 0 AND
        DATEDIFF(DAY, GETDATE(), A.Fecha_Programada) <= A.Dias_Anticipacion AND
        DATEDIFF(DAY, GETDATE(), A.Fecha_Programada) >= 0
    ORDER BY A.Fecha_Programada;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Cosecha]
    @ID_Cosecha INT
AS
BEGIN
    SELECT 
        C.ID_Cosecha,
        C.id_cultivo AS ID_Cultivo,
        C.Estado_Cosecha,
        C.Cantidad_Cosechada,
        C.Rendimiento,
        C.Calidad,
        C.Observaciones,
        C.Fecha_Cosecha,
        C.Costo_Cosecha,
        C.Precio_Venta,
        C.Beneficio,
        S.Nombre_Cultivo,
        S.Tipo_Cultivo,
        S.Categoria_Cultivo,
        S.Cantidad AS Cantidad_Sembrada,
        S.Fecha_Plantacion,
        S.Costo_Siembra
    FROM Cosecha C
    INNER JOIN Siembra S ON C.id_cultivo = S.ID_Cultivo
    WHERE C.ID_Cosecha = @ID_Cosecha;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Cultivo]
    @ID_Cultivo INT
AS
BEGIN
    SELECT 
        ID_Cultivo,
        Nombre_Cultivo,
        Tipo_Cultivo,
        Categoria_Cultivo,
        Estacion_Siembra,
        Cantidad,
        Tipo_Suelo,
        Fecha_Plantacion,
        Fecha_Cosecha,
        ISNULL(Estado_Cultivo, 'Activo') as Estado_Cultivo,
        ISNULL(Metodo_Siembra, '') as Metodo_Siembra,
        ISNULL(Profundidad, '') as Profundidad,
        ISNULL(Humedad_Suelo, 0) as Humedad_Suelo,
        ISNULL(Costo_Siembra, 0) as Costo_Siembra
    FROM Siembra
    WHERE ID_Cultivo = @ID_Cultivo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para obtener estadísticas de siembras
CREATE PROCEDURE [dbo].[Obtener_Estadisticas_Siembras]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT
        COUNT(*) AS Total_Siembras,
        SUM(CASE WHEN Estado_Cultivo = 'Cosechado' THEN 1 ELSE 0 END) AS Total_Cosechados,
        SUM(CASE WHEN Estado_Cultivo IS NULL OR Estado_Cultivo != 'Cosechado' THEN 1 ELSE 0 END) AS Total_En_Crecimiento,
        AVG(DATEDIFF(DAY, Fecha_Plantacion, Fecha_Cosecha)) AS Promedio_Dias_Ciclo,
        SUM(Costo_Siembra) AS Costo_Total_Siembras
    FROM
        Siembra;
        
    -- Estadísticas por tipo de cultivo
    SELECT
        Tipo_Cultivo,
        COUNT(*) AS Total_Siembras,
        SUM(CASE WHEN Estado_Cultivo = 'Cosechado' THEN 1 ELSE 0 END) AS Total_Cosechados,
        AVG(DATEDIFF(DAY, Fecha_Plantacion, Fecha_Cosecha)) AS Promedio_Dias_Ciclo
    FROM
        Siembra
    GROUP BY
        Tipo_Cultivo;
        
    -- Estadísticas por categoría
    SELECT
        Categoria_Cultivo,
        COUNT(*) AS Total_Siembras,
        SUM(CASE WHEN Estado_Cultivo = 'Cosechado' THEN 1 ELSE 0 END) AS Total_Cosechados,
        AVG(DATEDIFF(DAY, Fecha_Plantacion, Fecha_Cosecha)) AS Promedio_Dias_Ciclo
    FROM
        Siembra
    GROUP BY
        Categoria_Cultivo;
        
    -- Estadísticas por estación de siembra
    SELECT
        Estacion_Siembra,
        COUNT(*) AS Total_Siembras,
        SUM(CASE WHEN Estado_Cultivo = 'Cosechado' THEN 1 ELSE 0 END) AS Total_Cosechados,
        AVG(DATEDIFF(DAY, Fecha_Plantacion, Fecha_Cosecha)) AS Promedio_Dias_Ciclo
    FROM
        Siembra
    GROUP BY
        Estacion_Siembra;
        
    -- Siembras por mes del año actual
    SELECT
        MONTH(Fecha_Plantacion) AS Mes,
        COUNT(*) AS Total_Siembras
    FROM
        Siembra
    WHERE
        YEAR(Fecha_Plantacion) = YEAR(GETDATE())
    GROUP BY
        MONTH(Fecha_Plantacion)
    ORDER BY
        Mes;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Insumo]
    @Id_Insumo int
AS
BEGIN
    SELECT 
        Id_Insumo,
        Nombre,
        Tipo,
        Costo,
        Fecha,
        Cantidad
    FROM Insumos
    WHERE Id_Insumo = @Id_Insumo;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para obtener reporte de siembras
CREATE PROCEDURE [dbo].[Obtener_Reporte_Siembras]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        s.ID_Cultivo,
        s.Nombre_Cultivo,
        s.Tipo_Cultivo,
        s.Categoria_Cultivo,
        s.Estacion_Siembra,
        s.Cantidad,
        s.Tipo_Suelo,
        s.Fecha_Plantacion,
        s.Fecha_Cosecha,
        COALESCE(s.Estado_Cultivo, 'En crecimiento') AS Estado_Cultivo,
        s.Metodo_Siembra,
        s.Profundidad,
        s.Humedad_Suelo,
        s.Costo_Siembra,
        f.Tipo_Fertilizante,
        f.Metodo_Fertilizacion,
        f.Cantidad_Fertilizante,
        f.Costo_Fertilizacion,
        f.Fecha_Aplicacion,
        DATEDIFF(DAY, s.Fecha_Plantacion, GETDATE()) AS Dias_Transcurridos,
        DATEDIFF(DAY, GETDATE(), s.Fecha_Cosecha) AS Dias_Restantes,
        DATEDIFF(DAY, s.Fecha_Plantacion, s.Fecha_Cosecha) AS Duracion_Total
    FROM 
        Siembra s
    LEFT JOIN 
        Fertilizacion f ON s.ID_Cultivo = f.ID_Cultivo
    ORDER BY 
        s.Fecha_Plantacion DESC;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Terreno]
    @ID_Terreno INT
AS
BEGIN
    SELECT 
        ID_Terreno,
        Nombre_Terreno AS Nombre,
        Superficie_Total AS Area,
        Unidad_Medida AS UnidadMedida, -- Asegúrate de que el alias coincida con tu clase
        Tipo_Suelo AS TipoSuelo,
        pH_Suelo AS pH,
        TRY_CONVERT(DECIMAL(5,2), Pendiente) AS Pendiente,
        Humedad,
        Ubicacion,
        Propietario,
        Fecha_Registro AS FechaRegistro,
        Estado AS EstadoActual,
        Observaciones
    FROM 
        Terrenos
    WHERE 
        ID_Terreno = @ID_Terreno;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Obtener_Trabajador]
    @ID_Trabajador INT
AS
BEGIN
    SELECT
        ID_Trabajador,
        Nombre,
        Apellido,
        Cedula,
        Sexo,
        Email,
        Fecha_Nac,
        Direccion,
        Telefono,
        Ciudad,
        Sector,
        Posicion,
        Sueldo,
        Fecha_Ingreso
    FROM dbo.Trabajador
    WHERE ID_Trabajador = @ID_Trabajador;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Registrar_Alquiler_Administracion]
    @ID_Agricultor INT,
    @Tipo_Recurso VARCHAR(50),
    @ID_Recurso INT,
    @Descripcion VARCHAR(100),
    @Costo MONEY,
    @Fecha_Inicio DATE,
    @Fecha_Fin DATE = NULL
AS
BEGIN
    INSERT INTO Alquiler_Administracion 
    (ID_Agricultor, Tipo_Recurso, ID_Recurso, Descripcion, Costo, Fecha_Inicio, Fecha_Fin, Estado)
    VALUES 
    (@ID_Agricultor, @Tipo_Recurso, @ID_Recurso, @Descripcion, @Costo, @Fecha_Inicio, @Fecha_Fin, 'Activo');
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Registrar_Cosecha_Cultivo]
    @ID_Cultivo INT
AS
BEGIN
    DECLARE @Mensaje VARCHAR(200) = '';
    
    -- Verificar si la tabla Cosecha tiene registros para este cultivo
    IF EXISTS (SELECT 1 FROM Cosecha WHERE id_cultivo = @ID_Cultivo)
    BEGIN
        SET @Mensaje = 'Este cultivo ya está registrado en cosecha.';
    END
    ELSE
    BEGIN
        -- Actualizar el estado del cultivo a "Cosechado"
        UPDATE Siembra 
        SET Estado_Cultivo = 'Cosechado'
        WHERE ID_Cultivo = @ID_Cultivo;
        
        -- Registrar en la tabla Cosecha con estado "En Proceso"
        INSERT INTO Cosecha (Estado_Cosecha, id_cultivo)
        VALUES ('En Proceso', @ID_Cultivo);
        
        SET @Mensaje = 'Cultivo finalizado y listo para cosecha.';
    END
    
    RETURN @Mensaje;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Equipo_Mantenimiento]
    @Codigo VARCHAR(30),
    @Nombre VARCHAR(100),
    @Tipo VARCHAR(50),
    @Cantidad INT,
    @Costo DECIMAL(14,2),
    @Fecha_Adquisicion DATE = NULL,
    @Proximo_Mantenimiento DATE = NULL,
    @Ubicacion VARCHAR(80) = NULL,
    @Observaciones VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @Codigo = UPPER(LTRIM(RTRIM(@Codigo)));
    SET @Nombre = LTRIM(RTRIM(@Nombre));
    SET @Tipo = LTRIM(RTRIM(@Tipo));

    IF EXISTS (SELECT 1 FROM dbo.Equipos WHERE Codigo = @Codigo)
        THROW 51300, 'Ya existe un equipo con ese codigo.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Equipos
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(@Nombre)
          AND LOWER(LTRIM(RTRIM(Tipo_Equipo))) = LOWER(@Tipo)
          AND Estado <> 'Inactivo'
    )
        THROW 51301, 'Ya existe un equipo activo con el mismo nombre y tipo.', 1;

    BEGIN TRANSACTION;

    INSERT INTO dbo.Equipos
        (Codigo, Nombre, Tipo_Equipo, Cantidad, Costo_Equipo,
         Fecha_Adquisicion, Proximo_Mantenimiento, Ubicacion, Estado, Observaciones)
    VALUES
        (@Codigo, @Nombre, @Tipo, @Cantidad, @Costo,
         @Fecha_Adquisicion, @Proximo_Mantenimiento, @Ubicacion, 'Activo', @Observaciones);

    DECLARE @Id_Equipo INT = SCOPE_IDENTITY();

    IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL AND @Costo > 0
    BEGIN
        INSERT INTO dbo.Movimiento_Financiero
            (Tipo, Categoria, Descripcion, Monto, Fecha,
             Metodo_Pago, Estado, Referencia)
        VALUES
            ('Gasto', 'Equipos', LEFT('Compra de equipo: ' + @Nombre, 200),
             @Cantidad * @Costo, ISNULL(@Fecha_Adquisicion, CONVERT(DATE, GETDATE())),
             'Sistema', 'Pagado',
             'EQUIPO-' + CONVERT(VARCHAR(20), @Id_Equipo));
    END;

    COMMIT TRANSACTION;
    SELECT @Id_Equipo;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Gestion_Maquinaria]
    @Codigo VARCHAR(30), @Nombre VARCHAR(100), @Tipo VARCHAR(50),
    @Marca VARCHAR(50)=NULL, @Modelo VARCHAR(50)=NULL, @Numero_Serie VARCHAR(60)=NULL,
    @Costo DECIMAL(14,2), @Fecha_Adquisicion DATE,
    @Mantenimiento_Programado VARCHAR(30), @Proximo_Mantenimiento DATE=NULL,
    @Horometro DECIMAL(14,1), @Ubicacion VARCHAR(80)=NULL,
    @Observaciones VARCHAR(250)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF EXISTS(SELECT 1 FROM dbo.R_Maquinaria WHERE Codigo=@Codigo)
        THROW 51200, 'Ya existe una maquinaria con ese código.', 1;
    IF @Numero_Serie IS NOT NULL AND EXISTS
       (SELECT 1 FROM dbo.R_Maquinaria WHERE Numero_Serie=@Numero_Serie)
        THROW 51201, 'Ya existe una maquinaria con ese número de serie.', 1;
    BEGIN TRANSACTION;
    INSERT dbo.R_Maquinaria
      (Codigo,Nombre_Maquina,Tipo_Maquinaria,Marca,Modelo,Numero_Serie,
       Costo_Maquinaria,Fecha_Adquisicion,Mantenimiento_Programado,
       Proximo_Mantenimiento,Horometro,Ubicacion,Estado,Observaciones)
    VALUES
      (@Codigo,@Nombre,@Tipo,@Marca,@Modelo,@Numero_Serie,@Costo,@Fecha_Adquisicion,
       @Mantenimiento_Programado,@Proximo_Mantenimiento,@Horometro,@Ubicacion,
       'Disponible',@Observaciones);
    DECLARE @ID INT=SCOPE_IDENTITY();
    IF OBJECT_ID('dbo.Movimiento_Financiero','U') IS NOT NULL AND @Costo>0
       INSERT dbo.Movimiento_Financiero
       (Tipo,Categoria,Descripcion,Monto,Fecha,Metodo_Pago,Estado,Referencia)
       VALUES('Gasto','Maquinaria',LEFT('Adquisición de '+@Nombre,200),@Costo,
              @Fecha_Adquisicion,'Sistema','Pagado','MAQUINARIA-'+CONVERT(VARCHAR(20),@ID));
    COMMIT;
    SELECT @ID;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Mantenimiento_Equipo]
    @Id_Equipo INT,
    @Tipo_Mantenimiento VARCHAR(30),
    @Fecha_Mantenimiento DATE,
    @Cantidad_Atendida INT,
    @Costo DECIMAL(14,2),
    @Proximo_Mantenimiento DATE = NULL,
    @Responsable VARCHAR(100) = NULL,
    @Descripcion VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Nombre VARCHAR(100);
    DECLARE @Cantidad INT;
    DECLARE @Estado VARCHAR(20);

    SELECT
        @Nombre = Nombre,
        @Cantidad = Cantidad,
        @Estado = Estado
    FROM dbo.Equipos WITH (UPDLOCK, HOLDLOCK)
    WHERE Id_Equipos = @Id_Equipo;

    IF @Nombre IS NULL
        THROW 51302, 'El equipo seleccionado no existe.', 1;
    IF @Estado = 'Inactivo'
        THROW 51304, 'No se puede registrar mantenimiento a un equipo inactivo.', 1;
    IF @Cantidad_Atendida <= 0 OR @Cantidad_Atendida > @Cantidad
        THROW 51305, 'La cantidad atendida no puede superar la cantidad registrada del equipo.', 1;

    BEGIN TRANSACTION;

    INSERT INTO dbo.Mantenimiento_Equipos
        (Id_Equipo, Tipo_Mantenimiento, Fecha_Mantenimiento, Cantidad_Atendida,
         Costo, Proximo_Mantenimiento, Responsable, Descripcion)
    VALUES
        (@Id_Equipo, @Tipo_Mantenimiento, @Fecha_Mantenimiento, @Cantidad_Atendida,
         @Costo, @Proximo_Mantenimiento, @Responsable, @Descripcion);

    DECLARE @Id_Mantenimiento INT = SCOPE_IDENTITY();

    UPDATE dbo.Equipos
       SET Proximo_Mantenimiento = @Proximo_Mantenimiento,
           Estado = 'Activo'
     WHERE Id_Equipos = @Id_Equipo;

    IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL AND @Costo > 0
    BEGIN
        INSERT INTO dbo.Movimiento_Financiero
            (Tipo, Categoria, Descripcion, Monto, Fecha,
             Metodo_Pago, Estado, Referencia)
        VALUES
            ('Gasto', 'Mantenimiento', LEFT('Mantenimiento de equipo: ' + @Nombre, 200),
             @Costo, @Fecha_Mantenimiento,
             'Sistema', 'Pagado',
             'MANT-EQ-' + CONVERT(VARCHAR(20), @Id_Mantenimiento));
    END;

    COMMIT TRANSACTION;
    SELECT @Id_Mantenimiento;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Mantenimiento_Maquinaria]
    @ID_Maquina INT,@Tipo_Mantenimiento VARCHAR(30),@Fecha_Mantenimiento DATE,
    @Costo DECIMAL(14,2),@Horometro DECIMAL(14,1),@Proximo_Mantenimiento DATE=NULL,
    @Proveedor VARCHAR(100)=NULL,@Descripcion VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRANSACTION;
    IF EXISTS(SELECT 1 FROM dbo.Asignacion_Maquinaria
              WHERE ID_Maquina=@ID_Maquina AND Estado='Asignada')
        THROW 51205, 'La maquinaria está asignada y no puede recibir mantenimiento.', 1;
    INSERT dbo.Mantenimiento_Maquinaria
      (ID_Maquina,Tipo_Mantenimiento,Fecha_Mantenimiento,Costo,Horometro,
       Proximo_Mantenimiento,Proveedor,Descripcion)
    VALUES(@ID_Maquina,@Tipo_Mantenimiento,@Fecha_Mantenimiento,@Costo,@Horometro,
           @Proximo_Mantenimiento,@Proveedor,@Descripcion);
    DECLARE @ID INT=SCOPE_IDENTITY();
    UPDATE dbo.R_Maquinaria SET Horometro=@Horometro,
      Proximo_Mantenimiento=@Proximo_Mantenimiento,Estado='Disponible'
    WHERE ID_Maquina=@ID_Maquina;
    IF OBJECT_ID('dbo.Movimiento_Financiero','U') IS NOT NULL AND @Costo>0
       INSERT dbo.Movimiento_Financiero
       (Tipo,Categoria,Descripcion,Monto,Fecha,Metodo_Pago,Estado,Referencia)
       SELECT 'Gasto','Mantenimiento',LEFT('Mantenimiento de '+Nombre_Maquina,200),
              @Costo,@Fecha_Mantenimiento,'Sistema','Pagado',
              'MANT-MAQ-'+CONVERT(VARCHAR(20),@ID)
       FROM dbo.R_Maquinaria WHERE ID_Maquina=@ID_Maquina;
    COMMIT; SELECT @ID;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Movimiento_Inventario]
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

    BEGIN TRANSACTION;

    DECLARE @StockActual DECIMAL(14,3);
    DECLARE @CostoActual DECIMAL(14,2);
    DECLARE @Nombre VARCHAR(100);
    DECLARE @Estado VARCHAR(15);

    SELECT
        @StockActual = Cantidad,
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
            CASE
                WHEN @StockActual + @Cantidad = 0 THEN @Costo_Unitario
                ELSE
                    ((@StockActual * @CostoActual) + (@Cantidad * @Costo_Unitario))
                    / (@StockActual + @Cantidad)
            END;

        UPDATE dbo.Insumos
           SET Cantidad = Cantidad + @Cantidad,
               Costo = @NuevoCosto,
               Fecha = @Fecha_Movimiento
         WHERE Id_Insumo = @Id_Insumo;
    END
    ELSE
    BEGIN
        UPDATE dbo.Insumos
           SET Cantidad = Cantidad - @Cantidad,
               Fecha = @Fecha_Movimiento
         WHERE Id_Insumo = @Id_Insumo;
    END;

    INSERT INTO dbo.Movimiento_Inventario
        (Id_Insumo, Tipo_Movimiento, Cantidad, Costo_Unitario,
         Fecha_Movimiento, Motivo, Referencia)
    VALUES
        (@Id_Insumo, @Tipo_Movimiento, @Cantidad,
         CASE WHEN @Tipo_Movimiento = 'Entrada' THEN @Costo_Unitario ELSE @CostoActual END,
         @Fecha_Movimiento, @Motivo, @Referencia);

    DECLARE @ID_Movimiento INT = SCOPE_IDENTITY();

    IF @Tipo_Movimiento = 'Entrada'
       AND OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL
       AND @Costo_Unitario > 0
    BEGIN
        INSERT INTO dbo.Movimiento_Financiero
            (Tipo, Categoria, Descripcion, Monto, Fecha,
             Metodo_Pago, Estado, Referencia)
        VALUES
            ('Gasto', 'Insumos', LEFT('Compra de ' + @Nombre, 200),
             @Cantidad * @Costo_Unitario, @Fecha_Movimiento,
             'Sistema', 'Pagado',
             'INVENTARIO-' + CONVERT(VARCHAR(20), @ID_Movimiento));
    END;

    COMMIT TRANSACTION;
    SELECT @ID_Movimiento;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Registrar_Pago_Trabajador]
    @ID_Trabajador INT,
    @ID_Agricultor INT,
    @Monto MONEY,
    @Descripcion VARCHAR(100)
AS
BEGIN
    INSERT INTO Pago_Trabajador (ID_Trabajador, ID_Agricultor, Monto, Descripcion, Fecha_Pago)
    VALUES (@ID_Trabajador, @ID_Agricultor, @Monto, @Descripcion, GETDATE());
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Produccion]
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

    IF NOT EXISTS (SELECT 1 FROM dbo.Cosecha WHERE ID_Cosecha = @ID_Cosecha)
        THROW 51400, 'La cosecha seleccionada no existe.', 1;

    IF @Estado NOT IN ('Registrada', 'Almacenada', 'Vendida', 'Cancelada')
        THROW 51401, 'Estado de produccion no valido.', 1;

    IF @Cantidad_Bruta <= 0 OR @Merma < 0 OR @Merma > @Cantidad_Bruta
        THROW 51402, 'Las cantidades de produccion no son validas.', 1;

    DECLARE @Cantidad_Neta DECIMAL(14,3) = @Cantidad_Bruta - @Merma;
    DECLARE @Ingreso_Estimado DECIMAL(14,2) = @Cantidad_Neta * @Precio_Unitario;
    DECLARE @Beneficio_Neto DECIMAL(14,2) = @Ingreso_Estimado - @Costo_Postcosecha;

    INSERT INTO dbo.Produccion
        (ID_Cosecha, Fecha_Produccion, Unidad_Medida, Cantidad_Bruta,
         Merma, Cantidad_Neta, Precio_Unitario, Ingreso_Estimado,
         Costo_Postcosecha, Beneficio_Neto, Destino, Estado, Observaciones)
    VALUES
        (@ID_Cosecha, @Fecha_Produccion, @Unidad_Medida, @Cantidad_Bruta,
         @Merma, @Cantidad_Neta, @Precio_Unitario, @Ingreso_Estimado,
         @Costo_Postcosecha, @Beneficio_Neto, @Destino, @Estado, @Observaciones);

    SELECT CONVERT(INT, SCOPE_IDENTITY());
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Registrar_Producto_Inventario]
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

    IF EXISTS (SELECT 1 FROM dbo.Insumos WHERE Codigo = @Codigo)
        THROW 51100, 'Ya existe un producto con ese código.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Insumos
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(LTRIM(RTRIM(@Nombre)))
          AND LOWER(LTRIM(RTRIM(Tipo))) = LOWER(LTRIM(RTRIM(@Tipo)))
          AND Estado = 'Activo'
    )
        THROW 51101, 'Ya existe un producto activo con el mismo nombre y tipo.', 1;

    BEGIN TRANSACTION;

    INSERT INTO dbo.Insumos
        (Codigo, Nombre, Tipo, Costo, Fecha, Cantidad, Unidad_Medida,
         Stock_Minimo, Ubicacion, Fecha_Vencimiento, Estado)
    VALUES
        (@Codigo, @Nombre, @Tipo, @Costo, GETDATE(), @StockInicial, @Unidad_Medida,
         @Stock_Minimo, @Ubicacion, @Fecha_Vencimiento, 'Activo');

    DECLARE @Id_Insumo INT = SCOPE_IDENTITY();

    IF @StockInicial > 0
    BEGIN
        INSERT INTO dbo.Movimiento_Inventario
            (Id_Insumo, Tipo_Movimiento, Cantidad, Costo_Unitario,
             Fecha_Movimiento, Motivo, Referencia)
        VALUES
            (@Id_Insumo, 'Entrada', @StockInicial, @Costo,
             GETDATE(), 'Stock inicial', NULL);

        DECLARE @ID_MovimientoInicial INT = SCOPE_IDENTITY();

        IF OBJECT_ID('dbo.Movimiento_Financiero', 'U') IS NOT NULL
           AND @Costo > 0
        BEGIN
            INSERT INTO dbo.Movimiento_Financiero
                (Tipo, Categoria, Descripcion, Monto, Fecha,
                 Metodo_Pago, Estado, Referencia)
            VALUES
                ('Gasto', 'Insumos', LEFT('Compra inicial de ' + @Nombre, 200),
                 @StockInicial * @Costo, GETDATE(),
                 'Sistema', 'Pagado',
                 'INVENTARIO-' + CONVERT(VARCHAR(20), @ID_MovimientoInicial));
        END;
    END;

    COMMIT TRANSACTION;
    SELECT @Id_Insumo;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Reporte_Costos_Agricultor]
    @ID_Agricultor INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    -- Costos de pagos a trabajadores
    SELECT 
        'Pagos a Trabajadores' AS [Categoría],
        SUM(Monto) AS [Costo Total]
    FROM Pago_Trabajador
    WHERE ID_Agricultor = @ID_Agricultor
    AND Fecha_Pago BETWEEN @FechaInicio AND @FechaFin;
    
    -- Costos de alquiler por tipo de recurso
    SELECT 
        'Alquiler de ' + Tipo_Recurso AS [Categoría],
        SUM(Costo) AS [Costo Total]
    FROM Alquiler_Administracion
    WHERE ID_Agricultor = @ID_Agricultor
    AND Fecha_Inicio BETWEEN @FechaInicio AND @FechaFin
    GROUP BY Tipo_Recurso;
    
    -- Costo total general
    SELECT 
        'COSTO TOTAL' AS [Categoría],
        SUM(Costo) AS [Costo Total]
    FROM (
        SELECT Monto AS Costo FROM Pago_Trabajador
        WHERE ID_Agricultor = @ID_Agricultor
        AND Fecha_Pago BETWEEN @FechaInicio AND @FechaFin
        
        UNION ALL
        
        SELECT Costo FROM Alquiler_Administracion
        WHERE ID_Agricultor = @ID_Agricultor
        AND Fecha_Inicio BETWEEN @FechaInicio AND @FechaFin
    ) AS Costos;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Reporte_TipoM]
AS
BEGIN
    SELECT
        ID_Maquina AS [ID],
        Nombre_Maquina AS [Nombre],
        Tipo_Maquinaria AS [Tipo],
        Costo_Maquinaria AS [Costo],
        Fecha_Adquisicion AS [Fecha],
        Mantenimiento_Programado AS [Mantenimiento]
    FROM R_Maquinaria 
    WHERE Tipo_Maquinaria LIKE 'Tractor';
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Reporte_TipoMA]
    @Variable varchar(50)
AS
BEGIN
    SELECT
        ID_Maquina AS [ID],
        Nombre_Maquina AS [Nombre],
        Tipo_Maquinaria AS [Tipo],
        Costo_Maquinaria AS [Costo],
        Fecha_Adquisicion AS [Fecha],
        Mantenimiento_Programado AS [Mantenimiento]
    FROM R_Maquinaria 
    WHERE Tipo_Maquinaria LIKE @Variable;
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Resumen_Agricultor]
    @ID_Agricultor INT
AS
BEGIN
    -- Datos del agricultor
    SELECT 
        ID_Agricultor,
        Nombre + ' ' + Apellido AS [Nombre Completo],
        Cedula,
        Email,
        Telefono,
        Ciudad + ', ' + Sector AS [Ubicación]
    FROM Agricultor
    WHERE ID_Agricultor = @ID_Agricultor;
    
    -- Total de pagos realizados
    SELECT 
        COUNT(*) AS [Total Pagos Realizados],
        SUM(Monto) AS [Monto Total Pagado]
    FROM Pago_Trabajador
    WHERE ID_Agricultor = @ID_Agricultor;
    
    -- Alquileres activos
    SELECT 
        COUNT(*) AS [Alquileres Activos],
        SUM(Costo) AS [Costo Total Alquileres Activos]
    FROM Alquiler_Administracion
    WHERE ID_Agricultor = @ID_Agricultor AND Estado = 'Activo';
END
GO

/* ========================================================================== */
/* CATALOGO DE PLAGAS Y ENFERMEDADES */
/* ========================================================================== */

SET IDENTITY_INSERT [dbo].[Enfermedades] ON;
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (1, N'Fungica', N'Roya blanca', N'Albugo candida', N'Manchas blancas, pustulas, deformacion de hojas y amarillamiento.', N'Brassicas: col, rabano, nabo, coliflor, brocoli y coles de Bruselas.', N'Retirar hojas afectadas, evitar riego por aspersion, mejorar ventilacion, rotar cultivos y aplicar fungicida preventivo si la humedad se mantiene alta.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (2, N'Fungicas', N'Mancha foliar por Septoria', N'Septoria lycopersici', N'Sintomas en las hojas', N'Tomate, berenjena, papa', N'La mancha foliar por Septoria se puede tratar con fungicidas y medidas preventivas. 
Fungicidas 
Aplique fungicidas que contengan maneb, mancozeb, clorotalonil, azoxistrobina, o cobre.
Aplique los fungicidas a intervalos de 7 a 10 días, principalmente durante la floración y el cuajado de las frutas.
Aplique los fungicidas con cuidado muy temprano en el curso de la enfermedad.
Medidas preventivas
Retire y destruya las hojas infectadas. 
Trate las semillas con agua caliente (122 °F durante 25 minutos) para reducir el inóculo transmitido por las semillas. 
Espacie las plantas adecuadamente para promover el flujo de aire. 
Riegue solo temprano en el día. 
Eleve las plantas del suelo con tutores o jaulas para acelerar el secado. 
Disminuya la humedad y la temperatura. 
Labre los restos del cultivo para promover la muerte de los hongos que hibernan. ');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (4, N'Fúngicas', N'Roya del maíz', N'Puccinia sorghi', N'Pústulas de color marrón rojizo en hojas, que pueden fusionarse y secar el tejido foliar', N'Maíz', N'Control mediante fungicidas y manejo adecuado del cultivo.
Fungicidas:
Aplicar triazoles (como tebuconazol) o estrobilurinas.
Aplicaciones deben ser oportunas, al detectar las primeras pústulas.
Medidas preventivas:
Cultive variedades resistentes a la roya.
Destruya residuos de cultivos anteriores donde sobreviven las esporas.
Practique la rotación de cultivos con especies no hospederas.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (5, N'Fúngicas', N'Antracnosis del frijol', N'Colletotrichum lindemuthianum', N'Manchas oscuras en tallos, hojas y vainas que se hunden en el centro', N'Frijol común', N'La antracnosis se puede tratar con fungicidas y medidas culturales.
Fungicidas:
Use clorotalonil o benomil al detectar los primeros síntomas.
Realizar aplicaciones cada 7 a 10 días.
Medidas preventivas:
Utilizar semillas certificadas libres de patógenos.
Evitar sembrar en suelos contaminados.
Eliminar y destruir residuos de cosechas anteriores.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (6, N'Fungica', N'Tizon tardio', N'Phytophthora infestans', N'Manchas oscuras acuosas, necrosis rapida, moho blanco en el enves y pudricion de frutos o tuberculos.', N'Papa, tomate y otras solanaceas.', N'Eliminar focos infectados, evitar exceso de humedad, espaciar plantas, no mojar follaje y aplicar fungicida especifico al inicio de los sintomas.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (7, N'Fúngicas', N'Pudrición del cuello', N'Sclerotium rolfsii', N'Marchitamiento repentino, pudrición en la base del tallo con un moho blanco y esclerocios marrones', N'Pimiento, tomate, cebolla, berenjena', N'Control con manejo del suelo y fungicidas.
Fungicidas:
Aplicar metam sodio o PCNB en el suelo.
Medidas preventivas:
Rotación de cultivos con gramíneas.
Mejorar el drenaje del suelo.
Evitar heridas en el cuello de las plantas durante el trasplante.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (8, N'Virales', N'Virus del mosaico del pepino', N'Cucumber mosaic virus (CMV)', N'Hojas deformadas con mosaicos de color verde claro y oscuro, crecimiento atrofiado', N'Pepino, melón, calabaza, pimiento, tomate', N'No existe cura química, se basa en prevención.
Medidas preventivas:
Controlar vectores (pulgones) con insecticidas o trampas adhesivas.
Eliminar plantas infectadas rápidamente.
Usar variedades resistentes si están disponibles.
Controlar malezas que actúan como reservorio del virus.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (9, N'Fungica', N'Roya blanca', N'Albugo candida', N'Manchas blancas, pustulas, deformacion de hojas y amarillamiento.', N'Brassicas: col, rabano, nabo, coliflor, brocoli y coles de Bruselas.', N'Retirar hojas afectadas, evitar riego por aspersion, mejorar ventilacion, rotar cultivos y aplicar fungicida preventivo si la humedad se mantiene alta.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (10, N'Fungica', N'Roya blanca', N'Albugo candida', N'Manchas blancas, pustulas, deformacion de hojas y amarillamiento.', N'Brassicas: col, rabano, nabo, coliflor, brocoli y coles de Bruselas.', N'Retirar hojas afectadas, evitar riego por aspersion, mejorar ventilacion, rotar cultivos y aplicar fungicida preventivo si la humedad se mantiene alta.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (11, N'Fungicas', N'Mancha foliar por Septoria', N'Septoria lycopersici', N'Sintomas en las hojas', N'Tomate, berenjena, papa', N'La mancha foliar por Septoria se puede tratar con fungicidas y medidas preventivas. 
Fungicidas 
Aplique fungicidas que contengan maneb, mancozeb, clorotalonil, azoxistrobina, o cobre.
Aplique los fungicidas a intervalos de 7 a 10 días, principalmente durante la floración y el cuajado de las frutas.
Aplique los fungicidas con cuidado muy temprano en el curso de la enfermedad.
Medidas preventivas
Retire y destruya las hojas infectadas. 
Trate las semillas con agua caliente (122 °F durante 25 minutos) para reducir el inóculo transmitido por las semillas. 
Espacie las plantas adecuadamente para promover el flujo de aire. 
Riegue solo temprano en el día. 
Eleve las plantas del suelo con tutores o jaulas para acelerar el secado. 
Disminuya la humedad y la temperatura. 
Labre los restos del cultivo para promover la muerte de los hongos que hibernan. ');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (12, N'Fúngicas', N'Mildiu velloso', N'Peronospora spp.', N'Manchas amarillas o marrones en las hojas, moho gris o blanco en el envés', N'Uvas, pepino, melón, lechuga', N'Se puede controlar con fungicidas sistémicos y prácticas culturales.
Fungicidas
Use productos que contengan metalaxil, fosetil-Al o mandipropamid.
Realice aplicaciones preventivas, especialmente durante periodos húmedos.
Medidas preventivas:
Evite el riego por aspersión en la noche.
Promueva la ventilación entre plantas para reducir la humedad.
Utilice variedades resistentes si están disponibles.
Elimine restos de plantas infectadas al final del ciclo.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (13, N'Fúngicas', N'Roya del maíz', N'Puccinia sorghi', N'Pústulas de color marrón rojizo en hojas, que pueden fusionarse y secar el tejido foliar', N'Maíz', N'Control mediante fungicidas y manejo adecuado del cultivo.
Fungicidas:
Aplicar triazoles (como tebuconazol) o estrobilurinas.
Aplicaciones deben ser oportunas, al detectar las primeras pústulas.
Medidas preventivas:
Cultive variedades resistentes a la roya.
Destruya residuos de cultivos anteriores donde sobreviven las esporas.
Practique la rotación de cultivos con especies no hospederas.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (14, N'Fúngicas', N'Antracnosis del frijol', N'Colletotrichum lindemuthianum', N'Manchas oscuras en tallos, hojas y vainas que se hunden en el centro', N'Frijol común', N'La antracnosis se puede tratar con fungicidas y medidas culturales.
Fungicidas:
Use clorotalonil o benomil al detectar los primeros síntomas.
Realizar aplicaciones cada 7 a 10 días.
Medidas preventivas:
Utilizar semillas certificadas libres de patógenos.
Evitar sembrar en suelos contaminados.
Eliminar y destruir residuos de cosechas anteriores.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (15, N'Fungica', N'Tizon tardio', N'Phytophthora infestans', N'Manchas oscuras acuosas, necrosis rapida, moho blanco en el enves y pudricion de frutos o tuberculos.', N'Papa, tomate y otras solanaceas.', N'Eliminar focos infectados, evitar exceso de humedad, espaciar plantas, no mojar follaje y aplicar fungicida especifico al inicio de los sintomas.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (16, N'Fúngicas', N'Pudrición del cuello', N'Sclerotium rolfsii', N'Marchitamiento repentino, pudrición en la base del tallo con un moho blanco y esclerocios marrones', N'Pimiento, tomate, cebolla, berenjena', N'Control con manejo del suelo y fungicidas.
Fungicidas:
Aplicar metam sodio o PCNB en el suelo.
Medidas preventivas:
Rotación de cultivos con gramíneas.
Mejorar el drenaje del suelo.
Evitar heridas en el cuello de las plantas durante el trasplante.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (17, N'Virales', N'Virus del mosaico del pepino', N'Cucumber mosaic virus (CMV)', N'Hojas deformadas con mosaicos de color verde claro y oscuro, crecimiento atrofiado', N'Pepino, melón, calabaza, pimiento, tomate', N'No existe cura química, se basa en prevención.
Medidas preventivas:
Controlar vectores (pulgones) con insecticidas o trampas adhesivas.
Eliminar plantas infectadas rápidamente.
Usar variedades resistentes si están disponibles.
Controlar malezas que actúan como reservorio del virus.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (18, N'Fungica', N'Oidio o cenicilla', N'Erysiphe spp.', N'Polvo blanco en hojas, tallos tiernos, hojas amarillas y crecimiento detenido.', N'Cucurbitaceas, vid, leguminosas, hortalizas y ornamentales.', N'Podar partes afectadas, mejorar circulacion de aire, evitar exceso de nitrogeno y aplicar azufre, bicarbonato agricola o fungicida compatible con el cultivo.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (19, N'Fungica', N'Alternaria', N'Alternaria solani', N'Manchas circulares con anillos concentricos, amarillamiento y defoliacion progresiva.', N'Tomate, papa, berenjena, chile y otras solanaceas.', N'Retirar residuos de cultivo, evitar salpicaduras de suelo, rotar cultivos, usar cobertura y aplicar fungicida preventivo si hay humedad frecuente.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (20, N'Fungica', N'Fusariosis', N'Fusarium oxysporum', N'Marchitez unilateral, amarillamiento, oscurecimiento vascular y muerte progresiva de la planta.', N'Tomate, banano, melon, sandia, leguminosas y hortalizas.', N'No mover suelo contaminado, eliminar plantas enfermas, desinfectar herramientas, mejorar drenaje y usar variedades resistentes; no hay cura completa en plantas ya invadidas.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (21, N'Bacteriana', N'Mancha bacteriana', N'Xanthomonas spp.', N'Manchas pequenas oscuras con halo amarillo, lesiones acuosas y caida de hojas.', N'Tomate, pimiento, citricos, brassicas y varias hortalizas.', N'Retirar hojas enfermas, evitar trabajar con follaje mojado, desinfectar herramientas, usar semillas sanas y aplicar productos bactericidas permitidos de forma preventiva.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (22, N'Insecto chupador', N'Pulgon', N'Aphididae', N'Colonias en brotes tiernos, hojas enrolladas, melaza, hormigas y amarillamiento.', N'Hortalizas, frutales, leguminosas, cereales y ornamentales.', N'Lavar brotes afectados, favorecer mariquitas y crisopas, retirar malezas hospederas y aplicar jabon potasico, aceite agricola o insecticida selectivo si supera el umbral.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (23, N'Insecto chupador', N'Mosca blanca', N'Bemisia tabaci', N'Adultos blancos al mover la planta, ninfas en enves, melaza, fumagina y hojas amarillas.', N'Tomate, pimiento, pepino, melon, frijol, algodon y ornamentales.', N'Usar trampas amarillas, eliminar malezas, revisar el enves, aplicar jabon potasico o aceite agricola y rotar ingredientes activos para evitar resistencia.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (24, N'Insecto raspador', N'Trips', N'Thysanoptera', N'Plateado o raspado en hojas, puntos negros, deformacion de brotes y manchas en flores o frutos.', N'Cebolla, ajo, tomate, pimiento, fresas, flores y hortalizas.', N'Monitorear flores y brotes, usar trampas azules, retirar flores muy afectadas, mejorar humedad ambiental y aplicar control biologico o insecticida selectivo.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (25, N'Acaro', N'Arana roja', N'Tetranychus urticae', N'Punteado amarillo, telaranas finas, bronceado de hojas y defoliacion en clima seco.', N'Frutales, hortalizas, fresas, tomate, pepino, flores y ornamentales.', N'Aumentar humedad sin mojar en exceso, lavar hojas, retirar focos, favorecer acaros depredadores y aplicar acaricida especifico si la poblacion crece.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (26, N'Insecto masticador', N'Gusano cogollero', N'Spodoptera frugiperda', N'Perforaciones, excremento en cogollo, hojas rasgadas y dano rapido en plantas jovenes.', N'Maiz, sorgo, arroz, pastos y algunas hortalizas.', N'Revisar cogollos, retirar larvas visibles, aplicar Bacillus thuringiensis en larvas pequenas y usar insecticida dirigido al cogollo si supera el umbral.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (27, N'Insecto minador', N'Minador de hoja', N'Liriomyza spp.', N'Galerias serpenteantes en hojas, puntos de oviposicion y perdida de area fotosintetica.', N'Tomate, papa, frijol, lechuga, cucurbitaceas y ornamentales.', N'Retirar hojas muy minadas, usar trampas amarillas, conservar parasitoides, evitar insecticidas de amplio espectro y aplicar control selectivo si hay avance.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (28, N'Nematodo', N'Nematodo agallador', N'Meloidogyne spp.', N'Agallas en raices, bajo crecimiento, marchitez con suelo humedo y amarillamiento general.', N'Tomate, pimiento, pepino, melon, zanahoria, frijol y muchas hortalizas.', N'Rotar con gramineas, incorporar materia organica, solarizar suelo, usar plantines sanos y variedades resistentes; en infestaciones altas usar manejo profesional del suelo.');
GO

INSERT INTO [dbo].[Enfermedades] ([Id_CP], [Tipo], [Nombre_Comun], [Nombre_Cientifico], [Razgos], [Huesped], [Cura]) VALUES (29, N'Fungica', N'Botritis o moho gris', N'Botrytis cinerea', N'Moho gris, pudricion blanda, lesiones en flores, frutos y tallos bajo humedad alta.', N'Fresa, tomate, vid, lechuga, flores y cultivos en invernadero.', N'Retirar tejido afectado, ventilar, evitar exceso de humedad, espaciar plantas y aplicar fungicida preventivo alternando modos de accion.');
GO

SET IDENTITY_INSERT [dbo].[Enfermedades] OFF;
GO

;WITH CatalogoSinDuplicados AS
(
    SELECT
        Id_CP,
        ROW_NUMBER() OVER
        (
            PARTITION BY Nombre_Cientifico COLLATE Latin1_General_CI_AI
            ORDER BY Id_CP
        ) AS Numero
    FROM dbo.Enfermedades
)
DELETE FROM CatalogoSinDuplicados
WHERE Numero > 1;
GO

CREATE UNIQUE INDEX [UX_Enfermedades_Nombre_Cientifico]
ON [dbo].[Enfermedades] ([Nombre_Cientifico]);
GO

CREATE TABLE [dbo].[Esquema_Version]
(
	[Version] INT NOT NULL CONSTRAINT [PK_Esquema_Version] PRIMARY KEY,
	[Nombre] VARCHAR(150) NOT NULL,
	[Aplicada_En] DATETIME NOT NULL CONSTRAINT [DF_Esquema_Version_Aplicada] DEFAULT GETDATE()
);
GO

INSERT INTO [dbo].[Esquema_Version] ([Version], [Nombre]) VALUES
(0, 'Crear tabla de control de version de esquema'),
(1, 'Administrador y roles de usuario'),
(2, 'Seguridad de contrasenas PBKDF2'),
(3, 'Acceso limitado a administrador y agricultor'),
(4, 'Autorizacion centralizada y auditoria'),
(5, 'Procedimientos requeridos por terrenos y fertilizacion'),
(6, 'Estructura de alertas, siembra, cosecha y catalogos fuera del DAL'),
(7, 'Resultados de auditoria y autorizacion de modulos agricolas');
GO

/* ========================================================================== */
/* MIGRACION 008 INTEGRADA: AUTENTICACION Y MINIMO PRIVILEGIO */
/* ========================================================================== */

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

/* ============================================================
   MIGRACION 010 - REGLAS AGROCLIMATICAS Y VARIEDADES
   ============================================================ */
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


/* ============================================================
   MIGRACION 011 - PRODUCCION INTEGRADA CON FINANZAS
   ============================================================ */
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

/* Migracion 012: cierre de escritura directa en auditoria. */
IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL OR
   (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) < 11
    THROW 52100, 'Ejecute primero las migraciones 000 a 011.', 1;

IF OBJECT_ID(N'dbo.Auditoria_Seguridad', N'U') IS NULL OR
   COL_LENGTH(N'dbo.Auditoria_Seguridad', N'Resultado') IS NULL
    THROW 52101, 'La tabla de auditoria no tiene la estructura requerida.', 1;
GO

CREATE OR ALTER PROCEDURE dbo.Registrar_Auditoria_Seguridad
    @ID_Usuario INT = NULL,
    @Usuario VARCHAR(80),
    @Rol VARCHAR(30),
    @Permiso VARCHAR(80),
    @Entidad VARCHAR(80),
    @Accion VARCHAR(40),
    @Detalle VARCHAR(1000) = NULL,
    @Resultado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @Usuario = LEFT(LTRIM(RTRIM(ISNULL(@Usuario, 'Sin sesion'))), 80);
    SET @Rol = LEFT(LTRIM(RTRIM(ISNULL(@Rol, 'Sin rol'))), 30);
    SET @Permiso = LEFT(LTRIM(RTRIM(ISNULL(@Permiso, 'sin_permiso'))), 80);
    SET @Entidad = LEFT(LTRIM(RTRIM(ISNULL(@Entidad, 'Sistema'))), 80);
    SET @Accion = LEFT(LTRIM(RTRIM(ISNULL(@Accion, 'Registrar'))), 40);
    SET @Detalle = LEFT(LTRIM(RTRIM(ISNULL(@Detalle, ''))), 1000);
    SET @Resultado = LEFT(LTRIM(RTRIM(ISNULL(@Resultado, 'Error'))), 20);

    IF @Resultado NOT IN ('Exito', 'Fallido', 'Denegado', 'Error')
        THROW 52102, 'El resultado de auditoria no es valido.', 1;

    INSERT dbo.Auditoria_Seguridad
    (
        ID_Usuario, Usuario, Rol, Permiso, Entidad, Accion, Detalle,
        Resultado, Nombre_Equipo, Usuario_Windows, Fecha_Registro
    )
    VALUES
    (
        @ID_Usuario, @Usuario, @Rol, @Permiso, @Entidad, @Accion, @Detalle,
        @Resultado, HOST_NAME(), SUSER_SNAME(), GETDATE()
    );
END;
GO

IF DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app') IS NOT NULL
BEGIN
    REVOKE INSERT ON OBJECT::dbo.Auditoria_Seguridad
        FROM db_sistema_agricola_app;
    GRANT EXECUTE ON OBJECT::dbo.Registrar_Auditoria_Seguridad
        TO db_sistema_agricola_app;
END;
GO

MERGE dbo.Esquema_Version AS destino
USING
(
    SELECT 12 AS Version,
           'Autorizacion cerrada para registros y auditoria por procedimiento' AS Nombre
) AS origen
ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

IF OBJECT_ID(N'dbo.Registrar_Auditoria_Seguridad', N'P') IS NULL
    THROW 52103, 'No se creo el procedimiento de auditoria segura.', 1;

IF EXISTS
(
    SELECT 1
    FROM sys.database_permissions
    WHERE grantee_principal_id = DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app')
      AND class = 1
      AND major_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
      AND permission_name = N'INSERT'
      AND state IN (N'G', N'W')
)
    THROW 52104, 'El rol de aplicacion conserva INSERT directo sobre la auditoria.', 1;

IF (SELECT ISNULL(MAX(Version), -1) FROM dbo.Esquema_Version) <> 12
    THROW 52105, 'La version 12 no quedo registrada.', 1;
GO

PRINT 'Migracion 012 aplicada: registros y auditoria cerrados.';
GO


IF OBJECT_ID(N'dbo.Esquema_Version', N'U') IS NULL
    THROW 51900, 'La instalacion no creo dbo.Esquema_Version.', 1;
IF (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0) <> 32
    THROW 51901, 'La cantidad de tablas instaladas no coincide con el sistema.', 1;
IF (SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped = 0) <> 142
    THROW 51902, 'La cantidad de procedimientos instalados no coincide con el sistema.', 1;
IF OBJECT_ID(N'dbo.Actualizar_Terreno', N'P') IS NULL
    THROW 51903, 'No se creo dbo.Actualizar_Terreno.', 1;
IF OBJECT_ID(N'dbo.Eliminar_Terreno', N'P') IS NULL
    THROW 51904, 'No se creo dbo.Eliminar_Terreno.', 1;
IF OBJECT_ID(N'dbo.Insertar_Fertilizacion', N'P') IS NULL
    THROW 51905, 'No se creo dbo.Insertar_Fertilizacion.', 1;
IF (SELECT MAX(Version) FROM dbo.Esquema_Version) <> 12
    THROW 51907, 'La version del esquema instalado no coincide con la aplicacion.', 1;
IF COL_LENGTH(N'dbo.Usuario', N'Contraseña') IS NOT NULL
    THROW 51911, 'La instalacion conserva la columna de contrasena heredada.', 1;
IF COL_LENGTH(N'dbo.Usuario', N'Debe_Cambiar_Contrasena') IS NULL OR
   COL_LENGTH(N'dbo.Usuario', N'Intentos_Fallidos') IS NULL OR
   COL_LENGTH(N'dbo.Usuario', N'Bloqueado_Hasta') IS NULL
    THROW 51912, 'La autenticacion instalada no tiene las columnas de endurecimiento.', 1;
IF EXISTS
(
    SELECT 1
    FROM sys.database_permissions
    WHERE grantee_principal_id = DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app')
      AND class = 0
      AND permission_name IN (N'SELECT', N'INSERT', N'UPDATE', N'DELETE', N'EXECUTE')
      AND state IN (N'G', N'W')
)
    THROW 51913, 'El rol de aplicacion conserva permisos globales demasiado amplios.', 1;
IF COL_LENGTH(N'dbo.Auditoria_Seguridad', N'Resultado') IS NULL
    THROW 51910, 'La auditoria instalada no tiene la columna Resultado.', 1;
IF OBJECT_ID(N'dbo.Registrar_Auditoria_Seguridad', N'P') IS NULL
    THROW 51917, 'No se creo el procedimiento seguro de auditoria.', 1;
IF EXISTS
(
    SELECT 1
    FROM sys.database_permissions
    WHERE grantee_principal_id = DATABASE_PRINCIPAL_ID(N'db_sistema_agricola_app')
      AND class = 1
      AND major_id = OBJECT_ID(N'dbo.Auditoria_Seguridad')
      AND permission_name = N'INSERT'
      AND state IN (N'G', N'W')
)
    THROW 51918, 'El rol de aplicacion conserva INSERT directo sobre auditoria.', 1;
IF COL_LENGTH(N'dbo.Siembra', N'ID_Terreno') IS NULL OR
   COL_LENGTH(N'dbo.Siembra', N'Hectareas_Sembradas') IS NULL OR
   COL_LENGTH(N'dbo.Siembra', N'Costo_Siembra') IS NULL
    THROW 51908, 'La estructura instalada de dbo.Siembra esta incompleta.', 1;
IF COL_LENGTH(N'dbo.Cosecha', N'Cantidad_Cosechada') IS NULL OR
   COL_LENGTH(N'dbo.Cosecha', N'Beneficio') IS NULL
    THROW 51909, 'La estructura instalada de dbo.Cosecha esta incompleta.', 1;
IF COL_LENGTH(N'dbo.Terrenos', N'Provincia') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Zona_Agroclimatica') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Precipitacion_Anual') IS NULL OR
   COL_LENGTH(N'dbo.Terrenos', N'Disponibilidad_Riego') IS NULL OR
   COL_LENGTH(N'dbo.Siembra', N'Variedad_Cultivo') IS NULL
    THROW 51914, 'La estructura agroclimatica instalada esta incompleta.', 1;
IF OBJECT_ID(N'dbo.Insertar_Terreno', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Registrar_Siembra_Atomica', N'P') IS NULL OR
   OBJECT_ID(N'dbo.Actualizar_Siembra_Atomica', N'P') IS NULL
    THROW 51915, 'Faltan procedimientos requeridos por el esquema 10.', 1;
IF COL_LENGTH(N'dbo.Produccion', N'Costo_Base_Asignado') IS NULL OR
   COL_LENGTH(N'dbo.Produccion', N'Estado_Cobro') IS NULL OR
   COL_LENGTH(N'dbo.Movimiento_Financiero', N'Origen') IS NULL OR
   COL_LENGTH(N'dbo.Movimiento_Financiero', N'ID_Origen') IS NULL OR
   OBJECT_ID(N'dbo.Sincronizar_Finanzas_Produccion', N'P') IS NULL
    THROW 51916, 'La integracion entre produccion y finanzas esta incompleta.', 1;
IF EXISTS
(
    SELECT Nombre_Cientifico
    FROM dbo.Enfermedades
    GROUP BY Nombre_Cientifico
    HAVING COUNT(*) > 1
)
    THROW 51906, 'El catalogo de plagas y enfermedades contiene duplicados.', 1;
PRINT 'DBSistema_Agricola instalada correctamente: version 12, 32 tablas y 142 procedimientos.';
GO
SELECT DB_NAME() AS BaseDatos,
       (SELECT MAX(Version) FROM dbo.Esquema_Version) AS VersionEsquema,
       (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0) AS Tablas,
       (SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped = 0) AS Procedimientos,
       (SELECT COUNT(*) FROM dbo.Enfermedades) AS PlagasEnfermedades;
GO
/*
    SISTEMA AGRICOLA - SCRIPT OFICIAL DE BASE DE DATOS
    Aplicacion: 1.1.0
    Esquema requerido: 12

    Este es el unico script completo autorizado para una instalacion nueva.
    Las migraciones numeradas se conservan solamente para actualizar bases existentes.
*/


USE [BDD-Agenda]

DROP TABLE IF EXISTS [dbo].[eventos];
DROP TABLE IF EXISTS [dbo].[colores];
DROP TABLE IF EXISTS [dbo].[usuarios];
DROP TABLE IF EXISTS [dbo].[zonas_horarias];

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[zonas_horarias](
	[id_zona_horaria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) COLLATE Latin1_General_CS_AS UNIQUE NOT NULL,
	[diferencia] [real] UNIQUE NOT NULL,

	CONSTRAINT [PK_zonas_horarias] PRIMARY KEY CLUSTERED 
	(
		[id_zona_horaria] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) COLLATE Latin1_General_CS_AS UNIQUE NOT NULL,
	[contrasenia] [varchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
	[nombre_apellido] [varchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
	[telefono] [varchar](50) COLLATE Latin1_General_CS_AS NULL,
	[email] [varchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
	[foto] [varchar](50) COLLATE Latin1_General_CS_AS NULL,
	[id_zona_horaria] [int] NOT NULL,

	CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
	(
		[id_usuario] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],

	CONSTRAINT [FK_usuarios_zonas_horarias] FOREIGN KEY
	(
		[id_zona_horaria]
	)REFERENCES [dbo].[zonas_horarias] ([id_zona_horaria]),
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[colores](
	[id_color] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) COLLATE Latin1_General_CS_AS UNIQUE NOT NULL,
	[codigo_hex] [varchar](10) COLLATE Latin1_General_CS_AS UNIQUE NOT NULL,

	CONSTRAINT [PK_colores] PRIMARY KEY CLUSTERED 
	(
		[id_color] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eventos](
	[id_evento] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
	[descripcion] [varchar](50) COLLATE Latin1_General_CS_AS NOT NULL,
	[todo_el_dia] [bit] NOT NULL,
	[fecha_hora_evento] [datetime] NOT NULL,
	[fecha_hora_recordatorio] [datetime] NULL,
	[recordatorio_enviado] [bit] NULL,
	[id_usuario] [int] NOT NULL,
	[id_color] [int] NOT NULL,

	CONSTRAINT [PK_eventos] PRIMARY KEY CLUSTERED 
	(
		[id_evento] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],

	CONSTRAINT [FK_eventos_usuarios] FOREIGN KEY
	(
		[id_usuario]
	)REFERENCES [dbo].[usuarios] ([id_usuario]),

	CONSTRAINT [FK_eventos_colores] FOREIGN KEY
	(
		[id_color]
	)REFERENCES [dbo].[colores] ([id_color]),
) ON [PRIMARY]
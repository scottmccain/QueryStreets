USE [Streets_SanDiego]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StreetNames_Places]') AND parent_object_id = OBJECT_ID(N'[dbo].[StreetNames]'))
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [FK_StreetNames_Places]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_TLID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_TLID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_PlaceId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_PlaceId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_CFCC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_CFCC]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_DirPrefix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_DirPrefix]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_Name]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_Type]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetNames_DirSuffix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetNames] DROP CONSTRAINT [DF_StreetNames_DirSuffix]
END

GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetNames]    Script Date: 01/27/2013 23:31:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StreetNames]') AND type in (N'U'))
DROP TABLE [dbo].[StreetNames]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetNames]    Script Date: 01/27/2013 23:31:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StreetNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TigerLineId] [int] NOT NULL,
	[PlaceId] [int] NOT NULL,
	[CensusFeatureClassCode] [char](3) NOT NULL,
	[DirectionPrefix] [char](2) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Type] [varchar](4) NOT NULL,
	[DirectionSuffix] [char](2) NOT NULL,
 CONSTRAINT [PK_StreetNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[StreetNames]  WITH CHECK ADD  CONSTRAINT [FK_StreetNames_Places] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Places] ([Id])
GO

ALTER TABLE [dbo].[StreetNames] CHECK CONSTRAINT [FK_StreetNames_Places]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_TLID]  DEFAULT ((0)) FOR [TigerLineId]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_PlaceId]  DEFAULT ((0)) FOR [PlaceId]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_CFCC]  DEFAULT ('') FOR [CensusFeatureClassCode]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_DirPrefix]  DEFAULT ('') FOR [DirectionPrefix]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_Type]  DEFAULT ('') FOR [Type]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_DirSuffix]  DEFAULT ('') FOR [DirectionSuffix]
GO


USE [Streets_SanDiego]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_StateFIPS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_StateFIPS]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_CountyFIPS]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_CountyFIPS]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_PlaceCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_PlaceCode]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_StateName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_StateName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_CountyName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_CountyName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Places_PlaceName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Places] DROP CONSTRAINT [DF_Places_PlaceName]
END

GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[Places]    Script Date: 01/27/2013 23:31:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Places]') AND type in (N'U'))
DROP TABLE [dbo].[Places]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[Places]    Script Date: 01/27/2013 23:31:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Places](
	[Id] [int] NOT NULL,
	[StateCode] [char](2) NOT NULL,
	[CountyCode] [char](3) NOT NULL,
	[PlaceCode] [varchar](5) NOT NULL,
	[StateName] [varchar](60) NOT NULL,
	[CountyName] [varchar](30) NOT NULL,
	[PlaceName] [varchar](60) NOT NULL,
 CONSTRAINT [PK_Places] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_StateFIPS]  DEFAULT ('') FOR [StateCode]
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_CountyFIPS]  DEFAULT ('') FOR [CountyCode]
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_PlaceCode]  DEFAULT ('') FOR [PlaceCode]
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_StateName]  DEFAULT ('') FOR [StateName]
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_CountyName]  DEFAULT ('') FOR [CountyName]
GO

ALTER TABLE [dbo].[Places] ADD  CONSTRAINT [DF_Places_PlaceName]  DEFAULT ('') FOR [PlaceName]
GO


USE [Streets_SanDiego]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AddressRanges_AddressRanges]') AND parent_object_id = OBJECT_ID(N'[dbo].[AddressRanges]'))
ALTER TABLE [dbo].[AddressRanges] DROP CONSTRAINT [FK_AddressRanges_AddressRanges]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AdressRanges_TLID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AddressRanges] DROP CONSTRAINT [DF_AdressRanges_TLID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AdressRanges_RangeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AddressRanges] DROP CONSTRAINT [DF_AdressRanges_RangeId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AdressRanges_First]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AddressRanges] DROP CONSTRAINT [DF_AdressRanges_First]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AdressRanges_Last]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AddressRanges] DROP CONSTRAINT [DF_AdressRanges_Last]
END

GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[AddressRanges]    Script Date: 01/27/2013 23:31:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddressRanges]') AND type in (N'U'))
DROP TABLE [dbo].[AddressRanges]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[AddressRanges]    Script Date: 01/27/2013 23:31:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AddressRanges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TigerLineId] [int] NOT NULL,
	[RangeId] [int] NOT NULL,
	[First] [varchar](11) NOT NULL,
	[Last] [varchar](11) NOT NULL,
 CONSTRAINT [PK_AddressRanges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AddressRanges]  WITH CHECK ADD  CONSTRAINT [FK_AddressRanges_AddressRanges] FOREIGN KEY([Id])
REFERENCES [dbo].[AddressRanges] ([Id])
GO

ALTER TABLE [dbo].[AddressRanges] CHECK CONSTRAINT [FK_AddressRanges_AddressRanges]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_TLID]  DEFAULT ((0)) FOR [TigerLineId]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_RangeId]  DEFAULT ((0)) FOR [RangeId]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_First]  DEFAULT ('') FOR [First]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_Last]  DEFAULT ('') FOR [Last]
GO


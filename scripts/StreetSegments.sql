USE [Streets_SanDiego]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetSegments_TLID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetSegments] DROP CONSTRAINT [DF_StreetSegments_TLID]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StreetSegments_Sequence]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StreetSegments] DROP CONSTRAINT [DF_StreetSegments_Sequence]
END

GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetSegments]    Script Date: 01/27/2013 23:31:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StreetSegments]') AND type in (N'U'))
DROP TABLE [dbo].[StreetSegments]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetSegments]    Script Date: 01/27/2013 23:31:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StreetSegments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TigerLineId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Location] [geography] NOT NULL,
 CONSTRAINT [PK_StreetSegments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_StreetSegments] UNIQUE NONCLUSTERED 
(
	[TigerLineId] ASC,
	[Sequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StreetSegments] ADD  CONSTRAINT [DF_StreetSegments_TLID]  DEFAULT ((0)) FOR [TigerLineId]
GO

ALTER TABLE [dbo].[StreetSegments] ADD  CONSTRAINT [DF_StreetSegments_Sequence]  DEFAULT ((0)) FOR [Sequence]
GO


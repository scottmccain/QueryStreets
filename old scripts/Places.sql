USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[Places]    Script Date: 12/22/2010 21:11:04 ******/
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


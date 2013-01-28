USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetNames]    Script Date: 12/22/2010 21:11:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StreetNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TLID] [int] NOT NULL,
	[PlaceId] [int] NOT NULL,
	[CFCC] [char](3) NOT NULL,
	[DirPrefix] [char](2) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Type] [varchar](4) NOT NULL,
	[DirSuffix] [char](2) NOT NULL,
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

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_TLID]  DEFAULT ((0)) FOR [TLID]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_PlaceId]  DEFAULT ((0)) FOR [PlaceId]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_CFCC]  DEFAULT ('') FOR [CFCC]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_DirPrefix]  DEFAULT ('') FOR [DirPrefix]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_Type]  DEFAULT ('') FOR [Type]
GO

ALTER TABLE [dbo].[StreetNames] ADD  CONSTRAINT [DF_StreetNames_DirSuffix]  DEFAULT ('') FOR [DirSuffix]
GO


USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[StreetSegments]    Script Date: 12/22/2010 21:11:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StreetSegments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TLID] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Location] [Geography] NOT NULL
 CONSTRAINT [PK_StreetSegments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StreetSegments] ADD  CONSTRAINT [DF_StreetSegments_TLID]  DEFAULT ((0)) FOR [TLID]
GO

ALTER TABLE [dbo].[StreetSegments] ADD  CONSTRAINT [DF_StreetSegments_Sequence]  DEFAULT ((0)) FOR [Sequence]
GO



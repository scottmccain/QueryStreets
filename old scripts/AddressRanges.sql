USE [Streets_SanDiego]
GO

/****** Object:  Table [dbo].[AddressRanges]    Script Date: 12/22/2010 21:10:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AddressRanges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TLID] [int] NOT NULL,
	[RangeId] [int] NOT NULL,
	[First] [varchar](11) NOT NULL,
	[Last] [varchar](11) NOT NULL,
 CONSTRAINT [PK_AdressRanges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_TLID]  DEFAULT ((0)) FOR [TLID]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_RangeId]  DEFAULT ((0)) FOR [RangeId]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_First]  DEFAULT ('') FOR [First]
GO

ALTER TABLE [dbo].[AddressRanges] ADD  CONSTRAINT [DF_AdressRanges_Last]  DEFAULT ('') FOR [Last]
GO


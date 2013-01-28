USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_Places_PlaceCode]    Script Date: 01/27/2013 23:33:29 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Places]') AND name = N'IX_Places_PlaceCode')
DROP INDEX [IX_Places_PlaceCode] ON [dbo].[Places] WITH ( ONLINE = OFF )
GO

USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_Places_PlaceCode]    Script Date: 01/27/2013 23:33:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Places_PlaceCode] ON [dbo].[Places] 
(
	[StateCode] ASC,
	[CountyCode] ASC,
	[PlaceCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


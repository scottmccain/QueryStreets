USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_PlaceName]    Script Date: 01/27/2013 23:33:19 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Places]') AND name = N'IX_PlaceName')
DROP INDEX [IX_PlaceName] ON [dbo].[Places] WITH ( ONLINE = OFF )
GO

USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_PlaceName]    Script Date: 01/27/2013 23:33:19 ******/
CREATE NONCLUSTERED INDEX [IX_PlaceName] ON [dbo].[Places] 
(
	[PlaceName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


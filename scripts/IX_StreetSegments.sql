USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetSegments]    Script Date: 01/27/2013 23:34:13 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StreetSegments]') AND name = N'IX_StreetSegments')
ALTER TABLE [dbo].[StreetSegments] DROP CONSTRAINT [IX_StreetSegments]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetSegments]    Script Date: 01/27/2013 23:34:13 ******/
ALTER TABLE [dbo].[StreetSegments] ADD  CONSTRAINT [IX_StreetSegments] UNIQUE NONCLUSTERED 
(
	[TigerLineId] ASC,
	[Sequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


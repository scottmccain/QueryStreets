USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetNames_Name]    Script Date: 01/27/2013 23:33:51 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StreetNames]') AND name = N'IX_StreetNames_Name')
DROP INDEX [IX_StreetNames_Name] ON [dbo].[StreetNames] WITH ( ONLINE = OFF )
GO

USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetNames_Name]    Script Date: 01/27/2013 23:33:51 ******/
CREATE NONCLUSTERED INDEX [IX_StreetNames_Name] ON [dbo].[StreetNames] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetSegementsLocation]    Script Date: 01/28/2013 00:20:09 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StreetSegments]') AND name = N'IX_StreetSegementsLocation')
DROP INDEX [IX_StreetSegementsLocation] ON [dbo].[StreetSegments]
GO

USE [Streets_SanDiego]
GO

/****** Object:  Index [IX_StreetSegementsLocation]    Script Date: 01/28/2013 00:20:09 ******/
CREATE SPATIAL INDEX [IX_StreetSegementsLocation] ON [dbo].[StreetSegments] 
(
	[Location]
)USING  GEOGRAPHY_GRID 
WITH (
GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX  = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


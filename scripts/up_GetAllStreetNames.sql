USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetAllStreetNames]    Script Date: 01/27/2013 23:29:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetAllStreetNames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetAllStreetNames]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetAllStreetNames]    Script Date: 01/27/2013 23:29:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetAllStreetNames]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		Id, 
		TigerLineId, 
		PlaceId, 
		CensusFeatureClassCode, 
		DirectionPrefix, 
		[Name], 
		[Type], 
		DirectionSuffix
	FROM StreetNames
	
END


GO


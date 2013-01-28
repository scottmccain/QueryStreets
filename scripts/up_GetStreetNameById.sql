USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetNameById]    Script Date: 01/27/2013 23:30:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetStreetNameById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetStreetNameById]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetNameById]    Script Date: 01/27/2013 23:30:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetStreetNameById]
	-- Add the parameters for the stored procedure here
	@TigerLineId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select 
		Id, 
		TigerLineId, 
		PlaceId, 
		CensusFeatureClassCode, 
		DirectionPrefix, 
		Name, 
		[Type], 
		DirectionSuffix
	FROM 
		dbo.StreetNames
	WHERE 
		TigerLineId = @TigerLineId
END


GO


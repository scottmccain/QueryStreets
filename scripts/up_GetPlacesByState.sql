USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetPlacesByState]    Script Date: 01/27/2013 23:29:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetPlacesByState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetPlacesByState]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetPlacesByState]    Script Date: 01/27/2013 23:29:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetPlacesByState]
	-- Add the parameters for the stored procedure here
	@StateName varchar(60)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Id, StateCode, CountyCode, PlaceCode, StateName, CountyName, PlaceName
	FROM dbo.Places
	WHERE StateName = @StateName
END

GO


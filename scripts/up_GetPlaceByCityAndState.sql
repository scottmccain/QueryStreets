USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetPlaceByCityAndState]    Script Date: 01/27/2013 23:29:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetPlaceByCityAndState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetPlaceByCityAndState]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetPlaceByCityAndState]    Script Date: 01/27/2013 23:29:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetPlaceByCityAndState]
	-- Add the parameters for the stored procedure here
	@CityName as varchar(60),
	@StateName as varchar(60)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, StateCode, CountyCode, PlaceCode, StateName, CountyName, PlaceName
	FROM dbo.Places
	WHERE PlaceName = @CityName And StateName = @StateName
END

GO


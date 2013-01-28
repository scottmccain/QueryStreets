USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetByAddress]    Script Date: 01/27/2013 23:30:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetStreetByAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetStreetByAddress]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetByAddress]    Script Date: 01/27/2013 23:30:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetStreetByAddress]
	@number int,
	@place_id int,
	@street varchar(max)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @AddressLookup TABLE(StreetNameId int, StreetName varchar(30), [FullAddress] varchar(max))

	INSERT INTO @AddressLookup
	SELECT Id, Name, dbo.TRIM(ISNULL(DirectionPrefix, '') + ' ' + ISNULL(Name, '') + ' ' + ISNULL([Type], '') + ' ' + ISNULL(DirectionSuffix, '')) FROM StreetNames
	WHERE PlaceId = @place_id

	SELECT TOP 1
		sn.TigerLineId, ar.[First], ar.[Last], (@number - ar.[First]) as Diff, dbo.TRIM(ISNULL(sn.DirectionPrefix, '') + ' ' + ISNULL(sn.Name, '') + ' ' + ISNULL(sn.[Type], '') + ' ' + ISNULL(sn.DirectionSuffix, '')) as FullAddress
	FROM StreetNames sn
	INNER JOIN AddressRanges ar
	ON sn.TigerLineId = ar.TigerLineId
	INNER JOIN @AddressLookup fa
	ON sn.Id = fa.StreetNameId
	WHERE sn.PlaceId = @place_id
		  AND (fa.[FullAddress] = @street OR fa.[StreetName] = @street)
		  AND @number BETWEEN ar.[First] AND ar.[Last]
		  ORDER BY diff
END


GO


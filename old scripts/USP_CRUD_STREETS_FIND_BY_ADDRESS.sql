USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_STREET_NAMES_FIND_BY_ADDRESS]    Script Date: 12/22/2010 21:06:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_STREET_NAMES_FIND_BY_ADDRESS]
	@number int,
	@place_id int,
	@street varchar(max)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @FullAddress TABLE(StreetNameId int, [Address] varchar(max))

	INSERT INTO @FullAddress
	SELECT Id, dbo.TRIM(ISNULL(DirPrefix, '') + ' ' + ISNULL(Name, '') + ' ' + ISNULL([Type], '') + ' ' + ISNULL(DirSuffix, '')) FROM StreetNames
	WHERE PlaceId = @place_id

	SELECT TOP 1
		sn.TLID, ar.[First], ar.[Last], (@number - ar.[First]) as Diff, dbo.TRIM(ISNULL(sn.DirPrefix, '') + ' ' + ISNULL(sn.Name, '') + ' ' + ISNULL(sn.[Type], '') + ' ' + ISNULL(sn.DirSuffix, '')) as FullAddress
	FROM StreetNames sn
	INNER JOIN AddressRanges ar
	ON sn.TLID = ar.TLID
	INNER JOIN @FullAddress fa
	ON sn.Id = fa.StreetNameId
	WHERE sn.PlaceId = @place_id
		  AND fa.[Address] = @street
		  AND @number BETWEEN ar.[First] AND ar.[Last]
		  ORDER BY diff
END

GO


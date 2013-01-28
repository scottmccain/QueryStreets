USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetSegmentsByTLIDOrdered]    Script Date: 01/27/2013 23:30:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetStreetSegmentsByTLIDOrdered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetStreetSegmentsByTLIDOrdered]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetSegmentsByTLIDOrdered]    Script Date: 01/27/2013 23:30:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetStreetSegmentsByTLIDOrdered]
	-- Add the parameters for the stored procedure here
	@TLID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, TigerLineId, Sequence, Location.Lat as Latitude, Location.Long as Longitude
	FROM dbo.StreetSegments
	WHERE TigerLineId = @TLID
	ORDER BY Sequence
	
END

GO


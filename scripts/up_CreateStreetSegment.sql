USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateStreetSegment]    Script Date: 01/27/2013 23:29:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateStreetSegment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateStreetSegment]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateStreetSegment]    Script Date: 01/27/2013 23:29:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_CreateStreetSegment]
	-- Add the parameters for the stored procedure here
	@TigerLineId int,
	@Sequence int,
	@Latitude float,
	@Longitude float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT 
	INTO StreetSegments
	(
		TigerLineId,
		Sequence,
		Location
	)
	VALUES
	(
		@TigerLineId,
		@Sequence,
		geography::Point(@Latitude, @Longitude, 4326)	
	)
	
END


GO


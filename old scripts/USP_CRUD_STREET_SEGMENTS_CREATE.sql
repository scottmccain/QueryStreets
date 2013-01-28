USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_STREET_SEGMENTS_CREATE]    Script Date: 12/22/2010 21:10:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_STREET_SEGMENTS_CREATE]
	-- Add the parameters for the stored procedure here
	@TLID int,
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
		TLID,
		Sequence,
		Latitude,
		Longitude
	)
	VALUES
	(
		@TLID,
		@Sequence,
		geography::Point(@Latitude, @Longitude, 4326)	
	)

	
END

GO


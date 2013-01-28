USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_PLACES_CREATE]    Script Date: 12/22/2010 21:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_PLACES_CREATE] 
	-- Add the parameters for the stored procedure here
	@place_id int,
	@state_fips char(2),
	@county_fips char(3),
	@place_fips varchar(5),
	@state_name varchar(60),
	@county_name varchar(30),
	@place_name varchar(60)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO 
	Places 
	(
		Id, 
		StateCode, 
		CountyCode, 
		PlaceCode, 
		StateName, 
		CountyName,
		PlaceName
	) 
	VALUES 
	(
		@place_id,
		@state_fips,
		@county_fips,
		@place_fips,
		@state_name,
		@county_name,
		@place_name
	)
	
END

GO


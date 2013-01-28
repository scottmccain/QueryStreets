USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreatePlace]    Script Date: 01/27/2013 23:28:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreatePlace]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreatePlace]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreatePlace]    Script Date: 01/27/2013 23:28:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_CreatePlace] 
	-- Add the parameters for the stored procedure here
	@PlaceId int,
	@StateFips char(2),
	@CountyFips char(3),
	@PlaceFips varchar(5),
	@StateName varchar(60),
	@CountyName varchar(30),
	@PlaceName varchar(60)
	
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
		@PlaceId,
		@StateFips,
		@CountyFips,
		@PlaceFips,
		@StateName,
		@CountyName,
		@PlaceName
	)
	
END


GO


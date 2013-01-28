USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateStreet]    Script Date: 01/27/2013 23:28:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateStreet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateStreet]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateStreet]    Script Date: 01/27/2013 23:28:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_CreateStreet] 
	-- Add the parameters for the stored procedure here
	@TigerLineId int,
	@CensusFeatureClassCode char(3),
	@DirectionPrefix char(2),
	@Name varchar(30),
	@Type varchar(4),
	@Places varchar(max),
	@DirectionSuffix char(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Insert Into StreetNames ( TigerLineId, PlaceId, CensusFeatureClassCode, DirectionPrefix, Name, [Type], DirectionSuffix)
Select 
	@TigerLineId, 
	CONVERT(INT, Value),
	@CensusFeatureClassCode,
	@DirectionPrefix,
	@Name,
	@Type,
	@DirectionSuffix
FROM [Streets_SanDiego].[dbo].[FN_SplitDelimitedValues] (@places ,',')

END


GO


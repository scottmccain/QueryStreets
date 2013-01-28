USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetNamesByIdList]    Script Date: 01/27/2013 23:30:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_GetStreetNamesByIdList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_GetStreetNamesByIdList]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_GetStreetNamesByIdList]    Script Date: 01/27/2013 23:30:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_GetStreetNamesByIdList]
	-- Add the parameters for the stored procedure here
	@TigerLineIdList varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @IdTable AS TABLE( Id int )
	INSERT INTO @IdTable 
	SELECT DISTINCT Int_Value from fn_ParseText2Table(@TigerLineIdList, ',')
	
	SELECT 
		sn.* 
	FROM 
		dbo.StreetNames sn 
		INNER JOIN @IdTable it ON sn.TigerLineId = it.Id
END

GO


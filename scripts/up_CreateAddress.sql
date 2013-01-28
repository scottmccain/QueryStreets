USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateAddress]    Script Date: 01/27/2013 23:28:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateAddress]
GO

USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[up_CreateAddress]    Script Date: 01/27/2013 23:28:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_CreateAddress]
	-- Add the parameters for the stored procedure here
	@TigerLineId int,
	@RangeId int,
	@First varchar(11),
	@Last varchar(11)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO AddressRanges
	(
		TigerLineId,
		RangeId,
		[First],
		[Last]
	)
	VALUES
	(
		@TigerLineId,
		@RangeId,
		@First,
		@Last
	)
	
END


GO


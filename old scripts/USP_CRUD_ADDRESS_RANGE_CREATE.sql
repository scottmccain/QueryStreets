USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_ADDRESS_RANGE_CREATE]    Script Date: 12/22/2010 21:09:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_ADDRESS_RANGE_CREATE]
	-- Add the parameters for the stored procedure here
	@TLID int,
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
		TLID,
		RangeId,
		[First],
		[Last]
	)
	VALUES
	(
		@TLID,
		@RangeId,
		@First,
		@Last
	)
	
END

GO


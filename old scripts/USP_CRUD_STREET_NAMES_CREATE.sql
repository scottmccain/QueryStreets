USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_STREET_NAMES_CREATE]    Script Date: 12/22/2010 21:09:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_STREET_NAMES_CREATE] 
	-- Add the parameters for the stored procedure here
	@TLID int,
	@CFCC char(3),
	@DIR_PREFIX char(2),
	@NAME varchar(30),
	@TYPE varchar(4),
	@places varchar(max),
	@DIR_SUFFIX char(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Insert Into StreetNames ( tlid, PlaceId, CFCC, DirPrefix, Name, [Type], DirSuffix)
Select 
	@TLID, 
	CONVERT(INT, Value),
	@CFCC,
	@DIR_PREFIX,
	@NAME,
	@TYPE,
	@DIR_SUFFIX
FROM [Streets_SanDiego].[dbo].[FN_SplitDelimitedValues] (@places ,',')

END

GO


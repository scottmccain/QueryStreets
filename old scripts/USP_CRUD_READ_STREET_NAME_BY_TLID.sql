USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_READ_STREET_NAME_BY_TLID]    Script Date: 12/22/2010 21:09:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_READ_STREET_NAME_BY_TLID]
	-- Add the parameters for the stored procedure here
	@TLID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Id, TLID, PlaceId, CFCC, DirPrefix, Name, [Type], DirSuffix
	FROM DBO.StreetNames
	WHERE TLID = @TLID
END

GO


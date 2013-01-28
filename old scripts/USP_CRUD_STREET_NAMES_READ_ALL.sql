USE [Streets_SanDiego]
GO

/****** Object:  StoredProcedure [dbo].[USP_CRUD_STREET_NAMES_READ_ALL]    Script Date: 12/22/2010 21:09:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CRUD_STREET_NAMES_READ_ALL]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, TLID, PlaceId, CFCC, DirPrefix, [Name], [Type], DirSuffix
	FROM StreetNames
	
END

GO


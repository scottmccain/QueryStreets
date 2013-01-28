USE [Streets_SanDiego]
GO

/****** Object:  UserDefinedFunction [dbo].[TRIM]    Script Date: 12/23/2010 10:41:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[TRIM](@string VARCHAR(max))
RETURNS VARCHAR(MAX)
BEGIN
RETURN LTRIM(RTRIM(@string))
END

GO


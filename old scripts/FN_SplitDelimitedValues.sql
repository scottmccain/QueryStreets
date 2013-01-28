USE [Streets_SanDiego]
GO

/****** Object:  UserDefinedFunction [dbo].[FN_SplitDelimitedValues]    Script Date: 12/23/2010 10:41:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FN_SplitDelimitedValues]
(	
	-- Add the parameters for the function here
	@Value varchar(max),
	@Delimiter char(1)
)
RETURNS @temp TABLE(Id int identity, Value varchar(max))
BEGIN
	declare @xmlData xml
	
	SELECT @xmlData = CONVERT(xml,'<root><s>' + REPLACE(@Value,@Delimiter,'</s><s>') + '</s></root>')
	insert into @temp
	
	SELECT [Value] = [dbo].[TRIM](T.c.value('.','varchar(20)'))
	FROM @xmlData.nodes('/root/s') T(c)
	
	RETURN
END

GO


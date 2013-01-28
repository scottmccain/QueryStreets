USE [Streets_SanDiego]
GO

/****** Object:  UserDefinedFunction [dbo].[min3]    Script Date: 12/23/2010 10:41:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[min3](@a int, @b int, @c int)
returns int as
begin
  declare @min int
  set @min = @a
  if @b < @min set @min = @b
  if @c < @min set @min = @c
  return @min
end
GO


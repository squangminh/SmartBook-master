﻿--CREATE FUNCTION [dbo].[fn_ConcatCategoryFood]
--(
-- @Id SYSNAME
--)
--RETURNS NVARCHAR(MAX)
--WITH SCHEMABINDING
--AS
--BEGIN
-- DECLARE @s NVARCHAR(MAX);

-- SELECT @s = COALESCE(@s + N', ', N'') + im.[Name] 
-- FROM [dbo].[Food] i
--	INNER JOIN [dbo].[FoodFoodCategory] m ON m.[FoodId] = i.[Id]
--	INNER JOIN [dbo].[FoodCategory] im ON m.[FoodCategoryId] = im.[Id]	     
--	WHERE  i.[Id] = @Id
--	GROUP BY i.[Id], im.[Name], m.[FoodCategoryId] 
-- RETURN (@s);
--END

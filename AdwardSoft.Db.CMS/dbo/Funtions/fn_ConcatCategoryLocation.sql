﻿--CREATE FUNCTION [dbo].[fn_ConcatCategoryLocation]
--(
-- @Id SYSNAME
--)
--RETURNS NVARCHAR(MAX)
--WITH SCHEMABINDING
--AS
--BEGIN
-- DECLARE @s NVARCHAR(MAX);

-- SELECT @s = COALESCE(@s + N', ', N'') + im.[Name] 
-- FROM [dbo].[FoodLocation] f
--	INNER JOIN [dbo].[Food] i ON i.[FoodLocationId] = f.[Id]
--	INNER JOIN [dbo].[FoodFoodCategory] m ON m.[FoodId] = i.[Id]
--	INNER JOIN [dbo].[FoodCategory] im ON m.[FoodCategoryId] = im.[Id]	     
--	WHERE  f.[Id] = @Id
--	GROUP BY i.[Id], im.[Name], m.[FoodCategoryId], f.[Id], i.[FoodLocationId]
-- RETURN (@s);
--END

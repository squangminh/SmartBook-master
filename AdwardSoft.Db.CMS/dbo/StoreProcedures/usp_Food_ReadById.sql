CREATE PROCEDURE [dbo].[usp_Food_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT f.*, fl.[Name] [FoodLocationName], ffc.[FoodCategoryId] [CategoryId]
		FROM [dbo].[Food] f
		INNER JOIN [dbo].[FoodLocation] fl ON f.[FoodLocationId] = fl.[Id]
		INNER JOIN [dbo].[FoodFoodCategory] ffc ON ffc.[FoodId] = f.[Id] AND ffc.[IsDefault] = 1
		WHERE  f.[Id] =@Id 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_FoodFoodCategory_Read]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT ft.[Id] [CategoryId], ft.[Name], fft.[FoodId], CAST(ISNULL(fft.[FoodId], 0) AS BIT) [Active], fft.[IsDefault]
		FROM [FoodCategory] ft
		LEFT JOIN [FoodFoodCategory] fft ON fft.[FoodCategoryId] = ft.[Id] AND fft.[FoodId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END



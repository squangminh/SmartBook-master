CREATE PROCEDURE [dbo].[usp_Mobile_FoodLocation_GetByCategory]
	@pageSize INT,
	@skip INT,
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT DISTINCT fl.*
		FROM		[FoodLocation] fl
		INNER JOIN	[Food] f ON f.[FoodLocationId] = fl.[Id]
		INNER JOIN  [FoodFoodCategory] ffc ON ffc.[FoodId] = f.[Id]
		WHERE		ffc.[FoodCategoryId] = @CategoryId AND fl.[IsActive] = 1
		ORDER BY fl.[Name]
		OFFSET @skip ROWS 
		FETCH NEXT @pageSize ROWS ONLY;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

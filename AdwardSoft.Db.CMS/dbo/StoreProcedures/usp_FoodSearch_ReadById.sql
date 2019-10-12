CREATE PROCEDURE [dbo].[usp_FoodSearch_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT f.*, fl.[Latitude], fl.[Longitude], fl.[Name] AS [FoodLocationName]
		, [dbo].[fn_ConcatCategoryFood](f.[Id])  AS [Category], [dbo].[fn_ConcatCategoryLocation](fl.[Id])  AS [CategoryLocation]
		FROM [dbo].[Food] f
		INNER JOIN [dbo].[FoodLocation] fl ON f.[FoodLocationId] = fl.[Id]
		WHERE f.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

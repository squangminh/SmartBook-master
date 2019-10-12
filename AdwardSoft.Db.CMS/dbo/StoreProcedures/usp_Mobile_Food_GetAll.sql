CREATE PROCEDURE [dbo].[usp_Mobile_Food_GetAll]
	@LocationId INT

AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Number AS INT = 0
    --------------------------------------------------
    BEGIN TRY
        BEGIN
			SELECT f.*, fc.[Name] [CategoryName]
			FROM [Food] f
			INNER JOIN [FoodFoodCategory] ffc ON ffc.[FoodId] = f.[Id] AND ffc.[IsDefault] = 1
			INNER JOIN [FoodCategory] fc ON fc.[Id] = ffc.[FoodCategoryId]
			WHERE f.[FoodLocationId] = @LocationId
			ORDER BY fc.[Id], fc.[Sort]
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
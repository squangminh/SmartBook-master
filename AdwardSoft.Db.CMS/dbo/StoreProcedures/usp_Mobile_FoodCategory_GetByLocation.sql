CREATE PROCEDURE [dbo].[usp_Mobile_FoodCategory_GetByLocation]
	@LocationId INT

AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Number AS INT = 0
    --------------------------------------------------
    BEGIN TRY
        BEGIN
			SELECT DISTINCT f.[Id], SUBSTRING(( SELECT ' và ' + fc1.[Name]
			FROM [FoodFoodCategory] ffc1
			INNER JOIN [FoodCategory] fc1 ON fc1.[Id] = ffc1.[FoodCategoryId]
			WHERE ffc1.[FoodId] = ffc2.[FoodId]
			ORDER BY ffc1.[FoodId]
			FOR XML PATH ('')), 4, 1000) [Name]
			FROM [Food] f
			INNER JOIN [FoodFoodCategory] ffc2 ON f.[Id] = ffc2.[FoodId] AND ffc2.[IsDefault] <> 1
			INNER JOIN [FoodCategory] fc2 ON fc2.[Id] = ffc2.[FoodCategoryId]
			WHERE f.[FoodLocationId] = @LocationId
			--ORDER BY fc2.[Sort]
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

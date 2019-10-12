CREATE PROCEDURE [dbo].[usp_FoodFoodRefection_Read]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT fr.[Id] [RefectionId], fr.[Name], frt.[FoodId], CAST(ISNULL(frt.[FoodId], 0) AS BIT) [Active]
		FROM [FoodRefection] fr
		LEFT JOIN [FoodFoodRefection] frt ON frt.[FoodRefectionId] = fr.[Id] AND frt.[FoodId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END



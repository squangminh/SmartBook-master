CREATE PROCEDURE [dbo].[usp_FoodLocation_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	
	--------------------------------------------------
	BEGIN TRY	
		SELECT *
		FROM [dbo].[FoodLocation] f
		INNER JOIN [dbo].[OpeningTime] o ON f.[Id] = o.[FoodLocationId] 
		WHERE @Id = f.[Id]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
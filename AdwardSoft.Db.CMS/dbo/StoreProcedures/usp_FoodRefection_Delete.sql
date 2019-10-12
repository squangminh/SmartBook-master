CREATE PROCEDURE [dbo].[usp_FoodRefection_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	DECLARE @Sort INT = 0
	BEGIN TRY
		BEGIN TRAN
			DELETE [dbo].[FoodFoodRefection]
			WHERE [FoodRefectionId] = @Id

			DELETE [dbo].[FoodRefection]
			WHERE [Id] = @Id

		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return;
END

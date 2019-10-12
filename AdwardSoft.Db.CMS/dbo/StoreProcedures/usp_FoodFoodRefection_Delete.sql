CREATE PROCEDURE [dbo].[usp_FoodFoodRefection_Delete]
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
			WHERE [FoodId] = @Id
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return;
END

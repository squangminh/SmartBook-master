CREATE PROCEDURE [dbo].[usp_Food_Delete]
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
			DELETE [FoodFoodCategory] WHERE [FoodId] = @Id
			DELETE [FoodFoodRefection] WHERE [FoodId] = @Id
			DELETE [Food] WHERE [Id] = @Id
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return;
END

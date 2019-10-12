CREATE PROCEDURE [dbo].[usp_FoodCategory_Delete]
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
			SELECT @Sort = [Sort] FROM [dbo].[FoodCategory] WHERE [Id] = @Id

			DELETE [dbo].[FoodCategory]
			WHERE [Id] = @Id

			UPDATE [dbo].[FoodCategory] SET [Sort] = [Sort] - 1 WHERE [Sort] > @Sort
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return;
END

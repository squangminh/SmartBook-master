CREATE PROCEDURE [dbo].[usp_FoodLocation_Delete]
	@Id	INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			DELETE FROM [dbo].[OpeningTime]
			WHERE [FoodLocationId] = @Id

			DELETE FROM [dbo].[FoodLocation]
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

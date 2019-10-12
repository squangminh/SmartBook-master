CREATE PROCEDURE [dbo].[usp_FoodRefection_Update]
	@Id INT,
    @Name NVARCHAR(50) , 
    @DisplayTimeFrom VARCHAR(5), 
    @DisplayTimeTo VARCHAR(5)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [FoodRefection]
			SET [Name] = @Name,
				[DisplayTimeFrom] = @DisplayTimeFrom,
				[DisplayTimeTo] = @DisplayTimeTo
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return
END


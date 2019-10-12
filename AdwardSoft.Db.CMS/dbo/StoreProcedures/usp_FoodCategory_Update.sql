CREATE PROCEDURE [dbo].[usp_FoodCategory_Update]
	@Id INT , 
    @Name NVARCHAR(50) , 
    @Icon VARCHAR(2048) , 
    @Sort INT 
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [FoodCategory]
			SET [Name] = @Name,
				[Icon] = @Icon
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


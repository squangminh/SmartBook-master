CREATE PROCEDURE [dbo].[usp_Service_Update]
	@Id INT , 
    @Name NVARCHAR(50) , 
    @Amount NUMERIC(16, 2) , 
    @Description NVARCHAR(500) , 
    @NumberOfOrder INT , 
    @Day INT,
	@Image VARCHAR(2048)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[Service]
			SET [Name] = @Name,
				[Amount] = @Amount,
				[Description] = @Description,
				[NumberOfOrder] = @NumberOfOrder,
				[Day] = @Day,
				[Image] = @Image
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


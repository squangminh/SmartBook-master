CREATE PROCEDURE [dbo].[usp_Advertise_Update]
	@Id INT , 
    @Title NVARCHAR(50) , 
    @Image VARCHAR(2048) , 
    @Url VARCHAR(2048) 
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[Advertise]
			SET [Title] = @Title,
				[Image] = @Image,
				[Url] = @Url
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


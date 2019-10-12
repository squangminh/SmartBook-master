CREATE PROCEDURE [dbo].[usp_User_UpdatePassword]
	@Id BIGINT,	
	@PasswordHash NVARCHAR(MAX)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return AS BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[User]
			SET [PasswordHash] = @PasswordHash
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN;
		THROW;
	END CATCH

	RETURN @return
END

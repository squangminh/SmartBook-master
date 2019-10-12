CREATE PROCEDURE [dbo].[usp_User_UpdateConfirmEmail]
	@Id BIGINT,
	@Confirmed BIT
AS
BEGIN
	SET NOCOUNT OFF
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	-----------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			UPDATE [dbo].[User]
			SET [EmailConfirmed] = @Confirmed
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	--SELECT 1
END
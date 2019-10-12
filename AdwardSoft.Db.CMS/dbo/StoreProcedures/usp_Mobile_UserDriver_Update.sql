CREATE PROCEDURE [dbo].[usp_Mobile_UserDriver_Update]
	@UserId BIGINT,
	@LicensePlates VARCHAR(10),
	@IsActive BIT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			UPDATE [dbo].[UserDriver]
			SET [IsActive] = @IsActive
			WHERE [Userid] = @UserId
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
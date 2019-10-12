CREATE PROCEDURE [dbo].[usp_UserDriver_Create]
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
			INSERT INTO [dbo].[UserDriver] ([UserId], [LicensePlates], [IsActive])
			VALUES(@UserId, @LicensePlates, 0)
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
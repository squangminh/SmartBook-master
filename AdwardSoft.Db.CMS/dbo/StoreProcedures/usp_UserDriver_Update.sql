CREATE PROCEDURE [dbo].[usp_UserDriver_Update]
	@UserId BIGINT , 
    @LicensePlates VARCHAR(10) , 
    @IsActive BIT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			IF EXISTS (SELECT * FROM [dbo].[UserDriver] WHERE [UserId] = @UserId)
				BEGIN
					UPDATE [dbo].[UserDriver]
					SET [LicensePlates] = @LicensePlates
						--[IsActive] = @IsActive
					WHERE [UserId] = @UserId
				END
			ELSE
				BEGIN
					INSERT INTO [dbo].[UserDriver] ([UserId], [LicensePlates], [IsActive])
					VALUES(@UserId, @LicensePlates, 0)
				END
		COMMIT	
		SELECT 1;
	END TRY
	BEGIN CATCH
		SELECT 0
		ROLLBACK;
		THROW;
	END CATCH
END
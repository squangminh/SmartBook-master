CREATE PROCEDURE [dbo].[usp_Mobile_UserDriverActive_Update]
	@UserId BIGINT ,
    @IsActive BIT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[UserDriver]
			SET [IsActive] = @IsActive
			WHERE [UserId] = @UserId
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	SELECT [IsActive] FROM [dbo].[UserDriver] WHERE [UserId] = @UserId
END
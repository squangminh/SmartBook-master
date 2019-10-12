CREATE PROCEDURE [dbo].[usp_User_Delete]
	@Id BIGINT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
	DECLARE @res AS INT = 1
		BEGIN TRAN;
			DELETE FROM [AdwardSoftCMS].[dbo].[UserPlace]
			WHERE [UserId] = @Id
			
			DELETE FROM [dbo].[UserRole]
			WHERE [UserId] = @Id

			DELETE FROM [dbo].[User]
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @res = 0
		ROLLBACK TRAN
		THROW
	END CATCH
	SELECT @res
END

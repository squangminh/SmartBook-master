CREATE PROCEDURE [dbo].[usp_Role_ReadByEmail](
	@Email VARCHAR(256)
)	
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * FROM [dbo].[User]
				 WITH(INDEX(IX_User_Email))
				 WHERE [Email] = @Email
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
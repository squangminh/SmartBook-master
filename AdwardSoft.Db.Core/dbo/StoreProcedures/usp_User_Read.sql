CREATE PROCEDURE [dbo].[usp_User_Read]	
	@type TINYINT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		IF(@type <> 0)
		BEGIN
				SELECT * 
				FROM [dbo].[User]	
				WHERE [UserName] NOT IN ('AdwardSoft','Administrator', 'admin@adwardsoft.com') AND [Type] = @type
		END
		ELSE
		BEGIN
				SELECT * 
				FROM [dbo].[User]	
				WHERE [UserName] NOT IN ('AdwardSoft','Administrator', 'admin@adwardsoft.com')
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

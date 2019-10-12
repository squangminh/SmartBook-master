CREATE PROCEDURE [dbo].[usp_User_ReadByType]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN
			SELECT DISTINCT [Type] 
			FROM [dbo].[User]	
			WHERE [UserName] NOT IN ('AdwardSoft','Administrator') 
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
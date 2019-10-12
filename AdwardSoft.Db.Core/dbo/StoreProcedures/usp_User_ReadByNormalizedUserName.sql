-- =============================================
-- Author:		diemdv@adwardsoft.com
-- Create date: 05-09-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_ReadByNormalizedUserName]	
	@NormalizedUserName VARCHAR(256)
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[User]	
		WITH(INDEX(IX_User_UserName))
		WHERE [NormalizedUserName] = @NormalizedUserName
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

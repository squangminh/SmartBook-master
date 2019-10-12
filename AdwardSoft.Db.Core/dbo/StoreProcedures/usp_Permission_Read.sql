CREATE PROCEDURE [dbo].[usp_Permission_Read]
AS 
	BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[Permission]
		ORDER BY [ControllerName]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

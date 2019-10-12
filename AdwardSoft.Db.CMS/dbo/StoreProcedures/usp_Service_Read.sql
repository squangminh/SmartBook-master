CREATE PROCEDURE [dbo].[usp_Service_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Service] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
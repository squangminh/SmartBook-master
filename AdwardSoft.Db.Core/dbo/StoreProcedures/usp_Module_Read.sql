CREATE PROCEDURE [dbo].[usp_Module_Read]	
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[Module]
		ORDER BY [Sort]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
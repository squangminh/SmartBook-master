CREATE PROCEDURE [dbo].[usp_Support_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Support] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
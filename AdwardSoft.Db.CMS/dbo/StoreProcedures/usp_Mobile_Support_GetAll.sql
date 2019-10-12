CREATE PROCEDURE [dbo].[usp_Mobile_Support_GetAll]
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
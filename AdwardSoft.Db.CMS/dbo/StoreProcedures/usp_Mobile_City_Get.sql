CREATE PROCEDURE [dbo].[usp_Mobile_City_Get]
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[City]
		ORDER BY [Name]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
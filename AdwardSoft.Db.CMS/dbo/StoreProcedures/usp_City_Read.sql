CREATE PROCEDURE [dbo].[usp_City_Read]
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
CREATE PROCEDURE [dbo].[usp_Mobile_Service_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [Service] s
		ORDER BY s.[Name] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

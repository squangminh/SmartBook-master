CREATE PROCEDURE [dbo].[usp_Mobile_TransportFee_GetList]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[TransportFee] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
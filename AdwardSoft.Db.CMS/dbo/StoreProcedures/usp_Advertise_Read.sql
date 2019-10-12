CREATE PROCEDURE [dbo].[usp_Advertise_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Advertise] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
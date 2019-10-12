CREATE PROCEDURE [dbo].[usp_Mobile_PartnerService_Read]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[PartnerService] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
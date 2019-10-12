CREATE PROCEDURE [dbo].[usp_Mobile_AppSetting_Read]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[AppSetting]
		WHERE [Id] = 1
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
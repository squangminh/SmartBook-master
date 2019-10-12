CREATE PROCEDURE [dbo].[usp_WardDetail_Read]
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT w.*, c.[Name] AS [CityName]
		FROM [dbo].[Ward] w
		LEFT JOIN [dbo].[City] c ON c.[Id] = w.[CityId]
		ORDER BY w.[Name]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
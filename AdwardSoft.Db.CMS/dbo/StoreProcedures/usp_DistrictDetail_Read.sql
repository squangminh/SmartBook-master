CREATE PROCEDURE [dbo].[usp_DistrictDetail_Read]
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT d.*, c.[Name] AS [CityName]
		FROM [dbo].[District] d
		LEFT JOIN [dbo].[City] c ON c.[Id] = d.[CityId]
		ORDER BY d.[Name]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
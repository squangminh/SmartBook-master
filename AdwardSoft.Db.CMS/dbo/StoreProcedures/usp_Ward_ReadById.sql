CREATE PROCEDURE [dbo].[usp_Ward_ReadById]
	@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *, d.[Name]
		FROM		[dbo].[Ward] w
		INNER JOIN	[dbo].[District] d ON w.[DistrictId] = d.[Id]
		WHERE w.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
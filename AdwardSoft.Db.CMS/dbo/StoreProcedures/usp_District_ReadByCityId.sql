CREATE PROCEDURE [dbo].[usp_District_ReadByCityId]
	@CityId INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[District]
		WHERE [CityId] = @CityId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
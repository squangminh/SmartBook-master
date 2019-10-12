CREATE PROCEDURE [dbo].[usp_Ward_Create]
	@Id INT,
	@DistrictId INT,
	@CityId INT,
	@Name NVARCHAR(50)  
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
				INSERT INTO [dbo].[Ward] ([DistrictId], [CityId], [Name])
				VALUES (@DistrictId, @CityId, @Name)
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
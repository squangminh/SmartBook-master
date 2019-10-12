CREATE PROCEDURE [dbo].[usp_Ward_Update]
	@Id INT,
	@DistrictId INT,
	@CityId INT,
	@Name NVARCHAR(50)  
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[Ward]
			SET [Name] = @Name, [CityId] = @CityId, [DistrictId] = @DistrictId
			WHERE [Id] = @Id		
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
CREATE PROCEDURE [dbo].[usp_District_Update]
	@Id INT,
	@CityId INT,
	@Name NVARCHAR(50)  
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[District]
			SET [Name] = @Name, [CityId] = @CityId
			WHERE [Id] = @Id		
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
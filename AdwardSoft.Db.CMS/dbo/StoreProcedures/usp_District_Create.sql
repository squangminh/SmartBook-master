CREATE PROCEDURE [dbo].[usp_District_Create]
	@Id INT,
	@CityId INT,
	@Name NVARCHAR(50)  
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO [dbo].[District] ([CityId], [Name])
			VALUES (@CityId, @Name)
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
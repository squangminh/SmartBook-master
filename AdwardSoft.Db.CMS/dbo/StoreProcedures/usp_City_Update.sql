CREATE PROCEDURE [dbo].[usp_City_Update]
	@Id INT,
	@Name NVARCHAR(50)  
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[City]
			SET [Name] = @Name
			WHERE [Id] = @Id		
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_Role_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM	[dbo].[Role]
		WHERE	[Id] = @id
		ORDER BY [Name]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
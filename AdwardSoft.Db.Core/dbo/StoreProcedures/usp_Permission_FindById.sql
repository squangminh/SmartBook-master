CREATE PROCEDURE [dbo].[usp_Permission_FindById]
	@Id INT
AS 
	BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[Permission]
		WHERE [Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

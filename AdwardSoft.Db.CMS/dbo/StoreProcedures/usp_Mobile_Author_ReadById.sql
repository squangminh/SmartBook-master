CREATE PROCEDURE [dbo].[usp_Mobile_Author_ReadById]
	@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
			SELECT *
			FROM [Author] g
			WHERE g.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

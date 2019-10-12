CREATE PROCEDURE [dbo].[usp_GenreOfBook_ReadById]
@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
			SELECT *
			FROM [GenreOfBook] g
			WHERE g.[BookId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
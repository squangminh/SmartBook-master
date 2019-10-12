CREATE PROCEDURE [dbo].[usp_GenreOfBook_ReadByGenreId]
@GenreId INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
			SELECT *
			FROM [GenreOfBook] g
			WHERE g.[GenreId] = @GenreId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
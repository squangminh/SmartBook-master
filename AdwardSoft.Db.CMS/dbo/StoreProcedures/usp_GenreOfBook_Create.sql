CREATE PROCEDURE [dbo].[usp_GenreOfBook_Create]
	@GenreId INT,
	@BookId INT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO [GenreOfBook]
			VALUES (@GenreId, @BookId)
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

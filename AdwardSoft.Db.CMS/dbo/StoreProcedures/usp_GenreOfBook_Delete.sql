CREATE PROCEDURE [dbo].[usp_GenreOfBook_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN
			DELETE [dbo].[GenreOfBook]
			WHERE [BookId] = @Id
		COMMIT
	END TRY
	BEGIN CATCH
		SELECT 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END


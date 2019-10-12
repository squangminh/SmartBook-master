CREATE PROCEDURE [dbo].[usp_Book_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN
			DELETE [dbo].[BookAuthor]
			WHERE [BookId] = @Id

			DELETE [dbo].[GenreOfBook] 
			WHERE [BookId] = @Id

			DELETE [dbo].[Chapter] 
			WHERE [BookId] = @Id

			DELETE FROM [dbo].[Book]
			WHERE [Id] = @Id
			
		COMMIT
	END TRY
	BEGIN CATCH
		SELECT 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

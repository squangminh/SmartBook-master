CREATE PROCEDURE [dbo].[usp_Book_ReadById]
@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
			SELECT *
			FROM [Book] b
			WHERE b.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

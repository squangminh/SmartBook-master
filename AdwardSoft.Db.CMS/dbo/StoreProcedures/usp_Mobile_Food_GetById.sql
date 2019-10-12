CREATE PROCEDURE [dbo].[usp_Mobile_Food_GetById]
	@Id INT

AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Number AS INT = 0
    --------------------------------------------------
    BEGIN TRY
        BEGIN
			SELECT f.*, '' [LocationName]
			FROM [Food] f
			WHERE f.[Id] = @Id
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
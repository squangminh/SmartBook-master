CREATE PROCEDURE [dbo].[usp_Mobile_FoodLocation_GetById]
	@LocationId INT

AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Number AS INT = 0
    --------------------------------------------------
    BEGIN TRY
        BEGIN
			SELECT * FROM [FoodLocation] WHERE [Id] = @LocationId
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

CREATE PROCEDURE [dbo].[usp_Mobile_FoodCategory_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [FoodCategory] f
		ORDER BY f.[Sort]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

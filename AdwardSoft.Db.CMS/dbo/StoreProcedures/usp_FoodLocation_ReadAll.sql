CREATE PROCEDURE [dbo].[usp_FoodLocation_ReadAll]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[FoodLocation]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

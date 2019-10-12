CREATE PROCEDURE [dbo].[usp_FoodLocationSearch_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *, [dbo].[fn_ConcatCategoryLocation]([Id]) AS [Category]
		FROM [dbo].[FoodLocation] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
CREATE PROCEDURE [dbo].[usp_FoodRefection_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[FoodRefection] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END


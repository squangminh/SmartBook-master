CREATE PROCEDURE [dbo].[usp_FoodFoodRefection_Create]
	@FoodId INT , 
	@RefectionId INT , 
    @Name NVARCHAR(50) , 
    @Active BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[FoodFoodRefection] ([FoodId], [FoodRefectionId]) VALUES(@FoodId, @RefectionId)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

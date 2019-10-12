CREATE PROCEDURE [dbo].[usp_FoodFoodCategory_Create]
	@FoodId INT , 
    @CategoryId INT ,
	@Name NVARCHAR(50) , 
    @Active BIT,
	@IsDefault BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[FoodFoodCategory] ([FoodId], [FoodCategoryId], [IsDefault]) VALUES(@FoodId, @CategoryId, @IsDefault)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END
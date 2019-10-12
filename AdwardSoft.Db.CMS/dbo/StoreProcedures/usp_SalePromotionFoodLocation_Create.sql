CREATE PROCEDURE [dbo].[usp_SalePromotionFoodLocation_Create]
	@SalePromotionId INT,
	@FoodLocationId INT
AS
BEGIN 
	BEGIN TRY
		BEGIN TRAN
				BEGIN
					INSERT INTO [dbo].[SalePromotionFoodLocation] ([SalePromotionId], [FoodLocationId])
					VALUES (@SalePromotionId, @FoodLocationId);
				END
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		THROW
	END CATCH
	
END

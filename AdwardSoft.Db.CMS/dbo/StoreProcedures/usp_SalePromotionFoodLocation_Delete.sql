CREATE PROCEDURE [dbo].[usp_SalePromotionFoodLocation_Delete]
	@SalePromotionId INT
AS
BEGIN 
	DECLARE @Return BIT = 1
	BEGIN TRY
		BEGIN TRAN
			DELETE [dbo].[SalePromotionFoodLocation]
			WHERE [SalePromotionId] = @SalePromotionId
		COMMIT
	END TRY
	BEGIN CATCH
		SET @Return = 0
		ROLLBACK TRAN
		THROW
	END CATCH
	RETURN @Return
END

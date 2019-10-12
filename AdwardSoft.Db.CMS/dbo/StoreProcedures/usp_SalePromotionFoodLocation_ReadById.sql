CREATE PROCEDURE [dbo].[usp_SalePromotionFoodLocation_ReadById]
	@SalePromotionId INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[SalePromotionFoodLocation]
		WHERE [SalePromotionId] = @SalePromotionId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_Mobile_FoodLocation_GetBySalePromotion]
	@pageSize INT,
	@skip INT,
	@SaleId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT f.*
		FROM		[FoodLocation] f
		INNER JOIN	[SalePromotionFoodLocation] s ON s.[FoodLocationId] = f.[Id]		
		WHERE		s.[SalePromotionId] = @SaleId AND f.[IsActive] = 1
		ORDER BY f.[Name]
		OFFSET @skip ROWS 
		FETCH NEXT @pageSize ROWS ONLY;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_Mobile_SalePromotion_GetHomepage]
	@Type INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		IF(@Type = 3)
		BEGIN
			SELECT * 
			FROM  [dbo].[SalePromotion]
			WHERE [IsActive] = 1 AND [IsHomepage] = 1
		END
		ELSE
		BEGIN
			SELECT * 
			FROM  [dbo].[SalePromotion]
			WHERE [IsActive] = 1 AND [Type] = @Type AND [IsHomepage] = 1
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
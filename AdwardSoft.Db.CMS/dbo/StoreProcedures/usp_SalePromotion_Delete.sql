CREATE PROCEDURE [dbo].[usp_SalePromotion_Delete]
	@Id	INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	DECLARE @Sort INT
	BEGIN TRY
		BEGIN TRAN;
			SELECT @Sort = [Sort] FROM [dbo].[SalePromotion] WHERE [Id] = @Id

			DELETE FROM [dbo].[SalePromotion]
			WHERE [Id] = @Id
			
			UPDATE [dbo].[SalePromotion] SET [Sort] = [Sort] - 1 WHERE [Sort] > @Sort;
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END
CREATE PROCEDURE [dbo].[usp_SalePromotion_Sort]
	@Id INT,
	@Sort INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	DECLARE @TmpId INT
	DECLARE @IdChange INT
	BEGIN TRY
		BEGIN TRAN;
			
			
			IF(@Sort <> 1)
			BEGIN
				SELECT TOP 1 @TmpId = [Id] FROM [dbo].[SalePromotion] ORDER BY [Sort] DESC
				IF(@TmpId <> @Id)
				BEGIN
					SELECT @Sort = [Sort] FROM [dbo].[SalePromotion] WHERE [Id] = @Id
					SELECT @IdChange = [Id] FROM [dbo].[SalePromotion] WHERE [Sort] = @Sort + 1

					UPDATE [dbo].[SalePromotion]
					SET [Sort] = @Sort
					WHERE [Id] = @IdChange		
					
					UPDATE [dbo].[SalePromotion]
					SET [Sort] = @Sort + 1						
					WHERE [Id] = @Id	
				END				
			END
			ELSE
			BEGIN
				SELECT TOP 1 @TmpId = [Id] FROM [dbo].[SalePromotion] ORDER BY [Sort] ASC
				IF(@TmpId <> @Id)
				BEGIN
					SELECT @Sort = [Sort] FROM [dbo].[SalePromotion] WHERE [Id] = @Id
					SELECT @IdChange = [Id] FROM [dbo].[SalePromotion] WHERE [Sort] = @Sort -1

					UPDATE [dbo].[SalePromotion]
					SET [Sort] = @Sort					
					WHERE [Id] = @IdChange

					UPDATE [dbo].[SalePromotion]
					SET [Sort] = @Sort - 1						
					WHERE [Id] = @Id

				END			
			END			
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

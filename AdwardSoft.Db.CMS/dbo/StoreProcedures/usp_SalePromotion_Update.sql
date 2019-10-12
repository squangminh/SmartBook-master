CREATE PROCEDURE [dbo].[usp_SalePromotion_Update]
	@Id INT,
	@Title NVARCHAR(70),
	@Image VARCHAR(2048),	
	@Sort INT,
	@Type TINYINT,	
	@IsHomepage BIT,
	@IsActive BIT,
	@Count INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;						
			UPDATE [dbo].[SalePromotion]
			SET [Title] = @Title
			  , [Image] = @Image
			  , [Type] = @Type
			  , [IsHomepage] = @IsHomepage
			  , [IsActive] = @IsActive
			WHERE [Id] = @Id			
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

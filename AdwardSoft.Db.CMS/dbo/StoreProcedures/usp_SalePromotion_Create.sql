CREATE PROCEDURE [dbo].[usp_SalePromotion_Create]
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
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			SELECT @Sort = ISNULL(MAX([Sort]), 0) FROM [dbo].[SalePromotion]
			
			INSERT INTO [dbo].[SalePromotion]([Sort], [Title], [Image], [Type], [IsHomepage], [IsActive]) 
			VALUES(@Sort + 1, @Title, @Image, @Type, @IsHomepage, @IsActive)

			SET @Id = SCOPE_IDENTITY()
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @Id = 0
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
		
		SELECT  
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage; 
	END CATCH

	SELECT @Id;
END


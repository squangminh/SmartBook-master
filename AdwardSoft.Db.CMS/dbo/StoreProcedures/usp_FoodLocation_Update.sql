CREATE PROCEDURE [dbo].[usp_FoodLocation_Update]
	@Id INT,
	@Image VARCHAR(2048),
	@Name NVARCHAR(150),
	@Title NVARCHAR(70),
	@Address NVARCHAR(150),
	@Latitude FLOAT,
	@Longitude FLOAT,
	@Tel VARCHAR(20),
	@Note NVARCHAR(80),
	@IsActive BIT,
	@Count INT,
	@Monday1 VARCHAR(5),
	@Monday2 VARCHAR(5),
	@Tuesday1 VARCHAR(5),
	@Tuesday2 VARCHAR(5),
	@Webnesday1 VARCHAR(5),
	@Webnesday2 VARCHAR(5),
	@Thursday1 VARCHAR(5),
	@Thursday2 VARCHAR(5),
	@Friday1 VARCHAR(5),
	@Friday2 VARCHAR(5),
	@Saturday1 VARCHAR(5),
	@Saturday2 VARCHAR(5),
	@Sunday1 VARCHAR(5),
	@Sunday2 VARCHAR(5)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[FoodLocation] 
			SET [Image] = @Image
			  , [Name] = @Name
			  , [Title] = @Title
			  , [Address] = @Address
			  , [Latitude] = @Latitude
			  , [Longitude] = @Longitude
			  , [Tel] = @Tel
			  , [Note] = @Note
			  , [IsActive] = @IsActive
			WHERE [Id] = @Id

			UPDATE [dbo].[OpeningTime]
			SET [Monday1] = @Monday1
			  , [Monday2] = @Monday2
			  , [Tuesday1] = @Tuesday1
			  , [Tuesday2] = @Tuesday2
			  , [Webnesday1] = @Webnesday1
			  , [Webnesday2] = @Webnesday2
			  , [Thursday1] = @Thursday1
			  , [Thursday2] = @Thursday2
			  , [Friday1] = @Friday1
			  , [Friday2] = @Friday2
			  , [Saturday1] = @Saturday1
			  , [Saturday2] = @Saturday2
			  , [Sunday1] = @Sunday1
			  , [Sunday2] = @Sunday2
			WHERE [FoodLocationId] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
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

	SELECT @return;
END
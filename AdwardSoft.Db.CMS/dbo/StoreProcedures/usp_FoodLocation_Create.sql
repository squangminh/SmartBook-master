CREATE PROCEDURE [dbo].[usp_FoodLocation_Create]
	@Id INT,
	@Image VARCHAR(2048),
	@Name nvarchar(150),
	@Title NVARCHAR(70),
	@Address NVARCHAR(150),
	@Tel VARCHAR(20),
	@Note NVARCHAR(80),
	@Latitude FLOAT,
	@Longitude FLOAT,
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
	DECLARE @return INT = 0
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[FoodLocation] ([Image], [Name], [Title], [Address], [Tel], [Note], [IsActive], [Latitude], [Longitude])
			VALUES (@Image, @Name, @Title, @Address, @Tel, @Note, @IsActive, @Latitude, @Longitude)

			SET @return = SCOPE_IDENTITY();

			INSERT INTO [dbo].[OpeningTime] ([FoodLocationId], [Monday1], [Monday2], [Tuesday1], [Tuesday2], [Webnesday1], [Webnesday2]
			, [Thursday1], [Thursday2], [Friday1], [Friday2], [Saturday1], [Saturday2], [Sunday1], [Sunday2])
			VALUES (@return, @Monday1, @Monday2, @Tuesday1, @Tuesday2, @Webnesday1, @Webnesday2
			, @Thursday1, @Thursday2, @Friday1, @Friday2, @Saturday1, @Saturday2, @Sunday1, @Sunday2)
			
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

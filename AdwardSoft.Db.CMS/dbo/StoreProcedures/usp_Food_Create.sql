CREATE PROCEDURE [dbo].[usp_Food_Create]
	@Id INT,
	@Name NVARCHAR(150),
	@PriceOld NUMERIC(16,2),
	@Price NUMERIC(16,2),
	@Image VARCHAR(2048),
	@FoodLocationId INT,
	@Description NVARCHAR(300),
	@FoodLocationName NVARCHAR(150),
	@Count INT,
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Food] ([Name], [PriceOld], [Price], [Image], [FoodLocationId], [Description])
			VALUES(@Name, @PriceOld, @Price,@Image,@FoodLocationId, @Description)
			SET @return = SCOPE_IDENTITY() ;
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

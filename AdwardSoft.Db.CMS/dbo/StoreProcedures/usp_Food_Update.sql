CREATE PROCEDURE [dbo].[usp_Food_Update]
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
		BEGIN TRAN
			UPDATE	[dbo].[Food]
			SET		[Name] = @Name,
					[PriceOld] = @PriceOld,
					[Price] = @Price,
					[Image] = @Image,
					[FoodLocationId] = @FoodLocationId,
					[Description] = @Description
			WHERE	[Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

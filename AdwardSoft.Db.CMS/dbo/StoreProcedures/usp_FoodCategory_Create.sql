CREATE PROCEDURE [dbo].[usp_FoodCategory_Create]
	@Id INT , 
    @Name NVARCHAR(50) , 
    @Icon VARCHAR(2048) , 
    @Sort INT 
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			SET @Sort = (SELECT COUNT([Id]) FROM [FoodCategory])
			INSERT INTO [dbo].[FoodCategory] ([Name], [Icon], [Sort])
			VALUES(@Name, @Icon, @Sort + 1)
			SET @return = SCOPE_IDENTITY() ;
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

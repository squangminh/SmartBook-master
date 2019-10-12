CREATE PROCEDURE [dbo].[usp_OrderFood_Create]
	@OrderId CHAR(32) , 
    @FoodId INT ,
	@Quantity INT , 
    @Total NUMERIC(16, 2),
	@Name NVARCHAR(80)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[OrderFood] ([OrderId], [FoodId], [Quantity], [Total])
			VALUES(@OrderId, @FoodId, @Quantity, @Total)		
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

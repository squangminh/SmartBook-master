CREATE PROCEDURE [dbo].[usp_OrderDriverHistory_Create]
	@OrderId CHAR(32) , 
    @DriverId INT ,
	@Status INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[OrderDriverHistory] ([OrderId], [DriverId], [Status])
			VALUES(@OrderId, @DriverId, @Status)		
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

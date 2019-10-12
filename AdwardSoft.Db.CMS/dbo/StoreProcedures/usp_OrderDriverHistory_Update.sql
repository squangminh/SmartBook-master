CREATE PROCEDURE [dbo].[usp_OrderDriverHistory_Update]
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
			UPDATE [dbo].[OrderDriverHistory]
			SET [Status] = @Status
			WHERE [DriverId] = @DriverId AND [OrderId] = @OrderId			
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

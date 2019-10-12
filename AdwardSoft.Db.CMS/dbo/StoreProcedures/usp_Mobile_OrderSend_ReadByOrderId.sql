CREATE PROCEDURE [dbo].[usp_Mobile_OrderSend_ReadByOrderId]
	@OrderId CHAR(32)
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[OrderSend]
		WHERE [OrderId] = @OrderId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

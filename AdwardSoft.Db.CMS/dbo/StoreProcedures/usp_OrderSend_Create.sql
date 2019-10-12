CREATE PROCEDURE [dbo].[usp_OrderSend_Create]
	@OrderId CHAR(32) , 
    @Receiver NVARCHAR(30) ,
	@ReceiverPhone VARCHAR(20),
	@ReceiverAdress NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[OrderSend] ([OrderId], [Receiver], [ReceiverAdress], [ReceiverPhone])
			VALUES(@OrderId, @Receiver, @ReceiverPhone, @ReceiverAdress)		
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

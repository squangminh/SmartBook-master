CREATE PROCEDURE [dbo].[usp_CustomerServiceVNP_Update]
	@CustomerServiceCode CHAR(32), 
    @LanguageCode CHAR(2), 
    @ResponseCode VARCHAR(2), 
    @BankCode VARCHAR(20), 
    @BankTranNo VARCHAR(255), 
	@PayDate VARCHAR(14), 
	@TransactionNo VARCHAR(15), 
    @FireBaseDeviceToken VARCHAR(300),
	@IPAddress VARCHAR(20),
	@VNPSuccess INT
AS 
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;

			IF(@VNPSuccess = 2)
			BEGIN
				UPDATE [dbo].[CustomerServiceVNP]
			    SET [ResponseCode] = @ResponseCode				
			    ,  [PayDate] = @PayDate			    	   
			    WHERE [CustomerServiceCode] = @CustomerServiceCode
				
				
			END
			ELSE
			BEGIN
				UPDATE [dbo].[CustomerServiceVNP]
			    SET [ResponseCode] = @ResponseCode
				,  [BankTranNo] = @BankTranNo
			    ,  [PayDate] = @PayDate
			    ,  [TransactionNo] = @TransactionNo	
				,  [BankCode] = @BankCode
			    WHERE [CustomerServiceCode] = @CustomerServiceCode
			END   
			
			UPDATE [dbo].[CustomerService]
			SET [VNPSuccess] = @VNPSuccess					   
			WHERE [Id] = @CustomerServiceCode     

		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	RETURN @return;
END

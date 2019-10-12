CREATE PROCEDURE [dbo].[usp_CustomerService_Create]
	@Id CHAR(32),
	@CustomerId INT,
	@ServiceId TINYINT,
	@ExpiryDate SMALLDATETIME,
	@Date VARCHAR(10),
	@NumberOfOrder INT,
	@VNPSuccess VARCHAR(15),
	@TotalAmount NUMERIC(18,0),
	@DeviceToken VARCHAR(300),
	@IPAddress VARCHAR(20),
	@BankCode VARCHAR(50),
	@CurrencyCode CHAR(3),
	@LanguageCode CHAR(2)
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			SET @Id =  REPLACE(NEWID(), '-','')
			SET @Date = CONVERT(VARCHAR(10), GETDATE(), 103)
			SET @VNPSuccess = 0

			SELECT @ExpiryDate = DATEADD(DAY, [Day], GETDATE())
			FROM [dbo].[Service]
			WHERE [Id] = @ServiceId

			INSERT INTO [dbo].[CustomerService] ([Id], [CustomerId], [ServiceId], [ExpiryDate], [Date], [NumberOfOrder], [VNPSuccess])
			VALUES (@Id, @CustomerId, @ServiceId, @ExpiryDate, @Date, @NumberOfOrder, @VNPSuccess)

			INSERT INTO [CustomerServiceVNP] ([BankCode], [FireBaseDeviceToken], [IPAddress], [CustomerServiceCode], [TimeUpdate], [BankTranNo],  [TotalAmount], [PayDate], [ResponseCode], [TransactionNo], LanguageCode, CurrencyCode)
			VALUES ('', @DeviceToken, @IPAddress, @Id, GETDATE(), '', @TotalAmount, '', '', '', @LanguageCode, @CurrencyCode)
			
		COMMIT

			
	END TRY
	BEGIN CATCH
		SET @Id = ''
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

	SELECT @Id
		
END

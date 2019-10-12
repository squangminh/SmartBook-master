CREATE PROCEDURE [dbo].[usp_Order_Create]
	@Id VARCHAR(32), 
    @No INT, 
    @Date SMALLDATETIME, 
    @CustomerId BIGINT, 
	@DriverId BIGINT,
	@OrderType TINYINT,
    @Total NUMERIC(16, 2), 
    @Quantity INT, 
    @Discount NUMERIC(16, 2),
	@TransportFee NUMERIC(16, 2), 
    @GrandTotal NUMERIC(16, 2), 
    @Status TINYINT,
    @IsCompleted BIT,
	@Longitude1 FLOAT, 
    @Latitude1 FLOAT, 
    @Longitude2 FLOAT, 
    @Latitude2 FLOAT,
	@Note NVARCHAR(80),
	@Address1 NVARCHAR(200),
	@Address2 NVARCHAR(200),
	@AddressName1 NVARCHAR(200),
	@AddressName2 NVARCHAR(200),
	@Duration NUMERIC(4, 2),
	@Distance NUMERIC(4, 2),
	@CustomerServiceCode CHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @ServiceCode CHAR(32)
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			IF(@OrderType = 2)
			BEGIN
				SELECT TOP 1 @ServiceCode = [Id] FROM [dbo].[CustomerService] WHERE [ExpiryDate] >= CONVERT(DATE,GETDATE()) 
				AND [NumberOfOrder] > 0 AND [CustomerId] = @CustomerId AND [VNPSuccess] = 1 ORDER BY [ExpiryDate]
			END	

			SELECT @No = (COUNT([Id]) + 1) FROM [dbo].[Order] 
			SET @Id = REPLACE(NEWID(), '-','');
			INSERT INTO [dbo].[Order] ([Id], [No], [Date], [CustomerId], [DriverId], [OrderType],
			[Total], [Quantity], [Discount], [TransportFee], [GrandTotal], [Status], [IsCompleted],
			[Longitude1], [Latitude1], [Longitude2], [Latitude2], [Note],
			[Address1], [Address2], [AddressName1], [AddressName2], [Distance], [Duration], [CustomerServiceCode])
			VALUES(@Id, @No, @Date, @CustomerId, @DriverId, @OrderType,
			@Total, @Quantity, @Discount, @TransportFee, @GrandTotal, @Status, @IsCompleted,
			@Longitude1, @Latitude1, @Longitude2, @Latitude2, @Note,
			@Address1, @Address2, @AddressName1, @AddressName2, @Distance, @Duration, @ServiceCode)
			
			IF(@ServiceCode IS NOT NULL)
			BEGIN
				UPDATE  [dbo].[CustomerService]
				SET [NumberOfOrder] = [NumberOfOrder] - 1
				WHERE [Id] = @ServiceCode
			END

		COMMIT	
	END TRY
	BEGIN CATCH
		SET @Id = '';
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @Id
END

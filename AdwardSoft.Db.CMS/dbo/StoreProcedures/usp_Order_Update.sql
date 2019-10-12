CREATE PROCEDURE [dbo].[usp_Order_Update]
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
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [Order]
			SET [Date] = @Date,
				[CustomerId] = @CustomerId,
				[DriverId] = @DriverId,
				[OrderType] = @OrderType,
				[Total] = @Total,
				[Quantity] = @Quantity,
				[Discount] = @Discount,
				[TransportFee] = @TransportFee,
				[GrandTotal] = @GrandTotal,
				[Status] = @Status ,
				[IsCompleted] = @IsCompleted,
				[Longitude1] = @Longitude1,
				[Latitude1] = @Latitude1,
				[Longitude2] = @Longitude2,
				[Latitude2] = @Latitude2,
				[Note] = @Note,
				[Address1] = @Address1,
				[Address2] = @Address2,
				[AddressName1] = @AddressName1,
				[AddressName2] = @AddressName2,
				[Duration] = @Duration,
				[Distance] = @Distance
			WHERE [Id] = @Id

			IF(@OrderType = 2 AND @CustomerServiceCode IS NOT NULL)
			BEGIN
				UPDATE  [dbo].[CustomerService]
				SET [NumberOfOrder] = [NumberOfOrder] + 1
				WHERE [Id] = @CustomerServiceCode
			END	
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @Id = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @Id = @return
END

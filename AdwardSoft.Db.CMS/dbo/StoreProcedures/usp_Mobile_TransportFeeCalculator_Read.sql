CREATE PROCEDURE [dbo].[usp_Mobile_TransportFeeCalculator_Read]
	@UserId BIGINT,
	@Distance NUMERIC(16,2),
	@TypeOrder INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @Fee NUMERIC(16,2)
	DECLARE @Total NUMERIC(16,2) 
	BEGIN TRY
		
		SELECT @Fee = [Fee]
		FROM [dbo].[TransportFee]
		WHERE CONVERT(TIME,[TimeStart]) <= CONVERT(TIME,GETDATE()) AND CONVERT(TIME,[TimeEnd]) >= CONVERT(TIME,GETDATE())
		AND [Type] = @TypeOrder

		

		IF(@TypeOrder = 0)
		BEGIN
			IF EXISTS(SELECT TOP 1 1 FROM [dbo].[CustomerService] WHERE [ExpiryDate] >= CONVERT(DATE,GETDATE()) 
			AND [NumberOfOrder] > 0 AND [CustomerId] = @UserId AND [VNPSuccess] = 1)
			BEGIN
				SET @Total = 0;
			END
			ELSE
			BEGIN
				SET @Total = @Fee;
			END
			
		END
		ELSE IF (@TypeOrder = 1)
		BEGIN
			SET @Total = @Distance*@Fee;
		END
		ELSE
		BEGIN
			SET @Total = @Fee;
		END
	
		SELECT @Total AS [Total]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
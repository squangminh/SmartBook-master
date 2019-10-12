CREATE PROCEDURE [dbo].[usp_Mobile_CustomerServiceVNP_Read]
	@CustomerServiceCode CHAR(32)	
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		SELECT csvpn.[CustomerServiceCode] AS [CustomerServiceCode], csvpn.[LanguageCode] AS [LanguageCode] 
		,csvpn.[BankCode] AS [BankCode], csvpn.[ResponseCode] AS [ResponseCode]
		,csvpn.[FireBaseDeviceToken] AS [FireBaseDeviceToken] ,csvpn.[IPAddress] AS [IPAddress]
		, csvpn.[CurrencyCode] AS [CurrencyCode], cs.[NumberOfOrder] AS [OrderNo], csvpn.[TotalAmount]  AS [TotalAmount]
		FROM [dbo].[CustomerServiceVNP] csvpn
		INNER JOIN [CustomerService] cs ON cs.[Id] = csvpn.[CustomerServiceCode]
		WHERE csvpn.[CustomerServiceCode] = @CustomerServiceCode
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

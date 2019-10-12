CREATE PROCEDURE [dbo].[usp_Mobile_CustomerService_GetByUserId]
	@UserId BIGINT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT cs.[Id], cs.[CustomerId], cs.[ServiceId], cs.[ExpiryDate]
		, cs.[Date], csv.[TotalAmount], s.[Name], s.[Description], cs.[NumberOfOrder]
		FROM [dbo].[CustomerService] cs
		INNER JOIN [dbo].[Service] s ON s.[Id] = cs.[ServiceId]
		INNER JOIN [dbo].[CustomerServiceVNP] csv ON csv.[CustomerServiceCode] = cs.[Id]
		WHERE @UserId = cs.[CustomerId] AND cs.[VNPSuccess] = 1
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

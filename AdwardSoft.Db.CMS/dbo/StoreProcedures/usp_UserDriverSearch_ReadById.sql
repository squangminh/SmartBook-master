CREATE PROCEDURE [dbo].[usp_UserDriverSearch_ReadById]
@Id BIGINT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT  ud.[UserId] AS [Id], u.[UserName] AS [UserName], u.[PhoneNumber] AS [PhoneNumber], u.[Avatar] AS [Avatar], u.[FullName] AS [FullName], u.[Type] AS [Type]
			, ud.[LicensePlates] AS [LicensePlates], ud.[IsActive] AS [isActive] , 0.0 AS [Latitude], 0.0 AS [Longitude], 0 AS [ConnectUser]
			, 0 AS [DriverActivity]
		FROM [AdwardSoftCore.QB].[dbo].[User] u
		INNER JOIN [dbo].[UserDriver] ud ON u.[Id] = ud.[UserId]
		--LEFT JOIN [Order] o ON o.[DriverId] = ud.[UserId] AND o.[Date] = (SELECT MAX([Date]) FROM [Order] od WHERE od.[DriverId] = ud.[UserId])
		WHERE u.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END


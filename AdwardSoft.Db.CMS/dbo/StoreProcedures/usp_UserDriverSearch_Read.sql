CREATE PROCEDURE [dbo].[usp_UserDriverSearch_Read]
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT  ud.[UserId] AS [Id], u.[UserName] AS [UserName], u.[PhoneNumber] AS [PhoneNumber], u.[Avatar] AS [Avatar], u.[FullName] AS [FullName], u.[Type] AS [Type]
			, ud.[LicensePlates] AS [LicensePlates], ud.[IsActive] AS [isActive] , 0.0 AS [Latitude], 0.0 AS [Longitude], 0 AS [ConnectUser]
			,  (CASE
					WHEN o.[Status] = 0 THEN 1 -- pending
					WHEN o.[Status] = 1 THEN 2 -- process
					ELSE 0	-- none
				END) AS [DriverActivity]
		FROM [AdwardSoftCore.QB].[dbo].[User] u
		INNER JOIN [dbo].[UserDriver] ud ON u.[Id] = ud.[UserId]
		LEFT JOIN [Order] o ON o.[DriverId] = ud.[UserId] AND o.[Date] = (SELECT MAX([Date]) FROM [Order] od WHERE od.[DriverId] = ud.[UserId])
		WHERE u.[Type] = 3 OR u.[Type] = 4
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_Mobile_UserDriverDetail_ReadById]
	@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT ud.[IsActive], ud.[LicensePlates], u.[Id], u.[Avatar], u.[PhoneNumber], u.[Username] 
		FROM [dbo].[UserDriver] ud
		LEFT JOIN [AdwardSoftCore].[dbo].[User] u ON ud.[UserId] = u.[Id]
		WHERE ud.[UserId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
CREATE PROCEDURE [dbo].[usp_Mobile_PartnerService_ReadByPartnerId]
	@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[PartnerService]
		WHERE [PartnerId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

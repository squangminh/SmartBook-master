CREATE PROCEDURE [dbo].[usp_Mobile_PartnerService_Delete]
	@Id CHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		DELETE [dbo].[PartnerService]
		WHERE [Id] = @Id

		SELECT 1;
	END TRY
	BEGIN CATCH
		SELECT 0;
		THROW;
	END CATCH
END

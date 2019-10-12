CREATE PROCEDURE [dbo].[usp_Mobile_PartnerService_Create]
	@Id INT , 
    @PartnerId BIGINT, 
    @ServiceId INT,
	@ExpiryDate DATE,
	@Date VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
			DECLARE @ServiceDay INT = 0
			SELECT @ServiceDay = [Day] FROM [dbo].[Service] WHERE [Id] = @ServiceId

			INSERT INTO [PartnerService] ([Id], [PartnerId], [ServiceId], [Date], [ExpiryDate]) 
			VALUES (REPLACE(NEWID(), '-', ''), @PartnerId, @ServiceId, CONCAT(RIGHT('0' + RTRIM(DAY(GETDATE())), 2), '/', RIGHT('0' + RTRIM(MONTH(GETDATE())), 2), '/', YEAR(GETDATE())), DATEADD(DAY, @ServiceDay, GETDATE()))
		COMMIT	
		SELECT SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH
		SELECT ''
		ROLLBACK;
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_AppSetting_Update]
	@Id INT,
	@Author NVARCHAR(150),
	@Address NVARCHAR(150),
	@Version VARCHAR(20),
	@Phone VARCHAR(20),
	@Email VARCHAR(60),
	@IsNotification BIT,
	@TermsOfService NVARCHAR(MAX),
	@PrivacyPolicy NVARCHAR(MAX),
	@UpdateLocationTime INT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			UPDATE [dbo].[AppSetting]
			SET [Author] = @Author, [Address] = @Address, [Version] = @Version, [Phone] = @Phone, [Email] = @Email
			, [IsNotification] = @IsNotification, [TermsOfService] = @TermsOfService, [PrivacyPolicy] = @PrivacyPolicy, [UpdateLocationTime] = @UpdateLocationTime
			WHERE [Id] = 1
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
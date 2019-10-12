CREATE PROCEDURE [dbo].[usp_AppSetting_Create]
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
			INSERT INTO [dbo].[AppSetting] ([Id], [Author], [Address], [Version], [Phone], [Email], [IsNotification], [TermsOfService], [PrivacyPolicy], [UpdateLocationTime])
			VALUES (1, @Author, @Address, @Version, @Phone, @Email, @IsNotification, @TermsOfService, @PrivacyPolicy, @UpdateLocationTime)
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
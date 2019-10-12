CREATE PROCEDURE [dbo].[usp_User_Update]
	@Id INT,
	@UserName VARCHAR(256),
	@NormalizedUserName VARCHAR(256),
	@Email VARCHAR(256),
	@NormalizedEmail VARCHAR(256),
	@EmailConfirmed BIT,
	@PasswordHash NVARCHAR(max),
	@SecurityStamp NVARCHAR(max),
	@ConcurrencyStamp NVARCHAR(max),
	@PhoneNumber VARCHAR(50),
	@PhoneNumberConfirmed BIT,
	@TwoFactorEnabled BIT,
	@LockoutEndDateUtc DATETIME,
	@LockoutEnabled BIT ,
	@AccessFailedCount INT,
	@FullName NVARCHAR(128),
	@Avatar VARCHAR(256),
	@Type TINYINT ,
	@Claims INT,
	@Roles INT
	--@OldPassword	VARCHAR(50),
	--@NewPassword	VARCHAR(50),
	--@ParentUserId BIGINT,
	--@PlaceId INT,
	--@LocationId INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[User]
			   SET [UserName] = @UserName
				  ,[NormalizedUserName] = @NormalizedUserName
				  ,[Email] = @Email
				  ,[NormalizedEmail] = @NormalizedEmail
				  ,[EmailConfirmed] = @EmailConfirmed
				  ,[PasswordHash] = @PasswordHash
				  ,[SecurityStamp] = @SecurityStamp
				  ,[ConcurrencyStamp] = @ConcurrencyStamp
				  ,[PhoneNumber] = @PhoneNumber
				  ,[PhoneNumberConfirmed] = @PhoneNumberConfirmed
				  ,[TwoFactorEnabled] = @TwoFactorEnabled
				  ,[LockoutEnabled] = @LockoutEnabled
				  ,[AccessFailedCount] = @AccessFailedCount
				  ,[FullName] =@FullName
				  ,[Avatar] = @Avatar
				  ,[Type] = @Type
			 WHERE Id = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

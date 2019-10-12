CREATE PROCEDURE [dbo].[usp_User_Create]
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
	DECLARE @return INT = 0
	BEGIN TRY
	
		BEGIN TRAN;
		INSERT INTO [dbo].[User]
           ([UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[FullName]
           ,[Avatar]
		   ,[Type])
		 VALUES
           (@UserName
           ,@NormalizedUserName
           ,@Email
           ,@NormalizedEmail
           ,@EmailConfirmed
           ,@PasswordHash
           ,@SecurityStamp
           ,@ConcurrencyStamp
           ,@PhoneNumber
           ,@PhoneNumberConfirmed
           ,@TwoFactorEnabled
           ,@LockoutEnabled
           ,@AccessFailedCount
           ,@FullName
           ,@Avatar
		   ,@Type)

		SET @return = SCOPE_IDENTITY();
		INSERT INTO [dbo].[UserRole] ([UserId], [RoleId])
		VALUES (@return, 4)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK TRAN;
		THROW
	END CATCH
	SELECT @return
END

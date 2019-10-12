CREATE TABLE [dbo].[User]
(
	[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(256) NOT NULL, 
	[NormalizedUserName] VARCHAR(256) NULL,
    [Email] VARCHAR(256) NOT NULL, 
	[NormalizedEmail] VARCHAR(256) NULL, 
    [EmailConfirmed] BIT NOT NULL , 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [SecurityStamp] NVARCHAR(MAX) NOT NULL, 
	[ConcurrencyStamp] NVARCHAR(MAX) NULL, 
    [PhoneNumber] VARCHAR(50) NULL, 
    [PhoneNumberConfirmed] BIT NOT NULL, 
    [TwoFactorEnabled] BIT NOT NULL, 
    [LockoutEndDateUtc] DATETIME NULL, 
    [LockoutEnabled] BIT NOT NULL, 
    [AccessFailedCount] INT NOT NULL, 
	[FullName] NVARCHAR(128) NOT NULL,
    [Avatar] VARCHAR(256) NULL, 
    [Type] TINYINT NOT NULL DEFAULT 1, --1: Intelnal, 2: Customer, 3: Driver
)

GO

CREATE INDEX [IX_User_Email] ON [dbo].[User] ([Email])

GO

CREATE UNIQUE INDEX [IX_User_UserName] ON [dbo].[User] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL)

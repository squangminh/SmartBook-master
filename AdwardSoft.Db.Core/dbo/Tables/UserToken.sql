CREATE TABLE [dbo].[UserToken]
(
	[UserId] BIGINT NOT NULL PRIMARY KEY, 
    [LoginProvider] NVARCHAR(128) NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Value] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_UserToken_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

CREATE TABLE [dbo].[UserLogin]
(
	[LoginProvider] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [ProviderKey] NVARCHAR(128) NOT NULL, 
    [ProviderDisplayName] NVARCHAR(MAX) NULL, 
    [UserId] BIGINT NOT NULL
)

CREATE TABLE [dbo].[Claim]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] BIGINT NOT NULL, 
    [ClaimType] NVARCHAR(MAX) NULL, 
    [ClaimValue] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Claim_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

GO

CREATE INDEX [IX_Claim_UserId] ON [dbo].[Claim] ([UserId])

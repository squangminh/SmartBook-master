CREATE TABLE [dbo].[RoleClaims]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [RoleId] INT NOT NULL, 
    [ClaimType] NVARCHAR(MAX) NULL, 
    [ClaimValue] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_RoleClaim_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
)



GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims] ([RoleId])

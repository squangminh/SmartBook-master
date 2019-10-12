CREATE TABLE [dbo].[UserRole]
(
	[UserId] BIGINT NOT NULL , 
    [RoleId] INT NOT NULL, 
    PRIMARY KEY ([RoleId], [UserId]), 
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
)

GO

CREATE INDEX [IX_UserRoles_UserId] ON [dbo].[UserRole] ([UserId])

GO

CREATE INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRole] ([RoleId])

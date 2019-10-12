CREATE TABLE [dbo].[RolePermission]
(
	[RoleId] INT NOT NULL , 
    [PermissionId] INT NOT NULL, 
    PRIMARY KEY ([PermissionId], [RoleId]), 
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id]), 
    CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [Permission]([Id])
)

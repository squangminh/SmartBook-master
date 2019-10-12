CREATE PROCEDURE [dbo].[usp_Role_Permission_Create]
	@RoleId INT,
	@PermissionId INT
AS
BEGIN 
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO [dbo].[RolePermission] ([RoleId], [PermissionId])
			VALUES (@RoleId, @PermissionId);
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		THROW
	END CATCH
	
END

CREATE PROCEDURE [dbo].[usp_Role_Permission_Read]
	@Id	INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM		[dbo].[RolePermission] rp
		INNER JOIN	[Permission] p ON rp.[PermissionId] = p.[Id]
		WHERE		[RoleId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

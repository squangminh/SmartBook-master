CREATE PROCEDURE [dbo].[usp_Permission_CheckUse]
	@Id INT
AS 
	BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT TOP 1 p.*
		FROM [dbo].[Permission] p
		INNER JOIN [dbo].[RolePermission] rp ON rp.[PermissionId] = p.[Id]
		WHERE [Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

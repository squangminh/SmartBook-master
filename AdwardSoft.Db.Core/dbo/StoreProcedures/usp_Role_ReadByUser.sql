CREATE PROCEDURE [dbo].[usp_Role_ReadByUser](
	@UserId BIGINT
)	
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		-- Get roles
		SELECT ur.[UserId], r.[Name] AS RoleName, r.[Id] AS [RoleId]
		INTO #UserRole
		FROM [dbo].[UserRole] ur 
		INNER JOIN [dbo].[Role] r ON ur.[RoleId] = r.[Id]
		WHERE ur.[UserId] = @UserId

		-- Check administrtor role
		IF EXISTS(SELECT TOP 1 1 FROM #UserRole WHERE [RoleName] LIKE 'Administrator%')
			BEGIN	
				
				SELECT 'full_access'
			END
		ELSE
			BEGIN
				SELECT 
					STUFF((SELECT ', ' + RTRIM(p.[ControllerName]) + '.' + RTRIM(p.[ActionName])
					FROM #UserRole tmp
					INNER JOIN [dbo].[RolePermission] rp ON tmp.[RoleId] = rp.[RoleId]
					INNER JOIN [dbo].[Permission] p ON rp.[PermissionId] = p.[Id]
					WHERE tmp.[UserId] = ur.[UserId]
					ORDER BY p.[ControllerName], p.[ActionName]
					FOR XML PATH('')), 1, 1, '')
				FROM #UserRole ur
				GROUP BY ur.[UserId] 

			END
			DROP TABLE IF EXISTS #UserRole

	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
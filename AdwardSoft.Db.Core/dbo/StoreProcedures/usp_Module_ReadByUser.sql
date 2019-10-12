CREATE PROCEDURE [dbo].[usp_Module_ReadByUser]
    @UserId BIGINT
AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Module_Tempory TABLE (
            Idx INT,
            ParentIdx INT
    )
    --------------------------------------------------
    BEGIN TRY
        SELECT ur.[UserId], r.[Name] AS RoleName
        INTO #UserRole
        FROM [UserRole] ur
        INNER JOIN [Role] r ON ur.[RoleId] = r.[Id]
        WHERE ur.[UserId] = @UserId

        -- Check administrtor role
        IF EXISTS(SELECT TOP 1 1 FROM #UserRole WHERE [RoleName] LIKE 'Administrator%')
            BEGIN    
                SELECT        *
                FROM    [Module]
                WHERE    [Id] = [ParentId] OR [Id] IN
                (        
                        SELECT DISTINCT m.[Id]
                        FROM        [Module] m
                )
                ORDER BY [Sort]
            END
        ELSE
            BEGIN
                INSERT @Module_Tempory([Idx], [ParentIdx])
                SELECT DISTINCT m.[Id], m.[ParentId]
                FROM        [Module] m
                INNER JOIN    [Permission] p ON m.[ControllerName] = p.[ControllerName]
                INNER JOIN    [RolePermission] rp ON rp.[PermissionId] = p.[Id]
                INNER JOIN    [Role] r ON r.[Id] = rp.[RoleId]
                INNER JOIN    [UserRole] ur ON r.[Id] = ur.[RoleId]
                INNER JOIN    [User] u ON u.[Id] = ur.[UserId]
                WHERE        u.[Id] = @UserId AND p.[ActionName] like '%Access%'

                SELECT        *
                FROM    [Module]
                WHERE    [Id] IN (SELECT [ParentIdx] FROM @Module_Tempory) OR [Id] IN (SELECT [Idx] FROM @Module_Tempory)            
                ORDER BY [Sort]
            END

        DROP TABLE IF EXISTS #UserRole
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

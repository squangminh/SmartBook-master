CREATE PROCEDURE [dbo].[usp_RolesUser_Read]
	AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
        SELECT u.[id] UserId,
        STUFF((SELECT    '; ' + r.[Name]
        FROM        [UserRole] ur
        INNER JOIN    [Role] r ON r.[Id] = ur.[RoleId]
        WHERE        ur.[UserId] = u.[id]
        FOR XML PATH ('')), 1, 1, '') as RolesOfUser, MAX(u.[FullName]) AS FullName
        FROM [User] as u
        INNER JOIN [UserRole] ur ON u.[Id] = ur.[UserId]
        WHERE u.[id] <> 1
        GROUP BY u.[id]
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
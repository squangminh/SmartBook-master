CREATE PROCEDURE [dbo].[usp_Role_User_Read]
	@Id	INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM		[dbo].[UserRole] ur
		INNER JOIN	[Role] r ON ur.[RoleId] = r.[Id]
		WHERE		ur.[UserId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END


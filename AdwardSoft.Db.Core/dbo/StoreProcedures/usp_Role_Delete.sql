CREATE PROCEDURE [dbo].[usp_Role_Delete]
	@Id INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 0
	BEGIN TRY
		BEGIN TRAN;
			DECLARE @count INT = (SELECT COUNT(*) FROM [UserRole] WHERE [RoleId] =@Id)
			IF(@count = 0)
			BEGIN
			DELETE FROM [dbo].[RolePermission]
			WHERE [RoleId] = @Id
			DELETE FROM [dbo].[Role]
			WHERE [Id] = @Id
			SET @return = 1
			END
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return;
END
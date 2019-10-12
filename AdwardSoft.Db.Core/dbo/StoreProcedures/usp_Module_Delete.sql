CREATE PROCEDURE [dbo].[usp_Module_Delete]
	@Id INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
		DECLARE @count INT = (SELECT COUNT(*) FROM [dbo].[Module] WHERE [ParentId] = @Id AND [Id] <> @Id)
		IF(@count = 0 )
		BEGIN
			DELETE FROM [dbo].[Module]
			WHERE [Id] = @Id
		END
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		THROW;
	END CATCH
	SELECT @return
END

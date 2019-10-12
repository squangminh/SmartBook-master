CREATE PROCEDURE [dbo].[usp_UserDriver_Delete]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			DELETE FROM [dbo].[UserDriver]
			WHERE [UserId] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK;
		THROW;
	END CATCH
END

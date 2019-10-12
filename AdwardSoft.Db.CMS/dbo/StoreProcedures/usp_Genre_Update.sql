CREATE PROCEDURE [dbo].[usp_Genre_Update]
	@Id INT ,
	@Name NVARCHAR(250) ,
	@Description NVARCHAR(1000)
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN
			UPDATE [Genre]
			SET [Name] = @Name,
				[Description] = @Description
			WHERE [Id] = @Id
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

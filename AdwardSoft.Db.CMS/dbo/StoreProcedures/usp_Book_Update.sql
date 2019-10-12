CREATE PROCEDURE [dbo].[usp_Book_Update]
	@Id INT ,
	@Name NVARCHAR(250) ,
	@Image VARCHAR(2048) ,
	@Description NVARCHAR(1000) ,
	@Status INT , 
	@TimeCreate SMALLDATETIME,
	@CreateUserId BIGINT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN
			UPDATE [Book]
			SET [Name] = @Name,
				[Image] = @Image,
				[Description] = @Description,
				[Status] = @Status,
				[TimeCreate] = @TimeCreate,
				[CreateUserId] = @CreateUserId
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

CREATE PROCEDURE [dbo].[usp_Book_Create]
	@Id INT ,
	@Name NVARCHAR(250) ,
	@Image VARCHAR(2048) ,
	@Description NVARCHAR(1000) ,
	@Status INT , 
	@TimeCreate SMALLDATETIME,
	@CreateUserId BIGINT,
	@Count INT 
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 0
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO [Book] ([Name], [Image], [Description], [Status], [TimeCreate], [CreateUserId])
			VALUES (@Name, @Image, @Description, @Status, @TimeCreate, @CreateUserId)
			SET @return = SCOPE_IDENTITY()  
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

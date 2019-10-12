CREATE PROCEDURE [dbo].[usp_Genre_Create]
	@Id INT ,
	@Name NVARCHAR(250) ,
	@Description NVARCHAR(1000)

AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 0
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO [Genre]
			VALUES (@Name , @Description)
			SET @return = SCOPE_IDENTITY()  
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

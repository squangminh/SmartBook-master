CREATE PROCEDURE [dbo].[usp_Permission_Create]
	@Id INT,
	@Description NVARCHAR(128), 
    @ControllerName VARCHAR(50), 
    @ActionName VARCHAR(20)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Permission] 
			VALUES(@Description, @ControllerName, @ActionName)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

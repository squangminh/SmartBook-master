CREATE PROCEDURE [dbo].[usp_Module_Update]
	@Id INT,
	@Title NVARCHAR(50), 
    @Link VARCHAR(256), 
    @ClassName VARCHAR(50),
	@ControllerName VARCHAR(60),
	@ParentId INT,
	@Sort TINYINT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return BIT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[Module]
			   SET [Title] = @Title, [Link] = @Link,[ClassName] = @ClassName
			 WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

CREATE PROCEDURE [dbo].[usp_Module_Create]
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
	DECLARE @NewId INT
	
	BEGIN TRY
		BEGIN TRAN;
		IF(@ParentId<>0)
			BEGIN
			    SELECT @Sort = COUNT([Sort]) FROM [dbo].[Module] WHERE [ParentId] = @ParentId
				INSERT INTO [dbo].[Module] ([Title], [Link], [ClassName], [ControllerName], [ParentId], [Sort])
				VALUES(@Title, @Link, @ClassName, @ControllerName, @ParentId, @Sort+1)						
			END
			ELSE
			BEGIN			
				INSERT INTO [dbo].[Module] ([Title], [Link], [ClassName], [ControllerName], [ParentId], [Sort])
				VALUES(@Title, @Link, @ClassName, @ControllerName, 1, 1)
				SET @NewId = SCOPE_IDENTITY();

				UPDATE [dbo].[Module] SET [ParentId] = @NewId WHERE [Id] = @NewId
			END
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

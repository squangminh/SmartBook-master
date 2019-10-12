CREATE PROCEDURE [dbo].[usp_Support_Update]
	@Id INT , 
    @Type TINYINT , 
    @Title NVARCHAR(150) , 
    @Content NVARCHAR(MAX) 
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [dbo].[Support]
			SET [Type] = @Type,
				[Title] = @Title,
				[Content] = @Content
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK TRAN
		THROW;
	END CATCH
	SELECT @return
END


CREATE PROCEDURE [dbo].[usp_FeedbackIsRead_Update]
	@Id VARCHAR(32),
	@IsRead BIT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			UPDATE [dbo].[Feedback]
			SET [IsRead] = @IsRead
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
CREATE PROCEDURE [dbo].[usp_Feedback_ReadById]
	@Id VARCHAR(32)
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Feedback]
		WHERE @Id = [Id] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
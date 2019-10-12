CREATE PROCEDURE [dbo].[usp_Mobile_Support_GetByType]
	@Type TINYINT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Support]
		WHERE [Type] = @Type
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END


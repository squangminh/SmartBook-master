CREATE PROCEDURE [dbo].[usp_Mobile_Advertise_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [Advertise] a
		ORDER BY a.[Title]
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

CREATE PROCEDURE [dbo].[usp_Feedback_ReadByDate]
	@dateFrom DATE,
	@dateTo DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[Feedback]
		WHERE [Date] >= @dateFrom AND [Date] <= @dateTo 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
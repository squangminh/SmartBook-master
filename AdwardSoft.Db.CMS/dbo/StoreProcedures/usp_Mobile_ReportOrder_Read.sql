CREATE PROCEDURE [dbo].[usp_Mobile_ReportOrder_Read]
	@UserId BIGINT	
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT  tmp.[Total] AS [Total], tmp.[TotalSuccess] AS [TotalSuccess], (CONVERT(float ,tmp.[TotalSuccess])/CONVERT(float , tmp.[Total])*100) AS [PercentSucess]
		FROM (
		SELECT COUNT([Id]) AS [Total], SUM(CASE WHEN [Status] = 2 THEN 1 ELSE 0 END) AS [TotalSuccess]
		FROM [dbo].[Order]
		WHERE [DriverId] = @UserId AND YEAR(GETDATE()) = YEAR([Date]) AND MONTH(GETDATE()) = MONTH([Date])
		) tmp
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

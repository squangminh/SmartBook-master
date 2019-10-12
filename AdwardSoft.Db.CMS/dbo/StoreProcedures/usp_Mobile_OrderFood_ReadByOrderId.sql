CREATE PROCEDURE [dbo].[usp_Mobile_OrderFood_ReadByOrderId]
	@OrderId CHAR(32)
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT o.*, f.[Name] AS [Name]
		FROM [dbo].[OrderFood] o
		INNER JOIN [dbo].[Food] f ON f.[Id] = o.[FoodId]
		WHERE o.[OrderId] = @OrderId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

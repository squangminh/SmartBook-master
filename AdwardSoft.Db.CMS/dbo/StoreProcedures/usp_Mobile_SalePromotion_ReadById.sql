CREATE PROCEDURE [dbo].[usp_Mobile_SalePromotion_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM	[dbo].[SalePromotion] sp
		WHERE sp.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

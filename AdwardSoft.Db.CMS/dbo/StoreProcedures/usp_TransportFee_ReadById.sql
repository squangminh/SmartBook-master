CREATE PROCEDURE [dbo].[usp_TransportFee_ReadById]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[TransportFee]
		WHERE  [Id] = @Id 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
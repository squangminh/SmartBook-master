CREATE PROCEDURE [dbo].[usp_TransportFee_ReadForValidation]
@Id INT,
@Type TINYINT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT *
		FROM [dbo].[TransportFee]
		WHERE [Type] = @Type AND [Id] NOT IN (@Id)
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

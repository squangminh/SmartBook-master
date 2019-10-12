﻿CREATE PROCEDURE [dbo].[usp_District_Delete]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM [dbo].[District]
			WHERE [Id] = @Id
		COMMIT	
		SELECT 1
	END TRY
	BEGIN CATCH
		SELECT 0
		ROLLBACK;
		THROW;
	END CATCH
END
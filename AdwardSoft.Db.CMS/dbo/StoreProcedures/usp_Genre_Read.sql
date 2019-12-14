﻿CREATE PROCEDURE [dbo].[usp_Genre_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	----------------------------------------------
	BEGIN TRY
		BEGIN TRAN
			SELECT * FROM [Genre]
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END
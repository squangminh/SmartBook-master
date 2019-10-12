﻿CREATE PROCEDURE [dbo].[usp_Mobile_UserDriver_ReadById]
	@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		SELECT * 
		FROM [dbo].[UserDriver]
		WHERE [UserId] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
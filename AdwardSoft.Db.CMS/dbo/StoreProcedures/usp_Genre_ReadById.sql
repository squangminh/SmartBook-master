﻿CREATE PROCEDURE [dbo].[usp_Genre_ReadById]
@Id INT
AS 
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
			SELECT *
			FROM [Genre] g
			WHERE g.[Id] = @Id
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

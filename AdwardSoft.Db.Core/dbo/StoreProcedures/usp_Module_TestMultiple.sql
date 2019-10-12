CREATE PROCEDURE [dbo].[usp_Module_TestMultiple] @Id INT 
AS
	SELECT  ID FROM[dbo].[Module] WHERE Id = @Id

	SELECT * FROM [dbo].[Module]
RETURN 0

-- =======================================================  
-- Author:      <AdwardSoft>  
-- Create date: <Create Date: Nov 15, 2018>  
-- Description: <Description: Sorting Module by JsonData>  
-- =======================================================
CREATE PROCEDURE [dbo].[usp_Module_Sort]
	@Json VARCHAR(MAX)
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return AS BIT = 1 

	BEGIN TRY 
		BEGIN TRAN;
			-- Read Json String
			WITH q AS
			(
				SELECT [key] nodePath, 
					   CAST(JSON_VALUE(d.[value],'$.id') AS INT) [Id],
					   CAST(NULL AS INT) [ParentId],
					   CAST(JSON_QUERY(d.[value],'$.children') AS NVARCHAR(MAX)) [Children]
				FROM OPENJSON(@Json) d
				UNION ALL
				SELECT q.nodePath + '.' + d.[key] nodePath, 
					   CAST(JSON_VALUE(d.[value],'$.id') AS INT) [Id],
					   q.Id [ParentId], 
					   CAST(JSON_QUERY(d.[value],'$.children') AS NVARCHAR(MAX)) [Children]
				FROM q
				OUTER APPLY OPENJSON(q.children) d
				WHERE q.children IS NOT NULL
			)
			SELECT Id, ROW_NUMBER() OVER (ORDER BY nodePath) [Sort], 
			(CASE 
				WHEN [ParentId] IS NULL THEN [Id]
				ELSE [ParentId] END) AS [ParentId]
			INTO #JsonData
			FROM q
			ORDER BY [Id]

			--- Update target table
			UPDATE t
			SET t.[Sort] = s.[Sort], t.[ParentId] = s.[ParentId]
			FROM [dbo].[Module] t INNER JOIN #JsonData s
			ON t.[Id] = s.[Id]

			DROP TABLE IF EXISTS #JsonData;
		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

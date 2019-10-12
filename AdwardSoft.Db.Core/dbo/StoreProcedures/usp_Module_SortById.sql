-- =======================================================  
-- Author:      <AdwardSoft>  
-- Create date: <Create Date: Nov 15, 2018>  
-- Description: <Description: Sorting Module>  
-- =======================================================
CREATE PROCEDURE [dbo].[usp_Module_SortById]
	@Id INT,
	@Type CHAR(1) = 'T'
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return AS BIT = 1 
		DECLARE @current_sort TINYINT = 1
		DECLARE @prev_node_id SMALLINT = 0
		DECLARE @next_node_id SMALLINT = 0
		DECLARE @max_sort TINYINT = 0

		BEGIN TRY 
			BEGIN TRAN;
			--Current node
			SELECT @current_sort = [Sort] FROM [dbo].[Module] WHERE [Id] = @Id
		
			IF(@Type = 'U') -- UP
			BEGIN
				IF (@current_sort > 1)
				BEGIN
					SELECT @prev_node_id = [Id] FROM [dbo].[Module]
					WHERE [Sort] = @current_sort -1

					UPDATE [dbo].[Module] SET [Sort] = @current_sort -1 WHERE [Id] = @Id
					UPDATE [dbo].[Module] SET [Sort] = @current_sort WHERE [Id] = @prev_node_id
				END
			END
			IF(@Type = 'D') -- DOWN
			BEGIN
				-- Get max
				SElECT @max_sort = [Sort] FROM [dbo].[Module]
				IF (@current_sort < @max_sort)
				BEGIN
					SELECT @next_node_id = [Id] FROM [dbo].[Module]
					WHERE [Sort] = @current_sort + 1

					UPDATE [dbo].[Module] SET [Sort] = @current_sort + 1 WHERE [Id] = @Id
					UPDATE [dbo].[Module] SET [Sort] = @current_sort WHERE [Id] = @next_node_id
				END
			END

			IF(@Type = 't') -- TOP
			BEGIN
				-- Update current node
				UPDATE [dbo].[Module] SET [Sort] = 1 WHERE [Id] = @Id 
				--Update other node in table
				UPDATE r
				SET [Sort] = new_sort_node + 1
				FROM (
					SELECT * ,  new_sort_node = ROW_NUMBER()
					OVER(ORDER BY [Sort])
					FROM [dbo].[Module] WHERE [Id] != @Id
					) AS r
			END

			IF(@Type = 'b') -- BOT
			BEGIN
			
				--Update other node in table
				UPDATE r
				SET [Sort] = new_sort_node 
				FROM (
					SELECT * ,  new_sort_node = ROW_NUMBER()
					OVER(ORDER BY [Sort])
					FROM [dbo].[Module] WHERE [Id] != @Id
					) AS r

				-- Get max
				SElECT @max_sort = [Sort] FROM [dbo].[Module]

				-- Update current node
				UPDATE [dbo].[Module] SET [Sort] = @max_sort + 1 WHERE [Id] = @Id 
			END
			COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	RETURN @return;
END

CREATE PROCEDURE [dbo].[usp_Book_ReadPagination]
	@userId BIGINT,
	@pageSize INT,
	@skip INT,
	@searchBy NVARCHAR(50)
AS 
	BEGIN
	
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	DECLARE @Total INT
	--------------------------------------------------
	BEGIN TRY
		
		DECLARE @SqlStr1 NVARCHAR(MAX),
				@SqlStr2 NVARCHAR(MAX), 
				@ParamList  NVARCHAR(2000) = '@userId BIGINT,
											  @pageSize INT,
											  @skip INT,
											  @searchBy NVARCHAR(50)'
		
		SET @SqlStr1 = N'DECLARE @Total INT
						SELECT @Total = COUNT(bt.[Id])
						FROM [dbo].[Book] bt 
						WHERE bt.[CreateUserId] = @userId ';
		
		
		SET @SqlStr2 = N'SELECT * 
						FROM [Book] bt 
						WHERE bt.[CreateUserId] = @userId ';

		IF(@searchBy <> 'NULL')
		BEGIN
			SET @SqlStr1 = @SqlStr1 + N'AND bt.[Name] LIKE ''%''+@searchBy+''%'' '
			SET @SqlStr2 = @SqlStr2 + N'AND bt.[Name] LIKE ''%''+@searchBy+''%'' '
		END

		SET @SqlStr1 = @SqlStr1 + @SqlStr2 + N' ORDER BY bt.[TimeCreate]
												OFFSET @skip ROWS 
												FETCH NEXT @pageSize ROWS ONLY; '
		EXEC SP_EXECUTESQL @SqlStr1, 
						   @ParamList,
						   @userId,
						   @pageSize,
						   @skip,
						   @searchBy
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

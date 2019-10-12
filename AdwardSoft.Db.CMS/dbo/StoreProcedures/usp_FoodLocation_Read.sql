CREATE PROCEDURE [dbo].[usp_FoodLocation_Read]
	@pageSize INT,
    @skip INT,
    @searchBy NVARCHAR(50)
AS BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    DECLARE @Number AS INT = 0
    --------------------------------------------------
    BEGIN TRY
        BEGIN
			DECLARE @SqlStr1 NVARCHAR(MAX),
					@SqlStr2 NVARCHAR(MAX), 
					@ParamList  NVARCHAR(2000) = '@pageSize INT,
												  @skip INT,
												  @searchBy NVARCHAR(50) '
			SET @SqlStr1 = N'DECLARE @Number AS INT = 0
							SELECT @Number = COUNT(e.[Id])
							FROM [dbo].[FoodLocation] e ';
			
			SET @SqlStr2 = N'SELECT * , @Number AS [Count]
							FROM [dbo].[FoodLocation] e ';

			IF(@searchBy <> 'NULL')
			BEGIN
				SET @SqlStr1 = @SqlStr1 + N'WHERE e.[Name] LIKE ''%''+@searchBy+''%'' '
				SET @SqlStr2 = @SqlStr2 + N'WHERE e.[Name] LIKE ''%''+@searchBy+''%'' '
			END


			SET @SqlStr1 = @SqlStr1 + @SqlStr2 + N' ORDER BY e.[Name]  
													OFFSET @skip ROWS
													FETCH NEXT @pageSize ROWS ONLY;'
			EXEC SP_EXECUTESQL @SqlStr1, 
							   @ParamList,
							   @pageSize,
							   @skip,
							   @searchBy
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

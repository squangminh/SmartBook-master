CREATE PROCEDURE [dbo].[usp_Food_Read]
	@LocationId INT,
	@CategoryId NVARCHAR(MAX),
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
					@SqlWhere NVARCHAR(MAX) = N'WHERE 1 = 1 '

			SET @SqlStr1 = N'DECLARE @Number AS INT = 0
							SELECT @Number = COUNT(*)
							FROM [dbo].[Food] f ';
			
			SET @SqlStr2 = N'SELECT f.*, fl.[Name] [FoodLocationName] , @Number AS [Count]
							FROM [dbo].[Food] f INNER JOIN [FoodLocation] fl ON f.[FoodLocationId] = fl.[Id] ';

			IF(@CategoryId <> 'NULL')
			BEGIN
				SET @SqlWhere = N' INNER JOIN [FoodFoodCategory] fft ON fft.[FoodId] = f.[Id] INNER JOIN STRING_SPLIT('''+CONVERT(NVARCHAR(MAX), @CategoryId)+''','','') spl ON spl.[value] = fft.[FoodCategoryId] WHERE 1 = 1 '
			END

			IF(@LocationId <> 0)
			BEGIN
				SET @SqlWhere +=  + N'AND f.[FoodLocationId] = ' + CONVERT(NVARCHAR(MAX), @LocationId)
			END

			IF(@searchBy <> 'NULL')
			BEGIN
				SET @SqlWhere += N'AND f.[Name] LIKE ''%'+@searchBy+'%'' '
			END


			SET @SqlStr1 = @SqlStr1 + @SqlWhere + ' ' + @SqlStr2 + @SqlWhere + ' ' +  N' ORDER BY f.[Name] OFFSET '+CONVERT(NVARCHAR(MAX), @skip) + ' ROWS FETCH NEXT ' +CONVERT(NVARCHAR(MAX), @pageSize) + ' ROWS ONLY;'
			EXEC SP_EXECUTESQL @SqlStr1
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
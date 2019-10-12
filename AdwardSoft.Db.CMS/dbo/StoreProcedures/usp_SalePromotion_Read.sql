CREATE PROCEDURE [dbo].[usp_SalePromotion_Read]
	@pageSize INT,
    @skip INT,
    @searchBy NVARCHAR(50),
	@type INT
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
												  @searchBy NVARCHAR(50),
												  @type INT '
			SET @SqlStr1 = N'DECLARE @Number AS INT = 0
							SELECT @Number = COUNT(e.[Id])
							FROM [dbo].[SalePromotion] e ';
			
			SET @SqlStr2 = N'SELECT *, @Number AS [Count]
							FROM [dbo].[SalePromotion] e ';

			IF(@searchBy <> 'NULL')
			BEGIN
				SET @SqlStr1 = @SqlStr1 + N'WHERE e.[Title] LIKE ''%''+@searchBy+''%'' '
				SET @SqlStr2 = @SqlStr2 + N'WHERE e.[Title] LIKE ''%''+@searchBy+''%'' '
				IF(@type <> 3)
				BEGIN
					SET @SqlStr1 = @SqlStr1 + N'AND e.[Type] = @type '
					SET @SqlStr2 = @SqlStr2 + N'AND e.[Type] = @type '
				END
			END
			ELSE
			BEGIN
				IF(@type <> 3)
				BEGIN
					SET @SqlStr1 = @SqlStr1 + N'WHERE e.[Type] = @type '
					SET @SqlStr2 = @SqlStr2 + N'WHERE e.[Type] = @type '
				END
			END


			SET @SqlStr1 = @SqlStr1 + @SqlStr2 + N' ORDER BY e.[Sort]  
													OFFSET @skip ROWS
													FETCH NEXT @pageSize ROWS ONLY;'
			EXEC SP_EXECUTESQL @SqlStr1, 
							   @ParamList,
							   @pageSize,
							   @skip,
							   @searchBy,
							   @type
        END       
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

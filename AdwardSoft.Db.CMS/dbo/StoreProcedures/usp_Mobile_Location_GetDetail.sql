CREATE PROCEDURE [dbo].[usp_Mobile_Location_GetDetail]
	@Id INT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	
	DECLARE @Opentime AS VARCHAR(5)
	DECLARE @OpentimeFuture AS VARCHAR(5)
	DECLARE @Closetime AS VARCHAR(5)
	DECLARE @IsOpen AS BIT = 0
	DECLARE @Desciption AS NVARCHAR(50)
	--------------------------------------------------
	BEGIN TRY
		SELECT @OpentimeFuture = (CASE WHEN datepart(dw,getdate())  = 8 THEN [Monday1]
			WHEN datepart(dw,getdate())  = 2 THEN o.[Tuesday1]
			WHEN datepart(dw,getdate())  = 3 THEN o.[Webnesday1]
			WHEN datepart(dw,getdate())  = 4 THEN o.[Thursday1]
			WHEN datepart(dw,getdate())  = 5 THEN o.[Friday1]
			WHEN datepart(dw,getdate())  = 6 THEN o.[Saturday1]
			WHEN datepart(dw,getdate())  = 7 THEN o.[Sunday2]
       END),
	    @Opentime = (CASE WHEN datepart(dw,getdate())  = 2 THEN [Monday1]
			WHEN datepart(dw,getdate())  = 3 THEN o.[Tuesday1]
			WHEN datepart(dw,getdate())  = 4 THEN o.[Webnesday1]
			WHEN datepart(dw,getdate())  = 5 THEN o.[Thursday1]
			WHEN datepart(dw,getdate())  = 6 THEN o.[Friday1]
			WHEN datepart(dw,getdate())  = 7 THEN o.[Saturday1]
			WHEN datepart(dw,getdate())  = 8 THEN o.[Sunday2]
       END),
	   @Closetime = (CASE WHEN datepart(dw,getdate())  = 2 THEN [Monday2]
			WHEN datepart(dw,getdate())  = 3 THEN o.[Tuesday2]
			WHEN datepart(dw,getdate())  = 4 THEN o.[Webnesday2]
			WHEN datepart(dw,getdate())  = 5 THEN o.[Thursday2]
			WHEN datepart(dw,getdate())  = 6 THEN o.[Friday2]
			WHEN datepart(dw,getdate())  = 7 THEN o.[Saturday2]
			WHEN datepart(dw,getdate())  = 8 THEN o.[Sunday2]
       END)
		FROM [dbo].[FoodLocation] f
		INNER JOIN [dbo].[OpeningTime] o ON f.[Id] = o.[FoodLocationId] 
		WHERE @Id = f.[Id] 

		IF (CAST(@Opentime AS TIME) <= CAST(GETDATE() AS TIME) AND CAST(@Closetime AS TIME) >= CAST(GETDATE() AS TIME))
		BEGIN
			SET @IsOpen = 1
			SET @Desciption = N'ĐẾN ' + @Closetime + N' HÔM NAY';
		END
		ELSE
		BEGIN
			IF(CAST(GETDATE() AS TIME) >= CAST(@Closetime AS TIME))
			BEGIN
				SET @Desciption = N'MỞ CỬA LÚC ' + @Opentime + N' NGÀY MAI';
			END
			ELSE
			BEGIN
				SET @Desciption = N'MỞ CỬA LÚC ' + @Opentime + N' HÔM NAY';
			END			
		END

		SELECT *, @IsOpen AS [IsOpen], @Desciption AS [Desciption], [dbo].[fn_ConcatCategoryLocation](f.[Id])  AS [Category]
		FROM [dbo].[FoodLocation] f
		INNER JOIN [dbo].[OpeningTime] o ON f.[Id] = o.[FoodLocationId] 
		WHERE @Id = f.[Id] 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

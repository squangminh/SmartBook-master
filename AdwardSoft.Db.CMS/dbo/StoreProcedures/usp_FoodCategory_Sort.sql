CREATE PROCEDURE [dbo].[usp_FoodCategory_Sort]
	@Id INT,
	@Sort INT
AS BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	DECLARE @TmpId INT
	DECLARE @IdChange INT
	BEGIN TRY
		BEGIN TRAN;
			IF(@Sort <> 1) -- up
			BEGIN
				SELECT TOP 1 @TmpId = [Id] FROM [dbo].[FoodCategory]  ORDER BY [Sort] DESC
				IF(@TmpId <> @Id)
				BEGIN
					SELECT @Sort = [Sort] FROM [dbo].[FoodCategory] WHERE [Id] = @Id
					SELECT @IdChange = [Id] FROM [dbo].[FoodCategory] WHERE [Sort] = @Sort + 1

					UPDATE [dbo].[FoodCategory]
					SET [Sort] = @Sort
					WHERE [Id] = @IdChange

					UPDATE [dbo].[FoodCategory]
					SET [Sort] = @Sort + 1
					WHERE [Id] = @Id

				END
				ELSE
				BEGIN
					SET @return = 0
				END
			END
			ELSE -- down
			BEGIN
				SELECT TOP 1 @TmpId = [Id] FROM [dbo].[FoodCategory]  ORDER BY [Sort] ASC
				IF(@TmpId <> @Id)
				BEGIN
					SELECT @Sort = [Sort] FROM [dbo].[FoodCategory] WHERE [Id] = @Id
					SELECT @IdChange = [Id] FROM [dbo].[FoodCategory]  WHERE  [Sort] = @Sort -1

					UPDATE [dbo].[FoodCategory]
					SET [Sort] = @Sort
					WHERE [Id] = @IdChange

					UPDATE [dbo].[FoodCategory]
					SET [Sort] = @Sort - 1
					WHERE [Id] = @Id

				END
				ELSE
				BEGIN
					SET @return = 0
				END
			END
			
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK TRAN
		THROW;
	END CATCH

	SELECT @return;
END
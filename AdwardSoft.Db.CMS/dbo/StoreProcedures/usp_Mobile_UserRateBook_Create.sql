CREATE PROCEDURE [dbo].[usp_Mobile_UserRateBook_Create]
	@UserId BIGINT,
	@BookId INT,
	@Rate INT
AS
BEGIN
	SET NOCOUNT OFF;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT = 1
	DECLARE @oldRate INT
	BEGIN TRY
		BEGIN TRAN
			IF EXISTS(SELECT TOP 1 1 FROM [UserRateBook] WHERE [UserId] = @UserId AND [BookId] = @BookId)
			BEGIN
				SELECT @oldRate = [Rate] FROM [UserRateBook] WHERE [UserId] = @UserId AND [BookId] = @BookId

				UPDATE [UserRateBook]
				SET [Rate] = Rate
				WHERE [UserId] = @UserId AND [BookId] = @BookId

				IF(@oldRate = 1)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint1] = [RatingPoint1] - 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 2)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint2] = [RatingPoint2] - 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 3)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint3] = [RatingPoint3] - 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 4)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint4] = [RatingPoint4] - 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 5)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint5] = [RatingPoint5] - 1
					WHERE [BookId] = @BookId
				END
				
			END
			ELSE
			BEGIN
				INSERT INTO [UserRateBook]
				VALUES (@UserId, @BookId, @Rate)

				UPDATE [BookRate]
				SET [RatingCount] = [RatingCount] + 1
				WHERE [BookId] = @BookId
			END

				IF(@Rate = 1)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint1] = [RatingPoint1] + 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 2)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint2] = [RatingPoint2] + 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 3)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint3] = [RatingPoint3] + 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 4)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint4] = [RatingPoint4] + 1
					WHERE [BookId] = @BookId
				END
				ELSE IF(@oldRate = 5)
				BEGIN
					UPDATE [BookRate]
					SET [RatingPoint5] = [RatingPoint5] + 1
					WHERE [BookId] = @BookId
				END

				UPDATE [BookRate]
				SET [RatingPoint] = CONVERT(DECIMAL(3,1), (([RatingPoint5]*5) + ([RatingPoint4]*4) + ([RatingPoint3]*3) + ([RatingPoint2]*2) + ([RatingPoint1]*1))/[RatingCount])
				WHERE [BookId] = @BookId

		COMMIT
	END TRY
	BEGIN CATCH
		SET @return = 0
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

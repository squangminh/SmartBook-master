CREATE PROCEDURE [dbo].[usp_Role_CreateOrUpdate]
	@Id INT,
	@Name NVARCHAR(128),
	@NormalizedName NVARCHAR(256),
	@ConcurrencyStamp NVARCHAR(MAX),
	@IsDefault BIT
AS BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	BEGIN TRY
		BEGIN TRAN;
		IF(@Id = 0)
		BEGIN
			DECLARE @count INT = (SELECT COUNT(*) FROM [Role] WHERE LOWER([Name]) = LOWER(@Name))
			IF(@count = 0)
			BEGIN
			INSERT INTO [dbo].[Role]
						(
							[Name],
							[NormalizedName],
							[ConcurrencyStamp],
							[IsDefault]
						)
			VALUES		(
							@Name,
							@NormalizedName,
							@ConcurrencyStamp,
							@IsDefault
						)
			SELECT SCOPE_IDENTITY();
			END
			ELSE
			BEGIN
			SELECT 0
			END
		END
		ELSE
		BEGIN
			UPDATE	[dbo].[Role]
			SET		[Name] = @Name,
					[NormalizedName] = @NormalizedName,
					[ConcurrencyStamp] = @ConcurrencyStamp,
					[IsDefault] = @IsDefault
			WHERE	[Id] = @Id
		END
		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		THROW;
	END CATCH
	
END

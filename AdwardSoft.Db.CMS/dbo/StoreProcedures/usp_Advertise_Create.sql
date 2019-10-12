CREATE PROCEDURE [dbo].[usp_Advertise_Create]
	@Id INT , 
    @Title NVARCHAR(50) , 
    @Image VARCHAR(2048) , 
    @Url VARCHAR(2048) NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Advertise] ([Title], [Image], [Url])
			VALUES(@Title, @Image, @Url)
			SET @return = SCOPE_IDENTITY();
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

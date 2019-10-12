CREATE PROCEDURE [dbo].[usp_Service_Create]
	@Id INT , 
    @Name NVARCHAR(50) , 
    @Amount NUMERIC(16, 2) , 
    @Description NVARCHAR(500) , 
    @NumberOfOrder INT , 
    @Day INT,
	@Image VARCHAR(2048)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Service] ([Name], [Amount], [Description], [NumberOfOrder], [Day], [Image])
			VALUES(@Name, @Amount, @Description, @NumberOfOrder, @Day, @Image)
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
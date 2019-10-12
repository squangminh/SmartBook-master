CREATE PROCEDURE [dbo].[usp_Support_Create]
	@Id INT , 
    @Type TINYINT , 
    @Title NVARCHAR(150) , 
    @Content NVARCHAR(MAX) 
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[Support] ([Type], [Title], [Content])
			VALUES(@Type, @Title, @Content)
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

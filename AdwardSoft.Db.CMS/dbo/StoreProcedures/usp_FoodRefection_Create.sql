CREATE PROCEDURE [dbo].[usp_FoodRefection_Create]
	@Id INT , 
    @Name NVARCHAR(50) , 
    @DisplayTimeFrom VARCHAR(5), 
    @DisplayTimeTo VARCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [FoodRefection] ([Name], [DisplayTimeFrom], [DisplayTimeTo]) VALUES (@Name, @DisplayTimeFrom, @DisplayTimeTo)
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END

CREATE PROCEDURE [dbo].[usp_TransportFee_Create]
	@Id INT ,
    @TimeStart VARCHAR(5) , 
    @TimeEnd VARCHAR(5) , 
    @Fee NUMERIC(16, 2) , 
    @Type TINYINT 
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	--------------------------------------------------
	DECLARE @return INT 
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO [dbo].[TransportFee] ([TimeStart], [TimeEnd], [Fee], [Type])
			VALUES(@TimeStart, @TimeEnd, @Fee, @Type)
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

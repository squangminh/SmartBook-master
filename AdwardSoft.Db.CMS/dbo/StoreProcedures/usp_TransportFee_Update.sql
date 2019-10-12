CREATE PROCEDURE [dbo].[usp_TransportFee_Update]
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
	DECLARE @return INT = 1
	BEGIN TRY
		BEGIN TRAN;
			UPDATE [TransportFee] 
			SET [TimeStart] = @TimeStart,
				[TimeEnd] = @TimeEnd,
				[Fee] = @Fee,
				[Type] = @Type
			WHERE [Id] = @Id
		COMMIT	
	END TRY
	BEGIN CATCH
		SET @return = 0;
		ROLLBACK;
		THROW;
	END CATCH
	SELECT @return
END


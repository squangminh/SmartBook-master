CREATE FUNCTION [dbo].[ufn_SplitString]
(
	@Text       VARCHAR(MAX),
	@Separator  VARCHAR(MAX)
)
RETURNS @tableOut TABLE(VALUE VARCHAR(MAX))
AS
BEGIN
	DECLARE @value  VARCHAR(MAX),
	        @Pos    INT
	
	SET @Text = LTRIM(RTRIM(@Text)) + @Separator
	SET @Pos = CHARINDEX(@Separator, @Text, LEN(@Separator))
	
	IF REPLACE(@Text, @Separator, '') <> ''
	BEGIN
	    WHILE @Pos > 0
	    BEGIN
	        SET @value = LTRIM(RTRIM(LEFT(@Text, @Pos - 1)))
	        IF @value <> ''
	        BEGIN
	            INSERT INTO @tableOut(VALUE) VALUES(@value)
	        END
	        
	        SET @Text = RIGHT(@Text, LEN(@Text) -(@Pos + LEN(@Separator) - 1))
	        SET @Pos = CHARINDEX(@Separator, @Text, LEN(@Separator))
	    END
	END
	
	RETURN;
END
GO
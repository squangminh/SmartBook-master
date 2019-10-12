CREATE TABLE [dbo].[BookRate]
(
	[BookId] INT NOT NULL PRIMARY KEY, 
    [RatingCount] INT NOT NULL, 
    [RatingPoint1] INT NOT NULL, 
    [RatingPoint2] INT NOT NULL, 
    [RatingPoint3] INT NOT NULL, 
    [RatingPoint4] INT NOT NULL, 
    [RatingPoint5] INT NOT NULL, 
    [RatingPoint] NUMERIC(3, 1) NOT NULL
)

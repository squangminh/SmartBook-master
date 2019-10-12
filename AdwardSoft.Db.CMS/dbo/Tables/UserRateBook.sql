CREATE TABLE [dbo].[UserRateBook]
(
	[UserId] INT NOT NULL ,
	[BookId] INT NOT NULL ,
	[Rate] INT NOT NULL ,
	PRIMARY KEY ([UserId], [BookId]), 
)

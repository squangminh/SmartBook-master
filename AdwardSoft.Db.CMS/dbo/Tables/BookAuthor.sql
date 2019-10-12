CREATE TABLE [dbo].[BookAuthor]
(
	[AuthorId] INT NOT NULL,
	[BookId] INT NOT NULL ,
	PRIMARY KEY ([AuthorId], [BookId]), 
	CONSTRAINT [FK_AuthorOfBook_Book] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id]),
	CONSTRAINT [FK_AuthorOfBook_Author] FOREIGN KEY ([AuthorId]) REFERENCES [Author]([Id])
)

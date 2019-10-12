CREATE TABLE [dbo].[GenreOfBook]
(
	[GenreId] INT NOT NULL ,
	[BookId] INT NOT NULL ,
	PRIMARY KEY ([GenreId], [BookId]), 
	CONSTRAINT [FK_GenreOfBook_Book] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id]),
	CONSTRAINT [FK_GenreOfBook_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([Id])
)

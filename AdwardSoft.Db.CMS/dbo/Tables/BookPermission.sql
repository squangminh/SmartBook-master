CREATE TABLE [dbo].[BookPermission]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[BookId] INT NOT NULL,
	[UserId] BIGINT NOT NULL
)

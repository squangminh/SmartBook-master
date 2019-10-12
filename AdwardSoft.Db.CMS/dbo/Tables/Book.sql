CREATE TABLE [dbo].[Book]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(250) NOT NULL,
	[Image] VARCHAR(2048) NOT NULL,
	[Description] NVARCHAR(1000) NOT NULL,
	[Status] INT , --0 : Đang tiến hành, 1: Hoàn thành, 2 : Tạm dừng
	[TimeCreate] SMALLDATETIME,
	[CreateUserId] BIGINT
)

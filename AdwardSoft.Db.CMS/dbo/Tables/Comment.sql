﻿CREATE TABLE [dbo].[Comment]
(
	[Id] CHAR(32) NOT NULL PRIMARY KEY,
	[UserId] BIGINT NOT NULL,
	[Content] NVARCHAR(500) NOT NULL, 
    [ParentId ] CHAR(32) NULL
)

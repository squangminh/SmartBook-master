﻿CREATE TABLE [dbo].[UserLoginStatus]
(
	[UserId] BIGINT NOT NULL PRIMARY KEY, 
    [IPAddress] VARBINARY(20) NOT NULL, 
    [LastLogin] SMALLDATETIME NOT NULL, 
    [Note] VARCHAR(150) NULL 
)

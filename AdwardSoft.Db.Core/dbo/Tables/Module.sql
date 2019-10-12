CREATE TABLE [dbo].[Module]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) UNIQUE NOT NULL, 
    [Link] VARCHAR(256) NOT NULL, 
    [ClassName] VARCHAR(50) NOT NULL, 
	[ControllerName] VARCHAR(60) NOT NULL, -- Exp: Place, Module, Food (ContrllerName)
    [ParentId] INT NOT NULL, 
    [Sort] SMALLINT NOT NULL, 
   -- CONSTRAINT [FK_Module_Module] FOREIGN KEY ([ParentId]) REFERENCES [Module]([Id])
)

GO

CREATE INDEX [IX_Module_ParentId] ON [dbo].[Module] ([ParentId])
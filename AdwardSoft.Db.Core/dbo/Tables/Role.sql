CREATE TABLE [dbo].[Role]
(
   [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
   [Name] NVARCHAR(128) NOT NULL,
   [NormalizedName] NVARCHAR(256) NOT NULL,
   [ConcurrencyStamp] NVARCHAR(MAX) NULL,
   [IsDefault] BIT NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Role_Name] ON [dbo].[Role] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL)
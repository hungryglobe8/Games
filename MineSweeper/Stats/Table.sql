CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Wins] INT NULL DEFAULT 0, 
    [Losses] INT NULL DEFAULT 0, 
    [Percentage] DECIMAL NULL DEFAULT 0
)

CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Currency] INT NOT NULL, 
    [Balance] MONEY NOT NULL
)

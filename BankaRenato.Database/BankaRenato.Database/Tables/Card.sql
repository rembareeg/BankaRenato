CREATE TABLE [dbo].[Card]
(
	[Id] INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
    [AccountId] INT NOT NULL, 
    [CardType] INT NOT NULL
)

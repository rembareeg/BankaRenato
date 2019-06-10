CREATE TABLE [dbo].[Card]
(
	[Id] INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
    [AccountId] INT NOT NULL, 
    [CardType] INT NOT NULL, 
    CONSTRAINT [Card_To_Account] FOREIGN KEY ([AccountId]) REFERENCES [Account]([Id]),
	CONSTRAINT [Card_To_CardType] FOREIGN KEY ([CardType]) REFERENCES [CardType]([Id])
)

GO



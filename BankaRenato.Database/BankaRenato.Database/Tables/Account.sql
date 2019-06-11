CREATE TABLE [dbo].[Account]
(
	[Id] INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Currency] INT NOT NULL, 
    [Balance] MONEY NOT NULL,
	CONSTRAINT [FK_Account_To_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

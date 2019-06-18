CREATE TABLE [dbo].[Account]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  
    [Name] NVARCHAR(40) NOT NULL,
	[Balance] MONEY NOT NULL,
	[Currency] NVARCHAR(3) NOT NULL, 
	[UserId] INT NOT NULL
    
	CONSTRAINT [FK_Account_To_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)

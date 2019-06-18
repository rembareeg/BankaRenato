CREATE PROCEDURE [dbo].[SetupDatabase]
AS

	INSERT INTO [Role] (Id, Type) VALUES (1, 'Admin')
	INSERT INTO [Role] (Id, Type) VALUES (2, 'Client')
	
	DECLARE @salt UNIQUEIDENTIFIER=NEWID()
	
    INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email, RoleId)
    VALUES('admin', dbo.HashPassword('admin123', @salt), @salt, 'Admin', 'Admin', 'admin@banka.com', 1)

	INSERT INTO CardType (Type) VALUES ('VISA')
	INSERT INTO CardType (Type) VALUES ('AMERICAN')
	INSERT INTO CardType (Type) VALUES ('MAESTRO')
	INSERT INTO CardType (Type) VALUES ('MASTER')






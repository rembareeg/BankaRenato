CREATE PROCEDURE [dbo].[SetupDatabase]
AS
	DECLARE @salt UNIQUEIDENTIFIER=NEWID()

    INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email, Role)
    VALUES('admin', dbo.HashPassword('admin123', @salt), @salt, 'Admin', 'Admin', 'admin@banka.com', 'Admin')

	INSERT INTO CardType (Type) VALUES ('VISA')
	INSERT INTO CardType (Type) VALUES ('AMERICAN')
	INSERT INTO CardType (Type) VALUES ('MAESTRO')
	INSERT INTO CardType (Type) VALUES ('MASTER')






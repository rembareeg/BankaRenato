CREATE PROCEDURE [dbo].[SetupDatabase]
AS
	DECLARE @salt UNIQUEIDENTIFIER=NEWID()

    INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email, Role)
    VALUES('admin', HASHBYTES('SHA2_512', 'admin123' + CAST(@salt AS NVARCHAR(36))), @salt, 'Admin', 'Admin', 'admin@banka.com', 'Admin')

	INSERT INTO CardType (Type) VALUES ('VISA')
	INSERT INTO CardType (Type) VALUES ('AMERICAN')
	INSERT INTO CardType (Type) VALUES ('MAESTRO')
	INSERT INTO CardType (Type) VALUES ('MASTER')






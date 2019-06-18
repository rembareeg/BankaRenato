-- Description:	<procedure to add user in table user>
CREATE PROCEDURE [dbo].[RegisterUser] 
	@username NVARCHAR(50), 
    @password NVARCHAR(50),
	@email NVARCHAR(40),
    @firstName NVARCHAR(40), 
    @lastName NVARCHAR(40),
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email, RoleId)
        VALUES(@username, dbo.HashPassword(@password, @salt), @salt, @firstName, @lastName, @email, 2)

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
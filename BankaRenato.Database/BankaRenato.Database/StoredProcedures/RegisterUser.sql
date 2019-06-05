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

        INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email)
        VALUES(@username, HASHBYTES('SHA2_512', @password + CAST(@salt AS NVARCHAR(36))), @salt, @firstName, @lastName, @email)

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
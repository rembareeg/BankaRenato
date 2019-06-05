-- Description:	<procedure to Login user>
CREATE PROCEDURE [dbo].[UserLogin]
    @username NVARCHAR(40),
    @password NVARCHAR(40),
    @response int OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 Id FROM dbo.[User] WHERE Username=@username)
    BEGIN
        SET @userID=(SELECT Id FROM dbo.[User] WHERE Username=@username AND Password = HASHBYTES('SHA2_512', @password+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @response = -1  -- 'Incorrect password'
       ELSE 
	       SELECT TOP 1 Id, Username, Email, FirstName, LastName from dbo.[User] WHERE Id = @userID
           SET @response = 1   -- 'User successfully logged in'
    END
    ELSE
       SET @response = 0    --'Invalid login'

END
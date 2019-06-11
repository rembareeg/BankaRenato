-- Description:	<procedure to Login user>
CREATE PROCEDURE [dbo].[UserLogin]
    @username NVARCHAR(40),
    @password NVARCHAR(40)
    
AS
BEGIN

    SET NOCOUNT ON

    SELECT TOP 1 Id, Username, Password, Salt, Email, FirstName, LastName, Role
		FROM dbo.[User] WHERE Username=@username AND Password = HASHBYTES('SHA2_512', @password+CAST(Salt AS NVARCHAR(36)))

END
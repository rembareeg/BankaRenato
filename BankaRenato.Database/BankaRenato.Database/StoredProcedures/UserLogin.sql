-- Description:	<procedure to Login user>
CREATE PROCEDURE [dbo].[UserLogin]
    @username NVARCHAR(40),
    @password NVARCHAR(40)
    
AS
BEGIN

    SET NOCOUNT ON

    SELECT TOP 1 Id, Username, Password, Salt, Email, FirstName, LastName, RoleId
		FROM dbo.[User] WHERE Username=@username AND Password = dbo.HashPassword(@password, Salt)

END
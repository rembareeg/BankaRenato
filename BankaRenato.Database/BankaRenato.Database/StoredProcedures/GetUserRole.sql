CREATE PROCEDURE [dbo].[GetUserRole]
	@userId INT
AS
	SET NOCOUNT ON
	SELECT r.Id, r.Type FROM dbo.[Role] r 
		JOIN dbo.[User] u ON r.Id = u.RoleId 
			WHERE u.Id = @userId


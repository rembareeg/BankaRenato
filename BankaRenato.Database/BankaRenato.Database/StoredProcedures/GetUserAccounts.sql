CREATE PROCEDURE [dbo].[GetUserAccounts]
	@userId INT
	
AS
BEGIN
	SET NOCOUNT ON
	SELECT Id, UserId, Name, Currency, Balance FROM Account WHERE UserId = @userId

END
CREATE PROCEDURE [dbo].[GetUserAccounts]
	@usedId INT
	
AS
BEGIN
	SET NOCOUNT ON
	SELECT Id, UserId, Currency, Balance FROM Account WHERE UserId = @usedId

END
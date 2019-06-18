CREATE PROCEDURE [dbo].[GetAccountCards]
	@accountId INT
	
AS
BEGIN
	SET NOCOUNT ON
	SELECT c.Id AS Id, c.CardType AS CardType, ct.Type AS TYPE FROM dbo.[Card] c 
		JOIN dbo.[CardType] ct ON ct.Id = c.CardType WHERE c.AccountId = @accountId

END

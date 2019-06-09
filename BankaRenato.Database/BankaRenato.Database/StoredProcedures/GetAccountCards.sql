CREATE PROCEDURE [dbo].[GetAccountCards]
	@accountId INT
	
AS
BEGIN
	SET NOCOUNT ON
	SELECT cards.Id AS Id, cardTypes.Type as Type FROM dbo.Card cards 
		JOIN dbo.CardType cardTypes ON cards.CardType = cardTypes.id
			WHERE cards.AccountId = @accountId

END

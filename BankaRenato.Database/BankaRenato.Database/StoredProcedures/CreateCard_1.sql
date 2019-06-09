CREATE PROCEDURE [dbo].[CreateCard]
	@accountId INT,
	@cardId INT,
	@response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		INSERT INTO dbo.[Card] (AccountId, CardType) VALUES(@accountId, @cardId)
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
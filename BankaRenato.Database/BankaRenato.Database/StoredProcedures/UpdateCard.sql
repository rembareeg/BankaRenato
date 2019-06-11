CREATE PROCEDURE [dbo].[UpdateCard]
    @cardId INT,
	@cardType INT,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

		UPDATE dbo.[Card] SET CardType = @cardType WHERE Id = @cardId
		
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
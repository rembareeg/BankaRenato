CREATE PROCEDURE [dbo].[DeleteCard]
    @cardId INT,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

		DELETE dbo.[Card] WHERE Id = @cardId
		
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
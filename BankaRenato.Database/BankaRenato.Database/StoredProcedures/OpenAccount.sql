-- Description:	<procetude to open account for user>
CREATE PROCEDURE [dbo].[OpenAccount] 
	@usedId INT,
	@response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		INSERT INTO dbo.[Account] (UserId, Currency, Balance) VALUES(@usedId, 191, 0)
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
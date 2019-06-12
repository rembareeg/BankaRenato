-- Description:	<procetude to open account for user>
CREATE PROCEDURE [dbo].[OpenAccount] 
	@userId INT,
	@name VARCHAR(40),
	@response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		INSERT INTO dbo.[Account] (UserId, Name, Currency, Balance) VALUES(@userId, @name, 'HRK', 0)
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
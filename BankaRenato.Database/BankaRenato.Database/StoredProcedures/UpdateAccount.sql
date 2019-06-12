CREATE PROCEDURE [dbo].[UpdateAccount]
    @accountId INT,
	@name NVARCHAR(40),
	@balance MONEY,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

		UPDATE dbo.[Account] SET Name = @name, Balance = @balance WHERE Id = @accountId
		
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
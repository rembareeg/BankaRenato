CREATE PROCEDURE [dbo].[DeleteAccount]
    @accountId INT,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		
		DELETE C FROM dbo.[Account] A 
			INNER JOIN dbo.[Card] C ON C.AccountId = A.Id
					WHERE A.Id = @accountId
		
		DELETE dbo.[Account] WHERE Id = @accountId
		
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
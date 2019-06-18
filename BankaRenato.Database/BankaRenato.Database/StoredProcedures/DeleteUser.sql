CREATE PROCEDURE [dbo].[DeleteUser]
    @userId INT,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
		
		DELETE C FROM dbo.[User] U 
			INNER JOIN dbo.[Account] A ON A.UserId = U.Id
				INNER JOIN dbo.[Card] C ON C.AccountId= A.Id
					WHERE U.Id = @userId
		DELETE A FROM dbo.[User] U 
			INNER JOIN dbo.[Account] A ON A.UserId = U.Id
					WHERE U.Id = @userId
		
		DELETE dbo.[User] WHERE Id = @userId
		
		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
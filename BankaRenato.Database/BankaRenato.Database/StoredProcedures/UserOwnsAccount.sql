CREATE PROCEDURE [dbo].[UserOwnsAccount]
	@userId INT,
	@accountId INT,
	@response BIT OUT
AS
	IF EXISTS (SELECT * FROM [Account] WHERE Id = @accountId AND UserId = @userId)
BEGIN
	SET @response = 1
END
ELSE
BEGIN
	SET @response = 0
END


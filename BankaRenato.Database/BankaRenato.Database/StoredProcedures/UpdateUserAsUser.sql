CREATE PROCEDURE [dbo].[UpdateUserAsUser]
	@userId INT,
    @password NVARCHAR(50),
	@email NVARCHAR(40),
    @response BIT OUTPUT
AS
BEGIN
    
    BEGIN TRY
		IF (@password IS NOT NULL) OR (LEN(@password) > 0)
		BEGIN
			DECLARE @salt UNIQUEIDENTIFIER=NEWID()
			
			UPDATE dbo.[User] SET 
				Password =  dbo.HashPassword(@password, @salt),
				Salt = @salt,				
				Email = @email
			WHERE Id = @userId

		END
		ELSE
		BEGIN
			UPDATE dbo.[User] SET 
				Email = @email
			WHERE Id = @userId
		END

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END

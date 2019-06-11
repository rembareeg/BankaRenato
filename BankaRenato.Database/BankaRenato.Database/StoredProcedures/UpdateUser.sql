CREATE PROCEDURE [dbo].[UpdateUser]
	@userId INT,
	@username NVARCHAR(50), 
    @password NVARCHAR(50),
	@email NVARCHAR(40),
    @firstName NVARCHAR(40), 
    @lastName NVARCHAR(40),
    @response BIT OUTPUT
AS
BEGIN
    
    BEGIN TRY
		IF (@password IS NOT NULL) OR (LEN(@password) > 0)
		BEGIN
			DECLARE @salt UNIQUEIDENTIFIER=NEWID()
			
			UPDATE dbo.[User] SET 
				Username = @username,
				Password =  HASHBYTES('SHA2_512', @password + CAST(@salt AS NVARCHAR(36))),
				Salt = @salt,				
				FirstName = @firstName,
				LastName = @lastName,
				Email = @email
			WHERE Id = @userId

		END
		ELSE
		BEGIN
			UPDATE dbo.[User] SET 
				Username = @username,	
				FirstName = @firstName,
				LastName = @lastName,
				Email = @email
			WHERE Id = @userId
		END

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END

-- Description:	<procedure to see if user exists with same email in table User>
CREATE PROCEDURE [dbo].[EmailExists]
	@email NVARCHAR(40),
	@response  bit OUTPUT
AS
BEGIN
	
	IF EXISTS (SELECT TOP 1 Username FROM dbo.[User] WHERE Email=@email)
		BEGIN
			SET @response = 1
		END
	ELSE
		BEGIN
			SET @response = 0
		END

END

USE [BankaRenato]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/5/2019 10:25:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Username] [nvarchar](40) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
	[Email] [nvarchar](40) NULL,
	[FirstName] [nvarchar](40) NULL,
	[LastName] [nvarchar](40) NULL,
 CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[EmailExists]    Script Date: 6/5/2019 10:25:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Renato Marinic>
-- Create date: <05.06.2019>
-- Description:	<procedure to see if user exists with same email in table User>
-- =============================================
Create PROCEDURE [dbo].[EmailExists]
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
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 6/5/2019 10:25:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Renato Marinic>
-- Create date: <04.06.2019>
-- Description:	<procedure to add user in table user>
-- =============================================
CREATE PROCEDURE [dbo].[RegisterUser] 
	@username NVARCHAR(50), 
    @password NVARCHAR(50),
	@email NVARCHAR(40),
    @firstName NVARCHAR(40) = NULL, 
    @lastName NVARCHAR(40) = NULL,
    @response BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[User] (Username, Password, Salt, FirstName, LastName, Email)
        VALUES(@username, HASHBYTES('SHA2_512', @password+CAST(@salt AS NVARCHAR(36))), @salt, @firstName, @lastName, @email)

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UserExists]    Script Date: 6/5/2019 10:25:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Renato Marinic>
-- Create date: <04.06.2019>
-- Description:	<procedure to see if user exists with same username in table User>
-- =============================================
CREATE PROCEDURE [dbo].[UserExists]
	@username NVARCHAR(40),
	@response  bit OUTPUT
AS
BEGIN
	
	IF EXISTS (SELECT TOP 1 Username FROM dbo.[User] WHERE Username=@username)
		BEGIN
			SET @response = 1
		END
	ELSE
		BEGIN
			SET @response = 0
		END

END
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 6/5/2019 10:25:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Renato Marinic>
-- Create date: <04.06.2019>
-- Description:	<procedure to Login user>
-- =============================================
CREATE PROCEDURE [dbo].[UserLogin]
    @username NVARCHAR(40),
    @password NVARCHAR(40),
    @response int OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 Id FROM dbo.[User] WHERE Username=@username)
    BEGIN
        SET @userID=(SELECT Id FROM dbo.[User] WHERE Username=@username AND Password = HASHBYTES('SHA2_512', @password+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @response = -1  -- 'Incorrect password'
       ELSE 
	       SELECT TOP 1 Id, Username, Email, FirstName, LastName from dbo.[User] WHERE Id = @userID
           SET @response = 1   -- 'User successfully logged in'
    END
    ELSE
       SET @response = 0    --'Invalid login'

END
GO

CREATE PROCEDURE [dbo].[spAccount_Login]
	@EmailAddress VARCHAR(50),
	@Password VARCHAR(50),
	@UserId INT OUTPUT,
	@ErrorCode INT OUTPUT
AS
BEGIN
	DECLARE @HashedPassword VARBINARY(256);
	SET @HashedPassword = HASHBYTES('SHA2_256', @Password); -- Hash the input password

	SELECT @UserId = Id FROM Account WHERE EmailAddress = @EmailAddress AND Password = @HashedPassword;
	
	IF @UserId IS NULL
	BEGIN
		SET @ErrorCode = 52000; -- Invalid login credentials error
	END
	ELSE
	BEGIN
		SET @ErrorCode = 0; -- Success
	END
END

CREATE PROCEDURE [dbo].[spAccount_CreateAccount_WithValidation]
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@EmailAddress VARCHAR(50),
	@PhoneNumber VARCHAR(50),
	@GovernmentIdNumber VARCHAR(50),
	@Password VARCHAR(50),
	@ErrorCode INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT * FROM Account WHERE EmailAddress = @EmailAddress
	OR PhoneNumber = @PhoneNumber
	OR GovernmentIdNumber = @GovernmentIdNumber)
	BEGIN
		SET @ErrorCode = 51000; -- Duplicate record error
		RETURN;
	END
	ELSE 
	BEGIN
		DECLARE @HashedPassword VARBINARY(256);
		SET @HashedPassword = HASHBYTES('SHA2_256', @Password); -- Hash the password using SHA-256 algorithm
		
		INSERT INTO Account (FirstName, LastName, EmailAddress, PhoneNumber, GovernmentIdNumber, Password)
		VALUES (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @GovernmentIdNumber, @HashedPassword);
		SET @ErrorCode = 0; -- Success
	END
	SELECT CAST(SCOPE_IDENTITY() as int);
END

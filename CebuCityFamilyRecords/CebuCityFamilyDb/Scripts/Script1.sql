DECLARE @UserId INT;
DECLARE @ErrorCode INT;
EXEC [dbo].[spAccount_Login] 'string', 'string', @UserId OUTPUT, @ErrorCode OUTPUT;

IF @ErrorCode = 0
BEGIN
	-- Login successful, do nothing
	SELECT * FROM Account WHERE Id = @UserId;
END
ELSE
BEGIN
	-- Login failed, throw an error
	RAISERROR('Invalid login credentials', 16, 1);
END

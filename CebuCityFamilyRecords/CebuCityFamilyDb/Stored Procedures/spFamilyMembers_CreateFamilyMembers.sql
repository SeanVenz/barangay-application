CREATE PROCEDURE [dbo].[spFamilyMembers_CreateFamilyMembers]
	@Name NVARCHAR(50), 
    @FId INT
AS
BEGIN
    IF EXISTS (SELECT * FROM FamilyMembers WHERE Name = @Name AND FId = @FId)
		BEGIN
			SELECT CAST(0 as int);
		END
	ELSE 
		BEGIN
			INSERT INTO FamilyMembers (Name, FId) VALUES (@Name, @FId);
		END
	SELECT CAST(SCOPE_IDENTITY() as int);
END

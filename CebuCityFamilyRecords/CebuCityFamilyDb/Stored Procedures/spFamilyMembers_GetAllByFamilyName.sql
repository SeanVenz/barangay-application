CREATE PROCEDURE [dbo].[spFamilyMembers_GetAllByFamilyName]
	@name nvarchar(50)
AS
BEGIN 
	DECLARE @famId INT;

	SET @famId = (
		SELECT Id FROM Family WHERE Name = @name
	);

	SELECT * FROM FamilyMembers as fm INNER JOIN Details as d ON fm.id = d.FmId WHERE fm.FId = @famId;
END
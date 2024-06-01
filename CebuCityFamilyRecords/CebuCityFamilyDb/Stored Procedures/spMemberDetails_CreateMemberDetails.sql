CREATE PROCEDURE [dbo].[spMemberDetails_CreateMemberDetails]
	@LastName NVARCHAR(50),
	@FirstName NVARCHAR(50),
	@Age INT,
	@MaritalStatus NVARCHAR(50),
	@BirthDate NVARCHAR(50),
	@Gender NVARCHAR(50),
	@Occupation NVARCHAR(50),
	@ContactNo NVARCHAR(50),
	@Religion NVARCHAR(50),
	@FId INT
AS
BEGIN
	INSERT INTO FamilyMemberWithDetails (LastName, FirstName, Age, MaritalStatus, BirthDate, Gender, Occupation, ContactNo, Religion, FId)
	VALUES (@LastName, @FirstName, @Age, @MaritalStatus, @BirthDate, @Gender, @Occupation, @ContactNo, @Religion, @FId);
	SELECT CAST(SCOPE_IDENTITY() as int);
END

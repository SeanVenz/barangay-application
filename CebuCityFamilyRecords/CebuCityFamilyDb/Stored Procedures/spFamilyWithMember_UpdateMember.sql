CREATE PROCEDURE [dbo].[spFamilyWithMember_UpdateMember]
    @Id INT,
    @LastName NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @Age INT,
    @MaritalStatus NVARCHAR(50),
    @BirthDate NVARCHAR(50),
    @Gender NVARCHAR(50),
    @Occupation NVARCHAR(50),
    @ContactNo NVARCHAR(50),
    @Religion NVARCHAR(50)
AS
BEGIN
    UPDATE FamilyMemberWithDetails
    SET 
        LastName = @LastName,
        FirstName = @FirstName,
        Age = @Age,
        MaritalStatus = @MaritalStatus,
        BirthDate = @BirthDate,
        Gender = @Gender,
        Occupation = @Occupation,
        ContactNo = @ContactNo,
        Religion = @Religion
    WHERE Id = @Id
END
GO

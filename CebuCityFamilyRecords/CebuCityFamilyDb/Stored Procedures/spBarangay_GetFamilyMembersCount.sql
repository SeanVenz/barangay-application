CREATE PROCEDURE [dbo].[spBarangay_GetFamilyMembersCount]
    @barangayId INT
AS
BEGIN
    SELECT COUNT(fm.Id) AS FamilyMembersCount
    FROM FamilyMemberWithDetails fm
    WHERE fm.FId IN (SELECT f.ID FROM Family f WHERE f.BId = @barangayId)
END

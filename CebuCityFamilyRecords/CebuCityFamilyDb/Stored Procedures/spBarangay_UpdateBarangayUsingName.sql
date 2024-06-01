CREATE PROCEDURE [dbo].[spBarangay_UpdateBarangayUsingName]
    @oldName NVARCHAR(50),
    @newName NVARCHAR(50),
    @newCaptain NVARCHAR(50)
AS
BEGIN
    UPDATE Barangay 
    SET Name = @newName, Captain = @newCaptain 
    WHERE Name = @oldName;

    SELECT * FROM Barangay WHERE Name = @newName;
END

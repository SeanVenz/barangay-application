CREATE PROCEDURE [dbo].[spBarangay_DeleteBarangay]
	@name VARCHAR(50)
AS
BEGIN
	IF EXISTS(SELECT * FROM Barangay WHERE Name = @name)
	BEGIN
		DELETE FROM Barangay WHERE Name = @name;
	END
	ELSE
	BEGIN
		SELECT CAST(0 AS INT);
	END
END

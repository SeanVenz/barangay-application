CREATE PROCEDURE [dbo].[spBarangay_CreateBarangay]
	@Name VARCHAR(50),
	@Captain VARCHAR(50)
AS
BEGIN
	IF EXISTS (SELECT * FROM Barangay WHERE Name = @Name AND Captain = @Captain)
	BEGIN
		SELECT CAST(0 as int);
	END
	ELSE 
	BEGIN
		INSERT INTO Barangay (Name, Captain) VALUES (@Name, @Captain);
	END
	SELECT CAST(SCOPE_IDENTITY() as int);
END

CREATE PROCEDURE [dbo].[spBarangay_UpdateBarangay]
	@Name VARCHAR(50),
	@Captain VARCHAR(50),
	@Id INT
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Barangay WHERE Id = @Id)
	BEGIN
		SELECT CAST(0 AS INT);
	END
	ELSE
	BEGIN
		IF EXISTS (SELECT * FROM Barangay WHERE Name = @Name AND Captain = @Captain)
		BEGIN
			SELECT CAST(0 AS INT);
		END
		ELSE
		BEGIN
			UPDATE Barangay SET Name = @Name, Captain = @Captain WHERE Id = @Id;
			SELECT * FROM Barangay WHERE Id = @Id;
		END
	END
END

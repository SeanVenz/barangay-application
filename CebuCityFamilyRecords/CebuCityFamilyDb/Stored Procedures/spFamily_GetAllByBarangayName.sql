CREATE PROCEDURE [dbo].[spFamily_GetAllByBarangayName]
	@name nvarchar(50)
AS
BEGIN 
	DECLARE @brgyId INT;

	SET @brgyId = (
		SELECT Id FROM Barangay WHERE Name = @name
	);

	SELECT * FROM Family AS F WHERE F.BId = @brgyId;
END
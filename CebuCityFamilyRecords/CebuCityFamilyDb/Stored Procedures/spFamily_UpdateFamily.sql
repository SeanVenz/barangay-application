CREATE PROCEDURE [dbo].[spFamily_UpdateFamily]
	@Id INT,
	@Sitio VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Family WHERE Id = @Id)
	BEGIN
		SELECT CAST(0 AS INT);
	END
	ELSE
	BEGIN
		UPDATE Family SET Sitio = @Sitio WHERE Id = @Id;
		SELECT * FROM Family WHERE Id = @Id;
	END
END

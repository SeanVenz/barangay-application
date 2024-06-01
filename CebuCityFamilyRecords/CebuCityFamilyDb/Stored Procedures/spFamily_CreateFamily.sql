CREATE PROCEDURE [dbo].[spFamily_CreateFamily]
	@Name NVARCHAR(50), 
    @Sitio NVARCHAR(50),
    @BId INT
AS
BEGIN
	INSERT INTO Family (Name, Sitio, BId) VALUES (@Name, @Sitio, @BId);
	SELECT CAST(SCOPE_IDENTITY() as int);
END


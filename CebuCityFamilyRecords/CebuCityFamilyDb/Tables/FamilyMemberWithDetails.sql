CREATE TABLE [dbo].[FamilyMemberWithDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [Age] INT NOT NULL, 
    [MaritalStatus] NVARCHAR(50) NOT NULL, 
    [BirthDate] NVARCHAR(50) NOT NULL, 
    [Gender] NVARCHAR(50) NOT NULL, 
    [Occupation] NVARCHAR(50) NOT NULL, 
    [ContactNo] NVARCHAR(50) NOT NULL, 
    [Religion] NVARCHAR(50) NOT NULL, 
    [FId] INT NOT NULL
	CONSTRAINT [FK_FamilyMemberWithDetails] FOREIGN KEY ([FId]) REFERENCES [Family]([Id]) ON DELETE CASCADE
)

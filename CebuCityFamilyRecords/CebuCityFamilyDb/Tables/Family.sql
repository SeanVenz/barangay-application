﻿CREATE TABLE [dbo].[Family]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Sitio] NVARCHAR(50) NOT NULL,
    [BId] INT NOT NULL,
    CONSTRAINT [FK_Family] FOREIGN KEY ([BId]) REFERENCES [Barangay]([Id]) ON DELETE CASCADE
)

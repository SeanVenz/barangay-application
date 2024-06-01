USE [CebuCityFamilyDb]
GO

SET IDENTITY_INSERT [Barangay] ON
INSERT [Barangay] ([Id], [Name], [Captain]) 
VALUES 
    (1, 'Tisa', 'Arlee Cathesyed'),
    (2, 'Lahug', 'Brander Andrich'),
    (3, 'Basak', 'Tam Duck'),
    (4, 'Talamban', 'Clayton Porte');
SET IDENTITY_INSERT [Barangay] OFF

INSERT INTO [Family] (Name,Sitio,BId)
VALUES
  ('Uppett','Sidlakan',1),
  ('Pedro','Motra',1),
  ('Joerning','Panas',2),
  ('Berrick','Plaza',2),
  ('Kezar','Ulap',3),
  ('McGow','Guinabsan',3),
  ('Jakeway','Tago',4),
  ('Matashkin','Upper Tayong',4);

INSERT INTO [FamilyMemberWithDetails] (LastName, FirstName, Age,MaritalStatus,BirthDate,Gender,Occupation,ContactNo,Religion,FId)
VALUES
  ('Uppett','Andonis',27,'Married','01/01/1995','Male','Scaffolder','09123456789','Roman Catholic',1),
  ('Uppett','Andeee',21,'Married','02/01/2001','Female','Lecturer','09123456789','Roman Catholic',1),
  ('Uppett','Velma',10,'Single','03/01/2012','Female','Student','09123456789','Roman Catholic',1),
  ('Pedro','Penrod',52,'Married','04/01/1970','Male','Debt collector','09123456789','Roman Catholic',2),
  ('Pedro','Floris',51,'Married','05/01/1971','Female','Caretaker','09123456789','Roman Catholic',2),
  ('Pedro','Eduardo',25,'Single','06/01/1995','Male','Managing director','09123456789','Roman Catholic',2),
  ('Joerning','Maxine',44,'Married','07/01/1978','Female','Computer analyst','09123456789','Roman Catholic',3),
  ('Joerning','Raine',45,'Married','08/01/1977','Male','Postal delivery worker','09123456789','Roman Catholic',3),
  ('Joerning','Dorothea',22,'Single','09/01/2000','Female','Chemist','09123456789','Roman Catholic',3),
  ('Berrick','Clemmy',44,'Married','07/01/1978','Male','Doorman','09123456789','Roman Catholic',4),
  ('Berrick','Brennen',45,'Married','08/01/1977','Female','Waiter','09123456789','Roman Catholic',4),
  ('Berrick','Culley',22,'Single','09/01/2000','Male','Shoemaker','09123456789','Roman Catholic',4),
  ('Kezar','Ardys',40,'Married','10/01/1982','Male','Crane driver','09123456789','Roman Catholic',5),
  ('Kezar','Marlena',34,'Married','01/02/1988','Female','Occupational therapist','09123456789','Roman Catholic',5),
  ('Kezar','Chicky',17,'Single','02/02/2005','Female','Student','09123456789','Roman Catholic',5),
  ('McGow','Ambrosi',27,'Married','01/01/1995','Male','Photographer','09123456789','Roman Catholic',6),
  ('McGow','Glenda',21,'Married','02/01/2001','Female','Flying instructor','09123456789','Roman Catholic',6),
  ('McGow','Shelby',10,'Single','03/01/2012','Female','Student','09123456789','Roman Catholic',6),
  ('Jakeway','Aura',52,'Married','04/01/1970','Male','Lift engineer','09123456789','Roman Catholic',7),
  ('Jakeway','Livvie',51,'Married','05/01/1971','Female','Photographer','09123456789','Roman Catholic',7),
  ('Jakeway','Odele',25,'Single','06/01/1995','Female','Hairdresser','09123456789','Roman Catholic',7),
  ('Matashkin','Rubetta',22,'Single','09/01/2000','Female','IT consultant','09123456789','Roman Catholic',8),
  ('Matashkin','Arlen',44,'Married','07/01/1978','Female','Auctioneer','09123456789','Roman Catholic',8),
  ('Matashkin','Brewer',45,'Married','08/01/1977','Male','Roofer','09123456789','Roman Catholic',8);
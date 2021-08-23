USE master
GO

CREATE DATABASE EnsekMeterReadings
GO

USE EnsekMeterReadings
GO

CREATE TABLE Account
(
	Id INT NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL
)
GO

ALTER TABLE Account ADD CONSTRAINT PK_Account PRIMARY KEY (Id)
GO

CREATE TABLE MeterReading
(
	Id INT NOT NULL IDENTITY (1, 1),
	AccountId INT NOT NULL,
	MeterReadingDateTime DATETIME NOT NULL,
	[Value] INT NOT NULL
)

ALTER TABLE MeterReading ADD CONSTRAINT FK_MeterReading_Account FOREIGN KEY (AccountId) REFERENCES Account(Id)
GO


/* Seed data */
INSERT INTO Account (Id, FirstName, LastName)
VALUES (2344, 'Tommy', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2233, 'Barry', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (8766, 'Sally', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2345, 'Jerry', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2346, 'Ollie', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2347, 'Tara', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2348, 'Tammy', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2349, 'Simon', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2350, 'Colin', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2351, 'Gladys', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2352, 'Greg', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2353, 'Tony', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2355, 'Arthur', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (2356, 'Craig', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (6776, 'Laura', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (4534, 'JOSH', 'TEST')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1234, 'Freya', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1239, 'Noddy', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1240, 'Archie', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1241, 'Lara', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1242, 'Tim', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1243, 'Graham', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1244, 'Tony', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1245, 'Neville', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1246, 'Jo', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1247, 'Jim', 'Test')
GO

INSERT INTO Account (Id, FirstName, LastName)
VALUES (1248, 'Pam', 'Test')
GO
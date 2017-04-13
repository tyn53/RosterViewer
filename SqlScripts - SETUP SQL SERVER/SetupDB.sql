USE [master]
GO

-- Set Sql server to use sql server logins or change the connection string in the application web config to use windows authentication

CREATE DATABASE [RosterViewer]
GO


--Create Application Login
USE [master]
GO
CREATE LOGIN [RosterApp] WITH PASSWORD=N'roster', DEFAULT_DATABASE=[RosterViewer], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [RosterViewer]
GO
CREATE USER [RosterApp] FOR LOGIN [RosterApp]
GO

USE [RosterViewer]
GO
EXEC sp_addrolemember N'db_datareader', N'RosterApp'
GO
USE [RosterViewer]
GO
EXEC sp_addrolemember N'db_datawriter', N'RosterApp'
GO


--	Create tables for RosterViewer
USE [RosterViewer]
GO

CREATE TABLE Image (imageId INT NOT NULL IDENTITY(1,1),
				    imageFileName NVARCHAR(128),
					imageContent VARBINARY(MAX) NOT NULL,
					imageContentType NVARCHAR(100) NOT NULL,
					CONSTRAINT PK_Image PRIMARY KEY CLUSTERED (imageId));

CREATE TABLE Team (teamId INT NOT NULL IDENTITY(1,1),
				   teamName NVARCHAR(32) NOT NULL,
				   imageId INT,
				   CONSTRAINT PK_Team PRIMARY KEY CLUSTERED (teamId),
				   CONSTRAINT IX_Team UNIQUE (teamName),
				   CONSTRAINT FK_Team_Images FOREIGN KEY (imageId) REFERENCES Image(imageId));

CREATE TABLE EntityType (entityTypeId INT NOT NULL IDENTITY(1,1),
					     entityType NVARCHAR(10),
					     CONSTRAINT PK_Entity PRIMARY KEY CLUSTERED (entityTypeId),
						 CONSTRAINT IX_Entity UNIQUE (entityType));

INSERT INTO EntityType VALUES ('Team'), ('Player');

CREATE TABLE Player (playerId INT NOT NULL IDENTITY(1,1),
					 playerScreenName NVARCHAR(32) NOT NULL,
					 playerFirstName NVARCHAR(32) NOT NULL,
					 playerLastName NVARCHAR(32) NOT NULL,
					 imageId INT,
					 teamId INT,
					 CONSTRAINT PK_Player PRIMARY KEY CLUSTERED (playerId),
					 CONSTRAINT IX_Player UNIQUE (playerScreenName),
					 CONSTRAINT FK_Player_Images FOREIGN KEY (imageId) REFERENCES Image(imageId),
					 CONSTRAINT FK_Player_Team FOREIGN KEY (teamId) REFERENCES Team(teamId));

CREATE TABLE Stat (statId INT NOT NULL IDENTITY(1,1),
				   entityTypeId INT NOT NULL,
				   entityId INT NOT NULL,
				   statName NVARCHAR(50) NOT NULL,
				   statValue NVARCHAR(100) NOT NULL,
				   CONSTRAINT PK_Stat PRIMARY KEY (statId),
				   CONSTRAINT FK_Stat_EntityType FOREIGN KEY (entityTypeId) REFERENCES EntityType(entityTypeId));
					 
INSERT INTO Team VALUES
('Vegetable Esports',NULL),
('Virtus.Pro',NULL),
('Alliance',NULL),
('Fnatic',NULL),
('LGD Gaming',NULL),
('Newbee',NULL),
('Void Boys',NULL),
('Cloud9',NULL),
('Digital Chaos',NULL),
('Natus Vincere',NULL),
('MVP Phoenix',NULL);
				   
INSERT INTO Player VALUES				   
('Resolut1on','Roman','Fominok',NULL,9),
('w33','Aliwi','Omar',NULL,9),
('MoonMeander','David','Tan',NULL,9),
('Saksa','Martin','Sazdov',NULL,9),
('MiSeRy','Rasmus','Filipsen',NULL,9),
('Loda','Jonathan','Berg',NULL,3),
('Limmp','Linus','Blomdin',NULL,3),
('jonassomfan','Jonas','Lindholm',NULL,3),
('EGM','Jerry','Lundkvist',NULL,3),
('Handsken','Simon','Haag',NULL,3),
('Pajkatt','Per Anders Olsson','Lille',NULL,10),
('Dendi','Denil','Ishutin',NULL,10),
('GeneRaL','Victor','Nigrini',NULL,10),
('rmN-','Roman','Paley',NULL,10),
('Biver','Malthe','Winther',NULL,10),
('Meracle','Galvin Kang','Jian Wen',NULL,4),
('QO','Kim','Seon-yeop',NULL,4),
('Ohaiyo','Chong','Xin Khoo',NULL,4),
('DJ','Djardel Jicko','Mampusti',NULL,4),
('Febby','Kim','Yong-min',NULL,4);

INSERT INTO Stat VALUES
(1,3,'Created','04-12-2013'),	
(1,3,'Captain','Jonathan Berg'),	
(1,8,'Created','February 2014'),	
(1,8,'Country','United States'),	
(1,4,'Country','Malaysia'),	
(1,4,'Created','03-30-2012'),	
(2,6,'Country','Sweden'),	
(2,6,'Date of Birth','03-19-1988'),	
(2,6,'Role','Carry, Utility'),	
(2,6,'Signature Heroes','Lifestealer, Gyrocopter, Phantom Assassin');
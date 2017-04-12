/*
	Create tables for RosterViewer
	Author: Jason Bush
*/

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
				   statName NVARCHAR(16) NOT NULL,
				   statValue NVARCHAR(17) NOT NULL,
				   CONSTRAINT PK_Stat PRIMARY KEY (statId),
				   CONSTRAINT FK_Stat_EntityType FOREIGN KEY (entityTypeId) REFERENCES EntityType(entityTypeId));



       

					 

				   
				   


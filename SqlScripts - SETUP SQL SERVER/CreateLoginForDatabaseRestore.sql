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



There are two options for setting up the database.

Option 1 (Has images):
Restore RosterViewer.bak
Run CreateLoginForDatabaseRestore.sql

Option 2 (No images):
Run SetupDB.sql 
This is will create the server, the app user, tables, and add data without images.

You will need to change the web.config to use the connection info for your instance of sqlserver.

There are two options for setting up the database.

Option 1 (Has images):
Restore RosterViewer.bak
If using sql server authentication. Run CreateLoginForDatabaseRestore.sql 

Option 2 (No images):
Run SetupDB.sql 

This is will create the server, the sql server login, tables, and add some sample data without images. You can
add images in the app afterward.


In either case you will need to change the web.config to use the connection info for your instance of sql server.
I have both a sql server and windows auth connection string. The sql server auth is commented out.

If you have any questions please call me at (931)-349-9383.
--Jason Bush

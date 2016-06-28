sqlcmd -S "localhost" -i "Database/000_Drop.sql"
sqlcmd -S "localhost" -i "Database/001_CreateDb.sql"
sqlcmd -S "localhost" -i "Database/002_MessagesTable.sql"
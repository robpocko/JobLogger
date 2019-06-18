To add new migration, open up Package Manager Console, and execute something like:

add-migration DEV1.0

To apply the migrations, run (in Package Manager Console)

update-database



BACKUP DATABASE [RLP_JobLog] 
TO  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\RLP_JobLog_20190618.bak' 
WITH NOFORMAT, 
INIT,  
NAME = N'RLP_JobLog-Full Database Backup', 
SKIP, 
NOREWIND, 
NOUNLOAD,  
STATS = 10
GO
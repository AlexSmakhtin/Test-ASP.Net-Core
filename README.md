# Приложение ASP.Net Core 8
После скачивания репозитория перед запуском сервера нужно открыть терминал и вписать следующее:
1) sqllocaldb create Teledok
2) sqllocaldb start Teledok
3) sqlcmd -S "(localdb)\Teledok" -E -Q "create database Teledoc;"
4) sqlcmd -S "(localdb)\Teledok" -E
   USE master;
   GO
   CREATE LOGIN admin WITH PASSWORD = '12345';
   GO
   USE Teledoc;
   GO
   CREATE USER admin FOR LOGIN admin;
   GO
   ALTER ROLE db_owner ADD MEMBER admin;
   GO
5) cd .\Api\
6) dotnet ef database update (или dotnet-ef database update)

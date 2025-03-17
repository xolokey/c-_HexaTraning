

--To create database write DDL query
--syntax :create database Userdefined_database_Name
--create database TestDB

select * from master.sys.databases
select * from master.sys.master_files
-- call store procedure to get list of databases
exec sp_databases

select name as DATABASE_NAME from master.sys.master_files
--delete database from phy path
drop database AlterDB
--database name change
alter database TestDB1 modify name=AlterDB
--Test data base is exist at location before deletion
drop database if exists AlterDB

create database TestDB1
--proceddure to create a table
create table AlterDB.dbo.Student(
id INT IDENTIFY PRIMARY KEY,
Name LOKEY(21)NOT NULL


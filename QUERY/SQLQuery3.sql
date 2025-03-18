use AlterDB

insert into Employee (Id,name,mobilenumber,email,license,passport)
values (104,'jay',73857708,'lokeygmail.com','lic504','pas098')

--insert into Employee (Id,name,mobilenumber,email,license,passport)

select Id,name,mobilenumber,email,license,passport from Employee

--filter record

select* from Employee where Id=102

--all records

select * from Employee

--formating result set

select e.Id as Empolyee_Id , 
e.name as Employee_Name, 
e.mobilenumber as Employee_Mob,
e.email as Employee_Mail , 
e.license as Eployee_License,
e.passport as Employee_Pasport
from Employee e

--delete a row

delete from Employee
where Id=104;

-- updation of record

update Employee set name='vinay',mobilenumber=                 ,license=   ,passport=
where id=
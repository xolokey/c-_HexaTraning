use AlterDB
create table trainee
(
traineeid int identity(100,1) not null,--if query contains only one primary key add primay key near (100,1)
trainee_name varchar(50),
skillid int not null,
trainee_skill varchar(100),
constraint PK_TSID primary key(traineeid,skillid)--to add two primarykey
)


--insert of record

insert into trainee values ('lokey','sql,c#')

--selection of column
select * from trainee

--deletion of tarinee data
delete from trainee where id=102

--droping of table

drop table tarinee

--creating new table
use AlterDB
create table tarinee_skill
(
traineeid int identity(100,1) not null,
tainee_name varchar(50),
skillid int not null,
trainee_skill varchar(100) not null,
constraint LK_ID primary key(traineeid,skillid,trainer_skill)
)
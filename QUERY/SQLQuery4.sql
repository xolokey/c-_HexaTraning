--DATE 18/3/25


use AlterDB
--parent table
create table traine
(
id int identity(100,1) primary key not null,
name varchar(50)
);

--child table
create table skill
(
id int identity(200,1) primary key not null,
name varchar(100),
skill int not null,
constraint FK_skill foreign key (skill) 
references traine(id)
--refered to parent table
);
-- foreing key column must math with primary key
--foreign key should not be null

--insert in values in parent skill table


select * from traine;

insert into traine (name) values
('john'),
('iman'),
('kishore'),
('kiruba'),
('hari'),
('rv'),
('govind');

--foreign key colum shold match with the primary key in parent table

insert into skill (name, skill) values
('c#', 101),('c++', 102),('java', 103),('python', 104),('ai', 105),('data science', 106);

--selecting child table


select *from skill;

-- cannot delete the data from parent table when linked to child table
--but can update the foreign key

--updation of 


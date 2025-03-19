--joint function examples
use AlterDB
--fetch record
select* from Employee

--fetch record
select* from traine


--inner join query
select* from Employee
inner join
traine
on Employee.Id = traine.name

--- new tables

CREATE TABLE Departments (
    DeptID INT PRIMARY KEY,
    DeptName VARCHAR(50)
);

CREATE TABLE Employees (
    EmpID INT PRIMARY KEY,
    EmpName VARCHAR(50),
    DeptID INT,
    FOREIGN KEY (DeptID) REFERENCES Departments(DeptID)
);

--insertion of data

INSERT INTO Departments (DeptID, DeptName) VALUES 
--(1, 'HR'),
--(2, 'IT'),
--(3, 'Finance'),
(4,'MARKETING');

INSERT INTO Employees (EmpID, EmpName, DeptID) VALUES 
--(101, 'Alice', 1),
--(102, 'Bob', 2),
--(103, 'Charlie', 2),
--(104, 'David', 3),
(105,'sanjay',2),
(106,'fasinal',1);


-- inner joint

SELECT * FROM Employees
INNER JOIN Departments 
ON Employees.DeptID = Departments.DeptID;

--sample

SELECT Employees.EmpID, Employees.EmpName, Departments.DeptName
FROM Employees
INNER JOIN Departments 
ON Employees.DeptID = Departments.DeptID;

--outer left join
select * from Employees
left join
Departments
on Employees.DeptID = Departments.DeptID;


select * from Departments


-- right join

select EmpName, DeptName
from Employees as e
right join
Departments as d
on e.DeptID= d.DeptID

--full join

select EmpName, DeptName
from Employees as e
full join
Departments as d
on e.DeptID= d.DeptID
where EmpName is not null and DeptName is not null --reduce memory and time


--to get the count

select Departments.DeptName, count(Employees.EmpID) as EmployeeCount
from Departments
inner join Employees on Departments.DeptID = Employees.DeptID
group by Departments.DeptName
having count(Employees.DeptID) > 1;

--more than one dept

select Employees.EmpID, Employees.EmpName, count(Employees.DeptID) as DeptCount
from Employees
group by Employees.EmpID, Employees.EmpName
having count(Employees.DeptID) > 1;


--three join

create table trainer
(
id int identity primary key not null,
name varchar(50) null,

);
drop table trainer

insert into trainer values ('hellen'),('kavitha'),('reema'),('shemma')

alter table Employees add trainer_id int null constraint kf_tr foreign key references Employees(EmpID)

update Employees set trainer_id=1 where EmpID=101
select*from Employees
select*from trainer

--cross joint
create table shape
(
id int identity,
name varchar(200));

create table colour
(
id int identity,
name varchar(200));

insert into shape(name) values
('circle'),
('square'),
('rectangle'),
('triangle');


insert into colour(name) values
('red'),
('green'),
('blue');

select* from colour


select s.name,c.name from shape as s
cross join
colour as c
where s.id=c.id


--self join
SELECT e1.id AS EmployeeID, 
       e1.name AS EmployeeName, 
       e2.id AS ManagerID, 
       e2.name AS ManagerName
FROM employee e1
JOIN employee e2 
ON e1.manager_id = e2.id;

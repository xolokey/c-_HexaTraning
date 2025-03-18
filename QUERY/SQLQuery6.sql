--DATE 18/3/25

select* from Employee

alter table Employee add salary bigint null

select* from Employee 
update Employee set salary=3000 where Id=104
--arranging in dessending (desc) or assending(asc) (sorting)

select * from Employee order by Id desc

-- sorting by name attribute

select * from Employee order by name

--oprerators all,and,any,between,exits,in,like,not,or,some
select * from Employee where Id=104 or Id=102

select * from Employee where name like 's%y'  --sanjay first letter s and last letter y( '_a%' for second letter)('%a_' for second letter from last)

--offset and fetch clause

select * from Employee order by Id asc offset 2 rows
fetch  first 2 rows only

--aggregate functions in sql server
--counting fn

select count(Id) as No_of_Employees from Employee

--min salary

select min(salary) as Minimum_Salary from Employee

--Max salary

select max(salary) as Maximum_Salary from Employee

-- average salary

select avg(salary) as Average_Salary from Employee

-- sum of salary

select sum(salary) as Total_Salary from Employee

--

select * from Employee

select name, license from Employee group by license,name

-- count no of employees using same license

select count(name),license from Employee group by license --having salary <=1000 (for geting salary above 1000)







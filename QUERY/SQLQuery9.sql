--DATE 20/03/25
--Alter table
alter table tarinee add skill int null

--update traine with out  traineeid

update trainee set skill =(select id from trainee where name='python')
where name = 'trainee name'

--

-- student table
create table Students
(
student_id int identity(100,1) primary key not null,
first_name varchar(100),
last_name varchar(100),
date_of_birth date not null,
email varchar(100),
phone_number varchar(100)
);

 create table teacher
 (
 teacher_id int identity(400,1) primary key not null,
 first_name varchar(100),
 last_name varchar(100),
 email varchar(100),
 );

 insert into students(first_name,last_name,date_of_birth,email,phone_number) values
 ('sanjay','krishnan','2003-05-09','sanjay@gmail','903457899'),
 ('mathan','kumar','2003-03-03','mathan@gmail','893748700'),
 ('kishore','rv','2002-02-04','kisorerv@gmail','90809800'),
 ('kishore','c','2004-04-27','kichorec@gmail','088078789'),
 ('hariharan','si','2004-04-14','hariharan@gamil','0840300909'),
 ('immanuvel','krithic','2002-07-09','immanuvel@gmail','093200809'),
 ('harini','ramesh','2004-03-03','harinisri@gmail','098980909'),
 ('harini','a','2003-04-14','harinia@gmail','8789894789'),
 ('krish','sunil','2003-10-02','krish@gmail','8498178809'),
 ('kevin','kelvin','2004-01-12','kevin@gmail','900980980');

  insert into teacher(first_name,last_name,email) values
 ('abirami','a','abirami@gamil'),
 ('senthil','murugan','senthil@gamil'),
 ('ramesh','v','ramesh@gmail'),
 ('varsha','patil','varsha@gmail'),
 ('premalatha','s','prema@gmail'),
 ('arthi','m','arthi@gmail'),
 ('sakthi','vel','sakthi@gmail'),
 ('himanika','m','hima@gmail'),
 ('harisha','s','harisha@gmail'),
 ('priyanka','g','priyanka@gmail');

 select * from Students
 select* from teacher

 --sub query

 create table product
 (
 pid int identity(200,1) primary key not null,
 pname varchar(100),cx
 pprice bigint);

 insert into product values
 ('pen','10'),
 ('notebook','50'),
 ('toys','100'),
 ('laptop','20000'),
 ('pencl','10'),
 ('pencilbox','250');

 create table orders
 (
 orid int identity(200,1) primary key not null,
 ordate date,
 prid int,
 constraint pr_prid foreign key (prid) references product(pid)
 );

 insert into orders values (getdate(),(select pid from product where pname='pen')),
 (getdate(),(select pid from product where pname='notebook')),
 (getdate(),(select pid from product where pname='toys')),
 (getdate(),(select pid from product where pname='laptop')),
 (getdate(),(select pid from product where pname='pencl'));

 select* from orders

select* from product

delete from orders

insert into orders(ordate) values(getdate())

--subquery within update
 update orders set prid=(select pid from product where pname='laptop')
 where orid= 222
select* from orders

--subquery with delete
delete from product
where pid=202


delete from orders 
where prid=203


delete from orders where prid= 201

delete from product where pid not in (select prid from orders)
select*from product
select*from orders

--sub query with in select

select pname,pprice from (select*from product) as t

select avg(pprice) from product
select pname,pprice from product where  pprice >=(select avg(pprice) from product)

select pname,count(pprice)from product group by pname,pprice

select pname,pprice from product where pprice in(select avg(pprice) from product group by pname)


select pname,pprice from product where pprice >= any(select avg(pprice) from product group by pname)

--subquery use groupby , count and can be done using  inner join to count the porduct order or other situations

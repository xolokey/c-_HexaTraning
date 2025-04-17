--CASE STUDY
--create tables wit appropriate class
--create customer table

create table Customer(
CustomerID int primary key not null,
FirstName varchar(100),
LastName varchar(100),
Email varchar(100)unique,
PhoneNumber varchar(100) unique,
Address text,
UserName varchar(100) unique,
Password varchar(100),
RegistrationDate date);

select* from Customer
--create vehicle table

create table Vehicle(
VehicleID int identity(200,1) primary key not null,
Model varchar(100),
Make varchar(100),
Year date ,
Color varchar(100),
RegistrationNumber varchar(100) unique,
Availability tinyint not null default 1,
DailyRate decimal(10,2));

--create reservation table

create table Reservation(
ReservationID int identity(300,1) primary key not null,
CustomerID int,
VehicleID int,
StartDate date not null,
EndDate date not null,
Totalcost decimal(10,2),
Status Varchar(100),
foreign key(CustomerID) references Customer(CustomerID) on delete cascade,
foreign key(VehicleID) references Vehicle(VehicleID) on delete cascade,
check (EndDate>StartDate));

--create admin table

create table Admin(
AdminID int identity(400,1) primary key not null,
FirstName varchar(100),
LastName varchar(100),
Email varchar(100),
PhoneNumber varchar(100) unique,
UserName varchar(100) unique,
Password varchar(100) not null,
Role varchar(100)not null,
JoinDate date not null);

select* from Customer
select* from Admin
select* from Reservation
select* from Vehicle

delete from Customer
where CustomerID=101






--inserting values into customer table
 insert into Customer (first_name,last_name,email,phone_number,address,username,password) values
 ('rajesh','a','rajesh@gmail','8768798790','23,frank street,dindukal,tamilnadu','rajesh@cars','rajesh@123'),
 ('ramesh','m','ramesh@gmail','8768798990','24,frank street,dindukal,tamilnadu','ramesh@cars','ramesh@123'),
 ('suresh','kumar','suresh@gmail','87787948790','25,frank street,dinduka,tamilnadu','suresh@cars','suresh@123'),
 ('ranjith','kumar','ranjith@gmail','8768787790','26,frank street,dindukal,tamilnadu','ranjih@cars','rajith@123'),
 ('soujin','james','soujin@gmail','87689879790','27,frank street,dindukal,tamilnadu','soujin@cars','soujin@123'),
 ('priya','mohan','priya@gmail','8787798790','28,frank street,dindukal,tamilnadu','priya@cars','priya@123'),
 ('mary','jaine','mary@gmail','876879790','29,frank street,dindukal,tamilnadu','mary@cars','mary@123'),
 ('gayathri','t','gayathri@gmail','8769898790','30,frank street,dindukal,tamilnadu','gayu@cars','gayu@123');

  insert into Customer (first_name,last_name,email,phone_number,address,username,password) values
 ('gayathri','m','gayathrim@gmail','87698998790','31,frank street,dindukal,tamilnadu','gayum@cars','gayum@123');
 select* from Customer

 update Customer
 set registration_date=(getdate());
 
 
 delete from Customer
 where customer_id=108;

 -- insert values into vehicle table

 insert into vehicle(model,make,year,color,registration_number,Availability, dailyrate) values
 ('hundai i10','hundai','2022','blue','tn26bc6758',('available'),'3500'),
 ('hundai i20','hundai','2020','black','tn69bc6878','not avilable','4500'),
 ('baleno','suzhi','2021','white','tn54jc3458','not available','4000'),
 ('city','honda','2023','black','tn31bc6898','available','4500'),
 ('xuv 700','mahindra','2024','blue','tn64bc6757','available','5000'),
 ('x7','suzhi','2022','white','tn07bc8790','available','3500');






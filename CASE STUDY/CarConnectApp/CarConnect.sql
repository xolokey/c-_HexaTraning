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

--Sample Customers
INSERT INTO Customer VALUES 
(1, 'Alice', 'Smith', 'alice@example.com', '1234567890', '123 Main St', 'alice_smith', 'alicepass', GETDATE()),
(2, 'Bob', 'Johnson', 'bob@example.com', '2345678901', '456 Oak Ave', 'bobbyj', 'bobpass', GETDATE()),
(3, 'Carol', 'Williams', 'carol@example.com', '3456789012', '789 Pine Rd', 'carolw', 'carolpass', GETDATE());



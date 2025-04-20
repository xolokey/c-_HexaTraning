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
where CustomerID=102;


--Inserting Sample values into Customer Table
INSERT INTO Customer (CustomerID,FirstName,LastName,Email,PhoneNumber,Address,UserName,Password,RegistrationDate) VALUES 
(102, 'Alice', 'Johnson', 'alice.johnson@example.com', '9876543210', '12 Park Lane', 'alicej', 'passAlice', '2024-04-01'),
(103, 'Brian', 'Smith', 'brian.smith@example.com', '8765432109', '89 River Road', 'brians', 'passBrian', '2024-04-05'),
(104, 'Carla', 'Miller', 'carla.miller@example.com', '7654321098', '45 Maple Street', 'carlam', 'passCarla', '2024-04-10');

--Inserting Sample Values Into Admin table
INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, UserName, Password, Role, JoinDate)
VALUES 
('Amit', 'Shah', 'amit@admin.com', '9000011111', 'amitadmin', 'amit123', 'Admin', '2024-04-01'),
('Reema', 'Patel', 'reema@admin.com', '9000022222', 'reemaadmin', 'reema123', 'Manager', '2024-04-05'),
('Karan', 'Singh', 'karan@admin.com', '9000033333', 'karanadmin', 'karan123', 'Admin', '2024-04-10');

--Inserting Values Into Vehicle Table
INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES
('Corolla', 'Toyota', '2020-01-01', 'White', 'TN01AB1234', 1, 1800.00),
('City', 'Honda', '2021-03-15', 'Black', 'TN02CD5678', 1, 2200.00),
('Swift', 'Maruti', '2019-07-10', 'Red', 'TN03EF9012', 1, 1500.00);

--Inserting Sample Values Into Reservation Table
INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES
(101, 200, '2024-04-20', '2024-04-25', 9000.00, 'Confirmed'),
(102, 201, '2024-05-01', '2024-05-03', 4400.00, 'Pending'),
(103, 202, '2024-05-10', '2024-05-15', 7500.00, 'Cancelled');



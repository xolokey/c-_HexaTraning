--Codinding Challange
--Creating Vehicle Table

create table Vehicle (
vehicleID int identity(100,1) primary key not null,
make varchar(100),
model varchar(100),
year int,
dailyrate decimal(10,2),
status_avilable varchar (50),
passangerCapacity int,
engineCapacity int
);
--Creating Customer Table
create table Customer(
customerID int identity(200,1) primary key not null,
firstName varchar(100),
lastname varchar(100),
email varchar(50),
phoneNumber varchar(50)
);
--Creating Lease Table
create table Lease(
leaseID int identity(300,1) primary key not null,
vehicleID int,
customerID int,
startDate date,
endDate date,
leasetype varchar(50),
foreign key (vehicleID) references Vehicle(vehicleID),
foreign key (customerID) references Customer(customerId)
);
--Creating Payment Table
create table Payment(
paymentID int identity(400,1) primary key not null,
leaseID int,
paymentDate date,
amount decimal(10,2),
foreign key (leaseID) references Lease(leaseID)

);

select*from Customer
select* from Lease
select* from Payment
select* from Vehicle
--inserting values to Vehicle table
insert into Vehicle(make,model,year,dailyrate,status_avilable,passangerCapacity,engineCapacity) values
--('Toyota','Camry','2022','50.00','1 ','4','1450'),
('Honda','Civic','2023','45.00','1 ','7','1500'),
('Ford','Focus','2022','48.00','0','4','1400'),
('Nissan','Altima','2023','52.00','1','7','1200'),
('Chevrolet','Malibu','2022','47.00','1','4','1800'),
('Hyundai','Sonata','2023','49.00','0','7','1400'),
('BMW','3 Series','2023','60.00','1','7','2499'),
('Mercedes','C-Class','2022','58.00','1','8','2599'),
('Audi','A4','2022','55.00','0','4','2500'),
('Lexus','ES','2023','54.00','1','4','2500');

select* from Customer
--inserting values to customer table
insert into Customer(firstName,lastname,email,phoneNumber) values
--('John','Doe','johndoe@example.com','555-555-5555');
('Jane','Smith','janesmith@example.com','555-123-4567'),
('Robert','Johnson','robert@example.com','555-789-1234'),
('Sarah','Brown','sarah@example.com','555-456-7890'),
('David','David','david@example.com','555-987-6543'),
('Laura','Hall','laura@example.com','555-234-5678'),
('Michael','Davis','michael@example.com','555-876-5432'),
('Emma','Wilson','emma@example.com','555-432-1098'),
('William','Taylor','william@example.com','555-321-6547'),
('Olivia','Adams','olivia@example.com','555-765-4321');

--inserting values to lease table
insert into Lease(vehicleID,customerID,startDate,endDate,leasetype) values
--('101','201','2023-01-01','2023-01-05','Daily');
('100','200','2023-02-15','2023-02-28','Monthly'),
('102','202','2023-03-10','2023-03-15','Daily'),
('103','203','2023-04-20','2023-04-30','Monthly'),
('104','204','2023-05-05','2023-05-10','Daily'),
('105','205','2023-06-15','2023-06-30','Monthly'),
('106','206','2023-07-01','2023-07-10','Daily'),
('107','207','2023-08-12','2023-08-15','Monthly'),
('108','208','2023-09-07','2023-09-10','Daily'),
('109','209','2023-10-10','2023-10-31','Monthly');

--inserting values to Payment table
select * from Payment

insert into Payment(leaseID,paymentDate,amount) values
('300','2023-01-03','200.00'),
('301','2023-02-20','1000.00'),
('302','2023-03-12','75.00'),
('303','2023-04-25','900.00'),
('304','2023-05-07','60.00'),
('305','2023-06-18 ','1200.00'),
('306','2023-07-03','40.00'),
('307','2023-08-14','1100.00'),
('308','2023-09-09','80.00'),
('309','2023-10-25','1500');

--Coding Challanges Questions
-- 1. Update the daily rate for a Mercedes car to 68
UPDATE Vehicle 
SET dailyRate = 68 
WHERE make = 'Mercedes';



--2. Delete a specific customer and all associated leases and payments
DELETE FROM Payment WHERE leaseID IN (SELECT leaseID FROM Lease WHERE customerID = 204);
DELETE FROM Lease WHERE customerID = 204;
DELETE FROM Customer WHERE customerID = 204;

--3. Rename the 'paymentDate' column in the Payment table to 'transactionDate'
EXEC sp_rename 'Payment.paymentDate', 'transactionDate', 'COLUMN';--4. Find a specific customer by email
SELECT * FROM Customer WHERE email = 'johndoe@example.com';--5. Get active leases for a specific customer
SELECT * FROM Lease WHERE customerID = 200;--6. Find all payments made by a customer with a specific phone number
SELECT p.* FROM Payment p
JOIN Lease l ON p.leaseID = l.leaseID
JOIN Customer c ON l.customerID = c.customerID
WHERE c.phoneNumber = '555-123-4567';--7. Calculate the average daily rate of all available cars
SELECT AVG(dailyRate) AS AverageDailyRate FROM Vehicle WHERE status_avilable = '1';--8. Find the car with the highest daily rate

SELECT TOP 1 * FROM Vehicle 
ORDER BY dailyRate DESC;

--9. Retrieve all cars leased by a specific customer

SELECT v.* 
FROM Vehicle v
JOIN Lease l ON v.vehicleID = l.vehicleID
WHERE l.customerID = (SELECT customerID FROM Customer WHERE email = 'robert@example.com')

--10. Find the details of the most recent lease

SELECT TOP 1 * FROM Lease 
ORDER BY startDate DESC;

--11. List all payments made in the year 2023

SELECT * FROM Payment 
WHERE YEAR(paymentDate) = 2023;


--12. Retrieve customers who have not made any payments

SELECT c.* 
FROM Customer c
LEFT JOIN Lease l ON c.customerID = l.customerID
LEFT JOIN Payment p ON l.leaseID = p.leaseID;


delete from Payment
--13. Retrieve Car Details and Their Total Payments

SELECT v.vehicleID, v.make, v.model, SUM(p.amount) AS totalPayments
FROM Vehicle v
JOIN Lease l ON v.vehicleID = l.vehicleID
JOIN Payment p ON l.leaseID = p.leaseID
GROUP BY v.vehicleID, v.make, v.model;

--14. Calculate Total Payments for Each Customer

SELECT c.customerID, c.firstName, c.lastName, SUM(p.amount) AS totalSpent
FROM Customer c
JOIN Lease l ON c.customerID = l.customerID
JOIN Payment p ON l.leaseID = p.leaseID
GROUP BY c.customerID, c.firstName, c.lastName;

--15. List Car Details for Each Lease

SELECT l.leaseID, v.make, v.model, c.firstName, c.lastName, l.startDate, l.endDate, l.leasetype
FROM Lease l
JOIN Vehicle v ON l.vehicleID = v.vehicleID
JOIN Customer c ON l.customerID = c.customerID;


--16. Retrieve Details of Active Leases with Customer and Car Information

SELECT l.leaseID, c.firstName, c.lastName, v.make, v.model, l.startDate, l.endDate
FROM Lease l
JOIN Customer c ON l.customerID = c.customerID
JOIN Vehicle v ON l.vehicleID = v.vehicleID
WHERE l.endDate >= GETDATE();

--17. Find the Customer Who Has Spent the Most on Leases

SELECT TOP 1 c.customerID, c.firstName, c.lastName, SUM(p.amount) AS totalSpent
FROM Customer c
JOIN Lease l ON c.customerID = l.customerID
JOIN Payment p ON l.leaseID = p.leaseID
GROUP BY c.customerID, c.firstName, c.lastName
ORDER BY totalSpent DESC;

--18. List All Cars with Their Current Lease Information

SELECT v.vehicleID, v.make, v.model, l.startDate, l.endDate, c.firstName, c.lastName
FROM Vehicle v
LEFT JOIN Lease l ON v.vehicleID = l.vehicleID
LEFT JOIN Customer c ON l.customerID = c.customerID;--Coding Challange Completed

--CASE STUDY
--create tables wit appropriate class
--create customer table

create table Customer(
customer_id int identity(100,1) primary key not null,
first_name varchar(100),
last_name varchar(100),
email varchar(100)unique,
phone_number varchar(100) unique,
address text,
username varchar(100) unique,
password varchar(100),
registration_date date);

--create vehicle table

create table vehicle(
vehicle_id int identity(200,1) primary key not null,
model varchar(100),
make varchar(100),
year date ,
color varchar(100),
registration_number varchar(100) unique,
Availability tinyint not null default 1,
dailyrate decimal(10,2));

--create reservation table

create table reservation(
reservation_id int identity(300,1) primary key not null,
customer_id int,
vehicle_id int,
start_date date not null,
end_date date not null,
totalcost decimal(10,2),
--status enum ('pending','confirmed','canceled') not null default pending,
foreign key(customer_id) references customer(customer_id) on delete cascade,
foreign key(vehicle_id) references vehicle(vehicle_id) on delete cascade,
check (end_date>start_date));

--create admin table

create table admin(
admin_id int identity(400,1) primary key not null,
first_name varchar(100),
last_name varchar(100),
email varchar(100),
phone_number varchar(100) unique,
username varchar(100) unique,
password varchar(100) not null,
role varchar(100)not null,
joindate date not null);






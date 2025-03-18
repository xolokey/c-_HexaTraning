use SISDB

-- student table
create table Students
(
student_id int identity(100,1) primary key not null,
first_name varchar(100),
last_name varchar(100),
date_of_birth int not null,
email varchar(100),
mobile_number int
);



-- courses

--parent table

create table courses
(
course_id int identity(100,1) primary key not null,
course_name varchar(100),
creidts varchar(100),
);

--child table
create table teacher_id
(
id int identity(200,1) primary key not null,
name varchar(100),
teacher_id int not null,
--constraint TR_ID foreign key (teacher_id) 
--references courses(course_id)
);

--enrollments

--parent 
 create table enrollment
 (
 enrollement_id int identity(100,1) primary key not null,



 -- teacher table
 create table teacher
 (
 teacher_id int identity(100,1) primary key not null,
 first_name varchar(100),
 last_name varchar(100),
 email varchar(100),
 );

 --payments

 create table payments
 (
 payment_id int identity(100,1) primary key not null,
 amount int,
 payment_date int
 );
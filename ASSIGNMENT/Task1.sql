use StudentInfoDB
--TASK 1 STUDENT INFORMATION SYSTEM

-- student table
create table Students
(
StudentID int identity(100,1) primary key not null,
FirstName varchar(100) not null,
LastName varchar(100) not null,
DateOfBirth date not null,
Email varchar(100) not null,
PhoneNumber varchar(100) not null
);



-- courses

create table Courses
(
CourseID int identity(200,1) primary key not null,
CourseName varchar(100)not null,
CourseCode varchar(100)not null,
InstructorName varchar(100)not null,
--AssignedTeacher int 
--foreign key (AssignedTeacher) references Teacher(TeacherID) on delete set null,
);


--enrollments

--parent 
 create table Enrollment
 (
 EnrollmentID  int identity(300,1) primary key not null,
 StudentID int,
 CourseID  int,
 EnrollmentDate datetime not null,
 foreign key (StudentID) references Students(StudentID) on delete set null,
 foreign key (CourseID) references Courses(CourseID) on delete set null,
 );


 -- teacher table
 create table Teacher
 (
 TeacherID  int identity(400,1) primary key not null,
 FirstName varchar(100),
 LastName varchar(100),
 Email varchar(100),
 );

 --payments

 create table Payments
 (
 PaymentID int identity(500,1) primary key not null,
 StudentID int,
 Amount int,
 PaymentDate date not null,
 foreign key(StudentID) references Students(StudentID) on delete set null,
 );

 --Task 1 Completed
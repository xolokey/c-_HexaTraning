select*from Students
select*from Courses
select*from Enrollment
select*from Teacher
select*from Payments

delete from Courses
where CourseID=201;

delete from Students
where StudentID=101;

delete from Enrollment
where EnrollmentID=2002;

delete from Teacher
where TeacherID=301;

delete from Payments
where PaymentID=3002;

UPDATE Courses SET InstructorName = 'sara'
WHERE CourseID = 203;

select*from Students
select*from Courses
select*from Enrollment
select*from Teacher

SELECT CourseID, CourseName, CourseCode, InstructorName 
FROM Courses 
WHERE CourseID = 202;

insert into Students(StudentID,FirstName,LastName,DateOfBirth,Email,PhoneNumber) values
('102','Ajay','kumar','2004-05-14','ajay@gmail.com','67678678678'),
('103','Sunil','Kumar','2003-07-19','sunil@gmail.com','7687676696');
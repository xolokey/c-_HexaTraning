select*from Students
select*from Courses
select*from Enrollment
select*from Teacher
select*from Payments

delete from Courses
where CourseID=203;

delete from Students
where StudentID=101;

delete from Enrollment
where EnrollmentID=1011;

delete from Teacher
where TeacherID=301;

delete from Payments
where PaymentID=2000;

UPDATE Courses SET InstructorName = 'sara'
WHERE CourseID = 203;

select*from Students
select*from Courses
select*from Enrollment
select*from Teacher

SELECT CourseID, CourseName, CourseCode, InstructorName 
FROM Courses 
WHERE CourseID = 202;
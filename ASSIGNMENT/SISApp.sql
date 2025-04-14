select*from Students
select*from Courses
select*from Enrollment
select*from Teacher

delete from Courses
where CourseID=203;

delete from Students
where StudentID=101;

delete from Enrollment
where EnrollmentID=1009;

delete from Teacher
where TeacherID=301;

UPDATE Courses SET InstructorName = 'sara'
WHERE CourseID = 203;

select*from Students
select*from Courses
select*from Enrollment
select*from Teacher

SELECT CourseID, CourseName, CourseCode, InstructorName 
FROM Courses 
WHERE CourseID = 202;
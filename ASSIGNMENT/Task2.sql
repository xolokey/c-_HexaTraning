--Task-2 STUDENT INFORMATION SYSTEM
--(1)To insert a new student data
--1
insert into students(first_name,last_name,date_of_birth,email,phone_number) values
('sarath','kumar','2003-11-29','sarath@gmail','768767989');

select * from Students

--(2)insert into enrollment
 insert into enrollment(enrollment_date) values
 ('2024-04-16');

 select* from enrollment

 --(3)update emailid of specific teacher

update teacher
set email='himanika@gmail'
where teacher_id= 407;

select*from teacher

-- (4)delete a specific enrollment record

--updating student_id and course_id before deleting the enrollment record

update enrollment 
set student_id=109,course_id=209
where enrollment_id= 309;

select* from enrollment


delete from enrollment 
where student_id= 103 and course_id=203;
--where enrollment_id= 1311;

--(5)specific teacher to couress

select*from courses
select*from teacher
 
 update courses
 set teacher_id= 409
 where course_id= 209;

 --(6)deleting specific student and enrollments
 select* from Students

 delete from Students
 where student_id=1101;
 
 --(7)updating payment amount
 select*from payments

 update payments
 set amount=10000
 where payment_id=502;

 --TASK 2 COMPLETED


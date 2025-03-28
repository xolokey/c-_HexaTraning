--TASK 3
--updating student_id in payment column

update payments
set student_id=109
where payment_id=509;

-- Sum of payment by a student

select s.student_id, s.first_name, s.last_name, sum(p.amount) as total_payments
from Students s
join Payments p on s.student_id = p.student_id
where s.student_id = 102
group by s.student_id, s.first_name, s.last_name;

select* from payments
select *from Students

--count of list of students enrolled in a course

select* from enrollment

select c.course_id, c.course_name, count(e.student_id) as total_students
from Courses c
left join enrollment e on c.course_id = e.course_id
group by c.course_id, c.course_name
order by total_students desc;


--student who has not enrolled in any course

select * from Students
select* from enrollment
select* from courses

select s.student_id ,s.first_name, s.last_name
from Students s
left join enrollment e on s.student_id=e.student_id
where e.enrollment_id is null;

--students name with the courses they are enrolled in

select s.first_name,s.last_name,c.course_name
from Students s
join enrollment e on s.student_id=e.student_id
join courses c on e.course_id=c.course_id
order by s.first_name,s.last_name;

--name of teachers and the course they are assigned to

select* from teacher

select t.first_name,t.last_name,c.course_name
from teacher t
join courses c on t.teacher_id=c.teacher_id
order by t.first_name , t.last_name;

--list of students and their enrollment date with specific course(error)

select s.first_name,s.last_name,e.enrollment_date,c.course_name
from Students s
join enrollment e on s.date_of_birth=e.student_id
join courses c on e.course_id=c.course_id
where c.course_id= 201
order by e.enrollment_date;

--name of the students not made any payments

select* from Students
select* from payments

select s.student_id,s.first_name,s.last_name
from Students s
left join payments p on s.student_id=p.student_id
where p.payment_id is null;

update payments--modify the payment table for updating a student payment for result
set student_id= 102
where payment_id= 503;

--courses that have no enrollments

select* from courses
select* from enrollment

select c.course_id,c.course_name
from courses c
left join enrollment e on  c.course_id=e.course_id
where  e.enrollment_id is null;




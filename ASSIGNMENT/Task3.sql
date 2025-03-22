--TASK 3
--updating student_id in payment column

update payments
set student_id=109
where payment_id=509;

--(1)Sum of payment by a student

select s.student_id, s.first_name, s.last_name, sum(p.amount) as total_payments
from Students s
join Payments p on s.student_id = p.student_id
where s.student_id = 102
group by s.student_id, s.first_name, s.last_name;

select* from payments
select *from Students

--(2)count of list of students enrolled in a course

select* from enrollment

select c.course_id, c.course_name, count(e.student_id) as total_students
from Courses c
left join enrollment e on c.course_id = e.course_id
group by c.course_id, c.course_name
order by total_students desc;


--(3)student who has not enrolled in any course

select * from Students
select* from enrollment
select* from courses

select s.student_id ,s.first_name, s.last_name
from Students s
left join enrollment e on s.student_id=e.student_id
where e.enrollment_id is null;

--(4)students name with the courses they are enrolled in any course

select s.first_name,s.last_name,c.course_name
from Students s
join enrollment e on s.student_id=e.student_id
join courses c on e.course_id=c.course_id
order by s.first_name,s.last_name;

--(5)name of teachers and the course they are assigned to

select* from teacher

select t.first_name,t.last_name,c.course_name
from teacher t
join courses c on t.teacher_id=c.teacher_id
order by t.first_name , t.last_name;

--(6)list of students and their enrollment date with specific course

select s.first_name,s.last_name,e.enrollment_date,c.course_name
from Students s
join enrollment e on s.student_id=e.student_id
join courses c on e.course_id=c.course_id
group by c.course_name,e.enrollment_date,s.first_name,s.last_name
order by e.enrollment_date asc;

--(7)name of the students not made any payments

select* from Students
select* from payments

select s.student_id,s.first_name,s.last_name
from Students s
left join payments p on s.student_id=p.student_id
where p.payment_id is null;

update payments--modify the payment table for updating a student payment for result
set student_id= 102
where payment_id= 503;

--(8)courses that have no enrollments

select* from courses
select* from enrollment

select c.course_id,c.course_name
from courses c
left join enrollment e on  c.course_id=e.course_id
where  e.enrollment_id is null;

--(9)students who are enrolled in more than one course
select* from enrollment
update enrollment
set student_id=104
where course_id=207;



select s.student_id,s.first_name,s.last_name,count(e.course_id) as total_courses
from Students s
join enrollment e on s.student_id=e.student_id
group by s.student_id,s.first_name,s.last_name
having count(e.course_id)>1 
order by total_courses asc ;

--(10)find teachers who are not assigned to any course
select*from teacher
select*from courses

update courses
set teacher_id=400
where course_id=200;

select t.teacher_id,t.first_name,t.last_name
from teacher t
left join courses c on t.teacher_id=c.teacher_id
--order by t.first_name,t.last_name
where c.course_id is null;

--TASK 3 COMPLETED

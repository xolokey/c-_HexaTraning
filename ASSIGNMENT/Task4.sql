--TASK 4

--1 Average number of students enrolled in a course

select avg(student_count) as avg_no_of_student
from 
(select course_id , count(student_id) as student_count
from enrollment
group by course_id)
as courseenrollmentcount;

--using join query

select* from courses
select* from enrollment
select* from Students
 
select avg (student_count) as avg_student_per_course
from
(select c.course_id , count(Student_id) as student_count
from courses c
left join enrollment e on c.course_id=e.course_id
group by c.course_id
) as courseenrollmentcount;

--2 students who made the highest payments

select* from payments
select* from Students

select s.first_name,s.last_name,p.amount
from payments p
join Students s on p.student_id=s.student_id
where p.amount=(select max(amount) from payments);

--3 list of courses with highest no of enrollments
select* from courses
select* from enrollment

select c.course_name,count(student_id) as enrollmentcount
from courses c
join enrollment e on c.course_id=e.course_id
group by c.course_name,c.course_id
having count(e.student_id)=(select max(course_count) from (select count(student_id) as course_count
from enrollment
group by course_id) as coursecounts);

--4 total payment made to courses thaught by each teacher
select*from payments
select*from enrollment

select t.first_name ,t.last_name,
(select sum(p.amount)
from payments p
join enrollment e on p.student_id=e.student_id
where e.course_id in (select c.course_id
from courses c
where c.teacher_id=t.teacher_id)) as totalpayments
from teacher t;

--5 students enrolled in all avilable courses

select*from Students


select s.first_name,s.last_name
from Students s
where(select count(distinct course_id) from enrollment where student_id=s.student_id)=(select (count(course_id)) from courses);

--6 teacher not assigned to any course

select*from teacher
select*from courses

select t.first_name,t.last_name
from teacher t
where teacher_id not in(select distinct(teacher_id) from courses);

--7 calculate avg age of students
select* from Students

select avg(datediff (year,date_of_birth,getdate())) as average_age
from Students;

--8 courses with no enrollment
select* from enrollment
select* from courses

select c.course_name
from courses c
where c.course_id not in (select distinct course_id from enrollment);

--9 total payment per student per course
select*from payments
select* from enrollment

select e.student_id,e.course_id,sum(p.amount) as total_payment
from enrollment e
join payments p on e.student_id=p.student_id
group by e.student_id,e.course_id;

--10 student who made more than one payment

 select student_id , count(payment_id) as payment_count
 from payments
 group by student_id
 having count(payment_id) > 1;

 --11 total payment by each student

select*from payments
select* from Students

select s.first_name,s.last_name,count(p.amount) as total_payment
from payments p
join Students s on p.student_id=s.student_id
group by s.student_id,s.first_name,s.last_name;

--12 course name and count of enrolled students
select* from courses
select* from enrollment

select c.course_name,count(student_id) as student_count
from courses c
join enrollment e on c.course_id=e.course_id
group by c.course_id,c.course_name;

--13 calculate average payment amount by students
select* from payments
select* from Students

select s.first_name,s.last_name, avg(p.amount) as average_amount
from payments p
join Students s on p.student_id=s.student_id
group by s.first_name,s.last_name,s.student_id;

--TASK 4 COMPLETED





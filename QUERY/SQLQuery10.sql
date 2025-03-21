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

--student who has not enrolled in any course

select* from enrollment


--
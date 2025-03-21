select* from product
select* from  orders

update orders
set prid=208
where orid= 238;

insert into product (pname,pprice) values 
('laptop','1000000'),
('phone','10000'),
('watches','1000');


select  p.pid,p.pname,
(select count(prid) from Orders where prid= p.pid group by prid )  as Total_Order
from product as p

select pid, pname,pprice,(select ordate from orders where prid=p.pid) from product as p

select avg(pprice) from product group by pname
select pname,pprice from product where pprice >All(select avg(pprice) from product group by pname)
select * from product


select avg(pprice) from product group by pname
select pname,pprice from product where pprice >any(select avg(pprice) from product group by pname)
select * from product
use SISDB

-- student table
create table Students
(
student_id int identity(100,1) primary key not null,
first_name varchar(100),
last_name varchar(100),
date_of_birth date not null,
email varchar(100),
phone_number varchar(100)
);



-- courses


create table courses
(
course_id int identity(200,1) primary key not null,
course_name varchar(100),
credits varchar(100),
teacher_id int,
foreign key (teacher_id) references teacher(teacher_id) on delete set null
);


--enrollments

--parent 
 create table enrollment
 (
 enrollement_id int identity(300,1) primary key not null,
 student_id int,
 course_id int,
 enrollement_date int,
 --foreign key (student_id) references students(student_id) on delete cascade,
 --foreign key (course_id) references courses(course_id) on delete cascade,
 );


 -- teacher table
 create table teacher
 (
 teacher_id int identity(400,1) primary key not null,
 first_name varchar(100),
 last_name varchar(100),
 email varchar(100),
 );

 --payments

 create table payments
 (
 payment_id int identity(500,1) primary key not null,
 student_id int,
 amount int,
 payment_date int,
 --foreign key(student_id) references students(student_id) on delete cascade,
 );


 --inserting values student

 insert into students(first_name,last_name,date_of_birth,email,phone_number) values
 ('sanjay','krishnan','2003-05-09','sanjay@gmail','903457899'),
 ('mathan','kumar','2003-03-03','mathan@gmail','893748700'),
 ('kishore','rv','2002-02-04','kisorerv@gmail','90809800'),
 ('kishore','c','2004-04-27','kichorec@gmail','088078789'),
 ('hariharan','si','2004-04-14','hariharan@gamil','0840300909'),
 ('immanuvel','krithic','2002-07-09','immanuvel@gmail','093200809'),
 ('harini','ramesh','2004-03-03','harinisri@gmail','098980909'),
 ('harini','a','2003-04-14','harinia@gmail','8789894789'),
 ('krish','sunil','2003-10-02','krish@gmail','8498178809'),
 ('kevin','kelvin','2004-01-12','kevin@gmail','900980980');


 select* from students

 -- inserting values teacher

 insert into teacher(first_name,last_name,email) values
 ('abirami','a','abirami@gamil'),
 ('senthil','murugan','senthil@gamil'),
 ('ramesh','v','ramesh@gmail'),
 ('varsha','patil','varsha@gmail'),
 ('premalatha','s','prema@gmail'),
 ('arthi','m','arthi@gmail'),
 ('sakthi','vel','sakthi@gmail'),
 ('himanika','m','hima@gmail'),
 ('harisha','s','harisha@gmail'),
 ('priyanka','g','priyanka@gmail');


 select* from teacher

 --completed table teacher students
 --inserting values to courses
 insert into courses (course_name,credits) values
 ('c','1'),
 ('python','2'),
 ('c++','1'),
 ('c#','2'),
 ('sql','2'),
 ('go','1'),
 ('java','2'),
 ('data science','1'),
 ('ai','2'),
 ('ml','2');

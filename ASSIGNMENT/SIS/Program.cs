using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;



namespace StudentInformationSystem
{//TASK 1 (CREATE CLASS)
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateofBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public void EnrollinCourse(Course course)
        {
            // Implementation for enrolling in a course
            var enrollment = new Enrollment
            {
                EnrollmentID = new Random().Next(1000, 9999), // Random ID for demonstration
                StudentID = this.StudentID,
                CourseID = course,
                EnrollmentDate = DateTime.Now
            };
            enrollments.Add(enrollment);
            course.Add(enrollment);
            //Console.WriteLine($"{FirstName} {LastName} has enrolled in {course.CourseName}");
        }

        public void UpdateStudentIno(string firstName, string lastName, DateOnly dateofBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateofBirth = dateofBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            // Implementation for making a payment
            var payment = new Payment
            {
                PaymentID = new Random().Next(1000, 9999), // Random ID for demonstration
                StudentID = this.StudentID,
                Amount = amount,
                PaymentDate = paymentDate
            };
            payments.Add(payment);
        }

        public void DisplayStudentDetails()
        {
            Console.WriteLine($"Student ID: {StudentID}, Name: {FirstName} {LastName}, DOB: {DateofBirth}, Email: {Email}, Phone: {PhoneNumber}");
        }
        public List<Enrollment> GetEnrollments() => enrollments;

        public List<Payment> GetPaymentHistory() => payments;


    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int CourseCode { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public Teacher AssignedTeacher { get; set; }

        public List<Enrollment> Enrollments = new();

        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeacher = teacher;
            teacher.AssignedCourse = this;
        }

        public void UpdateCourseDetails(string courseName, int courseCode, string instructorName)
        {
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
        }

        public void DisplayCourseDetails()
        {
            Console.WriteLine($"Course ID: {CourseID}, Name: {CourseName}, Code: {CourseCode}, Instructor: {InstructorName}");
        }

        public List<Enrollment> GetEnrollments()=> Enrollments;
        public Teacher GetAssignedTeacher() => AssignedTeacher;

        public void Add(Enrollment enrollment)
        {
            Enrollments.Add(enrollment);
        }

        public int GetTotalEnrollments() => Enrollments.Count;

        public decimal GetTotalPayments(List<Student> students)
        {
            returm Enrolments.Sum(e => e.GetStudent(students)?.GetPaymentHistory().Sum(p=>p.Amount)??0);
        }  


    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student StudentID { get; set; }
        public Course CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Student GetStudent(List<Student> students) => students.FirstOrDefault(s => s.StudentID == StudentID);

        public Course GetCourse(List<Course> courses) => courses.FirstOrDefault(c => c.CourseID == CourseID);
    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        private List<Course> assignedCourses = new();

        public void UpdateTeacherDetails(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayTeacherDetails()
        {
            Console.WriteLine($"Teacher ID: {TeacherID}, Name: {FirstName} {LastName}, Email: {Email}");
        }

        public List<Course> GetAssignedCourses() => assignedCourses;

        public void AddAssignedCourse(Course course)
        {
            assignedCourses.Add(course);
        }


    }

    public class Payment
    {
        public int PaymentID { get; set; }
        public Student StudentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Student GetStudent()=> student;
        public decimal GetPaymentAmount() => Amount;
        public DateTime GetPaymentDate() => PaymentDate;
    }
    //TASK 1---------------COMPLETED--------------------
    //TASK 2: CONSTRUCTOR IMPLEMENTTION

    public class Program
    {
        static void Main(string[] args)
        {
            //Creating a student
            Student student1 = new Student
            {
                StudentID = 101,
                FirstName = "sanjay",
                LastName = "kumar",
                DateofBirth = new DateOnly(2004, 5, 15),
                Email = "sanjaykuma@gmail.com",
                PhoneNumber = "876578901"
            };
            //Creating a course
            Course course1 = new Course
            {
                CourseID = 201,
                CourseName = "C# Programming",
                CourseCode = 101,
                InstructorName = "Newtorn"
            };
            //Creating an enrollment
            Enrollment enrollment1 = new Enrollment
            {
                EnrollmentID = 301,
                StudentID = student1,
                CourseID = course1,
               EnrollmentDate = DateTime.Now
            };
            //Creating a teacher
            Teacher teacher1 = new Teacher
            {
                TeacherID = 401,
                FirstName = "Newtorn",
                LastName = "Dony",
                Email = "newtorn@gmail.com"
            };
            // Creating a payment
            Payment payment1 = new Payment
            {
            
                PaymentID = 501,
                StudentID = student1,
                Amount = 5000.00m,
                PaymentDate = DateTime.Now
            };
            //Display Details
            Console.WriteLine("Student Details:");
            Console.WriteLine($"ID: {student1.StudentID}, Name: {student1.FirstName} {student1.LastName}, DOB: {student1.DateofBirth}, Email: {student1.Email}, Phone: {student1.PhoneNumber}");
            Console.WriteLine("Course Details:");
            Console.WriteLine($"ID: {course1.CourseID}, Name: {course1.CourseName}, Code: {course1.CourseCode}, Instructor: {course1.InstructorName}");
            Console.WriteLine("Enrollment Details:");
            Console.WriteLine($"ID: {enrollment1.EnrollmentID}, Student: {student1.StudentID}, Course: {course1.CourseID}, Date: {enrollment1.EnrollmentDate}");
            Console.WriteLine("Teacher Details:");
            Console.WriteLine($"ID: {teacher1.TeacherID}, Name: {teacher1.FirstName} {teacher1.LastName}, Email: {teacher1.Email}");
            Console.WriteLine("Payment Details:");
            Console.WriteLine($"ID: {payment1.PaymentID}, Student: {student1.StudentID}, Amount: {payment1.Amount}, Date: {payment1.PaymentDate}");
            Console.ReadLine();

            //Creating a SIS object
            SIS sis = new SIS();

            //Enrolling a student in a course
            sis.EnrollStudentInCourse(student1, course1);
            Console.WriteLine($"Student {student1.FirstName} {student1.LastName} enrolled in course {course1.CourseName}");
            //Assigning a teacher to a course
            sis.AssignTecherToCourse(teacher1, course1);
            //Record Payment Detils
            sis.RecordPayments(student1, payment1.Amount, payment1.PaymentDate);

            sis.GenerateEnrollmentReport(student1, enrollment1, course1);
            sis.GeneratePaymentReport(student1, payment1);
            sis.GenerateCourseStatics(course1);

            //TASK 2---------------COMPLETED--------------------

        }


    }
}
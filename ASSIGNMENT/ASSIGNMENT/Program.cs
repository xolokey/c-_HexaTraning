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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseCode { get; set; }
        public string InstructorName { get; set; }
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student StudentID { get; set; }
        public Course CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
    }

    public class Payment
    {
        public int PaymentID { get; set; }
        public Student StudentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
    //TASK 1---------------COMPLETED--------------------
    //TASK 2: CONSTRUCTOR IMPLEMENTTION
    
    public class program
    {
        static void Main(string[] args)
        {
            //Creating a student
            Student student1 = new Student();
            {
                student1.StudentID = 10;
                student1.FirstName = "sanjay";
                student1.LastName = "kumar";
                student1.DateofBirth = new DateOnly(2004, 5, 15);
                student1.Email = "sanjaykuma@gmail.com";
                student1.PhoneNumber = "876578901";
            };
            //Creating a course
            Course course1 = new Course();
            {
                course1.CourseID = 201;
                course1.CourseName = "C# Programming";
                course1.CourseCode = 101;
                course1.InstructorName = "Newtor";
            };
            //Creating an enrollment
            Enrollment enrollment1 = new Enrollment
            {
                enrollment1.EnrollmentID = 301,
                enrollment1.StudentID = student1,
                enrollment1.CourseID = course1,
                enrollment1.EnrollmentDate = DateTime.Now
            };
            //Creating a teacher
            Teacher teacher1 = new Teacher
            {
                teacher1.TeacherID = 401,
                teacher1.FirstName = "Newtorn",
                teacher1.LastName = "Dony",
                teacher1.Email = "newtorn@gmail.com"
            };
            //Creating a payment
            Payment payment1 = new Payment
            {
                payment1.PaymentID = 501,
                payment1.StudentID = student1,
                payment1.Amount = 5000.00m,
                payment1.PaymentDate = DateTime.Now
            };
            //Display Details
            Console.WriteLine("Student Details:");
            Console.WriteLine($"ID: {student1.StudentID}, Name: {student1.FirstName} {student1.LastName}, DOB: {student1.DateofBirth}, Email: {student1.Email}, Phone: {student1.PhoneNumber}");
            Console.WriteLine("Course Details:");
            Console.WriteLine($"ID: {course1.CourseID}, Name: {course1.CourseName}, Code: {course1.CourseCode}, Instructor: {course1.InstructorName}");
            Console.WriteLine("Enrollment Details:");
            Console.WriteLine($"ID: {enrollment1.EnrollmentID}, Student: {enrollment1.StudentID.FirstName} {enrollment1.StudentID.LastName}, Course: {enrollment1.CourseID.CourseName}, Date: {enrollment1.EnrollmentDate}");
            Console.WriteLine("Teacher Details:");
            Console.WriteLine($"ID: {teacher1.TeacherID}, Name: {teacher1.FirstName} {teacher1.LastName}, Email: {teacher1.Email}");
            Console.WriteLine("Payment Details:");
            Console.WriteLine($"ID: {payment1.PaymentID}, Student: {payment1.StudentID.FirstName} {payment1.StudentID.LastName}, Amount: {payment1.Amount}, Date: {payment1.PaymentDate}");
            Console.ReadLine();
            //TASK 2---------------COMPLETED--------------------

        }


    }
}
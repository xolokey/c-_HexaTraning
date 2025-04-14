using System;
using SISApp.DAO;
using SISApp.Entities;
using SISApp.Util;
using SISApp.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SISApp.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create instances of StudentDao and sample data
            StudentDao studentDao = new StudentDao();

            // Sample student and course
            Students student1 = new Students
            {
                StudentID = 101,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1995,08 ,15),
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            };
            var newStudent1 = studentDao.SaveStudent(student1);
            Console.WriteLine(newStudent1 != null ? "Student1 is Saved" : "Error");
            Console.WriteLine();

            //creating a course

            CoursesDao courseDao = new CoursesDao();
            Courses course1 = new Courses
            {
                CourseID =201,
                CourseName = " Introduction to Programming",
                CourseCode = "CS102",
                InstructorName = "Ramesh"
            };
            var newCourse1 = courseDao.SaveCourse(course1);
            Console.WriteLine(newCourse1 != null ? "Course-1 is Saved" : "Error");
            Console.WriteLine();

            Courses course2 = new Courses
            {
                CourseID = 202,
                CourseName = "Mathematics 101",
                CourseCode = "MAT201",
                InstructorName = "Anjali"
            };
            var newCourse2 = courseDao.SaveCourse(course2);
            Console.WriteLine(newCourse2 != null ? "Course-2 is Saved" : "Error");
            Console.WriteLine();

            Courses course3 = new Courses
            {
                CourseID = 203,
                CourseName = "Advanced Database Management",
                CourseCode = " CS302",
                InstructorName = "Sarath"
            };
            var newCourse3 = courseDao.SaveCourse(course3);
            Console.WriteLine(newCourse3 != null ? "Course-3 is Saved" : "Error");
            Console.WriteLine();

            Courses courses = new Courses
            {
                CourseID = 204,
                CourseName = "Computer Science 101",
                CourseCode = "CS101",
                InstructorName = "Ravi"
            };
            var newCourse4 = courseDao.SaveCourse(courses);
            Console.WriteLine(newCourse4 != null ? "Course-4 is Saved" : "Error");

            // Enroll student in course
            Console.WriteLine("Enrolling student in course...");
            studentDao.EnrollStudentInCourse(student1, course1);
            Console.WriteLine($"Student {student1.FirstName} {student1.LastName} successfully enrolled in {course1.CourseName}.");
            Console.WriteLine();

            //Creating Teacher
            TeacherDao teacherDao = new TeacherDao();
            Teacher teacher1 = new Teacher
            {
                TeacherID = 301,
                FirstName = "Sarah",
                LastName = "Smith",
                Email= "sarah.smith@example.com"

            };
            var newTeacher1 = teacherDao.SaveTeacher(teacher1);
            Console.WriteLine(newTeacher1 != null ? "Teacher is Saved" : "Error");
            Console.WriteLine();

            //Assign teacher to course
            Console.WriteLine("Assigning teacher to course...");
            teacherDao.AssignTeacher(teacher1,course3);
            Console.WriteLine($"Teacher {teacher1.FirstName} {teacher1.LastName} successfully enrolled in {course3.CourseName}.");
            Console.WriteLine();

            //Get Course Info(Course 1)
            Console.WriteLine("Displaying course info...");
            courseDao.DisplayCourseInfo(course1);
            Console.WriteLine($"Course Info successfully Displayed.");
            Console.WriteLine();

            //Record Payment Details
            Console.WriteLine("Inserting Payment Record...");
            PaymentDao paymentDao = new PaymentDao();
            paymentDao.RecordPayment(101, 500.00m, new DateTime(2023, 4, 10));
            Console.WriteLine("Payment record inserted successfully.");

            StudentDao studentDao1 = new StudentDao();
            // Generate Enrollment Report
            studentDao1.GenerateEnrollmentReport(201);








        }


    }
}

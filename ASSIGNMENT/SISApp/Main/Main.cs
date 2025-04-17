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
            //Initiating the DAO classes
            StudentDao studentDao = new StudentDao();
            CoursesDao courseDao = new CoursesDao();
            TeacherDao teacherDao = new TeacherDao();
            PaymentDao paymentDao = new PaymentDao();
            //Creating the While loop for the menu

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("------Student Information System Menu-----");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. Add New Course");
                Console.WriteLine("3. Enroll Student in Course");
                Console.WriteLine("4. Add New Teacher");
                Console.WriteLine("5. Assign Teacher to Course");
                Console.WriteLine("6. Record Payment");
                Console.WriteLine("7. Generate Enrollment Report");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                //Creating Switch Cases
                switch (choice)
                {
                    //To Create a Student
                    case "1":
                        Console.Write("Student ID: ");
                        int studentId = int.Parse(Console.ReadLine());
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Date of Birth (yyyy-MM-dd): ");
                        DateTime dob = DateTime.Parse(Console.ReadLine());
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Phone Number: ");
                        string phone = Console.ReadLine();
                        

                        Students student = new Students
                        {
                            StudentID = studentId,
                            FirstName = firstName,
                            LastName = lastName,
                            DateOfBirth = dob,
                            Email = email,
                            PhoneNumber = phone
                        };

                        studentDao.SaveStudent(student);
                        Console.WriteLine("Student saved.");
                        break;
                    case "2":
                        //To Create a Course
                        Console.Write("Course ID: ");
                        int courseId = int.Parse(Console.ReadLine());
                        Console.Write("Course Name: ");
                        string courseName = Console.ReadLine();
                        Console.Write("Course Code: ");
                        string courseCode = Console.ReadLine();
                        Console.Write("Instructor Name: ");
                        string instructor = Console.ReadLine();

                        Courses course = new Courses
                        {
                            CourseID = courseId,
                            CourseName = courseName,
                            CourseCode = courseCode,
                            InstructorName = instructor
                        };

                        courseDao.SaveCourse(course);
                        Console.WriteLine("Course saved.");
                        break;
                    //To Enroll a Student in a Course
                    case "3":
                        Console.Write("Enter Student ID: ");
                        int sId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int cId = int.Parse(Console.ReadLine());

                        var studentToEnroll = studentDao.GetStudentById(sId);
                        var courseToEnroll = courseDao.GetCourseById(cId);
                        studentDao.EnrollStudentInCourse(studentToEnroll, courseToEnroll);
                        Console.WriteLine("Student enrolled in course.");
                        break;
                    //To Create a Teacher
                    case "4":
                        Console.Write("Teacher ID: ");
                        int teacherId = int.Parse(Console.ReadLine());
                        Console.Write("First Name: ");
                        string tFirst = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string tLast = Console.ReadLine();
                        Console.Write("Email: ");
                        string tEmail = Console.ReadLine();

                        Teacher teacher = new Teacher
                        {
                            TeacherID = teacherId,
                            FirstName = tFirst,
                            LastName = tLast,
                            Email = tEmail
                        };

                        teacherDao.SaveTeacher(teacher);
                        Console.WriteLine("Teacher saved.");
                        break;
                    //To Assign a Teacher to a Course
                    case "5":
                        Console.Write("Enter Teacher ID: ");
                        int tId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int cToAssign = int.Parse(Console.ReadLine());

                        var teacherToAssign = teacherDao.GetTeacherById(tId);
                        var courseToAssign = courseDao.GetCourseById(cToAssign);
                        teacherDao.AssignTeacher(teacherToAssign, courseToAssign);
                        Console.WriteLine("Teacher assigned to course.");
                        break;
                    //To Record a Payment
                    case "6":
                        Console.Write("Enter Student ID: ");
                        int payStudentId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Payment Amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter Payment Date (yyyy-MM-dd): ");
                        DateTime payDate = DateTime.Parse(Console.ReadLine());

                        paymentDao.RecordPayment(payStudentId, amount, payDate);
                        Console.WriteLine("Payment recorded successfully.");
                        break;
                    //To Generate an Enrollment Report
                    case "7":
                        Console.Write("Enter Course ID to generate report: ");
                        int reportCourseId = int.Parse(Console.ReadLine());
                        studentDao.GenerateEnrollmentReport(reportCourseId);
                        break;

                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }


    }
}

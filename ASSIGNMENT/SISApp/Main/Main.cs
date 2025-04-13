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
                StudentID = 102,
                FirstName = "Ajay",
                LastName = "Kumar",
                DateOfBirth = new DateTime(2004, 5, 16),
                Email = "ajay@gmail.com",
                PhoneNumber = "7575473467"
            };
            var newStudent1 = studentDao.SaveStudent(student1);
            Console.WriteLine(newStudent1 != null ? "Student is Saved" : "Error");
            //creating a course

            CoursesDao courseDao = new CoursesDao();
            Courses course1 = new Courses
            {
                //CourseID = 101,
                CourseName = "C#",
                CourseCode = "CS101",
                InstructorName = "Ramesh"
            };
            var newCourse1 = courseDao.SaveCourse(course1);
            Console.WriteLine(newCourse1 != null ? "Course is Saved" : "Error");

            // Enroll student in course
            Console.WriteLine("Enrolling student in course...");
            studentDao.EnrollStudentInCourse(student1, course1);
            Console.WriteLine($"Student {student1.FirstName} {student1.LastName} successfully enrolled in {course1.CourseName}.");
                    
                   
               

            
        }

        
    }
}

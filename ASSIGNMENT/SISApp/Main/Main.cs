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
            Students student = new Students
            {
                StudentID = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1995, 5, 15),
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            };

            Courses course = new Courses
            {
                CourseID = 101,
                CourseName = "Introduction to Programming",
                CourseCode = "CS101",
                InstructorName = "Dr. Smith"
            };

            // Enroll the student in a course
            Console.WriteLine("Enrolling student in course...");
            try
            {
                studentDao.EnrollStudentInCourse(student, course);
                Console.WriteLine($"Student {student.FirstName} {student.LastName} successfully enrolled in {course.CourseName}.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Update student information
            Console.WriteLine("\nUpdating student information...");
            try
            {
                studentDao.UpdateStudentInfo(student.StudentID, "Jane", "Doe", new DateTime(1995, 5, 15), "jane.doe@example.com", "987-654-3210");
                Console.WriteLine("Student information updated successfully.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nProgram execution completed. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

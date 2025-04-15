using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;
using SISApp.Exception;
using Microsoft.Identity.Client;

namespace SISApp.DAO
{
    public class StudentDao : IStudentDao<Students>
    {
        public Students SaveStudent(Students student)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string saveStudent = "INSERT INTO Students (StudentID, FirstName, LastName, DateOfBirth, Email, PhoneNumber) VALUES (@StudentID, @FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";
                    using (SqlCommand cmd = new SqlCommand(saveStudent, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", student.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                        cmd.ExecuteNonQuery();
                        return student;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return null;
            }
            catch (InvalidStudentDataException ex)
            {
                Console.WriteLine($"Invalid Data Error: {ex.Message}");
                return null;
            }
        }

        public void EnrollStudentInCourse(Students student, Courses course)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();

                    // Insert enrollment record
                    string insertSql = "INSERT INTO Enrollment (StudentID, CourseID, EnrollmentDate) VALUES (@studentId, @courseId, @EnrollmentDate)";
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@studentId", student.StudentID);
                        insertCmd.Parameters.AddWithValue("@courseId", course.CourseID);
                        insertCmd.Parameters.AddWithValue("@enrollmentDate", DateTime.Now);

                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while enrolling the student in the course: {ex.Message}");
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine($"Duplicate enrollment error: {ex.Message}");
            }
        }
        //To Get Enrollment Report----
        public void GenerateEnrollmentReport(int courseID)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();

                    // 1 Get CourseName using CourseID
                    string courseName = "";
                    string getCourseSql = "SELECT CourseName FROM Courses WHERE CourseID = @CourseID";
                    using (SqlCommand getCourseCmd = new SqlCommand(getCourseSql, conn))
                    {
                        getCourseCmd.Parameters.AddWithValue("@CourseID", courseID); 
                        var result = getCourseCmd.ExecuteScalar();
                        if (result == null)
                        {
                            Console.WriteLine("Course not found.");
                            return;
                        }
                        courseName = result.ToString();
                    }
                    // 2 Get Enrolled Students
                    string reportSql = @"SELECT S.StudentID, S.FirstName, S.LastName, S.Email, E.EnrollmentDate FROM Students S INNER JOIN Enrollment E ON S.StudentID = E.StudentID WHERE E.CourseID = @CourseID";

                    using (SqlCommand reportCmd = new SqlCommand(reportSql, conn))
                    {
                        reportCmd.Parameters.AddWithValue("@CourseID", courseID);
                        using (SqlDataReader reader = reportCmd.ExecuteReader())
                        {
                            Console.WriteLine($"\n--- Enrollment Report for '{courseName}' ---");
                            Console.WriteLine($"{"Student ID",-12} {"Name",-25} {"Email",-30} {"Enrolled On"}");

                            while (reader.Read())
                            {
                                int studentId = reader.GetInt32(0);
                                string fullName = reader.GetString(1) + " " + reader.GetString(2);
                                string email = reader.GetString(3);
                                DateTime enrollmentDate = reader.GetDateTime(4);

                                Console.WriteLine($"{studentId,-12} {fullName,-25} {email,-30} {enrollmentDate.ToShortDateString()}");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
            }
        }
        //----
        public Students GetStudentById(int studentId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students WHERE StudentID = @StudentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Students
                                {
                                    StudentID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    Email = reader.GetString(4),
                                    PhoneNumber = reader.GetString(5)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error fetching student: {ex.Message}");
            }
            return null;
        }



    }
}

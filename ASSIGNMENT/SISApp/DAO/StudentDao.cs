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
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader? dr;

        public Students SaveStudent(Students student)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "INSERT INTO Students (StudentID,FirstName, LastName, DateOfBirth, Email, PhoneNumber) VALUES (@StudentID,@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
                return student;
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
        //To Get Enrollment Report
        public void GenerateEnrollmentReport(int courseID)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();

                    // Step 1: Get CourseName using CourseID
                    string courseName = "";
                    string getCourseSql = "SELECT CourseName FROM Courses WHERE CourseID = @CourseID";
                    using (SqlCommand getCourseCmd = new SqlCommand(getCourseSql, conn))
                    {
                        getCourseCmd.Parameters.AddWithValue("@CourseID", courseID); // make sure courseId is provided
                        var result = getCourseCmd.ExecuteScalar();
                        if (result == null)
                        {
                            Console.WriteLine("Course not found.");
                            return;
                        }
                        courseName = result.ToString();
                    }
                    // Step 2: Get Enrolled Students
                    string reportSql = @"SELECT S.StudentID, S.FirstName, S.LastName, S.Email, E.EnrollmentDate FROM Students S INNER JOIN Enrollments E ON S.StudentID = E.StudentID WHERE E.CourseID = @CourseID";

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


    }
}

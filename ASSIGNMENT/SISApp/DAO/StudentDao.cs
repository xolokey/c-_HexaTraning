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
                cmd.CommandText = "INSERT INTO Students (FirstName, LastName, DateOfBirth, Email, PhoneNumber) VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";
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

                    // 1. Check for duplicate enrollment
                    string checkSql = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @studentId AND CourseID = @courseId";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@studentId", student.StudentID);
                        checkCmd.Parameters.AddWithValue("@courseId", course.CourseID);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            throw new DuplicateEnrollmentException("Student is already enrolled in this course.");
                        }
                    }

                    // 2. Insert enrollment record
                    string insertSql = "INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES (@studentId, @courseId, @EnrollmentDate)";
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

        public Students UpdateStudentInfo(Students student)
        {
            try
            {
                cmd.Connection = sqlCon;
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder = queryBuilder.Append($"update Students set FirstName='{student.FirstName}',LastName={student.LastName},DateOfBirth='{student.DateOfBirth}',Email={student.Email},PhoneNumber ='{student.PhoneNumber}' where StudentId={student.StudentID}");
                // queryBuilder = queryBuilder.Append($"update Students set FirstName='{students.FirstName}',LastName={students.LastName},DateOfBirth='{students.DateOfBirth}',Email='{students.Email}',PhoneNumber ='{students.}' where ProductId={students.LastName}");
                cmd.CommandText = queryBuilder.ToString();
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();//insert/delete/update
                
                return student;

            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (InvalidStudentDataException ex)
            {
                return null;
            }

        }
        public Students GetStudentDetails(Students student)
        {

        }
    }
}

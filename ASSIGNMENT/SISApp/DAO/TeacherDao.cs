using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;
using SISApp.Exception;

namespace SISApp.DAO
{
    public class TeacherDao : ITeacherDao<Teacher>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public Teacher SaveTeacher(Teacher teacher)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "INSERT INTO Teacher (TeacherID,FirstName, LastName, Email) VALUES (@TeacherID,@FirstName, @LastName, @Email)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
                return teacher;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return null;
            }
        }
        //Method to Assign a teacher to a course
        public void AssignTeacher(Teacher teacher, Courses course)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string fullName=$"{teacher.FirstName} {teacher.LastName}";

                    // Insert enrollment record
                    string insertSql = "UPDATE Courses SET InstructorName = @fullname WHERE CourseID = @CourseCode";
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@fullName", fullName);
                        insertCmd.Parameters.AddWithValue("@courseCode", course.CourseID);

                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while Assigning the Teacher in the course: {ex.Message}");
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine($"Duplicate enrollment error: {ex.Message}");
            }
        }

    }
}

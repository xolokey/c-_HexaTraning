using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;
using SISApp.Exception;

namespace SISApp.DAO
{
    public class CoursesDao: ICoursesDao<Courses>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Courses SaveCourse(Courses course)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "INSERT INTO Courses (CourseName, CourseCode, InstructorName) VALUES (@CourseName, @CourseCode, @InstructorName)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.Parameters.AddWithValue("@InstructorName", course.InstructorName);

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
                return course;
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

    }
}

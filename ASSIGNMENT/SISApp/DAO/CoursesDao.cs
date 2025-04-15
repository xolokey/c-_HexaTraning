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
        //SqlDataReader dr;
        public Courses SaveCourse(Courses course)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "INSERT INTO Courses (CourseID,CourseName, CourseCode, InstructorName) VALUES (@CourseID,@CourseName, @CourseCode, @InstructorName)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
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

        //Get Course by ID--------------
        public void DisplayCourseInfo(Courses course)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();

                    string selectSql = "SELECT CourseID, CourseName, CourseCode, InstructorName FROM Courses WHERE CourseID = @CourseID";
                    using (SqlCommand selectCmd = new SqlCommand(selectSql, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@CourseID", course.CourseID);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                course.CourseID = reader.GetInt32(0);
                                course.CourseName = reader.GetString(1);
                                course.CourseCode = reader.GetString(2);
                                course.InstructorName = reader.IsDBNull(3) ? null : reader.GetString(3);

                                Console.WriteLine($"Course ID: {course.CourseID}");
                                Console.WriteLine($"Course Name: {course.CourseName}");
                                Console.WriteLine($"Course Code: {course.CourseCode}");
                                Console.WriteLine($"Instructor Name: {course.InstructorName}");
                            }
                            else
                            {
                                Console.WriteLine("Course not found.");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the course info: {ex.Message}");
            }
        }

        public Courses GetCourseById(int courseId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", courseId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Courses
                                {
                                    CourseID = reader.GetInt32(0),
                                    CourseName = reader.GetString(1),
                                    CourseCode = reader.GetString(2),
                                    InstructorName = reader.GetString(3)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error fetching course: {ex.Message}");
            }
            return null;
        }


    }
}

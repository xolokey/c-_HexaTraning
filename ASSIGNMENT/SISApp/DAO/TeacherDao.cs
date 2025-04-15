using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;
using SISApp.Exception;
using System.Collections.Generic;

namespace SISApp.DAO
{
    public class TeacherDao : ITeacherDao<Teacher>
    {
        public Teacher SaveTeacher(Teacher teacher)
        {
            try
            {
                using (SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    sqlCon.Open();
                    string saveteacher = "INSERT INTO Teacher(TeacherID, FirstName, LastName, Email) VALUES(@TeacherID, @FirstName, @LastName, @Email)";
                    using (SqlCommand cmd = new SqlCommand(saveteacher,sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                        cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                        cmd.Parameters.AddWithValue("@Email", teacher.Email);
                       
                        cmd.ExecuteNonQuery();
                        return teacher;
                    }
                }
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
        public Teacher GetTeacherById(int teacherId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string selectSql = "SELECT TeacherID, FirstName, LastName, Email FROM Teacher WHERE TeacherID = @TeacherID";
                    using (SqlCommand selectCmd = new SqlCommand(selectSql, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@TeacherID", teacherId);
                        SqlDataReader dr = selectCmd.ExecuteReader();
                        if (dr.Read())
                        {
                            return new Teacher
                            {
                                TeacherID = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Email = dr.GetString(3)
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            return null;
        }

    }
}

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;

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
                cmd.CommandText = "INSERT INTO Teachers (FirstName, LastName, Email) VALUES (@fName, @Lname, @Email)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@Name", teacher.FirstName);
                cmd.Parameters.AddWithValue("@Lname", teacher.LastName);
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
    }
}

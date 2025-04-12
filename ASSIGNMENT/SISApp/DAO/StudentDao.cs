using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;

namespace SISApp.DAO
{
    public class StudentDao : IStudentDao<Students>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;


    }
}

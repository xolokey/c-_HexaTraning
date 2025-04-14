using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISApp.Entities;

namespace SISApp.DAO
{
    public interface ITeacherDao<T>
    {
        T SaveTeacher(T teacher);
        void AssignTeacher(Teacher teacher, Courses course);
    }
}

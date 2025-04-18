using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISApp.Entities;

namespace SISApp.DAO
{
    public interface ICoursesDao<T>
    {
        T SaveCourse(T course);
        void DisplayCourseInfo(Courses course);

        T GetCourseById(int courseId);


    }
}

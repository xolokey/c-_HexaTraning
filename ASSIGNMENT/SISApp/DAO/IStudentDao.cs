﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISApp.Entities;

namespace SISApp.DAO
{
    public interface IStudentDao<T>
    {
        void EnrollStudentInCourse(Students student, Courses course);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.DAO
{
    public interface ICoursesDao<T>
    {
        T SaveCourse(T course);

    }
}

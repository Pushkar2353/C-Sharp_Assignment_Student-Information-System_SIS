﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS_Library.SIS.Exception
{
    public class TeacherNotFoundException : ApplicationException
    {
        public TeacherNotFoundException(string message) : base(message) { }
    }
}

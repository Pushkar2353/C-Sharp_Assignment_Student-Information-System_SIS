﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS_Library.SIS.Exception
{
    public class InvalidStudentDataException : ApplicationException
    {
        public InvalidStudentDataException(string message) : base(message) { }
    }
}

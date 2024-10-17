using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS_Library.SIS.Exception
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}

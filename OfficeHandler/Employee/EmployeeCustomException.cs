using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    [Serializable()]
    public class EmployeeCustomException : Exception
    {
        public EmployeeCustomException() : base()
        {
            HelpLink = "http\\www.google.se";
        }
        public EmployeeCustomException(string message) : base(message)
        {
            HelpLink = "http\\www.google.se";
        }
        public EmployeeCustomException(string message, Exception inner) : base(message, inner)
        {
            HelpLink = "http\\www.google.se";
        }
    }
}

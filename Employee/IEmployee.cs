using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public interface IEmployee
    {
        int ID { get; set; }
        string Title { get; set; }
        void EmployeeInformation();
    }
}

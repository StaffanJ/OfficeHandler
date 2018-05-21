using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public abstract class Salary
    {
        public abstract void CalculateSalary(Employees employee, int hours, double bonus = 1.0);
    }
}

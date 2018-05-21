using System;
using EmployeeMethods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EmployeeLogic.CreateEmployee();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            Console.ReadLine();
        }
    }
}

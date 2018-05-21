using System;
using Employee;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class EmployeeEventArgs : EventArgs
    {
        public EmployeeEventArgs(Employees employee)
        {
            Employees = employee;
            Date = DateTime.Now;
        }

        public Employees Employees { get; set; }
        public DateTime Date { get; set; }
    }

    public class EmployeePublisher
    {
        public EventHandler<EmployeeEventArgs> EmployeeEvent;
        
        public void EmployeeAdded(Employees employee)
        {
            OnRaiseEvent(new EmployeeEventArgs(employee), "added");
        }

        public void EmployeeDeleted(Employees employee)
        {
            OnRaiseEvent(new EmployeeEventArgs(employee), "removed");
        }

        protected virtual void OnRaiseEvent(EmployeeEventArgs args, string message)
        {
            EmployeeEvent?.Invoke(this, args);
            Console.WriteLine($"\n{args.Employees.Name} was {message}!\n");
        }
    }

    public class EmployeeAddedSubscriber
    {
        public EmployeeAddedSubscriber(EmployeePublisher pub)
        {
            pub.EmployeeEvent = HandleAddedEmployee;
        }

        public void HandleAddedEmployee(object obj, EmployeeEventArgs args)
        {
            Console.WriteLine($"\nEmployee {args.Employees.Name} was added\n" +
                $"Number of employees: { Employees.NumberOfEmployees}");
        }
    }
}

using Employee.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    [Author("S.Jansson")]
    [Author("S.Jansson", Version = 2.0), Author("A.Benskiöld", Version = 3.0)]
    public class Employees : Salary, IEmployee, ICitizen
    {
        // Read fields
        public static int NumberOfEmployees;
        private static int counter;
        private EmployeePublisher employeePublisher;

        // Read-write instance property:
        public string Name { get; set; }
        public Guid SSN { get; set; }
        public Genders Gender { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        private readonly double baseSalary = 120;

        // A Constructor:
        public Employees()
        {
            // Calculate the employee's number:
            counter++;
            NumberOfEmployees = counter;
            ID = counter;

            //Console.WriteLine($"Counter {counter}");

            employeePublisher = new EmployeePublisher();
            employeePublisher.EmployeeEvent += DelegateEventEmployeeAdded;
        }

        //Methods for handling employees.
        /// <summary>
        /// Displays the employee information about the current employee.
        /// </summary>
        public void EmployeeInformation()
        {
            Console.WriteLine($"\nName: {this.Name}, Employee ID: {this.ID}");
        }

        /// <summary>
        /// Displays the citizen information about the first citizen found in the search.
        /// </summary>
        public void CitizenInformation()
        {
            Console.WriteLine($"\nName {this.Name}, SSN {this.SSN}, Gender {this.Gender}");
        }

        /// <summary>
        /// Shows the salary about the current employee, 
        /// it takes the Employee, 
        /// how many hours they've worked and a bonus.
        /// </summary>
        /// <param name="employee">Employee type</param>
        /// <param name="hours">Int type</param>
        /// <param name="bonus">Double type</param>
        public override void CalculateSalary(Employees employee, int hours, double bonus = 1.0)
        {
            double salary = hours * baseSalary * bonus;
            string bonusTrue = "no bonus";

            if(bonus > 1.0)
            {
                bonusTrue = "bonus";
            }

            Console.WriteLine($"\n{employee.Name} earns  {salary}, they have {bonusTrue}\n");
        }

        /// <summary>
        /// The method that matches the event signature.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        public void DelegateEventEmployeeAdded(object obj, EmployeeEventArgs args)
        {
            //Console.WriteLine($"We are calling the event");
        }

        /// <summary>
        /// Notifes the user when an employee have been added.
        /// Subscribes to EmployeeEvent
        /// </summary>
        public void EmployeeAdded()
        {
            employeePublisher.EmployeeAdded(this);
        }

        /// <summary>
        /// Notifies a user when an employee have been removed.
        /// Subscribes to EmployeeEvent
        /// </summary>
        public void EmployeeDeleted()
        {
            employeePublisher.EmployeeDeleted(this);
        }

        public bool Equals(Employees employees)
        {
            if (this.Name.GetHashCode() == employees.Name.GetHashCode())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Overrides the ToString method
        /// </summary>
        /// <returns>Returns a string with information about the current Employee</returns>
        public override string ToString()
        {
            return $"\n\nName: {this.Name}\n" +
                $"Gender: {this.Gender}\n" +
                $"SSN: {this.SSN}\n" +
                $"Title: {this.Title}\n" +
                $"Employee ID: {this.ID}\n";
        }
    }
}

using System;
using Employee;
using Employee.CustomAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Runtime.CompilerServices;

namespace EmployeeMethods
{
    public class EmployeeLogic
    {

        /// <summary>
        /// Create the employees and adds them.
        /// </summary>
        public static void CreateEmployee()
        {
            Console.Write("How many employees do you want to hire? ");
           
            //Checks to see if the input is a number, if not trows an exception.
            bool isNumber = int.TryParse(Console.ReadLine(), out int Hire);

            //Creates a list of employees from the FillEmployeeList method.
            IList<Employees> employees = FillEmployeeList();

            //A try catch block, tries to add employees to the list and in the end write to the JSON file
            try
            {
                Console.WriteLine($"There is a total of {Employees.NumberOfEmployees} in the company\n");
                //Is the isNumber bool is true it continues, else it throws an exception with an error message.
                if (isNumber)
                {
                    for (int i = 0; i < Hire; i++)
                    {
                        Console.Write("Type in the name: ");
                        string name = ConvertWord(Console.ReadLine());
                        employees = Employee(employees, name);
                    }
                    //Writes the list to a JSON file
                    WriteToFile(employees);

                    //If the number of employees are greater than 0 it shows info about the employees.
                    if (Employees.NumberOfEmployees > 0)
                        ShowEmployeeInfo(employees);
                }
                //The else statement that throws the exception with the message.
                else
                    throw new EmployeeCustomException("You must type in a number");
            }
            //Catch block that throws a new exception message.
            catch (EmployeeCustomException ex)
            {
                throw new ArgumentOutOfRangeException(ex.Message, ex);
            }

        }
        
        /// <summary>
        /// Fills the employee list in CreateEmployee method.
        /// </summary>
        /// <returns></returns>
        private static IList<Employees> FillEmployeeList()
        {
            //Create a new list with employees
            IList<Employees> employees = new List<Employees>();

            //Try catch block that tries to fill the list with data from the JsonConverter, if it dosen't exists it catches a IOException.
            try
            {
                //Assigns the list of Employees from the JSON converter and adds it to the list.
                employees = JsonConvert.DeserializeObject<List<Employees>>(File.ReadAllText(@"C:\Users\Staffan\Source\Repos\DelegateEvents2\data.json"));
            }
            //Catch block that is thrown if the file dosen't exists.
            catch (IOException)
            {
                Console.WriteLine("Could not find the file, the list is a new one,\n");
            }
            //A general exception handler, just throws a message that something went wrong, add more functionality if needed
            catch (Exception)
            {
                Console.WriteLine("Something went wrong, please try again!\n");
            }
            //Returns the list to the caller.
            return employees;
        }

        /// <summary>
        /// Shows information about the employees.
        /// </summary>
        /// <param name="employees"></param>
        private static void ShowEmployeeInfo(IList<Employees> employees)
        {
            Console.WriteLine("Employees:\n");
            //For each employee it shows the information about them.
            foreach (Employees item in employees)
            {
                //Calls the employeeinformation method from the Employee class
                item.EmployeeInformation();

                //Checks if the gender is female, if true it gives a bonus
                //Else it dosen't give a bonus
                if (item.Gender == Genders.Female)
                {
                    item.CalculateSalary(item, 120, 1.20);
                }
                else
                    item.CalculateSalary(item, 120);
            }

            //Calls the citizeninfo method.
            CitizenInfo(employees);
        }

        /// <summary>
        /// Adds employees to the "database"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static IList<Employees> Employee(IList<Employees> employees, string name)
        {
            Console.Write($"\nWhat gender is {name}? ");
            
            //Creates a string that calls the ConvertWord method with Console.ReadLine
            //It then converts the word with a starting capital letter and the other lowercase.
            string gender = ConvertWord(Console.ReadLine());

            //Instantize a Genders enum type.
            Genders genderType = new Genders();

            //A switch case that determains the gender of the employee, if no valid text is given it defaults to male.
            switch (gender)
            {
                case "Male":
                    genderType = Genders.Male;
                    break;
                case "Female":
                    genderType = Genders.Female;
                    break;
                default:
                    Console.WriteLine("Unknown gender, male is default!");
                    genderType = Genders.Male;
                    break;
            }

            //Create a new employee object
            Employees employee = new Employees
            {
                Name = name,
                SSN = Guid.NewGuid(),
                Gender = genderType,
                Title = "Employee"
            };

            //Calls the CheckToSeeIfEmployeeExits method, if it dosen't it adds to the list.
            employees = CheckToSeeIfEmployeeExists(employees, employee, name);
                
            return employees;
        }

        /// <summary>
        /// Checks to see if the employee exists in the list (Might be a more effecient method?)
        /// </summary>
        /// <param name="employees"></param>
        /// <param name="employee"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static IList<Employees> CheckToSeeIfEmployeeExists(IList<Employees> employees, Employees employee, string name)
        {
            //Check to see if the employees are lesser or equal to 0, if it is it adds the employee to the list and calls the Event EmployeeAdded.
            if (employees.Count <= 0)
            {
                employees.Add(employee);
                employee.EmployeeAdded();
            }
            //If the number of employees are greater than 0 it checks to see if the name is equals to the name of the new employee.
            //If it is it is true it writes out a message and breaks the loop.
            else
            {
                bool userExits = false;
                int intCounter = employees.Count;
                for (int i = 0; i < intCounter; i++)
                {
                    for (int j = 0; j < intCounter; j++)
                    {
                        if(employees[j].Name == name)
                        {
                            Console.WriteLine("\nThere already exits a user with that name");
                            userExits = true;
                            break;
                        }
                    }
                    if (userExits != true)
                    {
                        employees.Add(employee);
                        employee.EmployeeAdded();
                        break;
                    }
                    else
                        break;
                }
            }
            //Returns the list to the caller.
            return employees;
        }

        /// <summary>
        /// Asks if the user wants to remove an employee from the list, if yes it geos to RemoveEmployee if no, it returns
        /// </summary>
        /// <param name="employees"></param>
        private static void RemoveEmployees(IList<Employees> employees)
        {
            Console.Write("\nDo you wish to remove an employee? ");
            //Creates a string that calls the ConvertWord method with Console.ReadLine
            //It then converts the word with a starting capital letter and the other lowercase. 
            string answer = ConvertWord(Console.ReadLine());

            //Switch statement that handles the answer string, if it is yes it goes into the RemoveEmployee method.
            //If it is anything other than yes it returns to the caller.
            switch (answer)
            {
                case "Yes":
                    RemoveEmployee(employees);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Asks the user to type in the name of the employee they want to remove.
        /// If the name they type in dosen't exist 
        /// </summary>
        /// <param name="employees"></param>
        private static void RemoveEmployee(IList<Employees> employees)
        {
            Console.Write("Write in the employee you want to remove: ");
            //Creates a string that calls the ConvertWord method with Console.ReadLine
            //It then converts the word with a starting capital letter and the other lowercase.
            string name = ConvertWord(Console.ReadLine());

            //Create a implicit typed local using a LINQ statement.
            var findEmployee = from emp in employees
                               where emp.Name == name
                               select emp;

            //If the findEmployee type is lesser or equals to 0 it writes an error message.
            if (findEmployee.Count() <= 0)
                Console.WriteLine("We could not find the employee you where looking for\n");
            //Else it starts a statement that goes starts a remove statement.
            else
            {
                Console.Write($"Are you sure you want to remove {findEmployee.First().Name}\n");
                //Creates a string that calls the ConvertWord method with Console.ReadLine
                //It then converts the word with a starting capital letter and the other lowercase.
                string answer = ConvertWord(Console.ReadLine());

                //Switch statement that takes the answer string and removes the first employee it finds with the name. (There must be a more efficient way, look into it later)
                switch (answer)
                {
                    case "Yes":
                        findEmployee.First().EmployeeDeleted();
                        employees.Remove(findEmployee.First());
                        break;
                    default:
                        return;
                }
            }

            //Writes the new list to the file.
            WriteToFile(employees);
        }

        /// <summary>
        /// Asks if the users wants to show citizen information.
        /// Also uses named parameters.
        /// </summary>
        /// <param name="banan"></param>
        private static void CitizenInfo(IList<Employees> banan)
        {
            Console.Write("Do you wish to see the citizen information? ");
             //Creates a string that calls the ConvertWord method with Console.ReadLine
            //It then converts the word with a starting capital letter and the other lowercase.
            string answer = ConvertWord(Console.ReadLine());

            //Switch statement that takes the answer string.
            //If it is yes it calls the ShowCitizenInfo method.
            //If no it returns to the caller. Default is a message that writes out and calls the method again.
            switch (answer)
            {
                case "Yes":
                    //An example of a named parameter.
                    ShowCitizenInfo(employees: banan);
                    break;
                case "No":
                    return;
                default:
                    Console.WriteLine("You must type in either yes or no!");
                    CitizenInfo(banan);
                    break;
            }

            //Calls the RemoveEmployees method.
            RemoveEmployees(banan);
        }

        /// <summary>
        /// Shows information about the citizen
        /// </summary>
        /// <param name="employees"></param>
        private static void ShowCitizenInfo(IList<Employees> employees)
        {
            Console.Write("Type in the name of the person you want to find: ");

            //Creates a string that calls the ConvertWord method with Console.ReadLine
            //It then converts the word with a starting capital letter and the other lowercase.
            string name = ConvertWord(Console.ReadLine());

            //Create a implicit typed local using a LINQ statement.
            var findCitizen = from emp in employees
                              where emp.Name == name
                              select emp;

            //If findCitizen is lesser or equals to 0 it writes out an error message.
            if (findCitizen.Count() <= 0)
                Console.WriteLine("We could not find the person you where looking for");
            //Else it types out the info about citizen that we found in the findCitizen var.
            else
            {
                foreach (var item in findCitizen)
                {
                    item.CitizenInformation();
                }
            }
        }

        /// <summary>
        /// Writes to the JSON file by using File.WriteAllText, using Newtonsoft.Json extention
        /// </summary>
        /// <param name="employees"></param>
        private static void WriteToFile(IList<Employees> employees)
        {
            //Writes to the file, using JsonConvert.SerializeObject, also indents the JSON file.
            File.WriteAllText(@"C:\Users\Staffan Jansson\Documents\GitHub\OfficeHandler\OfficeHandler\Files\data.json", JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        /// A method for reworking words
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        static string ConvertWord(string word)
        {
            try
            {
                if (word.Length <= 0)
                {
                    throw new EmployeeCustomException("There must be a name");
                }

                return char.ToUpper(word[0]) + word.Substring(1).ToLower();

            }
            catch (EmployeeCustomException ex)
            {
                throw new ArgumentOutOfRangeException(ex.Message, ex);
                //Console.WriteLine(ex.Message);
                //CreateEmployee();
            }
        }

    }
}

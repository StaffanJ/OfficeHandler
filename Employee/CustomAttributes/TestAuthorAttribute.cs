#define TRACE_ON
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CustomAttributes
{
    public class TestAuthorAttribute
    {
        //A test method that calls the PrintAuthorInfo method with the typeof Employee
        public static void Test()
        {
            PrintAuthorInfo(typeof(Employees));
        }

        /// <summary>
        /// Different caller methods, use to display the filepath, line number of the code and the member that calls it.
        /// Also checks the Custom Attributes of the Employee class, from the test method
        /// </summary>
        /// <param name="t"></param>
        /// <param name="filename"></param>
        /// <param name="codeline"></param>
        /// <param name="membername"></param>
        private static void PrintAuthorInfo(Type t, [CallerFilePath] string filename = "", [CallerLineNumber] int codeline = 0, [CallerMemberName] string membername = "")
        {
            //Display the info about the callers
            Console.WriteLine($"membername: {membername}");

            Console.WriteLine($"codeline: {codeline}");

            //Prints out the name of the Type
            Console.WriteLine($"Author info for {t.Name}");

            //Creates an array of custom attributes.
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            //Prints the trace message if TRACE_ON is defined
            Trace.Msg($"Inside TestAuthorAttribute");

            //Goes through the attrs array
            foreach (var attr in attrs)
            {
                //If attr is an author attribute, it display the name of author and what version it is.
                if (attr is Author a)
                {
                    Console.WriteLine($"{a.GetName()} version: {a.Version}");
                }
            }
        }
    }
}

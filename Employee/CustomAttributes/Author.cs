using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Author : Attribute
    {
        private readonly string name;
        public double Version { get; set; }

        public Author(string name)
        {
            this.name = name;
            Version = 1.0;
        }

        public string GetName()
        {
            return name;
        }
    }
}

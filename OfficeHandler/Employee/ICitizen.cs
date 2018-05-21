using System;
using Employee;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public interface ICitizen
    {
        Guid SSN { get; set; }
        string Name { get; set; }
        Genders Gender { get; set; }
        void CitizenInformation();
    }
    [Flags]
    public enum Genders
    {
        None,
        Female,
        Male
    }
}

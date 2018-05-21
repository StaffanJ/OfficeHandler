using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class FormattedAddress : IEnumerable<string>
    {
        private List<string> internalList = new List<string>();

        public IEnumerator<string> GetEnumerator() => internalList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();

        public void Add(Employees employees, string address, string street, string zipcode) => internalList.Add(
            $@"{employees.Name} {employees.SSN} " +
            $"{address} " +
            $"{street} {zipcode}"
            );

    }
}

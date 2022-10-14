using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTask.DeepCloning
{
    public class Employee : IEquatable<Employee>
    {
        public string EmployeeName { get; set; }

        public bool Equals(Employee? other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return (this, other) switch
            {
                (_, null) => false,
                _ => EmployeeName.Equals(other.EmployeeName),
            };
        }
    }
}

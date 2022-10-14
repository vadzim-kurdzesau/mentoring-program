using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTask.Binary
{
    [Serializable]
    public class Department : IEquatable<Department>
    {
        public string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; }

        public bool Equals(Department? other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return (this, other) switch
            {
                (_, null) => false,
                _ => DepartmentName.Equals(other.DepartmentName)
                    && Employees.SequenceEqual(other.Employees)
            };
        }
    }
}

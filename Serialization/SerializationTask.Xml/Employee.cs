using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializationTask.Xml
{
    [XmlType(TypeName = "employee")]
    public class Employee: IEquatable<Employee>
    {
        [XmlElement("employeeName")]
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

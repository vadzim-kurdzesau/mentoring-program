using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTask.Json
{
    public class Department
    {
        public string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}

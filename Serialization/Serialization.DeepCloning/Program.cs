namespace SerializationTask.DeepCloning
{
    public class Program
    {
        public static void Main()
        {
            var department = new Department
            {
                DepartmentName = "TestDepartment",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "TestEmployee1" },
                    new Employee { EmployeeName = "TestEmployee2" },
                    new Employee { EmployeeName = "TestEmployee3" },
                }
            };

            var copy = department.Clone() as Department;
            if (!department.Equals(copy))
            {
                throw new Exception("Copied object does not equal the original.");
            }

            if (ReferenceEquals(department, copy))
            {
                throw new Exception("Copied object is the same instance as original.");
            }
        }
    }
}
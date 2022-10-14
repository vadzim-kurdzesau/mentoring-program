using System.Text.Json;

namespace SerializationTask.Json
{
    public class Program
    {
        public static void Main()
        {
            // Arrange
            const string serializationFileName = "SerializedJsonDepartment.json";
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

            // Act
            using (var fileStream = new FileStream(serializationFileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, department);
            }

            // Assert
            using (var fileStream = new FileStream(serializationFileName, FileMode.Open))
            {
                var actual = JsonSerializer.Deserialize<Department>(fileStream);
                if (!department.Equals(actual))
                {
                    throw new Exception("Deserialized object does not equal the original.");
                }
            }
        }
    }
}
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationTask.Binary
{
    public class Program
    {
        public static void Main()
        {
            // Arrange
            const string serializationFileName = "SerializedXmlDepartment.bin";
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
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, department);
            }

            // Assert
            using (var fileStream = new FileStream(serializationFileName, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var actual = (Department)binaryFormatter.Deserialize(fileStream);
                if (!department.Equals(actual))
                {
                    throw new Exception("Deserialized object does not equal the original.");
                }
            }
        }
    }
}
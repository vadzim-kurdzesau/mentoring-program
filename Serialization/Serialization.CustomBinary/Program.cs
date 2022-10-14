using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization.CustomBinary
{
    public class Program
    {
        public static void Main()
        {
            // Arrange
            const string serializationFileName = "CustomSerializedXmlDepartment.bin";
            var user = new User
            {
                Name = "Vadzim",
                BirthDay = new DateTime(2003, 2, 12)
            };

            // Act
            using (var fileStream = new FileStream(serializationFileName, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, user);
            }

            // Assert
            using (var fileStream = new FileStream(serializationFileName, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var actual = (User)binaryFormatter.Deserialize(fileStream);
                if (!user.Equals(actual))
                {
                    throw new Exception("Deserialized object does not equal the original.");
                }
            }
        }
    }
}
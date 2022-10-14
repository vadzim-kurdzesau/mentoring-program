using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.CustomBinary
{
    [Serializable]
    public class User : ISerializable, IEquatable<User>
    {
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public User()
        {
        }

        public User(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            Name = (string)info.GetValue("name", typeof(string));
            BirthDay = (DateTime)info.GetValue("birthday", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name, typeof(string));
            info.AddValue("birthday", BirthDay, typeof(DateTime));
        }

        public bool Equals(User? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return (this, other) switch
            {
                (_, null) => false,
                _ => Name.Equals(other.Name)
                    && BirthDay.Equals(other.BirthDay),
            };
        }
    }
}

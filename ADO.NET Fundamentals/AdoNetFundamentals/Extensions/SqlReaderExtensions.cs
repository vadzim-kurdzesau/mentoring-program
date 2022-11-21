using AdoNetFundamentals.Models;
using System.Data.SqlClient;

namespace AdoNetFundamentals.Extensions
{
    internal static class SqlReaderExtensions
    {
        public static T? GetValueOrDefault<T>(this SqlDataReader dataReader, int index)
        {
            return !dataReader.IsDBNull(index) ? (T)dataReader.GetValue(index) : default;
        }

        public static Product ToProduct(this SqlDataReader reader)
        {
            return new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Weight = reader.GetValueOrDefault<float>(3),
                Height = reader.GetValueOrDefault<float>(4),
                Width = reader.GetValueOrDefault<float>(5),
                Length = reader.GetValueOrDefault<float>(6)
            };
        }
    }
}

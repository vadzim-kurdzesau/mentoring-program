using Dapper;
using System.Data.SqlClient;

namespace OrmFundamentals.Dapper.Extensions
{
    internal static class SqlConnectionExtensions
    {
        public static bool Exists<T>(this SqlConnection connection, int id)
        {
            return connection.ExecuteScalar<bool>($"SELECT COUNT(1) FROM {typeof(T).Name}s WHERE Id=@id", new { id });
        }
    }
}

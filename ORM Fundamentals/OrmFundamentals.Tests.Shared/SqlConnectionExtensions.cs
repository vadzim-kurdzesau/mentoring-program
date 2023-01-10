using System.Data.SqlClient;

namespace OrmFundamentals.Tests.Shared
{
    public static class SqlConnectionExtensions
    {
        public static void ClearTable(this SqlConnection connection, string tableName)
        {
            using (connection)
            {
                var deleteCommand = new SqlCommand($"DELETE \"{tableName}\"", connection);
                var reseedComand = new SqlCommand($"DBCC CHECKIDENT ('{tableName}', RESEED, 0);", connection);

                connection.Open();

                deleteCommand.ExecuteNonQuery();
                reseedComand.ExecuteNonQuery();
            }
        }
    }
}

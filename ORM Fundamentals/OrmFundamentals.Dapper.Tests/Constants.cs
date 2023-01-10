namespace OrmFundamentals.Dapper.Tests
{
    internal static class Constants
    {
        public static readonly string ConnectionString = GetConnectionString();

        private static string GetConnectionString()
        {
            var path = Directory.GetCurrentDirectory();
            var testsFolderIndex = path.IndexOf("OrmFundamentals.Dapper.Tests");
            var result = Path.Combine(path[..testsFolderIndex], $"OrmFundamentals.Dapper.Tests{Path.DirectorySeparatorChar}Database{Path.DirectorySeparatorChar}OrmFundamentals.Dapper.Database.mdf");

            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{result}\";Integrated Security=True";
        }
    }
}

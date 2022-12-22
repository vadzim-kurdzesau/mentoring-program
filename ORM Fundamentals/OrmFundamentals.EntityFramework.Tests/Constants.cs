namespace OrmFundamentals.EntityFramework.Tests
{
    internal static class Constants
    {
        public static readonly string ConnectionString = GetConnectionString();

        private static string GetConnectionString()
        {
            var path = Directory.GetCurrentDirectory();
            var testsFolderIndex = path.IndexOf("OrmFundamentals.EntityFramework.Tests");
            var result = Path.Combine(path[..testsFolderIndex], $"OrmFundamentals.EntityFramework.Tests{Path.DirectorySeparatorChar}Database{Path.DirectorySeparatorChar}OrmFundamentals.EntityFramework.Database.mdf");

            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{result}\";Integrated Security=True";
        }
    }
}

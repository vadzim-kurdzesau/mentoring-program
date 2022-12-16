﻿namespace AdoNetFundamentals.Tests
{
    internal static class Constants
    {
        public static readonly string ConnectionString = GetConnectionString();

        private static string GetConnectionString()
        {
            var path = Directory.GetCurrentDirectory();
            var testsFolderIndex = path.IndexOf("AdoNetFundamentals.Tests");
            var result = Path.Combine(path[..testsFolderIndex], $"AdoNetFundamentals.Tests{Path.DirectorySeparatorChar}Database{Path.DirectorySeparatorChar}AdoNetFundamentalsTestDb.mdf");

            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{result}\";Integrated Security=True";
        }
    }
}

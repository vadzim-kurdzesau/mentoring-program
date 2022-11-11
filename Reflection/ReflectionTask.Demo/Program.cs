using System;
using System.IO;

namespace ReflectionTask.Demo
{
    internal class Program
    {
        private static void Main()
        {
            var pluginsFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Plugins");

            var configuration = new Configuration(pluginsFolderPath);

            configuration.LoadSettings();

            PrintConfiguration(configuration);

            Console.Write("New password: ");
            configuration.Password = Console.ReadLine();

            Console.Write("New balance: ");
            configuration.Balance = float.Parse(Console.ReadLine());

            configuration.SaveSettings();
            
            PrintConfiguration(configuration);
        }

        private static void PrintConfiguration(Configuration configuration)
        {
            Console.WriteLine($"User '{configuration.Username}': {configuration.Age} years, Balance: {configuration.Balance:C}, Password: '{configuration.Password}'.");
        }
    }
}